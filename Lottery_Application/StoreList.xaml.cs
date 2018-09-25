using Lottery_Application.Model;
using Lottery_Application.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class StoreList : Page
    {
        Regex regex = new Regex("^0*([0-9]*)$");
        Regex regex1 = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

        HttpClient client = new HttpClient();
        HomeVM vobj;
        public StoreList()
        {
            this.InitializeComponent();
            this.DataContext = new HomeVM();
            //client.BaseAddress = new Uri("http://63.142.245.165:1519/");
           client.BaseAddress = new Uri("http://localhost:5133/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void LbStoreList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            ObservableCollection<Model.Shift_Details> tempshift = new ObservableCollection<Model.Shift_Details>();
            ObservableCollection<Model.Login> empcoll = new ObservableCollection<Model.Login>();
            ObservableCollection<Model.Store_Info> Store1 = new ObservableCollection<Model.Store_Info>();
            int selectedIndex = LbStoreList.SelectedIndex;
            Object selectedItem = LbStoreList.SelectedItem;
            selectedItem = v.SelectedStore;
            ApplicationData.Store_Id = Convert.ToInt32(v.SelectedStore.StoreID);
            ApplicationData.StoreName = v.SelectedStore.StoreName;
            string status = v.SelectedStore.StoreStatus;
            if (status == "New Active")
            {
                v.IsNewStoreSettingsPopup = true;
                string json = "";
                v.ShiftObj.StoreId = ApplicationData.Store_Id;
                v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                v.ShiftObj.Date = System.DateTime.Today;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                IsHitTestVisible = false;
            }
            else
            {
                string json = "";
                v.ShiftObj.StoreId = ApplicationData.Store_Id;
                v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                v.ShiftObj.Date = System.DateTime.Today;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);

                var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                //Frame.Navigate(typeof(MainPage));
                if (response.IsSuccessStatusCode)
                {
                    Frame.Navigate(typeof(MainPage));
                }
                else if (response.ReasonPhrase == "Not Found")
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                    var res = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (res.IsSuccessStatusCode)
                    {
                        var w = res.Content.ReadAsStringAsync().Result;
                        tempshift = JsonConvert.DeserializeObject<ObservableCollection<Model.Shift_Details>>(w);
                    }
                    if (tempshift != null)
                    {
                        var r = tempshift.Where(x => x.IsClose == false).LastOrDefault();
                        if (r != null)
                        {
                            HttpResponseMessage empdet = client.GetAsync("api/Login/GetEmployeeDetails").Result;
                            var emp = empdet.Content.ReadAsStringAsync().Result;
                            empcoll = JsonConvert.DeserializeObject<ObservableCollection<Model.Login>>(emp);

                            var c = empcoll.Where(x => x.EmployeeId == r.EmployeeId).FirstOrDefault();

                            var dialog = new MessageDialog("Please " + c.Username + "  Close Previous Shift");
                            await dialog.ShowAsync();
                            Frame.Navigate(typeof(LoginWindow));
                        }
                    }
                }
            }
        }
        private void BtCloseGeneralStore1_Click(object sender, RoutedEventArgs e)
        {

            var v = this.DataContext as HomeVM;
            v.IsNewStoreSettingsPopup = false;
            Frame.Navigate(typeof(StoreList));
            IsHitTestVisible = true;
            txtBoxCount1.Text = "";
            txtEmail1.Text = "";
            txtEmail2.Text = "";
            txtEmail3.Text = "";
            txtPhoneNumber1.Text = "";
            txtPhoneNumber2.Text = "";
            txtPhoneNumber3.Text = "";

        }
        private void OpenTime_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (v != null)
            {
                v.OpenTime = null;
                v.OpenTime = OpenTime.Time.ToString();
            }
        }

        private void CloseTime_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (v != null)
            {
                v.CloseTime = null;
                v.CloseTime = CloseTime.Time.ToString();
            }
        }

        private async void BtNewActiveStoreSettingsPopup_Click(object sender, RoutedEventArgs e)
        {
            //if (txtBoxCount1.Text == "" || txtEmail1.Text == "" || txtEmail2.Text != "" || txtEmail2.Text != "" || txtPhoneNumber1.Text == "" || !regex.IsMatch(txtBoxCount1.Text) || !regex.IsMatch(txtPhoneNumber1.Text) || !regex.IsMatch(txtPhoneNumber2.Text) || !regex.IsMatch(txtPhoneNumber3.Text) || !regex1.IsMatch(txtEmail1.Text) || !regex1.IsMatch(txtEmail2.Text) || !regex1.IsMatch(txtEmail3.Text))
            //{

            if (txtBoxCount1.Text == "")
            {
                var dialog = new MessageDialog("Please Enter Number of boxes");
                await dialog.ShowAsync();
            }
            else if (!regex.IsMatch(txtBoxCount1.Text))
            {
                var dialog = new MessageDialog("Number of box should be numberic");
                await dialog.ShowAsync();
            }
            else if (txtEmail1.Text == "")
            {
                var dialog = new MessageDialog("Please Enter Email ID1");
                await dialog.ShowAsync();
            }
            else if (!regex1.IsMatch(txtEmail1.Text))
            {
                var dialog = new MessageDialog("Please enter Email Id in valid format");
                await dialog.ShowAsync();
            }
            else if (txtPhoneNumber1.Text == "")
            {
                var dialog = new MessageDialog("Please Enter Phone Number1");
                await dialog.ShowAsync();
            }
            else if (!regex.IsMatch(txtPhoneNumber1.Text))
            {
                var dialog = new MessageDialog("Phone Number should be numberic");
                await dialog.ShowAsync();
            }
            else if (!regex.IsMatch(txtPhoneNumber2.Text))
            {
                var dialog = new MessageDialog("Phone Number should be numberic");
                await dialog.ShowAsync();
            }
            else if (!regex.IsMatch(txtPhoneNumber3.Text))
            {
                var dialog = new MessageDialog("Phone Number should be numberic");
                await dialog.ShowAsync();
            }
            else if (txtEmail2.Text != "" && !regex1.IsMatch(txtEmail2.Text))
            {
                //if (!regex1.IsMatch(txtEmail2.Text))
                //{
                //    var dialog = new MessageDialog("Please enter Email Id in valid format");
                //    await dialog.ShowAsync();
                //}
                var dialog = new MessageDialog("Please enter Email Id in valid format");
                await dialog.ShowAsync();

            }
            else if (txtEmail3.Text != "" && !regex1.IsMatch(txtEmail3.Text))
            {
                //if (!regex1.IsMatch(txtEmail3.Text))
                //{
                //    var dialog = new MessageDialog("Please enter Email Id in valid format");
                //    await dialog.ShowAsync();
                //}
                var dialog = new MessageDialog("Please enter Email Id in valid format");
                await dialog.ShowAsync();

            }
            else
            {
                Frame.Navigate(typeof(MainPage));
                var v = this.DataContext as HomeVM;
                Store_Info tempObj = new Store_Info();
                string json = "";
                tempObj.StoreID = ApplicationData.Store_Id;
                tempObj.StoreName = ApplicationData.StoreName;
                tempObj.StoreStatus = v.SelectedStore.StoreStatus;
                tempObj.NoOfBoxes = v.Store_Details.NoOfBoxes;
                tempObj.EmailId1 = v.Store_Details.EmailId1;
                tempObj.EmailId2 = v.Store_Details.EmailId2;
                tempObj.EmailId3 = v.Store_Details.EmailId3;
                tempObj.OpenTime = v.OpenTime;
                tempObj.CloseTime = v.CloseTime;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);
                var response = client.PostAsync("api/StoreSetting/OnGeneralSetting", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    v.IsNewStoreSettingsPopup = false;
                    v.LoadLotteryCollection();
                    v.LoadBoxCollection();
                    v.LoadEmptyBoxes();
                }
             }
        }
    }
}
        
