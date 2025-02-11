using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
/// <summary>
/// 倒计时 ui 
/// </summary>
public class CountDown_UICtrl : UICtrl
{
    static readonly string[] Kill_Str = {"击杀：","Kill:"};
    static readonly string[] Score_Str = {"得分：","Score:"};
    
    /// <summary>
    /// 开始计时事件
    /// </summary>
    public const string StartCountDownEvent = "CountDown_StartCountDownEvent";
    /// <summary>
    /// 计时结束事件
    /// </summary>
    public const string CountDownFinishedEvent = "CountDown_CountDownFinishedEvent";
    
    /// <summary>
    /// 暂停计时
    /// </summary>
    public const string PauseCountDownEvent = "CountDown_PauseCountDownEvent";
    /// <summary>
    /// 恢复计时
    /// </summary>
    public const string RestoreCountDownEvent = "CountDown_RestoreCountDownEvent";
    
    
    /// <summary>
    /// 更新击杀数量以及累计积分事件
    /// </summary>
    public const string UpdateKillNumAndScoreEvent = "CountDown_UpdateKillNumAndScoreEvent";
    /// <summary>
    /// 更新boss血量事件
    /// </summary>
    public const string UpdateBossHPEvent = "CountDown_UpdateBossHPEvent";

    Transform bg;
    Text second1, second2, minute1, minute2, killNum, scoreNum;

    int levelCountSeconds;
    bool isCounting;
    int countSeconds;
    int scene;
    int targetKillNum;
    int targetScore;
    bool isBossLevel;
    int cacheKillNum;
    int cacheScore;

    Slider bossHPSlider;
    Image bossIcon;
    void Start()
    {
        this.bg = this.ViewNode("bg").transform;
        this.second1 = this.bg.Find("second1").GetComponent<Text>();
        this.second2 = this.bg.Find("second2").GetComponent<Text>();
        this.minute1 = this.bg.Find("minute1").GetComponent<Text>();
        this.minute2 = this.bg.Find("minute2").GetComponent<Text>();
        this.killNum = this.bg.Find("killNum").GetComponent<Text>();
        this.scoreNum = this.bg.Find("scoreNum").GetComponent<Text>();
        this.bossHPSlider = this.bg.Find("BossHPSlider").GetComponent<Slider>();
        this.bossIcon = bossHPSlider.transform.Find("header_bg/icon").GetComponent<Image>();    
        this.bg.DOLocalMoveY(120, 0.5f);

        this.bossHPSlider.value = 1f;
        this.isCounting = false;
        this.countSeconds = 0;
        this.targetKillNum = 0;
        this.targetScore = 0;
        UpdateText();

        // 监听开始计时事件
        EventMgr.Instance.AddListener(CountDown_UICtrl.StartCountDownEvent, (_, _) =>
        {
            int time = this.levelCountSeconds;

            UpdateText();
            if (this.isBossLevel)
            {
                // this.bossHPSlider.value = 1f;
                bossIcon.sprite = UIMgr.Instance.GetPlayerInfoSprite($"boss_icon{scene}");
                this.bossHPSlider.gameObject.SetActive(true);
                UpdateKillNumAndScore(0, 0, 0, 0);
            }
            else
            {
                this.bossHPSlider.gameObject.SetActive(false);
                UpdateKillNumAndScore(this.cacheKillNum, this.targetKillNum, this.cacheScore, this.targetScore);
            }
            
            this.bg.DOKill();
            this.bg.DOLocalMoveY(-58, 0.3f).SetEase(Ease.OutBack);
            
            StartCountDown(time);
        });
        
        // 暂停计时
        EventMgr.Instance.AddListener(CountDown_UICtrl.UpdateBossHPEvent, (_, udata) =>
        {
            var data = (int[])udata;
            int curHP = data[0];
            int totalHp = data[1];
            this.bossHPSlider.DOValue((float)curHP / totalHp, 0.2f);
        });
        
        // 暂停计时
        EventMgr.Instance.AddListener(CountDown_UICtrl.PauseCountDownEvent, (_, udata) =>
        {
            this.isCounting = false;
        });
        // 恢复计时
        EventMgr.Instance.AddListener(CountDown_UICtrl.RestoreCountDownEvent, (_, udata) =>
        {
            this.isCounting = true;
        });

        // 更新击杀数量以及累计积分事件
        EventMgr.Instance.AddListener(CountDown_UICtrl.UpdateKillNumAndScoreEvent, (_, udata) =>
        {
            if (this.isBossLevel)
                return;
            
            if (udata == null)
                return;

            var data = (int[])udata;
            this.cacheKillNum = data[0];
            this.cacheScore = data[1];
            // int targetKillNum = data[2];
            // int targetScore = data[3];
            UpdateKillNumAndScore(this.cacheKillNum, data[2], this.cacheScore, data[3]);
        });
        
        // 关卡开始
        EventMgr.Instance.AddListener(GameLevel.GameLevelStartEvent, (_, udata) =>
        {
            StopCountDown();

            if (udata == null)
                return;

            var data = (int[])udata;

            this.levelCountSeconds = data[1];
            this.countSeconds = this.levelCountSeconds;

            this.cacheKillNum = 0;
            this.cacheScore = 0;
            this.targetKillNum = data[2];
            this.targetScore = data[3];
            this.isBossLevel = data[4] == 1;
            scene = data[data.Length - 1];
            this.bg.DOKill();
            this.bg.DOLocalMoveY(62f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                this.bossHPSlider.value = 1f;
            });
        });

