using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class MonsterBase : MonoBehaviour
{
    protected const string ACTION_IDLE_NAME = "idle";
    protected const string ACTION_RUN_NAME = "run";
    
    static readonly protected int idleFlag = Animator.StringToHash("idle_flag");
    static readonly protected int action_enter = Animator.StringToHash("enter");
    static readonly protected int action_idle = Animator.StringToHash("idle");
    static readonly protected int action_run = Animator.StringToHash("run");
    static readonly protected int action_attack = Animator.StringToHash("attack");
    static readonly protected int action_die = Animator.StringToHash("die");
    static readonly protected int action_spawn = Animator.StringToHash("spawn"); // 产怪（休闲）

    protected Transform coinSlot, itemSlot;
    protected Collider unitCollider;
    protected int unitId;
    protected GameObject obj;
    protected Transform tran;
    protected int lessHP;
    protected int timerId = -1;
    private static bool isPlayVoice;
    public bool RecycleFlag { get; protected set; }

    public UnitConfigData.UnitConfig Config { get; protected set; }

    public virtual void Init(GameObject obj, int unitId)
    {
        this.unitId = unitId;
        this.Config = UnitMgr.Instance.GetUnitConfig(unitId);

        this.obj = obj;
        this.tran = this.obj.transform;
        this.unitCollider = this.obj.GetComponent<Collider>();
        coinSlot = this.tran.Find("coin_slot");
        itemSlot = this.tran.Find("item_slot");
    }
    public virtual void SetParentTran(Transform parent)
    {
        this.tran.SetParent(parent);
        this.tran.localPosition = Vector3.zero;
        this.tran.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public virtual void MoveOutQuickly()
    {
        Recycle();
    }
    public virtual void Recycle()
    {
        TimerMgr.Instance.UnSchedule(this.timerId);
        this.tran.DOKill();
        this.RecycleFlag = true;
        this.lessHP = 0;
        this.obj.SetActive(false);
        if (this.unitCollider != null)
            this.unitCollider.enabled = false;
    }
    public virtual void HitByPlayer(int player, int hp, int weapon)
    {

    }
    public virtual void Destroy()
    {
        GameObject.DestroyImmediate(this.obj);
    }
    public virtual float Distance(Vector3 pos)
    {
        if (this.unitCollider == null)
            return float.MaxValue;

        Vector3 unitPos = CameraController.Instance.MainCamera.WorldToScreenPoint(this.unitCollider.bounds.center);
        unitPos.z = 0;
        return Vector3.Distance(unitPos, pos);
    }

    protected void DoPathBySpeed(Vector3[] pathPosArray, float speed, Action completeCallback)
    {
        this.tran.DOLocalPath(pathPosArray, speed, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetOptions(false)
            .SetLookAt(0, true)
            .SetSpeedBased()
            .OnUpdate(() =>
            {
                Vector3 angle = this.tran.localEulerAngles;
                angle.z = 0;
                this.tran.localEulerAngles = angle;
            })
            .OnComplete(() =>
            {
                completeCallback?.Invoke();
            });
    }
    protected void DoPathByTime(Vector3[] pathPosArray, float time, Action completeCallback)
    {
        this.tran.DOLocalPath(pathPosArray, time, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetOptions(false)
            .SetLookAt(0, true)
            .OnUpdate(() =>
            {
                Vector3 angle = this.tran.localEulerAngles;
                angle.z = 0;
                this.tran.localEulerAngles = angle;
            })
            .OnComplete(() =>
            {
                completeCallback?.Invoke();
            });
    }
    protected float GetSpeed(float speed)
    {
        return speed > 0 ? speed : Random.Range(this.Config.minMoveSpeed, this.Config.maxMoveSpeed);
    }

    protected bool PlayVoice()
    {
        if (this.Config.deathVoicePercent <= 0)
            return false;

        if (Random.Range(0, 100) > this.Config.deathVoicePercent)
            return false;

        if (isPlayVoice)
            return false;

        isPlayVoice = true;
        TimerMgr.Instance.ScheduleOnce(o =>
        {
            isPlayVoice = false;
        }, 2f);

        if (MachineDataMgr.Instance.IsChineseLanguageVersion)
        {
            if (this.Config.deathVoiceList.Length <= 0)
                return false;

            int index = Random.Range(0, this.Config.deathVoiceList.Length);
            SoundMgr.Instance.PlayOneShot(this.Config.deathVoiceList[index]);

            return true;
        }
        else
        {
            if (this.Config.deathVoiceListEN.Length <= 0)
                return false;

            int index = Random.Range(0, this.Config.deathVoiceListEN.Length);
            SoundMgr.Instance.PlayOneShot(this.Config.deathVoiceListEN[index]);

            return true;
        }
    }

    protected Vector3 GetKillScorePos()
    {
        return this.coinSlot != null ? this.coinSlot.position : this.tran.position;
    }

    protected Vector3 GetPropPos()
    {
        return this.itemSlot != null ? this.itemSlot.position : this.tran.position;
    }

}
