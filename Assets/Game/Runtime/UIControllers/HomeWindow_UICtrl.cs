using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using LitJson;
/// <summary>
/// 主界面 包含排行榜
/// </summary>
public class HomeWindow_UICtrl : UICtrl
{
    static readonly string[] PleaseInsertCoinStr =
    {
        "请投币",
        "Please insert coin"
    };
    static readonly string[] PleaseStartGameStr =
    {
        "请开始",
        "Please start"
    };
    /// <summary>
    /// 隐藏待机界面以及排行榜
    /// </summary>
    public const string HideWaitTipsAndToplistEvent = "HomeWindow_HideWaitTipsAndToplistEvent";
    /// <summary>
    /// 玩家点击开始按键事件
    /// </summary>
    public const string PlayerClickStartBtnEvent = "HomeWindow_PlayerClickStartBtnEvent";
    /// <summary>
    /// 玩家激活事件
    /// </summary>
    public const string PlayerActiveEvent = "HomeWindow_PlayerActiveEvent";

    /// <summary>
    /// 打开或隐藏排行榜事件
    /// </summary>
    public const string IsShowToplistInfoEvent = "HomeWindow_IsShowToplistInfoEvent";

    GameObject bg;
    RawImage bgLogo;
    int bgLogoLanguageIndex;
    Text bgTips;
    Text curCoin;

    void Start()
    {
        this.bg = this.ViewNode("Bg");
        this.bgLogo = this.bg.transform.Find("Logo").GetComponent<RawImage>();
        this.bgLogoLanguageIndex = 0;
        this.bgLogo.transform.DOScale(1.05f, 1.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        this.bgTips = this.bg.transform.Find("Tips").GetComponent<Text>();
        this.curCoin = this.View<Text>("CurCoin");

        ToplistInit();

        // 打开或隐藏排行榜事件
        EventMgr.Instance.AddListener(HomeWindow_UICtrl.IsShowToplistInfoEvent, (_, udata) =>
        {
            var isShow = (bool)udata;
            if (isShow)
            {
                ShowToplist();
            }
            else
            {
                HideToplist();
            }
        });

        // 进入等待开始事件-开启待机显示
        EventMgr.Instance.AddListener(GameApp.WaitForStartEvent, (_, _) =>
        {
            if (GameApp.Instance.IsAnyPlayerCanPlay())
            {
                
                this.bg.SetActive(false);
                this.bgTips.DOKill();
                return;
            }

            HideToplist();

            if (MachineDataMgr.Instance.IsChineseLanguageVersion && this.bgLogoLanguageIndex != 0)
            {
                this.bgLogoLanguageIndex = 0;
                this.bgLogo.texture = ResMgr.Instance.LoadAssetSync<Texture>("Textures/logo");
                this.bgLogo.SetNativeSize();
            }
            else if (!MachineDataMgr.Instance.IsChineseLanguageVersion && this.bgLogoLanguageIndex == 0)
            {
                this.bgLogoLanguageIndex = 1;
                this.bgLogo.texture = ResMgr.Instance.LoadAssetSync<Texture>("Textures/logo_EN");
                this.bgLogo.SetNativeSize();
            }
            this.bg.SetActive(true);
            this.bgTips.DOKill();
            this.bgTips.transform.localScale = Vector3.one * 0.9f;
            this.bgTips.transform.DOScale(Vector3.one * 1.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InCirc);
        });

        // 游戏开始事件-关闭待机显示
        EventMgr.Instance.AddListener(GameApp.GameStartEvent, (_, _) =>
        {
            HideToplist();
            this.bg.SetActive(false);
        });
        // 隐藏待机界面以及排行榜
        EventMgr.Instance.AddListener(HomeWindow_UICtrl.HideWaitTipsAndToplistEvent, (_, _) =>
        {
            HideToplist();
            this.bg.SetActive(false);
        });

        // 投币事件
        EventMgr.Instance.AddListener(GunMgr.CoinAddEvent, (_, _) =>
        {
            SoundMgr.Instance.PlayOneShot(@"Sounds\insert_coin");
            MachineDataMgr.Instance.CoinNumForMachineAdd(1);
            UpdateCurCoin();
        });

        // 玩家点击开始按键事件
        EventMgr.Instance.AddListener(HomeWindow_UICtrl.PlayerClickStartBtnEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            var player = (int)udata;

            if (player >= MachineDataMgr.Instance.PlayerShowCount)
                return;

            if (!GameApp.Instance.IsPlayerCanStart(player))
                return;

            if (!MachineDataMgr.Instance.CheckCoinNumEnoughForStartGame(player))
                return;

            EventMgr.Instance.Emit(HomeWindow_UICtrl.PlayerActiveEvent, player);
            UpdateCurCoin();

            SoundMgr.Instance.PlayOneShot(@"Sounds\gun_start");
        });


        // 数据更新事件
        EventMgr.Instance.AddListener(MachineDataMgr.DataUpdateEvent, (_, _) =>
        {
            UpdateCurCoin();
            
            
            if (MachineDataMgr.Instance.IsChineseLanguageVersion && this.bgLogoLanguageIndex != 0)
            {
                this.bgLogoLanguageIndex = 0;
                this.bgLogo.texture = ResMgr.Instance.LoadAssetSync<Texture>("Textures/logo");
                this.bgLogo.SetNativeSize();
            }
            else if (!MachineDataMgr.Instance.IsChineseLanguageVersion && this.bgLogoLanguageIndex == 0)
            {
                this.bgLogoLanguageIndex = 1;
                this.bgLogo.texture = ResMgr.Instance.LoadAssetSync<Texture>("Textures/logo_EN");
                this.bgLogo.SetNativeSize();
            }
        });

        UpdateCurCoin();
    }
    /// <summary>
    /// 更新当前硬币
    /// </summary>
    void UpdateCurCoin()
    {
        int nowCoinNum = MachineDataMgr.Instance.CoinNumForMachine;
        int needCoin = MachineDataMgr.Instance.CoinNumForOneGame;
        this.curCoin.text = nowCoinNum >= needCoin
            ? $"{nowCoinNum}/{needCoin}"
            : $"<color=red>{nowCoinNum}</color>/{needCoin}";

        string str1;
        string str2;
        if (MachineDataMgr.Instance.IsChineseLanguageVersion)
        {
            str1 = PleaseInsertCoinStr[0];
            str2 = PleaseStartGameStr[0];
        }
        else
        {
            str1 = PleaseInsertCoinStr[1];
            str2 = PleaseStartGameStr[1];
        }
        this.bgTips.text = nowCoinNum >= needCoin ? str2 : $"<color=red>{str1}</color>";
    }


