
using System;
using System.Collections;
using System.Text;
using UnA;
using UnityEngine;

/*
 

被动接收

AE 索引 0 帧头
13 索引 1 数据长度
52 索引 2 序列号
01 索引 3 通信类型

03 索引 4 
01 索引 5 写数据
02 索引 6 ad类型
04 索引 7 4个ad数量

//应该是第一个玩家的xy
X轴：FF 0F 索引 8、9    45-47 0C
Y轴：FF 0F 索引 10、11

//应该是第二个玩家的xy
X轴：3D 05 索引 12、13
Y轴：D1 01 索引 14、15

01 索引 16 按键类型 电平输入
0C 索引 17 按键12个数量

一个字节 8 个位 表示8个按键  两个字节一共十六个按键
00 索引 18 
04 索引 19  

E3 索引 20 校验
FF 索引 21 帧尾
 


AE 13 53 01 03 01 02 04 FF 0F FF 0F 3D 05 D1 01 01 0C 00 04 E1 FF
AE 13 54 01 03 01 02 04 FF 0F FF 0F 3D 05 D0 01 01 0C 00 00 FA FF

//主动发送
ae 索引 0 帧头
11 索引 1 数据长度
d8 索引 2 序列号
00 索引 3 通信类型

04 索引 4 
01 索引 5 

03 索引 6 开关类型 
0e 索引 7 灯数量

8  9
01 c0 灯开关数据 也是按位计算  

04 索引 10 动感类型
06 索引 11 动感数量

//动感
09 89 01 玩家一 三个动感
01 01 01 玩家二 三个动感

b8 
ff








         */

public class GunInstruction : MonoBehaviour
{
    private GunSerialPort gunSerialPort;

    private bool canShoot;
    #region Unity Action

    private void Awake()
    {
        //是否可以射击
        EventMgr.Instance.AddListener(PlayerInfos_UICtrl.IsCanShootEvent, (_, udata) =>
        {
            if (udata == null)
                return;

            this.canShoot = (bool)udata;
        });

        //玩家没按
        EventMgr.Instance.AddListener(GunData.PLAYERTRIGGERNOPRESSED, (_, data) =>
        {
            bitArray[(int)data - 1] = 0;
            byte[] reset = GenerateInstructions((int)data, 0, false);
            gunSerialPort.SendData(reset, reset.Length);
        });

        //玩家单按
        EventMgr.Instance.AddListener(GunData.PLAYERTRIGGERPRESSED, (_, data) =>
        {
            if (!canShoot) return;
            bitArray[(int)data - 1] = 1;
            byte[] send = GenerateInstructions((int)data, 1, false);
            gunSerialPort.SendData(send, send.Length);
        });

        //玩家双按
        EventMgr.Instance.AddListener(GunData.PLAYERPRESSTOGETHER, (_, data) =>
        {
            if (!canShoot) return;
            byte[] reset = GenerateInstructions((int)data, 0, true);
            gunSerialPort.SendData(reset, reset.Length);

            bitArray[(int)data - 1] = 1;
            byte[] send = GenerateInstructions((int)data, 8, true);
            gunSerialPort.SendData(send, send.Length);
        });

        StartCoroutine(InitData());
    }

    private void Start()
    {
        gunSerialPort = new GunSerialPort("COM4", 20);

        byte[] instruction01 = new byte[8];
        instruction01[0] = 0xAE; 
        instruction01[1] = 0x05; 
        instruction01[2] = 0x01; 
        instruction01[3] = 0x00; 
        instruction01[4] = 0x00;
        instruction01[5] = 0x00;
        instruction01[6] = gunSerialPort.Checksum(instruction01, 1, instruction01[2]);
        instruction01[7] = 0xFF;
        gunSerialPort.SendData(instruction01, instruction01.Length);

        byte[] instruction02 = new byte[12];
        instruction02[0] = 0xAE; //帧头
        instruction02[1] = 0x09; //长度
        instruction02[2] = 0x02; //序列号
        instruction02[3] = 0x00; //通信类型
        instruction02[4] = 0x00;
        instruction02[5] = 0x01;
        instruction02[6] = 0x04;
        instruction02[7] = 0x03;
        instruction02[8] = 0x02;
        instruction02[9] = 0x01;
        instruction02[10] = gunSerialPort.Checksum(instruction02, 1, instruction02[2]);
        instruction02[11] = 0xFF;
        gunSerialPort.SendData(instruction02, instruction02.Length);
    }

    private void OnDestroy()
    {
        StartOrStopIO(1);
        gunSerialPort?.CloseSerialPort();
    }

