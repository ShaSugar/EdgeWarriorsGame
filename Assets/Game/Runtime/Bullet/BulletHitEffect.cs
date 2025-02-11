using UnityEngine;

/// <summary>
/// 子弹击中特效
/// </summary>
public class BulletHitEffect
{
    GameObject obj;
    Transform tran;
    string prefab;
    public BulletHitEffect(string prefab)
    {
        this.prefab = prefab;
        var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(prefab);
        this.obj = Object.Instantiate(nodePrefab);
        this.obj.SetActive(false);
        this.tran = this.obj.transform;
        this.tran.parent = CameraController.Instance.BulletCameraTran;
        this.tran.localPosition = Vector3.zero;
        this.tran.localEulerAngles = Vector3.zero;
        this.tran.localScale = Vector3.one;
    }
    public void Destroy()
    {
        RemoveTimer();
        Object.DestroyImmediate(this.obj);
    }

    int timerId = -1;
    /// <summary>
    /// 显示击中特效
    /// </summary>
    /// <param name="pos"></param>
    public void Show(Vector3 pos)
    {
        RemoveTimer();
        this.tran.position = pos;
        this.obj.SetActive(true);
        this.timerId = TimerMgr.Instance.ScheduleOnce(o =>
        {
            Recycle();
        }, 0.5f);
    }
    
    public void Recycle()
    {
        RemoveTimer();
        this.obj.SetActive(false);
        BulletEffectMgr.Instance.RecycleBulletHitEffect(this.prefab, this);
    }

    void RemoveTimer()
    {
        if (this.timerId != -1)
        {
            TimerMgr.Instance.UnSchedule(this.timerId);
            this.timerId = -1;
        }
    }
}
