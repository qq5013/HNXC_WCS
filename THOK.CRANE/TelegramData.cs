using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THOK.CRANE
{
    public class TelegramData
    {
        private TelegramType _telegramType = TelegramType.ACK;
        private string _craneNo = "01";
        private string _assignmenID = "00000000";
        //MMRRRSSSHHDD
        //RRR RACK 货架编号
        //SSS 横向坐标
        //HH  高度
        //DD  01
        //出入库站台RRR定义
        private string _startPosition = "300000000000";
        private string _destinationPosition = "300000000000";

        private string _craneMode = "1";
        private string _cranePosition = "300000000000";
        private string _rearForkLeft = "UL";
        private string _rearForkRight = "UL";
        private string _frontForkLeft = "UL";
        private string _frontForkRight = "UL";
        private string _faultIndicator = "0";
        private string _sequenceNo = "0000";
        private string _currentAisle = "00";
        private string _returnCode = "000";

        public TelegramType TelegramType
        {
            get
            {
                return this._telegramType;
            }
            set
            {
                this._telegramType = value;
            }
        }
        public string CraneNo
        {
            get
            {
                return this._craneNo;
            }
            set
            {
                this._craneNo = value;
            }
        }
        public string AssignmenID
        {
            get
            {
                return this._assignmenID;
            }
            set
            {
                this._assignmenID = value;
            }
        }
        public string StartPosition
        {
            get
            {
                return this._startPosition;
            }
            set
            {
                this._startPosition = value;
            }
        }
        public string DestinationPosition
        {
            get
            {
                return this._destinationPosition;
            }
            set
            {
                this._destinationPosition = value;
            }
        }
        public string CraneMode
        {
            get
            {
                return this._craneMode;
            }
            set
            {
                this._craneMode = value;
            }
        }
        public string CranePosition
        {
            get
            {
                return this._cranePosition;
            }
            set
            {
                this._cranePosition = value;
            }
        }
        public string RearForkLeft
        {
            get
            {
                return this._rearForkLeft;
            }
            set
            {
                this._rearForkLeft = value;
            }
        }
        public string RearForkRight
        {
            get
            {
                return this._rearForkRight;
            }
            set
            {
                this._rearForkRight = value;
            }
        }
        public string FrontForkLeft
        {
            get
            {
                return this._frontForkLeft;
            }
            set
            {
                this._frontForkLeft = value;
            }
        }
        public string FrontForkRight
        {
            get
            {
                return this._frontForkRight;
            }
            set
            {
                this._frontForkRight = value;
            }
        }
        public string FaultIndicator
        {
            get
            {
                return this._faultIndicator;
            }
            set
            {
                this._faultIndicator = value;
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
        public string CurrentAisle
        {
            get
            {
                return this._currentAisle;
            }
            set
            {
                this._currentAisle = value;
            }
        }
        public string ReturnCode
        {
            get
            {
                return this._returnCode;
            }
            set
            {
                this._returnCode = value;
            }
        }        
    }
}
