using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoopConfig_plotScene1", menuName = "GameConfig/剧情关卡相机循环动画配置",order = 0)]
public class PGL_CameraAniLoopClipConfigData : ScriptableObject
{
    [Serializable]
    public class PGL_CameraAniLoopClipConfig
    {
        // id
        public int id;
        // 相机循环动画
        public AnimationClip aniLoopClip;
        // 循环结束条件-时间（单位：秒  0表示不以时间做为结束条件）
        public float loopEndTime;
        // 循环结束条件-击杀怪物数量（0表示不以击杀数量做为结束条件）
        public int killMonsterCount;
    }

    // 配置列表
    public List<PGL_CameraAniLoopClipConfig> data;
}
