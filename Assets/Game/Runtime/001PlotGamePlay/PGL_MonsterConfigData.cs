using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterConfigData_plotScene1", menuName = "GameConfig/剧情玩法怪物配置",order = 0)]
public class PGL_MonsterConfigData : ScriptableObject
{
    [Serializable]
    public class PGL_MonsterConfig
    {
        // 怪物ID
        public int monsterID;
        // 怪物类型
        public PGL_MonsterType monsterType;
        // 怪物预设
        public string prefab;
        
        // 生成后是否自动激活
        public bool autoActive;
        
        // 入场动作
        public string enterAction;
        // 待机动作
        public string idleAction;
        // 受击动作
        public string hitAction;
        // 跑步动作
        public string runAction;
        // 攻击动作
        public string attackAction;
        // 死亡动作
        public string dieAction;
        
        // 路径曲线类型
        public Ease ease;
        // 路径是否锁定Z轴
        public bool isLockAngleZ;
        
        // 进入路径起点列表
        public Vector3[] enterStartPosList;
        // 进入路径速度
        public float enterPathSpeed;

        // 消失路径点列表
        public Vector3[] outStartPosList;
        // 进入路径速度
        public float outPathSpeed;

        // 生命值
        public int health;
        // 积分
        public int score;
        // 攻击力
        public int attack;
        
        
        // 死亡效果预设
        public string dieEffectPrefab;
        // 攻击音效
        public string attackSound;
        // 死亡音效
        public string dieSound;
        // 死亡人声概率
        public int dieVoicePercent;
        // 死亡人声列表
        public string[] dieVoiceList;
        // 死亡人声列表英文
        public string[] dieVoiceListEN;
        
        // 最小攻击时间间隔
        public float minAttackTime;
        // 最大攻击时间间隔
        public float maxAttackTime;
        // 攻击百分比
        public int attackPercent;
        // 攻击震屏百分比
        public int attackShakePercent;
        
        // 爆炸音效
        public string bombSound;
        // 爆炸伤害
        public int bombDamage;
    }

    [Serializable]
    public class PGL_MonsterGroupConfig
    {
        public int groupID;
        
        public List<int> monsterIDList = new List<int>();
    }
    
    public List<PGL_MonsterConfig> data = new List<PGL_MonsterConfig>();
    public List<PGL_MonsterGroupConfig> groupData = new List<PGL_MonsterGroupConfig>();
}
