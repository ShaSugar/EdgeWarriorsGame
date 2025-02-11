using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterTypeBossConfig1_5", menuName = "GameConfig/怪物类型Boss配置", order = 0)]
[Serializable]
public class MonsterTypeBossConfig : ScriptableObject
{
    [Serializable]
    public class MonsterTypeBossPlay2Config
    {
        // 玩法时长
        public float countTime;
        // 相机位置,旋转
        public Vector3 cameraPos, cameraRot;
        // 击打点怪物id
        public int hitPointUnitId;
        // 击打点位置列表
        public List<Vector3> hitPointPosList;

        // 镜头拉近时间
        public float cameraZoomInTime;
        // 镜头复位时间
        public float cameraZoomOutTime;
        // 动作放慢延时时间
        public float actionSlowDelayTime;
        // 击打点出现延时时间
        public float hitPointAppearDelayTime;
    }

    // 所属怪物id
    public int unitId;
    // 产怪点id
    public int spawnId;
    // boss角度
    public Vector3 angle;
    // 进场起点
    public Vector3 enterStartPos;
    // 进场终点
    public Vector3 enterEndPos;
    //进场是否隐藏
    public bool ApproachHide;
    // 进场总时间(即到达这个时间后进入表演阶段)
    public float enterTotalTime;
    //进场时间
    public float boss_action_enter_time;
    //boss攻击时间
    public float[] boss_action_attack_timeline_time;
    public float[] boss_action_attack_hp_time;

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
    public MonsterTypeBossPlay2Config[] play2Configs;
}


