using Lottery_Application.Model;
using Lottery_Application.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238  

namespace Lottery_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWindow : Page
    {

        List<string> _listSuggestion = null;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        HttpClient client = new HttpClient();
        Login Log = new Login();

        Login objlogin;

        public Login Objlogin
        {
            get
            {
                return objlogin;
            }

            set
            {
                objlogin = value;
                NotifyPropertyChanged("Objlogin");
            }
        }
        public LoginWindow()
        { 
            _listSuggestion = new List<string>();
            this.InitializeComponent();
            this.DataContext = new HomeVM();
          //client.BaseAddress = new Uri("http://63.142.245.165:1519/");
         client.BaseAddress = new Uri("http://localhost:5133/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            PopupSignUp.IsOpen = false;
            v.Emp_Details_Obj.Name = "";
            v.Emp_Details_Obj.Address = "";
            v.Emp_Details_Obj.Contactno = "";
            v.Emp_Details_Obj.Username = "";
            v.Emp_Details_Obj.Password = "";
            //  v.Emp_Details_Obj.Dob = "";

        }
        private void EmpDOB_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            v.Emp_Details_Obj.Dob = (EmpDOB.Date.Value.DateTime);
        }
        public async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var v1 = this.DataContext as HomeVM;
            // Login_Click();
            var vm = this.DataContext as HomeVM;
            if (txtUserName.Text == "" || txtPassword.Password == "" || ComboBoxState.SelectedItem == null)
            {
                if (txtUserName.Text == "")
                {
                    var dialog = new MessageDialog("Please Enter Username");
                    await dialog.ShowAsync();
                }
                else if (txtPassword.Password == "")
                {
                    var dialog = new MessageDialog("Please Enter Password");
                    await dialog.ShowAsync();
                }
                else if (ComboBoxState.SelectedItem == null)
                {
                    var dialog = new MessageDialog("Please Select State");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Please Enter Username and Password and Select Shift");
                    await dialog.ShowAsync();
                }
            }
            else
            {
                ObservableCollection<Employee_Details> GetEmployeeDetails = new ObservableCollection<Employee_Details>();
                Objlogin = new Login();
                ApplicationData.Username = vm.Emp_Details_Obj.Username;
                ApplicationData.Password = vm.Emp_Details_Obj.Password;
                //     v1.User = vm.Emp_Details_Obj.Username;
                Objlogin.Username = ApplicationData.Username;
                Objlogin.Password = ApplicationData.Password;
                Objlogin.StoreAddress = ApplicationData.SelectedState;
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(Objlogin);
                var response = client.PostAsync("api/Login/NewGetEmpRecords", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    GetEmployeeDetails = new ObservableCollection<Employee_Details>();
                    var v = response.Content.ReadAsStringAsync().Result;
                    GetEmployeeDetails = JsonConvert.DeserializeObject<ObservableCollection<Employee_Details>>(v);
                    var s = GetEmployeeDetails.FirstOrDefault();
                    ApplicationData.Emp_Id = s.Employeeid;
                    ApplicationData.Store_Id = s.StoreId;
                    ApplicationData.Manager = s.IsManager;
                    Frame.Navigate(typeof(StoreList), s);
                    vm.IsSingupPopup = false;
                    // var s = GetEmployeeDetails.FirstOrDefault();
                    vm.Emp_Details_Obj.Name = s.Name;
                    Log.EmployeeId = s.Employeeid;
                }
                else
                {
                    var dialog = new MessageDialog("Please Check Username and Password.");
                    await dialog.ShowAsync();
                }
            }
        }
        private void ComboBoxState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            int selectedIndex = ComboBoxState.SelectedIndex;
            Object selectedItem = ComboBoxState.SelectedItem;
            selectedItem = v.State;
            ApplicationData.SelectedState = v.State.Name;
            //v.State.Name = selectedItem;
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            Employee_Details emp1 = new Employee_Details();
            var vm = this.DataContext as HomeVM;
            ApplicationData.Username = vm.Emp_Details_Obj.Username;
            ApplicationData.Password = vm.Emp_Details_Obj.Password;
            bool? chk = IsRememberCheckBox.IsChecked;
            vm.IsRememberMe = Convert.ToBoolean(chk);
            emp1.Username = ApplicationData.Username;
            emp1.Password = ApplicationData.Password;
            emp1.Address = ApplicationData.SelectedState;
            emp1.IsRememberMe = vm.IsRememberMe;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(emp1);
            var response = client.PostAsync("api/Login/Employee_RememberPassword", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
        }

        private void txtUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = sender as TextBox;
            Employee_Details emp1 = new Employee_Details();
            var vm = this.DataContext as HomeVM;
            vm.OnEmployeeRemember();
            var s = vm.EmployeeHistory.Where(x => x.Username == t.Text && x.IsRememberMe == true).FirstOrDefault();
            if (s != null)
            {
                txtPassword.Password = s.Password;
                IsRememberCheckBox.IsChecked = true;
            }
        }
    }
}
