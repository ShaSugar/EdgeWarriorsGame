using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnA.Base;
using UnityEngine;
using UnityEngine.UI;

public class MonsterModule : UIModuleBase
{
    public GameObject gameMonsterHPOrAttack;
    private Transform gameMonsterHPOrAttackParent;

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

        for (int i = 0; i < gameMonsterHPOrAttackParent.childCount; i++)
        {
            Transform go = gameMonsterHPOrAttackParent.GetChild(i);
            ERealUnitType erealUnitType = (ERealUnitType)(i + 1);
            MachineDataMgr.Instance.SetUnitHP(erealUnitType, int.Parse(go.transform.Find("gameMonsterHPvalue_InputField").GetComponent<InputField>().text)); //hp
            if (i >= gameMonsterHPOrAttackParent.childCount - 1) { continue; }
            MachineDataMgr.Instance.SetUnitATT(erealUnitType, int.Parse(go.transform.Find("gameMonsterAttackvalue_InputField").GetComponent<InputField>().text)); //attack

        }


    }
    #endregion


    public override IEnumerator AwakeInit()
    {
        gameMonsterHPOrAttack = Resources.Load<GameObject>("GUIPrefabs/dynamicsItem/GameMonsterHPOrAttack");
        gameMonsterHPOrAttackParent = transform.Find("bgMonster/GameLevelMonsterView/Viewport/Content");
        for (int i = 0; i < 8; i++)
        {
            GameObject go = GameObject.Instantiate(gameMonsterHPOrAttack, gameMonsterHPOrAttackParent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.transform.Find("gameMonsterName_text").GetComponent<Text>().text = getMonsterName(i + 1);
            onClickMonsterTarget(go.transform.Find("leftArrowHP_btn").GetComponent<Button>(), go.transform.Find("gameMonsterHPvalue_InputField").GetComponent<InputField>(), 0, 5);
            onClickMonsterTarget(go.transform.Find("rightArrowHP_btn").GetComponent<Button>(), go.transform.Find("gameMonsterHPvalue_InputField").GetComponent<InputField>(), 1, 5);

            if (i == 5)
            {
                Destroy(go.transform.Find("leftArrowAttack_btn").gameObject);
                Destroy(go.transform.Find("rightArrowAttack_btn").gameObject);
                Destroy(go.transform.Find("gameMonsterAttackvalue_InputField").gameObject);
                Destroy(go.transform.Find("gameMonsterAttackvalueicon_text").gameObject);
            }
            else
            {
                onClickMonsterTarget(go.transform.Find("leftArrowAttack_btn").GetComponent<Button>(), go.transform.Find("gameMonsterAttackvalue_InputField").GetComponent<InputField>(), 0, 100);
                onClickMonsterTarget(go.transform.Find("rightArrowAttack_btn").GetComponent<Button>(), go.transform.Find("gameMonsterAttackvalue_InputField").GetComponent<InputField>(), 1, 100);
            }
        }
        yield return null;
        LanguageUpdate(MachineDataMgr.Instance.IsChineseLanguageVersion);
        gameObject.SetActive(false);
    }

    public override void LanguageUpdate(bool IsChinese)
    {
        if (IsChinese)
        {
            transform.Find("bgMonster/GameLevelMonsterView/GameLevelHPOrAttackTitle_text").GetComponent<Text>().text = "血值 / 攻击值";
            transform.Find("bgMonster/GameLevelMonsterView/GameLevelHPOrAttackTip_text").GetComponent<Text>().text = "注：关卡时间累计总和必需等时间总游戏";
            for (int i = 0; i < gameMonsterHPOrAttackParent.childCount; i++)
            {
                GameObject go = gameMonsterHPOrAttackParent.GetChild(i).gameObject;
                go.transform.Find("gameMonsterName_text").GetComponent<Text>().text = getMonsterName(i + 1);
            }

        }
        else
        {
            transform.Find("bgMonster/GameLevelMonsterView/GameLevelHPOrAttackTitle_text").GetComponent<Text>().text = "Blood value/attack value";
            transform.Find("bgMonster/GameLevelMonsterView/GameLevelHPOrAttackTip_text").GetComponent<Text>().text = "Note: The total accumulated time of each level must be equal to the total game time";
            for (int i = 0; i < gameMonsterHPOrAttackParent.childCount; i++)
            {
                GameObject go = gameMonsterHPOrAttackParent.GetChild(i).gameObject;
                go.transform.Find("gameMonsterName_text").GetComponent<Text>().text = getMonsterNameLanguage(i + 1);
            }
        }
    }

    /// <summary>
    /// 怪物血值、攻击值
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="text"></param>
    /// <param name="index">0---减  1---加</param>
    /// <param name="type">5---HP  100---ATT</param>
    private void onClickMonsterTarget(Button btn, InputField text, int index, int type)
    {
        btn.onClick.AddListener(() =>
        {
            int value = int.Parse(text.text);
            if (index == 0) //减
            {
                if (type == 5) //hp
                {
                    if (value > 1)
                    {
                        value -= 1;
                        text.text = value.ToString();
                    }
                }
                else if (type == 100) //attack
                {
                    if (value > 2)
                    {
                        value -= 2;
                        text.text = value.ToString();
                    }
                }
            }
            else if (index == 1) //加
            {
                if (type == 5) //hp
                {
                    if (value < 100)
                    {
                        value += 1;
                        text.text = value.ToString();
                    }
                }
                else if (type == 100) //attack
                {
                    if (value < 100)
                    {
                        value += 2;
                        text.text = value.ToString();
                    }
                }
            }
        });
    }

    private string getMonsterName(int index)
    {
        string name = string.Empty;
        switch (index)
        {
            case (int)ERealUnitType.EYu:
                name = "鳄鱼";
                break;
            case (int)ERealUnitType.FeiLong:
                name = "飞龙";
                break;
            case (int)ERealUnitType.ZongXiong:
                name = "棕熊";
                break;
            case (int)ERealUnitType.HaiDao:
                name = "海盗";
                break;
            case (int)ERealUnitType.Boss:
                name = "Boss";
                break;
            case (int)ERealUnitType.BossHitPoint:
                name = "Boss击打点";
                break;
            case (int)ERealUnitType.Monster100:
                name = "怪物100";
                break;
            case (int)ERealUnitType.Monster101:
                name = "怪物101";
                break;
            case (int)ERealUnitType.MAX:
                break;
        }
        return name;
    }
    private string getMonsterNameLanguage(int index)
    {
        string name = string.Empty;
        switch (index)
        {
            case (int)ERealUnitType.EYu:
                name = "crocodile";
                break;
            case (int)ERealUnitType.FeiLong:
                name = "Flying Dragon";
                break;
            case (int)ERealUnitType.ZongXiong:
                name = "brown bear";
                break;
            case (int)ERealUnitType.HaiDao:
                name = "pirate";
                break;
            case (int)ERealUnitType.Boss:
                name = "Boss";
                break;
            case (int)ERealUnitType.BossHitPoint:
                name = "Boss hits the dot";
                break;
            case (int)ERealUnitType.Monster100:
                name = "Monster100";
                break;
            case (int)ERealUnitType.Monster101:
                name = "Monster101";
                break;
            case (int)ERealUnitType.MAX:
                break;
        }
        return name;
    }

    public override void OnUpdateGUIData(int scene = 1)
    {
        for (int i = 0; i < gameMonsterHPOrAttackParent.childCount; i++)
        {
            Transform go = gameMonsterHPOrAttackParent.GetChild(i);
            ERealUnitType erealUnitType = (ERealUnitType)(i + 1);
            go.transform.Find("gameMonsterHPvalue_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.GetUnitHP(erealUnitType).ToString(); //hp
            if (i == 5)
            {

            }
            else
            {
                go.transform.Find("gameMonsterAttackvalue_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.GetUnitATT(erealUnitType).ToString(); //attack
            }
        }
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
