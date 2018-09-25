using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Master_List_Inventory : BaseHandler
    {
        string game_Id;
        string packet_No;
        string ticket_Name;
        string rate;
        string start_No;
        string end_No;
        string count;
        DateTime date;
        string state;
        int store_Id;
        int employee_Id;


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

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
                NotifyPropertyChanged("Date");
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

        public int Employee_Id
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
    }
}
