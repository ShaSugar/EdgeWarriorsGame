using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnA.Base;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PropModule : UIModuleBase
{
    public GameObject gameProp;
    private Transform gamePropParent;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        EventMgr.Instance.AddListener(BackendPanel.APPLYFUNCTION, Apply);
        EventMgr.Instance.AddListener(GameModule.SetGameLanguage, (_, obj) =>
        {
            LanguageUpdate((bool)obj);
        });
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveListener(BackendPanel.APPLYFUNCTION, Apply);
    }

    #region Event

    private void Apply(string name, object obj)
    {

        if (gameObject.activeSelf == false) { Debug.Log($"{gameObject.name} 不被更新"); return; }
        MachineDataMgr.Instance.HpSupply = int.Parse(gamePropParent.GetChild(0).transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text);
        MachineDataMgr.Instance.ScoreDouble = int.Parse(gamePropParent.GetChild(1).transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text);
        MachineDataMgr.Instance.ShellGun = int.Parse(gamePropParent.GetChild(2).transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text);
        MachineDataMgr.Instance.BoltCannon = int.Parse(gamePropParent.GetChild(3).transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text);


        //for (int i = 0; i < gamePropParent.childCount; i++)
        //{
        //    Transform go = gamePropParent.GetChild(i);
        //    EItemType eItemType = (EItemType)(i + 1);
        //    MachineDataMgr.Instance.SetDropItemWeight(eItemType, int.Parse(go.transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text)).ToString();
        //}


    }
    #endregion

    public override IEnumerator AwakeInit()
    {
        gameProp = Resources.Load<GameObject>("GUIPrefabs/dynamicsItem/GameProp");
        gamePropParent = transform.Find("bgProp/GamePropView/Viewport/Content");
        for (int i = 0; i < 4; i++)
        {
            GameObject go = GameObject.Instantiate(gameProp, gamePropParent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            (go.transform.Find("gamePropName_text").GetComponent<Text>().text, go.transform.Find("gamePropvalueicon_text").GetComponent<Text>().text) = getPropName(i + 1); 
            onClickProp(go.transform.Find("leftArrow_btn").GetComponent<Button>(), go.transform.Find("gamePropvalue_InputField").GetComponent<InputField>(), 0);
            onClickProp(go.transform.Find("rightArrow_btn").GetComponent<Button>(), go.transform.Find("gamePropvalue_InputField").GetComponent<InputField>(), 1);
        }
        yield return null;
        LanguageUpdate(MachineDataMgr.Instance.IsChineseLanguageVersion);
        gameObject.SetActive(false);
    }

    public override void LanguageUpdate(bool IsChinese)
    {
        if (IsChinese)
        {
            transform.Find("bgProp/GamePropView/GamePropTitle_text").GetComponent<Text>().text = "游戏道具";
            for (int i = 0; i < gamePropParent.childCount; i++)
            {
                GameObject go = gamePropParent.GetChild(i).gameObject;
                (go.transform.Find("gamePropName_text").GetComponent<Text>().text, go.transform.Find("gamePropvalueicon_text").GetComponent<Text>().text) = getPropName(i + 1);
            }
        }
        else
        {
            transform.Find("bgProp/GamePropView/GamePropTitle_text").GetComponent<Text>().text = "Game props";
            for (int i = 0; i < gamePropParent.childCount; i++)
            {
                GameObject go = gamePropParent.GetChild(i).gameObject;
                (go.transform.Find("gamePropName_text").GetComponent<Text>().text, go.transform.Find("gamePropvalueicon_text").GetComponent<Text>().text) = getPropNameLanguage(i + 1);
            }
        }
    }

    /// <summary>
    /// 道具
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="text"></param>
    /// <param name="index">0---减  1---加</param>
    private void onClickProp(Button btn, InputField text, int index)
    {
        btn.onClick.AddListener(() =>
        {
            int time = int.Parse(text.text);
            if (index == 0)
            {
                if (time > 1)
                {
                    time -= 1;
                    text.text = time.ToString();
                }
            }
            else if (index == 1)
            {
                if (time < 100)
                {
                    time += 1;
                    text.text = time.ToString();
                }
            }

        });
    }

    private (string,string) getPropName(int index)
    {
        string name = string.Empty;
        string icon = string.Empty;
        switch (index)
        {
            case (int)EItemType.HP_SUPPLY:
                name = "血量补给";
                icon = "hp";
                break;
            case (int)EItemType.SCORE_DOUBLE:
                name = "积分翻倍";
                icon = "倍";
                break;
            case (int)EItemType.SHELL_GUN:
                name = "散弹枪";
                icon = "数量";
                break;
            case (int)EItemType.BOLT_CANNON:
                name = "火箭炮";
                icon = "数量";
                break;

        }
        return (name,icon);
    }
    private (string, string) getPropNameLanguage(int index)
    {
        string name = string.Empty;
        string icon = string.Empty;
        switch (index)
        {
            case (int)EItemType.HP_SUPPLY:
                name = "Blood supply";
                icon = "hp";
                break;
            case (int)EItemType.SCORE_DOUBLE:
                name = "Double points";
                icon = "double";
                break;
            case (int)EItemType.SHELL_GUN:
                name = "Shotgun";
                icon = "number";
                break;
            case (int)EItemType.BOLT_CANNON:
                name = "Rocket launcher";
                icon = "number";
                break;

        }
        return (name, icon);
    }


    public override void OnUpdateGUIData(int scene = 1)
    {
        gamePropParent.GetChild(0).transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.HpSupply.ToString();
        gamePropParent.GetChild(1).transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.ScoreDouble.ToString();
        gamePropParent.GetChild(2).transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.ShellGun.ToString();
        gamePropParent.GetChild(3).transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.BoltCannon.ToString();


        //for (int i = 0; i < gamePropParent.childCount; i++)
        //{
        //    Transform go = gamePropParent.GetChild(i);
        //    EItemType eItemType = (EItemType)(i + 1);
        //    go.transform.Find("gamePropvalue_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.GetDropItemWeight(eItemType).ToString();
        //}
    }


    public override void OnEnter()
    {
        base.OnEnter();
        transform.DOLocalMoveY(1000, 0);
        OnUpdateGUIData();
        gameObject.SetActive(true);
        transform.DOLocalMoveY(0, 2f).SetEase(Ease.InBack).OnComplete(() => { });
    }

    public override void OnExit()
    {
        base.OnExit();
        transform.DOLocalMoveY(-1000, 2f).SetEase(Ease.InBack).OnComplete(() => { gameObject.SetActive(false); });
    }

}