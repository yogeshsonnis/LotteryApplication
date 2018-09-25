using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Store_Info : BaseHandler
    {
        int? storeID;
        string storeName;
        string noOfBoxes;
        int settlementDays;
        bool autoSettle;
        int? employeeId;
        string storeAddress;
        bool isAssignStore;
        string emailId1;
        string emailId2;
        string emailId3;
        string storeStatus;
        string openTime;
        string closeTime;
        bool? email1_On_Off;
        bool? email2_On_Off;
        bool? email3_On_Off;
        bool? isEmail_On_Off;
        int index;
        public int? StoreID
        {
            get
            {
                return storeID;
            }

            set
            {
                storeID = value;
                NotifyPropertyChanged("StoreID");
            }
        }

        public string StoreName
        {
            get
            {
                return storeName;
            }

            set
            {
                storeName = value;
                NotifyPropertyChanged("Store_Name");
            }
        }

        public string NoOfBoxes
        {
            get
            {
                return noOfBoxes;
            }

            set
            {
                noOfBoxes = value;
                NotifyPropertyChanged("NoOfBoxes");
            }
        }

        public int? EmployeeId
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

        public string StoreAddress
        {
            get
            {
                return storeAddress;
            }

            set
            {
                storeAddress = value;
                NotifyPropertyChanged("StoreAddress");
            }
        }

        public bool IsAssignStore
        {
            get
            {
                return isAssignStore;
            }

            set
            {
                isAssignStore = value;
                NotifyPropertyChanged("IsAssignStore");
            }
        }

        public string EmailId1
        {
            get
            {
                return emailId1;
            }

            set
            {
                emailId1 = value;
                NotifyPropertyChanged("EmailId1");
            }
        }

        public string EmailId2
        {
            get
            {
                return emailId2;
            }

            set
            {
                emailId2 = value;
                NotifyPropertyChanged("EmailId2");
            }
        }
        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
                NotifyPropertyChanged("Index");
            }
        }
        public string EmailId3
        {
            get
            {
                return emailId3;
            }

            set
            {
                emailId3 = value;
                NotifyPropertyChanged("EmailId3");
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

        public bool AutoSettle
        {
            get
            {
                return autoSettle;
            }

            set
            {
                autoSettle = value;
                NotifyPropertyChanged("AutoSettle");
            }
        }

        public string OpenTime
        {
            get
            {
                return openTime;
            }

            set
            {
                openTime = value;
                NotifyPropertyChanged("OpenTime");
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

        public string StoreStatus
        {
            get
            {
                return storeStatus;
            }

            set
            {
                storeStatus = value;
                NotifyPropertyChanged("StoreStatus");
            }
        }
  
        public bool? Email1_On_Off
        {
            get
            {
                return email1_On_Off;
            }

            set
            {
                email1_On_Off = value;
                NotifyPropertyChanged("Email1_On_Off");

            }
        }

        public bool? Email2_On_Off
        {
            get
            {
                return email2_On_Off;
            }

            set
            {
                email2_On_Off = value;
                NotifyPropertyChanged("Email2_On_Off");

            }
        }

        public bool? Email3_On_Off
        {
            get
            {
                return email3_On_Off;
            }

            set
            {
                email3_On_Off = value;
                NotifyPropertyChanged("Email3_On_Off");

            }
        }

        public bool? IsEmail_On_Off
        {
            get
            {
                return isEmail_On_Off;
            }

            set
            {
                isEmail_On_Off = value;
                NotifyPropertyChanged("IsEmail_On_Off");

            }
        }
    }
}
