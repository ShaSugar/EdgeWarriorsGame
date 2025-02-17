using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


public enum ERealUnitType
{
    EYu = 1, // 鳄鱼
    FeiLong = 2, // 飞龙
    ZongXiong = 3, // 棕熊
    HaiDao = 4, // 海盗
    Boss = 5, // BOSS
    BossHitPoint = 6, // Boss击打点
    Monster100 = 7, // 怪物100
    Monster101 = 8, // 怪物101
    MAX = 9,
}

public enum EUnitType
{
    monsterType1 = 1,
    monsterType2 = 2,
    monsterTypeBoss = 3,
    unitBossHitPoint = 4,
}

public class UnitMgr : UnitySingleton<UnitMgr>
{
    /// <summary>
    /// 怪物攻击玩家事件
    /// </summary>
    public const string UnitAttackEvent = "UnitMgr_UnitAttackEvent";

    /// <summary>
    /// 怪物走完路径消失事件
    /// </summary>
    public const string UnitPathOverEvent = "UnitMgr_UnitPathOverEvent";

    /// <summary>
    /// 怪物进入攻击点事件
    /// </summary>
    public const string UnitEnterAttackPointEvent = "UnitMgr_UnitEnterAttackPointEvent";

    /// <summary>
    /// 怪物离开攻击点事件
    /// </summary>
    public const string UnitLeaveAttackPointEvent = "UnitMgr_UnitLeaveAttackPointEvent";

    Dictionary<int, UnitConfigData.UnitConfig> unitConfigs;
    // Dictionary<int, List<UnitPathConfigData.UnitRandPathConfig>> unitRandPathConfigDatas;
    // Dictionary<int, List<UnitPathConfigData.UnitFixedPathConfig>> unitFixedPathConfigDatas;
    // Dictionary<int, List<UnitPathConfigData.UnitBossConfig>> unitBossConfigDatas;

    public List<MonsterBase> activeUnits;
    List<MonsterBase> waitToRemoveUnits;
    Dictionary<int, Queue<MonsterBase>> cacheUnits;
    public bool canSpawn;

    private Dictionary<int, GameObject> _preloadUnitPrefabs = new Dictionary<int, GameObject>();
    private Dictionary<int, Queue<GameObject>> _preloadUnitObjs = new Dictionary<int, Queue<GameObject>>();

    public IEnumerator Preload()
    {
        for (var i = 0; i < GameLevelMgr.Instance.gameLevelConfigData.data.Count; i++)
        {
            GameLevelConfigData.GameLevelConfig levelConfig = GameLevelMgr.Instance.gameLevelConfigData.data[i];
            if (levelConfig.scene != GameSceneMgr.Instance.CurScene)
                continue;

            for (var j = 0; j < levelConfig.spawnIds.Length; j++)
            {
                int spawnId = levelConfig.spawnIds[j];
                UnitSpawnConfigData.UnitSpawnConfig spawnConfig = UnitSpawnMgr.Instance.GetUnitSpawnConfig(spawnId);
                if(spawnConfig == null)
                    continue;

                for (var k = 0; k < spawnConfig.unitIds.Length; k++)
                {
                    int unitId = spawnConfig.unitIds[k];
                    
                    if(_preloadUnitPrefabs.ContainsKey(unitId))
                        continue;
                    
                    if(!unitConfigs.TryGetValue(unitId, out var unitConfig))
                        continue;

                    if (unitConfig.preloadCount <= 0 || string.IsNullOrEmpty(unitConfig.prefab))
                        continue;
                    
                    
                    ResourceRequest request = ResMgr.Instance.LoadAssetASync<GameObject>(unitConfig.prefab);
                    yield return request;
                    var nodePrefab = request.asset as GameObject;
                    if (nodePrefab == null)
                        continue;
        
                    _preloadUnitPrefabs.Add(unitId, nodePrefab);

                    for (var l = 0; l < unitConfig.preloadCount; l++)
                    {
                        GameObject obj = Object.Instantiate(nodePrefab, null, false);
                        obj.SetActive(false);
                        if (!_preloadUnitObjs.ContainsKey(unitId))
                            _preloadUnitObjs.Add(unitId, new Queue<GameObject>());
                        _preloadUnitObjs[unitId].Enqueue(obj);
                    }
                }
            }
        }
        yield return null;
    }
    public void PreloadClear()
    {
        _preloadUnitPrefabs.Clear();
        _preloadUnitObjs.Clear();
    }
    private GameObject GetUnitGameObject(int unitId)
    {
        if (_preloadUnitObjs.TryGetValue(unitId, out Queue<GameObject> queue))
        {
            if (queue.TryDequeue(out GameObject obj))
            {
                if (queue.Count <= 0)
                    _preloadUnitObjs.Remove(unitId);
                
                return obj;
            }
            else
            {
                if (queue.Count <= 0)
                    _preloadUnitObjs.Remove(unitId);
            }
        }

        if (_preloadUnitPrefabs.TryGetValue(unitId, out GameObject prefab))
        {
            Debug.Log($"GetUnitGameObject {unitId} failed1");
            GameObject obj = Object.Instantiate(prefab, null, false);
            return obj;
        }
        
        var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(this.unitConfigs[unitId].prefab);
        if (nodePrefab == null)
            return null;
        
        _preloadUnitPrefabs.Add(unitId, nodePrefab);
        
        Debug.Log($"GetUnitGameObject {unitId} failed2");
        return Object.Instantiate(nodePrefab, null, false);;
    }

