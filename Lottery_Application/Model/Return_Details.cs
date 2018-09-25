using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Return_Details : BaseHandler
    {

        int? box_Id;
        int? box_No;
        string game_Id;
        string packet_No;
        string ticket_Name;
        int price;
        string status;
        string start_No;
        string end_No;
        int return_At;
        string state;
        string count;
        int employeeID;
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


        public int Return_At
        {
            get
            {
                return return_At;
            }
            set
            {
                return_At = value;
                NotifyPropertyChanged("Return_At");
            }
        }
        public int? Box_Id
        {
            get
            {
                return box_Id;
            }
            set
            {
                box_Id = value;
                NotifyPropertyChanged("Box_Id");
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
    }
}