    GameObject toplistObj;
    GameObject toplistMask;
    Transform toplistBgTran;
    RawImage toplistBgImg;
    int toplistBgImgIndex;
    Sprite[] toplistPlayerSprites;
    TopListItem[] topListItems;
    const int TOPLIST_SHOW_NUM = 8; // 排行榜显示排名数量
    int toplistTimerId;
    void ToplistInit()
    {
        ToplistPlayerDataLoad();

        this.toplistTimerId = -1;
        this.toplistObj = this.ViewNode("TopList");
        this.toplistObj.SetActive(false);
        this.toplistMask = this.toplistObj.transform.Find("Mask").gameObject;
        this.toplistBgTran = this.toplistObj.transform.Find("Bg");
        this.toplistBgImg = this.toplistBgTran.GetComponent<RawImage>();
        this.toplistBgImgIndex = 0;
        toplistPlayerSprites = new Sprite[GameApp.MAX_PLAYER_COUNT];
        this.topListItems = new TopListItem[TOPLIST_SHOW_NUM];
        for (var i = 0; i < this.topListItems.Length; i++)
        {
            this.topListItems[i] = new TopListItem(this.toplistBgTran.Find($"Item{i}").gameObject);
            if (i < this.toplistPlayerSprites.Length)
            {
                this.toplistPlayerSprites[i] = this.topListItems[i].GetIconSprite();
            }
        }

        HideToplist();
    }
    void ShowToplist()
    {
        HideToplist();

        if (MachineDataMgr.Instance.IsChineseLanguageVersion && this.toplistBgImgIndex != 0)
        {
            this.toplistBgImgIndex = 0;
            this.toplistBgImg.texture = ResMgr.Instance.LoadAssetSync<Texture>("Textures/ranklistbg");
            this.toplistBgImg.SetNativeSize();
        }
        else if (!MachineDataMgr.Instance.IsChineseLanguageVersion && this.toplistBgImgIndex == 0)
        {
            this.toplistBgImgIndex = 1;
            this.toplistBgImg.texture = ResMgr.Instance.LoadAssetSync<Texture>("Textures/ranklistbg_EN");
            this.toplistBgImg.SetNativeSize();
        }

        toplistData ??= new ToplistData();
        int datasCount = toplistData.playerDatas.Count;
        for (var i = 0; i < this.topListItems.Length; i++)
        {
            if (i < datasCount)
            {
                this.topListItems[i].Show(toplistData.playerDatas[i],
                    this.toplistPlayerSprites[toplistData.playerDatas[i].player]);
            }
            else
            {
                this.topListItems[i].Hide();
            }
        }

        this.toplistMask.SetActive(true);
        this.toplistBgTran.localScale = new Vector3(1, 0, 1);
        this.toplistBgTran.DOScaleY(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            var count = 0;
            this.toplistTimerId = TimerMgr.Instance.Schedule((_) =>
            {
                count++;
                if (count >= 10)
                {
                    this.toplistMask.SetActive(false);
                    this.toplistBgTran.DOScaleY(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                    {
                        this.toplistObj.SetActive(false);
                        HideToplist();
                        GameLevelMgr.Instance.BackToMenuScene();
                    });
                }
                // else
                // {
                //     if (GameApp.Instance.IsAnyPlayerCanPlay())
                //     {
                //         this.toplistObj.SetActive(false);
                //         HideToplist();
                //         GameLevelMgr.Instance.BackToMenuScene();
                //     }
                // }
            }, -1, 0.5f, 0.5f);
        });

