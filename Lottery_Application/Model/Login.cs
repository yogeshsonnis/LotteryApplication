using Lottery_Application.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Application.Model
{
    public class Login : BaseHandler
    {
        int employeeId;
        DateTime date;
        int shiftId;
        string state;
        string username;
        string password;
        int storeId;
        string newPassword;
        string emailId;
        string name;
        string contactno;
        string role;
        int index; 
        bool isManager;
        bool isEmployee;
        bool? isRememberMe;
        bool isAssignStore;
        string manager;
        string employee;
        string assignThisStore;
        string emailId1;
        string emailId2;
        string emailId3;
        string storeAddress;


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

        public int ShiftId
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

        public string NewPassword
        {
            get
            {
                return newPassword;
            }

            set
            {
                newPassword = value;
                NotifyPropertyChanged("NewPassword");
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

        public string Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
                NotifyPropertyChanged("Role");
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

        public string Manager
        {
            get
            {
                return manager;
            }

            set
            {
                manager = value;
                NotifyPropertyChanged("Manager");
            }
        }

        public string Employee
        {
            get
            {
                return employee;
            }

            set
            {
                employee = value;
                NotifyPropertyChanged("Employee");
            }
        }

        public string AssignThisStore
        {
            get
            {
                return assignThisStore;
            }

            set
            {
                assignThisStore = value;
                NotifyPropertyChanged("AssignThisStore");
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
                NotifyPropertyChanged("IsAssignThisStore");
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
    }
}
