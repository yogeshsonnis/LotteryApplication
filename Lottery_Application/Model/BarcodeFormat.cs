using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
  public class BarcodeFormat:BaseHandler
    {
        #region Private Properties
        string username;
        string state;
        int employeeID;
        int barCodeLength;
        int gameIDFrom;
        int gameIDTo;
        int packetIDFrom;
        int packetIDTo;
        int sequenceIDTo;
        int sequenceNoFrom;
        #endregion

        #region Public Properties
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
                // NotifyPropertyChanged("Username");
            }
        }

        public string State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
                // NotifyPropertyChanged("State");
            }
        }

        public int EmployeeID
        {
            get
            {
                return employeeID;
            }

            set
            {
                employeeID = value;
                // NotifyPropertyChanged("EmployeeID");
            }
        }

        public int BarCodeLength
        {
            get
            {
                return barCodeLength;
            }

            set
            {
                barCodeLength = value;
                //NotifyPropertyChanged("TotalLengthofBarcode");
            }
        }

        public int GameIDFrom
        {
            get
            {
                return gameIDFrom;
            }

            set
            {
                gameIDFrom = value;
                // NotifyPropertyChanged("FromGameIDRange");
            }
        }

        public int GameIDTo
        {
            get
            {
                return gameIDTo;
            }

            set
            {
                gameIDTo = value;
                //NotifyPropertyChanged("ToGameIDRange");
            }
        }

        public int PacketIDFrom
        {
            get
            {
                return packetIDFrom;
            }

            set
            {
                packetIDFrom = value;
                // NotifyPropertyChanged("FromPacketIDRange");
            }
        }

        public int PacketIDTo
        {
            get
            {
                return packetIDTo;
            }

            set
            {
                packetIDTo = value;
                // NotifyPropertyChanged("ToPacketIDRange");
            }
        }

        public int SequenceIDTo
        {
            get
            {
                return sequenceIDTo;
            }

            set
            {
                sequenceIDTo = value;

            }
        }

        public int SequenceNoFrom
        {
            get
            {
                return sequenceNoFrom;
            }

            set
            {
                sequenceNoFrom = value;

            }
        }

        #endregion
    }
}
