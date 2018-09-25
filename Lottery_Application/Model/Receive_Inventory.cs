using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Lottery_Application.Model
{
    public class Receive_Inventory : BaseHandler
    {
        string game_Id;
        string packet_No;
        string ticket_Name;
        string rate;
        string start_No;
        string status;
        string end_No;
        DatePicker expire_Date;
        int emplyoeeId;
        string state;
        int store_Id;
        int shiftID;
        public int ShiftID
        {
            get
            {
                return shiftID;
            }

            set
            {
                shiftID = value;
                NotifyPropertyChanged("ShiftID");
            }
        }

        public int Store_Id
        {
            get
            {
                return store_Id;
            }

            set
            {
                store_Id = value;
                NotifyPropertyChanged("Store_Id");
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
                NotifyPropertyChanged("State");
            }
        }

        public string Game_Id
        {
            get
            {
                return game_Id;
            }
            set
            {
                game_Id = value;
                NotifyPropertyChanged("Game_Id");
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public string Packet_No
        {
            get
            {
                return packet_No;
            }
            set
            {
                packet_No = value;
                NotifyPropertyChanged("Packet_No");
            }
        }

        public string Ticket_Name
        {
            get
            {
                return ticket_Name;
            }
            set
            {
                ticket_Name = value;
                NotifyPropertyChanged("Ticket_Name");
            }
        }

        public string Rate
        {
            get
            {
                return rate;
            }
            set
            {
                rate = value;
                NotifyPropertyChanged("Rate");
            }
        }

        public string Start_No
        {
            get
            {
                return start_No;
            }
            set
            {
                start_No = value;
                NotifyPropertyChanged("Start_No");
            }
        }

        public string End_No
        {
            get
            {
                return end_No;
            }
            set
            {
                end_No = value;
                NotifyPropertyChanged("End_No");
            }
        }

        public DatePicker Expire_Date
        {
            get
            {
                return expire_Date;
            }
            set
            {
                expire_Date = value;
                NotifyPropertyChanged("Expire_Date");
            }
        }

        public int EmployeeId
        {
            get
            {
                return emplyoeeId;
            }

            set
            {
                emplyoeeId = value;
                NotifyPropertyChanged("EmployeeId");
            }
        }
    }
}
