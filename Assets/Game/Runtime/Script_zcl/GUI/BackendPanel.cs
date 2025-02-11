using System.Collections;
using System.Collections.Generic;
using UnA.Manager;
using UnityEngine;
using UnityEngine.UI;

public class BackendPanel : MonoBehaviour
{
    public const string QUITBACKEND = "QUITBACKEND"; //�˳�
    public const string APPLYFUNCTION = "APPLYFUNCTION"; //Ӧ��
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

        //��Ŀ����
        ClearingAccountsToZero_btn.onClick.AddListener(() =>
        {

        });

        //Ӧ��
        Apply_btn.onClick.AddListener(() =>
        {
            EventMgr.Instance.Emit(APPLYFUNCTION, null);
        });

        //�˳�
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
                transform.Find("leftOption/title").GetComponent<Text>().text = "��Ϸ����";
                game_btn.GetComponentInChildren<Text>().text = "��Ϸ";
                level_btn.GetComponentInChildren<Text>().text = "�ؿ�";
                monster_btn.GetComponentInChildren<Text>().text = "����";
                prop_btn.GetComponentInChildren<Text>().text = "����";
                accounts_btn.GetComponentInChildren<Text>().text = "��Ŀ";
                ClearingAccountsToZero_btn.GetComponentInChildren<Text>().text = "��Ŀ����";
                Apply_btn.GetComponentInChildren<Text>().text = "Ӧ��";
                Quit_btn.GetComponentInChildren<Text>().text = "�˳�";

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
