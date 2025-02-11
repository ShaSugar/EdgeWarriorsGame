using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterPathMgr : Singleton<MonsterPathMgr>
{
    List<MonsterType1PathConfig> monsterType1PathConfigs;
    List<MonsterType2PathConfig> monsterType2PathConfigs;
    List<MonsterTypeBossConfig> monsterTypeBossConfigs;
    public MonsterType1PathConfig GetMonsterType1PathConfig(int spawnId, int unitId)
    {
        this.monsterType1PathConfigs ??= new List<MonsterType1PathConfig>();
        MonsterType1PathConfig pathConfig = this.monsterType1PathConfigs.FirstOrDefault(config => config.spawnId == spawnId && config.unitId == unitId);
        if (pathConfig != null)
            return pathConfig;
        pathConfig = ResMgr.Instance.LoadAssetSync<MonsterType1PathConfig>($"Config/scene{GameSceneMgr.Instance.CurScene}/MonsterType1PathConfigData{spawnId}_{unitId}");
        if (pathConfig == null)
        {
            Debug.LogError($"MonsterType1PathConfigData{spawnId}_{unitId} not found");
            return null;
        }
            
        this.monsterType1PathConfigs.Add(pathConfig);

        return pathConfig;
    }
    public MonsterType2PathConfig GetMonsterType2PathConfig(int spawnId, int unitId)
    {
        this.monsterType2PathConfigs ??= new List<MonsterType2PathConfig>();
        MonsterType2PathConfig pathConfig = this.monsterType2PathConfigs.FirstOrDefault(config => config.spawnId == spawnId && config.unitId == unitId);
        if (pathConfig != null)
            return pathConfig;

        pathConfig = ResMgr.Instance.LoadAssetSync<MonsterType2PathConfig>($"Config/scene{GameSceneMgr.Instance.CurScene}/MonsterType2PathConfigData{spawnId}_{unitId}");
        if (pathConfig == null)
        {
            Debug.LogError($"MonsterType2PathConfigData{spawnId}_{unitId} not found");
            return null;
        }
            
        this.monsterType2PathConfigs.Add(pathConfig);

        return pathConfig;
    }
    public MonsterTypeBossConfig GetMonsterTypeBossConfig(int spawnId, int unitId)
    {
        this.monsterTypeBossConfigs ??= new List<MonsterTypeBossConfig>();
        MonsterTypeBossConfig pathConfig = this.monsterTypeBossConfigs.FirstOrDefault(config => config.spawnId == spawnId && config.unitId == unitId);
        if (pathConfig != null)
            return pathConfig;

        pathConfig = ResMgr.Instance.LoadAssetSync<MonsterTypeBossConfig>($"Config/scene{GameSceneMgr.Instance.CurScene}/MonsterTypeBossConfig{spawnId}_{unitId}");
        if (pathConfig == null)
        {
            Debug.LogError($"MonsterTypeBossConfig{spawnId}_{unitId} not found");
            return null;
        }
            
        this.monsterTypeBossConfigs.Add(pathConfig);

        return pathConfig;
    }
}
