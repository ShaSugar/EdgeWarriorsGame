using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class CameraController : UnitySingleton<CameraController>
{
    /// <summary>
    /// 进入场景重置相机
    /// </summary>
    public const string CameraResetForSceneStart = "CameraController_CameraResetForSceneStart";
    /// <summary>
    /// 移动相机事件
    /// </summary>
    public const string CameraMoveEvent = "CameraController_CameraMoveEvent";
    /// <summary>
    /// 相机震动事件
    /// </summary>
    public const string CameraShakeEvent = "CameraController_CameraShakeEvent";
    public const string CameraShakeEvent2 = "CameraController_CameraShakeEvent2";
    /// <summary>
    /// 移动相机结束事件
    /// </summary>
    public const string CameraMoveFinishedEvent = "CameraMoveFinishedEvent";
    
    public Camera MainCamera { get; private set; }

    public Transform MainCameraTran { get; private set; }

    public Transform MainCameraRootTran { get; private set; }
    public Animation MainCameraRootAnim { get; private set; }
    public Transform MainCameraSubRootTran { get; private set; }
    public Animation MainCameraSubRootAnim { get; private set; }
    public void RemoveAllCameraRootAnimClips()
    {
        this.MainCameraRootAnim.Stop();
        List<string> clips = (from AnimationState state in this.MainCameraRootAnim select state.clip.name).ToList();
        foreach (string clip in clips)
        {
            this.MainCameraRootAnim.RemoveClip(clip);
        }
        this.MainCameraSubRootAnim.Stop();
        List<string> subClips = (from AnimationState state in this.MainCameraSubRootAnim select state.clip.name).ToList();
        foreach (string clip in subClips)
        {
            this.MainCameraSubRootAnim.RemoveClip(clip);
        }
    }
    public void PlayCameraRootAnim(AnimationClip clip)
    {
        this.isMoving = false;
        this.MainCameraRootTran.DOKill();
        
        RemoveAllCameraRootAnimClips();
        clip.legacy = true;
        this.MainCameraRootAnim.AddClip(clip, clip.name);
        this.MainCameraRootAnim.Play(clip.name);
        this.MainCameraRootAnim[clip.name].speed = 1f;
    }

    public Camera BulletCamera { get; private set; }

    public Transform BulletCameraTran { get; private set; }

    static float MovingTime = 2f;
    bool isMoving;
    float movingPassedTime;
    Vector3 startPos, endPos;
    Quaternion startRot, endRot;
    Vector3 shakeStrength = new(1, 1, 0);
    Vector3 shakeStrength2 = new(0.5f, 0.5f, 0);

    public void Init()
    {
        this.isMoving = false;
        this.MainCameraRootTran = GameObject.Find("MainCameraRoot").transform;
        this.MainCameraRootAnim = this.MainCameraRootTran.GetComponent<Animation>();
        this.MainCameraSubRootTran = this.MainCameraRootTran.Find("SubRoot");
        this.MainCameraSubRootAnim = this.MainCameraSubRootTran.GetComponent<Animation>();
        RemoveAllCameraRootAnimClips();
        GameObject mainCameraObj = this.MainCameraSubRootTran.Find("MainCamera").gameObject;
        if (mainCameraObj == null)
        {
            Debug.LogError("找不到主摄像机，请检查主场景相机设置！！！");
            return;
        }
        this.MainCamera = mainCameraObj.GetComponent<Camera>();
        this.MainCameraTran = mainCameraObj.transform;

        GameObject bulletCameraObj = GameObject.Find("BulletCamera");
        if (bulletCameraObj == null)
        {
            return;
        }
        
        this.BulletCamera = bulletCameraObj.GetComponent<Camera>();
        this.BulletCameraTran = bulletCameraObj.transform;

        // 游戏开始重置相机
        EventMgr.Instance.AddListener(CameraController.CameraResetForSceneStart, (_, udata) =>
        {
            if (udata == null)
                return;

            // var scene = (int)udata;
            // if (scene < 0 ||
            //     scene >= GameLevelMgr.Instance.gameLevelConfigData.initSceneCameraPos.Length ||
            //     scene >= GameLevelMgr.Instance.gameLevelConfigData.initSceneCameraAngle.Length)
            // {
            //     Debug.LogError($"场景{scene}初始摄像机参数配置错误！！！");
            //     return;
            // }
            
            this.isMoving = false;
            RemoveAllCameraRootAnimClips();
            this.MainCameraTran.DOKill();
            this.MainCameraRootTran.DOKill();
            this.MainCameraTran.localPosition = Vector3.zero;
            this.MainCameraTran.localEulerAngles = Vector3.zero;
            this.MainCameraRootTran.position = GameSceneMgr.Instance.CurSceneInitCamPos();//GameLevelMgr.Instance.gameLevelConfigData.initSceneCameraPos[scene];
            this.MainCameraRootTran.eulerAngles = GameSceneMgr.Instance.CurSceneInitCamAngle();//GameLevelMgr.Instance.gameLevelConfigData.initSceneCameraAngle[scene];
        });
        EventMgr.Instance.AddListener(CameraController.CameraMoveEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            MoveTransform((Vector3[])udata);

        });
        
        // 震屏效果
        EventMgr.Instance.AddListener(CameraController.CameraShakeEvent, (_, _) =>
        {
            this.MainCameraTran.DOKill();
            this.MainCameraTran.localPosition = Vector3.zero;
            this.MainCameraTran.DOShakePosition(1f, shakeStrength)
                .SetEase(Ease.Linear)
                .OnComplete(()=>this.MainCameraTran.localPosition = Vector3.zero);
        });
        // 震屏效果
        EventMgr.Instance.AddListener(CameraController.CameraShakeEvent2, (_, _) =>
        {
            this.MainCameraTran.DOKill();
            this.MainCameraTran.localPosition = Vector3.zero;
            this.MainCameraTran.DOShakePosition(1f, shakeStrength2)
                .SetEase(Ease.Linear)
                .OnComplete(()=>this.MainCameraTran.localPosition = Vector3.zero);
        });
        
        RemoveAllCameraRootAnimClips();
        MainCameraRootTran.gameObject.SetActive(false);
    }

    void MoveTransform(Vector3[] cameraData)
    {
        MovingTime = GameLevelMgr.Instance.gameLevelConfigData.normalLevelCameraMoveTime;
        this.MainCameraRootTran.DOKill();
        this.isMoving = true;
        RemoveAllCameraRootAnimClips();
        this.movingPassedTime = 0;
        this.startPos = this.MainCameraRootTran.position;
        this.endPos = cameraData[0];
        this.startRot = this.MainCameraRootTran.rotation;
        this.endRot = Quaternion.Euler(cameraData[1]);
    }

    void Update()
    {
        if (!this.isMoving)
            return;

        this.movingPassedTime += Time.deltaTime;
        if (this.movingPassedTime >= MovingTime)
        {
            this.isMoving = false;
            this.movingPassedTime = 0;
            this.MainCameraRootTran.position = this.endPos;
            this.MainCameraRootTran.rotation = this.endRot;

            EventMgr.Instance.Emit(CameraController.CameraMoveFinishedEvent, null);
        }
        else
        {
            this.MainCameraRootTran.position = Vector3.Lerp(this.startPos, this.endPos, this.movingPassedTime / MovingTime);
            this.MainCameraRootTran.rotation =
                Quaternion.Lerp(this.startRot, this.endRot, this.movingPassedTime / MovingTime);
        }
    }
}
