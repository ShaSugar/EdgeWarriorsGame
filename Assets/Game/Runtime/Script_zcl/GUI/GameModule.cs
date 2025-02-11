using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnA.Base;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class GameModule : UIModuleBase
{
    public const string SetGameLanguage = "GameModule_SetGameLanguage";

    #region GameSound

    //private Button sound_leftArrow_btn,sound_rightArrow_btn;
    private InputField gameSoundvalue_inputField;
    public float temp_soundValue = 0;

    #endregion

    #region GameNumber

    //private Button number_leftArrow_btn, number_rightArrow_btn;
    private InputField gameNumbervalue_inputField;
    public int temp_numberValue = 4;

    #endregion



    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        EventMgr.Instance.AddListener(BackendPanel.APPLYFUNCTION, Apply);
        EventMgr.Instance.AddListener(SetGameLanguage, (_,obj) =>
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
        float sound = temp_soundValue / 100;
        MachineDataMgr.Instance.SetSoundVolume(sound);
        MachineDataMgr.Instance.SetMusicVolume(sound);
        MachineDataMgr.Instance.SetPlayerShowCount(temp_numberValue); //更新人数

    }

    #endregion

    public override IEnumerator AwakeInit()
    {
        #region GameSound

        gameSoundvalue_inputField = transform.Find("bg/functionView/GameSound/gameSoundvalue_InputField").GetComponent<InputField>();
        transform.Find("bg/functionView/GameSound/leftArrow_btn").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (temp_soundValue > 10)
            {
                temp_soundValue -= 10;
                gameSoundvalue_inputField.text = temp_soundValue.ToString();
            }
        });

        transform.Find("bg/functionView/GameSound/rightArrow_btn").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (temp_soundValue < 100)
            {
                temp_soundValue += 10;
                gameSoundvalue_inputField.text = temp_soundValue.ToString();
            }
        });
        yield return null;

        #endregion

        #region GameNumber

        gameNumbervalue_inputField = transform.Find("bg/functionView/GameNumber/gameNumbervalue_InputField").GetComponent<InputField>();
        transform.Find("bg/functionView/GameNumber/leftArrow_btn").GetComponent<Button>().onClick.AddListener(() =>
        {
            temp_numberValue = 2;
            gameNumbervalue_inputField.text = temp_numberValue.ToString();
        });

        transform.Find("bg/functionView/GameNumber/rightArrow_btn").GetComponent<Button>().onClick.AddListener(() =>
        {
            temp_numberValue = 4;
            gameNumbervalue_inputField.text = temp_numberValue.ToString();
        });
        yield return null;

        #endregion

        #region GameLanguage

        transform.Find("bg/functionView/GameLanguage/LanguageToggle").GetComponent<Toggle>().isOn = MachineDataMgr.Instance.IsChineseLanguageVersion;
        transform.Find("bg/functionView/GameLanguage/LanguageToggle").GetComponent<Toggle>().onValueChanged.AddListener((toggle) =>
        {
            EventMgr.Instance.Emit(SetGameLanguage, toggle);
            MachineDataMgr.Instance.IsChineseLanguageVersion = toggle;
        });

        yield return null;

        #endregion
        LanguageUpdate(MachineDataMgr.Instance.IsChineseLanguageVersion);
        gameObject.SetActive(false);
    }

    public override void LanguageUpdate(bool IsChinese)
    {
        if (IsChinese)
        {
            transform.Find("bg/functionView/GameSound/gameSound_text").GetComponent<Text>().text = "游戏音量";
            transform.Find("bg/functionView/GameNumber/gameNumber_text").GetComponent<Text>().text = "游戏人数";
            transform.Find("bg/functionView/GameNumber/gameNumbervalueicon_text").GetComponent<Text>().text = "人";
            transform.Find("bg/functionView/GameLanguage/gameLanguage_text").GetComponent<Text>().text = "语言设置";
            transform.Find("bg/functionView/GameLanguage/gameNumbervalueicon_text").GetComponent<Text>().text = "中文";
        }
        else
        {
            transform.Find("bg/functionView/GameSound/gameSound_text").GetComponent<Text>().text = "Game Volume";
            transform.Find("bg/functionView/GameNumber/gameNumber_text").GetComponent<Text>().text = "Number of players";
            transform.Find("bg/functionView/GameNumber/gameNumbervalueicon_text").GetComponent<Text>().text = "people";
            transform.Find("bg/functionView/GameLanguage/gameLanguage_text").GetComponent<Text>().text = "Language settings";
            transform.Find("bg/functionView/GameLanguage/gameNumbervalueicon_text").GetComponent<Text>().text = "Chinese";
        }
    }

    public override void OnUpdateGUIData(int scene = 1)
    {
        temp_soundValue = (int)(MachineDataMgr.Instance.MusicVolume * 100);
        gameSoundvalue_inputField.text = temp_soundValue.ToString(); //更新音量

        temp_numberValue = MachineDataMgr.Instance.PlayerShowCount;
        gameNumbervalue_inputField.text = temp_numberValue.ToString(); //更新人数

    }


    public override void OnEnter()
    {
        base.OnEnter();
        transform.DOLocalMoveY(1000,0);
        OnUpdateGUIData();
        gameObject.SetActive(true);
        transform.DOLocalMoveY(0, 2f).SetEase(Ease.InBack).OnComplete(() => {  });
    }

    public override void OnExit()
    {
        base.OnExit();
        transform.DOLocalMoveY(-1000, 2f).SetEase(Ease.InBack).OnComplete(() => { gameObject.SetActive(false); });
    }


}