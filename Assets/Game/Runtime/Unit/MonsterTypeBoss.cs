using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class MonsterTypeBoss : MonsterBase
{
    const float Play2SlowSpeed = 0.005f;
    /// <summary>
    /// 存储着 boss 攻击动画
    /// </summary>
    static readonly protected int[] boss_action_attacks = new[]
    {
        Animator.StringToHash("attack1"), // 攻击1（喷火）
        Animator.StringToHash("attack2"), // 攻击2（撞击屏幕）
        Animator.StringToHash("attack3"), // 攻击3（尾巴攻击）
    };
    static readonly protected int[] boss_action_hurt = new[]
    {
        Animator.StringToHash("hurt1"), // 攻击1（喷火）
        Animator.StringToHash("hurt2"), // 攻击2（撞击屏幕）
        Animator.StringToHash("hurt3"), // 攻击3（尾巴攻击）
    };
    // static readonly protected int boss_action_die = Animator.StringToHash("die"); // 死亡

    // const float boss_action_idle_time = 2f; // 待机
    // const float boss_action_spawn_time = 2.333f; // 产怪（休闲）
    //const float boss_action_enter_time = 4.5f; // 展示（飞行盘旋）
    //static readonly float[] boss_action_attack_hp_time = new[]
    //{
    //    2f, // 攻击1（喷火）
    //    1.3f, // 攻击2（撞击屏幕）
    //    2.15f, // 攻击3（尾巴攻击）
    //};
    static readonly float[] boss_action_attack_pause_time = new[]
    {
        0.2f, // 攻击1（喷火）
        0.2f, // 攻击2（撞击屏幕）
        0.2f, // 攻击3（尾巴攻击）
    };
    // static readonly float[] boss_action_attack_time = new[]
    // {
    //     2.767f, // 攻击1（喷火）
    //     3.667f, // 攻击2（撞击屏幕）
    //     3.333f, // 攻击3（尾巴攻击）
    // };
    //static readonly float[] boss_action_attack_timeline_time = new[]
    //{
    //    3.716667f, // 攻击1（喷火）
    //    4.45f, // 攻击2（撞击屏幕）
    //    3.366667f, // 攻击3（尾巴攻击）
    //};
    const float boss_action_hurt_time = 1f; // 受击
    // const float boss_action_die_time = 2.667f; // 死亡

    enum EBossState
    {
        none = 0,
        enter = 1, // 进场
        show = 2, // 表演
        playState1 = 3, // 玩法阶段1（击杀小怪）
        playState2 = 4, // 玩法阶段2（消灭击打点）
        die = 5, // 死亡
    }

    Animator animator;
    int attackTimer = -1;
    Transform cameraTran;
    Transform dieEffectTran;

    MonsterTypeBossConfig unitBossConfig;
    EBossState bossState;
    MonsterTypeBossConfig.MonsterTypeBossPlay2Config play2Config;

    int killNumTotal;
    Dictionary<int, int> killNumSingleUnits;

    bool gameIsOver;

    // 最后一个点的击杀玩家
    int bossEndKillPlayer;
    int play2AttackIndex;
    /// <summary>
    /// 进场动画
    /// </summary>
    PlayableDirector enterTimeline;
    /// <summary>
    /// 攻击手段
    /// </summary>
    PlayableDirector[] attackTimelines;


    bool canHitting;

    float playState2StartTime;

    public override void Init(GameObject obj, int unitId)
    {
        base.Init(obj, unitId);
        this.animator = this.tran.GetChild(0).GetComponent<Animator>();
        this.cameraTran = CameraController.Instance.MainCameraRootTran;
        this.dieEffectTran = this.tran.Find("dieEffectSlot");

        this.enterTimeline = this.tran.Find("enterTimeline").GetComponent<PlayableDirector>();
        this.attackTimelines = new PlayableDirector[3];
        for (var i = 0; i < this.attackTimelines.Length; i++)
        {
            this.attackTimelines[i] = this.tran.Find($"attack{i + 1}Timeline").GetComponent<PlayableDirector>();
        }
        HideAllTimelineObjs();

        Recycle();
    }

    /// <summary>
    /// 隐藏所有 特效时间片段 
    /// </summary>
    void HideAllTimelineObjs()
    {
        this.enterTimeline.Stop();
        this.enterTimeline.gameObject.SetActive(false);
        for (var i = 0; i < this.attackTimelines.Length; i++)
        {
            this.attackTimelines[i].Stop();
            this.attackTimelines[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 播放特效片段
    /// </summary>
    /// <param name="timeline"></param>
    void PlayTimeline(PlayableDirector timeline)
    {
        HideAllTimelineObjs();
        timeline.Play();
        timeline.gameObject.SetActive(true);
    }

    public void StartMoveIn(int spawnId, int unitId)
    {
        this.canHitting = false;
        this.unitId = unitId;
        Recycle();
        this.RecycleFlag = false;
        this.unitBossConfig = MonsterPathMgr.Instance.GetMonsterTypeBossConfig(spawnId, unitId);
        this.animator.enabled = true;
        this.animator.speed = 1f;

        this.lessHP = this.Config.health;
        this.killNumTotal = 0;
        this.gameIsOver = false;

        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false);
        UnitSpawnMgr.Instance.RecycleAllSpawn();
        UnitMgr.Instance.RecycleAllButBossUnits();
        StartEnter();

        EventMgr.Instance.AddListener(GameLevel.MonsterKilledEvent, MonsterKillEventListen);
        EventMgr.Instance.AddListener(GameLevel.BossHitPointDeductHPEvent, BossHitPointDeductHPEventListen);
        EventMgr.Instance.AddListener(GameLevel.FinishedEvent, GameOverEventListen);
        EventMgr.Instance.AddListener(GameLevel.FailedEvent, GameOverEventListen);
    }

    void BossHitPointDeductHPEventListen(string eventName, object udata)
    {
        if (RecycledContinue())
            return;

        if (!this.canHitting)
            return;

        if (this.gameIsOver)
            return;

        if (this.lessHP <= 0)
            return;

        var data = (int[])udata;
        int player = data[0];
        int hp = data[1];
        this.lessHP -= hp;
        EventMgr.Instance.Emit(CountDown_UICtrl.UpdateBossHPEvent, new[]
        {
            this.lessHP,
            this.Config.health,
        });

        if (this.lessHP <= 0)
        {
            EventMgr.Instance.Emit(CountDown_UICtrl.UpdateBossHPEvent, new[]
            {
                0,
                this.Config.health,
            });

            EventMgr.Instance.Emit(CountDown_UICtrl.PauseCountDownEvent, null);
            EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false);
            UnitSpawnMgr.Instance.RecycleAllSpawn();
            UnitMgr.Instance.RecycleAllButBossUnits();
            PlayState2End(player);
        }
        else
        {
            EventMgr.Instance.Emit(CountDown_UICtrl.UpdateBossHPEvent, new[]
            {
                this.lessHP,
                this.Config.health,
            });
        }
    }
    void GameOverEventListen(string eventName, object udata)
    {
        this.gameIsOver = true;
    }
    void MonsterKillEventListen(string eventName, object udata)
    {
        if (RecycledContinue())
            return;

        if (!this.canHitting)
            return;

        if (this.gameIsOver)
            return;

        if (udata == null)
            return;

        var datas = (int[])udata;
        int player = datas[0];
        int killUnitId = datas[1];
        // int score = datas[2];
        // int health = datas[3];
        // int isBossHitPoint = datas[4];

        if (killUnitId == this.unitId)
            return;

        // if (isBossHitPoint == 1)
        // {
        //     this.killHpTotal += health;
        // }

        this.killNumSingleUnits ??= new Dictionary<int, int>();
        this.killNumSingleUnits.TryAdd(killUnitId, 0);
        this.killNumSingleUnits[killUnitId]++;
        this.killNumTotal++;

        if (PlayState1CheckFinished())
        {
            EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false);
            UnitSpawnMgr.Instance.RecycleAllSpawn();
            UnitMgr.Instance.RecycleAllButBossUnits();
            PlayState1End();
        }
        if (PlayState2CheckFinished())
        {
            EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false);
            UnitSpawnMgr.Instance.RecycleAllSpawn();
            UnitMgr.Instance.RecycleAllButBossUnits();
            PlayState2End(player);
        }
    }
    public override void MoveOutQuickly()
    {
        this.bossState = EBossState.none;
        this.animator.enabled = false;
        this.canHitting = false;
        HideAllTimelineObjs();
        UnScheduleAllTimer();
        this.animator.Play(action_idle, 0, 0);
        this.tran
            .DOLocalMove(this.unitBossConfig.enterStartPos, this.unitBossConfig.enterTotalTime)
            // .SetEase(Ease.InBack)
            .OnComplete(Recycle);
    }
    public override void Recycle()
    {
        base.Recycle();

        UnScheduleAllTimer();
        EventMgr.Instance.RemoveListener(GameLevel.BossHitPointDeductHPEvent, BossHitPointDeductHPEventListen);
        EventMgr.Instance.RemoveListener(GameLevel.FinishedEvent, GameOverEventListen);
        EventMgr.Instance.RemoveListener(GameLevel.FailedEvent, GameOverEventListen);
        EventMgr.Instance.RemoveListener(GameLevel.MonsterKilledEvent, MonsterKillEventListen);
        this.bossState = EBossState.none;
        this.animator.enabled = false;
        this.canHitting = false;
        HideAllTimelineObjs();
    }

    void UnScheduleAllTimer()
    {
        this.tran.DOKill();
        TimerMgr.Instance.UnSchedule(this.timerId);
        TimerMgr.Instance.UnSchedule(this.attackTimer);
    }
    /// <summary>
    /// 播放动画控制器 指定动画
    /// </summary>
    /// <param name="action"></param>
    void PlayAniAction(int action)
    {
        this.animator.Play(action, 0, 0);
    }
    /// <summary>
    /// 开始进入 ---> boss 进场开始
    /// </summary>
    void StartEnter()
    {
        if (RecycledContinue())
            return;

        if (this.bossState != EBossState.none)
            return;
        this.bossState = EBossState.enter;
        PlayAniAction(action_idle);
        this.tran.DOKill();
        this.tran.localEulerAngles = this.unitBossConfig.angle;
        this.tran.localPosition = this.unitBossConfig.enterStartPos;
        this.tran
            .DOLocalMove(this.unitBossConfig.enterEndPos, this.unitBossConfig.enterTotalTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() => { if (unitBossConfig.ApproachHide) { this.obj.SetActive(true); } StartShow(); });
        if (!unitBossConfig.ApproachHide) { this.obj.SetActive(true); EventMgr.Instance.Emit(CameraController.CameraShakeEvent, null); }

    }
    /// <summary>
    /// 开始显示 boss 飞来飞去
    /// </summary>
    void StartShow()
    {
        if (RecycledContinue())
            return;

        if (this.bossState != EBossState.enter)
            return;

        UnScheduleAllTimer();
        this.bossState = EBossState.show;
        SoundMgr.Instance.PlayBossSound($@"Sounds\boss\{unitBossConfig.unitId}\boss_enter"); //boss进场音效
        PlayAniAction(action_enter);
        PlayTimeline(this.enterTimeline); //进场时间特效片段
        if (unitBossConfig.ApproachHide) { EventMgr.Instance.Emit(CameraController.CameraShakeEvent, null); }
        this.timerId = TimerMgr.Instance.ScheduleOnce((_) =>
        {
            EventMgr.Instance.Emit(CountDown_UICtrl.StartCountDownEvent, null);
            StartPlay();
        }, unitBossConfig.boss_action_enter_time);
    }

    void StartPlay()
    {
        if (RecycledContinue())
            return;

        StartPlayState1();
    }
    /// <summary>
    /// 开始播放状态1 ---> 休闲产怪
    /// </summary>
    void StartPlayState1()
    {
        if (RecycledContinue())
            return;

        UnScheduleAllTimer();

        //SoundMgr.Instance.PlayMusic(@"Sounds\bg\bg_scene1_level4_1");
        SoundMgr.Instance.PlayMusic($@"Sounds\bg\scene{GameSceneMgr.Instance.CurScene}\bg_scene{GameSceneMgr.Instance.CurScene}_bossbgm1");
        this.bossState = EBossState.playState1;
        this.killNumTotal = 0;
        this.killNumSingleUnits ??= new Dictionary<int, int>(8);
        this.killNumSingleUnits.Clear();

        this.cameraTran.DOKill();
        this.cameraTran.DOMove(this.unitBossConfig.play1CameraPos, 0.1f);
        this.cameraTran.DORotate(this.unitBossConfig.play1CameraRot, 0.1f);
        PlayAniAction(action_spawn);
        this.timerId = TimerMgr.Instance.ScheduleOnce((_) => //延时调用
        {
            if (this.gameIsOver)
                return;

            EventMgr.Instance.Emit(CameraController.CameraShakeEvent, this.unitBossConfig.play1CameraPos);
            EventMgr.Instance.Emit(UnitSpawnMgr.SetLimitCount,
                MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene,
                    MachineDataMgr.MaxLevelCount));
            // EventMgr.Instance.Emit(UnitSpawnMgr.SetLimitCount, this.unitBossConfig.play1TargetKillNum);
            int totalGroup = this.unitBossConfig.play1SpawnIds.Count / 4;
            int randSpawn = Random.Range(0, totalGroup);
            var spawnIds = new int[4];
            Array.Copy(this.unitBossConfig.play1SpawnIds.ToArray(), randSpawn * 4, spawnIds, 0, 4);
            EventMgr.Instance.Emit(UnitSpawnMgr.StartEvent, spawnIds);
            EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, true);
            this.canHitting = true;
            PlayState1CountDown(); //状态1倒计时
        }, this.unitBossConfig.play1SpawnTime);
    }
    /// <summary>
    /// 状态1倒计时 ---> 结束进入其他状态
    /// </summary>
    void PlayState1CountDown()
    {
        if (RecycledContinue())
            return;

        if (PlayState1CheckFinished())
        {
            PlayState1End();
            return;
        }

        UnScheduleAllTimer();

        float nextAttackTime =
            Random.Range(this.unitBossConfig.play1AttackMinTime, this.unitBossConfig.play1AttackMaxTime);
        this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { BossPlayState1Attack(); },
            nextAttackTime);
    }
    /// <summary>
    /// 检查状态1是否完成 ---> 击杀的数量足够，状态完成
    /// </summary>
    /// <returns></returns>
    bool PlayState1CheckFinished()
    {
        if (RecycledContinue())
            return false;

        if (this.bossState != EBossState.playState1)
            return false;

        return this.killNumTotal >=
               MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene,
                   MachineDataMgr.MaxLevelCount);
    }
    /// <summary>
    /// 状态1结束
    /// </summary>
    void PlayState1End()
    {
        this.canHitting = false;

        if (RecycledContinue())
            return;

        if (this.bossState != EBossState.playState1)
            return;

        UnScheduleAllTimer();
        this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { StartPlayState2(); }, 1f); //开始状态2 ---> 击打点
    }
    /// <summary>
    /// boss状态1 时的攻击
    /// </summary>
    void BossPlayState1Attack()
    {
        UnScheduleAllTimer();

        int randAttackIndex = Random.Range(0, boss_action_attacks.Length);
        //SoundMgr.Instance.PlayBossSound($@"Sounds\5_boss_attack{randAttackIndex + 1}"); //boss攻击音效
        SoundMgr.Instance.PlayBossSound($@"Sounds\boss\{unitBossConfig.unitId}\boss_attack{randAttackIndex + 1}"); //boss攻击音效
        PlayAniAction(boss_action_attacks[randAttackIndex]);
        PlayTimeline(this.attackTimelines[randAttackIndex]);
        this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { PlayState1CountDown(); },
            unitBossConfig.boss_action_attack_timeline_time[randAttackIndex]);

        this.attackTimer = TimerMgr.Instance.ScheduleOnce((_) =>
        {
            EventMgr.Instance.Emit(CameraController.CameraShakeEvent, this.unitBossConfig.play1CameraPos);
            EventMgr.Instance.Emit(UnitMgr.UnitAttackEvent, this.Config.attack);
        },
            unitBossConfig.boss_action_attack_hp_time[randAttackIndex]);
    }
    /// <summary>
    /// 开始进入boss状态2 ---> 击打玩法
    /// </summary>
    void StartPlayState2()
    {
        if (RecycledContinue())
            return;

        //SoundMgr.Instance.PlayMusic(@"Sounds\bg\bg_scene1_level4_2");
        SoundMgr.Instance.PlayMusic(@"Sounds\boss\bossman");
        this.bossState = EBossState.playState2;
        this.playState2StartTime = Time.time;

        UnScheduleAllTimer();

        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false);
        UnitSpawnMgr.Instance.RecycleAllSpawn();
        UnitMgr.Instance.RecycleAllButBossUnits();

        this.play2AttackIndex = Random.Range(0, boss_action_attacks.Length);
        this.play2Config = this.unitBossConfig.play2Configs[this.play2AttackIndex];
        //SoundMgr.Instance.PlayBossSound($@"Sounds\5_boss_attack{play2AttackIndex + 1}"); //boss攻击音效
        SoundMgr.Instance.PlayBossSound($@"Sounds\boss\{unitBossConfig.unitId}\boss_attack{play2AttackIndex + 1}");
        PlayAniAction(boss_action_attacks[this.play2AttackIndex]);
        PlayTimeline(attackTimelines[this.play2AttackIndex]);
        this.animator.speed = 1;
        attackTimelines[this.play2AttackIndex].playableGraph.GetRootPlayable(0).SetSpeed(1);


        this.killNumTotal = 0;
        this.killNumSingleUnits ??= new Dictionary<int, int>(8);
        this.killNumSingleUnits.Clear();

        float cameraMoveTime = this.play2Config.cameraZoomInTime;
        this.cameraTran.DOKill();
        this.cameraTran.DOMove(this.play2Config.cameraPos, cameraMoveTime);
        this.cameraTran.DORotate(this.play2Config.cameraRot, cameraMoveTime);

        this.attackTimer = TimerMgr.Instance.ScheduleOnce((_) =>
        {
            SoundMgr.Instance.SetBossSoundSpeed(Play2SlowSpeed);
            this.animator.speed = Play2SlowSpeed;
            attackTimelines[this.play2AttackIndex].playableGraph.GetRootPlayable(0).SetSpeed(Play2SlowSpeed);
        }, this.play2Config.actionSlowDelayTime);

        this.timerId = TimerMgr.Instance.ScheduleOnce((_) =>
        {
            PlayState2CountDown();
        }, this.play2Config.hitPointAppearDelayTime);
    }
    void PlayState2CountDown()
    {
        if (RecycledContinue())
            return;

        // UnScheduleAllTimer();

        for (var i = 0; i < this.play2Config.hitPointPosList.Count; i++)
        {
            UnitMgr.Instance.CreateUnitBossHitPoint(this.play2Config.hitPointUnitId,
                this.play2Config.hitPointPosList[i],
                this.play2Config.countTime);
        }

        TimerMgr.Instance.UnSchedule(this.timerId);
        this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { Play2Attack(); }, this.play2Config.countTime);

        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, true);
        this.canHitting = true;
    }
    bool PlayState2CheckFinished()
    {
        if (RecycledContinue())
            return false;

        if (this.bossState != EBossState.playState2)
            return false;

        if (this.lessHP <= 0)
        {
            return true;
        }

        int targetKillUnitId = this.play2Config.hitPointUnitId;
        int targetKillNum = this.play2Config.hitPointPosList.Count;

        if (targetKillUnitId == -1)
        {
            if (this.killNumTotal < targetKillNum)
                return false;
        }
        else
        {
            if (!this.killNumSingleUnits.TryGetValue(targetKillUnitId, out int curKillNum))
                return false;

            if (curKillNum < targetKillNum)
                return false;
        }

        return true;
    }
    void PlayState2End(int endKillPlayer = 0)
    {
        this.canHitting = false;

        if (this.bossState != EBossState.playState2)
            return;

        if (RecycledContinue())
            return;

        UnScheduleAllTimer();

        this.cameraTran.DOKill();
        this.cameraTran.DOMove(this.unitBossConfig.play1CameraPos,
            this.play2Config.cameraZoomOutTime);
        this.cameraTran.DORotate(this.unitBossConfig.play1CameraRot,
                this.play2Config.cameraZoomOutTime)
            .OnComplete(
                () =>
                {
                    this.animator.enabled = true;
                    this.animator.speed = 1f;
                    this.attackTimelines[this.play2AttackIndex].playableGraph.GetRootPlayable(0).SetSpeed(1f);
                    HideAllTimelineObjs();
                    SoundMgr.Instance.StopBossSound();

                    this.bossEndKillPlayer = endKillPlayer;
                    if (PlayState2CheckFinished())
                    {
                        Play2Hurted();
                    }
                });
    }

    void Play2Attack()
    {
        this.canHitting = false;

        if (RecycledContinue())
            return;

        UnScheduleAllTimer();

        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false);
        UnitSpawnMgr.Instance.RecycleAllSpawn();
        UnitMgr.Instance.RecycleAllButBossUnits();

        this.cameraTran.DOKill();
        this.cameraTran.DOMove(this.unitBossConfig.play1CameraPos,
            this.play2Config.cameraZoomOutTime);
        this.cameraTran.DORotate(this.unitBossConfig.play1CameraRot,
                this.play2Config.cameraZoomOutTime)
            .OnComplete(
                () =>
                {
                    this.animator.enabled = true;
                    this.animator.speed = 1f;
                    this.attackTimelines[this.play2AttackIndex].playableGraph.GetRootPlayable(0).SetSpeed(1f);
                    SoundMgr.Instance.SetBossSoundSpeed(1f);

                    // float aniPassTime = (Time.time - this.playState2StartTime) * Play2SlowSpeed;
                    float aniPassTime = this.play2Config.actionSlowDelayTime + (Time.time - this.playState2StartTime - this.play2Config.actionSlowDelayTime) * Play2SlowSpeed;

                    this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { StartPlayState2(); },
                        unitBossConfig.boss_action_attack_timeline_time[this.play2AttackIndex] - aniPassTime);

                    this.attackTimer = TimerMgr.Instance.ScheduleOnce((_) =>
                    {
                        if (GameApp.Instance.IsAnyPlayerCanPlay())
                        {
                            EventMgr.Instance.Emit(CameraController.CameraShakeEvent,
                                this.unitBossConfig.play1CameraPos);
                            EventMgr.Instance.Emit(UnitMgr.UnitAttackEvent, this.Config.attack);
                        }
                    }, unitBossConfig.boss_action_attack_hp_time[this.play2AttackIndex] - aniPassTime);
                });
    }
    void Play2Hurted()
    {
        if (RecycledContinue())
            return;

        UnScheduleAllTimer();

        if (this.lessHP <= 0)
        {
            EventMgr.Instance.RemoveListener(GameLevel.MonsterKilledEvent, MonsterKillEventListen);
            this.lessHP = 0;
            this.bossState = EBossState.die;
            this.PlayDieAction(this.bossEndKillPlayer);
        }
        else
        {
            PlayAniAction(boss_action_hurt[play2AttackIndex]);
            this.timerId = TimerMgr.Instance.ScheduleOnce((_) => { StartPlay(); }, boss_action_hurt_time);
        }
    }

    bool RecycledContinue()
    {
        if (this.gameIsOver)
            return true;

        if (this.RecycleFlag)
        {
            Recycle();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayDieAction(int player)
    {
        UnScheduleAllTimer();

        this.gameIsOver = true;

        if (string.IsNullOrEmpty(this.Config.deathEffect))
            UnitEffectMgr.Instance.ShowDieEffect(this.Config.deathEffect, this.dieEffectTran.position,
                this.tran.parent);
        if (!string.IsNullOrEmpty(this.Config.deathSound))
            SoundMgr.Instance.PlayBossSound(this.Config.deathSound);
        base.PlayVoice();

        this.animator.Play(action_die, 0, 0);

        int score = PlayerInfo.GetRealKillScore(this.Config.score, GameApp.Instance.IsPlayerInTimeDouble(player));
        int health = this.Config.health;
        int isBossHitPoint = this.Config.unitType == EUnitType.unitBossHitPoint ? 1 : 0;

        this.timerId = TimerMgr.Instance.ScheduleOnce((_) =>
        {
            Recycle();

            EventMgr.Instance.Emit(GameLevel.MonsterKilledEvent, new[]
            {
                player,
                this.unitId,
                score,
                health,
                isBossHitPoint
            });
        }, this.Config.deathAniTime);
        UIEffectMgr.Instance.ShowKillScoreEffect(player, score, GetKillScorePos());
    }

}
