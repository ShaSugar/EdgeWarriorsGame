using System;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectMgr : UnitySingleton<UIEffectMgr>
{
    static readonly string[,] PropFlyEffectSprite = new [,]
    {
        {"fly_prop1", "fly_prop1_EN"},
        {"fly_prop2", "fly_prop2_EN"},
        {"fly_prop3", "fly_prop3_EN"},
        {"fly_prop4", "fly_prop4_EN"},
    };
    
    static readonly string[] KillScoreEffectPrefab = new[]
    {
        "Effects/KillScoreEffect1",
        "Effects/KillScoreEffect2",
        "Effects/KillScoreEffect3",
        "Effects/KillScoreEffect4",
    };
    static readonly string[] PropFlyEffectPrefab = new[]
    {
        "Effects/PropFlyEffect1",
        "Effects/PropFlyEffect2",
        "Effects/PropFlyEffect3",
        "Effects/PropFlyEffect4",
    };
    
    Dictionary<string, Queue<KillScoreEffect>> killScoreEffectDic;
    Dictionary<string, Queue<PropFlyEffect>> propFlyEffectDic;
    Queue<GoldCoinEffect> goldCoinEffects;

    UIEffectView_UICtrl uiEffectViewUICtrl;
    Transform uiEffectViewTran;
    public void Init()
    {
        this.uiEffectViewUICtrl = UIMgr.Instance.ShowUIView("GUIPrefabs/UIEffectView") as UIEffectView_UICtrl;
        if (this.uiEffectViewUICtrl == null)
            return;

        this.uiEffectViewTran = this.uiEffectViewUICtrl.transform;
    }

    KillScoreEffect GetKillScoreEffect(string prefab)
    {
        if(this.killScoreEffectDic == null || 
           !this.killScoreEffectDic.ContainsKey(prefab) || 
           this.killScoreEffectDic[prefab].Count <= 0)
            return new KillScoreEffect(prefab, this.uiEffectViewTran, KillScoreEffectCallback);

        return this.killScoreEffectDic[prefab].Dequeue();
    }

    void KillScoreEffectCallback(string prefab, KillScoreEffect effect)
    {
        this.killScoreEffectDic ??= new Dictionary<string, Queue<KillScoreEffect>>();
        if(!this.killScoreEffectDic.ContainsKey(prefab))
            this.killScoreEffectDic.Add(prefab, new Queue<KillScoreEffect>());
        
        this.killScoreEffectDic[prefab].Enqueue(effect);
    }


    PropFlyEffect GetPropFlyEffect(string prefab)
    {
        if(this.propFlyEffectDic == null || 
           !this.propFlyEffectDic.ContainsKey(prefab) || 
           this.propFlyEffectDic[prefab].Count <= 0)
            return new PropFlyEffect(prefab, this.uiEffectViewTran, PropFlyEffectCallback);

        return this.propFlyEffectDic[prefab].Dequeue();
    }
    
    void PropFlyEffectCallback(string prefab, PropFlyEffect effect)
    {
        this.propFlyEffectDic ??= new Dictionary<string, Queue<PropFlyEffect>>();
        if(!this.propFlyEffectDic.ContainsKey(prefab))
            this.propFlyEffectDic.Add(prefab, new Queue<PropFlyEffect>());
        
        this.propFlyEffectDic[prefab].Enqueue(effect);
    }

    GoldCoinEffect GetGoldCoinEffect()
    {
        this.goldCoinEffects ??= new Queue<GoldCoinEffect>();

        this.goldCoinEffects.TryDequeue(out GoldCoinEffect effect);
        effect ??= new GoldCoinEffect(this.uiEffectViewTran, GoldCoinEffectCallback);

        return effect;
    }
    void GoldCoinEffectCallback(GoldCoinEffect effect)
    {
        this.goldCoinEffects ??= new Queue<GoldCoinEffect>();

        this.goldCoinEffects.Enqueue(effect);
    }

    // 播放击杀怪物得分飘字效果
    public void ShowKillScoreEffect(int player, int score, Vector3 pos)
    {
        if (score <= 0)
            return;

        pos = CameraController.Instance.MainCamera.WorldToScreenPoint(pos);
        pos.z = 0;

        UIEffectMgr.Instance.ShowGoldCoinEffect(player, score, pos);
    }
    public void ShowKillScoreEffectReal(int player, int score, Vector3 pos)
    {
        string prefab = KillScoreEffectPrefab[player];
        KillScoreEffect effect = GetKillScoreEffect(prefab);
        effect.Start(pos, score);
    }

    // 播放击杀怪物获得道具飞行效果(1血量补给；2限时翻倍；3散弹枪；4火箭炮)
    public void ShowPropFlyEffect(int player, int propType, Vector3 startPos, Vector3 endPos, Action<int, int> callback)
    {
        int languageIndex = MachineDataMgr.Instance.IsChineseLanguageVersion ? 0 : 1;
        string prefab = PropFlyEffectPrefab[propType - 1];

        PropFlyEffect effect = GetPropFlyEffect(prefab);
        effect.Start(player, propType, startPos, endPos, PropFlyEffectSprite[propType - 1, languageIndex], callback);
    }


    // 播放击杀怪物得分飘字效果
    public void ShowGoldCoinEffect(int player, int score, Vector3 pos)
    {
        GoldCoinEffect effect = GetGoldCoinEffect();

        effect.Start(player, score, pos);
    }
}
