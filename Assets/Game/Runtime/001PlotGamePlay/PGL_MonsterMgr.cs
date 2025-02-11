using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum PGL_MonsterType
{
    normal, // 普通怪
    boom,   // 爆炸怪
}

public class PGL_MonsterMgr : UnitySingleton<PGL_MonsterMgr>
{
    public const string SpawnMonsterEvent = "PGL_MonsterMgr_SpawnMonsterEvent";
    public const string ActiveMonsterEvent = "PGL_MonsterMgr_ActiveMonsterEvent";
    public const string SpawnMonsterGroupEvent = "PGL_MonsterMgr_SpawnMonsterGroupEvent";
    public const string ActiveMonsterGroupEvent = "PGL_MonsterMgr_ActiveMonsterGroupEvent";
    public const string ShowMonsterEvent = "PGL_MonsterMgr_ShowMonsterEvent";
    public const string HideMonsterEvent = "PGL_MonsterMgr_HideMonsterEvent";
    public const string ShowMonsterGroupEvent = "PGL_MonsterMgr_ShowMonsterGroupEvent";
    public const string HideMonsterGroupEvent = "PGL_MonsterMgr_HideMonsterGroupEvent";
    public const string DestroyMonsterEvent = "PGL_MonsterMgr_DestroyMonsterEvent";
    public const string DestroyMonsterGroupEvent = "PGL_MonsterMgr_DestroyMonsterGroupEvent";
    public const string MonsterBoomEvent = "PGL_MonsterMgr_MonsterBoomEvent";
    public const string MonsterGroupBoomEvent = "PGL_MonsterMgr_MonsterGroupBoomEvent";
    public const string MonsterDieEvent = "PGL_MonsterMgr_MonsterDieEvent";
    public const string MonsterRunOutEvent = "PGL_MonsterMgr_MonsterRunOutEvent";
    public const string MonsterRunOutGroupEvent = "PGL_MonsterMgr_MonsterRunOutGroupEvent";

    private Transform unitParentTran;
    private Dictionary<int, PGL_MonsterBase> activeMonsterDict = new Dictionary<int, PGL_MonsterBase>();
    
    public void Init()
    {
        unitParentTran = this.gameObject.transform.Find("UnitRoot");
    }
    public void ResetUnitRoot()
    {
        unitParentTran.position = Vector3.zero;
        unitParentTran.rotation = Quaternion.identity;
    }
    public void ClearAll()
    {
        foreach (PGL_MonsterBase monster in activeMonsterDict.Values)
        {
            Destroy(monster.gameObject);
        }
        activeMonsterDict.Clear();
    }

    public void GameStart()
    {
        EventMgr.Instance.AddListener(SpawnMonsterEvent, SpawnMonster);
        EventMgr.Instance.AddListener(ActiveMonsterEvent, ActiveMonster);
        EventMgr.Instance.AddListener(SpawnMonsterGroupEvent, SpawnMonsterGroup);
        EventMgr.Instance.AddListener(ActiveMonsterGroupEvent, ActiveMonsterGroup);
        EventMgr.Instance.AddListener(ShowMonsterEvent, ShowMonster);
        EventMgr.Instance.AddListener(HideMonsterEvent, HideMonster);
        EventMgr.Instance.AddListener(ShowMonsterGroupEvent, ShowMonsterGroup);
        EventMgr.Instance.AddListener(HideMonsterGroupEvent, HideMonsterGroup);
        EventMgr.Instance.AddListener(DestroyMonsterEvent, DestroyMonster);
        EventMgr.Instance.AddListener(DestroyMonsterGroupEvent, DestroyMonsterGroup);
        EventMgr.Instance.AddListener(MonsterBoomEvent, MonsterBoom);
        EventMgr.Instance.AddListener(MonsterGroupBoomEvent, MonsterGroupBoom);
        EventMgr.Instance.AddListener(MonsterDieEvent, MonsterDie);
        EventMgr.Instance.AddListener(MonsterRunOutEvent, MonsterRunOut);
        EventMgr.Instance.AddListener(MonsterRunOutGroupEvent, MonsterRunOutGroup);
    }

    public void GameEnd()
    {
        EventMgr.Instance.RemoveListener(SpawnMonsterEvent, SpawnMonster);
        EventMgr.Instance.RemoveListener(ActiveMonsterEvent, ActiveMonster);
        EventMgr.Instance.RemoveListener(SpawnMonsterGroupEvent, SpawnMonsterGroup);
        EventMgr.Instance.RemoveListener(ActiveMonsterGroupEvent, ActiveMonsterGroup);
        EventMgr.Instance.RemoveListener(ShowMonsterEvent, ShowMonster);
        EventMgr.Instance.RemoveListener(HideMonsterEvent, HideMonster);
        EventMgr.Instance.RemoveListener(ShowMonsterGroupEvent, ShowMonsterGroup);
        EventMgr.Instance.RemoveListener(HideMonsterGroupEvent, HideMonsterGroup);
        EventMgr.Instance.RemoveListener(DestroyMonsterEvent, DestroyMonster);
        EventMgr.Instance.RemoveListener(DestroyMonsterGroupEvent, DestroyMonsterGroup);
        EventMgr.Instance.RemoveListener(MonsterBoomEvent, MonsterBoom);
        EventMgr.Instance.RemoveListener(MonsterGroupBoomEvent, MonsterGroupBoom);
        EventMgr.Instance.RemoveListener(MonsterDieEvent, MonsterDie);
        EventMgr.Instance.RemoveListener(MonsterRunOutEvent, MonsterRunOut);
        EventMgr.Instance.RemoveListener(MonsterRunOutGroupEvent, MonsterRunOutGroup);

        ClearAll();
    }



