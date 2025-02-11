using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
/// <summary>
/// 游戏开始显示 ui
/// </summary>
public class GameStartShowView_UICtrl : UICtrl
{
	static readonly string[] ContentStr = {
		"城镇中有很多地方被怪物占据了，快跟你的小伙伴们一起去把它们消灭吧！还天地一个朗朗乾坤！加油喔！！！场景1",
		"城镇中有很多地方被怪物占据了，快跟你的小伙伴们一起去把它们消灭吧！还天地一个朗朗乾坤！加油喔！！！场景2",
        "城镇中有很多地方被怪物占据了，快跟你的小伙伴们一起去把它们消灭吧！还天地一个朗朗乾坤！加油喔！！！场景3",
    };
	static readonly string[] ContentStr_EN = {
		"There are many places in the town occupied by monsters. Go and kill them with your friends! Let's restore peace to the world! Come on!!! Scene 1",
		"There are many places in the town occupied by monsters. Go and kill them with your friends! Let's restore peace to the world! Come on!!! Scene 2",
        "There are many places in the town occupied by monsters. Go and kill them with your friends! Let's restore peace to the world! Come on!!! Scene 3",
    };
	Transform tipsTran;
	Text tipsContent;
	void Start()
	{
		this.tipsTran = this.View<Transform>("Tips");
		this.tipsContent = this.tipsTran.Find("Content").GetComponent<Text>();
		this.tipsTran.gameObject.SetActive(false);
	}
	/// <summary>
	/// 显示开始文本
	/// </summary>
	/// <param name="scene"></param>
	public void Show(int scene)
	{
		SoundMgr.Instance.PlayOneShot(@"Sounds\content_open", false);
		this.tipsContent.text = "";
		this.tipsContent.DOKill();
		this.tipsTran.DOKill();
		this.tipsTran.localScale = Vector3.zero;
		string content = MachineDataMgr.Instance.IsChineseLanguageVersion?ContentStr[scene]:ContentStr_EN[scene];
		this.tipsTran.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
		{
			this.tipsContent.DOText(content, 3f).SetEase(Ease.Linear).OnComplete(() =>
			{
				this.tipsTran.DOKill();
				this.tipsTran.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
				{
					SoundMgr.Instance.PlayOneShot(@"Sounds\content_close", false);
					EventMgr.Instance.Emit(GameApp.GameStartEvent, null);
					Hide();
				});
			});
		});
		
		this.tipsTran.gameObject.SetActive(true);
	}

	public void Hide()
	{
		this.tipsContent.DOKill();
		this.tipsTran.DOKill();
		this.tipsTran.gameObject.SetActive(false);
	}
}

