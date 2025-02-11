using System.Linq;
using UnityEngine;

public class GunMgr : UnitySingleton<GunMgr>
{
    /// <summary>
    /// 重置光标位置
    /// </summary>
    public const string ResetCursorsPosEvent = "ResetCursorsPosEvent";
    /// <summary>
    /// 光标位置更新
    /// </summary>
    public const string CursorsPosUpdateEvent = "CursorsPosUpdateEvent";
    /// <summary>
    /// 广播开枪事件
    /// </summary>
    public const string ShootEvent = "ShootEvent";
    /// <summary>
    /// 广播投币事件
    /// </summary>
    public const string CoinAddEvent = "CoinAddEvent";

    //缓存当前光标位置
    Vector2[] cursorPositions;
    // 缓存当前开枪键状态
    bool[] shootStates;

    public void Init()
    {
        this.cursorPositions = new Vector2[GameApp.MAX_PLAYER_COUNT];
        this.shootStates = new bool[GameApp.MAX_PLAYER_COUNT];
        for (var i = 0; i < GameApp.MAX_PLAYER_COUNT; i++)
        {
            this.cursorPositions[i] = Vector2.zero;
            this.shootStates[i] = false;
        }

        EventMgr.Instance.AddListener(GunMgr.ResetCursorsPosEvent, (string uname, object udata) =>
        {
            if (udata == null)
                return;

            var data = (Vector2[])udata;
            if (data.Length < GameApp.MAX_PLAYER_COUNT)
                return;

            this.cursorPositions = data;
        });

        isConnectSerialPort = true;
        gunData = new GunData(GameApp.MAX_PLAYER_COUNT);
        GameObject go = new GameObject("_GunInstruction");
        go.transform.parent = this.transform;
        go.AddComponent<GunInstruction>();
        //是否连接com
        EventMgr.Instance.AddListener(ConnectSerialPort, (_, _) =>
        {
            isConnectSerialPort = false;
        });
        //退出
        EventMgr.Instance.AddListener(BackendPanel.QUITBACKEND, (eventName, obj) =>
        {
            ExitBacken();
        });
        //玩家投币
        EventMgr.Instance.AddListener(GunData.PLAYERPUTINCOINS, (eventName, obj) =>
        {
            Debug.Log($"玩家投币:{obj}");
            EventMgr.Instance.Emit(GunMgr.CoinAddEvent, TestPlayerIndex);
        });
        //玩家开始
        EventMgr.Instance.AddListener(GunData.PLAYERSTARTKEY, (_, data) =>
        {
            EventMgr.Instance.Emit(HomeWindow_UICtrl.PlayerClickStartBtnEvent, (int)data - 1);
        });
    }

    //显示后台
    void ShowBacken()
    {
        if (GameLevelMgr.Instance.IsPlaying)
            return;

        UIMgr.Instance.ShowUIView("GUIPrefabs/Backend");
    }
    // 退出后台
    void ExitBacken()
    {
        UIMgr.Instance.RemoveUIView("GUIPrefabs/Backend");
    }

    void Update()
    {
        TestPlayerAMove();
        BroadcastShootEvent();
    }

