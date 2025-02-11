using UnityEngine;

public class GameLevel
{
    /// <summary>
    /// 关卡开始事件
    /// </summary>
    public const string GameLevelStartEvent = "GameLevel_StartEvent";
    /// <summary>
    /// 关卡完成事件
    /// </summary>
    public const string FinishedEvent = "GameLevel_FinishedEvent";
    /// <summary>
    /// 关卡失败事件
    /// </summary>
    public const string FailedEvent = "GameLevel_FailedEvent";
    /// <summary>
    /// 怪物被击杀事件
    /// </summary>
    public const string MonsterKilledEvent = "GameLevel_MonsterKilledEvent";

    /// <summary>
    /// boss击杀点扣血事件
    /// </summary>
    public const string BossHitPointDeductHPEvent = "GameLevel_BossHitPointDeductHPEvent";


    // const int MAX_VOICE_NUM = 4;
    // static readonly string[,] VOICE_PATHS = new[,] //存储提示音
    // {
    //     {
    //         "Sounds/voice_0",
    //         "Sounds/voice_0_EN"
    //     },
    //     {
    //         "Sounds/voice_1",
    //         "Sounds/voice_1_EN"
    //     },
    //     {
    //         "Sounds/voice_2",
    //         "Sounds/voice_2_EN"
    //     },
    //     {
    //         "Sounds/voice_3",
    //         "Sounds/voice_3_EN"
    //     }
    // };

    /// <summary>
    /// 游戏关卡配置文件
    /// </summary>
    public GameLevelConfigData.GameLevelConfig config;
    /// <summary>
    /// 是否结束
    /// </summary>
    public bool IsOver { get; private set; } = true;
    /// <summary>
    /// 是否正在等待继续
    /// </summary>
    public bool IsWaitingForContinue { get; private set; }
    /// <summary>
    /// 击杀数量
    /// </summary>
    public int KilledNum { get; private set; }
    /// <summary>
    /// 目标击杀数量
    /// </summary>
    int targetKilledNum;
    /// <summary>
    /// 分数
    /// </summary>
    public int GettedScore { get; private set; }
    /// <summary>
    /// 目标分数
    /// </summary>
    int targetGettedScore;
    /// <summary>
    /// 最大怪物数量
    /// </summary>
    public int MaxUnitNum { get; private set; }

    // int voiceTimerId;

    // public GameLevel(GameLevelConfigData.GameLevelConfig config)
    // {
    //     this.config = config;
    //     this.IsOver = true;
    // }
    
    int cameraAniMoveDelayTimerId;
    int cameraAniMoveTimerId;
    int cameraAniMoveIndex;
    void StartCameraAniMove()
    {
        StopCameraAniMove();

        if (cameraAniMoveIndex >= this.config.cameraMoveConfigs.Length)
            return;
        
        AnimationClip clip = this.config.cameraMoveConfigs[cameraAniMoveIndex].animClip;
        float delayTime = this.config.cameraMoveConfigs[cameraAniMoveIndex].delayTime;
        cameraAniMoveIndex += 1;

        if (cameraAniMoveIndex >= this.config.cameraMoveConfigs.Length)
        {
            cameraAniMoveTimerId = TimerMgr.Instance.ScheduleOnce(o =>
            {
                StartCameraAniMove();
            }, delayTime + clip.length);  
        }
        
        if (delayTime <= 0)
        {
            CameraController.Instance.PlayCameraRootAnim(clip);
        }
        else
        {
            cameraAniMoveDelayTimerId = TimerMgr.Instance.ScheduleOnce(o =>
            {
                CameraController.Instance.PlayCameraRootAnim(clip);
            }, delayTime);  
        }
    }
    void StopCameraAniMove()
    {
        if(cameraAniMoveDelayTimerId != -1)
        {
            TimerMgr.Instance.UnSchedule(cameraAniMoveDelayTimerId);
            cameraAniMoveDelayTimerId = -1;
        }
        if(cameraAniMoveTimerId != -1)
        {
            TimerMgr.Instance.UnSchedule(cameraAniMoveTimerId);
            cameraAniMoveTimerId = -1;
        }
        CameraController.Instance.RemoveAllCameraRootAnimClips();
    }

