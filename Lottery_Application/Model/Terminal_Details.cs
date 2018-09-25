using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Terminal_Details : BaseHandler
    {
        #region Properties
        string scratchSells;
        string scratchPayout;
        string onlineSells;
        string onlinePayout;
        string loan;
        int credit;
        int debit;
        int topUp;
        int topUPCancel;
        string totalSells;
        string totalPayout;
        string netCash;
        int? store_Id;
        int employeeID;
        string issuedInventory;
        string instockInventory;
        string activeInventory;
        string total;
        string short1;
        string over;
        string shortOver;
        string shortOverStock;
        string shortOverActive;
        string totalStockInventory;
        string totalActiveInventory;
        string countTerminalActiveReceive;
        string trackedAmount;
       string cashOnHand;
        int countActive;
        int countRecevied;
        int totalActiveReceviedStock;
        int shiftID;
        DateTime date;
        DayOfWeek day;
        string closeTime;

        DateTime hamburgerFromDateOk;
        DateTime hamburgerToDateOk;

        #endregion

        #region Public Properties
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
        public string ScratchSells
        {
            get
            {
                return scratchSells;
            }

            set
            {
                scratchSells = value;
                NotifyPropertyChanged("ScratchSells");
            }
        }
        public string ScratchPayout
        {
            get
            {
                return scratchPayout;
            }

            set
            {
                scratchPayout = value;
                NotifyPropertyChanged("ScratchPayout");
            }
        }
        public string OnlineSells
        {
            get
            {
                return onlineSells;
            }

            set
            {
                onlineSells = value;
                NotifyPropertyChanged("OnlineSells");
            }
        }
        public string OnlinePayout
        {
            get
            {
                return onlinePayout;
            }

            set
            {
                onlinePayout = value;
                NotifyPropertyChanged("OnlinePayout");
            }
        }
        public string Loan
        {
            get
            {
                return loan;
            }

            set
            {
                loan = value;
                NotifyPropertyChanged("Loan");
            }
        }
        
        public int Credit
        {
            get
            {
                return credit;
            }

            set
            {
                credit = value;
                NotifyPropertyChanged("Credit");
            }
        }

        public int Debit
        {
            get
            {
                return debit;
            }

            set
            {
                debit = value;
                NotifyPropertyChanged("Debit");
            }
        }

        public int TopUp
        {
            get
            {
                return topUp;
            }

            set
            {
                topUp = value;
                NotifyPropertyChanged("TopUp");
            }
        }

        public int TopUPCancel
        {
            get
            {
                return topUPCancel;
            }

            set
            {
                topUPCancel = value;
                NotifyPropertyChanged("TopUPCancel");
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

        

        public string IssuedInventory
        {
            get
            {
                return issuedInventory;
            }

            set
            {
                issuedInventory = value;
                NotifyPropertyChanged("IssuedInventory");
            }
        }

        public string InstockInventory
        {
            get
            {
                return instockInventory;
            }

            set
            {
                instockInventory = value;
                NotifyPropertyChanged("InstockInventory");
            }
        }

        public string ActiveInventory
        {
            get
            {
                return activeInventory;
            }

            set
            {
                activeInventory = value;
                NotifyPropertyChanged("ActiveInventory");
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

        public string ShortOver
        {
            get
            {
                return shortOver;
            }

            set
            {
                shortOver = value;
                NotifyPropertyChanged("ShortOver");
            }
        }

        public string ShortOverStock
        {
            get
            {
                return shortOverStock;
            }

            set
            {
                shortOverStock = value;
                NotifyPropertyChanged("ShortOverStock");
            }
        }

        public string ShortOverActive
        {
            get
            {
                return shortOverActive;
            }

            set
            {
                shortOverActive = value;
                NotifyPropertyChanged("ShortOverActive");
            }
        }

        public string TotalStockInventory
        {
            get
            {
                return totalStockInventory;
            }

            set
            {
                totalStockInventory = value;
                NotifyPropertyChanged("TotalStockInventory");
            }
        }

        public string TotalActiveInventory
        {
            get
            {
                return totalActiveInventory;
            }

            set
            {
                totalActiveInventory = value;
                NotifyPropertyChanged("TotalActiveInventory");
            }
        }

        public string TrackedAmount
        {
            get
            {
                return trackedAmount;
            }

            set
            {
                trackedAmount = value;
                NotifyPropertyChanged("TrackedAmount");
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

        public string TotalSells
        {
            get
            {
                return totalSells;
            }

            set
            {
                totalSells = value;
                NotifyPropertyChanged("TotalSells");
            }
        }

        public string TotalPayout
        {
            get
            {
                return totalPayout;
            }

            set
            {
                totalPayout = value;
                NotifyPropertyChanged("TotalPayout");
            }
        }

        public string NetCash
        {
            get
            {
                return netCash;
            }

            set
            {
                netCash = value;
                NotifyPropertyChanged("NetCash");
            }
        }

        public string CountTerminalActiveReceive
        {
            get
            {
                return countTerminalActiveReceive;
            }

            set
            {
                countTerminalActiveReceive = value;
                NotifyPropertyChanged("CountTerminalActiveReceive");
            }
        }

        public int CountActive
        {
            get
            {
                return countActive;
            }

            set
            {
                countActive = value;
                NotifyPropertyChanged("CountActive");
            }
        }

        public int CountRecevied
        {
            get
            {
                return countRecevied;
            }

            set
            {
                countRecevied = value;
                NotifyPropertyChanged("CountRecevied");
            }
        }

        public int TotalActiveReceviedStock
        {
            get
            {
                return totalActiveReceviedStock;
            }

            set
            {
                totalActiveReceviedStock = value;
                NotifyPropertyChanged("TotalActiveReceviedStock");
            }
        }

        public string CashOnHand
        {
            get
            {
                return cashOnHand;
            }

            set
            {
                cashOnHand = value;
                NotifyPropertyChanged("CashOnHand");
            }
        }
        public DayOfWeek Day
        {
            get
            {
                return day;
            }

            set
            {
                day = value;
                NotifyPropertyChanged("Day");
            }
        }

        public string CloseTime
        {
            get
            {
                return closeTime;
            }

            set
            {
                closeTime = value;
                NotifyPropertyChanged("CloseTime");
            }
        }

        public DateTime HamburgerFromDateOk
        {
            get
            {
                return hamburgerFromDateOk;
            }

            set
            {
                hamburgerFromDateOk = value;
                NotifyPropertyChanged("HamburgerFromDateOk");
            }
        }

        public DateTime HamburgerToDateOk
        {
            get
            {
                return hamburgerToDateOk;
            }

            set
            {
                hamburgerToDateOk = value;
                NotifyPropertyChanged("HamburgerToDateOk");
            }
        }

        public string Short1
        {
            get
            {
                return short1;
            }

            set
            {
                short1 = value;
                NotifyPropertyChanged("Short1");
            }
        }

        public string Over
        {
            get
            {
                return over;
            }

            set
            {
                over = value;
                NotifyPropertyChanged("Over");
            }
        }
        #endregion
    }
}
