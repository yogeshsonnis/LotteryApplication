using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Employee_Details : BaseHandler
    {
        #region Private Properties
        string name;
        string contactno;
        string address;
        DateTime dob;
        string username;
        string password;
        int employeeid;
        int shiftid;
        int storeId;
        string emailId;
        bool isManager;
        bool isEmployee;
        bool isAssignStore;
        bool? isRememberMe;
       

        #endregion

        #region Public Properties
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public string Contactno
        {
            get
            {
                return contactno;
            }

            set
            {
                contactno = value;
                NotifyPropertyChanged("Contactno");
            }
        }
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
                NotifyPropertyChanged("Address");
            }
        }
        public DateTime Dob
        {
            get
            {
                return dob;
            }

            set
            {
                dob = value;
                NotifyPropertyChanged("Dob");
            }
        }
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
                NotifyPropertyChanged("Username");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                NotifyPropertyChanged("Password");
            }
        }
        public int Employeeid
        {
            get
            {
                return employeeid;
            }

            set
            {
                employeeid = value;
                NotifyPropertyChanged("Employeeid");
            }
        }
        public int Shiftid
        {
            get
            {
                return shiftid;
            }

            set
            {
                shiftid = value;
                NotifyPropertyChanged("Shiftid");
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

        public string EmailId
        {
            get
            {
                return emailId;
            }

            set
            {
                emailId = value;
                NotifyPropertyChanged("EmailId");
            }
        }

        public bool IsManager
        {
            get
            {
                return isManager;
            }

            set
            {
                isManager = value;
                NotifyPropertyChanged("IsManager");
            }
        }

        public bool IsEmployee
        {
            get
            {
                return isEmployee;
            }

            set
            {
                isEmployee = value;
                NotifyPropertyChanged("IsEmployee");
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

        public bool? IsRememberMe
        {
            get
            {
                return isRememberMe;
            }

            set
            {
                isRememberMe = value;
                NotifyPropertyChanged("IsRememberMe");
            }
        }







        #endregion
    }
}
