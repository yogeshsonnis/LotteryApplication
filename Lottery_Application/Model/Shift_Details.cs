using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Shift_Details : BaseHandler
    {
        int? shiftId;
        string startTime;
        string endTime;
        string state;
        int storeId;
        int employeeId;
        int isCheck;
        DateTime date;
        string closeDate;
        Boolean? isLastShift;
        Boolean isClose;
       
        Boolean? isReportGenerated;
        //public ObservableCollection<Shift_Details> MainShiftReportColl { get; set; };

        string empname;
        public string Empname
        {
            get
            {
                return empname;
            }

            set
            {
                empname = value;
                NotifyPropertyChanged("Empname");
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

        public int? ShiftId
        {
            get
            {
                return shiftId;
            }

            set
            {
                shiftId = value;
                NotifyPropertyChanged("ShiftId");
            }
        }

        public string StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
                NotifyPropertyChanged("StartTime");
            }
        }

        public string EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
                NotifyPropertyChanged("EndTime");
            }
        }

        public int StoreId
        {
            get
            {
                return storeId;
            }

            set
            {
                storeId = value;
                NotifyPropertyChanged("StoreId");
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

        public bool? IsLastShift
        {
            get
            {
                return isLastShift;
            }

            set
            {
                isLastShift = value;
                NotifyPropertyChanged("IsLastShift");
            }
        }

        public bool IsClose
        {
            get
            {
                return isClose;
            }

            set
            {
                isClose = value;
                NotifyPropertyChanged("IsClose");
            }
        }

        public string CloseDate
        {
            get
            {
                return closeDate;
            }

            set
            {
                closeDate = value;
                NotifyPropertyChanged("CloseDate");
            }
        }

       

        public bool? IsReportGenerated
        {
            get
            {
                return isReportGenerated;
            }

            set
            {
                isReportGenerated = value;
                NotifyPropertyChanged("IsReportGenerated");
            }
        }

        public int IsCheck
        {
            get
            {
                return isCheck;
            }

            set
            {
                isCheck = value;
                NotifyPropertyChanged("IsCheck");
            }
        }
    }
}
