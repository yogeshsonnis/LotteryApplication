using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Main_Shift_Collection : BaseHandler
    {
        DateTime date;
        string getDate;

        

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

        public string GetDate
        {
            get
            {
                return getDate;
            }

            set
            {
                getDate = value;
                NotifyPropertyChanged("GetDate");
            }
        }

        public ObservableCollection<Shift_Details> ShiftReport { get; set; }

        public ObservableCollection<Terminal_Details> DailyReport { get; set; }

    }
}