    private void MonsterRunOutGroup(string arg1, object arg2)
    {
        int groupID = (int)arg2;

        PGL_MonsterConfigData.PGL_MonsterGroupConfig groupConfig = PGL_Main.GetMonsterGroupConfig(groupID);
        if (groupConfig == null)
        {
            Debug.LogError($"找不到ID为{groupID}的怪物组配置！");
            return;
        }

        if (groupConfig.monsterIDList != null && groupConfig.monsterIDList.Count > 0)
        {
            foreach (int monsterID in groupConfig.monsterIDList)
            {
                if (!activeMonsterDict.TryGetValue(monsterID, out PGL_MonsterBase monsterBase))
                    continue;

                monsterBase.RunOut();
            }
        }
    }

    private void MonsterRunOut(string arg1, object arg2)
    {
        int monsterID = (int)arg2;
        if (!activeMonsterDict.ContainsKey(monsterID))
            return;

        PGL_MonsterBase monsterBase = activeMonsterDict[monsterID];
        monsterBase.RunOut();
        Debug.Log($"MonsterRunOut {monsterID}");
    }
    private void MonsterDie(string arg1, object arg2)
    {
        int monsterID = (int)arg2;
        
        if(!activeMonsterDict.Remove(monsterID, out PGL_MonsterBase monsterBase))
            return;

        Destroy(monsterBase.gameObject);
    }

    private void SpawnMonster(string arg1, object arg2)
    {
        int monsterID = (int)arg2;
        if (activeMonsterDict.ContainsKey(monsterID))
        {
            Debug.LogError($"前面已使用了ID为{monsterID}的怪物，不能再次生成！");
            return;
        }

        PGL_MonsterConfigData.PGL_MonsterConfig config = PGL_Main.GetMonsterConfig(monsterID);
        if (config == null)
        {
            Debug.LogError($"找不到ID为{monsterID}的怪物配置！");
            return;
        }
        
        var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(config.prefab);
        GameObject obj = Object.Instantiate(nodePrefab, null, false);
        obj.transform.SetParent(unitParentTran, false);
        PGL_MonsterBase monsterBase = obj.GetComponent<PGL_MonsterBase>();
        if (monsterBase == null)
        {
            Debug.LogError($"生成的怪物对象{obj.name}没有PGL_MonsterBase组件！");
            return;
        }
        monsterBase.Init(config, 0);
        activeMonsterDict.Add(monsterID, monsterBase);
    }
    
    private void ActiveMonster(string arg1, object arg2)
    {
        int monsterID = (int)arg2;
        if (!activeMonsterDict.ContainsKey(monsterID))
            return;
        
        PGL_MonsterBase monsterBase = activeMonsterDict[monsterID];
        monsterBase.StartRun();
        Debug.Log($"Spawn Normal Monster {monsterID}");
    }

    private void SpawnMonsterGroup(string arg1, object arg2)
    {
        int groupID = (int)arg2;
        
        PGL_MonsterConfigData.PGL_MonsterGroupConfig groupConfig = PGL_Main.GetMonsterGroupConfig(groupID);
        if (groupConfig == null)
        {
            Debug.LogError($"找不到ID为{groupID}的怪物组配置！");
            return;
        }

        if (groupConfig.monsterIDList != null && groupConfig.monsterIDList.Count > 0)
        {
            foreach (int monsterID in groupConfig.monsterIDList)
            {
                if (activeMonsterDict.ContainsKey(monsterID))
                {
                    Debug.LogError($"前面已使用了ID为{monsterID}的怪物，不能再次生成！");
                    continue;
                }
                
                PGL_MonsterConfigData.PGL_MonsterConfig config = PGL_Main.GetMonsterConfig(monsterID);
                if (config == null)
                {
                    Debug.LogError($"找不到ID为{monsterID}的怪物配置！");
                    continue;
                }
                
                var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(config.prefab);
                GameObject obj = Object.Instantiate(nodePrefab, null, false);
                obj.transform.SetParent(unitParentTran, false);
                PGL_MonsterBase monsterBase = obj.GetComponent<PGL_MonsterBase>();
                if (monsterBase == null)
                {
                    Debug.LogError($"生成的怪物对象{obj.name}没有PGL_MonsterBase组件！");
                    continue;
                }
                monsterBase.Init(config, groupID);
                activeMonsterDict.Add(monsterID, monsterBase);
            }
        }
    }

