using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Service.UDP
{
    public class MessageParse: THOK.MCP.IProtocolParse
    {
        public Message Parse(string msg)
        {
            Message result = null;
            THOK.UDP.Util.MessageParser parser = new THOK.UDP.Util.MessageParser();
            try
            {
                THOK.UDP.Message message = parser.Parse(msg);
                result = new Message(true, msg, message.Command, message.Parameters);
            }
            catch
            {
                result = new Message(msg);
            }
            return result;
        }
    }
}