    // 测试的玩家序号
    public const int TestPlayerIndex = 0;
    bool keyFlag;
    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowBacken();
        }

        if (!isConnectSerialPort)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.nextBroadcastShootTime = 0;
                this.shootStates[TestPlayerIndex] = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                this.shootStates[TestPlayerIndex] = false;
            }
        }



        // 测试投币事件
        {
            if (Input.GetKey(KeyCode.A))
            {
                //if (this.keyFlag)
                //    return;

                this.keyFlag = true;


                EventMgr.Instance.Emit(GunMgr.CoinAddEvent, TestPlayerIndex);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                this.keyFlag = false;
            }
        }
        // 测试玩家开始事件
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (this.keyFlag)
                    return;

                this.keyFlag = true;


                EventMgr.Instance.Emit(HomeWindow_UICtrl.PlayerClickStartBtnEvent, TestPlayerIndex);
            }
            if (Input.GetKeyUp(KeyCode.B))
            {
                this.keyFlag = false;
            }
        }
        // 测试扣血事件
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (this.keyFlag)
                    return;

                this.keyFlag = true;

                int[] data = new[]
                {
                    TestPlayerIndex,
                    UnityEngine.Random.Range(5, 15)
                };

                EventMgr.Instance.Emit(PlayerInfos_UICtrl.HPDeductEvent, data);
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                this.keyFlag = false;
            }
        }
        //测试自动发炮
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (this.keyFlag)
                    return;

                this.keyFlag = true;


                if (MachineDataMgr.Instance.PlayerShowCount == 2)
                {
                    EventMgr.Instance.Emit(HomeWindow_UICtrl.PlayerClickStartBtnEvent, 1);
                }
                else
                {
                    EventMgr.Instance.Emit(HomeWindow_UICtrl.PlayerClickStartBtnEvent, 1);
                    EventMgr.Instance.Emit(HomeWindow_UICtrl.PlayerClickStartBtnEvent, 2);
                    EventMgr.Instance.Emit(HomeWindow_UICtrl.PlayerClickStartBtnEvent, 3);
                }
                TestAutoFire = true;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                this.keyFlag = false;
            }
        }
        //关闭自动发炮
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (this.keyFlag)
                    return;

                this.keyFlag = true;

                int[] data = new[]
                {
                    1,
                    100
                }; ;

                if (MachineDataMgr.Instance.PlayerShowCount == 2)
                {
                    EventMgr.Instance.Emit(PlayerInfos_UICtrl.HPDeductEvent, data);
                }
                else
                {
                    EventMgr.Instance.Emit(PlayerInfos_UICtrl.HPDeductEvent, data);
                    data[0] = 2;
                    EventMgr.Instance.Emit(PlayerInfos_UICtrl.HPDeductEvent, data);
                    data[0] = 3;
                    EventMgr.Instance.Emit(PlayerInfos_UICtrl.HPDeductEvent, data);
                }
                TestAutoFire = false;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                this.keyFlag = false;
            }
        }
    }


    const float BroadcastShootInterval = 0.01f;  // 开枪时间间隔
    float nextBroadcastShootTime = 0f;          // 下次开枪广播事件
    void BroadcastShootEvent()
    {
        if (TestAutoFire)
        {
            for (int i = 0; i < shootStates.Length; i++)
            {
                shootStates[i] = true;
            }
            nextBroadcastShootTime += Time.deltaTime;
            if (nextBroadcastShootTime > BroadcastShootInterval)
            {
                EventMgr.Instance.Emit(GunMgr.ShootEvent, this.shootStates);
                nextBroadcastShootTime = 0;
            }
            return;
        }

        if (isConnectSerialPort)
        {
            shootStates = gunData.GetPlayerPressed();
        }

        if (Time.time < nextBroadcastShootTime)
            return;

        nextBroadcastShootTime = Time.time + BroadcastShootInterval;

        if (!this.shootStates.Any(t => t))
            return;

        EventMgr.Instance.Emit(GunMgr.ShootEvent, this.shootStates);
    }

    const float MinMove = 1.0f;
    void TestPlayerAMove()
    {
        if (TestAutoFire)
        {
            Vector2[] player = gunData.GetPlayerPoint();

            if (UnitMgr.Instance.activeUnits.Count > MachineDataMgr.Instance.PlayerShowCount)
            {
                for (int i = 0; i < MachineDataMgr.Instance.PlayerShowCount; i++)
                {
                    int index = Random.Range(0, UnitMgr.Instance.activeUnits.Count);
                    player[i] = CameraController.Instance.MainCamera.WorldToScreenPoint(UnitMgr.Instance.activeUnits[index].transform.position);
                    player[i].y += 90;
                }
            }
            else if (UnitMgr.Instance.activeUnits.Count >= 1)
            {
                player[0] = CameraController.Instance.MainCamera.WorldToScreenPoint(UnitMgr.Instance.activeUnits[0].transform.position);
                player[0].y += 90;
            }
            EventMgr.Instance.Emit(GunMgr.CursorsPosUpdateEvent, player);
            return;
        }



        if (!isConnectSerialPort)
        {
            if (!(Mathf.Abs(this.cursorPositions[TestPlayerIndex].x - Input.mousePosition.x) > MinMove) &&
            !(Mathf.Abs(this.cursorPositions[TestPlayerIndex].y - Input.mousePosition.y) > MinMove))
                return;

            this.cursorPositions[TestPlayerIndex] = Input.mousePosition;
            if (this.cursorPositions[TestPlayerIndex].x > Screen.width)
                this.cursorPositions[TestPlayerIndex].x = Screen.width;
            if (this.cursorPositions[TestPlayerIndex].x < 0)
                this.cursorPositions[TestPlayerIndex].x = 0;
            if (this.cursorPositions[TestPlayerIndex].y > Screen.height)
                this.cursorPositions[TestPlayerIndex].y = Screen.height;
            if (this.cursorPositions[TestPlayerIndex].y < 0)
                this.cursorPositions[TestPlayerIndex].y = 0;

            EventMgr.Instance.Emit(GunMgr.CursorsPosUpdateEvent, this.cursorPositions);
        }
        else
        {
            EventMgr.Instance.Emit(GunMgr.CursorsPosUpdateEvent, gunData.GetPlayerPoint());
        }

    }



    #region Gun Data

    private GunData gunData;
    private GunInstruction gunInstruction;
    private bool isConnectSerialPort;
    public const string ConnectSerialPort = "isconnectSerialPort";


    bool TestAutoFire; //测试自动开火

    #endregion
}