    public void DestroyAllUnits()
    {
        if (this.activeUnits.Count > 0)
        {
            for (var i = 0; i < this.activeUnits.Count; i++)
            {
                this.activeUnits[i].Destroy();
            }
        }

        if (this.cacheUnits.Count > 0)
        {
            foreach (MonsterBase unit in this.cacheUnits.Values.SelectMany(queue => queue))
            {
                unit.Destroy();
            }
        }

        this.activeUnits.Clear();
        this.cacheUnits.Clear();
        this.waitToRemoveUnits.Clear();
    }

    public int curActiveNum;
    public Dictionary<int, int> curActiveUnitNum;

    int stayInAttackCount;
    public Dictionary<int, int> stayInAttackPointCount;

    Transform unitParentTran;

    /// <summary>
    /// 更新根节点 ---> 也就是怪物生成的父对象
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public void UpdateUnitRoot(Vector3 pos, Vector3 rot)
    {
        this.unitParentTran.position = pos;
        this.unitParentTran.rotation = Quaternion.Euler(rot);
    }

    public void Init()
    {
        this.unitParentTran = this.gameObject.transform.Find("UnitRoot");
        this.activeUnits = new List<MonsterBase>(8);
        this.waitToRemoveUnits = new List<MonsterBase>(8);
        this.cacheUnits = new Dictionary<int, Queue<MonsterBase>>(8);
        this.curActiveUnitNum = new Dictionary<int, int>(8);
        this.canSpawn = false;
        this.curActiveNum = 0;
        this.stayInAttackCount = 0;
        this.stayInAttackPointCount = new Dictionary<int, int>(8);

        LoadUnitConfig();

        // 监听产怪事件
        EventMgr.Instance.AddListener(UnitSpawnMgr.UnitSpawnEvent, (_, udata) =>
        {
            if (!this.canSpawn)
                return;

            if (udata == null)
                return;

            var data = (int[])udata;
            int spawnId = data[0];
            int unitId = data[1];
            // int spawnIndex = data[2];
            if (IsBoss(unitId))
            {
                CreateMonster(spawnId, unitId);
                // CreateMonsterTypeBoss(spawnId, unitId);
                return;
            }

            // if (this.curActiveNum >= GameLevelMgr.Instance.RemainingKillNum + 2)
            //     return;
            if (this.curActiveNum >= GameLevelMgr.Instance.MaxUnitNum)
                return;

            CreateMonster(spawnId, unitId);
            // switch (this.unitConfigs[unitId].unitType)
            // {
            //
            //     case EUnitType.unitRandPath:
            //         CreateUnit(spawnId, unitId);
            //         break;
            //     case EUnitType.unitFixedPath:
            //         CreateUnitFixed(spawnId, unitId);
            //         break;
            //     case EUnitType.unitBossHitPoint:
            //         // CreateUnitBossHitPoint(spawnId, unitId, spawnIndex);
            //         break;
            //     case EUnitType.monsterType1:
            //         CreateMonsterType1(spawnId, unitId);
            //         break;
            //     case EUnitType.monsterType2:
            //         CreateMonsterType2(spawnId, unitId);
            //         break;
            //     case EUnitType.monsterTypeBoss:
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        });
        EventMgr.Instance.AddListener(UnitMgr.UnitPathOverEvent, (_, udata) =>
        {
            if (!this.canSpawn)
                return;

            var datas = (int[])udata;
            int spawnId = datas[0];
            int unitId = datas[1];
            if (this.curActiveUnitNum.ContainsKey(unitId))
            {
                if (this.curActiveUnitNum[unitId] > 0)
                    this.curActiveUnitNum[unitId]--;
            }

            this.curActiveNum--;
            if (this.curActiveNum < 0)
                this.curActiveNum = 0;

            if (GameLevelMgr.Instance.RemainingKillNum > 0)
            {
                CreateMonster(spawnId, unitId);
                // switch (this.unitConfigs[unitId].unitType)
                // {
                //
                //     case EUnitType.unitRandPath:
                //         CreateUnit(spawnId, unitId);
                //         break;
                //     case EUnitType.unitFixedPath:
                //         CreateUnitFixed(spawnId, unitId);
                //         break;
                //     case EUnitType.unitBossHitPoint:
                //         break;
                //     case EUnitType.monsterType1:
                //         CreateMonsterType1(spawnId, unitId);
                //         break;
                //     case EUnitType.monsterType2:
                //         CreateMonsterType2(spawnId, unitId);
                //         break;
                //     case EUnitType.monsterTypeBoss:
                //         break;
                //     default:
                //         throw new ArgumentOutOfRangeException();
                // }
                // EventMgr.Instance.Emit(UnitSpawnMgr.SpawnOneNowEvent, null);
            }
        });
        // 进入等待开始事件-积分归零，血量归零，显示请投币提示
        EventMgr.Instance.AddListener(GameApp.WaitForStartEvent, (_, _) =>
        {
            RecycleAllUnits();
            this.canSpawn = false;
        });

        // 关卡开始--清除所有怪物
        EventMgr.Instance.AddListener(GameLevel.GameLevelStartEvent, (_, _) =>
        {
            // if (udata == null)
            //     return;
            //
            // var data = (int[])udata;
            // int level = data[0];
            // // 设置怪物父节点
            // this.unitParentTran = CameraController.Instance.GetUnitParentTrans(level);
            // this.unitParentTran = CameraController.Instance.MainCameraTran;

            RecycleAllUnits();
            this.canSpawn = true;
            this.curActiveNum = 0;
            this.curActiveUnitNum.Clear();
            this.stayInAttackCount = 0;
            this.stayInAttackPointCount.Clear();
        });

        // 关卡完成（过关）事件--禁止继续产怪
        EventMgr.Instance.AddListener(GameLevel.FinishedEvent, (_, _) =>
        {
            this.canSpawn = false;

            if (this.activeUnits.Count <= 0)
                return;

            for (var i = 0; i < this.activeUnits.Count; i++)
            {
                this.activeUnits[i].MoveOutQuickly();
            }
        });
        // 关卡失败（游戏结束）事件--禁止继续产怪
        EventMgr.Instance.AddListener(GameLevel.FailedEvent, (_, _) =>
        {
            this.canSpawn = false;

            if (this.activeUnits.Count <= 0)
                return;

            for (var i = 0; i < this.activeUnits.Count; i++)
            {
                this.activeUnits[i].MoveOutQuickly();
            }
        });
        // // 开始产怪
        // EventMgr.Instance.AddListener(UnitSpawnMgr.StartEvent, (_, udata) =>
        // {
        //     RecycleAllUnits();
        //     this.curActiveNum = 0;
        //     this.canSpawn = true;
        // });


        // 怪物进入攻击点
        EventMgr.Instance.AddListener(UnitMgr.UnitEnterAttackPointEvent, (_, udata) =>
        {
            this.stayInAttackCount++;
            var attackPointIndex = (int)udata;
            this.stayInAttackPointCount.TryAdd(attackPointIndex, 0);
            this.stayInAttackPointCount[attackPointIndex]++;
        });
        // 怪物离开攻击点
        EventMgr.Instance.AddListener(UnitMgr.UnitLeaveAttackPointEvent, (_, udata) =>
        {
            var attackPointIndex = (int)udata;
            this.stayInAttackPointCount.TryAdd(attackPointIndex, 0);
            this.stayInAttackPointCount[attackPointIndex]--;
            if (this.stayInAttackPointCount[attackPointIndex] < 0)
                this.stayInAttackPointCount[attackPointIndex] = 0;

            if (this.stayInAttackCount > 0)
                this.stayInAttackCount--;
        });
    }

