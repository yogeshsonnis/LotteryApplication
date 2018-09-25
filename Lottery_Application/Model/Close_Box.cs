using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Close_Box : BaseHandler
    {
        string game_Id;
        string packet_Id;
        int? box_No;
        string status;
        string ticket_Name;
        int price;
        DateTime created_Date;
        string start_No;
        string end_No;
        string close_At;
        string count;
        int? employeeID;
        string state;
        int? total_Price;
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

        public string Packet_Id
        {
            get
            {
                return packet_Id;
            }

            set
            {
                packet_Id = value;
                NotifyPropertyChanged("Packet_Id");
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

        public string Close_At
        {
            get
            {
                return close_At;
            }

            set
            {
                close_At = value;
                NotifyPropertyChanged("Close_At");
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

        public int? EmployeeID
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
    }
}
