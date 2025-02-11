using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 最初加载 
/// </summary>
public class PatchWindow_UICtrl : UICtrl
{
    // 重置光标位置
    public const string UpdateLoadingProcessEvent = "UpdateLoadingProcessEvent";

    int curProcess;
    int targetProcess;


    // UGUI相关
    RawImage bgImg;
    private Slider slider;
    private TextMeshProUGUI tips;
    private GameObject messageBoxObj;

    static readonly string[] loadingTips = new[]
    {
        "loading",
        "loading.",
        "loading..",
        "loading...",
    };

    void Start()
    {
        this.bgImg = this.transform.Find("bg").Find("logo").GetComponent<RawImage>();
        if (PlayerPrefs.GetInt(MachineDataMgr.SaveKey_LanguageVersion, 0) == 1)
        {
            this.bgImg.texture = ResMgr.Instance.LoadAssetSync<Texture>("Textures/logo_EN");
            this.bgImg.SetNativeSize();
        }
        this.slider = this.View<Slider>($"Slider");
        this.slider.value = 0f;
        this.tips = this.slider.transform.Find("txt_tips").GetComponent<TextMeshProUGUI>();
        this.messageBoxObj = this.ViewNode($"MessageBox");

        EventMgr.Instance.AddListener(PatchWindow_UICtrl.UpdateLoadingProcessEvent, UpdateProcess);

        ShowLoadingTips();
    }

    void Update()
    {
        if (this.curProcess >= this.targetProcess)
        {
            if (this.curProcess >= 100)
            {
                EventMgr.Instance.RemoveListener(PatchWindow_UICtrl.UpdateLoadingProcessEvent, UpdateProcess);
                TimerMgr.Instance.UnSchedule(this.loadingTipsTimerId);
                this.loadingTipsTimerId = -1;
                UIMgr.Instance.RemoveUIView("GUIPrefabs/PatchWindow");
                
                EventMgr.Instance.Emit(GameApp.WaitForStartEvent, null);
            }
            return;
        }

        this.curProcess += 1;
        this.slider.value = (float)this.curProcess / 100;
    }

    void OnDestroy()
    {
        if(this.loadingTipsTimerId != -1)
        {
            TimerMgr.Instance.UnSchedule(this.loadingTipsTimerId);
            this.loadingTipsTimerId = -1;
        }
        
        EventMgr.Instance.RemoveListener(PatchWindow_UICtrl.UpdateLoadingProcessEvent, UpdateProcess);
    }

    void UpdateProcess(string eventName, object udata)
    {
        if (udata == null)
            return;

        var process = (int)udata;
        if (process > this.targetProcess)
            this.targetProcess = process;
    }

    int loadingTipsTimerId = -1;
    int loadingTipsIndex;
    void ShowLoadingTips()
    {
        this.loadingTipsTimerId = TimerMgr.Instance.Schedule(_ =>
        {
            this.loadingTipsIndex = this.loadingTipsIndex >= PatchWindow_UICtrl.loadingTips.Length ? 0 : loadingTipsIndex;
            this.tips.text = PatchWindow_UICtrl.loadingTips[loadingTipsIndex++];
        }, -1, 0.5f, 0);
    }

}
