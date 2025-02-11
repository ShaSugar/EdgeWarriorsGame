using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using UnityEngine;

namespace UnA
{
    public class SerialPortBase
    {
        protected SerialPort serialPort;
        protected Thread serialPort_Received; //接收消息线程
        protected byte[] data;

        protected string portName; //端口名称
        protected int baudRate; //波特率
        protected Parity parity; //校验位
        protected int dataBits; //数据位
        protected StopBits stopbits; //停止位
        protected bool shouldStop;
        protected int receiveFrameRate; //接收帧率


        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="_portName">端口名称</param>
        /// <param name="_baudRate">波特率 115200比特每秒,波特率指的是数据传输的速度。</param>
        /// <param name="_parity">校验位</param>
        /// <param name="_dataBits">数据位</param>
        /// <param name="_stopbits">停止位</param>
        /// <param name="receiveFrameRate">接收帧率</param>
        public virtual void OpenSerialPort(string _portName, int _baudRate, Parity _parity, int _dataBits, StopBits _stopbits, int receiveFrameRate)
        {
            try
            {
                CloseSerialPort();
                serialPort = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopbits);
                serialPort.Encoding = Encoding.UTF8; // 指定编码方式
                serialPort.ReadTimeout = 500;
                serialPort.WriteTimeout = 500;
                serialPort.Open();
                serialPort.RtsEnable = true;
                data = new byte[1024];
                shouldStop = false;
                this.receiveFrameRate = receiveFrameRate;

                serialPort_Received = new Thread(SerialPortReceivedData);
                serialPort_Received.IsBackground = true;
                serialPort_Received.Start();

            }
            catch (Exception e)
            {
                Debug.Log($"连接失败:{e.Message}");
                EventMgr.Instance.Emit(GunMgr.ConnectSerialPort, false);
            }
        }

        //关闭
        public virtual void CloseSerialPort()
        {
            serialPort?.Close();
            shouldStop = true;
            if (serialPort_Received != null)
            {
                if (!serialPort_Received.Join(100))
                {
                    serialPort_Received.Interrupt();
                    serialPort_Received.Abort();
                }
            }
        }

        #region Send SerialPort

        public virtual void SendData(byte[] data, int count)
        {
            SerialPortSendData(data, 0, count);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset">偏移量，从哪里发</param>
        /// <param name="count"></param>
        private void SerialPortSendData(byte[] data, int offset, int count)
        {
            try
            {
                if (shouldStop)
                {
                    return;
                }

                if (!serialPort.IsOpen)
                {
                    OpenSerialPort(portName, baudRate, parity, dataBits, stopbits, receiveFrameRate);
                    Debug.Log("连接已断开...重连中...");
                }

                serialPort.Write(data, offset, count);
            }
            catch (Exception e)
            {
                Debug.LogError($"出现无法处理的数据发送错误： {e.Message}");
            }
        }

        #endregion

        #region Receive SerialPort

        //接收数据线程
        private void SerialPortReceivedData()
        {
            try
            {
                while (true)
                {
                    if (shouldStop)
                    {
                        break;
                    }

                    DwkUnityMainThreadDispatcher.Instance.Enqueue(() => {
                        ReceiveHandle();
                    });

                    Thread.Sleep(1000 / receiveFrameRate);
                }
            }
            catch (TimeoutException)
            {
                Debug.Log("读取超时");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        //接收处理
        private void ReceiveHandle()
        {
            if (serialPort.IsOpen)
            {
                int count = serialPort.BytesToRead; //字节读取的长度
                if (count > 0)
                {
                    if (count >= data.Length)
                    {
                        count = data.Length - 1;
                        Debug.Log("大于接收的数组");
                    }
                    serialPort.Read(data, 0, count);
                    ReceivedDataProcessing(data, count); //处理数据
                    serialPort.DiscardOutBuffer(); //清空发送缓冲区数据
                    serialPort.DiscardInBuffer(); //清空接收缓冲区数据
                    Array.Clear(data, 0, count);
                }
                else
                {
                    Debug.Log("没有数据读取");
                }
            }
            else
            {
                OpenSerialPort(portName, baudRate, parity, dataBits, stopbits, receiveFrameRate);
                Thread.Sleep(3000);
                if (!serialPort.IsOpen)
                {
                    Debug.Log($"连接已断开...无法通信...");
                }
            }
        }
        //数据处理
        public virtual void ReceivedDataProcessing(byte[] data, int count) { }

        #endregion


        public virtual string BytesToStr(byte[] bytes, int length)
        {
            return Tools.BytesToStr(bytes, length);
        }

        public virtual byte[] StrToBytes(string message)
        {
            return Tools.StrToBytes(message);
        }

        public virtual string[] SerialScanningPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}



