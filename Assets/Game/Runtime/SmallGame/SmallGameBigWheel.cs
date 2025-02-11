using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// 轮盘主要实现逻辑
/// </summary>
public class SmallGameBigWheel
{
    /// <summary>
    /// 大转盘发奖事件
    /// </summary>
    public const string BigWheelRewardEvent = "SmallGameBigWheel_BigWheelRewardEvent";
    /// <summary>
    /// 每个奖励区域角度
    /// </summary>
    const float ITEM_ANGLE = 360 / 8; //大转盘切分 8 个区域，每一个区域为 45

    GameObject root;
    Transform tran;
    /// <summary>
    /// 转盘对象
    /// </summary>
    Transform turnTran;
    Sequence wheelSequence;
    /// <summary>
    /// 亮灯的图像
    /// </summary>
    RawImage lightingRawImage;
    RawImage turnRawImage;
    /// <summary>
    /// 外部环
    /// </summary>
    RawImage coverRawImage;
    int rawImageIndex;
    int index;
    int result;
    /// <summary>
    /// 检测大转盘是否正在旋转
    /// </summary>
    public bool IsPlaying { get; private set; }
    /// <summary>
    /// 转动完成后的回调
    /// </summary>
    Action finishedCallback;
    public SmallGameBigWheel(GameObject root, Action finishedCallback)
    {
        this.root = root;
        this.tran = this.root.transform;
        this.turnTran = this.tran.Find("Turn");
        this.lightingRawImage = this.tran.Find("Lighting").GetComponent<RawImage>();
        this.finishedCallback = finishedCallback;

        this.turnRawImage = this.turnTran.GetComponent<RawImage>();
        this.coverRawImage = this.tran.Find("Cover").GetComponent<RawImage>();
        this.rawImageIndex = 0;

        Hide();
    }

    /// <summary>
    /// 把转盘显示出来 怪物起点 ---> 玩家终点
    /// </summary>
    /// <param name="result">0~7对应8个奖励</param>
    public void Show(int player, int result, Vector3 startPos, Vector3 endPos)
    {
        Hide();

        if (MachineDataMgr.Instance.IsChineseLanguageVersion && this.rawImageIndex != 0)
        {
            this.rawImageIndex = 0;
            this.turnRawImage.texture = Resources.Load<Texture>("Textures/smallGame_BigWheel01");
            this.turnRawImage.SetNativeSize();
            this.coverRawImage.texture = Resources.Load<Texture>("Textures/smallGame_BigWheel02");
            this.coverRawImage.SetNativeSize();
        }
        else if (!MachineDataMgr.Instance.IsChineseLanguageVersion && this.rawImageIndex == 0)
        {
            this.rawImageIndex = 1;
            this.turnRawImage.texture = Resources.Load<Texture>("Textures/smallGame_BigWheel01_EN");
            this.turnRawImage.SetNativeSize();
            this.coverRawImage.texture = Resources.Load<Texture>("Textures/smallGame_BigWheel02_EN");
            this.coverRawImage.SetNativeSize();
        }
        
        this.index = player;
        this.result = result;
        this.IsPlaying = true;
        this.tran.position = startPos;
        this.tran.localScale = Vector3.zero;
        this.turnTran.localEulerAngles = Vector3.zero;
        this.tran.DOMoveY(startPos.y + 50, 0.3f).SetEase(Ease.OutBack);
        this.tran.DOScale(0.2f, 0.3f).SetEase(Ease.OutBack);
        TimerMgr.Instance.ScheduleOnce(
            (_) =>
            {
                this.tran.DOScale(1f, 0.5f).SetEase(Ease.InBack);
                this.tran.DOMove(endPos, 0.5f).SetEase(Ease.InBack)
                    .OnComplete(StartTurn);
            }, 1);
        this.lightingRawImage.DOKill();
        this.lightingRawImage.gameObject.SetActive(false);
        this.root.SetActive(true);
    }
    // public void Show(int player, int result, Vector3 startPos, Vector3 endPos)
    // {
    //     Hide();
    //     this.IsPlaying = true;
    //     Vector3 pos = this.tran.localPosition;
    //     pos.y = -600f;
    //     this.tran.localPosition = pos;
    //     this.turnTran.localEulerAngles = Vector3.zero;
    //     this.tran.DOLocalMoveY(0f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
    //     {
    //         StartTurn(result);
    //     });
    //     this.root.SetActive(true);
    // }

    /// <summary>
    /// 隐藏转盘
    /// </summary>
    void Hide()
    {
        ClearTween();
        this.tran.DOKill();
        this.turnTran.DOKill();
        this.root.SetActive(false);
        this.IsPlaying = false;
    }
    /// <summary>
    /// 释放动画资源
    /// </summary>
    private void ClearTween()
    {
        if (wheelSequence == null)
            return;

        wheelSequence.Kill();
        wheelSequence = null;
    }
    /// <summary>
    /// 开始旋转
    /// </summary>
    void StartTurn()
    {
        ClearTween();
        int loops = Random.Range(4, 6); //旋转圈数
        //旋转的角度
        float degree = loops * 360f + 360f - (this.result - 1) * ITEM_ANGLE + this.turnTran.localEulerAngles.z - 22.5f;

        wheelSequence = DOTween.Sequence()
            .Append(this.turnTran.DORotate(new Vector3(0f, 0f, -degree), 4f, RotateMode.WorldAxisAdd))
            .SetEase(Ease.InOutCubic)
            .OnUpdate(OnWheelUpdate)
            .OnComplete(OnWheelFinished)
            .SetAutoKill(true)
            .Play();
    }

    void OnWheelUpdate()
    {
    }
    /// <summary>
    /// 转动完成
    /// </summary>
    void OnWheelFinished()
    {
        Color color = this.lightingRawImage.color;
        color.a = 0.2f;
        this.lightingRawImage.color = color;
        this.lightingRawImage.gameObject.SetActive(true);
        this.lightingRawImage.DOKill();
        this.lightingRawImage.DOFade(1, 0.1f).SetLoops(-1, LoopType.Yoyo);
        TimerMgr.Instance.ScheduleOnce((_) =>
        {
            this.lightingRawImage.DOKill();
            color.a = 1f;
            this.lightingRawImage.color = color;
            
            this.tran.DOKill();
            ClearTween();
            this.tran.DOScale(0f, 0.5f).SetEase(Ease.InBack).OnComplete(Hide);
            // 在此发送事假给发奖励模块
            // 玩家：this.index
            // 奖励：this.result(0~7对应八个位置的奖励)
            EventMgr.Instance.Emit(SmallGameBigWheel.BigWheelRewardEvent, new[]
            {
                this.index,
                this.result
            });
            this.finishedCallback?.Invoke();
        }, 1f);
    }
}
