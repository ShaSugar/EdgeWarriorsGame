using System;
using System.Collections.Generic;
using AutoLOD.MeshDecimator.QualityMeshDecimator.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSceneConfigData", menuName = "GameConfig/场景配置",order = 0)]
public class GameSceneConfigData : ScriptableObject
{
    [Serializable]
    public class GameSceneConfig
    {
        // 场景名称
        public string sceneName;
        // 场景玩法
        public GameScenePlay playType;
        // 初始相机位置
        public Vector3 initCameraPos;
        // 初始相机角度
        public Vector3 initCameraAngle;
        // 是否显示开场效果
        public bool isShowGameStartEffect;
        // 可继续挑战冷却时间
        public int continueTime;
        
        // 关卡数量
        public int levelNum;
    }

    // 关卡配置列表
    public List<GameSceneConfig> data;
}
