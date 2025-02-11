using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerInfos_UICtrl : UICtrl
{
    /// <summary>
    /// 是否可以射击
    /// </summary>
    public const string IsCanShootEvent = "PlayerInfos_IsCanShootEvent";
    // public const string ScoreAddEvent = "PlayerInfos_ScoreAddEvent";
    /// <summary>
    /// hp 扣血事件
    /// </summary>
    public const string HPDeductEvent = "PlayerInfos_HPDeductEvent";

    public const string ShowPlotTipsEvent = "PlayerInfos_ShowPlotTipsEvent";

    PlayerInfo[] playerInfos;

    public GameObject playerCursorsRoot { get; private set; }
    /// <summary>
    /// 玩家光标位置
    /// </summary>
    Transform[] playerCursors;
    Vector2[] cursorsOriginalPos;

    bool canShoot;
    // RectTransform rectTransform;
    
    
    Transform plotTipsTran;
    Text plotTipsContent;
    void Start()
    {
        plotTipsTran = this.View<Transform>("PlotTips");
        plotTipsContent = this.plotTipsTran.Find("Content").GetComponent<Text>();
        HidePlotTips();
        
        // this.rectTransform = this.GetComponent<RectTransform>();
        this.canShoot = false;

        playerCursorsRoot = this.ViewNode("PlayerCursors");
        this.playerInfos = new PlayerInfo[GameApp.MAX_PLAYER_COUNT];
        this.playerCursors = new Transform[GameApp.MAX_PLAYER_COUNT];
        this.cursorsOriginalPos = new Vector2[GameApp.MAX_PLAYER_COUNT];
        for (var i = 0; i < this.playerInfos.Length; i++)
        {
            GameObject root = this.ViewNode($"Player{i}");
            Transform cursor = playerCursorsRoot.transform.Find($"Player{i}Cursor");
            this.playerInfos[i] = new PlayerInfo(i, root, cursor);
            this.playerCursors[i] = cursor;
            this.cursorsOriginalPos[i] = this.playerCursors[i].position;
        }
        
        EventMgr.Instance.AddListener(PlayerInfos_UICtrl.ShowPlotTipsEvent, (_, udata) => 
        {
            if (udata == null)
                return;
            
            ShowPlotTips((string)udata);
        });
        
        //是否可以射击
        EventMgr.Instance.AddListener(PlayerInfos_UICtrl.IsCanShootEvent, (_, udata) => 
        {
            if (udata == null)
                return;

            this.canShoot = (bool)udata;
        });

        //进入等待开始事件
        EventMgr.Instance.AddListener(GameApp.WaitForStartEvent, (_, _) =>
        {
            for (var i = 0; i < this.playerInfos.Length; i++)
            {
                this.playerInfos[i].ResetForWaitStart();
            }
        });
        //玩家激活事件
        EventMgr.Instance.AddListener(HomeWindow_UICtrl.PlayerActiveEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            var player = (int)udata;
            this.playerInfos[player].PlayerActive(int.MaxValue);
        });

        // EventMgr.Instance.AddListener(PlayerInfos_UICtrl.ScoreAddEvent, (_, udata) =>
        // {
        //     if (udata == null)
        //         return;
        //
        //     var datas = (object[])udata;
        //     var player = (int)datas[0];
        //     var unitInfo = (Unit)datas[1];
        //
        //     this.playerInfos[player].AddScore(unitInfo.Score);
        //
        // });

        //怪物被击杀事件
        EventMgr.Instance.AddListener(GameLevel.MonsterKilledEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            var datas = (int[])udata;
            int player = datas[0];
            int score = datas[2];

            this.playerInfos[player].AddScore(score);
            this.playerInfos[player].AddKillNum();
        });
        //hp 扣血
        EventMgr.Instance.AddListener(PlayerInfos_UICtrl.HPDeductEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            var datas = (int[])udata;
            int player = datas[0];
            int hp = datas[1];

            if (player >= 0 && player < this.playerInfos.Length)
            {
                this.playerInfos[player].DeductHP(hp, true);
            }
            else
            {
                for (var i = 0; i < this.playerInfos.Length; i++)
                {
                    this.playerInfos[i].DeductHP(hp, true);
                }
            }
        });
        //怪物攻击玩家事件
        EventMgr.Instance.AddListener(UnitMgr.UnitAttackEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            var hp = (int)udata;

            for (var i = 0; i < this.playerInfos.Length; i++)
            {
                this.playerInfos[i].DeductHP(hp, false);
            }
        });
        //关卡完成事件
        EventMgr.Instance.AddListener(GameLevel.FinishedEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            this.canShoot = false;

            var level = (int)udata;
            if (level >= GameLevelMgr.Instance.LevelNum)
            {
                for (var i = 0; i < this.playerInfos.Length; i++)
                {
                    this.playerInfos[i].ResetForGameOver();
                }
            }

        });
        //关卡失败事件
        EventMgr.Instance.AddListener(GameLevel.FailedEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            this.canShoot = false;

            for (var i = 0; i < this.playerInfos.Length; i++)
            {
                this.playerInfos[i].ResetForGameOver();
            }

            HomeWindow_UICtrl.ToplistPlayerDataSave();
        });
        //光标位置更新
        EventMgr.Instance.AddListener(GunMgr.CursorsPosUpdateEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            var data = (Vector2[])udata;
            if (data.Length < this.playerCursors.Length)
                return;

            for (var i = 0; i < this.playerCursors.Length; i++)
            {
                if(!this.playerInfos[i].CanPlay)
                    continue;
                
                this.playerCursors[i].position = data[i];
            }
        });
        //广播开枪事件
        EventMgr.Instance.AddListener(GunMgr.ShootEvent, (_, udata) =>
        {

            if (false == this.canShoot)
                return;

            if (udata == null)
                return;

            var data = (bool[])udata;

            for (var i = 0; i < data.Length; i++)
            {
                if (!data[i])
                    continue;

                playerInfos[i].Fire();

                // if (playerInfos[i].IsSelectWeapon())
                //     continue;
                //
                // if(!playerInfos[i].CanShoot)
                //     continue;
                //     
                // BulletEffectMgr.Instance.ShowBulletEffect(i, 0, this.playerInfos[i].bulletPos.position, this.playerCursors[i]);
            }
        });

        ResetAllCursorPos();
    }
    /// <summary>
    /// 检测任何玩家都可以玩不 ---> 不太理解
    /// </summary>
    /// <returns></returns>
    public bool IsAnyPlayerCanPlay()
    {
        return this.playerInfos.Any(t => t.CanPlay);
    }

    public bool IsAnyPlayerHaveHp()
    {
        return this.playerInfos.Any(t => t.HaveHp);
    }
    /// <summary>
    /// 先获得可以玩的玩家
    /// </summary>
    /// <returns></returns>
    public int GetFirstCanPlayPlayer()
    {
        for (var i = 0; i < this.playerInfos.Length; i++)
        {
            if (this.playerInfos[i].CanPlay)
            {
                return i;
            }
        }
        return -1;
    }
    /// <summary>
    /// 获取玩家光标位置
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Vector3 GetPlayerCursorPos(int player)
    {
        return this.playerCursors[player].position;
    }
    /// <summary>
    /// 检测玩家是否可以开始 ---> hp > 0
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsPlayerCanStart(int player)
    {
        return !this.playerInfos[player].CanShoot;
    }

    public void UpdateToplistData()
    {
        for (var i = 0; i < this.playerInfos.Length; i++)
        {
            if (this.playerInfos[i].KillNum > 0 || this.playerInfos[i].Score > 0)
            {
                HomeWindow_UICtrl.ToplistPlayerDataInsert(i, this.playerInfos[i].KillNum, this.playerInfos[i].Score);
            }
        }
        HomeWindow_UICtrl.ToplistPlayerDataSave();
    }

    /// <summary>
    /// 重置所有光标位置
    /// </summary>
    void ResetAllCursorPos()
    {
        EventMgr.Instance.Emit(GunMgr.ResetCursorsPosEvent, this.cursorsOriginalPos);
        for (var i = 0; i < this.playerCursors.Length; i++)
        {
            this.playerCursors[i].position = this.cursorsOriginalPos[i];
        }
    }

    
    /// <summary>
    /// 显示所有玩家
    /// </summary>
    /// <param name="flag"></param>
    public void ShowAllPlayers(bool flag)
    {
        if (flag)
        {
            for (var i = 0; i < this.playerInfos.Length; i++)
            {
                this.playerInfos[i].UpdateData();
            }
            
            UpdatePlayerShowCount();
        }
        else
        {
            for (var i = 0; i < this.playerInfos.Length; i++)
            {
                this.playerInfos[i].root.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 更新玩家显示数量
    /// </summary>
    void UpdatePlayerShowCount()
    {
        int playerShowCount = MachineDataMgr.Instance.PlayerShowCount;
        for (var i = 0; i < this.playerInfos.Length; i++)
        {
            if (i < playerShowCount)
            {
                this.playerInfos[i].root.SetActive(true);
            }
            else
            {
                this.playerInfos[i].root.SetActive(false);
                this.playerInfos[i].ResetForGameOver();
            }
        }
        if (playerShowCount <= 1)
        {
            Vector3 pos = this.playerInfos[0].root.transform.localPosition;
            pos.x = 0;
            this.playerInfos[0].root.transform.localPosition = pos;
            
            this.cursorsOriginalPos[0].x = 147;
        }
        else if (playerShowCount <= 2)
        {
            Vector3 pos = this.playerInfos[0].root.transform.localPosition;
            pos.x = -480;
            this.playerInfos[0].root.transform.localPosition = pos;
            pos = this.playerInfos[1].root.transform.localPosition;
            pos.x = 480;
            this.playerInfos[1].root.transform.localPosition = pos;
            
            this.cursorsOriginalPos[0].x = -333;
            this.cursorsOriginalPos[1].x = 633;
        }
        else if (playerShowCount <= 3)
        {
            Vector3 pos = this.playerInfos[0].root.transform.localPosition;
            pos.x = -640;
            this.playerInfos[0].root.transform.localPosition = pos;
            pos = this.playerInfos[1].root.transform.localPosition;
            pos.x = 0;
            this.playerInfos[1].root.transform.localPosition = pos;
            pos = this.playerInfos[2].root.transform.localPosition;
            pos.x = 640;
            this.playerInfos[2].root.transform.localPosition = pos;
            
            this.cursorsOriginalPos[0].x = -493;
            this.cursorsOriginalPos[1].x = 153;
            this.cursorsOriginalPos[2].x = 799;
        }
        else
        {
            Vector3 pos = this.playerInfos[0].root.transform.localPosition;
            pos.x = -711;
            this.playerInfos[0].root.transform.localPosition = pos;
            pos = this.playerInfos[1].root.transform.localPosition;
            pos.x = -237;
            this.playerInfos[1].root.transform.localPosition = pos;
            pos = this.playerInfos[2].root.transform.localPosition;
            pos.x = 237;
            this.playerInfos[2].root.transform.localPosition = pos;
            pos = this.playerInfos[3].root.transform.localPosition;
            pos.x = 711;
            this.playerInfos[3].root.transform.localPosition = pos;

            this.cursorsOriginalPos[0].x = -564;
            this.cursorsOriginalPos[1].x = -84;
            this.cursorsOriginalPos[2].x = 396;
            this.cursorsOriginalPos[3].x = 876;
        }

        ResetAllCursorPos();
    }
    /// <summary>
    /// 玩家的积分是否双倍 ---> 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsPlayerInTimeDouble(int player)
    {
        return this.playerInfos[player].isInTimeDouble;
    }
    /// <summary>
    /// 检测道具
    /// </summary>
    /// <param name="player">玩家</param>
    /// <param name="unitId">怪物id</param>
    /// <param name="unitPos">怪物位置</param>
    public void CheckProp(int player, int unitId, Vector3 unitPos)
    {
        this.playerInfos[player].CheckProp(unitId, unitPos);
    }
    /// <summary>
    /// 这个好像是死亡图标位置 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Vector3 IconCoverPos(int player)
    {
        return this.playerInfos[player].IconCoverPos;
    }

    public void DeductAllPlayersHP()
    {
        foreach (var t in this.playerInfos)
        {
            t.DeductHP(int.MaxValue, true);
        }
    }
    
    

    // bool flag;
    // void OnGUI()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha0))
    //     {
    //         if (flag)
    //             return;
    //         
    //         
    //         Tweener t = DOTween.To(value => { 
    //             Debug.LogWarning($"1111111-----{value}");
    //         }, startValue: 0, endValue: 10, duration: 0.5f);
    //        
    //         // DOTween.To(() => 0, x =>
    //         // {
    //         // }, 10, 1f).SetEase(Ease.Linear);
    //     }
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         if (flag)
    //             return;
    //         
    //         
    //     }
    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         if (flag)
    //             return;
    //         
    //     }
    //     if (Input.GetKeyUp(KeyCode.Alpha0))
    //     {
    //         flag = false;
    //     }
    //     if (Input.GetKeyUp(KeyCode.Alpha1))
    //     {
    //         flag = false;
    //     }
    //     if (Input.GetKeyUp(KeyCode.Alpha2))
    //     {
    //         flag = false;
    //     }
    // }

    private int _plotTipsTimerId;
    void HidePlotTips()
    {
        TimerMgr.Instance.UnSchedule(_plotTipsTimerId);
        plotTipsTran.DOKill();
        Vector3 scale = plotTipsTran.localScale;
        scale.y = 0;
        plotTipsTran.localScale = scale;
        plotTipsTran.gameObject.SetActive(false);
    }
    
    void ShowPlotTips(string content)
    {
        HidePlotTips();
		
        SoundMgr.Instance.PlayOneShot(@"Sounds\content_open", false);
        plotTipsContent.text = content;
        plotTipsTran.gameObject.SetActive(true);

        plotTipsTran.DOScaleY(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _plotTipsTimerId = TimerMgr.Instance.ScheduleOnce(o =>
            {
                plotTipsTran.DOScaleY(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    SoundMgr.Instance.PlayOneShot(@"Sounds\content_close", false);
                    HidePlotTips();
                });
            }, 1f);
        });
    }
}

