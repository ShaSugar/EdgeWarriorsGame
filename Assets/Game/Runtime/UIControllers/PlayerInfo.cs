using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo
{
    static readonly string[,] CursorSprite = new string[,]
    {
        {
            "player-0-cursor1",
            "player-0-cursor2",
            "player-0-cursor3"
        },
        {
            "player-1-cursor1",
            "player-1-cursor2",
            "player-1-cursor3"
        },
        {
            "player-2-cursor1",
            "player-2-cursor2",
            "player-2-cursor3"
        },
        {
            "player-3-cursor1",
            "player-3-cursor2",
            "player-3-cursor3"
        }
    };
    static readonly string[,] NameSprite = new string[,]
    {
        {
            "player-a-name",
            "player-a-name_EN",
        },
        {
            "player-b-name",
            "player-b-name_EN",
        },
        {
            "player-c-name",
            "player-c-name_EN",
        },
        {
            "player-d-name",
            "player-d-name_EN",
        }
    };
    /// <summary>
    /// 获取光标图集
    /// </summary>
    /// <param name="player"></param>
    /// <param name="weapon"></param>
    /// <returns></returns>
    static Sprite GetCursorSprite(int player, int weapon)
    {
        string spriteName = CursorSprite[player, weapon];
        return UIMgr.Instance.GetPlayerInfoSprite(spriteName);
    }

    public bool CanShoot
    {
        get => this.hp > 0;
    }

    int index;
    int hp;
    /// <summary>
    /// 玩家分数
    /// </summary>
    public int Score { get; private set; }
    /// <summary>
    /// 玩家击杀数量
    /// </summary>
    public int KillNum { get; private set; }

    public readonly GameObject root;
    Slider hpSlider;
    Text scoreTex;
    Text killNumTex;
    /// <summary>
    /// 玩家道济武器 
    /// </summary>
    public int propWeapon { get; private set; }
    // 开枪时间间隔
    private float fireInterval;
    // 下次允许开枪时间
    private float fireNextTime;

    int showingWeapon;
    /// <summary>
    /// 玩家道具武器子弹 ---> 大概指的是散弹、火箭炮
    /// </summary>
    public int propWeaponBullet { get; private set; }
    /// <summary>
    /// 是否积分翻倍
    /// </summary>
    public bool isInTimeDouble { get; private set; }


    public readonly Transform cursor;
    private Image cursorImage;
    public Transform weaponSelected, bulletPos;
    // RectTransform weapon1Rect, weapon2Rect;

    GameObject insertCoinTips;
    Text insertCoinTipsNum;
    /// <summary>
    /// 玩家死亡图标位置
    /// </summary>
    public Vector3 IconCoverPos
    {
        get => this.iconCover.transform.position;
    }
    /// <summary>
    /// 这个好像是死亡图标
    /// </summary>
    Image iconCover;
    /// <summary>
    /// 死亡文本倒计时
    /// </summary>
    Text iconCoverNum;

    GameObject weapon1;
    GameObject weapon2;
    Image weapon2Img;
    Text weapon2Num;
    GameObject weapon3;
    Text weapon3Num;
    GameObject doubleScore;
    Image doubleScoreCover;

    float protectedTime;

    int languageIndex;
    Image nameImg;
    Transform[,] insertCoinWords;

    public PlayerInfo(int index, GameObject root, Transform cursor)
    {
        this.showingWeapon = -1;
        this.index = index;
        this.root = root;
        this.cursor = cursor;
        MatchComponent();
        InitData();

        UpdateScore();
        UpdateHPSlider();
        HideIconCover();

        MoveOut();
        InsertCoinTipsMoveIn();
    }

    void MatchComponent()
    {
        this.cursorImage = this.cursor.GetComponent<Image>();
        this.insertCoinTips = this.root.transform.Find("InsertCoinTips").gameObject;
        this.insertCoinTipsNum = this.insertCoinTips.transform.Find("Num").GetComponent<Text>();
        this.insertCoinTipsNum.text = MachineDataMgr.Instance.CoinNumForOneGame.ToString();

        this.hpSlider = this.root.transform.Find("HPSlider").GetComponent<Slider>();
        this.scoreTex = this.root.transform.Find("ScoreBg").Find("Score").GetComponent<Text>();
        this.killNumTex = this.root.transform.Find("KillNum").GetComponent<Text>();
        this.bulletPos = this.root.transform.Find("BulletPos");

        Transform tran = this.root.transform.Find("Weapons");
        this.weaponSelected = tran.Find("WeaponSelected");
        // this.weapon1Rect = tran.Find("Weapon1Rect").GetComponent<RectTransform>();
        // this.weapon2Rect = tran.Find("Weapon2Rect").GetComponent<RectTransform>();
        this.weapon1 = tran.Find("Weapon1").gameObject;
        this.weapon2 = tran.Find("Weapon2").gameObject;
        this.weapon2Img = this.weapon2.GetComponent<Image>();
        this.weapon2Num = this.weapon2.transform.Find("Num").GetComponent<Text>();
        this.weapon3 = tran.Find("Weapon3").gameObject;
        this.weapon3Num = this.weapon3.transform.Find("Num").GetComponent<Text>();

        this.doubleScore = this.root.transform.Find("DoubleScore").gameObject;
        this.doubleScoreCover = this.doubleScore.transform.Find("Cover").GetComponent<Image>();

        Transform iconCoverTran = this.root.transform.Find("IconCover");
        this.iconCover = iconCoverTran.GetComponent<Image>();
        this.iconCoverNum = iconCoverTran.Find("Num").GetComponent<Text>();

        this.languageIndex = -1;
        this.nameImg = this.root.transform.Find("Name").GetComponent<Image>();
        this.insertCoinWords = new Transform[3, 2];
        this.insertCoinWords[0, 0] = this.insertCoinTips.transform.Find("Word1");
        this.insertCoinWords[0, 1] = this.insertCoinTips.transform.Find("Word1_EN");
        this.insertCoinWords[1, 0] = this.insertCoinTips.transform.Find("Word2");
        this.insertCoinWords[1, 1] = this.insertCoinTips.transform.Find("Word2_EN");
        this.insertCoinWords[2, 0] = this.insertCoinTips.transform.Find("Word3");
        this.insertCoinWords[2, 1] = this.insertCoinTips.transform.Find("Word3_EN");
        
        UpdateForLanguage();
    }
    /// <summary>
    /// 语言更新
    /// </summary>
    void UpdateForLanguage()
    {
        if (MachineDataMgr.Instance.IsChineseLanguageVersion && this.languageIndex != 0)
        {
            this.languageIndex = 0;
            this.nameImg.sprite = UIMgr.Instance.GetPlayerInfoSprite(NameSprite[this.index, 0]);
            this.weapon2Img.sprite = UIMgr.Instance.GetPlayerInfoSprite("weapon1");
            for (var i = 0; i < 3; i++)
            {
                this.insertCoinWords[i, 0]?.gameObject.SetActive(true);
                this.insertCoinWords[i, 1]?.gameObject.SetActive(false);
            }
        }
        else if (!MachineDataMgr.Instance.IsChineseLanguageVersion && this.languageIndex != 1)
        {
            this.languageIndex = 1;
            this.nameImg.sprite = UIMgr.Instance.GetPlayerInfoSprite(NameSprite[this.index, 1]);
            this.weapon2Img.sprite = UIMgr.Instance.GetPlayerInfoSprite("weapon1_EN");
            for (var i = 0; i < 3; i++)
            {
                this.insertCoinWords[i, 0]?.gameObject.SetActive(false);
                this.insertCoinWords[i, 1]?.gameObject.SetActive(true);
            }
        }
    }

    void InitData()
    {
        this.hp = 0;
        this.Score = 0;
        this.scoreScrollTarget = 1;
        this.KillNum = 0;
        this.propWeapon = 0;
        this.fireInterval = MachineDataMgr.Instance.GetWeaponInterval((WeaponType)this.propWeapon);
        this.fireNextTime = 0;
        this.propWeaponBullet = 0;
        this.isInTimeDouble = false;
    }
    /// <summary>
    /// 重置等待开始
    /// </summary>
    public void ResetForWaitStart()
    {
        ResetScore(); //重置分数
        UpdateHPSlider(); //满血
        HideIconCover();//隐藏死亡图标

        if (this.hp <= 0) //没有血 将玩家对象 移动出
        {
            MoveOut();
            InsertCoinTipsMoveIn();
        }
        else //移动进
        {
            MoveIn();
            InsertCoinTipsMoveOut();
        }
    }
    /// <summary>
    /// 玩家激活 ---> 大概是进玩家
    /// </summary>
    /// <param name="addBlood"></param>
    public void PlayerActive(int addBlood)
    {
        this.protectedTime = Time.time + 3f;
        this.hp += MachineDataMgr.Instance.HpSupply;
        if (this.hp > GameApp.PLAYER_MAX_BLOOD)
            this.hp = GameApp.PLAYER_MAX_BLOOD;
        
        UpdateHPSlider();
        HideIconCover();
        InsertCoinTipsMoveOut();
        MoveIn();
    }

    public void ResetScore()
    {
        this.Score = 0;
        this.scoreScrollTarget = 1;
        this.KillNum = 0;
        this.propWeapon = 0;
        this.fireInterval = MachineDataMgr.Instance.GetWeaponInterval((WeaponType)this.propWeapon);
        this.fireNextTime = 0;
        this.propWeaponBullet = 0;
        this.isInTimeDouble = false;
        UpdateProps();
        UpdateScore();
    }
    /// <summary>
    /// 重置游戏结束
    /// </summary>
    public void ResetForGameOver()
    {
        this.hp = 0;
        this.cursor.gameObject.SetActive(false);
    }
    /// <summary>
    /// 加分数
    /// </summary>
    /// <param name="addScore"></param>
    public void AddScore(int addScore)
    {
        this.Score += addScore;
        UpdateScore();
    }
    /// <summary>
    /// 加击杀数量
    /// </summary>
    public void AddKillNum()
    {
        this.KillNum++;
        UpdateScore();
    }
    /// <summary>
    /// 扣血
    /// </summary>
    /// <param name="value"></param>
    /// <param name="force"></param>
    public void DeductHP(int value, bool force = false)
    {
        if (this.hp <= 0)
            return;

        if (force == false && Time.time < this.protectedTime)
            return;

        this.hp -= value;
        if (this.hp <= 0)
        {
            this.hp = 0;

            InsertCoinTipsMoveIn();
            ShowIconCover();
        }

        UpdateHPSlider();
    }
    public bool CanPlay
    {
        get => this.hp > 0 || this.cursor.gameObject.activeSelf;
    }
    
    public bool HaveHp => this.hp > 0;

    // public bool IsSelectWeapon()
    // {
    //     Vector2 pos = this.cursor.position;
    //     if (RectTransformUtility.RectangleContainsScreenPoint(this.weapon1Rect,
    //         pos))
    //     {
    //         this.weaponSelected.position = this.weapon1Rect.position;
    //         return true;
    //     }
    //     if (RectTransformUtility.RectangleContainsScreenPoint(this.weapon2Rect,
    //         pos))
    //     {
    //         this.weaponSelected.position = this.weapon2Rect.position;
    //         return true;
    //     }
    //
    //     return false;
    // }

    /// <summary>
    /// 开枪
    /// </summary>
    public void Fire()
    {
        if (this.hp <= 0)
            return;

        if (Time.time < this.fireNextTime)
            return;

        this.fireNextTime = Time.time + this.fireInterval;

        this.cursor.DOKill();
        this.cursor.localScale = Vector3.one;
        this.cursor.DOScale(1.1f, 0.1f).OnComplete(() => { this.cursor.DOScale(1f, 0.1f).OnComplete(() => { }); });

        SoundMgr.Instance.PlayOneShot($@"Sounds\weapon{this.propWeapon}_fire", false);
        BulletEffectMgr.Instance.ShowBulletEffect(this.index, this.propWeapon, this.bulletPos.position, this.cursor);
        if (this.propWeapon <= 0)
            return;

        this.propWeaponBullet--;
        if (this.propWeaponBullet <= 0)
        {
            PropWeaponEnd();
        }
        else
        {
            UpdateProps();
        }

    }
    /// <summary>
    /// 移动玩家的ui对象 进来
    /// </summary>
    void MoveIn()
    {
        this.root.transform.DOKill();
        this.root.transform.DOLocalMoveY(2f, 0.5f).SetEase(Ease.OutBack);
        this.cursor.gameObject.SetActive(true);
    }
    /// <summary>
    /// 移动玩家的ui对象 出去
    /// </summary>
    void MoveOut()
    {
        this.root.transform.DOKill();
        this.root.transform.DOLocalMoveY(-250f, 0.5f).SetEase(Ease.InBack);
        this.cursor.gameObject.SetActive(false);
    }

    void InsertCoinTipsMoveIn()
    {
        this.insertCoinTips.transform.DOKill();
        this.insertCoinTips.transform.DOLocalMoveY(360f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            this.insertCoinTips.transform.DOLocalMoveY(360f - 20f, 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        });
    }
    void InsertCoinTipsMoveOut(TweenCallback action = null)
    {
        this.insertCoinTips.transform.DOKill();
        this.insertCoinTips.transform.DOLocalMoveY(-160f, 0.5f).SetEase(Ease.InBack).OnComplete(action);
    }

    Tweener scoreScrollTweener;
    int scoreScrollTarget;
    void UpdateScore()
    {
        if (this.Score < this.scoreScrollTarget)
        {
            this.scoreScrollTweener?.Kill();
            this.scoreScrollTarget = this.Score;
            this.scoreTex.text = this.Score.ToString();
        }
        else if (this.Score > this.scoreScrollTarget)
        {
            this.scoreScrollTweener?.Kill();
            this.scoreScrollTweener = DOTween.To(value =>
            {
                int curValue = Mathf.FloorToInt(value);
                if (!this.scoreScrollTarget.Equals(curValue))
                {
                    this.scoreScrollTarget = curValue;
                    this.scoreTex.text = this.scoreScrollTarget.ToString();
                }
            }, this.scoreScrollTarget, this.Score, 0.2f);
            this.scoreScrollTarget = this.Score;
        }

        this.killNumTex.text = this.KillNum.ToString();
    }
    void UpdateHPSlider()
    {
        this.hpSlider.DOValue((float)this.hp / GameApp.PLAYER_MAX_BLOOD, 0.1f);
        // this.hpSlider.value = this.hp / 100f;
    }

    int iconCoverTimerId = -1;
    int iconCoverTimeCount = 10;
    /// <summary>
    /// 隐藏玩家死亡图标
    /// </summary>
    void HideIconCover()
    {
        this.iconCoverTimeCount = 10;
        if (this.iconCoverTimerId != -1)
        {
            TimerMgr.Instance.UnSchedule(this.iconCoverTimerId);
            this.iconCoverTimerId = -1;
        }
        this.iconCover.DOKill();
        this.iconCover.gameObject.SetActive(false);
    }
    /// <summary>
    /// 显示封面 ---> 结束封面
    /// </summary>
    void ShowIconCover()
    {
        HideIconCover();
        this.iconCoverNum.text = this.iconCoverTimeCount.ToString();
        this.iconCoverTimerId = TimerMgr.Instance.Schedule(_ =>
        {
            this.iconCoverTimeCount--;
            if (this.iconCoverTimeCount <= 0)
                this.iconCoverTimeCount = 0;
            this.iconCoverNum.text = this.iconCoverTimeCount.ToString();
        }, 10, 1, 1);

        this.iconCover.fillAmount = 1;
        this.iconCover.DOFillAmount(0, 11).OnComplete(() =>
        {
            if (this.KillNum > 0 || this.Score > 0)
            {
                HomeWindow_UICtrl.ToplistPlayerDataInsert(index, this.KillNum, this.Score);
                HomeWindow_UICtrl.ToplistPlayerDataSave();
            }

            this.Score = 0;
            this.scoreScrollTarget = 1;
            this.KillNum = 0;
            this.propWeapon = 0;
            this.fireInterval = MachineDataMgr.Instance.GetWeaponInterval((WeaponType)this.propWeapon);
            this.fireNextTime = 0;
            this.propWeaponBullet = 0;
            this.isInTimeDouble = false;
            UpdateProps();
            UpdateScore();
            MoveOut();
            InsertCoinTipsMoveIn();
            HideIconCover();

            if (!GameApp.Instance.IsAnyPlayerCanPlay())
            {
                EventMgr.Instance.Emit(CountDown_UICtrl.PauseCountDownEvent, null);
                EventMgr.Instance.Emit(CountDown_UICtrl.CountDownFinishedEvent, null);
            }
        }).SetEase(Ease.Linear);

        this.iconCover.gameObject.SetActive(true);
    }
    /// <summary>
    /// 更新道具
    /// </summary>
    void UpdateProps()
    {
        Vector3 doubleTimePos;
        if (this.showingWeapon != this.propWeapon)
        {
            this.showingWeapon = this.propWeapon;
            this.cursorImage.sprite = GetCursorSprite(this.index, this.showingWeapon);
            this.cursorImage.SetNativeSize();
        }

        if (this.propWeapon == 1) // 散弹枪
        {
            // this.cursor.localScale = Vector3.one * 2;
            this.weaponSelected.position = this.weapon2.transform.position;
            this.weapon2Num.text = this.propWeaponBullet.ToString();
            this.weapon2.SetActive(true);
            this.weapon3.SetActive(false);
            doubleTimePos = this.weapon2.transform.position + new Vector3(96, 0, 0);
        }
        else if (this.propWeapon == 2) // 火箭炮
        {
            // this.cursor.localScale = Vector3.one * 5;
            this.weaponSelected.position = this.weapon3.transform.position;
            this.weapon3Num.text = this.propWeaponBullet.ToString();
            this.weapon3.SetActive(true);
            this.weapon2.SetActive(false);
            doubleTimePos = this.weapon3.transform.position + new Vector3(96, 0, 0);
        }
        else // 普通枪
        {
            // this.cursor.localScale = Vector3.one * 1;
            this.weaponSelected.position = this.weapon1.transform.position;
            this.weapon2.SetActive(false);
            this.weapon3.SetActive(false);
            doubleTimePos = this.weapon2.transform.position;
        }

        if (this.isInTimeDouble)
        {
            this.doubleScore.transform.position = doubleTimePos;
            this.doubleScore.SetActive(true);
        }
        else
        {
            this.doubleScoreCover.DOKill();
            this.doubleScore.SetActive(false);
        }
    }


    /// <summary>
    /// 激活散弹枪
    /// </summary>
    void ActiveWeapon2()
    {
        if (this.propWeapon != 0)
            return;

        this.propWeapon = 1;
        this.fireInterval = MachineDataMgr.Instance.GetWeaponInterval((WeaponType)this.propWeapon);
        this.fireNextTime = 0;
        this.propWeaponBullet = MachineDataMgr.Instance.ShellGun;

        UpdateProps();
    }

    /// <summary>
    /// 激活火箭炮
    /// </summary>
    void ActiveWeapon3()
    {
        if (this.propWeapon != 0)
            return;

        this.propWeapon = 2;
        this.fireInterval = MachineDataMgr.Instance.GetWeaponInterval((WeaponType)this.propWeapon);
        this.fireNextTime = 0;
        this.propWeaponBullet = MachineDataMgr.Instance.BoltCannon;

        UpdateProps();
    }
    /// <summary>
    /// 道具枪使用完毕
    /// </summary>
    void PropWeaponEnd()
    {
        this.propWeapon = 0;
        this.fireInterval = MachineDataMgr.Instance.GetWeaponInterval((WeaponType)this.propWeapon);
        this.fireNextTime = 0;
        this.propWeaponBullet = 0;

        UpdateProps();
    }

    /// <summary>
    /// 激活得分双倍
    /// </summary>
    void ActiveTimeDouble()
    {
        if (this.isInTimeDouble)
            return;

        this.isInTimeDouble = true;
        UpdateProps();
        this.doubleScoreCover.DOKill();
        this.doubleScoreCover.fillAmount = 1;
        this.doubleScoreCover.DOFillAmount(0, MachineDataMgr.Instance.ScoreDouble).OnComplete(() =>
        {
            this.isInTimeDouble = false;
            UpdateProps();
        }).SetEase(Ease.Linear);
    }

    bool checkPropFlag;
    /// <summary>
    /// 检测道具
    /// </summary>
    /// <param name="unitId"></param>
    /// <param name="unitPos"></param>
    public void CheckProp(int unitId, Vector3 unitPos)
    {
        if (this.checkPropFlag)
            return;

        EItemType propType = MachineDataMgr.Instance.GetDropItem();
        if (propType == EItemType.HP_SUPPLY) // 血量补充
        {
            if (this.hp >= GameApp.PLAYER_MAX_BLOOD)
                return;
        }
        else if (propType == EItemType.SCORE_DOUBLE) // 限时双倍
        {
            if (this.isInTimeDouble)
                return;
        }
        else if (propType is EItemType.SHELL_GUN or EItemType.BOLT_CANNON) // 散弹枪或火箭炮
        {
            if (this.propWeapon != 0)
                return;
        }
        else
        {
            Debug.LogError("未知道具类型");
        }

        this.checkPropFlag = true;
        Vector3 startPos = CameraController.Instance.MainCamera.WorldToScreenPoint(unitPos);
        startPos.z = 0;
        Vector3 endPos = this.iconCover.transform.position;
        UIEffectMgr.Instance.ShowPropFlyEffect(this.index, (int)propType, startPos, endPos, PropFlyCallback);
    }
    void PropFlyCallback(int player, int propType)
    {
        if (player != index)
            return;

        this.checkPropFlag = false;

        if (propType == 1)
        {
            PlayerActive(MachineDataMgr.Instance.HpSupply);
        }
        else if (propType == 2)
        {
            ActiveTimeDouble();
        }
        else if (propType == 3)
        {
            ActiveWeapon2();
        }
        else if (propType == 4)
        {
            ActiveWeapon3();
        }

    }
    /// <summary>
    /// 获取真实击杀分数 ---> 在判断是否积分翻倍
    /// </summary>
    /// <param name="score"></param>
    /// <param name="isInTimeDouble"></param>
    /// <returns></returns>
    public static int GetRealKillScore(int score, bool isInTimeDouble)
    {
        if (isInTimeDouble)
            return score * 2;
        else
            return score;
    }

    public void UpdateData()
    {
        this.insertCoinTipsNum.text = MachineDataMgr.Instance.CoinNumForOneGame.ToString();
        UpdateForLanguage();
    }
}
