using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


public class UnitEffectMgr : Singleton<UnitEffectMgr>
{
    class UnitDieEffect
    {
        string prefab;
        GameObject obj;
        Transform tran;
        Action<string, UnitDieEffect> onDieCallback;

        public UnitDieEffect(string prefab, Action<string, UnitDieEffect> onDie, Transform parent)
        {
            this.prefab = prefab;
            this.onDieCallback = onDie;

            this.prefab = prefab;
            var nodePrefab = ResMgr.Instance.LoadAssetSync<GameObject>(prefab);
            this.obj = Object.Instantiate(nodePrefab);
            this.obj.SetActive(false);
            this.tran = this.obj.transform;
            this.tran.SetParent(parent);
            this.tran.localPosition = Vector3.zero;
            this.tran.localEulerAngles = Vector3.zero;
            this.tran.localScale = Vector3.one;
            this.obj.SetActive(false);
        }
        public void Show(Vector3 pos)
        {
            this.tran.position = pos;
            this.obj.SetActive(true);
            TimerMgr.Instance.ScheduleOnce((_) =>
            {
                Hide();
            }, 2f);
        }
        void Hide()
        {
            this.obj.SetActive(false);
            this.onDieCallback?.Invoke(this.prefab, this);
        }

        public void Destroy()
        {
            onDieCallback = null;
            Object.DestroyImmediate(this.obj);
        }
    }

    Dictionary<string, Stack<UnitDieEffect>> unitDieEffectDic = new Dictionary<string, Stack<UnitDieEffect>>();

    public void DestroyAllEffect()
    {
        foreach (UnitDieEffect e in unitDieEffectDic.Values.SelectMany(effect => effect))
        {
            e.Destroy();
        }
        unitDieEffectDic.Clear();
    }

    public void ShowDieEffect(string prefab, Vector3 pos, Transform parent)
    {
        UnitDieEffect effect;
        if (unitDieEffectDic.ContainsKey(prefab) && unitDieEffectDic[prefab].Count > 0)
        {
            effect = unitDieEffectDic[prefab].Pop();
        }
        else
        {
            effect = new UnitDieEffect(prefab, RecycleUnitEffect, parent);
        }

        effect.Show(pos);
    }
    void RecycleUnitEffect(string prefab, UnitDieEffect effect)
    {
        if (!unitDieEffectDic.ContainsKey(prefab))
            unitDieEffectDic.Add(prefab, new Stack<UnitDieEffect>());

        unitDieEffectDic[prefab].Push(effect);
    }
}