    void Update()
    {
        this.waitToRemoveUnits.Clear();
        for (var i = 0; i < this.activeUnits.Count; i++)
        {
            if (this.activeUnits[i].RecycleFlag)
            {
                this.waitToRemoveUnits.Add(this.activeUnits[i]);
            }
        }

        if (this.waitToRemoveUnits.Count > 0)
        {
            for (var i = 0; i < this.waitToRemoveUnits.Count; i++)
            {
                RecycleUnit(this.waitToRemoveUnits[i]);
            }

            this.waitToRemoveUnits.Clear();
        }
    }

    // public void CreateUnit(int spawnId, int unitId)
    // {
    //     this.curActiveUnitNum.TryGetValue(unitId, out int activeNum);
    //     if (activeNum >= GameLevelMgr.Instance.SingleUnitLimit(unitId))
    //         return;
    //
    //     if (!UnitSpawnMgr.Instance.CheckLessSpawn())
    //         return;
    //
    //     Unit unit = GetUnit(unitId);
    //     unit.SetParentTran(this.unitParentTran);
    //     unit.StartMoveIn(spawnId, unitId);
    //     this.activeUnits.Add(unit);
    //     this.curActiveNum++;
    //     this.curActiveUnitNum.TryAdd(unitId, 0);
    //     this.curActiveUnitNum[unitId]++;
    // }
    // public void CreateUnitFixed(int spawnId, int unitId)
    // {
    //     this.curActiveUnitNum.TryGetValue(unitId, out int activeNum);
    //     if (activeNum >= GameLevelMgr.Instance.SingleUnitLimit(unitId))
    //         return;
    //
    //     if (!UnitSpawnMgr.Instance.CheckLessSpawn())
    //         return;
    //
    //     UnitFixedPath unit = GetUnitFixedPath(unitId);
    //     unit.SetParentTran(this.unitParentTran);
    //     unit.StartMoveIn(spawnId, unitId);
    //     this.activeUnits.Add(unit);
    //     this.curActiveNum++;
    //     this.curActiveUnitNum.TryAdd(unitId, 0);
    //     this.curActiveUnitNum[unitId]++;
    // }

