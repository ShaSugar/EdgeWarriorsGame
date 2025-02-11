using System;
using System.Collections.Generic;
using AutoLOD.MeshDecimator.QualityMeshDecimator.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "gameLevelConfigData", menuName = "GameConfig/关卡配置",order = 0)]
public class GameLevelConfigData : ScriptableObject
{
    [Serializable]
    public class CameraMoveConfig
    {
        public float delayTime;
        public AnimationClip animClip;
    }
    [Serializable]
    public class GameLevelConfig
    {
        public int scene;
        // 关卡ID
        public int level;
        // 击杀怪物种类-1为任意怪物
        public int unitId;
        // 挑战关卡提示文字
        public string levelStr;
        // 挑战关卡成功提示文字
        public string winStr;
        // 挑战关卡失败提示文字
        public string failStr;
        // 挑战关卡提示文字（英文）
        public string levelStr_EN;
        // 挑战关卡成功提示文字（英文）
        public string winStr_EN;
        // 挑战关卡失败提示文字（英文）
        public string failStr_EN;
        // 关卡时间走完后可继续挑战冷却时间
        public int continueTime;
        // 场上怪物最大数量
        public int maxUnitNum;
        // 场上怪物最大数量2
        public int maxUnitNum2;
        // 单个怪物数量限制
        public SerializableDictionary<int, int> singleUnitLimit;
        //是否提前产怪
        public bool isAdvanceSpawn;
        // 出怪点ID列表
        public int[] spawnIds;
        // 镜头坐标
        public Vector3 cameraPos;
        // 镜头角度
        public Vector3 cameraAngle;
        // 镜头前怪物数量（即停留在攻击点的怪物数量）
        public int stayInAttackUnitNum;
        // 最终攻击点列表
        public Vector3[] attackPointList;
        // 进入攻击点前的偏移量 ---> 预攻击点，没有怪物攻击就进到 最终攻击点列表
        public Vector3[] attackPointPre;
        // 是否是boss关卡(boss关卡需要boss来控制什么时候开始计时，什么时候允许开枪)
        public bool isBossLevel;
        // 关卡背景音乐
        public string bgmPath;
        
        // 关卡开始摄像机动画
        public AnimationClip cameraMoveAnim;
        // 是否显示通关条件文字提示
        public bool isShowPassConditionText;
        // 是否显示关卡结果文字提示
        public bool isShowResultText;
        
        public CameraMoveConfig[] cameraMoveConfigs;

    }

    // // 总关卡数量
    // public int levelNum;
    // // 场景对应关卡数量
    // public int[] sceneLevelNum;
    // 关卡配置列表
    public List<GameLevelConfig> data;
    
    // public Vector3[] initSceneCameraPos;
    // public Vector3[] initSceneCameraAngle;

    // 第1~3关卡镜头移动时间
    public float normalLevelCameraMoveTime;
}
