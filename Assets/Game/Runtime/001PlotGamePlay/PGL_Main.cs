using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PGL_Main : MonoBehaviour
{
    private static Dictionary<int, PGL_CameraAniLoopClipConfigData.PGL_CameraAniLoopClipConfig>
        cameraAniLoopClipConfigs = new Dictionary<int, PGL_CameraAniLoopClipConfigData.PGL_CameraAniLoopClipConfig>();

    private static List<AnimationClip> cameraAniClips = new List<AnimationClip>();
    
    private static Dictionary<int, PGL_MonsterConfigData.PGL_MonsterConfig> monsterConfigs = new Dictionary<int, PGL_MonsterConfigData.PGL_MonsterConfig>();
    private static Dictionary<int, PGL_MonsterConfigData.PGL_MonsterGroupConfig> monsterGroupConfigs = new Dictionary<int, PGL_MonsterConfigData.PGL_MonsterGroupConfig>();

    public static IEnumerator LoadCameraAniClips()
    {
        cameraAniLoopClipConfigs.Clear();
        cameraAniClips.Clear();
        monsterConfigs.Clear();
        monsterGroupConfigs.Clear();
        
        string curSceneName = GameSceneMgr.Instance.CurSceneName();

        string aniClipPath = $"CameraAnim/PlotGamePlay/{curSceneName}_";
        int levelCount = GameSceneMgr.Instance.CurSceneLevelNum();
        for (int i = 1; i <= levelCount; i++)
        {
            string aniClipName = $"{aniClipPath}{i}";
            ResourceRequest request = ResMgr.Instance.LoadAssetASync<AnimationClip>(aniClipName);
            yield return request;

            AnimationClip aniClip = request.asset as AnimationClip;
            if (aniClip != null)
            {
                cameraAniClips.Add(aniClip);
            }
            else
            {
                Debug.LogError($"找不到{aniClipName}动画资源！");
            }
        }

        string configsPath = $"Config/PlotGamePlay/LoopConfig_{curSceneName}";
        ResourceRequest configsRequest =
            ResMgr.Instance.LoadAssetASync<PGL_CameraAniLoopClipConfigData>(configsPath);
        yield return configsRequest;
        PGL_CameraAniLoopClipConfigData loopConfigsData = configsRequest.asset as PGL_CameraAniLoopClipConfigData;
        if (loopConfigsData != null && loopConfigsData.data != null && loopConfigsData.data.Count > 0)
        {
            foreach (var config in loopConfigsData.data)
            {
                if (config == null) continue;

                cameraAniLoopClipConfigs.TryAdd(config.id, config);
            }
        }
        
        configsPath = $"Config/PlotGamePlay/MonsterConfigData_{curSceneName}";
        configsRequest =
            ResMgr.Instance.LoadAssetASync<PGL_MonsterConfigData>(configsPath);
        yield return configsRequest;
        PGL_MonsterConfigData monsterConfigsData = configsRequest.asset as PGL_MonsterConfigData;
        if (monsterConfigsData != null && monsterConfigsData.data != null && monsterConfigsData.data.Count > 0)
        {
            foreach (var config in monsterConfigsData.data)
            {
                if (config == null) continue;

                monsterConfigs.TryAdd(config.monsterID, config);
            }
        }
        if (monsterConfigsData != null && monsterConfigsData.groupData != null && monsterConfigsData.groupData.Count > 0)
        {
            foreach (var config in monsterConfigsData.groupData)
            {
                if (config == null) continue;

                monsterGroupConfigs.TryAdd(config.groupID, config);
            }
        }
        
    }

    private static void ClearCameraAniClips()
    {
        cameraAniClips.Clear();
        cameraAniLoopClipConfigs.Clear();
        monsterConfigs.Clear();
        monsterGroupConfigs.Clear();
    }

    public static PGL_MonsterConfigData.PGL_MonsterConfig GetMonsterConfig(int monsterID)
    {
        return monsterConfigs.GetValueOrDefault(monsterID);
    }
    public static PGL_MonsterConfigData.PGL_MonsterGroupConfig GetMonsterGroupConfig(int groupID)
    {
        return monsterGroupConfigs.GetValueOrDefault(groupID);
    }

    public const string LevelFinishedEvent = "PGL_Main_LevelFinishedEvent";
    public const string EnterLoopAniEvent = "PGL_Main_EnterLoopAniEvent";

    public Transform MainCameraRootTran { get; private set; }
    public Animation MainCameraRootAnim { get; private set; }
    public Transform MainCameraSubRootTran { get; private set; }
    public Animation MainCameraSubRootAnim { get; private set; }

    private int levelIndex, levelCount, timerId;
    private PGL_CameraAniLoopClipConfigData.PGL_CameraAniLoopClipConfig curLoopConfig;

    private bool isWaitingForContinue;

    private void Awake()
    {
        levelIndex = 0;
        levelCount = GameSceneMgr.Instance.CurSceneLevelNum();

        MainCameraRootTran = CameraController.Instance.MainCameraRootTran;
        MainCameraRootAnim = CameraController.Instance.MainCameraRootAnim;
        MainCameraSubRootTran = CameraController.Instance.MainCameraSubRootTran;
        MainCameraSubRootAnim = CameraController.Instance.MainCameraSubRootAnim;

        MainCameraRootTran.position = GameSceneMgr.Instance.CurSceneInitCamPos();
        MainCameraRootTran.eulerAngles = GameSceneMgr.Instance.CurSceneInitCamAngle();
        MainCameraRootAnim.Stop();

        MainCameraSubRootTran.localPosition = Vector3.zero;
        MainCameraSubRootTran.localRotation = Quaternion.identity;
        MainCameraSubRootAnim.Stop();
        
        PGL_MonsterMgr.Instance.ResetUnitRoot();
    }

    private void Start()
    {
        isWaitingForContinue = false;
        EventMgr.Instance.AddListener(PGL_Main.LevelFinishedEvent, OnLevelFinishedEvent);
        EventMgr.Instance.AddListener(PGL_Main.EnterLoopAniEvent, OnEnterLoopAniEvent);
        EventMgr.Instance.AddListener(CountDown_UICtrl.CountDownFinishedEvent, OnCountDownFinished);
        EventMgr.Instance.AddListener(HomeWindow_UICtrl.PlayerActiveEvent, OnPlayerActive);
        EventMgr.Instance.AddListener(LevelContinueTips_UICtrl.CountFinishedEvent, OnContinueCountFinished);

        PGL_MonsterMgr.Instance.GameStart();
        
        StartLevel();
    }

    
    private void OnContinueCountFinished(string s, object o)
    {
        GameOver(false);
    }
    private void OnPlayerActive(string s, object o)
    {
        if (!isWaitingForContinue)
            return;

        isWaitingForContinue = false;
        EventMgr.Instance.Emit(LevelContinueTips_UICtrl.StopTipsEvent, null); //触发 停止是否继续提示
    }
    private void OnCountDownFinished(string s, object o)
    {
        if (isWaitingForContinue)
            return;

        isWaitingForContinue = true;
        EventMgr.Instance.Emit(LevelContinueTips_UICtrl.ShowTipsEvent, GameSceneMgr.Instance.CurSceneContinueTime()); //触发 显示是否继续
    }
    private void OnLevelFinishedEvent(string s, object o)
    {
        levelIndex++;
        if (levelIndex >= levelCount)
        {
            GameOver(true);
        }
        else
        {
            StartLevel();
        }
    }

    private int killedCount;
    private int loopAniId;
    private void OnEnterLoopAniEvent(string s, object o)
    {
        int loopId = (int)o;
        if (!cameraAniLoopClipConfigs.ContainsKey(loopId))
        {
            Debug.LogError($"找不到loopId为{loopId}的循环配置！");
            return;
        }
        
        Debug.Log($"OnEnterLoopAniEvent, loopId = {loopId}");
        
        MainCameraRootAnim[cameraAniName].speed = 0f;

        loopAniId = loopId;
        killedCount = 0;
        TimerMgr.Instance.UnSchedule(timerId);
        curLoopConfig = cameraAniLoopClipConfigs[loopId];

        if (curLoopConfig.loopEndTime > 0)
        {
            timerId = TimerMgr.Instance.ScheduleOnce((_) =>
            {
                OnLoopAniEnd();
            }, curLoopConfig.loopEndTime);
        }

        if (curLoopConfig.aniLoopClip != null)
        {
            PlaySubCameraAni(curLoopConfig.aniLoopClip);
        }

        if (curLoopConfig.killMonsterCount > 0)
        {
            //怪物被击杀事件
            EventMgr.Instance.AddListener(GameLevel.MonsterKilledEvent, OnMonsterKilledEvent);
        }
    }

    private void OnMonsterKilledEvent(string arg1, object arg2)
    {
        if (arg2 == null)
            return;

        var datas = (int[])arg2;
        int player = datas[0];
        int unitId = datas[1];
        int score = datas[2];
        int health = datas[3];
        
        killedCount++;
        if (killedCount >= curLoopConfig.killMonsterCount)
        {
            OnLoopAniEnd();
        }
    }

    private void OnLoopAniEnd()
    {
        Debug.Log($"OnLoopAniEnd, loopId = {loopAniId}");
        if (curLoopConfig.killMonsterCount > 0)
        {
            EventMgr.Instance.RemoveListener(GameLevel.MonsterKilledEvent, OnMonsterKilledEvent);
        }
        TimerMgr.Instance.UnSchedule(timerId);
        curLoopConfig = null;

        ClearSubCameraAni(false);
        MainCameraRootAnim[cameraAniName].speed = 1f;
    }

    private string cameraAniName;
    private void StartLevel()
    {
        cameraAniName = cameraAniClips[levelIndex].name;
        CameraController.Instance.PlayCameraRootAnim(cameraAniClips[levelIndex]);
    }

    // 游戏结束
    private void GameOver(bool isWin)
    {
        // 禁止射击
        EventMgr.Instance.Emit(PlayerInfos_UICtrl.IsCanShootEvent, false);
        
        PGL_MonsterMgr.Instance.GameEnd();
        
        EventMgr.Instance.RemoveListener(PGL_Main.LevelFinishedEvent, OnLevelFinishedEvent);
        EventMgr.Instance.RemoveListener(PGL_Main.EnterLoopAniEvent, OnEnterLoopAniEvent);
        EventMgr.Instance.RemoveListener(CountDown_UICtrl.CountDownFinishedEvent, OnCountDownFinished);
        EventMgr.Instance.RemoveListener(HomeWindow_UICtrl.PlayerActiveEvent, OnPlayerActive);
        EventMgr.Instance.RemoveListener(LevelContinueTips_UICtrl.CountFinishedEvent, OnContinueCountFinished);
        TimerMgr.Instance.UnSchedule(timerId);
        CameraController.Instance.StopAllCoroutines();
        ClearCameraAniClips();

        MainCameraRootTran = null;
        MainCameraRootAnim = null;
        MainCameraSubRootTran = null;
        MainCameraSubRootAnim = null;

        GameLevelMgr.Instance.GameOver(isWin);
    }

    private void ClearSubCameraAni(bool reset = true)
    {
        MainCameraSubRootTran.DOKill();
        MainCameraSubRootAnim.Stop();
        List<string> subClips = (from AnimationState state in MainCameraSubRootAnim select state.clip.name).ToList();
        foreach (string clip in subClips)
        {
            this.MainCameraSubRootAnim.RemoveClip(clip);
        }

        if (reset)
        {
            MainCameraSubRootTran.localPosition = Vector3.zero;
            MainCameraSubRootTran.localRotation = Quaternion.identity;
        }
        else
        {
            MainCameraSubRootTran.DOLocalMove(Vector3.zero, 1f);
            MainCameraSubRootTran.DOLocalRotateQuaternion(Quaternion.identity, 1f);
        }
    }

    private void PlaySubCameraAni(AnimationClip clip)
    {
        ClearSubCameraAni(true);
        clip.legacy = true;
        MainCameraSubRootAnim.AddClip(clip, clip.name);
        MainCameraSubRootAnim.Play(clip.name);
    }
}