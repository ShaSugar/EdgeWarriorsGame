using System;
using System.Collections;
using System.Collections.Generic;
using UnA;
using UnityEngine;

public class GunData
{
    public const string GUNDATAUPDATEPOINTSANDPRESSED = "GUNDATAUPDATEPOINTSANDPRESSED"; //更新处理玩家基础数据
    /// <summary>
    /// 玩家投币
    /// </summary>
    public const string PLAYERPUTINCOINS = "PLAYERPUTINCOINS"; 
    /// <summary>
    /// 玩家开始按键
    /// </summary>
    public const string PLAYERSTARTKEY = "PLAYERSTARTKEY";
    /// <summary>
    /// 玩家没按
    /// </summary>
    public const string PLAYERTRIGGERNOPRESSED = "PLAYERTRIGGERNOPRESSED";
    /// <summary>
    /// 玩家单按
    /// </summary>
    public const string PLAYERTRIGGERPRESSED = "PLAYERTRIGGERPRESSED";
    /// <summary>
    /// 玩家双按
    /// </summary>
    public const string PLAYERPRESSTOGETHER = "PLAYERPRESSTOGETHER"; 
    private DataPacket dataPacket; 

    public GunData(int playerCount)
    {
        dataPacket = new DataPacket(playerCount);
        EventMgr.Instance.AddListener(GUNDATAUPDATEPOINTSANDPRESSED, (_, data) =>
        {
            dataPacket.UpdatePlayerBaseData((byte[])data);
        });

    }

    ~GunData()
    {

    }

    public Vector2[] GetPlayerPoint()
    {
        return dataPacket.GetPlayerPoint();
    }

    public bool[] GetPlayerPressed()
    {
        return dataPacket.GetPlayerPressed();
    }

}

public class DataPacket
{
    private PlayerData[] playerData;
    private int playerCount;
    private Vector2[] playerPoint;
    private bool[] playerPressed;
    private int[] keybit;

    private bool IsPutInCoins; //是否投币
    private float putInConinsTimer; //计时器
    public DataPacket(int _playerCount)
    {
        playerCount = _playerCount;
        playerData = new PlayerData[playerCount];
        playerPoint = new Vector2[playerCount];
        playerPressed = new bool[playerCount];
        keybit = new int[16];
        for (int i = 0; i < playerCount; i++)
        {
            playerData[i] = new PlayerData(i + 1);
            playerPoint[i] = Vector2.zero;
            playerPressed[i] = false;
        }

        IsPutInCoins = false;
        putInConinsTimer = 0;
    }
    
    //更新玩家基础数据
    public void UpdatePlayerBaseData(byte[] data)
    {
        for (int i = 0; i < playerData.Length; i++)
        {
            playerData[i].Update();
        }

        if (data.Length < 20) { return; }

        int offset = 8; 
        //更新位置
        for (int i = 0; i < playerCount; i++)
        {   
            if (i > 1)
            {
                playerData[i].UpdatePosition(0, 0);
            }
            else
            {
                // 8 + 4 = 12
                if (offset + 4 >= data.Length)
                {
                    Debug.Log($"数据超出范围！{offset}  {data.Length}");
                    return;
                }
                //data[offset + 1] 8 + 1 = 9   data[offset] 8
                int x = (data[offset + 1] << 8) + data[offset] - 4095;
                //data[offset + 3] 8 + 3 = 11  data[offset] 10
                int y = (data[offset + 3] << 8) + data[offset + 2];

                playerData[i].UpdatePosition(x + 1000, y - 450); //更新位置
                offset += 4; //每个玩家数据4个字节

            }
            
        }


        offset += 2;
        byte byte1 = data[offset + 1];
        byte byte2 = data[offset];
        int combined = (byte1 << 8) | byte2; // 将两个字节合并成16位的整数
        // 输出16个bit位
        for (int i = 0; i < 16; i++)
        {
            int bit = (combined >> i) & 1; // 通过按位操作获取每一位
            keybit[i] = bit;
        }
        //玩家1 左 1 右 2 开始 3
        //玩家2 左 5 右 6 开始 7
        //投币 11 共用

        UpdatePlayerInput(0, 0, 1, 2); // 玩家一
        UpdatePlayerInput(1, 4, 5, 6); // 玩家二

        if (IsPutInCoins)
        {
            putInConinsTimer += Time.deltaTime;
            if (putInConinsTimer > 0.2f && keybit[10] == 0)
            {
                IsPutInCoins = false;
                putInConinsTimer = 0;
            }
        }
        else
        {
            if (keybit[10] == 1) //投币
            {
                EventMgr.Instance.Emit(GunData.PLAYERPUTINCOINS, null);
                IsPutInCoins = true;
            }
        }
    }

