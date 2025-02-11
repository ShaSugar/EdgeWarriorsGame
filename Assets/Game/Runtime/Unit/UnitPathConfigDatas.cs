using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "unitPathConfigDatas", menuName = "GameConfig/产怪点路径配置",order = 0)]
public class UnitPathConfigDatas : ScriptableObject
{
    public List<UnitPathConfigData> datas;
}
