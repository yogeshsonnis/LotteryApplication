using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class TicketCollection : BaseHandler
    {
        int box;
        string ticket_ID;
        string pack_No;
        string ticket_Name;
        string open_No;
        string close_No;
        string ticket_Count;
        string ticket_Value;
        string total;
        string state;

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

        public int Box
        {
            get
            {
                return box;
            }
            set
            {
                box = value;
                NotifyPropertyChanged("Box");
            }
        }

        public string Ticket_ID
        {
            get
            {
                return ticket_ID;
            }
            set
            {
                ticket_ID = value;
                NotifyPropertyChanged("Ticket_ID");
            }
        }

        public string Pack_No
        {
            get
            {
                return pack_No;
            }
            set
            {
                pack_No = value;
                NotifyPropertyChanged("Pack_No");
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

        public string Open_No
        {
            get
            {
                return open_No;
            }
            set
            {
                open_No = value;
                NotifyPropertyChanged("Open_No");
            }
        }

        public string Close_No
        {
            get
            {
                return close_No;
            }
            set
            {
                close_No = value;
                NotifyPropertyChanged("Close_No");
            }
        }

        public string Ticket_Count
        {
            get
            {
                return ticket_Count;
            }
            set
            {
                ticket_Count = value;
                NotifyPropertyChanged("Ticket_Count");
            }
        }

        public string Ticket_Value
        {
            get
            {
                return ticket_Value;
            }
            set
            {
                ticket_Value = value;
                NotifyPropertyChanged("Ticket_Value");
            }
        }

        public string Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
                NotifyPropertyChanged("Total");
            }
        }
    }
}
