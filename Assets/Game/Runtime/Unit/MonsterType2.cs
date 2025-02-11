using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
//0,-3,35
public class MonsterType2 : MonsterBase
{
    protected int spawnId;
    protected MonsterType2PathConfig pathConfig;

    protected Animator animator;
    protected GameObject attackTimeline;
    protected int attackTimer = -1;
    protected Transform cameraTran;
    protected int attackCount;
    protected bool isStayInAttackPoint;
    protected int attackPointIndex;
    protected Vector3 attackPos;
    protected int pathPosIndex;
    protected List<Vector3> pathPosList;
    protected bool pathReverseFlag;
    
    Transform dieEffectTran;

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
        this.pathConfig = MonsterPathMgr.Instance.GetMonsterType2PathConfig(spawnId, unitId);
        this.pathPosIndex = Random.Range(0, this.pathConfig.pathPosList.Length);
        this.attackPointIndex =
            GameLevelMgr.Instance.GetAttackPoint(out this.attackPos, 
                this.pathConfig.pathPosList[this.pathPosIndex].attackIndex);
        this.pathReverseFlag = false;
        if (this.pathConfig.pathPosList[this.pathPosIndex].reverseFlag)
        {
            if(Random.Range(0, 2) == 0)
                this.pathReverseFlag = true;
        }
        
        this.unitId = unitId;
        this.spawnId = spawnId;

        Recycle();
        
        this.RecycleFlag = false;
        this.animator.enabled = true;
        if (this.pathReverseFlag)
        {
            int len = this.pathConfig.pathPosList[this.pathPosIndex].outPosList.Length;
            this.tran.localPosition = this.pathConfig.pathPosList[this.pathPosIndex].outPosList[len - 1];
        }
        else
        {
            this.tran.localPosition = this.pathConfig.pathPosList[this.pathPosIndex].inPosList[0];
        }
        this.tran.LookAt(this.cameraTran);
        this.obj.SetActive(true);
        
