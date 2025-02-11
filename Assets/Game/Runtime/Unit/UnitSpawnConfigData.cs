using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "unitSpawnConfigData", menuName = "GameConfig/产怪配置",order = 0)]
public class UnitSpawnConfigData : ScriptableObject
{
    [Serializable]
    public class UnitSpawnConfig
    {
        // 出怪点ID
        public int spawnId;
        // 首次产怪时间
        public float delay;
        // 产怪最小时间间隔
        public float minInterval;
        // 产怪最大时间间隔
        public float maxInterval;
        // 产怪次数(<=0为无限产怪)
        public int spawnCount;
        // 产怪持续时间(<=0为无限)
        public float durationTime;
        // 怪物ID列表
        public int[] unitIds;
        // 是否能马上补充怪物
        public bool canSpawnNow;
        // 是否按顺序出怪
        public bool sequence;
        // 每次产怪最小数量
        public int minSpawnCount;
        // 每次产怪最大数量
        public int maxSpawnCount;
    }

    public List<UnitSpawnConfig> data;
}