    private void ActiveMonsterGroup(string arg1, object arg2)
    {
        int groupID = (int)arg2;
        
        PGL_MonsterConfigData.PGL_MonsterGroupConfig groupConfig = PGL_Main.GetMonsterGroupConfig(groupID);
        if (groupConfig == null)
        {
            Debug.LogError($"找不到ID为{groupID}的怪物组配置！");
            return;
        }

        if (groupConfig.monsterIDList != null && groupConfig.monsterIDList.Count > 0)
        {
            foreach (int monsterID in groupConfig.monsterIDList)
            {
                if (!activeMonsterDict.TryGetValue(monsterID, out PGL_MonsterBase monsterBase))
                    continue;

                monsterBase.StartRun();
            }
        }
    }
    

    private void HideMonsterGroup(string arg1, object arg2)
    {
        int groupID = (int)arg2;
        
        PGL_MonsterConfigData.PGL_MonsterGroupConfig groupConfig = PGL_Main.GetMonsterGroupConfig(groupID);
        if (groupConfig == null)
        {
            Debug.LogError($"找不到ID为{groupID}的怪物组配置！");
            return;
        }

        if (groupConfig.monsterIDList != null && groupConfig.monsterIDList.Count > 0)
        {
            foreach (int monsterID in groupConfig.monsterIDList)
            {
                if (!activeMonsterDict.TryGetValue(monsterID, out PGL_MonsterBase monsterBase))
                    continue;

                monsterBase.Show(false);
            }
        }
    }

    private void ShowMonsterGroup(string arg1, object arg2)
    {
        int groupID = (int)arg2;
        
        PGL_MonsterConfigData.PGL_MonsterGroupConfig groupConfig = PGL_Main.GetMonsterGroupConfig(groupID);
        if (groupConfig == null)
        {
            Debug.LogError($"找不到ID为{groupID}的怪物组配置！");
            return;
        }

        if (groupConfig.monsterIDList != null && groupConfig.monsterIDList.Count > 0)
        {
            foreach (int monsterID in groupConfig.monsterIDList)
            {
                if (!activeMonsterDict.TryGetValue(monsterID, out PGL_MonsterBase monsterBase))
                    continue;

                monsterBase.Show(true);
            }
        }
    }

    private void HideMonster(string arg1, object arg2)
    {
        int monsterID = (int)arg2;
        
        if (!activeMonsterDict.TryGetValue(monsterID, out PGL_MonsterBase monsterBase))
            return;

        monsterBase.Show(false);
    }

    private void ShowMonster(string arg1, object arg2)
    {
        int monsterID = (int)arg2;
        
        if (!activeMonsterDict.TryGetValue(monsterID, out PGL_MonsterBase monsterBase))
            return;
        
        monsterBase.Show(true);
    }
    
    private void DestroyMonsterGroup(string arg1, object arg2)
    {
        int groupID = (int)arg2;
        
        PGL_MonsterConfigData.PGL_MonsterGroupConfig groupConfig = PGL_Main.GetMonsterGroupConfig(groupID);
        if (groupConfig == null)
        {
            Debug.LogError($"找不到ID为{groupID}的怪物组配置！");
            return;
        }

        if (groupConfig.monsterIDList != null && groupConfig.monsterIDList.Count > 0)
        {
            foreach (int monsterID in groupConfig.monsterIDList)
            {
                if(!activeMonsterDict.Remove(monsterID, out PGL_MonsterBase monsterBase))
                    continue;

                Destroy(monsterBase.gameObject);
            }
        }
    }

    private void DestroyMonster(string arg1, object arg2)
    {
        int monsterID = (int)arg2;
        
        if(!activeMonsterDict.Remove(monsterID, out PGL_MonsterBase monsterBase))
            return;

        Destroy(monsterBase.gameObject);
    }
    private void MonsterGroupBoom(string arg1, object arg2)
    {
        int groupID = (int)arg2;
        
        PGL_MonsterConfigData.PGL_MonsterGroupConfig groupConfig = PGL_Main.GetMonsterGroupConfig(groupID);
        if (groupConfig == null)
        {
            Debug.LogError($"找不到ID为{groupID}的怪物组配置！");
            return;
        }

        if (groupConfig.monsterIDList != null && groupConfig.monsterIDList.Count > 0)
        {
            foreach (int monsterID in groupConfig.monsterIDList)
            {
                if (!activeMonsterDict.TryGetValue(monsterID, out PGL_MonsterBase monsterBase))
                    continue;

                monsterBase.Boom();
            }
        }
    }

    private void MonsterBoom(string arg1, object arg2)
    {
        int monsterID = (int)arg2;

        if (!activeMonsterDict.TryGetValue(monsterID, out PGL_MonsterBase monsterBase))
            return;

        monsterBase.Boom();
    }
    
    public List<PGL_MonsterBase> GetWeapon2KillUnits(Vector3 pos)
    {
        if (this.activeMonsterDict is not { Count: > 0 })
            return null;

        pos.z = 0;
        var units = new List<PGL_MonsterBase>();
        foreach (var monster in this.activeMonsterDict.Values)
        {
            if (monster.Distance(pos) < 400)
            {
                units.Add(monster);
            }
        }

        return units;
    }
}