    void CreateMonsterTypeBoss(int spawnId, int unitId)
    {
        this.curActiveUnitNum.TryGetValue(unitId, out int activeNum);
        if (activeNum >= GameLevelMgr.Instance.SingleUnitLimit(unitId))
            return;

        MonsterTypeBoss unit = GetMonsterTypeBoss(unitId);
        // UnitPathConfigData.UnitBossConfig unitBossConfig = GetUnitBossConfig(spawnId, unitId);
        unit.SetParentTran(this.unitParentTran);
        unit.StartMoveIn(spawnId, unitId);
        // this.curActiveNum++;// Boss不计入总数计算
        this.activeUnits.Add(unit);
        this.curActiveUnitNum.TryAdd(unitId, 0);
        this.curActiveUnitNum[unitId]++;
    }

    void CreateMonsterType1(int spawnId, int unitId)
    {
        this.curActiveUnitNum.TryGetValue(unitId, out int activeNum);
        if (activeNum >= GameLevelMgr.Instance.SingleUnitLimit(unitId))
            return;

        if (!UnitSpawnMgr.Instance.CheckLessSpawn())
            return;

        MonsterType1 unit = GetMonsterType1(unitId);
        unit.SetParentTran(this.unitParentTran);
        unit.StartMoveIn(spawnId, unitId);

        this.activeUnits.Add(unit);
        this.curActiveNum++;
        this.curActiveUnitNum.TryAdd(unitId, 0);
        this.curActiveUnitNum[unitId]++;
    }

