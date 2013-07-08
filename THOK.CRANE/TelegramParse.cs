using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.CRANE
{
    public class TelegramParse
    {
        public static Telegram Parse(string telegram)
        {
            Telegram tgm = new Telegram();
            tgm.STX = telegram.Substring(0, 1);
            tgm.RequestFlag = byte.Parse(telegram.Substring(1, 1));
            tgm.SequenceNo = telegram.Substring(2, 4);
            tgm.Receiver = telegram.Substring(6, 6);
            tgm.Sender = telegram.Substring(12, 6);
            //字串
            tgm.TelegramData = telegram.Substring(18, telegram.Length-21);
            //对象
            tgm.TelegramData1 = DataParse(tgm.TelegramData);
            tgm.CRC = telegram.Substring(telegram.Length - 3, 2);
            tgm.ETX = telegram.Substring(telegram.Length - 1, 1);
            return tgm;
        }
        /// <summary>
        /// Application telegram data解析
        /// </summary>
        /// <param name="telegramData"></param>
        /// <returns></returns>
        public static TelegramData DataParse(string telegramData)
        {
            //指令类型
            TelegramData tgd = new TelegramData();
            string AssignmentType = telegramData.Substring(0, 3);
            tgd.TelegramType = (TelegramType)Enum.Parse(typeof(TelegramType), AssignmentType);
            tgd.CraneNo = telegramData.Substring(3, 2);
            tgd.AssignmenID = telegramData.Substring(5, 8);
            
            switch (AssignmentType)
            {
                case "ACK":
                    tgd.FaultIndicator = telegramData.Substring(13,1);
                    tgd.SequenceNo = telegramData.Substring(14, 4);
                    break;
                case "NCK":
                    tgd.FaultIndicator = telegramData.Substring(13,1);
                    tgd.SequenceNo = telegramData.Substring(14, 4);
                    break;
                case "ACP":
                    tgd.CranePosition = telegramData.Substring(13, 12);
                    tgd.RearForkLeft = telegramData.Substring(25, 2);
                    tgd.RearForkRight = telegramData.Substring(27, 2);
                    tgd.FrontForkLeft = telegramData.Substring(29, 2);
                    tgd.FrontForkRight = telegramData.Substring(31, 2);
                    tgd.ReturnCode = telegramData.Substring(33, 3);
                    break;
                case "DEC":
                    tgd.ReturnCode = telegramData.Substring(13, 3);
                    break;
                case "CSR":
                    tgd.CraneMode = telegramData.Substring(13, 1);
                    tgd.CranePosition = telegramData.Substring(14, 6);
                    tgd.RearForkLeft = telegramData.Substring(20, 2);
                    tgd.RearForkRight = telegramData.Substring(22, 2);
                    tgd.FrontForkLeft = telegramData.Substring(24, 2);
                    tgd.FrontForkRight = telegramData.Substring(26, 2);
                    tgd.CurrentAisle = telegramData.Substring(28, 2);
                    tgd.ReturnCode = telegramData.Substring(30, 3);
                    break;
            }
            return tgd;
        }
    }
}
