using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.TCP.Util
{
    public class MessageParse
    {

        private Dictionary<string, string> GetParameters(string telegramData)
        {
            
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string AssignmentType = telegramData.Substring(0, 3);
            dictionary.Add("TelegramType",AssignmentType);


            switch (AssignmentType)
            {
                case "ACK":
                case "NCK":
                    dictionary.Add("FaultIndicator", telegramData.Substring(13, 1));
                    dictionary.Add("SequenceNo", telegramData.Substring(14, 4));
                    break;
                case "ACP":
                    dictionary.Add("CraneNo", telegramData.Substring(3, 2));
                    dictionary.Add("AssignmenID", telegramData.Substring(5, 8));
                    dictionary.Add("CranePosition", telegramData.Substring(13, 12));
                    dictionary.Add("RearForkLeft", telegramData.Substring(25, 2));
                    dictionary.Add("RearForkRight", telegramData.Substring(27, 2));
                    dictionary.Add("FrontForkLeft", telegramData.Substring(29, 2));
                    dictionary.Add("FrontForkRight", telegramData.Substring(31, 2));
                    dictionary.Add("ReturnCode", telegramData.Substring(33, 3));
                    break;
                case "DEC":
                    dictionary.Add("ReturnCode", telegramData.Substring(33, 3));
                    break;
                case "CSR":
                    dictionary.Add("CraneNo", telegramData.Substring(3, 2));
                    dictionary.Add("AssignmenID", telegramData.Substring(5, 8));
                    dictionary.Add("CraneMode", telegramData.Substring(13, 1));
                    dictionary.Add("CranePosition", telegramData.Substring(14, 6));
                    dictionary.Add("RearForkLeft", telegramData.Substring(20, 2));
                    dictionary.Add("RearForkRight", telegramData.Substring(22, 2));
                    dictionary.Add("FrontForkLeft", telegramData.Substring(24, 2));
                    dictionary.Add("FrontForkRight", telegramData.Substring(26, 2));
                    dictionary.Add("CurrentAisle", telegramData.Substring(28, 2));
                    dictionary.Add("ReturnCode", telegramData.Substring(30, 3));
                    break;
            }
            return dictionary;
        }

        public Message Parse(string msg)
        {
            Message message = null;
            try
            {
                string sender = msg.Substring(12, 6);
                string command = msg.Substring(18, 3);
                string receiver = msg.Substring(6, 6);
                string telegramData = msg.Substring(18, msg.Length-21);
                Dictionary<string, string> parameters = this.GetParameters(telegramData);
                message = new Message(msg, sender, command, receiver, parameters);
            }
            catch
            {
            }
            return message;
        }
    }
}
