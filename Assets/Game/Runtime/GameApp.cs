using System.Collections;
using UnityEngine;

public class GameApp : UnitySingleton<GameApp>
{
    /// <summary>
    /// 玩家数量
    /// </summary>
    public const int MAX_PLAYER_COUNT = 4;
    /// <summary>
    /// 玩家满血血量
    /// </summary>
    public const int PLAYER_MAX_BLOOD = 100;

    /// <summary>
    /// 进入等待开始事件
    /// </summary>
    public const string WaitForStartEvent = "GameApp_WaitForStartEvent";
    /// <summary>
    /// 游戏开始事件
    /// </summary>
    public const string GameStartEvent = "GameApp_GameStartEvent";

    // HomeWindow_UICtrl homeWindowUICtrl;
    PlayerInfos_UICtrl playerInfosUICtrl;
    GameStartShowView_UICtrl gameStartShowViewUICtrl;

    public bool canStartFlag;

    public void Init()
    {
        canStartFlag = false;
        
        // 进入等待开始事件
        EventMgr.Instance.AddListener(GameApp.WaitForStartEvent, (_, _) =>
        {
            SoundMgr.Instance.PlayMusic(@"Sounds\bg\bg_waiting");
            
            this.gameStartShowViewUICtrl.Hide();
            StartCoroutine(WaitForLevelStart());
        });

    }

    // 进入游戏
    public IEnumerator EnterGame()
    {
        // 显示加载界面
        UIMgr.Instance.ShowUIView("GUIPrefabs/PatchWindow");

        // 添加关卡管理器
        this.gameObject.AddComponent<GameLevelMgr>().Init();

        // 添加怪物管理器
        this.gameObject.AddComponent<UnitMgr>().Init();

        // 添加剧情玩法怪物管理器
        this.gameObject.AddComponent<PGL_MonsterMgr>().Init();

        // 添加产怪管理器
        this.gameObject.AddComponent<UnitSpawnMgr>().Init();
        
        // 添加UI效果管理器
        this.gameObject.AddComponent<UIEffectMgr>().Init();
        
        // 添加小游戏管理器
        this.gameObject.AddComponent<SmallGameMgr>().Init();
        
        // 机器数据初始化
        MachineDataMgr.Instance.Init();
        this.gameObject.AddComponent<GunMgr>().Init();
        
        // 添加摄像机管理器
        this.gameObject.AddComponent<CameraController>().Init();
        // 添加子弹相关效果管理器
        this.gameObject.AddComponent<BulletEffectMgr>().Init();

        // 等待加载场景完成
        // yield return SceneMgr.Instance.EnterSceneAsync("main",
        //     f => { EventMgr.Instance.Emit(PatchWindow_UICtrl.UpdateLoadingProcessEvent, (int)(f * 80)); });
        //

        yield return new WaitForSeconds(0.5f);
        EventMgr.Instance.Emit(PatchWindow_UICtrl.UpdateLoadingProcessEvent, 90);

        GameSceneMgr.Instance.Init();
        // 显示玩家信息面板
        this.playerInfosUICtrl = UIMgr.Instance.ShowUIView("GUIPrefabs/PlayerInfos") as PlayerInfos_UICtrl;
        this.gameStartShowViewUICtrl = UIMgr.Instance.ShowUIView("GUIPrefabs/GameStartShowView") as GameStartShowView_UICtrl;
        // 显示倒计时
        UIMgr.Instance.ShowUIView("GUIPrefabs/CountDown");
        // 关卡条件提示
        UIMgr.Instance.ShowUIView("GUIPrefabs/LevelConditionTips");
        // 关卡继续游戏提示
        UIMgr.Instance.ShowUIView("GUIPrefabs/LevelContinueTips");
        // 显示待机界面
        // this.homeWindowUICtrl = UIMgr.Instance.ShowUIView("GUIPrefabs/HomeWindow") as HomeWindow_UICtrl;
        UIMgr.Instance.ShowUIView("GUIPrefabs/HomeWindow");
        
        EventMgr.Instance.Emit(PatchWindow_UICtrl.UpdateLoadingProcessEvent, 100);
    }

    // 玩家是否能扣除币开始(还有血量的玩家不能扣除币开始)
    public bool IsPlayerCanStart(int player)
    {
        return canStartFlag && this.playerInfosUICtrl.IsPlayerCanStart(player);
    }

    // 等待玩家开始
    IEnumerator WaitForLevelStart()
    {
        canStartFlag = true;
        GameApp.Instance.DeductAllPlayersHP();
        
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (this.playerInfosUICtrl.IsAnyPlayerCanPlay())
                break;
        }

        GameSceneMgr.Instance.ShowChooseSceneWindow();
        // this.gameStartShowViewUICtrl.Show();
        EventMgr.Instance.Emit(HomeWindow_UICtrl.HideWaitTipsAndToplistEvent, null);
        // // EventMgr.Instance.Emit(GameApp.GameStartEvent, null);
    }

    public bool IsAnyPlayerCanPlay()
    {
        return this.playerInfosUICtrl.IsAnyPlayerCanPlay();
    }
    public bool IsAnyPlayerHaveHp()
    {
        return this.playerInfosUICtrl.IsAnyPlayerHaveHp();
    }
    /// <summary>
    /// 先获得可以玩的玩家
    /// </summary>
    /// <returns></returns>
    public int GetFirstCanPlayPlayer()
    {
        return this.playerInfosUICtrl.GetFirstCanPlayPlayer();
    }
    /// <summary>
    /// 获取玩家光标位置
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Vector3 GetPlayerCursorPos(int player)
    {
        return this.playerInfosUICtrl.GetPlayerCursorPos(player);
    }
    /// <summary>
    /// 更新排行数据
    /// </summary>
    public void UpdateToplistData()
    {
        this.playerInfosUICtrl.UpdateToplistData();
    }

    public bool IsPlayerInTimeDouble(int player)
    {
        return this.playerInfosUICtrl.IsPlayerInTimeDouble(player);
    }

    public void CheckProp(int player, int unitId, Vector3 unitPos)
    {
        this.playerInfosUICtrl.CheckProp(player, unitId, unitPos);
    }
    
    public Vector3 IconCoverPos(int player)
    {
        return this.playerInfosUICtrl.IconCoverPos(player);
    }

    public void ShowGameStartView(int scene)
    {
        this.gameStartShowViewUICtrl.Show(scene);
    }

    public void ShowOrHidePlayerCursorsRoot(bool isShow)
    {
        this.playerInfosUICtrl.playerCursorsRoot.SetActive(isShow);
    }

    public void ShowAllPlayers(bool flag)
    {
        this.playerInfosUICtrl.ShowAllPlayers(flag);
    }

    // 扣除所有玩家血量
    public void DeductAllPlayersHP()
    {
        this.playerInfosUICtrl.DeductAllPlayersHP();
    }


    bool flag;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (flag)
                return;
            
            flag = true;
            
            if (GameLevelMgr.Instance.IsPlaying)
                return;
            
            MachineDataMgr.Instance.ClearAllData();
            MachineDataMgr.Instance.SetDefaultData();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
            flag = false;
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (flag)
                return;
            
            flag = true;
            
            if (GameLevelMgr.Instance.IsPlaying)
                return;
            
            MachineDataMgr.Instance.IsChineseLanguageVersion = !MachineDataMgr.Instance.IsChineseLanguageVersion;
        }
        else if(Input.GetKeyUp(KeyCode.Z))
            flag = false;
    }
}