        if (this.pathConfig.pathPosList[this.pathPosIndex].isPlayEnterAnim)
        {
            this.animator.Play(action_enter, 0, 0);
            this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { EnterFinish(); }, this.Config.enterAniTime + pathConfig.pathPosList[this.pathPosIndex].enterAnimTimer);
        }
        else
        {
            EnterFinish();
        }
    }
    void StartMoveIn(MonsterType2PathConfig pathConfig)
    {
        this.pathConfig = pathConfig;
        this.pathPosIndex = Random.Range(0, this.pathConfig.pathPosList.Length);
        this.attackPointIndex =
            GameLevelMgr.Instance.GetAttackPoint(out this.attackPos, 
                this.pathConfig.pathPosList[this.pathPosIndex].attackIndex);
        this.pathReverseFlag = false;
        if (this.pathConfig.pathPosList[this.pathPosIndex].reverseFlag)
        {
            if(Random.Range(0, 2) == 0)
                this.pathReverseFlag = true;
        }
        
        this.unitId = this.pathConfig.unitId;
        this.spawnId = this.pathConfig.spawnId;

        Recycle();
        
        this.RecycleFlag = false;
        this.animator.enabled = true;
        if (this.pathReverseFlag)
        {
            int len = this.pathConfig.pathPosList[this.pathPosIndex].outPosList.Length;
            this.tran.localPosition = this.pathConfig.pathPosList[this.pathPosIndex].outPosList[len - 1];
        }
        else
        {
            this.tran.localPosition = this.pathConfig.pathPosList[this.pathPosIndex].inPosList[0];
        }
        this.tran.LookAt(this.cameraTran);
        this.obj.SetActive(true);
        
        if (this.pathConfig.pathPosList[this.pathPosIndex].isPlayEnterAnim)
        {
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
        StopAllTimer();
        this.lessHP = this.Config.health;
        this.unitCollider.enabled = true;
        this.animator.Play(action_run, 0, 0);
        float speed = GetSpeed(this.pathConfig.inSpeed);
        // if (UnitMgr.Instance.CanStayInAttackPoint(this.attackPointIndex))
        // {
        //     EventMgr.Instance.Emit(UnitMgr.UnitEnterAttackPointEvent, this.attackPointIndex);
        //     this.isStayInAttackPoint = true;
            
            this.pathPosList.Clear();
            if (this.pathReverseFlag)
            {
                this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].outPosList);
                this.pathPosList.Reverse();
            }
            else
            {
                this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].inPosList);
            }
            // this.pathPosList.Add(this.attackPos);
        // }
        // else
        // {
        //     this.pathPosList.Clear();
        //     this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].inPosList);
        //     if(this.pathConfig.pathPosList[this.pathPosIndex].isEnterAttackPos)
        //         this.pathPosList.Add(this.attackPos);
        //     this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].outPosList);
        //     if (this.pathReverseFlag)
        //     {
        //         this.pathPosList.Reverse();
        //     }
        // }
        
        DoPathBySpeed(this.pathPosList.ToArray(), speed, () =>
        {
            // �жϹ���λ���Ƿ���ã������ã���ռ�ù���λ�ý��빥��������ֱ�����˳�·��
            if (UnitMgr.Instance.CanStayInAttackPoint(this.attackPointIndex) && Random.Range(0, 100) < this.pathConfig.attackPercent)
            {
                EventMgr.Instance.Emit(UnitMgr.UnitEnterAttackPointEvent, this.attackPointIndex);
                this.isStayInAttackPoint = true;
                
                this.pathPosList.Clear();
                this.pathPosList.Add(this.tran.localPosition);
                this.pathPosList.Add(this.attackPos);
                this.pathPosList.Add(this.attackPos);
                DoPathBySpeed(this.pathPosList.ToArray(), speed, PlayAttack);
            }
            else
            {
                this.pathPosList.Clear();
                if (this.pathReverseFlag)
                {
                    this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].inPosList);
                    if(this.pathConfig.pathPosList[this.pathPosIndex].isEnterAttackPos)
                        this.pathPosList.Add(this.attackPos);
                    this.pathPosList.Add(this.tran.localPosition);
                    this.pathPosList.Reverse();
                }
                else
                {
                    this.pathPosList.Add(this.tran.localPosition);
                    if(this.pathConfig.pathPosList[this.pathPosIndex].isEnterAttackPos)
                        this.pathPosList.Add(this.attackPos);
                    this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].outPosList);
                }
                DoPathBySpeed(this.pathPosList.ToArray(), speed, ()=>
                {
                    StartMoveIn(this.pathConfig);
                });
            }
            // if (this.isStayInAttackPoint)
            // {
            //     PlayAttack();
            // }
            // else
            // {
            //     StartMoveIn(this.pathConfig);
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
            SoundMgr.Instance.PlayOneShot(this.Config.attackSound);
            this.animator.Play(action_attack, 0, 0);
            this.attackTimeline?.SetActive(false);
            this.attackTimeline?.SetActive(true);
            this.attackTimer = TimerMgr.Instance.ScheduleOnce((_) =>
            {
                EventMgr.Instance.Emit(CameraController.CameraShakeEvent2, null);
                EventMgr.Instance.Emit(UnitMgr.UnitAttackEvent, this.Config.attack);
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
        
        this.pathPosList.Clear();
        if (this.pathReverseFlag)
        {
            this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].inPosList);
            this.pathPosList.Add(this.tran.localPosition);
            this.pathPosList.Reverse();
        }
        else
        {
            this.pathPosList.Add(this.tran.localPosition);
            this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].outPosList);
        }
        
        if (this.isStayInAttackPoint)
        {
            this.isStayInAttackPoint = false;
            EventMgr.Instance.Emit(UnitMgr.UnitLeaveAttackPointEvent, this.attackPointIndex);
        }

        if (!IsPlayingAni(ACTION_RUN_NAME))
            this.animator.Play(action_run, 0, 0);

        DoPathBySpeed(this.pathPosList.ToArray(), GetSpeed(this.pathConfig.outSpeed), () => { StartMoveIn(this.pathConfig); });
    }

    public override void MoveOutQuickly()
    {
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
        
        this.pathPosList.Clear();
        if (this.pathReverseFlag)
        {
            this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].inPosList);
            this.pathPosList.Add(this.tran.localPosition);
            this.pathPosList.Reverse();
        }
        else
        {
            this.pathPosList.Add(this.tran.localPosition);
            this.pathPosList.AddRange(this.pathConfig.pathPosList[this.pathPosIndex].outPosList);
        }

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

        // ������߻򴥷���ת��
        if (!SmallGameMgr.Instance.CheckForBigWheel(player, unitId, this.tran.position + this.tran.up * 3) &&
            !SmallGameMgr.Instance.IsPlayerPlayingBigWheel(player) &&
            weapon == 0)
        {
            if (Random.Range(0, 100) < MachineDataMgr.Instance.GetDropItemProbability)
                GameApp.Instance.CheckProp(player, unitId, GetPropPos());
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

}