    private void OnApplicationQuit()
    {
        StartOrStopIO(1);
        gunSerialPort?.CloseSerialPort();
    }

    #endregion

    #region 指令生成

    private byte[] LightandMotion_data;
    private byte seq; //序列
    private int[] bitArray; //开对应灯  第一个灯是玩家一   第二个灯是玩家二
    private StringBuilder stringBuilder;
    IEnumerator InitData()
    {
        LightandMotion_data = new byte[20];
        bitArray = new int[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 };
        stringBuilder = new StringBuilder();
        yield return null;
        LightandMotion_data[0] = 0xAE; //帧头
        LightandMotion_data[1] = 0x11; //长度
        LightandMotion_data[2] = seq; //序列号
        LightandMotion_data[3] = 0x00; //通信类型
        LightandMotion_data[4] = 0x04; 
        LightandMotion_data[5] = 0x01;
        LightandMotion_data[6] = 0x03; //开关类型
        LightandMotion_data[7] = 0x0E; //灯数量

        LightandMotion_data[8] = 0x00; //灯数据
        LightandMotion_data[9] = 0xC0;

        LightandMotion_data[10] = 0x04; //动感类型
        LightandMotion_data[11] = 0x06; //动感数量

        //玩家一
        LightandMotion_data[12] = 0x01; //动感1
        LightandMotion_data[13] = 0x01; //动感2
        LightandMotion_data[14] = 0x01; //动感3

        //玩家二
        LightandMotion_data[15] = 0x01; //动感1
        LightandMotion_data[16] = 0x01; //动感2
        LightandMotion_data[17] = 0x01; //动感3
         
        LightandMotion_data[18] = gunSerialPort.Checksum(LightandMotion_data, 1, LightandMotion_data[2]);
        LightandMotion_data[19] = 0xFF; //动感3
        yield return null;
    }

    private void StartOrStopIO(byte io)
    {
        byte[] instruction03 = new byte[9];
        instruction03[0] = 0xAE; //帧头
        instruction03[1] = 0x06; //长度
        instruction03[2] = 0x01; //序列号
        instruction03[3] = 0x00; //通信类型
        instruction03[4] = 0x05;
        instruction03[5] = 0x01;
        instruction03[6] = io;
        instruction03[7] = gunSerialPort.Checksum(instruction03, 1, instruction03[2], false);
        instruction03[8] = 0xFF;
        gunSerialPort.SendData(instruction03, instruction03.Length);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player">指定玩家</param>
    /// <param name="gearLevel">挡位</param>
    /// <param name="isDoubleShot">单双发</param>
    /// <returns></returns>
    private byte[] GenerateInstructions(int player, int gearLevel, bool isDoubleShot)
    {
        seq += 1;
        LightandMotion_data[2] = seq; //序列号
        stringBuilder.Clear();  
        //修改灯
        for (int i = bitArray.Length - 1; i >= 0; i--)
        {
            stringBuilder.Append(bitArray[i]);
        }
        // 从二进制字符串还原成字节
        byte[] restoredBytes = ConvertFromBinaryString(stringBuilder.ToString());
        LightandMotion_data[8] = restoredBytes[1];
        LightandMotion_data[9] = restoredBytes[0];

        if (player == 1)
        {
            LightandMotion_data[12] = GenerateGearByte(gearLevel, isDoubleShot); //动感1
        }
        else if (player == 2) 
        {
            LightandMotion_data[15] = GenerateGearByte(gearLevel, isDoubleShot); //动感1
        }

        LightandMotion_data[18] = gunSerialPort.Checksum(LightandMotion_data, 1, LightandMotion_data[2],true);
        return LightandMotion_data;
    }

    // 将二进制字符串还原为字节数组
    private byte[] ConvertFromBinaryString(string binaryString)
    {
        // 处理并拆分成两个字节
        byte byte1 = Convert.ToByte(binaryString.Substring(0, 8), 2);
        byte byte2 = Convert.ToByte(binaryString.Substring(8, 8), 2);

        return new byte[] { byte1, byte2 };
    }

    private byte GenerateGearByte(int gearLevel, bool isDoubleShot)
    {
        // 检查档位是否在有效范围内（0 到 8 档）
        if (gearLevel < 0 || gearLevel > 8)
        {
            Debug.LogError("档位只能在 0 到 8 之间");
            return 0;
        }

        // 确定个位值：0 表示单发，9 表示双发
        int singleOrDouble = isDoubleShot ? 9 : 1;

        // 计算最终字节值
        int gearByte = gearLevel * 0x10 + singleOrDouble;
        return (byte)gearByte;
    }



    #endregion

}








