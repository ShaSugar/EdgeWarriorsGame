using UnityEngine;

public class PGL_ShotEventListener : MonoBehaviour
{
    public void Shot(int val)
    {
        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, val != 0); //触发是否可以射击
    }
}
