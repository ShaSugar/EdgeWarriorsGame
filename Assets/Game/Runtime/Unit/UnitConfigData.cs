using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfigData", menuName = "GameConfig/怪物配置",order = 0)]
public class UnitConfigData : ScriptableObject
{
    [Serializable]
    public class UnitConfig
    {
        // 怪物ID
        public int unitId;
        // 怪物种类
        public ERealUnitType realUnitType;
        // 怪物类型
        public EUnitType unitType;
        // 怪物预设
        public string prefab;
        // 预生成数量
        public int preloadCount = 1;
        // 生命值
        public int health;
        // 积分
        public int score;
        // 攻击力
        public int attack;
        // 进场动画时间
        public float enterAniTime;
        // 攻击动画时间
        public float attackAniTime;
        // 死亡动画时间
        public float deathAniTime;
        // 攻击扣血延时时间
        public float attackDeductTime;
        // 最小运动速度
        public float minMoveSpeed;
        // 最大运动速度
        public float maxMoveSpeed;
        // // 获得道具概率
        // public int propPercent;
        // 死亡效果预设
        public string deathEffect;
        // 攻击音效
        public string attackSound;
        // 死亡音效
        public string deathSound;
        // 死亡人声概率
        public int deathVoicePercent;
        // 死亡人声列表
        public string[] deathVoiceList;
        // 死亡人声列表英文
        public string[] deathVoiceListEN;
    }

    public List<UnitConfig> data = new List<UnitConfig>(8);
}