    void CreateMonsterType2(int spawnId, int unitId)
    {
        this.curActiveUnitNum.TryGetValue(unitId, out int activeNum);
        if (activeNum >= GameLevelMgr.Instance.SingleUnitLimit(unitId))
            return;

        if (!UnitSpawnMgr.Instance.CheckLessSpawn())
            return;

        MonsterType2 unit = GetMonsterType2(unitId);
        unit.SetParentTran(this.unitParentTran);
        unit.StartMoveIn(spawnId, unitId);

        this.activeUnits.Add(unit);
        this.curActiveNum++;
        this.curActiveUnitNum.TryAdd(unitId, 0);
        this.curActiveUnitNum[unitId]++;
    }

    public void CreateUnitBossHitPoint(int unitId, Vector3 pos, float hitTime)
    {
        MonsterTypeBossHitPoint unit = GetUnitBossHitPoint(unitId);
        unit.SetParentTran(this.unitParentTran);
        unit.StartMoveIn(pos, hitTime);
        this.activeUnits.Add(unit);
    }

    void CreateMonster(int spawnId, int unitId)
    {
        if (!this.canSpawn)
            return;

        switch (this.unitConfigs[unitId].unitType)
        {
            case EUnitType.monsterType1:
                CreateMonsterType1(spawnId, unitId);
                break;
            case EUnitType.monsterType2:
                CreateMonsterType2(spawnId, unitId);
                break;
            case EUnitType.monsterTypeBoss:
                CreateMonsterTypeBoss(spawnId, unitId);
                break;
            default:
                Debug.LogError($"试图创建不存在的怪物！！！！spawnId:{spawnId}  unitId:{unitId}");
                break;
        }
    }


    public void RecycleAllButBossUnits()
    {
        if (this.activeUnits.Count <= 0)
            return;

        for (var i = 0; i < this.activeUnits.Count; i++)
        {
            if (this.activeUnits[i] is MonsterTypeBoss)
                continue;

            this.activeUnits[i].MoveOutQuickly();
        }
    }

