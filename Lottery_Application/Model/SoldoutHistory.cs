using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class SoldoutHistory : BaseHandler
    {
        int employeeId;        
        int no_of_Tickets_Sold;
        string state;
        string ticket_Name;
        string packet_No;
        string price;
        string startNo;
        string endNo;
        int? box_No;
        int? total_Price;
        string game_Id;
        DateTime created_Date;


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
        public int EmployeeId
        {
            get
            {
                return employeeId;
            }

            set
            {
                employeeId = value;
                NotifyPropertyChanged("EmployeeId");
            }
        }
        public int No_of_Tickets_Sold
        {
            get
            {
                return no_of_Tickets_Sold;
            }

            set
            {
                no_of_Tickets_Sold = value;
                NotifyPropertyChanged("No_of_Tickets_Sold");
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
        public string Price
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
        public string Start_No
        {
            get
            {
                return startNo;
            }

            set
            {
                startNo = value;
                NotifyPropertyChanged("StartNo");
            }
        }
        public string End_No
        {
            get
            {
                return endNo;
            }

            set
            {
                endNo = value;
                NotifyPropertyChanged("EndNo");
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

        public int? Total_Price
        {
            get
            {
                return total_Price;
            }

            set
            {
                total_Price = value;
                NotifyPropertyChanged("Total");
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
    }
}
