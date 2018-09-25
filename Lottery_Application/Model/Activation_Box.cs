using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Lottery_Application.Model
{
    public class Activation_Box : BaseHandler
    {
        int? box_Id;
        int? box_No;
        string game_Id;
        string packet_No;
        string ticket_Name;
        int price;
        string start_no;
        string end_no;
        string stopped_at;
        string return_At;
        string status;
        string count;
        int? total_Price;
        string state;
        string partial_Packet;
        int employeeId;
        int store_Id;
        int shiftID;
        int changeToBox;
        DateTime activation_Date;
    
        int settlementDays;

        public DateTime Activation_Date
        {
            get
            {
                return activation_Date;
            }

            set
            {
                activation_Date = value;
                NotifyPropertyChanged("Ativation_Date");
            }
        }
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
      

        public string Return_At
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

        public DateTime Created_Date { get; set; }

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

        public SolidColorBrush BackColor
        {
            get
            {
                return backColor;
            }

            set
            {
                backColor = value;
                NotifyPropertyChanged("BackColor");
            }
        }


        public bool IsScanned
        {
            get
            {
                return isScanned;
            }

            set
            {
                isScanned = value;
                NotifyPropertyChanged("IsScanned");
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

        public string Partial_Packet
        {
            get
            {
                return partial_Packet;
            }

            set
            {
                partial_Packet = value;
                NotifyPropertyChanged("Partial_Packet");
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

        public int SettlementDays
        {
            get
            {
                return settlementDays;
            }

            set
            {
                settlementDays = value;
                NotifyPropertyChanged("SettlementDays");
            }
        }

        SolidColorBrush backColor;

        bool isScanned;

        public Activation_Box()
        {
            SolidColorBrush s = new SolidColorBrush();
            s.Color = Color.FromArgb(255, 153, 204, 51);
            BackColor = new SolidColorBrush(s.Color);
        }
        public Activation_Box(string gameid)
        {

        }
    }
}
