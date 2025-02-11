using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterType1 : MonsterBase
{
    protected int spawnId;
    protected MonsterType1PathConfig pathConfig;

    protected Animator animator;
    protected GameObject attackTimeline;
    protected int attackTimer = -1;
    protected Transform cameraTran;
    protected int attackCount;
    protected bool isStayInAttackPoint;
    protected int attackPointIndex;
    protected Vector3 attackPos;
    protected Vector3 attackPosPre;
    protected int pathPosIndex;
    protected List<Vector3> pathPosList;

    Transform dieEffectTran;

    bool isEntering;

    public override void Init(GameObject obj, int unitId)
    {
        base.Init(obj, unitId);
        this.pathPosList = new List<Vector3>();
        this.animator = this.tran.GetChild(0).GetComponent<Animator>();
        this.cameraTran = CameraController.Instance.MainCameraRootTran;
        this.dieEffectTran = this.tran.Find("dieEffectSlot");
        Transform attackTimelineTran = this.tran.Find("attackTimeline");
        this.attackTimeline = attackTimelineTran == null ? null : attackTimelineTran.gameObject;

        Recycle();
    }

    public void StartMoveIn(int spawnId, int unitId)
    {
        Recycle();
        this.RecycleFlag = false;

        this.pathConfig = MonsterPathMgr.Instance.GetMonsterType1PathConfig(spawnId, unitId);
        GetPathPosListIndex();
        GetAttackPointIndex(false);

        this.pathPosList.Clear();
        this.pathPosList.Add(GetVector3(this.pathConfig.pathPosList[this.pathPosIndex].inStartPosList, true));

        this.unitId = unitId;
        this.spawnId = spawnId;

        this.animator.enabled = true;
        this.tran.localPosition = this.pathPosList[0];
        this.tran.LookAt(this.cameraTran);
        this.obj.SetActive(true);

        if (this.pathConfig.pathPosList[this.pathPosIndex].isPlayEnterAnim)
        {
            this.isEntering = true;
            this.animator.Play(action_enter, 0, 0);
            this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { EnterFinish(); }, this.Config.enterAniTime);
        }
        else
        {
            EnterFinish();
        }
    }
    void StartMoveIn(MonsterType1PathConfig pathConfig)
    {
        Recycle();
        this.RecycleFlag = false;

        this.pathConfig = pathConfig;
        GetPathPosListIndex();
        GetAttackPointIndex(false);

        this.pathPosList.Clear();
        this.pathPosList.Add(GetVector3(this.pathConfig.pathPosList[this.pathPosIndex].inStartPosList, true));

        this.unitId = this.pathConfig.unitId;
        this.spawnId = this.pathConfig.spawnId;

        this.animator.enabled = true;
        this.tran.localPosition = this.pathPosList[0];
        this.tran.LookAt(this.cameraTran);
        this.obj.SetActive(true);

        if (this.pathConfig.pathPosList[this.pathPosIndex].isPlayEnterAnim)
        {
            this.isEntering = true;
            this.animator.Play(action_enter, 0, 0);
            this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { EnterFinish(); }, this.Config.enterAniTime);
        }
        else
        {
            EnterFinish();
        }
    }
    void EnterFinish()
    {
        this.isEntering = false;
        StopAllTimer();
        this.lessHP = this.Config.health;
        this.unitCollider.enabled = true;
        this.animator.Play(action_run, 0, 0);

        this.pathPosList.Add(RandomVector3(this.pathConfig.pathPosList[this.pathPosIndex].inCenterPos1,
            this.pathConfig.pathPosList[this.pathPosIndex].inCenterPos2));
        this.pathPosList.Add(this.attackPosPre);

        // if (UnitMgr.Instance.CanStayInAttackPoint(this.attackPointIndex))
        // {
        //     EventMgr.Instance.Emit(UnitMgr.UnitEnterAttackPointEvent, this.attackPointIndex);
        //     this.isStayInAttackPoint = true;
        //     this.pathPosList.Add(this.attackPos);
        // }
        // else
        // {
        //     if (this.pathConfig.pathPosList[this.pathPosIndex].isEnterAttackPos)
        //         this.pathPosList.Add(this.attackPos);
        //     GetPathPosListIndex();
        //     this.pathPosList.Add(RandomVector3(this.pathConfig.pathPosList[this.pathPosIndex].inCenterPos1,
        //         this.pathConfig.pathPosList[this.pathPosIndex].inCenterPos2));
        //     this.pathPosList.Add(GetVector3(this.pathConfig.pathPosList[this.pathPosIndex].outEndPosList));
        // }
        DoPathBySpeed(this.pathPosList.ToArray(), GetSpeed(this.pathConfig.inSpeed), () =>
        {
            // if (this.isStayInAttackPoint)
            // {// 若已占用了攻击位置，则直接进入攻击
            //     this.tran.LookAt(this.cameraTran.position);
            //     PlayAttack();
            // }
            // else
            // {
            // // 判断攻击位置是否可用，若可用，则占用攻击位置进入攻击，否则直接走退出路径
            // if (Random.Range(0, 100) < this.pathConfig.attackPercent)
            // {
                if (UnitMgr.Instance.CanStayInAttackPoint(this.attackPointIndex))
                {
                    EventMgr.Instance.Emit(UnitMgr.UnitEnterAttackPointEvent, this.attackPointIndex);
                    this.isStayInAttackPoint = true;

                    this.pathPosList.Clear();
                    this.pathPosList.Add(this.tran.localPosition);
                    this.pathPosList.Add(this.attackPos);
                    this.pathPosList.Add(this.attackPos);
                    DoPathBySpeed(this.pathPosList.ToArray(), GetSpeed(this.pathConfig.inSpeed), PlayAttack);
                }
                else
                {
                    // // 重新寻找一个可以攻击的位置
                    // GetAttackPointIndex();
                    // if (UnitMgr.Instance.CanStayInAttackPoint(this.attackPointIndex))
                    // {
                    //     EventMgr.Instance.Emit(UnitMgr.UnitEnterAttackPointEvent, this.attackPointIndex);
                    //     this.isStayInAttackPoint = true;
                    //
                    //     this.pathPosList.Clear();
                    //     this.pathPosList.Add(this.tran.localPosition);
                    //     this.pathPosList.Add(this.attackPos);
                    //     this.pathPosList.Add(this.attackPos);
                    //     DoPathBySpeed(this.pathPosList.ToArray(), GetSpeed(this.pathConfig.inSpeed), PlayAttack);
                    // }
                    // else
                    {
                        this.pathPosList.Clear();
                        this.pathPosList.Add(this.tran.localPosition);
                        // 退出是否需要经过攻击位置
                        if (this.pathConfig.pathPosList[this.pathPosIndex].isEnterAttackPos)
                            this.pathPosList.Add(this.attackPos);
                        GetPathPosListIndex();
                        this.pathPosList.Add(RandomVector3(this.pathConfig.pathPosList[this.pathPosIndex].outCenterPos1,
                            this.pathConfig.pathPosList[this.pathPosIndex].outCenterPos2));
                        this.pathPosList.Add(GetVector3(this.pathConfig.pathPosList[this.pathPosIndex].outEndPosList));
                        DoPathBySpeed(this.pathPosList.ToArray(), GetSpeed(this.pathConfig.inSpeed),
                            () => { StartMoveIn(this.pathConfig); });
                    }
                }
            // }
            // else
            // {
            //     this.pathPosList.Clear();
            //     this.pathPosList.Add(this.tran.localPosition);
            //     // 退出是否需要经过攻击位置
            //     if (this.pathConfig.pathPosList[this.pathPosIndex].isEnterAttackPos)
            //         this.pathPosList.Add(this.attackPos);
            //     GetPathPosListIndex();
            //     this.pathPosList.Add(RandomVector3(this.pathConfig.pathPosList[this.pathPosIndex].inCenterPos1,
            //         this.pathConfig.pathPosList[this.pathPosIndex].inCenterPos2));
            //     this.pathPosList.Add(GetVector3(this.pathConfig.pathPosList[this.pathPosIndex].outEndPosList));
            //     DoPathBySpeed(this.pathPosList.ToArray(), GetSpeed(this.pathConfig.inSpeed),
            //         () => { StartMoveIn(this.pathConfig); });
            // }
            // }
        });
    }
    void PlayAttack()
    {
        if (this.lessHP <= 0)
            return;

        if (this.attackCount >= this.pathConfig.attackCount)
        {
            MoveOut();
            return;
        }

        StopAllTimer();

        this.attackCount++;
        this.tran.LookAt(this.cameraTran);

        if (GameApp.Instance.IsAnyPlayerCanPlay())
        {
            if (Random.Range(0, 100) < this.pathConfig.attackPercent)
            {
                SoundMgr.Instance.PlayOneShot(this.Config.attackSound);
                this.animator.Play(action_attack, 0, 0);
                this.attackTimeline?.SetActive(false);
                this.attackTimeline?.SetActive(true);
                this.attackTimer = TimerMgr.Instance.ScheduleOnce((_) =>
                {
                    EventMgr.Instance.Emit(CameraController.CameraShakeEvent2, null);
                    EventMgr.Instance.Emit(UnitMgr.UnitAttackEvent, GetAttack());
                }, this.Config.attackDeductTime);

                this.timerId = TimerMgr.Instance.ScheduleOnce((_) =>
                {
                    StopAllTimer();

                    if (this.attackCount >= this.pathConfig.attackCount)
                    {
                        MoveOut();
                    }
                    else
                    {
                        if (!IsPlayingAni(ACTION_IDLE_NAME))
                            this.animator.Play(action_idle, 0, 0);

                        float nextAttackTime =
                            Random.Range(this.pathConfig.minAttackTime, this.pathConfig.maxAttackTime);
                        this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { PlayAttack(); }, nextAttackTime);
                    }
                }, this.Config.attackAniTime);
            }
            else
            {
                if (!IsPlayingAni(ACTION_IDLE_NAME))
                    this.animator.Play(action_idle, 0, 0);
                float nextAttackTime =
                    Random.Range(this.pathConfig.minAttackTime, this.pathConfig.maxAttackTime);
                this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { PlayAttack(); }, nextAttackTime);
            }
        }
        else
        {
            if (this.attackCount >= this.pathConfig.attackCount)
            {
                MoveOut();
            }
            else
            {
                if (!IsPlayingAni(ACTION_IDLE_NAME))
                    this.animator.Play(action_idle, 0, 0);
                float nextAttackTime =
                    Random.Range(this.pathConfig.minAttackTime, this.pathConfig.maxAttackTime);
                this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { PlayAttack(); }, nextAttackTime);
            }
        }
    }

    void MoveOut()
    {
        StopAllTimer();

        GetPathPosListIndex();

        this.pathPosList.Clear();
        this.pathPosList.Add(this.tran.localPosition);
        this.pathPosList.Add(RandomVector3(this.pathConfig.pathPosList[this.pathPosIndex].outCenterPos1,
            this.pathConfig.pathPosList[this.pathPosIndex].outCenterPos2));
        this.pathPosList.Add(GetVector3(this.pathConfig.pathPosList[this.pathPosIndex].outEndPosList));

        if (this.isStayInAttackPoint)
        {
            this.isStayInAttackPoint = false;
            EventMgr.Instance.Emit(UnitMgr.UnitLeaveAttackPointEvent, this.attackPointIndex);
        }

        if (!IsPlayingAni(ACTION_RUN_NAME))
            this.animator.Play(action_run, 0, 0);

        DoPathBySpeed(this.pathPosList.ToArray(), GetSpeed(this.pathConfig.outSpeed),
            () => { StartMoveIn(this.pathConfig); });
    }

    public override void MoveOutQuickly()
    {
        if (this.isEntering)
        {
            StopAllTimer();
            Recycle();
            return;
        }
        if (this.lessHP <= 0)
            return;

        StopAllTimer();

        this.lessHP = 0;
        this.unitCollider.enabled = false;

        if (this.isStayInAttackPoint)
        {
            this.isStayInAttackPoint = false;
            EventMgr.Instance.Emit(UnitMgr.UnitLeaveAttackPointEvent, this.attackPointIndex);
        }
        this.attackTimeline?.SetActive(false);
        if (!IsPlayingAni(ACTION_RUN_NAME))
            this.animator.Play(action_run, 0, 0);
        GetPathPosListIndex();
        this.pathPosList.Clear();
        this.pathPosList.Add(this.tran.localPosition);
        this.pathPosList.Add(RandomVector3(this.pathConfig.pathPosList[this.pathPosIndex].outCenterPos1,
            this.pathConfig.pathPosList[this.pathPosIndex].outCenterPos2));
        this.pathPosList.Add(GetVector3(this.pathConfig.pathPosList[this.pathPosIndex].outEndPosList));

        DoPathByTime(this.pathPosList.ToArray(), 0f, Recycle);
    }

    public override void Recycle()
    {
        base.Recycle();

        StopAllTimer();
        this.attackTimeline?.SetActive(false);
        this.attackCount = 0;
        this.animator.enabled = false;
        this.isStayInAttackPoint = false;
        this.pathPosList.Clear();
        this.isEntering = false;
    }

    public override void HitByPlayer(int player, int hp, int weapon)
    {
        if (this.lessHP <= 0)
            return;

        this.lessHP -= hp;
        if (this.lessHP > 0)
            return;

        StopAllTimer();

        this.lessHP = 0;
        this.unitCollider.enabled = false;
        PlayDieAction(player, weapon);
    }

    void PlayDieAction(int player, int weapon)
    {
        StopAllTimer();

        ShowDieEffect();
        if (!string.IsNullOrEmpty(this.Config.deathSound))
            SoundMgr.Instance.PlayOneShot(this.Config.deathSound);
        base.PlayVoice();
        this.attackTimeline?.SetActive(false);
        this.animator.Play(action_die, 0, 0);

        int score = PlayerInfo.GetRealKillScore(this.Config.score, GameApp.Instance.IsPlayerInTimeDouble(player));
        int health = this.Config.health;
        int isBossHitPoint = this.Config.unitType == EUnitType.unitBossHitPoint ? 1 : 0;
        EventMgr.Instance.Emit(GameLevel.MonsterKilledEvent, new[]
        {
            player,
            unitId,
            score,
            health,
            isBossHitPoint
        });

        UIEffectMgr.Instance.ShowKillScoreEffect(player, score, GetKillScorePos());

        // 掉落道具或触发大转盘
        if (!SmallGameMgr.Instance.CheckForBigWheel(player, unitId, this.tran.position + this.tran.up * 3) &&
            !SmallGameMgr.Instance.IsPlayerPlayingBigWheel(player) &&
            weapon == 0)
        {
            if (Random.Range(0, 100) < MachineDataMgr.Instance.GetDropItemProbability)
            {
                GameApp.Instance.CheckProp(player, unitId, GetPropPos());
            }
        }

        this.timerId = TimerMgr.Instance.ScheduleOnce((_) =>
        {
            EventMgr.Instance.Emit(UnitMgr.UnitPathOverEvent, new[]
            {
                this.spawnId,
                this.Config.unitId
            });
            if (this.isStayInAttackPoint)
            {
                this.isStayInAttackPoint = false;
                EventMgr.Instance.Emit(UnitMgr.UnitLeaveAttackPointEvent, this.attackPointIndex);
            }

            Recycle();
        }, this.Config.deathAniTime);
    }

    void ShowDieEffect()
    {
        if (string.IsNullOrEmpty(this.Config.deathEffect) || this.dieEffectTran == null)
            return;

        this.tran.LookAt(this.cameraTran);
        Vector3 pos = this.dieEffectTran.position;

        UnitEffectMgr.Instance.ShowDieEffect(this.Config.deathEffect, pos, this.tran.parent);
    }

    void StopAllTimer()
    {
        TimerMgr.Instance.UnSchedule(this.timerId);
        TimerMgr.Instance.UnSchedule(this.attackTimer);
        // this.attackTimeline?.SetActive(false);
        this.tran.DOKill();
    }

    bool IsPlayingAni(string aniName)
    {
        return this.animator.GetCurrentAnimatorStateInfo(0).IsName(aniName);
    }

    void GetPathPosListIndex()
    {
        this.pathPosIndex = Random.Range(0, this.pathConfig.pathPosList.Length);
    }
    void GetAttackPointIndex(bool isReal = true)
    {
        if (isReal)
            this.attackPointIndex =
                GameLevelMgr.Instance.GetRealAttackPoint(out this.attackPos, out this.attackPosPre,
                    this.pathConfig.pathPosList[this.pathPosIndex].attackIndexList);
        else
            this.attackPointIndex =
                GameLevelMgr.Instance.GetAttackPoint(out this.attackPos, out this.attackPosPre,
                    this.pathConfig.pathPosList[this.pathPosIndex].attackIndexList);
    }
    Vector3 RandomVector3(Vector3 min, Vector3 max)
    {
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
    Vector3 GetVector3(Vector3[] list, bool randomLerp = false)
    {
        if (list is not { Length: > 0 })
            return Vector3.zero;

        if (list.Length == 1)
            return list[0];

        if (randomLerp && list.Length == 2)
            return RandomVector3(list[0], list[1]);

        return list[Random.Range(0, list.Length)];
    }


    protected int GetAttack()
    {
        int attack = Config.attack;
        if (Random.Range(0, 100) < pathConfig.strikePercent){
            attack = attack*pathConfig.strikePercent/100;
        }

        return attack;
    }
}
