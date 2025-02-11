using System.Collections;
using System.Collections.Generic;
using UnA.Manager;
using UnityEngine;
using UnityEngine.UI;

public class BackendPanel : MonoBehaviour
{
    public const string QUITBACKEND = "QUITBACKEND"; //退出
    public const string APPLYFUNCTION = "APPLYFUNCTION"; //应用
    public Transform highlightIcon;
    public Button game_btn, level_btn, monster_btn, prop_btn, accounts_btn;
    public Button ClearingAccountsToZero_btn, Apply_btn, Quit_btn;
    private Vector3 moveHighlightPos;
    private void Awake()
    {
        moveHighlightPos = new Vector3(5, -10, 0);
        game_btn.onClick.AddListener(() =>
        {
            if (MoveHighlightPos(game_btn.transform))
            {
                UIManager.Instance.PopUI();
                UIManager.Instance.PushUI("GameModule");
            }

        });

        level_btn.onClick.AddListener(() =>
        {
            if (MoveHighlightPos(level_btn.transform))
            {
                UIManager.Instance.PopUI();
                UIManager.Instance.PushUI("LevelModule");
            }
        });

        monster_btn.onClick.AddListener(() =>
        {
            if (MoveHighlightPos(monster_btn.transform))
            {
                UIManager.Instance.PopUI();
                UIManager.Instance.PushUI("MonsterModule");
            }

        });

        prop_btn.onClick.AddListener(() =>
        {
            if (MoveHighlightPos(prop_btn.transform))
            {
                UIManager.Instance.PopUI();
                UIManager.Instance.PushUI("PropModule");
            }
        });

        accounts_btn.onClick.AddListener(() =>
        {
            if (MoveHighlightPos(accounts_btn.transform))
            {
                UIManager.Instance.PopUI();
                UIManager.Instance.PushUI("AccountsModule");
            }
        });

        //账目清零
        ClearingAccountsToZero_btn.onClick.AddListener(() =>
        {

        });

        //应用
        Apply_btn.onClick.AddListener(() =>
        {
            EventMgr.Instance.Emit(APPLYFUNCTION, null);
        });

        //退出
        Quit_btn.onClick.AddListener(() =>
        {
            //EventMgr.Instance.Emit(QUITBACKEND,null);
            Object.Destroy(this.transform.parent.gameObject);
        });

        EventMgr.Instance.AddListener(GameModule.SetGameLanguage, (_, obj) =>
        {
            LanguageUpdate((bool)obj);
        });

        LanguageUpdate(MachineDataMgr.Instance.IsChineseLanguageVersion);
    }

    public void LanguageUpdate(bool IsChinese)
    {
        try
        {
            if (IsChinese)
            {
                transform.Find("leftOption/title").GetComponent<Text>().text = "游戏设置";
                game_btn.GetComponentInChildren<Text>().text = "游戏";
                level_btn.GetComponentInChildren<Text>().text = "关卡";
                monster_btn.GetComponentInChildren<Text>().text = "怪物";
                prop_btn.GetComponentInChildren<Text>().text = "道具";
                accounts_btn.GetComponentInChildren<Text>().text = "账目";
                ClearingAccountsToZero_btn.GetComponentInChildren<Text>().text = "账目清零";
                Apply_btn.GetComponentInChildren<Text>().text = "应用";
                Quit_btn.GetComponentInChildren<Text>().text = "退出";

            }
            else
            {
                transform.Find("leftOption/title").GetComponent<Text>().text = "Game Options";
                game_btn.GetComponentInChildren<Text>().text = "Game";
                level_btn.GetComponentInChildren<Text>().text = "Level";
                monster_btn.GetComponentInChildren<Text>().text = "Monste";
                prop_btn.GetComponentInChildren<Text>().text = "Prop";
                accounts_btn.GetComponentInChildren<Text>().text = "Accounts";
                ClearingAccountsToZero_btn.GetComponentInChildren<Text>().text = "Accounts Clear";
                Apply_btn.GetComponentInChildren<Text>().text = "Apply";
                Quit_btn.GetComponentInChildren<Text>().text = "Quit";
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        
    }


    public void OpenBackend()
    {
        gameObject.SetActive(true);
        MoveHighlightPos(game_btn.transform);
        UIManager.Instance.PushUI("GameModule");
    }

    public void CloseBackend()
    {
        if (MoveHighlightPosState(game_btn.transform))
        {
            UIManager.Instance.PopUI();
        }
        gameObject.SetActive(false);
    }


    private bool MoveHighlightPos(Transform parent)
    {
        if (highlightIcon.parent == parent)
        {
            return false;
        }
        else
        {
            highlightIcon.SetParent(parent);
            highlightIcon.localPosition = moveHighlightPos;
            return true;    
        }
    }

    private bool MoveHighlightPosState(Transform parent)
    {
        if (highlightIcon.parent == parent)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}
