using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
/// <summary>
/// 关卡条件提示
/// </summary>
public class LevelConditionTips_UICtrl : UICtrl
{
	/// <summary>
	/// 显示通关条件提示
	/// </summary>
	public const string ShowTipsEvent = "LevelConditionTips_ShowTipsEvent";
	/// <summary>
	/// 通关条件提示显示完毕
	/// </summary>
	public const string ShowTipsFinishedEvent = "LevelConditionTips_ShowTipsFinishedEvent";
	
	
	/// <summary>
	/// 显示关卡结算提示
	/// </summary>
	public const string ShowLevelEndTipsEvent = "LevelConditionTips_ShowLevelEndTipsEvent";
	/// <summary>
	/// 通关关卡结算显示完毕
	/// </summary>
	public const string ShowLevelEndTipsFinishedEvent = "LevelConditionTips_ShowLevelEndTipsFinishedEvent";
	
	Transform tipsTran;
	Text tipsContent;
	void Start()
	{
		this.tipsTran = this.View<Transform>("Tips");
		this.tipsContent = this.tipsTran.Find("Content").GetComponent<Text>();
		
		// 显示通关条件提示
		EventMgr.Instance.AddListener(LevelConditionTips_UICtrl.ShowTipsEvent, (_, udata) =>
		{
			if (udata == null)
				return;

			ShowTips((string)udata);
		});
		
		// 显示关卡结算提示
		EventMgr.Instance.AddListener(LevelConditionTips_UICtrl.ShowLevelEndTipsEvent, (_, udata) =>
		{
			if (udata == null)
				return;

			ShowLevelEndTips((string)udata);
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
	void ShowTips(string content)
	{
		HideTips();
		
		SoundMgr.Instance.PlayOneShot(@"Sounds\content_open", false);
		this.tipsContent.text = content;
		this.tipsTran.gameObject.SetActive(true);

		this.tipsTran.DOScaleY(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
		{
			this.timerId = TimerMgr.Instance.ScheduleOnce(o =>
			{
				this.tipsTran.DOScaleY(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
				{
					SoundMgr.Instance.PlayOneShot(@"Sounds\content_close", false);
					EventMgr.Instance.Emit(LevelConditionTips_UICtrl.ShowTipsFinishedEvent, null);
					HideTips();
				});
			}, 1f);
		});
	}

	void ShowLevelEndTips(string content)
	{
		HideTips();
		
		this.timerId = TimerMgr.Instance.ScheduleOnce(o =>
		{
			SoundMgr.Instance.PlayOneShot(@"Sounds\content_open", false);
			this.tipsContent.text = content;
			this.tipsTran.gameObject.SetActive(true);

			this.tipsTran.DOScaleY(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
			{
				this.timerId = TimerMgr.Instance.ScheduleOnce(o =>
				{
					this.tipsTran.DOScaleY(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
					{
						SoundMgr.Instance.PlayOneShot(@"Sounds\content_close", false);
						EventMgr.Instance.Emit(LevelConditionTips_UICtrl.ShowLevelEndTipsFinishedEvent, null);
						HideTips();
					});
				}, 1f);
			});
		}, 1f);
		
	}

}

