using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace UnA
{
    public class Tools
    {
        public static void Init()
        {

        }

        public static string BytesToStr(byte[] bytes, int length)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
            {
                stringBuilder.AppendFormat("{0:x2} ", bytes[i]);
            }
            str = stringBuilder.ToString().ToUpper();
            stringBuilder.Clear();
            return str;
        }

        /// <summary>
        /// 仅支持 FF FF 00 ....类似这样的指令进行转换
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] StrToBytes(string message)
        {
            message = message.Replace(" ", ""); // 移除十六进制字符串中的空格
            byte[] byteArray = new byte[message.Length / 2];

            for (int i = 0; i < byteArray.Length; i++)
            {
                // 每两个字符转换为一个字节 从指定的 i * 2 开始检索 2 个字符 第一次0*2 检索2个字符，第二次1*2=2 检索2位置两个字符，下次就是4
                string hexByte = message.Substring(i * 2, 2);
                byteArray[i] = byte.Parse(hexByte, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return byteArray;
        }
    }
}