    void LoadUnitConfig()
    {
        this.unitConfigs = new Dictionary<int, UnitConfigData.UnitConfig>(8);
        var data = ResMgr.Instance.LoadAssetSync<UnitConfigData>("Config/unitConfigData");

        if (data == null || data.data == null || data.data.Count <= 0)
        {
            Debug.LogError("找不到怪物配置！！");
            return;
        }

        for (var i = 0; i < data.data.Count; i++)
        {
            this.unitConfigs.Add(data.data[i].unitId, data.data[i]);
        }

        // var pathDatas = ResMgr.Instance.LoadAssetSync<UnitPathConfigDatas>("Config/unitPathConfigDatas");
        // if (pathDatas == null || pathDatas.datas == null || pathDatas.datas.Count <= 0)
        // {
        //     Debug.LogError("找不到路径配置！！");
        //     return;
        // }
        // this.unitRandPathConfigDatas = new Dictionary<int, List<UnitPathConfigData.UnitRandPathConfig>>(16);
        // this.unitBossConfigDatas = new Dictionary<int, List<UnitPathConfigData.UnitBossConfig>>(4);
        // this.unitFixedPathConfigDatas = new Dictionary<int, List<UnitPathConfigData.UnitFixedPathConfig>>(4);
        // for (var i = 0; i < pathDatas.datas.Count; i++)
        // {
        //     int spawnId = pathDatas.datas[i].spawnId;
        //     for (var j = 0; j < pathDatas.datas[i].randPathConfigs.Count; j++)
        //     {
        //         int unitId = pathDatas.datas[i].randPathConfigs[j].unitId;
        //         int key = GetPathKey(spawnId, unitId);
        //         if (!this.unitRandPathConfigDatas.ContainsKey(key))
        //             this.unitRandPathConfigDatas.Add(key, new List<UnitPathConfigData.UnitRandPathConfig>(8));
        //
        //         this.unitRandPathConfigDatas[key].Add(pathDatas.datas[i].randPathConfigs[j]);
        //     }
        //     for (var j = 0; j < pathDatas.datas[i].fixedPathConfigs.Count; j++)
        //     {
        //         int unitId = pathDatas.datas[i].fixedPathConfigs[j].unitId;
        //         int key = GetPathKey(spawnId, unitId);
        //         if (!this.unitFixedPathConfigDatas.ContainsKey(key))
        //             this.unitFixedPathConfigDatas.Add(key, new List<UnitPathConfigData.UnitFixedPathConfig>(8));
        //
        //         this.unitFixedPathConfigDatas[key].Add(pathDatas.datas[i].fixedPathConfigs[j]);
        //     }
        //     for (var j = 0; j < pathDatas.datas[i].unitBossConfigs.Count; j++)
        //     {
        //         int unitId = pathDatas.datas[i].unitBossConfigs[j].unitId;
        //         int key = GetPathKey(spawnId, unitId);
        //         if (!this.unitBossConfigDatas.ContainsKey(key))
        //             this.unitBossConfigDatas.Add(key, new List<UnitPathConfigData.UnitBossConfig>(4));
        //
        //         this.unitBossConfigDatas[key].Add(pathDatas.datas[i].unitBossConfigs[j]);
        //     }
        // }
    }

    public void UpdateUnitConfigs()
    {
        //foreach (UnitConfigData.UnitConfig unitConfig in this.unitConfigs.Values)
        //{
        //    unitConfig.health = MachineDataMgr.Instance.GetUnitHP(unitConfig.realUnitType);
        //    unitConfig.attack = MachineDataMgr.Instance.GetUnitATT(unitConfig.realUnitType);
        //}
    }

    void RecycleAllUnits()
    {
        if (this.activeUnits.Count <= 0)
            return;

        for (var i = 0; i < this.activeUnits.Count; i++)
        {
            this.activeUnits[i].Recycle();
        }
    }

