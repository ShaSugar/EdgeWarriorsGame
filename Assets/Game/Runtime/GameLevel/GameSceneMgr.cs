using UnityEngine;

// 场景玩法
public enum GameScenePlay
{
    Challenge,  // 挑战玩法
    Plot,       // 剧情玩法
}

/// <summary>
/// 游戏场景管理 ---> 进游戏选择哪个场景关卡
/// </summary>
public class GameSceneMgr : Singleton<GameSceneMgr>
{
    GameSceneWindow_UICtrl gameSceneWindowUICtrl;

    GameSceneConfigData gameSceneConfigData;
    
    public void Init()
    {
        this.gameSceneWindowUICtrl =
            UIMgr.Instance.ShowUIView("GUIPrefabs/GameSceneWindow") as GameSceneWindow_UICtrl;
        
        
        gameSceneConfigData = ResMgr.Instance.LoadAssetSync<GameSceneConfigData>("Config/GameSceneConfigData");
    }
    public void ShowChooseSceneWindow()
    {
        this.gameSceneWindowUICtrl.ShowChooseView();
    }

    public int CurScene
    {
        get => this.gameSceneWindowUICtrl == null ? 1 : this.gameSceneWindowUICtrl.loadedScene + 1;
    }

    public int SceneNum
    {
        get
        {
            if (gameSceneConfigData == null || gameSceneConfigData.data == null)
                return 0;
            else
                return gameSceneConfigData.data.Count;
        }
    }
    
    public string CurSceneName()
    {
        int index = this.gameSceneWindowUICtrl.loadedScene;
        
        return gameSceneConfigData.data[index].sceneName;
    }

    public Vector3 CurSceneInitCamPos()
    {
        int index = this.gameSceneWindowUICtrl.loadedScene;

        return gameSceneConfigData.data[index].initCameraPos;
    }
    public Vector3 CurSceneInitCamAngle()
    {
        int index = this.gameSceneWindowUICtrl.loadedScene;

        return gameSceneConfigData.data[index].initCameraAngle;
    }

    public int CurSceneLevelNum()
    {
        int index = this.gameSceneWindowUICtrl.loadedScene;
        //Debug.Log("loadedScene: " + index);

        return gameSceneConfigData.data[index].levelNum;
    }

    public GameScenePlay CurScenePlayType()
    {
        int index = this.gameSceneWindowUICtrl.loadedScene;

        return gameSceneConfigData.data[index].playType;
    }

    public int CurSceneContinueTime()
    {
        int index = this.gameSceneWindowUICtrl.loadedScene;

        return gameSceneConfigData.data[index].continueTime;
    }

    public bool IsShowGameStartEffect()
    {
        int index = this.gameSceneWindowUICtrl.loadedScene;

        return gameSceneConfigData.data[index].isShowGameStartEffect;
    }
}
