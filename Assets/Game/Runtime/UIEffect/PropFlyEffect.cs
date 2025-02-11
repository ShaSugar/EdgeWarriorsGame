using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PropFlyEffect
{

    GameObject obj;
    Transform tran;
    Image image;

    string prefab;

    int player, propType;
    Action<int, int> callback;
    Action<string, PropFlyEffect> cycleCallback;

    public PropFlyEffect(string prefab, Transform parent, Action<string, PropFlyEffect> cycleCallback)
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
        this.image = this.tran.Find("Image").GetComponent<Image>();
    }

    public void Start(int player, int propType, Vector3 startPos, Vector3 endPos, string spriteName, Action<int, int> callback)
    {
        SoundMgr.Instance.PlayOneShot(@"Sounds\prop_get", false);
        
        this.tran.DOKill();
        this.image.DOKill();
        this.callback = callback;
        this.player = player;
        this.propType = propType;
        this.tran.position = startPos;
        this.image.sprite = UIMgr.Instance.GetPlayerInfoSprite(spriteName);
        this.obj.SetActive(true);

        Color color = this.image.color;
        color.a = 0;
        this.image.color = color;
        color.a = 1;
        Vector3 scale = this.tran.localScale;
        scale.x = 0;
        scale.y = 0;
        this.tran.localScale = scale;
        this.image.DOColor(color, 0.3f);
        this.tran.DOMoveY(startPos.y + 50, 0.3f).SetEase(Ease.OutBack);
        this.tran.DOScale(1, 0.3f).SetEase(Ease.OutBack);
        
        TimerMgr.Instance.ScheduleOnce(
            (_) =>
            {
                SoundMgr.Instance.PlayOneShot(@"Sounds\prop_fly", false);
                this.tran.DOMove(endPos, 0.5f).SetEase(Ease.InBack).OnComplete(Finished);
                this.tran.DOScale(0.5f, 0.5f).SetEase(Ease.InBack);
            }, 1);
    }
    public void Finished()
    {
        this.callback?.Invoke(this.player, this.propType);
        Recycle();
    }

    void Recycle()
    {
        this.tran.DOKill();
        this.image.DOKill();
        this.obj.SetActive(false);
        this.cycleCallback?.Invoke(this.prefab, this);
    }
}
