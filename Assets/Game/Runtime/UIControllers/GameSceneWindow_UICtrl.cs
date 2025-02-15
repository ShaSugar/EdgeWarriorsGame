using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
/// <summary>
/// 选择哪个场景视图
/// </summary>
public class GameSceneWindow_UICtrl : UICtrl
{
    /// <summary>
    /// 加载场景对应数量 ---> 其实就是 scene1 后面的数字
    /// </summary>
    public int loadedScene { get; private set; }

    int controllerPlayer;


    bool chooseViewFlag;
    GameObject chooseViewObj;
    int selectedScene;
    /// <summary>
    /// 存储着 场景选择的对象
    /// </summary>
    RectTransform[] chooseViewItemRects;

    GameObject loadingViewObj;
    RawImage loadingViewImg;
    int loadingViewImgIndex;
    Slider loadingViewSlider;
    bool[] chooseViewItemSelected;
    static readonly WaitForSeconds waitTime = new WaitForSeconds(0.2f);
    void Start()
    {
        this.loadedScene = -1;

        this.chooseViewObj = this.ViewNode($"ChooseView");
        int sceneNum = GameSceneMgr.Instance.SceneNum;
        this.chooseViewItemRects = new RectTransform[sceneNum];
        for (var i = 0; i < this.chooseViewItemRects.Length; i++)
        {
            this.chooseViewItemRects[i] = this.chooseViewObj.transform.Find($"group/Item{i + 1}").GetComponent<RectTransform>();
        }

        // 初始化布尔数组，根据 chooseViewItemRects 的长度设置
        chooseViewItemSelected = new bool[chooseViewItemRects.Length];
        for (int i = 0; i < chooseViewItemSelected.Length; i++)
        {
            chooseViewItemSelected[i] = false;
        }

        this.loadingViewObj = this.ViewNode($"LoadingView");
        this.loadingViewImg = this.loadingViewObj.transform.Find("BgRawImage").GetComponent<RawImage>();
        this.loadingViewSlider = this.loadingViewObj.transform.Find("Slider").GetComponent<Slider>();
        this.loadingViewSlider.value = 0f;

        EventMgr.Instance.AddListener(GunMgr.ShootEvent, (_, udata) =>
        {
            if (!this.chooseViewFlag)
                return;

            if (udata == null)
                return;

            var data = (bool[])udata;
            if (!data[this.controllerPlayer])
                return;

            Vector2 pos = GameApp.Instance.GetPlayerCursorPos(this.controllerPlayer);
            for (var i = 0; i < this.chooseViewItemRects.Length; i++)
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(this.chooseViewItemRects[i],
                    pos))
                    continue;

                StartCoroutine(EnterScene(i));
                return;
            }
        });

        HideAllViews();
    }
    /// <summary>
    /// 显示选择场景
    /// </summary>
    public void ShowChooseView()
    {
        GameApp.Instance.ShowAllPlayers(false);
        HideAllViews();
        this.controllerPlayer = GameApp.Instance.GetFirstCanPlayPlayer();
        this.selectedScene = this.loadedScene >= 0 ? this.loadedScene : 0;
        ChooseViewUpdateSelected();
        this.chooseViewObj.SetActive(true);
        this.gameObject.SetActive(true);
        this.chooseViewFlag = true;
    }
    /// <summary>
    /// 选择更新
    /// </summary>
    void ChooseUpdate()
    {
        if (!this.chooseViewFlag)
            return;

        Vector2 pos = GameApp.Instance.GetPlayerCursorPos(this.controllerPlayer);
        for (var i = 0; i < this.chooseViewItemRects.Length; i++)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(this.chooseViewItemRects[i], pos))
            {

                if (this.selectedScene != -1)
                {
                    this.selectedScene = -1;
                    ChooseViewUpdateSelected();
                }
                // 鼠标离开对象时重置已选状态
                chooseViewItemSelected[i] = false;
                continue;
            }

            if (i == this.selectedScene)
                return;

            this.selectedScene = i;

            // 如果当前项尚未被选中，则播放音效
            if (!chooseViewItemSelected[i])
            {
                chooseViewItemSelected[i] = true;
                SoundMgr.Instance.PlayOneShot($@"Sounds\prop_get", false);
            }

            ChooseViewUpdateSelected();
            return;
        }
    }
    void ChooseViewUpdateSelected()
    {
        for (var i = 0; i < this.chooseViewItemRects.Length; i++)
        {
            if (i == this.selectedScene)
            {
                this.chooseViewItemRects[i].localScale = Vector3.one * 1.2f;
            }
            else
            {
                this.chooseViewItemRects[i].localScale = Vector3.one;
            }
        }
    }
    IEnumerator EnterScene(int scene)
    {
        // int lastLoadedScene = this.loadedScene;
        this.loadedScene = scene;
        EventMgr.Instance.Emit(CameraController.CameraResetForSceneStart, scene);

        this.loadingViewSlider.DOKill();
        this.loadingViewSlider.value = 0f;
        this.chooseViewFlag = false;

        var flag = false;
        if (MachineDataMgr.Instance.IsChineseLanguageVersion && this.loadingViewImgIndex != 0)
            flag = true;
        else if (!MachineDataMgr.Instance.IsChineseLanguageVersion && this.loadingViewImgIndex != 1)
            flag = true;
        else if (this.loadedScene != scene)
            flag = true;

        this.loadingViewImgIndex = MachineDataMgr.Instance.IsChineseLanguageVersion ? 0 : 1;
        if (!flag)
        {
            //Debug.Log(scene);
            var imgPath = $"Textures/loading_bg_scene{scene + 1}";
            if (!MachineDataMgr.Instance.IsChineseLanguageVersion)
                imgPath = $"Textures/loading_bg_scene{scene + 1}_EN";
            ResourceRequest request = ResMgr.Instance.LoadAssetASync<Texture>(imgPath);
            yield return request;

            this.loadingViewImg.texture = request.asset as Texture;

            UnitMgr.Instance.DestroyAllUnits();
            UnitEffectMgr.Instance.DestroyAllEffect();
        }

        if (GameSceneMgr.Instance.CurScenePlayType() == GameScenePlay.Plot)
        {
            yield return PGL_Main.PreLoadResource();
        }
        else
        {
        }

        this.loadingViewObj.SetActive(true);
        this.loadingViewSlider.DOValue(0.9f, 1f);
        this.chooseViewObj.SetActive(false);
        GameApp.Instance.ShowOrHidePlayerCursorsRoot(false);

        // if (this.loadedScene != lastLoadedScene)
        // {
        yield return SceneMgr.Instance.EnterSceneAsync(GameSceneMgr.Instance.CurSceneName());
        // }
        // else
        // {
        //     yield return waitTime;
        // }

        GameLevelMgr.Instance.PreloadClear();
        yield return GameLevelMgr.Instance.Preload();

        System.GC.Collect();

        this.loadingViewSlider.DOKill();
        this.loadingViewSlider.DOValue(1f, 0.2f);

        CameraController.Instance.RemoveAllCameraRootAnimClips();
        CameraController.Instance.MainCameraRootTran.gameObject.SetActive(true);

        yield return waitTime;

        UnitMgr.Instance.UpdateUnitConfigs();
        SmallGameMgr.Instance.UpdatePlayerShowCount();
        GameApp.Instance.ShowAllPlayers(true);
        GameApp.Instance.ShowOrHidePlayerCursorsRoot(true);
        if (GameSceneMgr.Instance.IsShowGameStartEffect())
        {
            GameApp.Instance.ShowGameStartView(scene);
        }
        else
        {
            EventMgr.Instance.Emit(GameApp.GameStartEvent, null);
        }

        HideAllViews();
    }
    void Update()
    {
        ChooseUpdate();
    }

    void HideAllViews()
    {
        this.gameObject.SetActive(false);
        this.chooseViewFlag = false;
        this.chooseViewObj.SetActive(false);

        this.loadingViewSlider.DOKill();
        this.loadingViewSlider.value = 0f;
        this.loadingViewObj.SetActive(false);
    }

}

