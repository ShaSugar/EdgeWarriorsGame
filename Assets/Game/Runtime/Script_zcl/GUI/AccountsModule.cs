using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnA.Base;
using UnityEngine;
using UnityEngine.UI;

public class AccountsModule : UIModuleBase
{

    private InputField totalCoins_inputField;
    private InputField totalLotteryTickets_inputField;
    Transform content;
    private Text gameCoinRatiovalue_text, gameLotteryProportionvalue_text;
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
        if (!PlayerPrefs.HasKey(SetgameLotteryProportionvalue_text))
        {
            PlayerPrefs.SetInt(SetgameLotteryProportionvalue_text,10);
            PlayerPrefs.Save();
        }

        //EventMgr.Instance.AddListener(SmallGameBigWheel.BigWheelRewardEvent, (eventName, obj) =>
        //{

        //});
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveListener(BackendPanel.APPLYFUNCTION, Apply);
    }

    #region Event

    private void Apply(string name,object obj)
    {
        if (gameObject.activeSelf == false) { Debug.Log($"{gameObject.name} ��������"); return; }

        totalCoins_inputField.text = MachineDataMgr.Instance.CoinNumForMachine.ToString();//��Ͷ����
        MachineDataMgr.Instance.SetCoinNumForOneGame(int.Parse(content.Find("CoinRatio/gameCoinRatiovalue_text").GetComponent<Text>().text.Split(' ')[0])); //����һ�ֶ��ٱ�
        totalLotteryTickets_inputField.text = "0"; //�ܲ�Ʊ
                                                   //content.Find("LotteryProportion/gameLotteryProportionvalue_text").GetComponent<Text>().text = "10 ��"; //��ȡһ�ֶ��ٷ�
    }

    #endregion

    public override IEnumerator AwakeInit()
    {
        #region TotalCoins


        content = transform.Find("bgAccounts/GameAccountsView/Viewport/Content");
        totalCoins_inputField = content.Find("TotalCoins/TotalCoins_inputField").GetComponent<InputField>();
        totalCoins_inputField.text = "0";
        totalLotteryTickets_inputField = content.Find("TotalLotteryTickets/TotalLotteryTickets_inputField").GetComponent<InputField>();
        totalLotteryTickets_inputField.text = "0";
        gameCoinRatiovalue_text = content.Find("CoinRatio/gameCoinRatiovalue_text").GetComponent<Text>();
        gameLotteryProportionvalue_text = content.Find("LotteryProportion/gameLotteryProportionvalue_text").GetComponent<Text>();
        onClickAccounts(content.Find("CoinRatio/leftArrow_btn").GetComponent<Button>(), gameCoinRatiovalue_text, 0, 5);
        onClickAccounts(content.Find("CoinRatio/rightArrow_btn").GetComponent<Button>(), gameCoinRatiovalue_text, 1, 5);
        onClickAccounts(content.Find("LotteryProportion/leftArrow_btn").GetComponent<Button>(), gameLotteryProportionvalue_text, 0, 100);
        onClickAccounts(content.Find("LotteryProportion/rightArrow_btn").GetComponent<Button>(), gameLotteryProportionvalue_text, 1, 100);
        #endregion
        yield return null;
        LanguageUpdate(MachineDataMgr.Instance.IsChineseLanguageVersion);
        gameObject.SetActive(false);
    }

    public override void LanguageUpdate(bool IsChinese)
    {
        if (IsChinese)
        {
            transform.Find("bgAccounts/GameAccountsView/GamePropTitle_text").GetComponent<Text>().text = "��Ŀ��ϸ";

            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/TotalCoins/gameTotalCoinsName_text").GetComponent<Text>().text = "��Ͷ����";
            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/TotalCoins/gameTotalCoinsvalueicon_text").GetComponent<Text>().text = "��";

            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/TotalLotteryTickets/gameTotalLotteryTicketsName_text").GetComponent<Text>().text = "�ܳ���Ʊ";
            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/TotalLotteryTickets/gameTotalLotteryTicketsvalueicon_text").GetComponent<Text>().text = "��";

            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/CoinRatio/gameCoinRatioName_text").GetComponent<Text>().text = "Ͷ�ұ���";
            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/CoinRatio/gameCoinRatiovalueicon_text").GetComponent<Text>().text = "��";

            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/LotteryProportion/gameLotteryProportionName_text").GetComponent<Text>().text = "��Ʊ����";
            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/LotteryProportion/gameLotteryProportionvalueicon_text").GetComponent<Text>().text = "Ʊ";

            char[] c = gameCoinRatiovalue_text.text.ToArray();
            int value = int.Parse(gameCoinRatiovalue_text.text.Split(' ')[0]);
            gameCoinRatiovalue_text.text = $"{value} ��";

            c = gameLotteryProportionvalue_text.text.ToArray();
            value = int.Parse(gameLotteryProportionvalue_text.text.Split(' ')[0]);
            gameLotteryProportionvalue_text.text = $"{value} ��";
        }
        else
        {
            transform.Find("bgAccounts/GameAccountsView/GamePropTitle_text").GetComponent<Text>().text = "Account Details";

            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/TotalCoins/gameTotalCoinsName_text").GetComponent<Text>().text = "Total Coins ";
            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/TotalCoins/gameTotalCoinsvalueicon_text").GetComponent<Text>().text = "coin";

            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/TotalLotteryTickets/gameTotalLotteryTicketsName_text").GetComponent<Text>().text = "Total lottery tickets";
            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/TotalLotteryTickets/gameTotalLotteryTicketsvalueicon_text").GetComponent<Text>().text = "fix";

            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/CoinRatio/gameCoinRatioName_text").GetComponent<Text>().text = "Coin ratio";
            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/CoinRatio/gameCoinRatiovalueicon_text").GetComponent<Text>().text = "chessboard";

            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/LotteryProportion/gameLotteryProportionName_text").GetComponent<Text>().text = "Lottery proportion";
            transform.Find("bgAccounts/GameAccountsView/Viewport/Content/LotteryProportion/gameLotteryProportionvalueicon_text").GetComponent<Text>().text = "vote";


            char[] c = gameCoinRatiovalue_text.text.ToArray();
            int value = int.Parse(gameCoinRatiovalue_text.text.Split(' ')[0]);
            gameCoinRatiovalue_text.text = $"{value} coin";

            c = gameLotteryProportionvalue_text.text.ToArray();
            value = int.Parse(gameLotteryProportionvalue_text.text.Split(' ')[0]);
            gameLotteryProportionvalue_text.text = $"{value} divide";
        }
    }

    /// <summary>
    /// �� �� Ʊ
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="text"></param>
    /// <param name="index">0---��  1---��</param>
    /// <param name="type">5---��  100---Ʊ</param>
    private void onClickAccounts(Button btn, Text text, int index, int type)
    {
        btn.onClick.AddListener(() =>
        {
            char[] c = text.text.ToArray();
            int value = int.Parse(text.text.Split(' ')[0]);
            if (index == 0) //��
            {
                if (type == 5) //��
                {
                    if (value > 1)
                    {
                        value -= 1;

                        text.text = MachineDataMgr.Instance.IsChineseLanguageVersion ? $"{value} ��" : $"{value} coin";
                    }
                }
                else if (type == 100) //Ʊ
                {
                    if (value > 2)
                    {
                        value -= 2;
                        text.text = MachineDataMgr.Instance.IsChineseLanguageVersion ? $"{value} ��" : $"{value} divide";
                        PlayerPrefs.SetInt(SetgameLotteryProportionvalue_text, int.Parse(text.text.Split(' ')[0]));
                        PlayerPrefs.Save();
                    }
                }
            }
            else if (index == 1) //��
            {
                if (type == 5) //��
                {
                    if (value < 100)
                    {
                        value += 1;
                        text.text = MachineDataMgr.Instance.IsChineseLanguageVersion ? $"{value} ��" : $"{value} coin";
                    }
                }
                else if (type == 100) //Ʊ
                {
                    if (value < 10000)
                    {
                        value += 2;
                        text.text = MachineDataMgr.Instance.IsChineseLanguageVersion ? $"{value} ��" : $"{value} divide";
                        PlayerPrefs.SetInt(SetgameLotteryProportionvalue_text, int.Parse(text.text.Split(' ')[0]));
                        PlayerPrefs.Save();
                    }
                }
            }
        });
    }

    const string SetgameLotteryProportionvalue_text = "SetgameLotteryProportionvalue_text";
    public override void OnUpdateGUIData(int scene = 1)
    {
        totalCoins_inputField.text = MachineDataMgr.Instance.CoinNumForMachine.ToString();//��Ͷ����
        content.Find("CoinRatio/gameCoinRatiovalue_text").GetComponent<Text>().text = MachineDataMgr.Instance.IsChineseLanguageVersion ? $"{MachineDataMgr.Instance.CoinNumForOneGame} ��" : $"{MachineDataMgr.Instance.CoinNumForOneGame} coin"; //��ȡһ�ֶ��ٱ�
        totalLotteryTickets_inputField.text = "0"; //�ܲ�Ʊ
        content.Find("LotteryProportion/gameLotteryProportionvalue_text").GetComponent<Text>().text = MachineDataMgr.Instance.IsChineseLanguageVersion ? $"{PlayerPrefs.GetInt(SetgameLotteryProportionvalue_text)} ��" : $"{PlayerPrefs.GetInt(SetgameLotteryProportionvalue_text)} divide"; //��ȡһ�ֶ��ٷ�
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