    public void LevelStart(GameLevelConfigData.GameLevelConfig config)
    {
        StopCameraAniMove();
        
        this.config = config;

        UnitMgr.Instance.UpdateUnitRoot(this.config.cameraPos, this.config.cameraAngle);

        if (!this.config.isBossLevel)
            SoundMgr.Instance.PlayMusic(this.config.bgmPath);

        int targetNum =
            MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene, this.config.level); //获取关卡击杀怪物数量
        int targetScore =
            MachineDataMgr.Instance.GetLevelTargetScore(GameSceneMgr.Instance.CurScene, this.config.level); //获取关卡目标分数
        this.MaxUnitNum = Random.Range(this.config.maxUnitNum, this.config.maxUnitNum2);
        this.IsOver = false;
        this.IsWaitingForContinue = false;
        this.KilledNum = 0;
        this.GettedScore = 0;
        if (this.config.isBossLevel)
        {
            this.targetKilledNum = 1;
            this.targetGettedScore = int.MaxValue;
        }
        else
        {
            this.targetKilledNum = targetNum > 0 ? targetNum : int.MaxValue;
            this.targetGettedScore = targetScore > 0 ? targetScore : int.MaxValue;
        }

        int[] data = new[]
        {
            this.config.level,//对应关卡
            MachineDataMgr.Instance.GetLevelTime(GameSceneMgr.Instance.CurScene, this.config.level), //关卡时间
            MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene, this.config.level), //关卡怪物目标数量
            MachineDataMgr.Instance.GetLevelTargetScore(GameSceneMgr.Instance.CurScene, this.config.level), //关卡目标分数
            this.config.isBossLevel ? 1 : 0, //是否为boss
            this.config.scene
        };

        EventMgr.Instance.Emit(GameLevel.GameLevelStartEvent, data); //触发关卡开始
        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false); //触发是否可以射击

        if (this.config.cameraMoveAnim != null)
        {
            if (this.config.isAdvanceSpawn)
            {
                EventMgr.Instance.Emit(UnitSpawnMgr.SetLimitCount, //触发设置产怪数量
                MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene, this.config.level) + 10); //获取数量
                EventMgr.Instance.Emit(UnitSpawnMgr.StartEvent, this.config.spawnIds); //触发开始产怪
            }

            StopCameraAniMove();
            
            var clip = this.config.cameraMoveAnim;
            cameraAniMoveTimerId = TimerMgr.Instance.ScheduleOnce(o =>
            {
                EventMgr.Instance.Emit(CameraController.CameraMoveFinishedEvent, null);
            }, clip.length);  
            
            CameraController.Instance.PlayCameraRootAnim(clip);
        }
        else
        {
            if (this.config.isAdvanceSpawn)
            {
                EventMgr.Instance.Emit(UnitSpawnMgr.SetLimitCount, //触发设置产怪数量
                MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene, this.config.level) + 10); //获取数量
                EventMgr.Instance.Emit(UnitSpawnMgr.StartEvent, this.config.spawnIds); //触发开始产怪
            }

            EventMgr.Instance.Emit(CameraController.CameraMoveEvent, new[] //触发相机移动
            {
                this.config.cameraPos,
                this.config.cameraAngle
            });
        }
    }
    /// <summary>
    /// 显示条件
    /// </summary>
    public void ShowPassCondition()
    {
        if (this.IsOver)
            return;


        if (!this.config.isAdvanceSpawn)
        {
            EventMgr.Instance.Emit(UnitSpawnMgr.SetLimitCount, //触发设置产怪数量
            MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene, this.config.level) + 10); //获取数量
            EventMgr.Instance.Emit(UnitSpawnMgr.StartEvent, this.config.spawnIds); //触发开始产怪
        }
        

        StopCameraAniMove();
        cameraAniMoveIndex = 0;
        if (this.config.cameraMoveConfigs is { Length: > 0 })
        {
            StartCameraAniMove();
        }

        if (this.config.isShowPassConditionText)
        {

            string conditionStr = GameLevelMgr.Instance.LevelConditionStr(this.config.level,this.config.isBossLevel,
                MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene, this.config.level),
                MachineDataMgr.Instance.GetLevelTargetScore(GameSceneMgr.Instance.CurScene, this.config.level));
            EventMgr.Instance.Emit(LevelConditionTips_UICtrl.ShowTipsEvent, conditionStr); //触发显示条件 ---> ui显示
        }
        else
        {
            ShowPassConditionFinished();
        }
    }
    /// <summary>
    /// 显示条件完成
    /// </summary>
    public void ShowPassConditionFinished()
    {
        if (this.IsOver)
            return;

        if (!this.config.isBossLevel) //非boss
        {
            EventMgr.Instance.Emit(CountDown_UICtrl.StartCountDownEvent, null); //触发游戏开始倒计时
            EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, true); //触发玩家可射击
        }
    }
    /// <summary>
    /// 检测等待继续
    /// </summary>
    public void WaitForContinue()
    {
        if (this.IsOver || this.IsWaitingForContinue) //over 或 再等待
            return;

        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false);
        EventMgr.Instance.Emit(LevelContinueTips_UICtrl.ShowTipsEvent, GameSceneMgr.Instance.CurSceneContinueTime()); //触发 显示是否继续

        this.IsWaitingForContinue = true;
    }
    /// <summary>
    /// 继续关卡
    /// </summary>
    public void ContinueLevel()
    {
        if (this.IsOver)
            return;

        this.IsWaitingForContinue = false;
        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, true); 
        EventMgr.Instance.Emit(LevelContinueTips_UICtrl.StopTipsEvent, null); //触发 停止是否继续提示
        EventMgr.Instance.Emit(CountDown_UICtrl.StartCountDownEvent, null); //触发游戏开始倒计时
    }
    /// <summary>
    /// 击杀怪物
    /// </summary>
    /// <param name="player">玩家</param>
    /// <param name="unitId">怪物id</param>
    /// <param name="score">对应分数</param>
    /// <param name="health">血量</param>
    public void KillMonster(int player, int unitId, int score, int health)
    {
        if (this.config.unitId == -1 || this.config.unitId == unitId)
        {
            this.KilledNum++;
        }

        this.GettedScore += score;

        EventMgr.Instance.Emit(CountDown_UICtrl.UpdateKillNumAndScoreEvent, new[] // 更新击杀数量以及累计积分事件
        {
            this.KilledNum, //击杀数量
            this.GettedScore, //分数
            MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene, this.config.level), //目标数量
            MachineDataMgr.Instance.GetLevelTargetScore(GameSceneMgr.Instance.CurScene, this.config.level), //目标分数
        });
        //符合条件
        if (this.KilledNum >= this.targetKilledNum || this.GettedScore >= this.targetGettedScore)
        {
            LevelFinished(); //关卡完成
        }
    }

    bool isWin;
    //关卡完成 
    public void LevelFinished()
    {
        this.isWin = true;
        if (this.config.level >= GameLevelMgr.Instance.LevelNum)
        {
            GameApp.Instance.canStartFlag = false;
        }

        EventMgr.Instance.Emit(GameLevel.FinishedEvent, this.config.level); //触发 关卡完成事件

        if (this.config.isShowResultText)
        {
            EventMgr.Instance.Emit(LevelConditionTips_UICtrl.ShowLevelEndTipsEvent, //触发 显示关卡结算提示
                MachineDataMgr.Instance.IsChineseLanguageVersion ? this.config.winStr : this.config.winStr_EN);
        }
        else
        {
            TimerMgr.Instance.ScheduleOnce(o =>
            {
                ShowLevelEndTipsFinished();
            }, 0.1f);  
        }
    }
    //关卡失败
    public void LevelFailed()
    {
        GameApp.Instance.canStartFlag = false;
        
        this.isWin = false;

        EventMgr.Instance.Emit(GameLevel.FailedEvent, this.config.level); //触发 关卡失败事件

        if (this.config.isShowResultText)
        {
            EventMgr.Instance.Emit(LevelConditionTips_UICtrl.ShowLevelEndTipsEvent, //触发 显示关卡结算提示
                MachineDataMgr.Instance.IsChineseLanguageVersion ? this.config.failStr : this.config.failStr_EN);
        }
        else
        {
            TimerMgr.Instance.ScheduleOnce(o =>
            {
                ShowLevelEndTipsFinished();
            }, 0.1f);  
        }

    }
    /// <summary>
    /// 显示关卡结束提示已完成 ---> 要么失败、要么完成、要么进行下关
    /// </summary>
    public void ShowLevelEndTipsFinished()
    {
        StopCameraAniMove();
        
        if (this.isWin)
        {
            if (this.config.level >= GameLevelMgr.Instance.LevelNum)
            {
                GameLevelMgr.Instance.GameOver(true);
            }
            else
            {
                GameLevelMgr.Instance.LevelStart(this.config.level + 1);
            }
        }
        else
        {
            GameLevelMgr.Instance.GameOver(false);
        }
    }
}