        this.toplistObj.SetActive(true);
    }
    void HideToplist()
    {
        if (this.toplistTimerId != -1)
        {
            TimerMgr.Instance.UnSchedule(this.toplistTimerId);
            this.toplistTimerId = -1;
        }
        this.toplistBgTran.DOKill();
        if (this.toplistObj.activeSelf)
        {
            this.toplistMask.SetActive(false);
            this.toplistBgTran.DOScaleY(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                this.toplistObj.SetActive(false);
            });
        }
    }

    class TopListItem
    {
        GameObject root;
        Image icon;
        Text killNum, score;
        public TopListItem(GameObject root)
        {
            this.root = root;
            this.icon = this.root.transform.Find("Icon").GetComponent<Image>();
            this.killNum = this.root.transform.Find("KillNum").GetComponent<Text>();
            this.score = this.root.transform.Find("Score").GetComponent<Text>();
        }

        public Sprite GetIconSprite()
        {
            return this.icon.sprite;
        }

        public void Show(ToplistPlayerData data, Sprite iconSprite)
        {
            this.icon.sprite = iconSprite;
            this.killNum.text = data.killNum.ToString();
            this.score.text = data.score.ToString();
            this.root.SetActive(true);
        }

        public void Hide()
        {
            this.root.SetActive(false);
        }
    }

    static ToplistData toplistData;
    static string ToplistPlayerDataFilePath;
    const string SAVEKEY_TOPLISTPLAYERDATA = "SAVEKEY_TOPLISTPLAYERDATA";
    public static void ToplistPlayerDataLoad()
    {
        string str = PlayerPrefs.GetString(SAVEKEY_TOPLISTPLAYERDATA, string.Empty);
        if (!string.IsNullOrEmpty(str))
        {
            try
            {
                toplistData = JsonMapper.ToObject<ToplistData>(str);
                return;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        toplistData = null;

        // ToplistPlayerDataFilePath = Application.persistentDataPath + "/ToplistPlayerDataFile.json";
        //
        // string str = FileUtils.SafeReadAllText(ToplistPlayerDataFilePath);
        // if (!string.IsNullOrEmpty(str))
        // {
        //     toplistData = JsonMapper.ToObject<ToplistData>(str);
        // }
    }
    public static void ToplistPlayerDataInsert(int player, int killNum, int score)
    {
        if (score <= 0 && killNum <= 0)
            return;

        var data = new ToplistPlayerData(player, killNum, score);

        toplistData ??= new ToplistData();

        if (toplistData.playerDatas.Count >= TOPLIST_SHOW_NUM &&
            toplistData.playerDatas[TOPLIST_SHOW_NUM - 1].CompareTo(data) == 1)
            toplistData.playerDatas.RemoveAt(TOPLIST_SHOW_NUM - 1);

        toplistData.playerDatas.Add(data);
        toplistData.playerDatas.Sort();
    }
    public static void ToplistPlayerDataSave()
    {
        if (toplistData == null)
            return;

        try
        {
            string str = JsonMapper.ToJson(toplistData);
            if (string.IsNullOrEmpty(str))
                return;

            PlayerPrefs.SetString(SAVEKEY_TOPLISTPLAYERDATA, str);
            PlayerPrefs.Save();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        
        // FileUtils.SafeWriteAllText(ToplistPlayerDataFilePath, str);
    }

    class ToplistData
    {
        public List<ToplistPlayerData> playerDatas = new(8);
    }

    struct ToplistPlayerData : IComparable<ToplistPlayerData>
    {
        public ToplistPlayerData(int player, int killNum, int score)
        {
            this.player = player;
            this.killNum = killNum;
            this.score = score;
        }

        public int player;
        public int killNum;
        public int score;

        public int CompareTo(ToplistPlayerData obj)
        {
            if (this.score > obj.score)
                return -1;
            if (this.score < obj.score)
                return 1;

            if (this.killNum > obj.killNum)
                return -1;
            if (this.killNum < obj.killNum)
                return 1;

            return 0;
        }
    }
}
