using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterType2PathConfigData1_1", menuName = "GameConfig/怪物类型2路径配置",order = 0)]
[Serializable]
public class MonsterType2PathConfig : ScriptableObject
{
    [Serializable]
    public class PathPos
    {
        // 进入路径列表
        public Vector3[] inPosList;
        // 退出路径列表
        public Vector3[] outPosList;
        // 攻击点序号
        public int attackIndex;
        public float enterAnimTimer;
        // 是否播放出场动作
        public bool isPlayEnterAnim;
        // 能否翻转
        public bool reverseFlag;
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
    
    public PathPos[] pathPosList;
}