    // Unit GetUnit(int unitId)
    // {
    //     if (!this.cacheUnits.ContainsKey(unitId))
    //         this.cacheUnits.Add(unitId, new Queue<MonsterBase>(8));
    //
    //     if (this.cacheUnits[unitId].Count <= 0)
    //     {
    //         var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(this.unitConfigs[unitId].prefab);
    //         GameObject obj = Object.Instantiate(nodePrefab, null, false);
    //         var unit = obj.AddComponent<Unit>();
    //         unit.Init(obj, unitId);
    //         return unit;
    //     }
    //     else
    //     {
    //         return this.cacheUnits[unitId].Dequeue() as Unit;
    //     }
    // }
    // UnitFixedPath GetUnitFixedPath(int unitId)
    // {
    //     if (!this.cacheUnits.ContainsKey(unitId))
    //         this.cacheUnits.Add(unitId, new Queue<MonsterBase>(8));
    //
    //     if (this.cacheUnits[unitId].Count <= 0)
    //     {
    //         var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(this.unitConfigs[unitId].prefab);
    //         GameObject obj = Object.Instantiate(nodePrefab, null, false);
    //         var unit = obj.AddComponent<UnitFixedPath>();
    //         unit.Init(obj, unitId);
    //         return unit;
    //     }
    //     else
    //     {
    //         return this.cacheUnits[unitId].Dequeue() as UnitFixedPath;
    //     }
    // }
    MonsterTypeBoss GetMonsterTypeBoss(int unitId)
    {
        if (!this.cacheUnits.ContainsKey(unitId))
            this.cacheUnits.Add(unitId, new Queue<MonsterBase>(8));

        if (this.cacheUnits[unitId].Count <= 0)
        {
            // var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(this.unitConfigs[unitId].prefab);
            // GameObject obj = Object.Instantiate(nodePrefab, null, false);
            GameObject obj = GetUnitGameObject(unitId);
            var unit = obj.AddComponent<MonsterTypeBoss>();
            unit.Init(obj, unitId);
            return unit;
        }
        else
        {
            return this.cacheUnits[unitId].Dequeue() as MonsterTypeBoss;
        }
    }

    MonsterTypeBossHitPoint GetUnitBossHitPoint(int unitId)
    {
        if (!this.cacheUnits.ContainsKey(unitId))
            this.cacheUnits.Add(unitId, new Queue<MonsterBase>(8));

        if (this.cacheUnits[unitId].Count <= 0)
        {
            // var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(this.unitConfigs[unitId].prefab);
            // GameObject obj = Object.Instantiate(nodePrefab, null, false);
            GameObject obj = GetUnitGameObject(unitId);
            var unit = obj.AddComponent<MonsterTypeBossHitPoint>();
            unit.Init(obj, unitId);
            return unit;
        }
        else
        {
            return this.cacheUnits[unitId].Dequeue() as MonsterTypeBossHitPoint;
        }
    }

    MonsterType1 GetMonsterType1(int unitId)
    {
        if (!this.cacheUnits.ContainsKey(unitId))
            this.cacheUnits.Add(unitId, new Queue<MonsterBase>(8));

        if (this.cacheUnits[unitId].Count <= 0)
        {
            // var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(this.unitConfigs[unitId].prefab);
            // GameObject obj = Object.Instantiate(nodePrefab, null, false);
            GameObject obj = GetUnitGameObject(unitId);
            var unit = obj.AddComponent<MonsterType1>();
            unit.Init(obj, unitId);
            return unit;
        }
        else
        {
            return this.cacheUnits[unitId].Dequeue() as MonsterType1;
        }
    }

    MonsterType2 GetMonsterType2(int unitId)
    {
        if (!this.cacheUnits.ContainsKey(unitId))
            this.cacheUnits.Add(unitId, new Queue<MonsterBase>(8));

        if (this.cacheUnits[unitId].Count <= 0)
        {
            // var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(this.unitConfigs[unitId].prefab);
            // GameObject obj = Object.Instantiate(nodePrefab, null, false);
            GameObject obj = GetUnitGameObject(unitId);
            var unit = obj.AddComponent<MonsterType2>();
            unit.Init(obj, unitId);
            return unit;
        }
        else
        {
            return this.cacheUnits[unitId].Dequeue() as MonsterType2;
        }
    }


