using System;
using System.Collections;
using System.Collections.Generic;
using AutoLOD.MeshDecimator.QualityMeshDecimator.Internal;
using UnityEngine;


[CreateAssetMenu(fileName = "itemConfigData", menuName = "GameConfig/道具配置",order = 0)]
public class ItemConfigData : ScriptableObject
{
    [Serializable]
    public class ItemConfig
    {
        public EItemType itemType;
        public int itemParam;
    }

    public ItemConfig hpConfig = new ItemConfig() { itemType = EItemType.HP_SUPPLY, itemParam = 100 };
    public ItemConfig doubleConfig = new ItemConfig() { itemType = EItemType.SCORE_DOUBLE, itemParam = 10 };
    public ItemConfig bulletConfig = new ItemConfig() { itemType = EItemType.SHELL_GUN, itemParam = 30 };
    public ItemConfig rocketConfig = new ItemConfig() { itemType = EItemType.BOLT_CANNON, itemParam = 5 };
}
