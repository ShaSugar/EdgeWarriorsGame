using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

// 击杀怪物得分飘字
public class KillScoreEffect
{
    GameObject obj;
    Transform tran;
    Text text;

    string prefab;

    Action<string, KillScoreEffect> cycleCallback;

    public KillScoreEffect(string prefab, Transform parent, Action<string, KillScoreEffect> cycleCallback)
    {
        this.cycleCallback = cycleCallback;
        this.prefab = prefab;
        var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(prefab);
        this.obj = Object.Instantiate(nodePrefab);
        this.obj.SetActive(false);
        this.tran = this.obj.transform;
        this.tran.SetParent(parent);
        this.tran.localPosition = Vector3.zero;
        this.tran.localEulerAngles = Vector3.zero;
        this.tran.localScale = Vector3.one;
        this.text = this.tran.Find("Text").GetComponent<Text>();
    }

    public void Start(Vector3 startPos, int score)
    {
        this.tran.DOKill();
        this.text.DOKill();
        
        this.tran.position = startPos;
        this.text.text = $"+{score}";
        Color color = this.text.color;
        color.a = 0;
        this.text.color = color;
        this.obj.SetActive(true);

        this.tran.localScale = Vector3.zero;
        this.text.DOFade(1f, 0.1f);
        this.tran.DOScale(1f, 0.2f)
            .SetEase(Ease.OutBack);
        
        Vector3 endPos = this.tran.localPosition;
        endPos.y += 50;
        endPos.x += 30;
        TimerMgr.Instance.ScheduleOnce((_) =>
        {
            this.text.DOFade(0f, 0.2f);
            this.tran.DOLocalMove(endPos, 0.2f).SetEase(Ease.InBack).OnComplete(Recycle);
        }, 0.5f);
    }

    void Recycle()
    {
        this.tran.DOKill();
        this.text.DOKill();
        this.obj.SetActive(false);
        this.cycleCallback?.Invoke(prefab, this);
    }
}
