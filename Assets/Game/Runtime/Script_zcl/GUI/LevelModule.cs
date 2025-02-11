using DG.Tweening;
using System.Collections;
using UnA.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelModule : UIModuleBase
{
    private GameObject gameLevelTime, gameLevelTarget;
    private Transform gameLevelTimeParent, gameLevelTargetParent;
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
        scene = 1;
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveListener(BackendPanel.APPLYFUNCTION, Apply);
    }

    #region Event

    private void Apply(string name, object obj)
    {

        if (gameObject.activeSelf == false) { Debug.Log($"{gameObject.name} 不被更新"); return; }

        for (int i = 0; i < gameLevelTimeParent.childCount; i++)
        {
            Transform go = gameLevelTimeParent.GetChild(i);
            MachineDataMgr.Instance.SetLevelTime(scene, i + 1, int.Parse(go.transform.Find("gameLevelTimevalue_InputField").GetComponent<InputField>().text));
        }

        for (int i = 0; i < gameLevelTargetParent.childCount; i++)
        {
            Transform go = gameLevelTargetParent.GetChild(i);
            MachineDataMgr.Instance.SetLevelTargetMonsterCount(scene, i + 1, int.Parse(go.transform.Find("gameLevelTargetvalue01_InputField").GetComponent<InputField>().text)); //数量
            MachineDataMgr.Instance.SetLevelTargetScore(scene, i + 1, int.Parse(go.transform.Find("gameLevelTargetvalue02_InputField").GetComponent<InputField>().text)); //分数
        }


    }

    #endregion

    public override IEnumerator AwakeInit()
    {
        #region Level Scene leftArrow_btn
        LevelScene(transform.Find("LevelScene/leftArrow_btn").GetComponent<Button>(), transform.Find("LevelScene/LevelScenevalue_InputField").GetComponent<InputField>(), 0);
        LevelScene(transform.Find("LevelScene/rightArrow_btn").GetComponent<Button>(), transform.Find("LevelScene/LevelScenevalue_InputField").GetComponent<InputField>(), 1);
        #endregion

        #region bgTime
        gameLevelTime = Resources.Load<GameObject>("GUIPrefabs/dynamicsItem/GameLevelTime");
        gameLevelTarget = Resources.Load<GameObject>("GUIPrefabs/dynamicsItem/GameLevelTarget");
        gameLevelTimeParent = transform.Find("bgTime/GameLevelTimeView/Viewport/Content");
        for (int i = 0; i < MachineDataMgr.MaxLevelCount; i++)
        {
            GameObject go = GameObject.Instantiate(gameLevelTime, gameLevelTimeParent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.transform.Find("gamelevelName_text").GetComponent<Text>().text = $"第 {i + 1} 关卡";
            onClickLevelTime(go.transform.Find("leftArrow_btn").GetComponent<Button>(), go.transform.Find("gameLevelTimevalue_InputField").GetComponent<InputField>(), 0);
            onClickLevelTime(go.transform.Find("rightArrow_btn").GetComponent<Button>(), go.transform.Find("gameLevelTimevalue_InputField").GetComponent<InputField>(), 1);
        }

        yield return null;
        #endregion

        #region bgTarget

        gameLevelTargetParent = transform.Find("bgTarget/GameLevelTargetView/Viewport/Content");
        for (int i = 0; i < MachineDataMgr.MaxLevelCount; i++)
        {
            GameObject go = GameObject.Instantiate(gameLevelTarget, gameLevelTargetParent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.transform.Find("gamelevelName_text").GetComponent<Text>().text = $"第 {i + 1} 关卡";
            onClickLevelTarget(go.transform.Find("leftArrow01_btn").GetComponent<Button>(), go.transform.Find("gameLevelTargetvalue01_InputField").GetComponent<InputField>(), 0, 5);
            onClickLevelTarget(go.transform.Find("rightArrow01_btn").GetComponent<Button>(), go.transform.Find("gameLevelTargetvalue01_InputField").GetComponent<InputField>(), 1, 5);
            onClickLevelTarget(go.transform.Find("leftArrow02_btn").GetComponent<Button>(), go.transform.Find("gameLevelTargetvalue02_InputField").GetComponent<InputField>(), 0, 100);
            onClickLevelTarget(go.transform.Find("rightArrow02_btn").GetComponent<Button>(), go.transform.Find("gameLevelTargetvalue02_InputField").GetComponent<InputField>(), 1, 100);
        }
        yield return null;
        #endregion
        LanguageUpdate(MachineDataMgr.Instance.IsChineseLanguageVersion);
        gameLevelTimeParent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        gameLevelTargetParent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        
        gameObject.SetActive(false);
    }

    public override void LanguageUpdate(bool IsChinese)
    {
        if (IsChinese)
        {
            #region bgTime

            transform.Find("bgTime/GameLevelTimeView/GameLevelTimeTitle_text").GetComponent<Text>().text = "关卡时间";
            transform.Find("bgTime/GameLevelTimeView/GameLevelTimeTip_text").GetComponent<Text>().text = "注：关卡时间累计总和必需等时间总游戏";
            for (int i = 0; i < gameLevelTimeParent.childCount; i++)
            {
                GameObject go = gameLevelTimeParent.GetChild(i).gameObject;
                go.transform.Find("gamelevelName_text").GetComponent<Text>().text = $"第 {i + 1} 关卡";
                go.transform.Find("gameLevelTimevalueicon_text").GetComponent<Text>().text = $"秒";
            }

            #endregion

            #region bgTarget

            transform.Find("bgTarget/GameLevelTargetView/GameLevelTargetTitle_text").GetComponent<Text>().text = "击杀目标";
            transform.Find("bgTarget/GameLevelTargetView/GameLevelTargetTip_text").GetComponent<Text>().text = "注：击杀目标建议使用默认值";
            for (int i = 0; i < gameLevelTargetParent.childCount; i++)
            {
                GameObject go = gameLevelTargetParent.GetChild(i).gameObject;
                go.transform.Find("gamelevelName_text").GetComponent<Text>().text = $"第 {i + 1} 关卡";
                go.transform.Find("gameLevelTargetvalueicon01_text").GetComponent<Text>().text = $"只";
                go.transform.Find("gameLevelTargetvalueicon02_text").GetComponent<Text>().text = $"分";
            }

            #endregion

            transform.Find("LevelScene/LevelScene_text").GetComponent<Text>().text = "关卡";

        }
        else
        {
            #region bgTime

            transform.Find("bgTime/GameLevelTimeView/GameLevelTimeTitle_text").GetComponent<Text>().text = "Level time";
            transform.Find("bgTime/GameLevelTimeView/GameLevelTimeTip_text").GetComponent<Text>().text = "Note: The total accumulated time of each level must be equal to the total game time";
            for (int i = 0; i < gameLevelTimeParent.childCount; i++)
            {
                GameObject go = gameLevelTimeParent.GetChild(i).gameObject;
                go.transform.Find("gamelevelName_text").GetComponent<Text>().text = $"Level {i + 1}";
                go.transform.Find("gameLevelTimevalueicon_text").GetComponent<Text>().text = $"second";
            }

            #endregion

            #region bgTarget

            transform.Find("bgTarget/GameLevelTargetView/GameLevelTargetTitle_text").GetComponent<Text>().text = "Kill the target";
            transform.Find("bgTarget/GameLevelTargetView/GameLevelTargetTip_text").GetComponent<Text>().text = "Note: It is recommended to use the default values when killing targets";
            for (int i = 0; i < gameLevelTargetParent.childCount; i++)
            {
                GameObject go = gameLevelTargetParent.GetChild(i).gameObject;
                go.transform.Find("gamelevelName_text").GetComponent<Text>().text = $"Level {i + 1}";
                go.transform.Find("gameLevelTargetvalueicon01_text").GetComponent<Text>().text = $"only";
                go.transform.Find("gameLevelTargetvalueicon02_text").GetComponent<Text>().text = $"divide";
            }

            #endregion

            transform.Find("LevelScene/LevelScene_text").GetComponent<Text>().text = "Level";
        }
    }

    private void LevelScene(Button btn, InputField text, int index)
    {
        btn.onClick.AddListener(() =>
        {

            scene = int.Parse(text.text);
            if (index == 0)
            {
                if (scene > 1)
                {
                    scene -= 1;
                    text.text = scene.ToString();
                }
            }
            else if (index == 1)
            {
                if (scene < 3)
                {
                    scene += 1;
                    text.text = scene.ToString();
                }
            }
            OnUpdateGUIData(scene);
        });
    }

    /// <summary>
    /// 关卡时间
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="text"></param>
    /// <param name="index">0---减  1---加</param>
    private void onClickLevelTime(Button btn, InputField text, int index)
    {
        btn.onClick.AddListener(() =>
        {
            int time = int.Parse(text.text);
            if (index == 0)
            {
                if (time > 10)
                {
                    time -= 5;
                    text.text = time.ToString();
                }
            }
            else if (index == 1)
            {
                if (time < 600)
                {
                    time += 5;
                    text.text = time.ToString();
                }
            }

        });
    }

    /// <summary>
    /// 关卡数量跟得分
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="text"></param>
    /// <param name="index">0---减  1---加</param>
    /// <param name="type">5---数量  100---得分</param>
    private void onClickLevelTarget(Button btn, InputField text, int index, int type)
    {
        btn.onClick.AddListener(() =>
        {
            int value = int.Parse(text.text);
            if (index == 0) //减
            {
                if (type == 5) //数量
                {
                    if (value > 10)
                    {
                        value -= 5;
                        text.text = value.ToString();
                    }
                }
                else if (type == 100) //得分
                {
                    if (value >= 200)
                    {
                        value -= 100;
                        text.text = value.ToString();
                    }
                }
            }
            else if (index == 1) //加
            {
                if (type == 5) //数量
                {
                    if (value < 100)
                    {
                        value += 5;
                        text.text = value.ToString();
                    }
                }
                else if (type == 100) //得分
                {
                    if (value < 10000)
                    {
                        value += 100;
                        text.text = value.ToString();
                    }
                }
            }
        });
    }


    public override void OnUpdateGUIData(int scene)
    {
        for (int i = 0; i < gameLevelTimeParent.childCount; i++)
        {
            Transform go = gameLevelTimeParent.GetChild(i);
            go.transform.Find("gameLevelTimevalue_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.GetLevelTime(scene, i + 1).ToString();
        }

        for (int i = 0; i < gameLevelTargetParent.childCount; i++)
        {
            Transform go = gameLevelTargetParent.GetChild(i);
            go.transform.Find("gameLevelTargetvalue01_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.GetLevelTargetMonsterCount(scene, i + 1).ToString(); //数量
            go.transform.Find("gameLevelTargetvalue02_InputField").GetComponent<InputField>().text = MachineDataMgr.Instance.GetLevelTargetScore(scene, i + 1).ToString(); //分数
        }
    }


    public override void OnEnter()
    {
        base.OnEnter();
        gameLevelTimeParent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        gameLevelTargetParent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.DOLocalMoveY(1000, 0);
        OnUpdateGUIData(scene);
        gameObject.SetActive(true);
        transform.DOLocalMoveY(0, 2f).SetEase(Ease.InBack).OnComplete(() => { });
    }

    public override void OnExit()
    {
        base.OnExit();
        transform.DOLocalMoveY(-1000, 2f).SetEase(Ease.InBack).OnComplete(() => { gameObject.SetActive(false); });
    }
}