    //更新玩家输入
    private void UpdatePlayerInput(int playerIndex, int key1, int key2, int startKey)
    {
        // 判断玩家是否同时按下两个按键
        if (keybit[key1] == 1 && keybit[key2] == 1)
        {
            playerData[playerIndex].PressTogether(); //双按
            playerData[playerIndex].IsTriggerPressed = true;
            playerData[playerIndex].IsPressTogether = true;
            playerData[playerIndex].IsTriggerNoPressed = true;
        }
        else if (keybit[key1] == 1 || keybit[key2] == 1) //任意按
        {
            playerData[playerIndex].TriggerPressed(); //单按
            playerData[playerIndex].IsTriggerPressed = true;
            playerData[playerIndex].IsPressTogetherUp = true;
            playerData[playerIndex].IsTriggerNoPressed = true;
        }
        else //没按
        {
            playerData[playerIndex].TriggerNoPressed(); //没按
            playerData[playerIndex].IsTriggerPressed = false;
            playerData[playerIndex].IsPressTogetherUp = true;
            playerData[playerIndex].IsTriggerNoPressedUp = true;
        }

        // 判断玩家是否按下开始键
        if (keybit[startKey] == 1)
        {
            playerData[playerIndex].TriggerStartKey();
            playerData[playerIndex].startKey = true;
        }
        else
        {
            playerData[playerIndex].startKeyUp = true;
        }
    }

    public Vector2[] GetPlayerPoint()
    {
        for (int i = 0; i < playerCount; i++)
        {
            playerPoint[i] = playerData[i].point;
        }
        return playerPoint;
    }

    public bool[] GetPlayerPressed()
    {
        for (int i = 0; i < playerCount; i++)
        {
            playerPressed[i] = playerData[i].IsTriggerPressed;
        }
        return playerPressed;
    }
}


public class PlayerData
{
    public int playerID;
    public Vector3 point;

    public bool IsTriggerNoPressedUp; // 没按扳机值
    public bool IsTriggerNoPressed;
    private float triggerNoPressedTimer;

    public bool IsTriggerPressed; // 单按扳机值

    public bool IsPressTogether; //双按
    public bool IsPressTogetherUp; //双按抬起
    private float pressTogetherTimer;

    public bool startKey; //开始键
    public bool startKeyUp; //开始键抬起
    private float startKeyTimer; //计时器

    public PlayerData(int playerID)
    {
        point = Vector3.zero;
        this.playerID = playerID;

        IsPressTogether = false;
        IsPressTogetherUp = false;
        startKey = false;
        startKeyUp = false;
        startKeyTimer = 0;
    }

    public void Update()
    {
        if (startKey) //开始
        {
            startKeyTimer += Time.deltaTime;
            if (startKeyTimer > 0.2f && startKeyUp)
            {
                startKey = false;
                startKeyUp = false;
                startKeyTimer = 0;
            }
        }

        if (IsTriggerNoPressedUp && IsTriggerNoPressed) //没按
        {
            triggerNoPressedTimer += Time.deltaTime;
            if (triggerNoPressedTimer > 0.1f)
            {
                IsTriggerNoPressed = false;
                IsTriggerNoPressedUp = false;
                triggerNoPressedTimer = 0;
            }
        }

        if (IsPressTogether) //双
        {
            pressTogetherTimer += Time.deltaTime;
            if (pressTogetherTimer > 0.2f && IsPressTogetherUp)
            {
                IsPressTogether = false;
                IsPressTogetherUp = false;
                pressTogetherTimer = 0;
            }
        }

    }

    //更新玩家xy位置
    public void UpdatePosition(int x, int y)
    {
        // 使用比例系数或偏移量调整方向
        float adjustedX = x * -1f; // 乘以负数来调整方向
        float adjustedY = y;
        if (Mathf.Abs(adjustedX) > 1900)
        {
            adjustedX = 1900;
        }

        if (Mathf.Abs(adjustedY) > 1200)
        {
            adjustedY = 1050;
        }

        point.x = adjustedX;
        point.y = adjustedY;

        //Debug.Log($"{adjustedX}  {adjustedY}");
    }

    //没按
    public void TriggerNoPressed()
    {
        if (IsTriggerNoPressed)
        {
            EventMgr.Instance.Emit(GunData.PLAYERTRIGGERNOPRESSED, playerID);
        }
    }

    //单按
    public void TriggerPressed()
    {
        EventMgr.Instance.Emit(GunData.PLAYERTRIGGERPRESSED, playerID);
    }

    //双按
    public void PressTogether()
    {
        if (!IsPressTogether && IsPressTogetherUp)
        {
            EventMgr.Instance.Emit(GunData.PLAYERPRESSTOGETHER, playerID);
        }
    }

    //触发玩家开始
    public void TriggerStartKey()
    {
        if (!startKey && startKeyUp)
        {
            EventMgr.Instance.Emit(GunData.PLAYERSTARTKEY, playerID);
        }
    }
}
