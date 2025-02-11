using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

// 击杀怪物飘金币效果
public class GoldCoinEffect
{
    const string PREFAB = "Effects/GoldCoinEffect";
    
    GameObject obj;
    Transform tran;
    Image image;

    Action<GoldCoinEffect> cycleCallback;

    public GoldCoinEffect(Transform parent, Action<GoldCoinEffect> cycleCallback)
    {
        this.cycleCallback = cycleCallback;
        var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(PREFAB);
        this.obj = Object.Instantiate(nodePrefab);
        this.obj.SetActive(false);
        this.tran = this.obj.transform;
        this.tran.SetParent(parent);
        this.tran.localPosition = Vector3.zero;
        this.tran.localEulerAngles = Vector3.zero;
        this.tran.localScale = Vector3.zero;
        this.image = this.tran.Find("Image").GetComponent<Image>();
    }

    public void Start(int player, int score, Vector3 pos)
    {
        this.image.DOKill();
        Color color = this.image.color;
        color.a = 0;
        this.image.color = color;
        this.image.DOFade(1, 0.1f);
        
        this.tran.position = pos;
        this.tran.localScale = Vector3.zero;
        pos = this.tran.localPosition;
        this.tran.DOLocalMoveY(pos.y + 100, 0.25f).SetEase(Ease.OutBack);
        this.tran.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
        this.obj.SetActive(true);

        TimerMgr.Instance.ScheduleOnce((_) =>
        {
            UIEffectMgr.Instance.ShowKillScoreEffectReal(player, score, this.tran.position);
            Recycle();
        },1.0f);
    }

    void Recycle()
    {
        this.image.DOKill();
        this.tran.DOKill();
        this.obj.SetActive(false);
        this.cycleCallback?.Invoke(this);
    }
}
