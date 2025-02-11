using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterType1PathConfigData1_1", menuName = "GameConfig/怪物类型1路径配置",order = 0)]
[Serializable]
public class MonsterType1PathConfig : ScriptableObject
{
    [Serializable]
    public class PathPos
    {
        // 进入路径起点列表
        public Vector3[] inStartPosList;
        // 进入路径中间点1
        public Vector3 inCenterPos1;
        // 进入路径中间点2
        public Vector3 inCenterPos2;
        // 退出路径中间点1
        public Vector3 outCenterPos1;
        // 退出路径中间点2
        public Vector3 outCenterPos2;
        // 退出路径终点列表
        public Vector3[] outEndPosList;
        // 攻击点列表
        public int[] attackIndexList;
        // 是否播放出场动作
        public bool isPlayEnterAnim;
        // 不能攻击时是否进入攻击点
        public bool isEnterAttackPos;
    }
    // 所属怪物id
    public int unitId;
    // 产怪点id
    public int spawnId;
    // 进入速度
    public float inSpeed;
    // 退出速度
    public float outSpeed;
    // 最小攻击时间间隔
    public float minAttackTime;
    // 最大攻击时间间隔
    public float maxAttackTime;
    // 攻击次数
    public int attackCount;
    // 攻击百分比
    public int attackPercent;

    // 暴击百分比
    public int strikePercent;
    // 暴击攻击力百分比
    public int strikeAttackPercent;

    public PathPos[] pathPosList;
}
