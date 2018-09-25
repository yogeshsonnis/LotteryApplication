using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Activate_Ticket : BaseHandler
    {
        string game_Id;
        string packet_No;
        int? box_No;
        string status;
        string ticket_Name;
        int price;
        bool isNewStore;
        string start_no;
        string end_no;
        string stopped_at;
        int employeeID;
        string state;
        string count;
        int store_Id;
        int? total_Price;
        int shiftID;
        int changeToBox;
        
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

        public string Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                NotifyPropertyChanged("Count");
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
        public string Stopped_At
        {
            get
            {
                return stopped_at;
            }
            set
            {
                stopped_at = value;
                NotifyPropertyChanged("Stopped_At");
            }
        }
        public string Start_No
        {
            get
            {
                return start_no;
            }
            set
            {
                start_no = value;
                NotifyPropertyChanged("Start_No");
            }
        }

        public string End_No
        {
            get
            {
                return end_no;
            }
            set
            {
                end_no = value;
                NotifyPropertyChanged("End_No");
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

        public int? Box_No
        {
            get
            {
                return box_No;
            }
            set
            {
                box_No = value;
                NotifyPropertyChanged("Box_No");
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

        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                NotifyPropertyChanged("Price");
            }
        }

        DateTime created_Date;
        public DateTime Created_Date
        {
            get
            {
                return created_Date;
            }
            set
            {
                created_Date = value;
                NotifyPropertyChanged("Created_Date");
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
                NotifyPropertyChanged("EmployeeID");
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

        public int? Total_Price
        {
            get
            {
                return total_Price;
            }

            set
            {
                total_Price = value;
                NotifyPropertyChanged("Total_Price");
            }
        }

        public int ChangeToBox
        {
            get
            {
                return changeToBox;
            }

            set
            {
                changeToBox = value;
                NotifyPropertyChanged("ChangeToBox");
            }
        }

        public bool IsNewStore
        {
            get
            {
                return isNewStore;
            }

            set
            {
                isNewStore = value;
                NotifyPropertyChanged("IsNewStore");

            }
        }
    }
}