    // public UnitPathConfigData.UnitRandPathConfig GetUnitRandPathConfig(int spawnId, int unitId)
    // {
    //     int key = GetPathKey(spawnId, unitId);
    //     if (this.unitRandPathConfigDatas == null ||
    //         !this.unitRandPathConfigDatas.TryGetValue(key,
    //             out List<UnitPathConfigData.UnitRandPathConfig> randPathList) ||
    //         randPathList.Count <= 0)
    //         return null;
    //
    //     int index = Random.Range(0, randPathList.Count);
    //     return randPathList[index];
    // }
    // public UnitPathConfigData.UnitFixedPathConfig GetUnitFixedPathConfig(int spawnId, int unitId)
    // {
    //     int key = GetPathKey(spawnId, unitId);
    //     if (this.unitFixedPathConfigDatas == null ||
    //         !this.unitFixedPathConfigDatas.TryGetValue(key,
    //             out List<UnitPathConfigData.UnitFixedPathConfig> randPathList) ||
    //         randPathList.Count <= 0)
    //         return null;
    //
    //     int index = Random.Range(0, randPathList.Count);
    //     return randPathList[index];
    // }
    // public UnitPathConfigData.UnitBossConfig GetUnitBossConfig(int spawnId, int unitId)
    // {
    //     int key = GetPathKey(spawnId, unitId);
    //     if (this.unitBossConfigDatas == null ||
    //         !this.unitBossConfigDatas.TryGetValue(key,
    //             out List<UnitPathConfigData.UnitBossConfig> randList) ||
    //         randList.Count <= 0)
    //         return null;
    //
    //     int index = Random.Range(0, randList.Count);
    //     return randList[index];
    // }

    public void RecycleUnit(MonsterBase unit)
    {
        if (unit == null)
            return;

        int unitId = unit.Config.unitId;
        if (!this.cacheUnits.ContainsKey(unitId))
            this.cacheUnits.Add(unitId, new Queue<MonsterBase>(8));
        this.cacheUnits[unitId].Enqueue(unit);
        if (this.activeUnits.Contains(unit))
            this.activeUnits.Remove(unit);
    }

    /// <summary>
    /// 检测是否可以留在攻击点
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool CanStayInAttackPoint(int index)
    {
        if (this.stayInAttackCount >= GameLevelMgr.Instance.StayInAttackPointLimit)
            return false;

        if (this.stayInAttackPointCount.TryGetValue(index, out int value))
        {
            return value <= 0;
        }

        return true;
    }

    public bool IsBoss(int unitId)
    {
        return this.unitConfigs[unitId].unitType == EUnitType.monsterTypeBoss;
    }


    // // 获取散弹枪击杀怪物列表
    // public List<MonsterBase> GetWeapon1KillUnits(Vector3 pos)
    // {
    //     if (this.activeUnits is not { Count: > 0 })
    //         return null;
    //
    //     pos.z = 0;
    //     var units = new List<MonsterBase>();
    //     for (var i = 0; i < this.activeUnits.Count; i++)
    //     {
    //         if (this.activeUnits[i] is MonsterTypeBoss)
    //             continue;
    //
    //         if (this.activeUnits[i].Distance(pos) < 100)
    //         {
    //             units.Add(this.activeUnits[i]);
    //         }
    //     }
    //
    //     return units;
    // }

    /// <summary>
    /// 获取火箭炮击杀怪物列表
    /// </summary>
    /// <param name="pos">射击终点位置</param>
    /// <returns></returns>
    public List<MonsterBase> GetWeapon2KillUnits(Vector3 pos)
    {
        if (this.activeUnits is not { Count: > 0 })
            return null;

        pos.z = 0;
        var units = new List<MonsterBase>();
        for (var i = 0; i < this.activeUnits.Count; i++)
        {
            if (this.activeUnits[i] is MonsterTypeBoss)
                continue;

            if (this.activeUnits[i].Distance(pos) < 400)
            {
                units.Add(this.activeUnits[i]);
            }
        }

        return units;
    }

    static int GetPathKey(int spawn, int unitId)
    {
        return spawn * 100 + unitId;
    }


    public UnitConfigData.UnitConfig GetUnitConfig(int unitId)
    {
        return this.unitConfigs[unitId];
    }
}