        // 关卡完成（过关）事件
        EventMgr.Instance.AddListener(GameLevel.FinishedEvent, (_, _) => { this.bossHPSlider.gameObject.SetActive(false); StopCountDown(); });
        // 关卡失败（游戏结束）事件
        EventMgr.Instance.AddListener(GameLevel.FailedEvent, (_, udata) => { this.bossHPSlider.gameObject.SetActive(false); StopCountDown(); });

        // 进入等待开始事件
        EventMgr.Instance.AddListener(GameApp.WaitForStartEvent, (_, _) =>
        {

            this.bg.DOKill();
            this.bg.DOLocalMoveY(62, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                this.bossHPSlider.value = 1f;
            });
        });
    }

    void OnDestroy()
    {
        StopCountDown();
    }
    /// <summary>
    /// 更新时间文本
    /// </summary>
    void UpdateText()
    {
        int second = (this.countSeconds % 60);
        int minute = (this.countSeconds / 60) % 60;

        this.second1.text = (second % 10).ToString();
        this.second2.text = (second / 10).ToString();
        this.minute1.text = (minute % 10).ToString();
        this.minute2.text = (minute / 10).ToString();
    }
    /// <summary>
    /// 更新击杀数量和分数
    /// </summary>
    /// <param name="curKillNum"></param>
    /// <param name="targetKillNum"></param>
    /// <param name="curScore"></param>
    /// <param name="targetScore"></param>
    void UpdateKillNumAndScore(int curKillNum, int targetKillNum, int curScore, int targetScore)
    {
        if (targetKillNum <= 0)
        {
            this.killNum.text = "";
        }
        else
        {
            string str = MachineDataMgr.Instance.IsChineseLanguageVersion ? Kill_Str[0] : Kill_Str[1];
            if (curKillNum >= targetKillNum)
            {
                this.killNum.text = $"{str}{curKillNum}/{targetKillNum}";
            }
            else
            {
                this.killNum.text = $"{str}<color=red>{curKillNum}</color>/{targetKillNum}";
            }
        }
        if (targetScore <= 0)
        {
            this.scoreNum.text = "";
        }
        else
        {
            string str = MachineDataMgr.Instance.IsChineseLanguageVersion ? Score_Str[0] : Score_Str[1];
            if (curScore >= targetScore)
            {
                this.scoreNum.text = $"{str}{curScore}/{targetScore}";
            }
            else
            {
                this.scoreNum.text = $"{str}<color=red>{curScore}</color>/{targetScore}";
            }
        }
    }

    int loadingTipsTimerId = -1;
    /// <summary>
    /// 开始倒计时
    /// </summary>
    /// <param name="time"></param>
    void StartCountDown(int time)
    {
        StopCountDown();
        this.isCounting = true;
        this.countSeconds = time;
        this.loadingTipsTimerId = TimerMgr.Instance.Schedule(o =>
        {
            if (!this.isCounting)
                return;

            this.countSeconds--;
            if (this.countSeconds <= 0)
            {
                this.isCounting = false;
                this.countSeconds = 0;
                UpdateText();
                StopCountDown();
                EventMgr.Instance.Emit(CountDown_UICtrl.CountDownFinishedEvent, null);
            }
            else
            {
                UpdateText();
            }
        }, -1, 1f, 1);
    }
    /// <summary>
    /// 停止倒计时
    /// </summary>
    void StopCountDown()
    {
        this.isCounting = false;
        if (this.loadingTipsTimerId == -1)
            return;
        TimerMgr.Instance.UnSchedule(this.loadingTipsTimerId);
        this.loadingTipsTimerId = -1;
    }

}
