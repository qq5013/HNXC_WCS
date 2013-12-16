using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.CRANE
{
    public class Telegram
    {
        private string _STX = "<";
        private byte _requestFlag = 0;
        private string _sequenceNo = "0000";
        private string _receiver = "CRAN30";
        private string _sender = "THOK01";
        private string _telegramData = "";
        private TelegramData _telegramData1;
        private string _CRC = "00";
        private string _ETX = ">";

        public string STX
        {
            get
            {
                return this._STX;
            }
            set
            {
                this._STX = value;
            }
        }
        public byte RequestFlag
        {
            get
            {
                return this._requestFlag;
            }
            set
            {
                this._requestFlag = value;
            }
        }
        public string SequenceNo
        {
            get
            {
                return this._sequenceNo;
            }
            set
            {
                this._sequenceNo = value;
            }
        }
        public string Receiver
        {
            get
            {
                return this._receiver;
            }
            set
            {
                this._receiver = value;
            }
        }
        public string Sender
        {
            get
            {
                return this._sender;
            }
            set
            {
                this._sender = value;
            }
        }
        public string TelegramData
        {
            get
            {
                return this._telegramData;
            }
            set
            {
                this._telegramData = value;
            }
        }
        public TelegramData TelegramData1
        {
            get
            {
                return this._telegramData1;
            }
            set
            {
                this._telegramData1 = value;
            }
        }
        public string CRC
        {
            get
            {
                return this._CRC;
            }
            set
            {
                this._CRC = value;
            }
        }
        public string ETX
        {
            get
            {
                return this._ETX;
            }
            set
            {
                this._ETX = value;
            }
        }
        public override string ToString()
        {
            return this.STX + this.RequestFlag.ToString() + this.SequenceNo + this.Receiver + this.Sender + this.TelegramData + this.CRC + this.ETX;
        }
    }
}
