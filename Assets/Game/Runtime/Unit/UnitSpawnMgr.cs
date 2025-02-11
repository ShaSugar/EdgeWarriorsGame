using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnMgr : UnitySingleton<UnitSpawnMgr>
{
    /// <summary>
    /// 产怪数量 
    /// </summary>
    public const string SetLimitCount = "UnitSpawnMgr_SetLimitCount";
    /// <summary>
    /// 开始产怪的准备事件
    /// </summary>
    public const string StartEvent = "UnitSpawnMgr_StartEvent";
    /// <summary>
    /// 怪物正式产怪
    /// </summary>
    public const string UnitSpawnEvent = "UnitSpawnMgr_UnitSpawnEvent";
    public const string SpawnOneNowEvent = "UnitSpawnMgr_SpawnOneNowEvent";
    
    
    Dictionary<int, UnitSpawnConfigData.UnitSpawnConfig> unitSpawnConfigs;

    public UnitSpawnConfigData.UnitSpawnConfig GetUnitSpawnConfig(int spawnId)
    {
        return unitSpawnConfigs.GetValueOrDefault(spawnId);
    }

    List<UnitSpawn> activeUnitSpawns;
    Queue<UnitSpawn> cacheUnitSpawns;
    List<UnitSpawn> waitForRemoveUnitSpawns;

    List<int> canSpawnNowIndexList;

    int lessSpawnCount;
    
    public void Init()
    {
        this.activeUnitSpawns = new List<UnitSpawn>(8);
        this.cacheUnitSpawns = new Queue<UnitSpawn>(8);
        this.waitForRemoveUnitSpawns = new List<UnitSpawn>(8);
        this.canSpawnNowIndexList = new List<int>(8);
        LoadConfig();
        
        // 设置产怪数量
        EventMgr.Instance.AddListener(UnitSpawnMgr.SetLimitCount, (_, udata) =>
        {
            this.lessSpawnCount = (int)udata;
        });
        // 开始产怪
        EventMgr.Instance.AddListener(UnitSpawnMgr.StartEvent, (_, udata) =>
        {
            RecycleAllSpawn();

            if (udata == null)
                return;

            var ids = (int[])udata;
            if (ids.Length <= 0)
                return;

            for (var i = 0; i < ids.Length; i++)
            {
                UnitSpawn spawn = GetUnitSpawn();
                spawn.Start(this.unitSpawnConfigs[ids[i]]);
                this.activeUnitSpawns.Add(spawn);
            }
            
            this.canSpawnNowIndexList.Clear();
            if (this.activeUnitSpawns.Count <= 0)
                return;
            for (var i = 0; i < this.activeUnitSpawns.Count; i++)
            {
                if(this.activeUnitSpawns[i].config.canSpawnNow)
                    this.canSpawnNowIndexList.Add(i);
            }
        });
        
        // 进入等待开始事件-积分归零，血量归零，显示请投币提示
        EventMgr.Instance.AddListener(GameApp.WaitForStartEvent, (_, _) =>
        {
            this.lessSpawnCount = 0;
            RecycleAllSpawn();
        });
        
        // 关卡开始--清除所有怪物
        EventMgr.Instance.AddListener(GameLevel.GameLevelStartEvent, (_, _) =>
        {
            this.lessSpawnCount = 0;
            RecycleAllSpawn();
        });
        
        // 关卡完成（过关）事件--禁止继续产怪
        EventMgr.Instance.AddListener(GameLevel.FinishedEvent, (_, _) =>
        {
            this.lessSpawnCount = 0;
            RecycleAllSpawn();
        });
        // 关卡失败（游戏结束）事件--禁止继续产怪
        EventMgr.Instance.AddListener(GameLevel.FailedEvent, (_, _) =>
        {
            this.lessSpawnCount = 0;
            RecycleAllSpawn();
        });
        
        // // 有怪物被击杀了，马上补充一个吧
        // EventMgr.Instance.AddListener(UnitSpawnMgr.SpawnOneNowEvent, (_, _) =>
        // {
        //     if (this.lessSpawnCount <= 0)
        //         return;
        //     
        //     if (this.canSpawnNowIndexList.Count <= 0)
        //         return;
        //     
        //     int index = UnityEngine.Random.Range(0, this.canSpawnNowIndexList.Count);
        //     if(this.activeUnitSpawns.Count <= this.canSpawnNowIndexList[index])
        //         return;
        //     
        //     this.activeUnitSpawns[this.canSpawnNowIndexList[index]].OpenTimer(0);
        // });
        
    }

    void Update()
    {
        if (this.activeUnitSpawns.Count <= 0)
            return;

        this.waitForRemoveUnitSpawns.Clear();
        for (var i = 0; i < this.activeUnitSpawns.Count; i++)
        {
            if (this.activeUnitSpawns[i].flag == false)
            {
                this.waitForRemoveUnitSpawns.Add(this.activeUnitSpawns[i]);
            }
            else
            {
                this.activeUnitSpawns[i].Update();
            }
        }

        if (this.waitForRemoveUnitSpawns.Count > 0)
        {
            for (var i = 0; i < this.waitForRemoveUnitSpawns.Count; i++)
            {
                this.cacheUnitSpawns.Enqueue(this.waitForRemoveUnitSpawns[i]);
                this.activeUnitSpawns.Remove(this.waitForRemoveUnitSpawns[i]);
            }
            
            this.waitForRemoveUnitSpawns.Clear();
            
            this.canSpawnNowIndexList.Clear();
            if (this.activeUnitSpawns.Count <= 0)
                return;
            for (var i = 0; i < this.activeUnitSpawns.Count; i++)
            {
                if(this.activeUnitSpawns[i].config.canSpawnNow)
                    this.canSpawnNowIndexList.Add(i);
            }
        }
    }

    public bool CheckLessSpawn()
    {
        if (this.lessSpawnCount <= 0)
            return false;

        this.lessSpawnCount--;
        if (this.lessSpawnCount <= 0)
        {
            for (var i = 0; i < this.activeUnitSpawns.Count; i++)
            {
                this.activeUnitSpawns[i].Stop();
            }
        }
        return true;
    }


    void LoadConfig()
    {
        this.unitSpawnConfigs = new Dictionary<int, UnitSpawnConfigData.UnitSpawnConfig>(32);
        var data = ResMgr.Instance.LoadAssetSync<UnitSpawnConfigData>("Config/unitSpawnConfigData");
        if (data == null || data.data == null || data.data.Count <= 0)
        {
            Debug.LogError("找不到产怪配置！！");
            return;
        }
        for (var i = 0; i < data.data.Count; i++)
        {
            this.unitSpawnConfigs.Add(data.data[i].spawnId, data.data[i]);
        }
        data = null;
    }

    public void RecycleAllSpawn()
    {
        if (this.activeUnitSpawns.Count <= 0)
            return;

        for (var i = 0; i < this.activeUnitSpawns.Count; i++)
        {
            this.activeUnitSpawns[i].Stop();
        }
    }
    UnitSpawn GetUnitSpawn()
    {
        return this.cacheUnitSpawns.Count <= 0 ? new UnitSpawn() : this.cacheUnitSpawns.Dequeue();
    }
    
}
