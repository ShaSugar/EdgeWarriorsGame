using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PGL_AniEventMonster : MonoBehaviour
{
    private PGL_MonsterConfigData.PGL_MonsterConfig _config;

    public void UpdateMonsterConfig(PGL_MonsterConfigData.PGL_MonsterConfig config)
    {
        _config = config;
    }
    
    // 扣血事件
    private void DeductPlayerHp()
    {
        if (_config is not { attack: > 0 })
            return;
        
        EventMgr.Instance.Emit(UnitMgr.UnitAttackEvent, _config.attack);
        
        if (Random.Range(0, 100) < _config.attackShakePercent)
            EventMgr.Instance.Emit(CameraController.CameraShakeEvent2, null);
    }

    private void OnDestroy()
    {
        _config = null;
    }
}
