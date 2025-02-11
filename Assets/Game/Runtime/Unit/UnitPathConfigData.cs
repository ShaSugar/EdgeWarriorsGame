using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "unitPathConfigData", menuName = "GameConfig/路径配置",order = 0)]
public class UnitPathConfigData : ScriptableObject
{
    [Serializable]
    public class UnitRandPathConfig
    {
        // 所属怪物id
        public int unitId;
        // 进入速度
        public float inSpeed;
        // 退出速度
        public float outSpeed;
        // 进入路径起点
        public Vector3 inStartPos;
        // 进入路径起点列表
        public Vector3[] inStartPosList;
        // 进入路径中间点1
        public Vector3 inCenterPos1;
        // 进入路径中间点2
        public Vector3 inCenterPos2;
        // // 攻击点1
        // public Vector3 inAttackPos1;
        // // 攻击点2
        // public Vector3 inAttackPos2;
        // 退出路径中间点1
        public Vector3 outCenterPos1;
        // 退出路径中间点2
        public Vector3 outCenterPos2;
        // 退出路径终点
        public Vector3 outEndPos;
        // 退出路径终点列表
        public Vector3[] outEndPosList;
        // 最小攻击时间间隔
        public float minAttackTime;
        // 最大攻击时间间隔
        public float maxAttackTime;
        // 攻击次数
        public int attackCount;
        // 攻击点列表
        public int[] attackIndexList;
        // 攻击百分比
        public int attackPercent;
    }
    [Serializable]
    public class UnitFixedPathConfig
    {// 怪物固定路径配置
        // 所属怪物id
        public int unitId;
        // 进入速度
        public float inSpeed;
        // 退出速度
        public float outSpeed;
        // 路径点列表
        public Vector3[] posList;
        // 攻击点起始序号
        public int attackPos1;
        // 攻击点结束序号
        public int attackPos2;
        // 最小攻击时间间隔
        public float minAttackTime;
        // 最大攻击时间间隔
        public float maxAttackTime;
        // 攻击次数
        public int attackCount;
        // 攻击百分比
        public int attackPercent;
    }
    [Serializable]
    public class UnitBossConfig
    {
        // 所属怪物id
        public int unitId;
        // boss角度
        public Vector3 angle; 
        // 进场起点
        public Vector3 enterStartPos;
        // 进场终点
        public Vector3 enterEndPos;
        // 进场总时间(即到达这个时间后进入表演阶段)
        public float enterTotalTime;
        
        // // 玩法1 需击杀怪物id（-1为任意怪物）
        // public int play1TargetKillUnitId;
        // // 玩法1 需击杀怪物数量
        // public int play1TargetKillNum;
        // 玩法1 相机位置,旋转
        public Vector3 play1CameraPos, play1CameraRot;
        // 玩法1 boss产小怪时间
        public float play1SpawnTime;
        // 玩法1 产怪组列表每四个一组
        public List<int> play1SpawnIds;
        // 玩法1 boss攻击时间最小间隔
        public float play1AttackMinTime;
        // 玩法1 boss攻击时间最大间隔
        public float play1AttackMaxTime;
        
        // 玩法2 配置列表
        public List<UnitBossPlay2Config> play2Configs;
    }
    
    [Serializable]
    public class UnitBossPlay2Config
    {
        // 玩法时长
        public float countTime;
        // 相机位置,旋转
        public Vector3 cameraPos, cameraRot;
        // 击打点怪物id
        public int hitPointUnitId;
        // 击打点位置列表
        public List<Vector3> hitPointPosList;
    }

    // 产怪点id
    public int spawnId;
    // 随机路径列表
    public List<UnitRandPathConfig> randPathConfigs;
    // 固定路径怪物配置列表
    public List<UnitFixedPathConfig> fixedPathConfigs;
    // boss怪物配置列表
    public List<UnitBossConfig> unitBossConfigs;
}



