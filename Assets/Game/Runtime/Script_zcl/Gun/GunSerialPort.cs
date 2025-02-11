using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnA;
using UnityEngine;

namespace UnA
{
    public class GunSerialPort : UnA.SerialPortBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <param name="receiveFrameRate">接收帧率</param>
        public GunSerialPort(string portName, int receiveFrameRate)
        {
            OpenSerialPort(portName, 115200, Parity.None, 8, StopBits.One, receiveFrameRate);
        }

        //打开
        public override void OpenSerialPort(string _portName, int _baudRate, Parity _parity, int _dataBits, StopBits _stopbits, int receiveFrameRate)
        {
            base.OpenSerialPort(_portName, _baudRate, _parity, _dataBits, _stopbits, receiveFrameRate);
        }

        //关闭
        public override void CloseSerialPort()
        {
            base.CloseSerialPort();
        }

        //发送数据
        public override void SendData(byte[] data, int count)
        {
            base.SendData(data, count);
        }

        //接收
        public override void ReceivedDataProcessing(byte[] data, int count)
        {
            ProcessingDataAfterEnablingCommunication(data, count);
            if (data_list.Count > 0)
            {
                HandleData(data_list.ToArray());
            }
            else if (data_dic.Count > 0)
            {
                foreach (var item in data_dic)
                {
                    HandleData(item.Value.ToArray());
                }
            }
            data_list.Clear();
            data_dic.Clear();
        }

        #region 处理开启通信后的数据


        List<byte> data_list = new List<byte>();
        Dictionary<int, List<byte>> data_dic = new Dictionary<int, List<byte>>();
        private void ProcessingDataAfterEnablingCommunication(byte[] bytes, int data_count) //目前针对的是，开启通信时，板子发送很多数据过来的处理
        {
            if (data_count > 30) //出现粘包
            {
                for (int i = 0; i < data_count; i++)
                {
                    if ((i + 1) >= data_count) { return; }
                    if ($"{bytes[i]:x2}".ToUpper() == "AE") //判断是否帧头
                    {
                        int count = data_count - i;
                        byte[] newArray = new byte[count];
                        Array.Copy(bytes, i, newArray, 0, count); //复制一份数据出来
                        int dataLeng = (int)(bytes[i + 1]) + 1; //数据长度
                        List<byte> datalist = new List<byte>();
                        for (int j = 0; j < count; j++) //遍历添加数据
                        {
                            if (j > dataLeng)
                            {
                                if ($"{newArray[j]:x2}".ToUpper() == "FF") //查看是否为真尾，是的话,直接停止，证明已经提取到一条完整的数据了，接着提取后面的
                                {
                                    datalist.Add(newArray[j]);
                                    break;
                                }
                            }
                            datalist.Add(newArray[j]);
                        }
                        data_dic.Add(i, datalist);
                    }
                }
            }
            else
            {
                for (int i = 0; i < data_count; i++) //遍历查找帧头
                {
                    if ((i + 1) >= data_count) { return; }
                    if ($"{bytes[i]:x2}".ToUpper() == "AE") //判断是否帧头
                    {
                        int count = data_count - i;
                        byte[] newArray = new byte[count];
                        Array.Copy(bytes, i, newArray, 0, count); //复制一份数据出来
                        int dataLeng = (int)(bytes[i + 1]) + 1; //数据长度
                        for (int j = 0; j < count; j++) //遍历添加数据
                        {
                            if (j > dataLeng)
                            {
                                if ($"{newArray[j]:x2}".ToUpper() == "FF") //查看是否为真尾，是的话，直接返回，后面的数据不要
                                {
                                    data_list.Add(newArray[j]);
                                    return;
                                }
                            }
                            data_list.Add(newArray[j]);
                        }

                        return;
                    }
                }
            }
        }

        private void HandleData(byte[] data)
        {

            switch (data[1])
            {
                case 19:
                    if (data.Length < data[1]) {  return; }
                    if (data[data.Length - 2] == Checksum(data, 1, data[2], true))
                    {
                        EventMgr.Instance.Emit(GunData.GUNDATAUPDATEPOINTSANDPRESSED, data);
                    }
                    break;
                default:
                    //Debug.Log($"接收的数据：{BytesToStr(data, data.Length)}");
                    break;
            }


            //for (int i = 0; i < data.Length; i++)
            //{
            //    if (i > 7 && i < 10)
            //    {
            //        Debug.Log($"  索引 i : {i}   " + $" 大写 {data[i]:x2}".ToUpper() + $" 纯 byte  {data[i]}  ");
            //    }
            //}
        }

        #endregion



        public override string BytesToStr(byte[] bytes, int length)
        {
            return base.BytesToStr(bytes, length);
        }

        public override byte[] StrToBytes(string message)
        {
            return base.StrToBytes(message);
        }

        #region Calcuta Check Sum

        private byte CheckCrc = 0;
        /// <summary>
        /// 计算校验和数据 通过相加之后去^异或序列号
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public byte Checksum(byte[] data, int start, byte mark,bool isRemoveLeng = false)
        {
            CheckCrc = 0;
            int leng = 0;   
            if (isRemoveLeng)
            {
                leng = data.Length - 2;
            }
            else
            {
                leng = data.Length;
            }

            for (int i = start; i < leng; i++)
            {
                CheckCrc += data[i];
            }
            CheckCrc ^= mark;
            return CheckCrc;
        }



        #endregion
    }

}


