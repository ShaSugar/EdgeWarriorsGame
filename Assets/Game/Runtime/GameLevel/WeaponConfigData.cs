using System;
using UnityEngine;

public enum WeaponType
{
    Default = 0,// 默认武器
    Shotgun = 1,// 霰弹枪
    RocketGun = 2,// 火箭炮
}

[CreateAssetMenu(fileName = "WeaponConfigData", menuName = "GameConfig/枪配置",order = 0)]
public class WeaponConfigData : ScriptableObject
{
    [Serializable]
    public class WeaponConfig
    {
        // 武器类型
        public WeaponType weaponType;
        // 开枪时间间隔
        public float interval;
    }
    
    public WeaponConfig defaultWeapon = new WeaponConfig() { weaponType = WeaponType.Default, interval = 0.3f };
    public WeaponConfig shotgunWeapon = new WeaponConfig() { weaponType = WeaponType.Shotgun, interval = 0.5f };
    public WeaponConfig rocketGunWeapon = new WeaponConfig() { weaponType = WeaponType.RocketGun, interval = 1f };
}
