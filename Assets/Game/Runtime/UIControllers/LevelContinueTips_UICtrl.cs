using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 关卡倒计时提示 ---> 玩家死亡是否继续倒计时
/// </summary>
public class LevelContinueTips_UICtrl : UICtrl 
{
	/// <summary>
	/// 显示是否继续提示
	/// </summary>
	public const string ShowTipsEvent = "LevelContinueTips_ShowTipsEvent";
	/// <summary>
	/// 停止是否继续提示
	/// </summary>
	public const string StopTipsEvent = "LevelContinueTips_StopTipsEvent";
	/// <summary>
	/// 是否继续提示倒计时完毕
	/// </summary>
	public const string CountFinishedEvent = "LevelContinueTips_CountFinishedEvent";

	Transform tipsTran;
	Image wordImg;
	int wordImgIndex;
	Text tipsNum;
	void Start()
	{
		this.tipsTran = this.View<Transform>("Tips");
		this.wordImg = this.tipsTran.Find("Word").GetComponent<Image>();
		this.wordImgIndex = 0;
		this.tipsNum = this.tipsTran.Find("Num").GetComponent<Text>();
		
		// 显示是否继续提示
		EventMgr.Instance.AddListener(LevelContinueTips_UICtrl.ShowTipsEvent, (_, udata) =>
		{
			if (udata == null)
				return;

			ShowTips((int)udata);
		});
		// 停止是否继续提示
		EventMgr.Instance.AddListener(LevelContinueTips_UICtrl.StopTipsEvent, (_, _) =>
		{
			HideTips();
		});
		
		HideTips();
	}
	
	

	void HideTips()
	{
		if (this.timerId != -1)
		{
			TimerMgr.Instance.UnSchedule(this.timerId);
			this.timerId = -1;
		}
		this.tipsTran.DOKill();
		Vector3 scale = this.tipsTran.localScale;
		scale.y = 0;
		this.tipsTran.localScale = scale;
		this.tipsTran.gameObject.SetActive(false);
	}

	int timerId = -1;
	int countTime;
	void ShowTips(int countTime)
	{
		HideTips();

		if (MachineDataMgr.Instance.IsChineseLanguageVersion && this.wordImgIndex != 0)
		{
			this.wordImgIndex = 0;
			this.wordImg.sprite = UIMgr.Instance.GetPlayerInfoSprite("level-continue-word");
			this.wordImg.SetNativeSize();
		}
		else if (!MachineDataMgr.Instance.IsChineseLanguageVersion && this.wordImgIndex != 1)
		{
			this.wordImgIndex = 1;
			this.wordImg.sprite = UIMgr.Instance.GetPlayerInfoSprite("level-continue-word_EN");
			this.wordImg.SetNativeSize();
		}
		this.countTime = countTime;
		this.tipsNum.text = this.countTime.ToString();
		this.tipsTran.gameObject.SetActive(true);

		this.tipsTran.DOScaleY(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
		{
			this.timerId = TimerMgr.Instance.Schedule(o =>
			{
				if (this.countTime <= 0)
					return;

				this.countTime--;
				this.tipsNum.text = this.countTime.ToString();

				if (this.countTime > 0)
					return;

				this.tipsTran.DOScaleY(0, 0.5f).SetEase(Ease.InBack).OnComplete(HideTips);
				EventMgr.Instance.Emit(LevelContinueTips_UICtrl.CountFinishedEvent, null);
				
			},countTime,1,1);
		});
	}

}

