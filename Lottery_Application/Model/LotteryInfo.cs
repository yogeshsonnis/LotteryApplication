using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class LotteryInfo : BaseHandler
    {
        int price;
        int? box_No;
        string game_Id;
        string packet_No;
        string status;
        string ticket_Name;  
        string start_No;
        string end_No;
        string count;
        string state;
        int? total_Price;
        string stopped_At; 
        int? store_Id;
        int? employee_Id;
        int? settle_Box_No;
        string settle_Status;



        public int? Store_Id
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

        public string Stopped_At
        {
            get
            {
                return stopped_At;
            }

            set
            {
                stopped_At = value;
                NotifyPropertyChanged("Stopped_At");
            }
        }

        public int? Employee_Id
        {
            get
            {
                return employee_Id;
            }

            set
            {
                employee_Id = value;
                NotifyPropertyChanged("Employee_Id");
            }
        }

        public int? Settle_Box_No
        {
            get
            {
                return settle_Box_No;
            }

            set
            {
                settle_Box_No = value;
                NotifyPropertyChanged("Settle_Box_No");
            }
        }

        public string Settle_Status
        {
            get
            {
                return settle_Status;
            }

            set
            {
                settle_Status = value;
                NotifyPropertyChanged("Settle_Status");
            }
        }
    }
}
