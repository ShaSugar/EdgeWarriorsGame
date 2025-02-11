using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLevelMgr : UnitySingleton<GameLevelMgr>
{
    //static readonly string[] LevelConditionStrFormats = new[]
    //{
    //    "击杀任意怪物{0}个",
    //    "本关卡累计得分达到{0}分",
    //    "击杀任意怪物{0}个或者本关卡累计得分达到{1}分",
    //    "击杀Boss",
    //};

    //static readonly string[] LevelConditionStrFormats_EN = new[]
    //{
    //    "Kill any monster {0} times",
    //    "The cumulative score of this level reaches {0} points",
    //    "Kill any monster {0} or reach {1} points in this level",
    //    "Kill the Boss",
    //};

    public bool IsPlaying { get; private set; }

    public GameLevelConfigData gameLevelConfigData { get; private set; }

    /// <summary>
    /// 关卡条件输出
    /// </summary>
    /// <param name="isBossLevel">是否boss</param>
    /// <param name="targetNum">目标数量</param>
    /// <param name="targetScore">目标分数</param>
    /// <returns></returns>
    public string LevelConditionStr(int level, bool isBossLevel, int targetNum, int targetScore)
    {
        bool isChinese = MachineDataMgr.Instance.IsChineseLanguageVersion;
        //if (isBossLevel)
        //{
        //    return isChinese ? LevelConditionStrFormats[3] : LevelConditionStrFormats_EN[3];
        //}
        //else
        //{
        //    if (targetNum > 0 && targetScore > 0)
        //        return string.Format(isChinese ? LevelConditionStrFormats[2] : LevelConditionStrFormats_EN[2],
        //            targetNum, targetScore);
        //    else if (targetNum > 0)
        //        return string.Format(isChinese ? LevelConditionStrFormats[0] : LevelConditionStrFormats_EN[0],
        //            targetNum);
        //    else
        //        return string.Format(isChinese ? LevelConditionStrFormats[1] : LevelConditionStrFormats_EN[1],
        //            targetScore);
        //}
        GameLevelConfigData.GameLevelConfig config = gameLevelConfigData.data.FirstOrDefault(t => t.level == level && t.scene == GameSceneMgr.Instance.CurScene);
        if (config.levelStr.Contains("{") && config.levelStr.Contains("}") && config.levelStr_EN.Contains("{") && config.levelStr_EN.Contains("}"))
        {
            if (isBossLevel)
            {
                return isChinese ? config.levelStr : config.levelStr_EN;
            }
            else
            {
                if (targetNum > 0 && targetScore > 0)
                    return string.Format(isChinese ? config.levelStr : config.levelStr_EN, targetNum, targetScore);
                else if (targetNum > 0)
                    return string.Format(isChinese ? config.levelStr : config.levelStr_EN, targetNum);
                else
                    return string.Format(isChinese ? config.levelStr : config.levelStr_EN, targetScore);
            }
        }
        else
        {
            return isChinese ? config.levelStr : config.levelStr_EN;
        }
    }

    /// <summary>
    /// 关卡数量
    /// </summary>
    public int LevelNum
    {
        get
        {
            return GameSceneMgr.Instance.CurSceneLevelNum();
            // int curScene = GameSceneMgr.Instance.CurScene - 1;
            // if (curScene >= 0 && curScene < this.gameLevelConfigData.sceneLevelNum.Length)
            //     return this.gameLevelConfigData.sceneLevelNum[curScene];
            //
            // return this.gameLevelConfigData.levelNum;
        }
    }

    private GameLevel curGameLevel;
    private bool isChallengePlay;//是否挑战玩法

    public IEnumerator Preload()
    {
        if (GameSceneMgr.Instance.CurScenePlayType() == GameScenePlay.Challenge)
        {
            yield return UnitMgr.Instance.Preload();
            yield return SoundMgr.Instance.Preload();
        }
        else if (GameSceneMgr.Instance.CurScenePlayType() == GameScenePlay.Plot)
        {
            yield break;
        }
    }

    public void PreloadClear()
    {
        UnitMgr.Instance.PreloadClear();
        SoundMgr.Instance.PreloadClear();
    }
    
    public void Init()
    {
        this.IsPlaying = false;

        gameLevelConfigData = ResMgr.Instance.LoadAssetSync<GameLevelConfigData>("Config/gameLevelConfigData");

        curGameLevel = new GameLevel();
        //游戏开始事件
        EventMgr.Instance.AddListener(GameApp.GameStartEvent, (_, _) =>
        {
            this.IsPlaying = true;

            isChallengePlay = GameSceneMgr.Instance.CurScenePlayType() == GameScenePlay.Challenge;
            if (!isChallengePlay)
                return;

            LevelStart(1);
        });
        //移动相机结束事件
        EventMgr.Instance.AddListener(CameraController.CameraMoveFinishedEvent, (_, _) =>
        {
            if (!this.IsPlaying)
                return;
            
            if (!isChallengePlay)
                return;

            curGameLevel.ShowPassCondition();
        });
        //通关条件提示显示完毕
        EventMgr.Instance.AddListener(LevelConditionTips_UICtrl.ShowTipsFinishedEvent, (_, _) =>
        {
            if (!this.IsPlaying)
                return;
            
            if (!isChallengePlay)
                return;

            curGameLevel.ShowPassConditionFinished();
        });
        //计时结束事件
        EventMgr.Instance.AddListener(CountDown_UICtrl.CountDownFinishedEvent, (_, _) =>
        {
            if (!this.IsPlaying)
                return;
            
            if (!isChallengePlay)
                return;

            int[] data = new[]
            {
                -1,
                GameApp.PLAYER_MAX_BLOOD
            };
            EventMgr.Instance.Emit(PlayerInfos_UICtrl.HPDeductEvent, data); //触发 hp 扣血事件

            curGameLevel.WaitForContinue();
        });
        //玩家激活事件
        EventMgr.Instance.AddListener(HomeWindow_UICtrl.PlayerActiveEvent, (_, _) =>
        {
            if (!this.IsPlaying)
                return;
            
            if (!isChallengePlay)
                return;

            // if (udata == null)
            //     return;
            //
            // var player = (int)udata;
            //
            // if (curGameLevel.IsWaitingForContinue)
            {
                curGameLevel.ContinueLevel();
            }
        });
        //是否继续提示倒计时完毕
        EventMgr.Instance.AddListener(LevelContinueTips_UICtrl.CountFinishedEvent, (_, _) =>
        {
            if (!this.IsPlaying)
                return;
            
            if (!isChallengePlay)
                return;

            curGameLevel.LevelFailed();
        });
        //通关关卡结算显示完毕
        EventMgr.Instance.AddListener(LevelConditionTips_UICtrl.ShowLevelEndTipsFinishedEvent, (_, _) =>
        {
            if (!this.IsPlaying)
                return;
            
            if (!isChallengePlay)
                return;

            curGameLevel.ShowLevelEndTipsFinished();
        });

        //怪物被击杀事件
        EventMgr.Instance.AddListener(GameLevel.MonsterKilledEvent, (_, udata) =>
        {
            if (!this.IsPlaying)
                return;
            
            if (!isChallengePlay)
                return;

            if (udata == null)
                return;

            var datas = (int[])udata;
            int player = datas[0];
            int unitId = datas[1];
            int score = datas[2];
            int health = datas[3];

            curGameLevel.KillMonster(player, unitId, score, health);

            // var datas = (object[])udata;
            // var player = (int)datas[0];
            // var unitInfo = (Unit)datas[1];
            // curGameLevel.KillMonster(player, unitInfo);
        });
    }

    /// <summary>
    /// 开始关卡
    /// </summary>
    /// <param name="level"></param>
    public void LevelStart(int level)
    {
        int curScene = GameSceneMgr.Instance.CurScene; // 当前场景
        GameLevelConfigData.GameLevelConfig config =
            this.gameLevelConfigData.data.FirstOrDefault(t => t.level == level && t.scene == curScene);
        if (config == null)
        {
            Debug.LogError($"找不到关卡配置  scene:{curScene}  level:{level}");
            EventMgr.Instance.Emit(GameApp.WaitForStartEvent, null); //触发 进入等待开始事件
            return;
        }

        this.curGameLevel.LevelStart(config);
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    /// <param name="isWin"></param>
    public void GameOver(bool isWin)
    {
        GameApp.Instance.canStartFlag = false;
        this.IsPlaying = false;
        GameApp.Instance.UpdateToplistData(); //更新排名
        EventMgr.Instance.Emit(HomeWindow_UICtrl.IsShowToplistInfoEvent, true); //触发 打开或隐藏排行榜事件
    }

    public void BackToMenuScene()
    {
        PreloadClear();
        
        StartCoroutine(BackToMenuSceneCoroutine());
    }

    private IEnumerator BackToMenuSceneCoroutine()
    {
        yield return SceneMgr.Instance.EnterSceneAsync("menu");
        UnitMgr.Instance.DestroyAllUnits();
        UnitEffectMgr.Instance.DestroyAllEffect();
        
        CameraController.Instance.RemoveAllCameraRootAnimClips();
        CameraController.Instance.MainCameraRootTran.gameObject.SetActive(false);
        
        
        EventMgr.Instance.Emit(GameApp.WaitForStartEvent, null);
        
        System.GC.Collect();
    }
    
    public int RemainingKillNum
    {
        get
        {
            if (!this.IsPlaying)
                return 0;

            int targetNum = MachineDataMgr.Instance.GetLevelTargetMonsterCount(GameSceneMgr.Instance.CurScene,
                this.curGameLevel.config.level);
            if (this.curGameLevel.KilledNum >= targetNum)
                return 0;

            return targetNum - this.curGameLevel.KilledNum;
        }
    }

    public int MaxUnitNum
    {
        get => this.IsPlaying ? this.curGameLevel.MaxUnitNum : 0;
    }

    /// <summary>
    /// 获取对应id怪物的数量
    /// </summary>
    /// <param name="unitId"></param>
    /// <returns></returns>
    public int SingleUnitLimit(int unitId)
    {
        if (!this.IsPlaying)
            return 0;

        return this.curGameLevel.config.singleUnitLimit.TryGetValue(unitId, out int limit) ? limit : int.MaxValue;
    }

    //保持在攻击点的数量 ---> 镜头前可留的数量
    public int StayInAttackPointLimit
    {
        get => this.IsPlaying ? this.curGameLevel.config.stayInAttackUnitNum : 0;
    }

    public int GetAttackPoint(out Vector3 attackPos, out Vector3 attackPosPre, int[] attackIndexList)
    {
        if (!this.IsPlaying)
        {
            attackPos = Vector3.zero;
            attackPosPre = Vector3.zero;
            return 0;
        }

        int index1 = Random.Range(0, attackIndexList.Length);
        int index = attackIndexList[index1];
        attackPos = this.curGameLevel.config.attackPointList[index];
        attackPosPre = this.curGameLevel.config.attackPointPre[index];
        return index;
    }

    /// <summary>
    /// 获得真正的攻击点
    /// </summary>
    /// <param name="attackPos"></param>
    /// <param name="attackPosPre"></param>
    /// <param name="attackIndexList"></param>
    /// <returns></returns>
    public int GetRealAttackPoint(out Vector3 attackPos, out Vector3 attackPosPre, int[] attackIndexList)
    {
        if (!this.IsPlaying)
        {
            attackPos = Vector3.zero;
            attackPosPre = Vector3.zero;
            return 0;
        }

        // 尝试寻找可以停留的攻击点
        for (var i = 0; i < attackIndexList.Length; i++)
        {
            if (!UnitMgr.Instance.CanStayInAttackPoint(attackIndexList[i]))
                continue;

            attackPos = this.curGameLevel.config.attackPointList[attackIndexList[i]];
            attackPosPre = this.curGameLevel.config.attackPointPre[attackIndexList[i]];
            return attackIndexList[i];
        }

        // 否则随机选择攻击点
        int index1 = Random.Range(0, attackIndexList.Length);
        int index = attackIndexList[index1];
        attackPos = this.curGameLevel.config.attackPointList[index];
        attackPosPre = this.curGameLevel.config.attackPointPre[index];
        return index;
    }

    public int GetAttackPoint(out Vector3 attackPos, int attackIndex)
    {
        if (!this.IsPlaying)
        {
            attackPos = Vector3.zero;
            return 0;
        }

        attackPos = this.curGameLevel.config.attackPointList[attackIndex];
        return attackIndex;
    }
}