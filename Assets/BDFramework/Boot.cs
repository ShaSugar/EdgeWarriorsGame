using System;
using System.Collections;
using UnityEngine;

public class Boot : UnitySingleton<Boot>
{
    public override void Awake()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        base.Awake();

        this.StartCoroutine(this.BootStartup());
    }

    IEnumerator BootStartup()
    {

        // 框架初始化
        yield return InitFramework();
        // end

        // 进入游戏
        yield return GameApp.Instance.EnterGame();
        // end
    }


    IEnumerator InitFramework()
    {
        this.gameObject.AddComponent<ResMgr>().Init();
        this.gameObject.AddComponent<EventMgr>().Init();
        this.gameObject.AddComponent<TimerMgr>().Init();
        this.gameObject.AddComponent<SoundMgr>().Init();
        this.gameObject.AddComponent<NodePoolMgr>().Init();
        this.gameObject.AddComponent<UIMgr>().Init();
        this.gameObject.AddComponent<SceneMgr>().Init();
        this.gameObject.AddComponent<GameApp>().Init();
        yield break;
    }
}
