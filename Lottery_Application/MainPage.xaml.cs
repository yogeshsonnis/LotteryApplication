using Lottery_Application.Model;
using Lottery_Application.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Data.Pdf;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.AccessCache;
using System.Threading.Tasks;
using System.Net;
using Windows.Networking.BackgroundTransfer;
using Windows.Foundation;
using Windows.ApplicationModel.Email;
using LightBuzz.SMTP;
using Windows.System;
using EASendMailRT;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lottery_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        #region Properties
        bool isValidBarcode;
        bool isvalid;
        public int Flag = 0;
        DateTime t1;
        public bool IsGenerateReport;

        Regex regex = new Regex("[0-9]");
        public bool IsValid
        {
            get
            {
                return isvalid;
            }
            set
            {
                isvalid = value;
                NotifyPropertyChanged("IsValid");
            }
        }
        public bool IsValidBarcode
        {
            get
            {
                return isValidBarcode;
            }
            set
            {
                isValidBarcode = value;
                NotifyPropertyChanged("IsValidBarcode");
            }
        }
        ObservableCollection<Activation_Box> editableTicket;
        List<string> _listSuggestion = null;
        List<string> _listSuggestion1 = null;
        ObservableCollection<Activation_Box> tempCollection = new ObservableCollection<Activation_Box>();
        public ObservableCollection<Master_List_Inventory> MasterListColl { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TerminalFromDate { get; set; }
        public DateTime ShiftHamburgerFromDate { get; set; }
        public DateTime ShiftHamburgerToDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime TerminalToDate { get; set; }
        public string Words { get; set; }
        public ObservableCollection<BitmapImage> pdfPages;
        HomeVM vobj;
        public Activation_Box ABC { get; set; }
        public Login Objlogin { get; set; }
        ObservableCollection<BarcodeFormat> barcodeFormatCollection;
        ObservableCollection<Terminal_Details> terminalColl;
        public BarcodeFormat GetBarcodeFormat { get; set; }
        public ObservableCollection<BarcodeFormat> BarcodeFormatCollection
        {
            get
            {
                return barcodeFormatCollection;
            }
            set
            {
                barcodeFormatCollection = value;

            }
        }
        public ObservableCollection<Terminal_Details> TerminalColl
        {
            get
            {
                return terminalColl;
            }
            set
            {
                terminalColl = value;
            }
        }
        #endregion

        #region WebApifields

        HttpClient client = new HttpClient();

        #endregion

        public MainPage()
        {
            this.InitializeComponent();
            InitalizeVariable();
            this.DataContext = new HomeVM();
            vobj = this.DataContext as HomeVM;
            _listSuggestion = new List<string>();
            BarcodeFormatDetails();
            BtSoldTickets.Click += BtSoldTickets_Click;
            BtDeactived.Click += BtSoldTickets_Click;
            BtReturnedTickets.Click += BtSoldTickets_Click;
            BtSettledTickets.Click += BtSoldTickets_Click;
            BtnReceive.Click += BtnReceive_Click;
            btnReceiveReturn.Click += BtnReceiveReturn_Click;
            //btnReturn.Click += BtnReceiveReturn_Click;
            txtReceiveBarcode.Loaded += TxtReceiveBarcode_Loaded;
            txtBarcodeActive.Loaded += TxtReceiveBarcode_Loaded;
            txtBarcodeDeactivate.Loaded += TxtReceiveBarcode_Loaded;
            txtBarcodeSoldout.Loaded += TxtReceiveBarcode_Loaded;
            TxtReturnBarcode.Loaded += TxtReceiveBarcode_Loaded;
            TxtSettleBarcode.Loaded += TxtReceiveBarcode_Loaded;
            txtBarcodeCloseShift.Loaded += TxtReceiveBarcode_Loaded;
            TBDailyReport.Click += TBDailyReport_Click;
            IsGenerateReport = false;
            CheckNewShift();

            //OpenTime.Time = TimeSpan.Parse("17:29:00");

        }

        private async void CheckNewShift()
        {
            string json = "";
            Store_Info tempObj1 = new Store_Info();
            tempObj1.StoreID = ApplicationData.Store_Id;
            tempObj1.EmployeeId = ApplicationData.Emp_Id;
            tempObj1.StoreAddress = ApplicationData.SelectedState;
            json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj1);
            var responce = client.PostAsync("api/StoreSetting/NewGetStoreHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (responce.IsSuccessStatusCode)
            {
                var storestatus = responce.Content.ReadAsStringAsync().Result;
                var coll = JsonConvert.DeserializeObject<ObservableCollection<Store_Info>>(storestatus);
                var e = coll.Where(j => j.StoreID == ApplicationData.Store_Id).FirstOrDefault();
                if (e.StoreStatus == "New Active")
                {
                    var dialog = new MessageDialog("Do you want to insert the existing box records ?");
                    dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                    dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                    var res = await dialog.ShowAsync();
                    if ((int)res.Id == 0)
                    {
                        vobj.IsNewStoreSettingsPopup = false;
                        vobj.LoadLotteryCollection();
                        vobj.LoadBoxCollection();
                        vobj.LoadEmptyBoxes();
                        vobj.IsHitTestVisible = false;
                        vobj.IsNewStoreSetupScanExistingBoxesInOrder = true;
                    }
                    else
                    {
                        vobj.IsNewStoreSettingsPopup = false;
                        vobj.LoadLotteryCollection();
                        vobj.LoadBoxCollection();
                        vobj.LoadEmptyBoxes();
                    }
                    //var v = this.DataContext as HomeVM;
                    //Store_Info tempObj = new Store_Info();
                    //tempObj.StoreID = ApplicationData.Store_Id;
                    //tempObj.StoreName = ApplicationData.StoreName;
                    //tempObj.StoreStatus = v.SelectedStore.StoreStatus;
                    //tempObj.NoOfBoxes = v.Store_Details.NoOfBoxes;
                    //tempObj.EmailId1 = v.Store_Details.EmailId1;
                    //tempObj.EmailId2 = v.Store_Details.EmailId2;
                    //tempObj.EmailId3 = v.Store_Details.EmailId3;
                    //tempObj.OpenTime = v.OpenTime;
                    //tempObj.CloseTime = v.CloseTime;
                    //json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);
                    //var response = client.PostAsync("api/StoreSetting/OnGeneralSetting", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                }
                // Frame.Navigate(typeof(MainPage));

            }


        }

        #region Methods

        public void BarcodeFormatDetails()
        {
            ObservableCollection<BarcodeFormat> GetBarcodeDetails = new ObservableCollection<BarcodeFormat>();
            BarcodeFormatCollection = new ObservableCollection<BarcodeFormat>();
            Objlogin = new Login();
            Objlogin.State = ApplicationData.SelectedState;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Objlogin);
            // HttpResponseMessage res = client.GetAsync("api/BarCodeFormat/GetBarCodeFormat").Result;
            var response = client.PostAsync("api/BarCodeFormat/NewGetBarCodeFormat", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
            {
                GetBarcodeDetails = new ObservableCollection<BarcodeFormat>();
                var v = response.Content.ReadAsStringAsync().Result;
                GetBarcodeDetails = JsonConvert.DeserializeObject<ObservableCollection<BarcodeFormat>>(v);
                GetBarcodeFormat = GetBarcodeDetails.FirstOrDefault();
            }
        }
        private void BtnReceiveReturn_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (v.IsReceiveChecked == true)
            {
                ReturnEndno.IsReadOnly = true;
            }
            else
            {
                ReturnEndno.IsReadOnly = false;
            }


        }
        private void TxtReceiveBarcode_Loaded(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Focus(FocusState.Keyboard);
        }
        private void BtnReceive_Click(object sender, RoutedEventArgs e)
        {
            txtReceiveBarcode.Text = "";
        }
        private void BtSoldTickets_Click(object sender, RoutedEventArgs e)
        {
            FromCalendar.Date = System.DateTime.Now;
            ToCalendar.Date = System.DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void BTBoxView_Click(object sender, RoutedEventArgs e)
        {
            GDBoxView.Visibility = Visibility.Visible;
            GDListView.Visibility = Visibility.Collapsed;

        }
        private void BTListView_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsReceiveChecked = true;
            v.OnReceiveHistory();
            GDListView.Visibility = Visibility.Visible;
            GDBoxView.Visibility = Visibility.Collapsed;
            v.IsVisiblebtnGrid = Visibility.Visible;
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = !MySplitview.IsPaneOpen;
        }
        private void BtUpdateInventory_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            PopupReceive.IsOpen = true;
            v.IsHitTestVisible = false;
        }
        private void LBEmptyTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LBEmptyTicket.ScrollIntoView(LBEmptyTicket.SelectedItem);
        }
        private void BtManualupdate_Click(object sender, RoutedEventArgs e)
        {
            //PopupReceiveManual.Visibility = Visibility.Visible;
            //PopupReceive.Visibility = Visibility.Collapsed;
        }
        private void BtAddSoldOutTicket_Click(object sender, RoutedEventArgs e)
        {
            PopupSoldout.IsOpen = true;
            txtBarcodeSoldout.Text = "";
        }
        private void BtReturnTicket_Click(object sender, RoutedEventArgs e)
        {
            PopupReturnticket.IsOpen = true;
            TxtReturnBarcode.Text = "";
        }
        public void btnEmptyBox_Click(object sender, RoutedEventArgs e)
        {
            PopupActivateBox.IsOpen = false;
            PopupEmptybox.IsOpen = true;
            Button btn = sender as Button;
            string content = btn.Content.ToString();
            txtSelectedBox_No.Text = content;
            txtDisplayEmptyBox_No.Text = content;
        }
        private void btnCloseEmptyBoxPopup_Click(object sender, RoutedEventArgs e)
        {
            PopupEmptybox.IsOpen = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (CloseshiftPopup.IsOpen == true)
            {
                v.IsHitTestVisible = false;
            }
            else { v.IsHitTestVisible = true; }
            v.IsHitTestVisiblePopup = true;
            v.IsActivateBox = false;
            v.SelectedData = null;
            v.ActiveBoxObj.Box_No = null;
            v.ActiveBoxObj = new Activate_Ticket();
            v.Return_Obj = new Return_Details();
            v.SoldOutObj = new SoldOut_Details();
            v.Settle_Obj = new Settle_Details();
            v.IsShowActivateReturnPopup = false;
            v.ReceiveselectedData = null;
            v.IsCloseShiftActivateBox = false;
            v.IsCloseBoxReopen = false;
        }
        public void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string content = btn.Content.ToString();
            textBoxNo.Text = content;
            txtDisplayEmptyBox_No.Text = content;
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisible = false;
            v.GetBoxRecord(content);
            //ReturnEndno.IsReadOnly = false;
            v.DisplayInfo(content);
            //v.ShowBoxNo();
        }
        private void BtDeactivateTicket_Click(object sender, RoutedEventArgs e)
        {
            PopupDeactivate.IsOpen = true;
            txtBarcodeActive.Text = "";
        }
        private void ActivateCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            //v.Active_StatusObj.Created_Date = (ActivateCalendar.Date.Value.DateTime);
        }
        private void FromCelendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (FromCalendar.Date != null)
            {
                FromDate = (FromCalendar.Date.Value.DateTime);
                FromDate = FromDate.AddDays(-1);
            }
            else
            {
                FromDate = System.DateTime.Now;
                FromDate = FromDate.AddDays(-1);
            }
        }
        private void ToCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {

            if (ToCalendar.Date != null)
            {
                ToDate = (ToCalendar.Date.Value.DateTime);

            }
            else
            {
                ToDate = System.DateTime.Now;

            }


        }
        private async void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (FromDate > ToDate)
            {
                var dialog = new MessageDialog("To date must be greater than or same as From date");
                await dialog.ShowAsync();
            }
            else
            {
                var v = this.DataContext as HomeVM;
                v.IsVisiblebtnGrid = Visibility.Collapsed;
                if (v.IsDeactivateChecked == true)
                {
                    v.DeactivatehistoryColl = new ObservableCollection<Activation_Box>();
                    HttpResponseMessage response = client.GetAsync("api/Deactivate/GetDeactivateHistory").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
                    }
                    var list = tempCollection.Where(p => p.Created_Date >= FromDate && p.Created_Date <= ToDate).ToList();
                    v.CountDeactiveBox = list.Count;
                    foreach (var i in list)
                    {
                        v.DeactivatehistoryColl.Add(new Activation_Box
                        {
                            Box_No = i.Box_No,
                            Game_Id = i.Game_Id,
                            Packet_No = i.Packet_No,
                            Ticket_Name = i.Ticket_Name,
                            Price = i.Price,
                            Created_Date = i.Created_Date,
                            Start_No = i.Start_No,
                            Stopped_At = i.Stopped_At
                        });
                    }
                }
                else if (v.IsSoldoutChecked == true)
                {
                    v.SoldOutObj = new SoldOut_Details();
                    v.SoldouthistoryColl = new ObservableCollection<Activation_Box>();
                    v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                    v.SoldOutObj.EmployeeID = ApplicationData.Emp_Id;

                    string json = "";
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                    var response = client.PostAsync("api/CloseShift/NewGetSoldOutHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;


                    //  HttpResponseMessage response = client.GetAsync("api/SoldOut/NewGetSoldOutHistory").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
                    }
                    var list = tempCollection.Where(p => p.Created_Date >= FromDate && p.Created_Date <= ToDate).ToList();
                    v.CountSoldOutBox = list.Count;
                    foreach (var i in list)
                    {
                        v.SoldouthistoryColl.Add(new Activation_Box
                        {
                            Game_Id = i.Game_Id,
                            Packet_No = i.Packet_No,
                            Ticket_Name = i.Ticket_Name,
                            Price = i.Price,
                            Created_Date = i.Created_Date,
                            Start_No = i.Start_No,
                            Box_No = i.Box_No,
                            End_No = i.End_No
                        });
                    }
                }
                else if (v.IsReturnChecked == true)
                {
                    v.ReturnhistoryColl = new ObservableCollection<Activation_Box>();
                    HttpResponseMessage response = client.GetAsync("api/Return/GetReturnHistory").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
                    }
                    var list = tempCollection.Where(p => p.Created_Date >= FromDate && p.Created_Date <= ToDate).ToList();
                    v.CountReturnBox = list.Count;
                    foreach (var i in list)
                    {
                        v.ReturnhistoryColl.Add(new Activation_Box
                        {
                            Game_Id = i.Game_Id,
                            Box_No = i.Box_No,
                            Packet_No = i.Packet_No,
                            Ticket_Name = i.Ticket_Name,
                            Price = i.Price,
                            Return_At = i.Return_At,
                            Created_Date = i.Created_Date,
                            Start_No = i.Start_No,
                            End_No = i.End_No
                        });
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync("api/Settle/GetSettleHistory").Result;
                    v.SettleHistoryColl = new ObservableCollection<Activation_Box>();
                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
                    }
                    var list = tempCollection.Where(p => p.Created_Date >= FromDate && p.Created_Date <= ToDate).ToList();
                    v.CountSettleBox = list.Count;
                    foreach (var i in list)
                    {
                        v.SettleHistoryColl.Add(new Activation_Box
                        {
                            Game_Id = i.Game_Id,
                            Packet_No = i.Packet_No,
                            Ticket_Name = i.Ticket_Name,
                            Price = i.Price,
                            Start_No = i.Start_No,
                            End_No = i.End_No,
                            Created_Date = i.Created_Date

                        });
                    }
                }
            }
        }
        private void PopupSoldOutBox_LayoutUpdated(object sender, object e)
        {
            if (GdPopupSoldOutBox.ActualWidth == 0 && GdActivate.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupSoldOutBox.HorizontalOffset;
            double ActualVerticalOffset = this.PopupSoldOutBox.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdPopupSoldOutBox.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdPopupSoldOutBox.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupSoldOutBox.HorizontalOffset = NewHorizontalOffset;
                this.PopupSoldOutBox.VerticalOffset = NewVerticalOffset;
            }
        }
        private void btnSoldCancel_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsSoldOutBox = false;
        }
        private async void AutotxtPackId_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Activate GameID Textchanged
            var v = this.DataContext as HomeVM;
            v.OnReceiveHistory();
            //if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            //{
            //var coll = v.HistoryColl.Select(x => x.Game_Id).ToList();
            var coll = v.HistoryColl.Where(x => x.State == ApplicationData.SelectedState).Select(x => x.Game_Id).ToList();
            _listSuggestion = (coll.Where(x => x.StartsWith(sender.Text)).Distinct()).ToList();
            if (_listSuggestion.Count == 0)
            {
                v.Active_StatusObj.Ticket_Name = "";
                v.Active_StatusObj.Price = 0;
                v.Active_StatusObj.Start_No = "";

                var dialog1 = new MessageDialog("Not Availble in Store Inventory.");
                await dialog1.ShowAsync();

                v.Active_StatusObj.Game_Id = "";
                v.Active_StatusObj.Packet_No = "";
                v.Active_StatusObj.Ticket_Name = "";
                v.Active_StatusObj.Price = 0;
                v.Active_StatusObj.Start_No = "";
                v.Active_StatusObj.End_No = "";
                txtBarcodeActive.Text = "";

            }
            else
            {
                sender.ItemsSource = _listSuggestion;

                if (txtGameId.Text == "")
                {
                    //v.Active_StatusObj.Game_Id = "";
                    v.Active_StatusObj.Packet_No = "";
                    v.Active_StatusObj.Ticket_Name = "";
                    v.Active_StatusObj.Price = 0;
                    v.Active_StatusObj.Start_No = "";
                    v.Active_StatusObj.End_No = "";

                }
            }
            // }
        }
        private async void txtReceiveGameIDAutoSuggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            v.OnMasterHistory();

            //if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            //{

            var coll = v.MasterColl.Where(x => x.State == ApplicationData.SelectedState && x.Store_Id == ApplicationData.Store_Id).Select(x => x.Game_Id).ToList();
            //var coll = v.MasterColl.Where(x => x.State).ToList();
            //var coll = v.MasterColl.Select(x => x.Game_Id).ToList();
            _listSuggestion1 = (coll.Where(x => x.StartsWith(sender.Text)).Distinct()).ToList();
            if (-_listSuggestion1.Count == 0)
            {
                var dialog = new MessageDialog("Not Available in Master List.Please Add in Master List.");
                await dialog.ShowAsync();
                txtReceiveBarcode.Text = "";
                v.ReceiveObj.Game_Id = "";
                v.ReceiveObj.Packet_No = "";
                v.ReceiveObj.Ticket_Name = "";
                v.ReceiveObj.Rate = "";
                v.ReceiveObj.Start_No = "";
                v.ReceiveObj.End_No = "";
            }
            else
            {
                sender.ItemsSource = _listSuggestion1;
                if (txtReceiveGameIDAutoSuggest.Text == "")
                {
                    v.ReceiveObj.Game_Id = "";
                    v.ReceiveObj.Packet_No = "";
                    v.ReceiveObj.Ticket_Name = "";
                    v.ReceiveObj.Rate = "";
                    v.ReceiveObj.Start_No = "";
                    v.ReceiveObj.End_No = "";

                }
            }


            // }

        }
        private void AutotxtPackId_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            string GameID = args.ChosenSuggestion.ToString();
            v.OnReceiveHistory();
            _listSuggestion = v.HistoryColl.Where(x => x.Game_Id == GameID).Select(x => x.Packet_No).ToList();
            txtPackId.ItemsSource = _listSuggestion;
        }
        private void txtAutosuggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            string PacketID = args.QueryText;
            v.OnReceiveHistory();
            txtPackId.ItemsSource = _listSuggestion;
            var s = v.HistoryColl.Where(x => x.Packet_No == PacketID).FirstOrDefault();
        }
        private void txtReceiveGameIDAutoSuggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                var v = this.DataContext as HomeVM;
                string GameId = args.QueryText.ToString();
                v.OnMasterHistory();
                var s = v.MasterColl.Where(x => x.Game_Id == GameId && x.Store_Id == ApplicationData.Store_Id).FirstOrDefault();
                if (args.QueryText != null)
                {
                    v.ReceiveObj.Game_Id = s.Game_Id;
                    v.ReceiveObj.Packet_No = s.Packet_No;
                    v.ReceiveObj.Ticket_Name = s.Ticket_Name;
                    v.ReceiveObj.Rate = s.Rate;
                    v.ReceiveObj.Start_No = s.Start_No;
                    v.ReceiveObj.End_No = s.End_No;
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void FromCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            FromCalendar.Date = System.DateTime.Now;
        }
        private void ToCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            ToCalendar.Date = System.DateTime.Now;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.Emp_Details_Obj = e.Parameter as Employee_Details;
            // v.State = e.Parameter as StateClass;
            // parameters.Name
            // parameters.Text
            // ...
        }
        DispatcherTimer dispatcherTimer;

        int? Box_No;
        int timesTicked = 0;
        int i = 0;

        public void dispatcherTimer_Tick(object sender, object e)
        {
            timesTicked++;
            if (i < vobj.Active_BoxCollection.Count && timesTicked == 5)
            {
                Activation_Box data = vobj.Active_BoxCollection.Where(p => p.Box_No == vobj.Active_BoxCollection[i].Box_No).FirstOrDefault();
                Box_No = data.Box_No;
                data.BackColor = new SolidColorBrush(Color.FromArgb(255, 0, 162, 232));
                data.IsScanned = true;
                //  LastShiftChBox.IsEnabled = true;
            }
            else if (i == vobj.Active_BoxCollection.Count)
            {
                LoaderIsScanBox.IsActive = false;
                this.IsEnabled = true;
                //LastShiftChBox.IsEnabled = true;
                dispatcherTimer.Stop();
            }

            if (timesTicked == 6)
            {
                timesTicked = 0;
                i++;
            }
        }
        public async void HighlightBox()
        {
            var v = this.DataContext as HomeVM;
            var abc = v.HistoryColl.Select(x => x.Box_No).ToList();
            var pqr = v.ActivatehistoryColl.Select(x => x.Box_No).ToList();

            if (abc != pqr)
            {
                v.IsReportPopup = false;
                var dialog = new MessageDialog("Cannot Submit Until All Active Boxes have been Scanned.");
                await dialog.ShowAsync();

            }
        }
        private async void btnShiftCancel_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            var dialog = new MessageDialog("Are You Sure!Want to Close?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var res = await dialog.ShowAsync();
            if ((int)res.Id == 0)
            {
                v.IsHitTestVisible = true;
                v.Temp = 0;
                dispatcherTimer = new DispatcherTimer();
                CloseshiftPopup.IsOpen = false;
                dispatcherTimer.Stop();
                txtBarcodeCloseShift.Text = "";
                LoaderIsScanBox.IsActive = false;
                // LastShiftChBox.IsEnabled = false;
            }
        }
        private async void btnShiftSubmit_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.ShiftObj.IsCheck = 0;
            v.TerminalObj = new Terminal_Details();
            v.IsHitTestVisible = false;
            v.IsHitTestVisiblePopup = false;
            //v.TerminalObj = new Terminal_Details();
            //string json = "";
            //v.ShiftObj.StoreId = ApplicationData.Store_Id;
            //v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
            //v.ShiftObj.Date = System.DateTime.Today;
            //if(LastShiftChBox.IsChecked == true)
            //{
            //    v.ShiftObj.IsLastShift = true;
            //    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
            //    var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            //}
            // HighlightBox();
            dispatcherTimer = new DispatcherTimer();
            int flag = 1;
            // var v = this.DataContext as HomeVM;
            // v.GetSoldOutHistory();
            foreach (var i in v.Active_BoxCollection)
            {
                if (i.IsScanned != true && i.Status == "Active")
                {
                    flag = 0;
                }

                else if (i.IsScanned == true && i.Status == "Close")
                {
                    flag = 1;
                }
            }

            if (flag == 0)
            {
                dispatcherTimer.Stop();
                v.IsHitTestVisiblePopup = true;
                var dialog = new MessageDialog("Cannot Submit Until All Active Boxes have been Scanned.");
                await dialog.ShowAsync();
                dispatcherTimer.Start();
            }
            else
            {
                string json = "";
                ObservableCollection<Shift_Details> tempshift = new ObservableCollection<Shift_Details>();
                v.ShiftObj = new Shift_Details();
                v.ShiftObj.StoreId = ApplicationData.Store_Id;
                v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                var result = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (result.IsSuccessStatusCode)
                {
                    var w = result.Content.ReadAsStringAsync().Result;
                    tempshift = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(w);
                }
                var g = tempshift.LastOrDefault();

                if (g.IsReportGenerated == true && g.IsLastShift == true && LastShiftChBox.IsChecked == true)
                {
                    //var dialog = new MessageDialog("Daily report for this shift is already generated. Do you want to replace it ? ");
                    //dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                    //dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                    //var res = await dialog.ShowAsync();


                    v.IsDataTerminalPopup = true;

                    //   v.IsHitTestVisiblePopup = true;

                    // LastShiftChBox.IsEnabled = true;
                    //  v.IsReportPopup = true;
                    // v.GetReport();
                }
                else
                {
                    if (LastShiftChBox.IsChecked == false && g.IsReportGenerated == true)
                    {
                        v.ShiftObj.IsCheck = 1;
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                        var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    }
                    v.ShiftObj.IsCheck = 0;
                    v.IsDataTerminalPopup = true;
                }
            }
        }
        private void BtCloseAddInventory_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            // v.IsHitTestVisible = true;
            v.IsHitTestVisiblePopup = true;
            vobj.IsAddInventoryPopup = false;
            // vobj.IsReceiveManuallyPopup = true;                                                                                                                                                                                                                                                                             
            vobj.MasterListObj.Game_Id = "";
            vobj.MasterListObj.Packet_No = "";
            vobj.MasterListObj.Ticket_Name = "";
            vobj.MasterListObj.Rate = "";
            vobj.MasterListObj.Start_No = "";
            vobj.MasterListObj.End_No = "";
            tb7.Text = "";
            //txtMasterBarcode.Text = "";
        }
        private void btnAddInventory_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            // vobj.IsReceiveManuallyPopup = true;
            v.IsHitTestVisiblePopup = false;
            vobj.IsAddInventoryPopup = true;
            v.ReceiveObj.Game_Id = "";
            v.ReceiveObj.Packet_No = "";
            v.ReceiveObj.Rate = "";
            v.ReceiveObj.End_No = "";
            v.ReceiveObj.Ticket_Name = "";
            v.ReceiveObj.Start_No = "";
            txtReceiveBarcode.Text = "";
        }
        private void tb6_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var v = this.DataContext as HomeVM;
                if (v.MasterListObj.End_No == "")
                {
                    tb7.Text = "";
                }
                else
                {
                    tb7.Text = (Convert.ToInt32(v.MasterListObj.End_No) - Convert.ToInt32(v.MasterListObj.Start_No) + 1).ToString();
                }
            }
            catch (Exception ex)
            {

            }

        }
        private async void TBLogout_Click(object sender, RoutedEventArgs e)
        {
            //var v = this.DataContext as HomeVM;
            ////Frame.Navigate(typeof(LoginWindow));
            //string json = "";
            //v.ShiftObj.StoreId = ApplicationData.Store_Id;
            //v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
            //v.ShiftObj.Date = System.DateTime.Today;
            //json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
            //var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            //if (response.ReasonPhrase == "Not Found")
            //{
            //    var dialog = new MessageDialog("Please Close Current Shift Before Logout.");
            //    await dialog.ShowAsync();
            //}
            var dialog = new MessageDialog("Are you sure you want to logout ?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });

            var res = await dialog.ShowAsync();

            if ((int)res.Id == 0)
            {
                Frame.Navigate(typeof(LoginWindow));
            }
        }
        private void BtCloseShift_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisible = true;
            v.IsCloseshitfPopup = false;
            IsGenerateReport = false;
            LastShiftChBox.IsChecked = false;
            //LastShiftChBox.IsEnabled = false;
            //tempLastshiftchbox_Click();
            btnDailyReport.Visibility = Visibility.Collapsed;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Stop();
            txtBarcodeCloseShift.Text = "";
            LoaderIsScanBox.IsActive = false;
            if (v.Temp == 1 || v.Dailytemp == 1)
            {

                v.IsHitTestVisible = false;
                PopupStartNewShift.IsOpen = true;
            }
            //else if (v.Temp == 0)
            //{

            //    tempLastshiftchbox_Click();
            //}
            //v.Temp = 0;
        }
        private async void btnRowDelete_Click(object sender, RoutedEventArgs e)
        {
            var B = ((sender as Button).DataContext as Activation_Box).Packet_No;
            var D = ((sender as Button).DataContext as Activation_Box).Game_Id;
            var v = this.DataContext as HomeVM;
            ABC = new Activation_Box(B);
            ABC.Packet_No = B;
            ABC.Game_Id = D;
            var dialog = new MessageDialog("Are You Sure!Want to Delete the Record?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var res = await dialog.ShowAsync();
            string json = "";
            if ((int)res.Id == 0)
            {

                json = Newtonsoft.Json.JsonConvert.SerializeObject(ABC);
                var response = client.PostAsync("api/Receive/DeleteSelectedRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    v.GetReceiveBoxCount();
                    v.OnReceiveHistory();
                    //Return_Obj = null;
                    v.BoxCollection = new ObservableCollection<Activation_Box>();
                    v.LoadBoxCollection();
                    v.LoadLotteryCollection();
                    v.LoadEmptyBoxes();
                }
                else
                {
                    var dialog1 = new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                    await dialog1.ShowAsync();
                }
            }
        }
        private async void btnScanBarcode_Click(object sender, RoutedEventArgs e)
        {
            if (txtReceiveBarcode.Text == "")
            {
                var dialog = new MessageDialog("Please Enter the Barcode.");
                await dialog.ShowAsync();
            }
            else
            {
                Words = txtReceiveBarcode.Text;
                string[] split = Words.Split(new Char[] { ' ', '-' });
                string a = split[0];
                string b = split[1];
                //string c = split[2];
                //string d = split[3];

                txtReceiveGameIDAutoSuggest.Text = a;
                txtReceivedPacketId.Text = b;


                var v = this.DataContext as HomeVM;
                //string GameId = args.ChosenSuggestion.ToString();

                v.OnMasterHistory();
                var s = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                if (s != null)
                {
                    v.ReceiveObj.Game_Id = s.Game_Id;
                    // v.ReceiveObj.Packet_No = s.Packet_No;
                    v.ReceiveObj.Ticket_Name = s.Ticket_Name;
                    v.ReceiveObj.Rate = s.Rate;
                    v.ReceiveObj.Start_No = s.Start_No;
                    v.ReceiveObj.End_No = s.End_No;
                }
                else
                {
                    var dialog1 = new MessageDialog("Not Available in Master List.");
                    await dialog1.ShowAsync();
                }
            }
        }
        public void GetRefreshUnsoldGrid()
        {
            var v = this.DataContext as HomeVM;
            v.SoldouthistoryColl = new ObservableCollection<Activation_Box>();
            HttpResponseMessage response = client.GetAsync("api/SoldOut/GetSoldOutHistory").Result;

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
            }
            var list = tempCollection.Where(p => p.Created_Date >= FromDate && p.Created_Date <= ToDate).ToList();
            v.CountSoldOutBox = list.Count;
            foreach (var i in list)
            {
                v.SoldouthistoryColl.Add(new Activation_Box
                {
                    Game_Id = i.Game_Id,
                    Packet_No = i.Packet_No,
                    Ticket_Name = i.Ticket_Name,
                    Price = i.Price,
                    Created_Date = i.Created_Date,
                    Start_No = i.Start_No,
                    Box_No = i.Box_No,
                    End_No = i.End_No
                });
            }
        }
        private async void btnUnsold_Click(object sender, RoutedEventArgs e)
        {
            var c = ((sender as Button).DataContext as Activation_Box).Packet_No;
            var d = ((sender as Button).DataContext as Activation_Box).Box_No;
            var f = ((sender as Button).DataContext as Activation_Box).Game_Id;
            var a = ((sender as Button).DataContext as Activation_Box).Start_No;
            var v = this.DataContext as HomeVM;
            ABC = new Activation_Box(c);
            ABC.Packet_No = c;
            ABC.Game_Id = f;
            ABC.Box_No = d;
            ABC.Start_No = a;
            ABC.State = ApplicationData.SelectedState;
            ABC.Store_Id = ApplicationData.Store_Id;
            ABC.EmployeeId = ApplicationData.Emp_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ABC);
            HttpResponseMessage response = client.PostAsync("api/SoldOut/Unsold", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (response.ReasonPhrase == "Not Found")
            {
                var dialog = new MessageDialog("Packet cannot be unsold until new packet in Box " + d + " is de-activated ");
                await dialog.ShowAsync();
            }
            else if (response.ReasonPhrase == "Conflict")
            {
                var dialog = new MessageDialog("Packet Already Active");
                await dialog.ShowAsync();
            }
            else if (response.IsSuccessStatusCode)
            {
                var dialog = new MessageDialog("Packet Activated");
                await dialog.ShowAsync();
                GetRefreshUnsoldGrid();
                v.GetUpdatedsoldCount();
                v.OnActivateHistory();
                v.OnSoldOutHistory();
                v.OnReturnHistory();
                v.GetSettledBoxCount();
                v.GetEmptyBoxCount();
                v.GetActivedBoxCount();
                v.LoadBoxCollection();
                v.OnDeActivateHistory();
                v.LoadLotteryCollection();

            }
        }
        private void txtPackId_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            //Activate
            var v = this.DataContext as HomeVM;
            string PacketID = args.QueryText;
            v.OnReceiveHistory();
            txtPackId.ItemsSource = _listSuggestion;
            var s = v.HistoryColl.Where(x => x.Packet_No == PacketID).FirstOrDefault();

        }
        private void txtGameId_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            //Activate
            var v = this.DataContext as HomeVM;
            if (v.IsNewStoreChecked != true)
            {
                string GameID = args.SelectedItem.ToString();
                v.OnReceiveHistory();
                _listSuggestion = v.HistoryColl.Where(x => x.Game_Id == GameID).Select(x => x.Packet_No).ToList();
                txtPackId.ItemsSource = _listSuggestion;
            }
        }
        private void btnCancel1_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisible = true;
            v.IsShowActivateReturnPopup = false;
            v.ReceiveselectedData = null;
            v.Active_StatusObj = new Activate_Ticket();
            v.Return_Obj = new Return_Details();
        }
        private void txtGameId_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                //Activate
                var v = this.DataContext as HomeVM;
                string GameID = args.QueryText;
                if (v.IsNewStoreChecked != true)
                {
                    v.OnReceiveHistory();

                    // txtPackId.ItemsSource = _listSuggestion;
                    var s = v.HistoryColl.Where(x => x.Game_Id == GameID && x.Store_Id == ApplicationData.Store_Id).FirstOrDefault();
                    if (args.QueryText != null)
                    {
                        v.Active_StatusObj.Ticket_Name = s.Ticket_Name;
                        v.Active_StatusObj.Price = s.Price;
                        v.Active_StatusObj.Start_No = s.Start_No;
                        v.Active_StatusObj.End_No = s.End_No;
                    }
                    else
                    {
                        v.Active_StatusObj.Ticket_Name = "";
                        v.Active_StatusObj.Price = 0;
                        v.Active_StatusObj.Start_No = "";
                        v.Active_StatusObj.End_No = "";
                    }
                }
                else
                {
                    v.OnMasterHistory();
                    var s = v.MasterColl.Where(x => x.Game_Id == GameID && x.Store_Id == ApplicationData.Store_Id).FirstOrDefault();
                    if (args.QueryText != null)
                    {
                        v.Active_StatusObj.Game_Id = s.Game_Id;
                        v.Active_StatusObj.Packet_No = s.Packet_No;
                        v.Active_StatusObj.Ticket_Name = s.Ticket_Name;
                        v.Active_StatusObj.Price = Convert.ToInt32(s.Rate);
                        v.Active_StatusObj.Start_No = s.Start_No;
                        v.Active_StatusObj.End_No = s.End_No;
                    }
                    else
                    {
                        v.Active_StatusObj.Ticket_Name = "";
                        v.Active_StatusObj.Price = 0;
                        v.Active_StatusObj.Start_No = "";
                        v.Active_StatusObj.End_No = "";
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
        private async void txtGameIdSettle_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            sender.ItemsSource = null;
            txtPackId.ItemsSource = null;
            if (v.SelectedActived_Box_ForSettle == null)
            {
                if (txtGameIdSettle.Text == "")
                {
                    v.IsVisibleComboBox = Visibility.Visible;
                    v.Settle_Obj.Packet_No = "";
                    v.Settle_Obj.Ticket_Name = "";
                    v.Settle_Obj.Price = 0;
                    v.Settle_Obj.Start_No = "";
                    v.Settle_Obj.End_No = "";
                }
                else
                {
                    v.OnReceiveHistory();
                    if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                    {
                        var coll = v.HistoryColl.Select(x => x.Game_Id).ToList();
                        _listSuggestion = (coll.Where(x => x.StartsWith(sender.Text)).Distinct()).ToList();
                        if (_listSuggestion.Count == 0)
                        {
                            var dialog1 = new MessageDialog("Not Availble in Store Inventory.");
                            await dialog1.ShowAsync();
                        }
                        else
                        {
                            sender.ItemsSource = _listSuggestion;
                        }


                    }
                }

            }

            else
            {

                if (txtGameIdSettle.Text == "")
                {
                    v.SelectedActived_Box_ForSettle = null;
                    v.Settle_Obj.Box_No = null;
                    v.IsVisibleComboBox = Visibility.Visible;
                    v.Settle_Obj.Packet_No = "";
                    v.Settle_Obj.Ticket_Name = "";
                    v.Settle_Obj.Price = 0;
                    v.Settle_Obj.Start_No = "";
                    v.Settle_Obj.End_No = "";
                }
            }

        }
        private void txtGameIdSettle_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                var v = this.DataContext as HomeVM;
                v.IsVisibleComboBox = Visibility.Collapsed;
                string GameID = args.QueryText;
                v.OnReceiveHistory();


                txtAutosuggest.ItemsSource = _listSuggestion;
                var s = v.HistoryColl.Where(x => x.Game_Id == GameID).FirstOrDefault();
                if (args.QueryText != null)
                {

                    v.Settle_Obj.Ticket_Name = s.Ticket_Name;
                    v.Settle_Obj.Price = s.Price;
                    v.Settle_Obj.Start_No = s.Start_No;
                    v.Settle_Obj.End_No = s.End_No;
                }
                else
                {
                    v.Settle_Obj.Ticket_Name = "";
                    v.Settle_Obj.Price = 0;
                    v.Settle_Obj.Start_No = "";
                    v.Settle_Obj.End_No = "";
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txtGameIdSettle_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            string GameID = args.SelectedItem.ToString();
            v.OnReceiveHistory();
            _listSuggestion = v.HistoryColl.Where(x => x.Game_Id == GameID).Select(x => x.Packet_No).ToList();
            txtPackId.ItemsSource = _listSuggestion;
        }

        //private void txtGameIdSettle_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        //{
        //    var v = this.DataContext as HomeVM;
        //   // TextBox t = sender as TextBox;

        //    if (((txtGameIdSettle.Text.Length)-8) != 0)
        //    {
        //        v.IsVisibleComboBox = Visibility.Collapsed;
        //        v.SelectedActived_Box_ForSettle = null;
        //    }
        //    else 
        //    {
        //        v.IsVisibleComboBox = Visibility.Visible;
        //    }

        //}

        private void txtGameIdSettle_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            // TextBox t = sender as TextBox;

            if (((txtGameIdSettle.Text.Length) - 8) != 0)
            {
                v.IsVisibleComboBox = Visibility.Collapsed;
                v.SelectedActived_Box_ForSettle = null;
            }
            else
            {
                v.IsVisibleComboBox = Visibility.Visible;
            }


        }
        public async void showMessage()
        {
            IsValidBarcode = false;
            var dialog = new MessageDialog("Please Enter Only Numbers.");
            await dialog.ShowAsync();
        }
        public bool SwitchCase(Char Barcode)
        {

            switch (Barcode)
            {
                case '-':
                    if (Barcode == '-')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '/':
                    if (Barcode == '/')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '+':
                    if (Barcode == '+')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '=':
                    if (Barcode == '=')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case ' ':
                    if (Barcode == ' ')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '[':
                    if (Barcode == '[')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case ']':
                    if (Barcode == ']')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '{':
                    if (Barcode == '}')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '}':
                    if (Barcode == '}')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '*':
                    if (Barcode == '*')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '#':
                    if (Barcode == '#')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '@':
                    if (Barcode == '@')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '$':
                    if (Barcode == '$')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '%':
                    if (Barcode == '%')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                case '^':
                    if (Barcode == '^')
                    {
                        showMessage();
                    }
                    else
                    {
                        IsValidBarcode = true;
                    }
                    break;

                default:
                    IsValidBarcode = true;
                    break;

            }
            return IsValidBarcode;
        }
        int eventCount1 = 0;
        private async void txtReceiveBarcode_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount1++;
                if (eventCount1 == 2)
                {
                    eventCount1 = 0;
                    string s = txtReceiveBarcode.Text.ToString();
                    foreach (Char i in s)
                    {
                        Char c = i;
                        IsValidBarcode = SwitchCase(c);
                    }
                    if (IsValidBarcode)
                    {
                        if (GetBarcodeFormat != null)
                        {

                            Words = txtReceiveBarcode.Text;

                            if (txtReceiveBarcode.Text.Length < GetBarcodeFormat.BarCodeLength || txtReceiveBarcode.Text.Length > GetBarcodeFormat.BarCodeLength)
                            {
                                if (Flag == 0)
                                {
                                    Flag = 1;
                                    var dialog = new MessageDialog("Barcode must be Equal to " + GetBarcodeFormat.BarCodeLength + " Digit For " + ApplicationData.SelectedState + " State.");
                                    await dialog.ShowAsync();
                                }
                            }
                            else
                            {
                                int GameidRange = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
                                int PacketidRange = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                                int SequencenoRange = GetBarcodeFormat.SequenceIDTo - GetBarcodeFormat.SequenceNoFrom;


                                string GameId = Words.Substring(GetBarcodeFormat.GameIDFrom, GameidRange);
                                string PacketID = Words.Substring(GetBarcodeFormat.PacketIDFrom, PacketidRange);
                                string Startno = Words.Substring(GetBarcodeFormat.SequenceNoFrom, SequencenoRange);


                                v.ReceiveObj.Game_Id = GameId;
                                v.ReceiveObj.Packet_No = PacketID;
                                v.ReceiveObj.Start_No = Startno;

                                v.OnMasterHistory();
                                // var s1 = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                                var s1 = v.MasterColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                                var v1 = s1.Where(p => p.Game_Id == GameId).FirstOrDefault();

                                if (v1 != null)
                                {
                                    v.ReceiveObj.Game_Id = v1.Game_Id;
                                    // v.ReceiveObj.Packet_No = s.Packet_No;
                                    v.ReceiveObj.Ticket_Name = v1.Ticket_Name;
                                    v.ReceiveObj.Rate = v1.Rate;
                                    //v.ReceiveObj.Start_No = v1.Start_No;
                                    v.ReceiveObj.End_No = v1.End_No;
                                }
                                //else
                                //{
                                //    var dialog1 = new MessageDialog("Not Available in Master List.");
                                //    await dialog1.ShowAsync();
                                //}
                            }
                        }
                        else
                        {
                            var dialog1 = new MessageDialog("Barcode Format was Not Set for " + ApplicationData.SelectedState + " State.");
                            await dialog1.ShowAsync();
                        }
                        Flag = 0;
                    }
                }
            }
        }
        private async void txtReceiveBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (txtReceiveBarcode.Text == "")
            {
                txtReceiveGameIDAutoSuggest.Text = "";
                txtReceivedPacketId.Text = "";
            }

            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // IsValidBarcode = SwitchCase(last);

                // isValidBarcode = true;
                if (txtReceiveBarcode.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Barcode must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtReceiveBarcode.Text = txtReceiveBarcode.Text.Substring(0, txtReceiveBarcode.Text.Length - 1);
                            txtReceiveBarcode.SelectionStart = (txtReceiveBarcode.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }

                    }
            }
        }
        private void txtAutosuggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            if (v.SelectedActived_Box_ForSettle != null)
            {
                txtAutosuggest.ItemsSource = null;
            }
        }
        private async void txtBarcodeActive_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (txtBarcodeActive.Text == "")
            {
                txtGameId.Text = "";
                txtPackId.Text = "";
                v.Active_StatusObj.Ticket_Name = "";
                v.Active_StatusObj.Price = 0;
                v.Active_StatusObj.Start_No = "";
                v.Active_StatusObj.End_No = "";
            }

            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // IsValidBarcode = SwitchCase(last);
                // isValidBarcode = true;
                //if (txtBarcodeActive.Text != null)
                if (!regex.IsMatch(last.ToString()))
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Barcode must be numeric.");
                    await dialog.ShowAsync();
                    try
                    {
                        txtBarcodeActive.Text = txtBarcodeActive.Text.Substring(0, txtBarcodeActive.Text.Length - 1);
                        txtBarcodeActive.SelectionStart = (txtBarcodeActive.Text.Length + 1) - 1;
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }

        private async void txtBarcodeActive_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount1++;
                if (eventCount1 == 2)
                {
                    eventCount1 = 0;
                    string s = txtBarcodeActive.Text.ToString();

                    foreach (Char i in s)
                    {
                        Char c = i;
                        IsValidBarcode = SwitchCase(c);
                    }
                    if (IsValidBarcode)
                    {
                        if (GetBarcodeFormat != null)
                        {
                            Words = txtBarcodeActive.Text;

                            if (txtBarcodeActive.Text.Length < GetBarcodeFormat.BarCodeLength || txtBarcodeActive.Text.Length > GetBarcodeFormat.BarCodeLength)
                            {
                                if (Flag == 0)
                                {
                                    Flag = 1;
                                    var dialog = new MessageDialog("Barcode must be Equal to " + GetBarcodeFormat.BarCodeLength + " Digit For " + ApplicationData.SelectedState + " State.");
                                    await dialog.ShowAsync();
                                }
                            }
                            else
                            {
                                int abc = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
                                int pqr = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                                int xyz = GetBarcodeFormat.SequenceIDTo - GetBarcodeFormat.SequenceNoFrom;

                                string GameId = Words.Substring(GetBarcodeFormat.GameIDFrom, abc);
                                string PacketId = Words.Substring(GetBarcodeFormat.PacketIDFrom, pqr);
                                string StartNo = Words.Substring(GetBarcodeFormat.SequenceNoFrom, xyz);
                                // string c = Words.Substring(list.SequenceNoFrom, xyz);

                                v.Active_StatusObj.Game_Id = GameId;
                                v.Active_StatusObj.Packet_No = PacketId;
                                v.Active_StatusObj.Start_No = StartNo;

                                if (v.IsNewStoreChecked == true)
                                {

                                    v.OnMasterHistory();
                                    // var s1 = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                                    var s1 = v.MasterColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                                    var v1 = s1.Where(p => p.Game_Id == GameId).FirstOrDefault();

                                    if (v1 != null)
                                    {
                                        v.Active_StatusObj.Game_Id = v1.Game_Id;
                                        v.Active_StatusObj.Ticket_Name = v1.Ticket_Name;
                                        v.Active_StatusObj.Price = Convert.ToInt32(v1.Rate);
                                        // v.Active_StatusObj.Start_No = v1.Start_No;
                                        v.Active_StatusObj.End_No = v1.End_No;

                                        //if (Convert.ToInt32(v.Active_StatusObj.Start_No) > Convert.ToInt32(v.Active_StatusObj.End_No))
                                        //{
                                        //    IsValid = false;
                                        //    var dialog = new MessageDialog("Start no. must be less than End no.");
                                        //    await dialog.ShowAsync();
                                        //    IsHitTestVisible = false;
                                        //}
                                        //else
                                        //{
                                        //    v.OnStatusUpdate();
                                        //}

                                    }
                                    else
                                    {
                                        var dialog = new MessageDialog("Not Available in Master List , Do you want to Add to Mater List ?");
                                        dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                                        dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                                        var res = await dialog.ShowAsync();
                                        if ((int)res.Id == 0)
                                        {
                                            v.IsHitTestVisiblePopup = false;
                                            v.IsAddInventoryPopup = true;

                                        }
                                    }
                                }
                                else
                                {
                                    v.OnReceiveHistory();
                                    //var s1 = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                                    var s1 = v.HistoryColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                                    var v1 = s1.Where(p => p.Game_Id == GameId && p.Packet_No == PacketId).FirstOrDefault();

                                    if (v1 != null)
                                    {
                                        v.Active_StatusObj.Game_Id = v1.Game_Id;
                                        //v.ReceiveObj.Packet_No = s.Packet_No;
                                        v.Active_StatusObj.Ticket_Name = v1.Ticket_Name;
                                        v.Active_StatusObj.Price = v1.Price;
                                        v.Active_StatusObj.Start_No = v1.Start_No;
                                        v.Active_StatusObj.End_No = v1.End_No;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var dialog = new MessageDialog("Barcode Format was Not Set for " + ApplicationData.SelectedState + " State.");
                            await dialog.ShowAsync();
                        }
                        Flag = 0;
                    }
                }
            }
        }
        private async void txtBarcodeDeactivate_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount1++;
                if (eventCount1 == 2)
                {
                    eventCount1 = 0;
                    if (GetBarcodeFormat != null)
                    {
                        Words = txtBarcodeDeactivate.Text;

                        if (txtBarcodeDeactivate.Text.Length < GetBarcodeFormat.BarCodeLength || txtBarcodeDeactivate.Text.Length > GetBarcodeFormat.BarCodeLength)
                        {

                            var dialog = new MessageDialog("Barcode must be Equal to " + GetBarcodeFormat.BarCodeLength + " Digit For " + ApplicationData.SelectedState + " State.");
                            await dialog.ShowAsync();

                        }
                        else
                        {
                            int abc = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
                            int pqr = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                            int xyz = GetBarcodeFormat.SequenceIDTo - GetBarcodeFormat.SequenceNoFrom;

                            string a = Words.Substring(GetBarcodeFormat.GameIDFrom, abc);
                            string b = Words.Substring(GetBarcodeFormat.PacketIDFrom, pqr);
                            string c = Words.Substring(GetBarcodeFormat.SequenceNoFrom, xyz);


                            v.ActiveBoxObj.Game_Id = a;
                            v.ActiveBoxObj.Packet_No = b;
                            v.ActiveBoxObj.Stopped_At = c;

                            v.OnActivateHistory();
                            // var s1 = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                            var s1 = v.ActivatehistoryColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                            var v1 = s1.Where(p => p.Game_Id == a && p.Packet_No == b).FirstOrDefault();

                            if (v1 != null)
                            {
                                v.IsContentChecked = true;
                                v.ActiveBoxObj.Box_No = v1.Box_No;
                                v.ActiveBoxObj.Game_Id = v1.Game_Id;
                                // v.ReceiveObj.Packet_No = s.Packet_No;
                                v.ActiveBoxObj.Ticket_Name = v1.Ticket_Name;
                                v.ActiveBoxObj.Price = v1.Price;
                                v.ActiveBoxObj.Start_No = v1.Start_No;
                                //v.ActiveBoxObj.End_No = v1.End_No;
                            }
                            else
                            {
                                v.ActiveBoxObj.Ticket_Name = "";
                                v.ActiveBoxObj.Price = 0;
                                v.ActiveBoxObj.Start_No = "";

                                var dialog = new MessageDialog("Not Available in Store Inventory.");
                                await dialog.ShowAsync();

                                CbActiveBoxes.SelectedIndex = -1;
                                // v.SelectedActived_Box.Box_No = null;
                                v.ActiveBoxObj.Game_Id = "";
                                v.ActiveBoxObj.Packet_No = "";
                                v.ActiveBoxObj.Ticket_Name = "";
                                v.ActiveBoxObj.Price = 0;
                                v.ActiveBoxObj.Start_No = "";
                                v.ActiveBoxObj.End_No = "";
                                v.ActiveBoxObj.Stopped_At = "";
                                txtBarcodeDeactivate.Text = "";
                            }
                        }
                    }
                    else
                    {
                        var dialog = new MessageDialog("Barcode Format was Not Set for " + ApplicationData.SelectedState + " State.");
                        await dialog.ShowAsync();
                    }

                }
            }
        }
        private async void txtBarcodeSoldout_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            ObservableCollection<BarcodeFormat> GetBarcodeDetails = new ObservableCollection<BarcodeFormat>();
            var vm = this.DataContext as HomeVM;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount1++;
                if (eventCount1 == 2)
                {
                    eventCount1 = 0;
                    string s = txtBarcodeSoldout.Text.ToString();
                    foreach (Char i in s)
                    {
                        Char c = i;
                        IsValidBarcode = SwitchCase(c);
                    }
                    if (IsValidBarcode)
                    {
                        if (GetBarcodeFormat != null)
                        {
                            Words = txtBarcodeSoldout.Text;
                            if (txtBarcodeSoldout.Text.Length < GetBarcodeFormat.BarCodeLength || txtBarcodeSoldout.Text.Length > GetBarcodeFormat.BarCodeLength)
                            {
                                MessageDialog msgbox = new MessageDialog("Barcode must be Equal to " + GetBarcodeFormat.BarCodeLength + " Digit For " + ApplicationData.SelectedState + " State.");
                                await msgbox.ShowAsync();

                            }
                            else
                            {
                                int abc = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
                                int pqr = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                                int xyz = GetBarcodeFormat.SequenceIDTo - GetBarcodeFormat.SequenceNoFrom;

                                string GameId = Words.Substring(GetBarcodeFormat.GameIDFrom, abc);
                                string PacketId = Words.Substring(GetBarcodeFormat.PacketIDFrom, pqr);
                                string EndNo = Words.Substring(GetBarcodeFormat.SequenceNoFrom, xyz);

                                vm.SoldOutObj.Game_Id = GameId;
                                vm.SoldOutObj.Packet_No = PacketId;
                                vm.SoldOutObj.End_No = EndNo;

                                vm.OnActivateHistory();
                                // v.OnSoldOutHistory();
                                // var s1 = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                                var s1 = vm.ActivatehistoryColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                                var v1 = s1.Where(p => p.Game_Id == GameId && p.Packet_No == PacketId).FirstOrDefault();

                                if (v1 != null)
                                {
                                    vm.IsContentChecked = true;
                                    vm.SoldOutObj.Box_No = v1.Box_No;
                                    vm.SoldOutObj.Game_Id = v1.Game_Id;
                                    // v.ReceiveObj.Packet_No = s.Packet_No;
                                    vm.SoldOutObj.Ticket_Name = v1.Ticket_Name;
                                    vm.SoldOutObj.Price = v1.Price;
                                    vm.SoldOutObj.Start_No = v1.Start_No;
                                    vm.SoldOutObj.End_No = v1.End_No;
                                }
                                else
                                {
                                    vm.SoldOutObj.Ticket_Name = "";
                                    vm.SoldOutObj.Price = 0;
                                    vm.SoldOutObj.Start_No = "";

                                    var dialog1 = new MessageDialog("Not Available in Store Inventory.");
                                    await dialog1.ShowAsync();

                                    CbBoxes2.SelectedIndex = -1;
                                    vm.SoldOutObj.Game_Id = "";
                                    vm.SoldOutObj.Packet_No = "";
                                    vm.SoldOutObj.Ticket_Name = "";
                                    vm.SoldOutObj.Price = 0;
                                    vm.SoldOutObj.Start_No = "";
                                    vm.SoldOutObj.End_No = "";
                                    txtBarcodeSoldout.Text = "";
                                }
                            }
                        }
                        else
                        {
                            var dialog1 = new MessageDialog("Barcode Format was Not Set for " + ApplicationData.SelectedState + " State.");
                            await dialog1.ShowAsync();
                        }

                    }
                }
            }
        }
        private async void TxtReturnBarcode_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            Objlogin = new Login();
            var v = this.DataContext as HomeVM;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount1++;
                if (eventCount1 == 2)
                {
                    eventCount1 = 0;
                    string s = TxtReturnBarcode.Text.ToString();
                    foreach (Char i in s)
                    {
                        Char c = i;
                        IsValidBarcode = SwitchCase(c);

                    }
                    if (IsValidBarcode)
                    {
                        if (GetBarcodeFormat != null)
                        {
                            Words = TxtReturnBarcode.Text;

                            if (TxtReturnBarcode.Text.Length < GetBarcodeFormat.BarCodeLength || TxtReturnBarcode.Text.Length > GetBarcodeFormat.BarCodeLength)
                            {
                                var dialog = new MessageDialog("Barcode must be Equal to " + GetBarcodeFormat.BarCodeLength + " Digit For " + ApplicationData.SelectedState + " State.");
                                await dialog.ShowAsync();

                            }
                            else
                            {
                                int abc = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
                                int pqr = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                                int xyz = GetBarcodeFormat.SequenceIDTo - GetBarcodeFormat.SequenceNoFrom;

                                string GameID = Words.Substring(GetBarcodeFormat.GameIDFrom, abc);
                                string PacketId = Words.Substring(GetBarcodeFormat.PacketIDFrom, pqr);
                                string EndNo = Words.Substring(GetBarcodeFormat.SequenceNoFrom, xyz);

                                v.Return_Obj.Game_Id = GameID;
                                v.Return_Obj.Packet_No = PacketId;
                                v.Return_Obj.End_No = EndNo;
                                v.OnReceiveHistory();
                                v.OnActivateHistory();
                                var s1 = v.HistoryColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                                var v1 = s1.Where(p => p.Game_Id == GameID && p.Packet_No == PacketId).FirstOrDefault();
                                var s2 = v.ActivatehistoryColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                                var v2 = s2.Where(p => p.Game_Id == GameID && p.Packet_No == PacketId).FirstOrDefault();

                                if (v1 != null)
                                {
                                    ReturnEndno.IsReadOnly = true;
                                    v.IsContentChecked = true;
                                    v.Return_Obj.Box_No = v1.Box_No;
                                    v.Return_Obj.Game_Id = v1.Game_Id;
                                    // v.ReceiveObj.Packet_No = s.Packet_No;
                                    v.Return_Obj.Ticket_Name = v1.Ticket_Name;
                                    v.Return_Obj.Price = v1.Price;
                                    v.Return_Obj.Start_No = v1.Start_No;
                                    // v.Return_Obj.End_No = v1.End_No;
                                }
                                else if (v2 != null)
                                {
                                    ReturnEndno.IsReadOnly = false;
                                    v.IsContentChecked = true;
                                    v.ActiveBoxObj.Box_No = v2.Box_No;
                                    v.Return_Obj.Box_No = v2.Box_No;
                                    v.Return_Obj.Game_Id = v2.Game_Id;
                                    // v.ReceiveObj.Packet_No = s.Packet_No;
                                    v.Return_Obj.Ticket_Name = v2.Ticket_Name;
                                    v.Return_Obj.Price = v2.Price;
                                    v.Return_Obj.Start_No = v2.Start_No;
                                    // v.Return_Obj.End_No = v1.End_No;
                                }
                                else
                                {
                                    v.Return_Obj.Ticket_Name = "";
                                    v.Return_Obj.Price = 0;
                                    v.Return_Obj.Start_No = "";

                                    var dialog = new MessageDialog("Not Available in Store Inventory.");
                                    await dialog.ShowAsync();

                                    CbBoxes3.SelectedIndex = -1;
                                    v.Return_Obj.Game_Id = "";
                                    v.Return_Obj.Packet_No = "";
                                    v.Return_Obj.Ticket_Name = "";
                                    v.Return_Obj.Price = 0;
                                    v.Return_Obj.Start_No = "";
                                    v.Return_Obj.End_No = "";
                                    TxtReturnBarcode.Text = "";
                                }
                            }
                        }
                        else
                        {
                            var dialog = new MessageDialog("Barcode Format was Not Set for " + ApplicationData.SelectedState + " State.");
                            await dialog.ShowAsync();
                        }
                    }
                }
            }
        }
        private async void TxtSettleBarcode_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount1++;
                if (eventCount1 == 2)
                {
                    eventCount1 = 0;
                    string s = TxtSettleBarcode.Text.ToString();
                    foreach (Char i in s)
                    {
                        Char c = i;
                        IsValidBarcode = SwitchCase(c);
                    }
                    if (IsValidBarcode)
                    {
                        if (GetBarcodeFormat != null)
                        {
                            Words = TxtSettleBarcode.Text;

                            if (TxtSettleBarcode.Text.Length < GetBarcodeFormat.BarCodeLength || TxtSettleBarcode.Text.Length > GetBarcodeFormat.BarCodeLength)
                            {

                                var dialog = new MessageDialog("Barcode must be Equal to " + GetBarcodeFormat.BarCodeLength + " Digit For " + ApplicationData.SelectedState + " State.");
                                await dialog.ShowAsync();

                            }
                            else
                            {
                                int abc = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
                                int pqr = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                                int xyz = GetBarcodeFormat.SequenceIDTo - GetBarcodeFormat.SequenceNoFrom;

                                string GameId = Words.Substring(GetBarcodeFormat.GameIDFrom, abc);
                                string PacketId = Words.Substring(GetBarcodeFormat.PacketIDFrom, pqr);
                                string c = Words.Substring(GetBarcodeFormat.SequenceNoFrom, xyz);

                                v.Settle_Obj.Game_Id = GameId;
                                v.Settle_Obj.Packet_No = PacketId;
                                v.Settle_Obj.End_No = c;

                                // v.OnActivateHistory();
                                v.OnActivateHistory();
                                // var s1 = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                                var s1 = v.ActivatehistoryColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                                var v1 = s1.Where(p => p.Game_Id == GameId && p.Packet_No == PacketId).FirstOrDefault();

                                if (v1 != null)
                                {
                                    v.IsContentChecked = true;
                                    v.Settle_Obj.Box_No = v1.Box_No;
                                    v.Settle_Obj.Game_Id = v1.Game_Id;
                                    // v.ReceiveObj.Packet_No = s.Packet_No;
                                    v.Settle_Obj.Ticket_Name = v1.Ticket_Name;
                                    v.Settle_Obj.Price = v1.Price;
                                    v.Settle_Obj.Start_No = v1.Start_No;
                                    v.Settle_Obj.End_No = v1.End_No;
                                }
                                else
                                {
                                    v.Settle_Obj.Ticket_Name = "";
                                    v.Settle_Obj.Price = 0;
                                    v.Settle_Obj.Start_No = "";

                                    //if(v.SettleTemp == 1)
                                    //{

                                    //}
                                    //else if(v.SettleTemp == 0)
                                    //{
                                    var dialog1 = new MessageDialog("Not Available in Store Inventory.");
                                    await dialog1.ShowAsync();
                                    //}                                                                    

                                    CbBoxes4.SelectedIndex = -1;
                                    v.Settle_Obj.Game_Id = "";
                                    v.Settle_Obj.Packet_No = "";
                                    v.Settle_Obj.Ticket_Name = "";
                                    v.Settle_Obj.Price = 0;
                                    v.Settle_Obj.Start_No = "";
                                    v.Settle_Obj.End_No = "";
                                    TxtSettleBarcode.Text = "";
                                }
                            }
                        }
                        else
                        {
                            var dialog1 = new MessageDialog("Barcode Format was Not Set for " + ApplicationData.SelectedState + " State.");
                            await dialog1.ShowAsync();
                        }
                    }
                }
            }
            //v.SettleTemp = 0;
        }
        private async void txtBarcodeDeactivate_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (txtBarcodeDeactivate.Text == "")
            {
                v.IsContentChecked = false;
                v.ActiveBoxObj.Game_Id = "";
                v.ActiveBoxObj.Packet_No = "";
                v.ActiveBoxObj.Ticket_Name = "";
                v.ActiveBoxObj.Price = 0;
                v.ActiveBoxObj.Start_No = "";
                v.ActiveBoxObj.End_No = "";
                v.ActiveBoxObj.Stopped_At = "";

            }
            else
            {
                TextBox t = sender as TextBox;
                string s = t.Text.ToString();
                if (s != string.Empty)
                {
                    char last = s[s.Length - 1];
                    // IsValidBarcode = SwitchCase(last);

                    // isValidBarcode = true;
                    //if (txtBarcodeDeactivate.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Barcode must be Numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtBarcodeDeactivate.Text = txtBarcodeDeactivate.Text.Substring(0, txtBarcodeDeactivate.Text.Length - 1);
                            txtBarcodeDeactivate.SelectionStart = (txtBarcodeDeactivate.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }


        }
        private async void txtBarcodeSoldout_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (txtBarcodeSoldout.Text == "")
            {
                v.IsContentChecked = false;
                v.SoldOutObj.Game_Id = "";
                v.SoldOutObj.Packet_No = "";
                v.SoldOutObj.Ticket_Name = "";
                v.SoldOutObj.Price = 0;
                v.SoldOutObj.Start_No = "";
                v.SoldOutObj.End_No = "";

            }
            else
            {
                TextBox t = sender as TextBox;
                string s = t.Text.ToString();
                if (s != string.Empty)
                {
                    char last = s[s.Length - 1];
                    // IsValidBarcode = SwitchCase(last);

                    // isValidBarcode = true;
                    //if (txtReceiveBarcode.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Barcode must be Numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtBarcodeSoldout.Text = txtBarcodeSoldout.Text.Substring(0, txtBarcodeSoldout.Text.Length - 1);
                            txtBarcodeSoldout.SelectionStart = (txtBarcodeSoldout.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
        private async void TxtReturnBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (TxtReturnBarcode.Text == "")
            {
                v.IsContentChecked = false;
                // v.Return_Obj.Box_No = 0;
                v.Return_Obj.Game_Id = "";
                v.Return_Obj.Packet_No = "";
                v.Return_Obj.Ticket_Name = "";
                v.Return_Obj.Price = 0;
                v.Return_Obj.Start_No = "";
                v.Return_Obj.End_No = "";

            }
            else
            {
                TextBox t = sender as TextBox;
                string s = t.Text.ToString();
                if (s != string.Empty)
                {
                    char last = s[s.Length - 1];
                    if (TxtReturnBarcode.Text != null)
                    {
                        if (!regex.IsMatch(last.ToString()))
                        {
                            IsValid = false;
                            var dialog = new MessageDialog("Barcode must be Numeric.");
                            await dialog.ShowAsync();
                            try
                            {
                                TxtReturnBarcode.Text = TxtReturnBarcode.Text.Substring(0, TxtReturnBarcode.Text.Length - 1);
                                TxtReturnBarcode.SelectionStart = (TxtReturnBarcode.Text.Length + 1) - 1;
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
            }
        }
        private async void TxtSettleBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (TxtSettleBarcode.Text == "")
            {
                v.IsContentChecked = false;
                v.Settle_Obj.Game_Id = "";
                v.Settle_Obj.Packet_No = "";
                v.Settle_Obj.Ticket_Name = "";
                v.Settle_Obj.Price = 0;
                v.Settle_Obj.Start_No = "";
                v.Settle_Obj.End_No = "";

            }
            else
            {
                TextBox t = sender as TextBox;
                string s = t.Text.ToString();
                if (s != string.Empty)
                {
                    char last = s[s.Length - 1];
                    // IsValidBarcode = SwitchCase(last);

                    // isValidBarcode = true;
                    //if (txtReceiveBarcode.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Barcode must be Numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            TxtSettleBarcode.Text = TxtSettleBarcode.Text.Substring(0, TxtSettleBarcode.Text.Length - 1);
                            TxtSettleBarcode.SelectionStart = (TxtSettleBarcode.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
        private async void btnReturnback_Click(object sender, RoutedEventArgs e)
        {
            // var B = ((sender as Button).DataContext as Activation_Box).Packet_No;

            var c = ((sender as Button).DataContext as Activation_Box).Packet_No;
            var d = ((sender as Button).DataContext as Activation_Box).Box_No;
            var f = ((sender as Button).DataContext as Activation_Box).Game_Id;
            var g = ((sender as Button).DataContext as Activation_Box).Start_No;
            var h = ((sender as Button).DataContext as Activation_Box).End_No;
            var v = this.DataContext as HomeVM;
            ABC = new Activation_Box(c);
            ABC.Packet_No = c;
            ABC.Game_Id = f;
            ABC.Box_No = d;
            ABC.Start_No = g;
            ABC.End_No = h;
            ABC.State = ApplicationData.SelectedState;
            ABC.Store_Id = ApplicationData.Store_Id;
            ABC.EmployeeId = ApplicationData.Emp_Id;
            string json = "";

            json = Newtonsoft.Json.JsonConvert.SerializeObject(ABC);
            HttpResponseMessage response = client.PostAsync("api/Return/ReturnBack", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (response.ReasonPhrase == "Not Found")
            {
                var dialog = new MessageDialog("Packet cannot be Return Back until new packet in Box " + d + " is de-activated ");
                await dialog.ShowAsync();
            }
            else if (response.ReasonPhrase == "Conflict")
            {
                var dialog = new MessageDialog("Packet Already Active");
                await dialog.ShowAsync();
            }
            else if (response.IsSuccessStatusCode)
            {
                var dialog = new MessageDialog("Packet Return Back Successfully.");
                await dialog.ShowAsync();
                // GetRefreshUnsoldGrid();
                v.GetReturnedBoxCount();
                // v.GetUpdatedsoldCount();
                v.OnActivateHistory();
                // v.OnSoldOutHistory();
                v.GetActivedBoxCount();
                v.GetEmptyBoxCount();
                v.GetSoldOutBoxCount();
                v.LoadBoxCollection();
                v.LoadActive_BoxCollection();
                v.LoadLotteryCollection();

            }

        }
        private async void txtGameId_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Activate GameID Textchanged
            var v = this.DataContext as HomeVM;
            if (v.IsNewStoreChecked != true)
            {
                v.OnReceiveHistory();
                //if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                //{
                //var coll = v.HistoryColl.Select(x => x.Game_Id).ToList();
                var coll = v.HistoryColl.Where(x => x.State == ApplicationData.SelectedState).Select(x => x.Game_Id).ToList();
                _listSuggestion = (coll.Where(x => x.StartsWith(sender.Text)).Distinct()).ToList();
                if (_listSuggestion.Count == 0 && v.FlagAutoGameID == 0 && v.Active_StatusObj.Game_Id != "")
                {
                    v.Active_StatusObj.Ticket_Name = "";
                    v.Active_StatusObj.Price = 0;
                    v.Active_StatusObj.Start_No = "";
                    if (Flag == 0)
                    {
                        Flag = 1;
                        var dialog1 = new MessageDialog("Not Availble in Store Inventory.");
                        await dialog1.ShowAsync();
                    }
                    v.Active_StatusObj.Game_Id = "";
                    v.Active_StatusObj.Packet_No = "";
                    v.Active_StatusObj.Ticket_Name = "";
                    v.Active_StatusObj.Price = 0;
                    v.Active_StatusObj.Start_No = "";
                    v.Active_StatusObj.End_No = "";
                    txtBarcodeActive.Text = "";

                }
                else
                {
                    sender.ItemsSource = _listSuggestion;

                    if (txtGameId.Text == "")
                    {
                        //v.Active_StatusObj.Game_Id = "";
                        v.Active_StatusObj.Packet_No = "";
                        v.Active_StatusObj.Ticket_Name = "";
                        v.Active_StatusObj.Price = 0;
                        v.Active_StatusObj.Start_No = "";
                        v.Active_StatusObj.End_No = "";

                    }
                    // }
                }
                Flag = 0;
            }
            else
            {
                v.OnMasterHistory();

                //if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                //{

                var coll = v.MasterColl.Where(x => x.State == ApplicationData.SelectedState && x.Store_Id == ApplicationData.Store_Id).Select(x => x.Game_Id).ToList();
                //var coll = v.MasterColl.Where(x => x.State).ToList();
                //var coll = v.MasterColl.Select(x => x.Game_Id).ToList();
                _listSuggestion = (coll.Where(x => x.StartsWith(sender.Text)).Distinct()).ToList();
                if (_listSuggestion.Count == 0)
                {
                    var dialog = new MessageDialog("Not Available in Master List.Please Add in Master List.");
                    await dialog.ShowAsync();
                    txtNewstoreActiveBarcode.Text = "";
                    v.Active_StatusObj.Game_Id = "";
                    v.Active_StatusObj.Packet_No = "";
                    v.Active_StatusObj.Ticket_Name = "";
                    v.Active_StatusObj.Price = 0;
                    v.Active_StatusObj.Start_No = "";
                    v.Active_StatusObj.End_No = "";
                }
                else
                {
                    sender.ItemsSource = _listSuggestion;

                    if (txtNewstoreActiveGameIDAutoSuggest.Text == "")
                    {
                        v.Active_StatusObj.Game_Id = "";
                        v.Active_StatusObj.Packet_No = "";
                        v.Active_StatusObj.Ticket_Name = "";
                        v.Active_StatusObj.Price = 0;
                        v.Active_StatusObj.Start_No = "";
                        v.Active_StatusObj.End_No = "";

                    }
                }
            }
        }
        private void txtPackId_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            if (NewStoreCheck.IsChecked != true)
            {
                string GameID = args.SelectedItem.ToString();
                v.OnReceiveHistory();
                _listSuggestion = v.HistoryColl.Where(x => x.Game_Id == GameID).Select(x => x.Packet_No).ToList();
                txtPackId.ItemsSource = _listSuggestion;
            }
        }
        private async void btnUnsettle_Click(object sender, RoutedEventArgs e)
        {
            var c = ((sender as Button).DataContext as Activation_Box).Packet_No;
            var d = ((sender as Button).DataContext as Activation_Box).Box_No;
            var f = ((sender as Button).DataContext as Activation_Box).Game_Id;
            var g = ((sender as Button).DataContext as Activation_Box).Start_No;
            var v = this.DataContext as HomeVM;
            ABC = new Activation_Box(c);
            ABC.Start_No = g;
            ABC.Packet_No = c;
            ABC.Game_Id = f;
            ABC.Box_No = d;
            ABC.State = ApplicationData.SelectedState;
            ABC.Store_Id = ApplicationData.Store_Id;
            ABC.EmployeeId = ApplicationData.Emp_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ABC);
            HttpResponseMessage response = client.PostAsync("api/Settle/Unsettle", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var dialog = new MessageDialog("Packet Unsettle");
                await dialog.ShowAsync();
                //  GetRefreshUnsoldGrid();
                //v.GetUpdatedsoldCount();
                //  v.OnActivateHistory();
                //v.OnSoldOutHistory();
                // v.OnReturnHistory();
                v.GetEmptyBoxCount();
                v.GetActivedBoxCount();
                //  v.OnSettelementHistory();
                v.GetSettledBoxCount();
                v.LoadBoxCollection();
                v.LoadLotteryCollection();
            }
            else if (response.ReasonPhrase == "Conflict")
            {
                var dialog = new MessageDialog("Can't Unsettle this packet already soldout");
                await dialog.ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog("Box Already Active");
                await dialog.ShowAsync();
            }
        }
        private void btnCloseShiftDashboard_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string content = btn.Content.ToString();
            //textBoxNo.Text = content;
            //txtDisplayEmptyBox_No.Text = content;
            txtCloseShiftBoxNo.Text = content;
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisible = false;
            v.IsHitTestVisiblePopup = false;
            // v.IsCloseShiftActivateBox = true;
            v.GetBoxRecord(content);
            if (v.Single_Record.Status == "Active" || v.Single_Record.Status == "Settle")
            {
                v.IsCloseShiftActivateBox = true;
            }
            else
            {
                v.IsCloseBoxReopen = true;
            }
        }
        private void BtCloseReportPopup_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisiblePopup = true;
            v.IsReportPopup = false;
            v.ShiftReportSelectedData = null;
            v.SingleShiftReportDate = null;
            string json = "";
            ObservableCollection<Shift_Details> tempshift = new ObservableCollection<Shift_Details>();


            v.ShiftObj = new Shift_Details();
            v.ShiftObj.StoreId = ApplicationData.Store_Id;
            v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
            json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
            var result = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (result.IsSuccessStatusCode)
            {
                var w = result.Content.ReadAsStringAsync().Result;
                tempshift = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(w);
            }
            var g = tempshift.LastOrDefault();




            if (v.IsReportPopup == false)
            {
                if (LastShiftChBox.IsChecked == true)
                {
                    v.IsCloseshitfPopup = true;
                    btnDailyReport.Visibility = Visibility.Visible;
                    PopupStartNewShift.IsOpen = false;
                    v.IsVisibleShiftSubmit = Visibility.Collapsed;
                }
                if (IsGenerateReport == true && LastShiftChBox.IsChecked == true)
                {
                    string json2 = "";
                    v.ShiftObj = new Shift_Details();
                    //btnDailyReport.Visibility = Visibility.Visible;
                    v.ShiftObj.StoreId = ApplicationData.Store_Id;
                    v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                    v.ShiftObj.IsReportGenerated = true;
                    json2 = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                    var response2 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json2, System.Text.Encoding.UTF8, "application/json")).Result;
                }

                //string json2 = "";
                //v.ShiftObj = new Shift_Details();
                ////btnDailyReport.Visibility = Visibility.Visible;
                //v.ShiftObj.StoreId = ApplicationData.Store_Id;
                //v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                //v.ShiftObj.IsReportGenerated = true;
                //json2 = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                //var response2 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json2, System.Text.Encoding.UTF8, "application/json")).Result;



                //string json1 = "";
                //v.SoldOutObj = new SoldOut_Details();
                //v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                //v.SoldOutObj.EmployeeID = 1;

                //json1 = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                //var response1 = client.PostAsync("api/CloseShift/CreatePDF", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

                //Uri source = new Uri("http://63.142.245.165///Pdf/DailyReport.pdf");
                //string destination = "LatshiftDailyReport.pdf";

                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(source);
                //WebResponse res = await req.GetResponseAsync();
                //Stream str = res.GetResponseStream();

                //StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                //StorageFile destinationFile = await local.CreateFileAsync(destination, CreationCollisionOption.ReplaceExisting);

                //await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, ReadStream(str));

                //await Windows.System.Launcher.LaunchFileAsync(destinationFile);
            }
            else if (g.IsLastShift == true)
            {
                v.IsCloseshitfPopup = true;
                btnDailyReport.Visibility = Visibility.Visible;
                PopupStartNewShift.IsOpen = false;
                v.IsVisibleShiftSubmit = Visibility.Collapsed;

                string json2 = "";
                v.ShiftObj = new Shift_Details();
                btnDailyReport.Visibility = Visibility.Visible;
                v.ShiftObj.StoreId = ApplicationData.Store_Id;
                v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                v.ShiftObj.IsReportGenerated = true;
                json2 = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                var response2 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json2, System.Text.Encoding.UTF8, "application/json")).Result;



                //string json1 = "";
                //v.SoldOutObj = new SoldOut_Details();
                //v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                //v.SoldOutObj.EmployeeID = 1;

                //json1 = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                //var response1 = client.PostAsync("api/CloseShift/CreatePDF", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

                //Uri source = new Uri("http://63.142.245.165///Pdf/DailyReport.pdf");
                //string destination = "LatshiftDailyReport.pdf";

                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(source);
                //WebResponse res = await req.GetResponseAsync();
                //Stream str = res.GetResponseStream();

                //StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                //StorageFile destinationFile = await local.CreateFileAsync(destination, CreationCollisionOption.ReplaceExisting);

                //await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, ReadStream(str));

            }
            else
            {
                v.IsCloseshitfPopup = true;
                PopupStartNewShift.IsOpen = false;
            }

            v.IsBtnSaveVisible = Visibility.Visible;
            v.IsVisibleGenerateReport = Visibility.Collapsed;
            v.TerminalObj = new Terminal_Details();
            if (GDMainDashboard.Visibility == Visibility.Visible)
            {
                v.Temp = 1;
            }
        }

        public void tempLastshiftchbox_Click()
        {
            var v = this.DataContext as HomeVM;
            v.ShiftObj = new Shift_Details();

            string json = "";
            v.ShiftObj.StoreId = ApplicationData.Store_Id;
            v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
            btnDailyReport.Visibility = Visibility.Collapsed;
            v.ShiftObj.IsLastShift = false;

            json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
            var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

        }
        private async void LastShiftChBox_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            ObservableCollection<Shift_Details> tempshift = new ObservableCollection<Shift_Details>();

            string json = "";

            if (LastShiftChBox.IsChecked == true)
            {
                v.ShiftObj = new Shift_Details();
                v.ShiftObj.StoreId = ApplicationData.Store_Id;
                v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                var result = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (result.IsSuccessStatusCode)
                {
                    var w = result.Content.ReadAsStringAsync().Result;
                    tempshift = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(w);
                }
                var g = tempshift.LastOrDefault();

                //    if (g.IsLastShift == true)
                //    {
                //        var dialog = new MessageDialog("Last shift is generated. Do you want to replace it ?");
                //        dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                //        dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                //        var res = await dialog.ShowAsync();

                //        if ((int)res.Id == 1)  // no
                //        {
                //            LastShiftChBox.IsChecked = false;
                //        }
                //        else if ((int)res.Id == 0)  // yes
                //        {
                //            v.ShiftObj = new Shift_Details();
                //            btnDailyReport.Visibility = Visibility.Visible;
                //            v.ShiftObj.StoreId = ApplicationData.Store_Id;
                //            v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                //            v.ShiftObj.IsLastShift = false;

                //            json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                //            var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                //            var dialog1 = new MessageDialog("Last shift is not generated");
                //            await dialog1.ShowAsync();

                //            LastShiftChBox.IsChecked = false;
                //            btnDailyReport.Visibility = Visibility.Collapsed;

                //        }
                //    }
                //    else
                //    {
                //        v.ShiftObj = new Shift_Details();
                //        btnDailyReport.Visibility = Visibility.Visible;
                //        v.ShiftObj.StoreId = ApplicationData.Store_Id;
                //        v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                //        v.ShiftObj.IsLastShift = true;

                //        json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                //        var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                //        if (v.Temp == 1)
                //        {
                //            v.ShiftObj = new Shift_Details();
                //            btnDailyReport.Visibility = Visibility.Visible;
                //            v.ShiftObj.StoreId = ApplicationData.Store_Id;
                //            v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                //            v.ShiftObj.IsReportGenerated = true;
                //            json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                //            var response2 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;


                //            string json1 = "";
                //            v.SoldOutObj = new SoldOut_Details();
                //            v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                //            v.SoldOutObj.EmployeeID = 1;

                //            json1 = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                //            var response1 = client.PostAsync("api/CloseShift/CreatePDF", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

                //            Uri source = new Uri("http://63.142.245.165///Pdf/DailyReport.pdf");
                //            string destination = "LastshiftDailyReport.pdf";

                //            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(source);
                //            WebResponse res1 = await req.GetResponseAsync();
                //            Stream str = res1.GetResponseStream();

                //            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                //            StorageFile destinationFile = await local.CreateFileAsync(destination, CreationCollisionOption.ReplaceExisting);

                //            await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, ReadStream(str));

                //            //await Windows.System.Launcher.LaunchFileAsync(destinationFile);
                //            }
                //        }
                //}
                //else
                //{
                //    btnDailyReport.Visibility = Visibility.Collapsed;
                //    v.ShiftObj.IsLastShift = false;

                //    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                //    var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                //}  

                if (g.IsReportGenerated == true)
                {
                    if (g.IsReportGenerated == true && g.IsLastShift == null)
                    {
                        var dialog = new MessageDialog("Do you want to do this shift as last shift?");
                        dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                        dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                        var res = await dialog.ShowAsync();
                        if ((int)res.Id == 1)
                        {
                            LastShiftChBox.IsChecked = false;
                        }
                        else if ((int)res.Id == 0)
                        {
                            v.ShiftObj = new Shift_Details();
                            btnDailyReport.Visibility = Visibility.Visible;
                            v.ShiftObj.StoreId = ApplicationData.Store_Id;
                            v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                            v.ShiftObj.IsLastShift = true;

                            json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                            var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                            if (v.Temp == 1)
                            {
                                v.ShiftObj = new Shift_Details();
                                btnDailyReport.Visibility = Visibility.Visible;
                                v.ShiftObj.StoreId = ApplicationData.Store_Id;
                                v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                                v.ShiftObj.IsReportGenerated = true;
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                                var response2 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;


                                //string json1 = "";
                                //v.SoldOutObj = new SoldOut_Details();
                                //v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                                //v.SoldOutObj.EmployeeID = 1;

                                //json1 = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                                //var response1 = client.PostAsync("api/CloseShift/CreatePDF", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

                                //Uri source = new Uri("http://63.142.245.165///Pdf/DailyReport.pdf");
                                //string destination = "LastshiftDailyReport.pdf";

                                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(source);
                                //WebResponse res1 = await req.GetResponseAsync();
                                //Stream str = res1.GetResponseStream();

                                //StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                                //StorageFile destinationFile = await local.CreateFileAsync(destination, CreationCollisionOption.ReplaceExisting);

                                //await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, ReadStream(str));

                                //await Windows.System.Launcher.LaunchFileAsync(destinationFile);
                            }
                        }
                    }
                    else
                    {
                        var dialog = new MessageDialog("Last shift is generated. Do you want to replace it ?");
                        dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                        dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                        var res = await dialog.ShowAsync();
                        if ((int)res.Id == 1)
                        {
                            LastShiftChBox.IsChecked = false;
                        }
                        else if ((int)res.Id == 0)
                        {
                            v.ShiftObj = new Shift_Details();
                            btnDailyReport.Visibility = Visibility.Visible;
                            v.ShiftObj.StoreId = ApplicationData.Store_Id;
                            v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                            v.ShiftObj.IsLastShift = true;

                            json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                            var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                            if (v.Temp == 1)
                            {
                                v.ShiftObj = new Shift_Details();
                                btnDailyReport.Visibility = Visibility.Visible;
                                v.ShiftObj.StoreId = ApplicationData.Store_Id;
                                v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                                v.ShiftObj.IsReportGenerated = true;
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                                var response2 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                            }
                        }
                    }

                }
                else
                {
                    btnDailyReport.Visibility = Visibility.Visible;
                    v.ShiftObj = new Shift_Details();
                    v.ShiftObj.StoreId = ApplicationData.Store_Id;
                    v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                    v.ShiftObj.IsLastShift = true;

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                    var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (v.Temp == 1)
                    {
                        string json2 = "";
                        v.ShiftObj = new Shift_Details();
                        //btnDailyReport.Visibility = Visibility.Visible;
                        v.ShiftObj.StoreId = ApplicationData.Store_Id;
                        v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                        v.ShiftObj.IsReportGenerated = true;
                        json2 = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                        var response2 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json2, System.Text.Encoding.UTF8, "application/json")).Result;


                        //string json1 = "";
                        //v.SoldOutObj = new SoldOut_Details();
                        //v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                        //v.SoldOutObj.EmployeeID = 1;

                        //json1 = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                        //var response1 = client.PostAsync("api/CloseShift/CreatePDF", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

                        //Uri source = new Uri("http://63.142.245.165///Pdf/DailyReport.pdf");
                        //string destination = "LastshiftDailyReport.pdf";

                        //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(source);
                        //WebResponse res = await req.GetResponseAsync();
                        //Stream str = res.GetResponseStream();

                        //StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                        //StorageFile destinationFile = await local.CreateFileAsync(destination, CreationCollisionOption.ReplaceExisting);

                        //await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, ReadStream(str));

                        //await Windows.System.Launcher.LaunchFileAsync(destinationFile);
                    }
                }
            }
            else
            {
                btnDailyReport.Visibility = Visibility.Collapsed;
                btnShiftSubmit.Visibility = Visibility.Visible;
                v.ShiftObj.IsLastShift = false;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                var response = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            }
        }

        private void BtCloseDailyReportPopup_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisiblePopup = true;
            v.IsDailyReportPopup = false;
            PopupStartNewShift.IsOpen = false;
            v.IsCloseshitfPopup = true;
            v.TerminalObj = new Terminal_Details();
            v.HamburgerSelectedData = null;
            v.SingleShiftReportDate = null;
            v.Dailytemp = 1;
            v.DailytempHamburger = 1;
        }
        private void BtBoxClose_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsBoxClosePopup = false;
            v.IsHitTestVisiblePopup = true;
        }

        int eventCount = 0;
        private async void txtBarcodeCloseShift_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount++;
                if (eventCount == 2)
                {
                    eventCount = 0;
                    LoaderIsScanBox.IsActive = true;
                    this.IsEnabled = false;
                    if (GetBarcodeFormat != null)
                    {
                        Words = txtBarcodeCloseShift.Text;

                        if (txtBarcodeCloseShift.Text.Length < GetBarcodeFormat.BarCodeLength || txtBarcodeCloseShift.Text.Length > GetBarcodeFormat.BarCodeLength)
                        {

                            var dialog = new MessageDialog("Barcode must be Equal to " + GetBarcodeFormat.BarCodeLength + " Digit For " + ApplicationData.SelectedState + " State.");
                            await dialog.ShowAsync();
                            LoaderIsScanBox.IsActive = false;
                            this.IsEnabled = true;

                        }

                        else
                        {
                            int GameIdRange = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
                            int PacketIDRange = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                            int EndNoRange = GetBarcodeFormat.SequenceIDTo - GetBarcodeFormat.SequenceNoFrom;

                            string GameId = Words.Substring(GetBarcodeFormat.GameIDFrom, GameIdRange);
                            string PacketId = Words.Substring(GetBarcodeFormat.PacketIDFrom, PacketIDRange);
                            string EndNo = Words.Substring(GetBarcodeFormat.SequenceNoFrom, EndNoRange);

                            v.OnActivateHistory();
                            // var s1 = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                            var s1 = v.ActivatehistoryColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                            var v1 = s1.Where(p => p.Game_Id == GameId && p.Packet_No == PacketId).FirstOrDefault();
                            if (v1 == null && Flag == 0)
                            {
                                //v.SelectedBox.IsScanned = false;

                                var dialog = new MessageDialog("This Packet does not match with any Active Packet.");
                                await dialog.ShowAsync();
                                txtBarcodeCloseShift.Text = "";
                                LoaderIsScanBox.IsActive = false;
                                this.IsEnabled = true;

                            }
                            else
                            {

                                v.ObjCloseBox = new Close_Box();
                                v.ObjCloseBox.Store_Id = ApplicationData.Store_Id;
                                v.ObjCloseBox.EmployeeID = ApplicationData.Emp_Id;
                                v.ObjCloseBox.Game_Id = GameId;
                                v.ObjCloseBox.Packet_Id = PacketId;
                                v.ObjCloseBox.Start_No = v1.Start_No;
                                v.ObjCloseBox.End_No = EndNo;
                                v.ObjCloseBox.Box_No = v1.Box_No;
                                v.ObjCloseBox.Ticket_Name = v1.Ticket_Name;
                                v.ObjCloseBox.Price = v1.Price;
                                v.ObjCloseBox.Close_At = EndNo;
                                v.ObjCloseBox.State = ApplicationData.SelectedState;
                                string json = "";
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ObjCloseBox);
                                LoaderIsScanBox.IsActive = true;
                                this.IsEnabled = false;
                                HttpResponseMessage response = client.PostAsync("api/CloseBox/OnClose_Box", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    v.LoadLotteryCollection();
                                    v.LoadActive_BoxCollection();
                                    v.GetActivedBoxCount();
                                    txtBarcodeCloseShift.Text = "";
                                    LoaderIsScanBox.IsActive = false;
                                    this.IsEnabled = true;
                                    //Flag = 1;
                                }
                                else if (response.ReasonPhrase == "Not Found")
                                {
                                    // Flag = 1;
                                    var dialog = new MessageDialog("Please Check Total Tickets.");
                                    await dialog.ShowAsync();

                                    LoaderIsScanBox.IsActive = false;
                                    this.IsEnabled = true;
                                }

                                else
                                {
                                    LoaderIsScanBox.IsActive = false;
                                    this.IsEnabled = true;

                                }
                            }

                        }

                        //dispatcherTimer = new DispatcherTimer();
                        //i = 0;
                        //timesTicked = 0;

                        //dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
                        //dispatcherTimer.Tick += dispatcherTimer_Tick;
                        //dispatcherTimer.Start();

                    }

                }
                LoaderIsScanBox.IsActive = false;
                this.IsEnabled = true;
            }

        }

        private void ChCardDetails_Click(object sender, RoutedEventArgs e)
        {
            if (ChCardDetails.IsChecked == true)
            {
                GDCardDetails.Visibility = Visibility.Visible;
            }
            else
            {
                GDCardDetails.Visibility = Visibility.Collapsed;
            }
        }
        private void BtTerminalCloseShift_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisiblePopup = true;
            v.IsDataTerminalPopup = false;
            v.TerminalObj = new Terminal_Details();
            LoaderIsScanBox.IsActive = false;
            v.IsBtnSaveVisible = Visibility.Visible;
            v.IsVisibleGenerateReport = Visibility.Collapsed;
        }
        private void txtTotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTotal.Text = (Convert.ToInt32(txtIssued_Inventory.Text) + Convert.ToInt32(txtInstock_Inventory.Text) + Convert.ToInt32(txtActive_Inventory.Text)).ToString();
        }

        //private void btnSaveTerminalData_Click(object sender, RoutedEventArgs e)
        //{
        //    int a;
        //    var v = this.DataContext as HomeVM;
        //    a = v.TerminalObj.OnlinePayout;
        //}

        private async void txtActive_Inventory_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtActive_Inventory.Text != null)
                {
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtActive_Inventory.Text = txtActive_Inventory.Text.Substring(0, txtActive_Inventory.Text.Length - 1);
                            txtActive_Inventory.SelectionStart = (txtActive_Inventory.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (txtIssued_Inventory.Text == "" || txtInstock_Inventory.Text == "" || txtActive_Inventory.Text == "" || !regex.IsMatch(last.ToString()))
                    {
                        if (!regex.IsMatch(last.ToString()))
                        {
                            //var dialog = new MessageDialog("Value must be numeric.");
                            //await dialog.ShowAsync();
                            try
                            {
                                txtActive_Inventory.Text = txtActive_Inventory.Text.Substring(0, txtActive_Inventory.Text.Length - 1);
                                txtActive_Inventory.SelectionStart = (txtActive_Inventory.Text.Length + 1) - 1;
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            var dialog = new MessageDialog("Please Fill All Information");
                            await dialog.ShowAsync();
                            try
                            {
                                txtActive_Inventory.Text = txtActive_Inventory.Text.Substring(0, txtActive_Inventory.Text.Length - 1);
                                txtActive_Inventory.SelectionStart = (txtActive_Inventory.Text.Length + 1) - 1;
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    else
                    {
                        txtTotal.Text = (Convert.ToInt32(txtIssued_Inventory.Text) + Convert.ToInt32(txtInstock_Inventory.Text) + Convert.ToInt32(txtActive_Inventory.Text)).ToString();
                    }

                }

            }

        }

        private async void txtOnlineSell_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // char secondlast = s[s.Length - 2];

                if (txtOnlineSell.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtOnlineSell.Text = txtOnlineSell.Text.Substring(0, txtOnlineSell.Text.Length - 1);
                            txtOnlineSell.SelectionStart = (txtOnlineSell.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtOnlinePayout_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtOnlinePayout.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtOnlinePayout.Text = txtOnlinePayout.Text.Substring(0, txtOnlinePayout.Text.Length - 1);
                            txtOnlinePayout.SelectionStart = (txtOnlinePayout.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtScratchPayout_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtScratchPayout.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtScratchPayout.Text = txtScratchPayout.Text.Substring(0, txtScratchPayout.Text.Length - 1);
                            txtScratchPayout.SelectionStart = (txtScratchPayout.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtIssued_Inventory_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtIssued_Inventory.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtIssued_Inventory.Text = txtIssued_Inventory.Text.Substring(0, txtIssued_Inventory.Text.Length - 1);
                            txtIssued_Inventory.SelectionStart = (txtIssued_Inventory.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtInstock_Inventory_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtInstock_Inventory.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtInstock_Inventory.Text = txtInstock_Inventory.Text.Substring(0, txtInstock_Inventory.Text.Length - 1);
                            txtInstock_Inventory.SelectionStart = (txtInstock_Inventory.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtLoan_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtLoan.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtLoan.Text = txtLoan.Text.Substring(0, txtLoan.Text.Length - 1);
                            txtLoan.SelectionStart = (txtLoan.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtCashOnHand_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtCashOnHand.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtCashOnHand.Text = txtCashOnHand.Text.Substring(0, txtCashOnHand.Text.Length - 1);
                            txtCashOnHand.SelectionStart = (txtCashOnHand.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtCredit_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtCredit.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtCredit.Text = txtCredit.Text.Substring(0, txtCredit.Text.Length - 1);
                            txtCredit.SelectionStart = (txtCredit.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtDebit_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtDebit.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtDebit.Text = txtDebit.Text.Substring(0, txtDebit.Text.Length - 1);
                            txtDebit.SelectionStart = (txtDebit.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtTopUp_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtTopUp.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtTopUp.Text = txtTopUp.Text.Substring(0, txtTopUp.Text.Length - 1);
                            txtTopUp.SelectionStart = (txtTopUp.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }
        private async void txtTopUPCancel_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                if (txtTopUPCancel.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Value must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtTopUPCancel.Text = txtTopUPCancel.Text.Substring(0, txtTopUPCancel.Text.Length - 1);
                            txtTopUPCancel.SelectionStart = (txtTopUPCancel.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }
        private async void txtCloseBoxClosePosition_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var v = this.DataContext as HomeVM;
            //TextBox t = sender as TextBox;
            //string s = t.Text.ToString();
            //if (s != string.Empty)
            //{
            //    char last = s[s.Length - 1];
            //    if (txtCloseBoxClosePosition.Text != null)
            //        if (!regex.IsMatch(last.ToString()))
            //        {
            //            IsValid = false;
            //            var dialog = new MessageDialog("Value must be numeric.");
            //            await dialog.ShowAsync();
            //        }
            //}
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // IsValidBarcode = SwitchCase(last);

                // isValidBarcode = true;
                if (txtCloseBoxClosePosition.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Close_At must be Numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtCloseBoxClosePosition.Text = ReturnEndno.Text.Substring(0, txtCloseBoxClosePosition.Text.Length - 1);
                            txtCloseBoxClosePosition.SelectionStart = (txtCloseBoxClosePosition.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }
        private void TBDashboard_Click(object sender, RoutedEventArgs e)
        {
            GDHamburgerShiftReport.Visibility = Visibility.Collapsed;
            GDHambugerDailyReport.Visibility = Visibility.Collapsed;
            GDMainDashboard.Visibility = Visibility.Visible;
            GDUser.Visibility = Visibility.Collapsed;
            GDEmail.Visibility = Visibility.Collapsed;
            GDPassword.Visibility = Visibility.Collapsed;
            GDStoreSettings.Visibility = Visibility.Collapsed;
            //GDSupportSettings.Visibility = Visibility.Collapsed;
            var v = this.DataContext as HomeVM;
            v.IsUserPopup = false;
            v.IsChangePasswordPopup = false;
            v.IsStoreSettingsPopup = false;
            v.IsSupportSettingsPopup = false;

        }
        private void TBShiftReport_Click(object sender, RoutedEventArgs e)
        {
            GDMainDashboard.Visibility = Visibility.Collapsed;
            GDHambugerDailyReport.Visibility = Visibility.Collapsed;
            GDHamburgerShiftReport.Visibility = Visibility.Visible;
            GDEmail.Visibility = Visibility.Collapsed;
            GDUser.Visibility = Visibility.Collapsed;
            GDPassword.Visibility = Visibility.Collapsed;
            GDStoreSettings.Visibility = Visibility.Collapsed;
            //GDSupportSettings.Visibility = Visibility.Collapsed;
            HamburgerFromCalendar.Date = System.DateTime.Now.Date;
            HamburgerToCalendar.Date = System.DateTime.Now.Date;
            var v = this.DataContext as HomeVM;
            v.ShiftReportSelectedDate = null;
            v.IsUserPopup = false;
            v.IsChangePasswordPopup = false;
            v.IsStoreSettingsPopup = false;
            v.IsSupportSettingsPopup = false;
        }
        public void TBDailyReport_Click(object sender, RoutedEventArgs e)
        {
            GDHambugerDailyReport.Visibility = Visibility.Visible;
            GDMainDashboard.Visibility = Visibility.Collapsed;
            GDHamburgerShiftReport.Visibility = Visibility.Collapsed;
            GDEmail.Visibility = Visibility.Collapsed;
            GDUser.Visibility = Visibility.Collapsed;
            GDStoreSettings.Visibility = Visibility.Collapsed;
            //GDSupportSettings.Visibility = Visibility.Collapsed;
            GDPassword.Visibility = Visibility.Collapsed;
            HamburgerDailyReportFromCalendar.Date = System.DateTime.Now.Date;
            HamburgerDailyReportToCalendar.Date = System.DateTime.Now.Date;
            var v = this.DataContext as HomeVM;
            v.IsUserPopup = false;
            v.IsChangePasswordPopup = false;
            v.IsStoreSettingsPopup = false;
            v.IsSupportSettingsPopup = false;
        }

        #endregion

        #region Closing Methods
        private void BtCloseReceive_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsReceiveManuallyPopup = false;
            txtReceiveGameIDAutoSuggest.Text = "";
            txtReceivedPacketId.Text = "";
            tb3.Text = "";
            tb4.Text = "";
            tb5.Text = "";
            tb6.Text = "";
            txtReceiveBarcode.Text = "";
            // (this.DataContext as HomeVM).LoadLotteryCollection();
            (this.DataContext as HomeVM).LoadEmptyBoxes();
            v.IsHitTestVisible = true;

        }
        private void BtCloseActivate_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisible = true;
            v.IsActivatePopup = false;
            v.SelectedData = null;
            txtGameId.Text = "";
            txtPackId.Text = "";
            txtTicketName.Text = "";
            txtPrice.Text = "";
            txtStartNo.Text = "";
            txtEndNo.Text = "";
            txtBarcodeActive.Text = "";
            v.Active_StatusObj.Box_No = null;
            v.ActiveBoxObj.Box_No = null;
            v.IsVisibleActiveBoxNo = Visibility.Collapsed;
            v.IsVisibleActiveComboBox = Visibility.Visible;
            v.FlagAutoGameID = 0;
            v.IsNewStoreChecked = false;
            v.OnReceiveHistory();



        }
        private void BtCloseDeactivate_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisible = true;
            v.ActiveBoxObj.Box_No = null;
            v.SelectedData = null;
            v.FlagAutoGameID = 0;
            txtBarcodeDeactivate.Text = "";
            PopupDeactivate.IsOpen = false;

            (this.DataContext as HomeVM).LoadEmptyBoxes();


            v.ActiveBoxObj = new Activate_Ticket();
        }
        private void BtCloseSoldout_Click(object sender, RoutedEventArgs e)
        {
            PopupSoldout.IsOpen = false;
            var v = this.DataContext as HomeVM;
            if (CloseshiftPopup.IsOpen == true)
            {
                v.IsHitTestVisible = false;
            }
            else { v.IsHitTestVisible = true; }
            v.IsHitTestVisiblePopup = true;
            v.ActiveBoxObj.Box_No = null;
            v.SelectedData = null;
            v.FlagAutoGameID = 0;
            txtBarcodeSoldout.Text = "";
            v.SoldOutObj = new SoldOut_Details();
            v.ActiveBoxObj = new Activate_Ticket();

            (this.DataContext as HomeVM).LoadEmptyBoxes();

        }
        private void BtCloseReturnticket_Click(object sender, RoutedEventArgs e)
        {
            PopupReturnticket.IsOpen = false;
            ReturnEndno.IsReadOnly = false;
            var v = this.DataContext as HomeVM;
            if (CloseshiftPopup.IsOpen == true)
            {
                v.IsHitTestVisible = false;
            }
            else { v.IsHitTestVisible = true; }
            v.IsHitTestVisiblePopup = true;
            v.SelectedData = null;
            v.FlagAutoGameID = 0;
            TxtReturnBarcode.Text = "";
            //v.DeactivateselectedData = null;
            v.ActiveBoxObj.Box_No = null;
            v.Return_Obj = new Return_Details();
            v.ActiveBoxObj = new Activate_Ticket();
            // (this.DataContext as HomeVM).LoadLotteryCollection();
            (this.DataContext as HomeVM).LoadEmptyBoxes();
            if (v.IsReceiveChecked == true)
            {
                v.OnReceiveHistory();
            }
            else if (v.IsActivateChecked == true)
            {
                v.GetActivedBoxCount();
            }
            else
            {
                v.GetDeactivedBoxCount();
            }

        }
        private void BtCloseSettleticket_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisible = true;
            v.ActiveBoxObj.Box_No = null;
            v.FlagAutoGameID = 0;
            v.SelectedData = null;
            TxtSettleBarcode.Text = "";
            v.IsSettlePopup = false;
            v.Settle_Obj = new Settle_Details();
            v.ActiveBoxObj = new Activate_Ticket();

        }

        #endregion        

        #region Popup Layout Methods
        private void PopupReceive_LayoutUpdated(object sender, object e)
        {
            if (GdReceive.ActualWidth == 0 && GdReceive.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupReceive.HorizontalOffset;
            double ActualVerticalOffset = this.PopupReceive.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdReceive.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdReceive.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupReceive.HorizontalOffset = NewHorizontalOffset;
                this.PopupReceive.VerticalOffset = NewVerticalOffset;
            }
        }
        private void PopupDeactivate_LayoutUpdated_1(object sender, object e)
        {
            if (GdDeactivate.ActualWidth == 0 && GdDeactivate.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupDeactivate.HorizontalOffset;
            double ActualVerticalOffset = this.PopupDeactivate.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdDeactivate.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdDeactivate.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupDeactivate.HorizontalOffset = NewHorizontalOffset;
                this.PopupDeactivate.VerticalOffset = NewVerticalOffset;
            }
        }
        private void PopupSoldout_LayoutUpdated(object sender, object e)
        {
            if (GdSoldout.ActualWidth == 0 && GdSoldout.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupSoldout.HorizontalOffset;
            double ActualVerticalOffset = this.PopupSoldout.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdSoldout.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdSoldout.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupSoldout.HorizontalOffset = NewHorizontalOffset;
                this.PopupSoldout.VerticalOffset = NewVerticalOffset;
            }
        }
        private void PopupReturnticket_LayoutUpdated(object sender, object e)
        {
            if (GdReturnticket.ActualWidth == 0 && GdReturnticket.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupReturnticket.HorizontalOffset;
            double ActualVerticalOffset = this.PopupReturnticket.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdReturnticket.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdReturnticket.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupReturnticket.HorizontalOffset = NewHorizontalOffset;
                this.PopupReturnticket.VerticalOffset = NewVerticalOffset;
            }
        }
        private void PopupActivate_LayoutUpdated(object sender, object e)
        {
            if (GdActivate.ActualWidth == 0 && GdActivate.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupActivate.HorizontalOffset;
            double ActualVerticalOffset = this.PopupActivate.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdActivate.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdActivate.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupActivate.HorizontalOffset = NewHorizontalOffset;
                this.PopupActivate.VerticalOffset = NewVerticalOffset;
            }
        }
        private void PopupSettleticket_LayoutUpdated(object sender, object e)
        {
            if (GdSettleticket.ActualWidth == 0 && GdActivate.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupSettleticket.HorizontalOffset;
            double ActualVerticalOffset = this.PopupSettleticket.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdSettleticket.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdSettleticket.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupSettleticket.HorizontalOffset = NewHorizontalOffset;
                this.PopupSettleticket.VerticalOffset = NewVerticalOffset;
            }
        }
        private void PopupEmptybox_LayoutUpdated(object sender, object e)
        {
            if (GdPopupEmptybox.ActualWidth == 0 && GdActivate.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupEmptybox.HorizontalOffset;
            double ActualVerticalOffset = this.PopupEmptybox.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdPopupEmptybox.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdPopupEmptybox.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupEmptybox.HorizontalOffset = NewHorizontalOffset;
                this.PopupEmptybox.VerticalOffset = NewVerticalOffset;
            }
        }
        private void PopupActivateBox_LayoutUpdated(object sender, object e)
        {
            if (GdPopupActivateBox.ActualWidth == 0 && GdActivate.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupActivateBox.HorizontalOffset;
            double ActualVerticalOffset = this.PopupActivateBox.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdPopupActivateBox.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdPopupActivateBox.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupActivateBox.HorizontalOffset = NewHorizontalOffset;
                this.PopupActivateBox.VerticalOffset = NewVerticalOffset;
            }
        }
        #endregion      

        #region Action Button Calls
        public async void btnDeActivate_Click(object sender, RoutedEventArgs e)
        {
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Single_Record);
            var response = client.PostAsync("api/Deactivate_Ticket/Deactivate_Tickets", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
            {
                var dialog = new MessageDialog("Ticket Deactivated Successfully..");
                await dialog.ShowAsync();
                Single_Record = null;
                //  (this.DataContext as HomeVM).LoadLotteryCollection();
                (this.DataContext as HomeVM).LoadEmptyBoxes();
            }
            else
            {
                var dialog = new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                await dialog.ShowAsync();
            }

            PopupDeactivate.IsOpen = true;


        }

        //public async void btnReturn_Click(object sender, RoutedEventArgs e)
        //{
        //    string json = "";
        //    json = Newtonsoft.Json.JsonConvert.SerializeObject(Single_Record);
        //    var response = client.PostAsync("api/Return_Ticket/Return_Tickets", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var dialog = new MessageDialog("Ticket Marked Returned Successfully..");
        //        await dialog.ShowAsync();
        //        Single_Record = null;
        //      //  (this.DataContext as HomeVM).LoadLotteryCollection();
        //        (this.DataContext as HomeVM).LoadEmptyBoxes();
        //    }
        //    else
        //    {
        //        var dialog = new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        //        await dialog.ShowAsync();
        //    }
        //}

        //public async void btnSoldout_Click(object sender, RoutedEventArgs e)
        //{
        //    string json = "";
        //    json = Newtonsoft.Json.JsonConvert.SerializeObject(Single_Record);
        //    var response = client.PostAsync("api/SouldOut_Ticket/SoldOut_Tickets", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var dialog = new MessageDialog("Ticket Marked Sold Out Successfully..");
        //        await dialog.ShowAsync();
        //        Single_Record = null;
        //        //BoxCollection = new ObservableCollection<Activation_Box>();
        //        //LoadBoxCollection();
        //       // (this.DataContext as HomeVM).LoadLotteryCollection();
        //        (this.DataContext as HomeVM).LoadEmptyBoxes();
        //    }
        //    else
        //    {
        //        var dialog = new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        //        await dialog.ShowAsync();
        //    }

        //}

        //public async void btnSettle_Click(object sender, RoutedEventArgs e)
        //{
        //    string json = "";
        //    json = Newtonsoft.Json.JsonConvert.SerializeObject(Single_Record);
        //    var response = client.PostAsync("api/Settle_Ticket/Settle_Tickets", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var dialog = new MessageDialog("Ticket Marked Returned Successfully..");
        //        await dialog.ShowAsync();
        //        Single_Record = null;
        //        //BoxCollection = new ObservableCollection<Activation_Box>();
        //        //LoadBoxCollection();
        //       // (this.DataContext as HomeVM).LoadLotteryCollection();
        //        (this.DataContext as HomeVM).LoadEmptyBoxes();
        //    }
        //    else
        //    {
        //        var dialog = new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        //        await dialog.ShowAsync();
        //    }
        //}

        public void btnEditTicket_Click(object sender, RoutedEventArgs e)
        {
            EditableTicketColl = new ObservableCollection<Activation_Box>();
            EditableTicketColl = (this.DataContext as HomeVM).Active_BoxCollection;
            //string abc = txtGameId_Display.Text;
            //Editable_Record = EditableTicketColl.Where<LotteryInfo>(i => i.Box_No == Convert.ToInt32(content)).SingleOrDefault();
        }

        #endregion

        #region WebAPI Call
        public void InitalizeVariable()
        {
            //client.BaseAddress = new Uri("http://63.142.245.165:1519/");
            client.BaseAddress = new Uri("http://localhost:5133/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        #endregion

        #region Public Properties
        public ObservableCollection<Activation_Box> EditableTicketColl { get; set; }
        public LotteryInfo Editable_Record { get; set; }
        public ObservableCollection<LotteryInfo> TemporaryColl { get; set; }
        public LotteryInfo Single_Record { get; set; }
        public ObservableCollection<LotteryInfo> Selected_Record { get; set; }
        private void PopupHistory_LayoutUpdated(object sender, object e)
        {
            if (GdPopupHistory.ActualWidth == 0 && GdPopupHistory.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.PopupHistory.HorizontalOffset;
            double ActualVerticalOffset = this.PopupHistory.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdPopupHistory.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdPopupHistory.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.PopupHistory.HorizontalOffset = NewHorizontalOffset;
                this.PopupHistory.VerticalOffset = NewVerticalOffset;
            }
        }
        private void btnHistoryPopupCancel_Click(object sender, RoutedEventArgs e)
        {
            PopupHistory.IsOpen = false;
        }
        private void ActivatePopupHistory_LayoutUpdated(object sender, object e)
        {
            if (GdActivatePopupHistory.ActualWidth == 0 && GdActivatePopupHistory.ActualHeight == 0)
            {
                return;
            }

            double ActualHorizontalOffset = this.ActivatePopupHistory.HorizontalOffset;
            double ActualVerticalOffset = this.ActivatePopupHistory.VerticalOffset;

            double NewHorizontalOffset = (Window.Current.Bounds.Width - GdActivatePopupHistory.ActualWidth) / 2;
            double NewVerticalOffset = (Window.Current.Bounds.Height - GdActivatePopupHistory.ActualHeight) / 2;

            if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
            {
                this.ActivatePopupHistory.HorizontalOffset = NewHorizontalOffset;
                this.ActivatePopupHistory.VerticalOffset = NewVerticalOffset;
            }
        }
        private void btnActivateHistoryPopupCancel_Click(object sender, RoutedEventArgs e)
        {
            ActivatePopupHistory.IsOpen = false;
        }
        private async void txtBarcodeCloseShift_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // IsValidBarcode = SwitchCase(last);

                // isValidBarcode = true;
                if (txtBarcodeCloseShift.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Barcode must be Numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtBarcodeCloseShift.Text = txtBarcodeCloseShift.Text.Substring(0, txtBarcodeCloseShift.Text.Length - 1);
                            txtBarcodeCloseShift.SelectionStart = (txtBarcodeCloseShift.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }


        #endregion

        private void btnCancelTerminalData_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisiblePopup = true;
            v.IsDataTerminalPopup = false;
            v.IsVisibleGenerateReport = Visibility.Collapsed;
            v.IsBtnSaveVisible = Visibility.Visible;
            v.TerminalObj = new Terminal_Details();
        }

        private void btnStartNewShiftCacel_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsHitTestVisible = true;
            PopupStartNewShift.IsOpen = false;
            v.Temp = 0;
            v.Dailytemp = 0;

        }

        private async void btnStartNewShift_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as HomeVM;
            vm.Temp = 0;
            if (vm.Active_BoxCollection.Count > 0)
            {
                foreach (var i in vm.Active_BoxCollection)
                {
                    if (i.Status == "Active")
                    {
                        var dialog = new MessageDialog("Cannot Create New Shift Until All Active Boxes have been Scanned.");
                        await dialog.ShowAsync();
                        PopupStartNewShift.IsOpen = false;
                        vm.IsHitTestVisible = true;
                        vm.Temp = 1;
                    }
                }
                if (vm.Temp == 0)
                {
                    Frame.Navigate(typeof(LoginWindow));
                    //vm.Flag = 1;
                    DateTime CurrentDate = System.DateTime.Today;
                    string json = "";
                    vm.ObjCloseBox = new Close_Box();
                    vm.ObjCloseBox.Store_Id = ApplicationData.Store_Id;
                    vm.ObjCloseBox.EmployeeID = ApplicationData.Emp_Id;
                    vm.ObjCloseBox.Created_Date = CurrentDate;
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(vm.ObjCloseBox);
                    var response = client.PostAsync("api/CloseBox/NewGetChangetoActive", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    if (response.ReasonPhrase == "Not Found")
                    {
                        var dialog = new MessageDialog("This PacketId Already Activated");
                        await dialog.ShowAsync();
                    }
                    var response1 = client.PostAsync("api/CloseBox/NewGetChangeSoldOutToEmpty", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                }
            }
            else
            {
                Frame.Navigate(typeof(LoginWindow));
                //vm.Flag = 1;
                DateTime CurrentDate = System.DateTime.Today;
                string json = "";
                vm.ObjCloseBox = new Close_Box();
                vm.ObjCloseBox.Store_Id = ApplicationData.Store_Id;
                vm.ObjCloseBox.EmployeeID = ApplicationData.Emp_Id;
                vm.ObjCloseBox.Created_Date = CurrentDate;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(vm.ObjCloseBox);

                var response = client.PostAsync("api/CloseBox/NewGetChangetoActive", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.ReasonPhrase == "Not Found")
                {
                    var dialog = new MessageDialog("This PacketId Already Activated");
                    await dialog.ShowAsync();
                }
                var response1 = client.PostAsync("api/CloseBox/NewGetChangeSoldOutToEmpty", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            }


        }

        private async void txtStoppedAt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // IsValidBarcode = SwitchCase(last);

                // isValidBarcode = true;
                if (txtStoppedAt.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Stopped_At must be Numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtStoppedAt.Text = txtStoppedAt.Text.Substring(0, txtStoppedAt.Text.Length - 1);
                            txtStoppedAt.SelectionStart = (txtStoppedAt.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void ReturnEndno_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // IsValidBarcode = SwitchCase(last);

                // isValidBarcode = true;
                if (ReturnEndno.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Stopped_At must be Numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            ReturnEndno.Text = ReturnEndno.Text.Substring(0, ReturnEndno.Text.Length - 1);
                            ReturnEndno.SelectionStart = (ReturnEndno.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
            }
        }

        private async void txtReceivedPacketId_TextChanged(object sender, TextChangedEventArgs e)
        {
            int PacketIdRange = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;

            if (txtReceivedPacketId.Text.Length > PacketIdRange)
            {
                var dialog = new MessageDialog("PacketId Must be " + PacketIdRange + " Digit.");
                await dialog.ShowAsync();
                try
                {
                    txtReceivedPacketId.Text = txtReceivedPacketId.Text.Substring(0, txtReceivedPacketId.Text.Length - 1);
                    txtReceivedPacketId.SelectionStart = (txtReceivedPacketId.Text.Length + 1) - 1;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async void MasterInventoryGameId_TextChanged(object sender, TextChangedEventArgs e)
        {
            int GameIdRange = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;

            if (MasterInventoryGameId.Text.Length > GameIdRange)
            {
                var dialog = new MessageDialog("GameId Must be " + GameIdRange + " Digit.");
                await dialog.ShowAsync();
                try
                {
                    MasterInventoryGameId.Text = MasterInventoryGameId.Text.Substring(0, MasterInventoryGameId.Text.Length - 1);
                    MasterInventoryGameId.SelectionStart = (MasterInventoryGameId.Text.Length + 1) - 1;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async void txtMasterStartNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int StartNo = Convert.ToInt32(txtMasterStartNo.Text);
                if (StartNo != 0)
                {
                    var dialog = new MessageDialog("StartNo. Must be 000.");
                    await dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void HamburgerDailyReportFromCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            HamburgerDailyReportFromCalendar.Date = System.DateTime.Now;
        }

        private void HamburgerDailyReportFromCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (HamburgerDailyReportFromCalendar.Date != null)
            {
                TerminalFromDate = (HamburgerDailyReportFromCalendar.Date.Value.DateTime);
                TerminalFromDate = TerminalFromDate.AddDays(-1);
            }
            else
            {
                TerminalFromDate = System.DateTime.Now;
                TerminalFromDate = TerminalFromDate.AddDays(-1);
            }
        }

        private void HamburgerDailyReportToCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            HamburgerDailyReportToCalendar.Date = System.DateTime.Now;
        }

        private void HamburgerDailyReportToCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (HamburgerDailyReportToCalendar.Date != null)
            {
                TerminalToDate = (HamburgerDailyReportToCalendar.Date.Value.DateTime);

            }
            else
            {
                TerminalToDate = System.DateTime.Now;

            }
        }

        private async void btnShow1_Click(object sender, RoutedEventArgs e)
        {

            var v = this.DataContext as HomeVM;

            Double temp;
            temp = (TerminalToDate - TerminalFromDate).TotalDays;
            if (HamburgerDailyReportFromCalendar.Date == null || HamburgerDailyReportToCalendar.Date == null)
            {
                if (HamburgerDailyReportFromCalendar.Date == null)
                {
                    var dialog = new MessageDialog("Please Select From Date");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Please Select To Date");
                    await dialog.ShowAsync();
                }
            }

            //else if (TerminalFromDate >= TerminalToDate)
            //{
            //    var dialog = new MessageDialog("To Date must be greater than or same as From date");
            //    await dialog.ShowAsync();
            //    HamburgerDailyReportFromCalendar.Date = System.DateTime.Today;
            //    HamburgerDailyReportToCalendar.Date = System.DateTime.Today;
            //    v.OnTBDailyReport();
            //}
            else if (TerminalToDate >= System.DateTime.Now || TerminalFromDate >= System.DateTime.Now)
            {
                if (TerminalToDate >= System.DateTime.Now)
                {
                    var dialog = new MessageDialog("To Date must be less than Current Date");
                    await dialog.ShowAsync();
                    HamburgerDailyReportFromCalendar.Date = System.DateTime.Today;
                    HamburgerDailyReportToCalendar.Date = System.DateTime.Today;
                    v.OnTBDailyReport();
                }
                else
                {
                    var dialog = new MessageDialog("From Date must be less than Current Date");
                    await dialog.ShowAsync();
                }

            }
            else if (temp > 7)
            {
                var dialog = new MessageDialog("Date range cannot exceed 7 days");
                await dialog.ShowAsync();
                HamburgerDailyReportFromCalendar.Date = System.DateTime.Today;
                HamburgerDailyReportToCalendar.Date = System.DateTime.Today;
                v.OnTBDailyReport();
            }
            else
            {
                v.TerminalDailyReport = new ObservableCollection<Terminal_Details>();
                v.TerminalObj = new Terminal_Details();
                v.MainDailyReportColl = new ObservableCollection<Terminal_Details>();
                v.DailyReport = new ObservableCollection<Terminal_Details>();
                v.ShiftObj = new Shift_Details();
                ObservableCollection<DateTime> tempdate = new ObservableCollection<DateTime>();
                ObservableCollection<Shift_Details> tempshift = new ObservableCollection<Shift_Details>();
                v.TotScratchsell = 0;
                v.TotScratchPayout = 0;
                v.TotOnlineSells = 0;
                v.TotOnlinePayout = 0;
                v.TotTrackedAmount = 0;
                v.TotCashOnHand = 0;
                v.TerminalObj.Store_Id = ApplicationData.Store_Id;
                v.TerminalObj.ShiftID = 1;
                v.TerminalObj.HamburgerFromDateOk = TerminalFromDate;
                v.TerminalObj.HamburgerToDateOk = TerminalToDate;

                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(v.TerminalObj);
                var response = client.PostAsync("api/CloseShift/NewGetTerminalDataRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync().Result;
                    v.TerminalDailyReport = JsonConvert.DeserializeObject<ObservableCollection<Terminal_Details>>(res);

                    v.ShiftObj.StoreId = ApplicationData.Store_Id;
                    v.ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.ShiftObj);
                    var get = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    if (get.IsSuccessStatusCode)
                    {
                        var w = get.Content.ReadAsStringAsync().Result;
                        tempshift = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(w);
                    }
                    var g = tempshift.LastOrDefault();

                    if (v.TerminalDailyReport != null)
                    {

                        foreach (var i in v.TerminalDailyReport)
                        {
                            t1 = i.Date;
                            if (t1 > TerminalFromDate && t1 <= TerminalToDate)
                            {
                                if (tempshift != null)
                                {
                                    var j = tempshift.Where(y => y.Date == i.Date && y.IsLastShift == true).ToList();

                                    if (tempdate.Count > 0)
                                    {
                                        var r = tempdate.Where(x => x.Date == i.Date).ToList();

                                        if (r.Count == 0)
                                            if (j.Count == 1)
                                                tempdate.Add(i.Date);
                                    }
                                    else
                                    {
                                        if (j.Count == 1)
                                            tempdate.Add(i.Date);
                                    }
                                }

                            }

                        }

                    }
                    if (tempdate.Count == 0)
                    {
                        v.IsDailyRecordNotFound = Visibility.Visible;
                        v.IsDailyRecordFound = Visibility.Collapsed;
                    }
                    else
                    {
                        v.IsDailyRecordNotFound = Visibility.Collapsed;
                        v.IsDailyRecordFound = Visibility.Visible;
                    }

                    foreach (var j in tempdate)
                    {
                        v.TotDateScratchsells = 0;
                        v.TotDateOnlineSells = 0;
                        v.TotDateScratchPayout = 0;
                        v.TotDateOnlinePayout = 0;
                        v.TotDateTrackedAmount = 0;
                        v.TotDateCashOnHand = 0;
                        var w = v.TerminalDailyReport.Where(x => x.Date == j.Date).ToList();
                        foreach (var i in w)
                        {
                            int a1 = Convert.ToInt32(i.ScratchSells) + Convert.ToInt32(i.OnlineSells) - Convert.ToInt32(i.OnlinePayout) - Convert.ToInt32(i.ScratchPayout);
                            t1 = i.Date;
                            v.TotDateScratchsells = v.TotDateScratchsells + Convert.ToInt32(i.ScratchSells);
                            v.TotDateOnlineSells = v.TotDateOnlineSells + Convert.ToInt32(i.OnlineSells);
                            v.TotDateScratchPayout = v.TotDateScratchPayout + Convert.ToInt32(i.ScratchPayout);
                            v.TotDateOnlinePayout = v.TotDateOnlinePayout + Convert.ToInt32(i.OnlinePayout);
                            v.TotDateTrackedAmount = v.TotDateTrackedAmount + a1;
                            v.TotDateCashOnHand = v.TotDateCashOnHand + Convert.ToInt32(i.CashOnHand);
                        }

                        v.MainDailyReportColl.Add(new Terminal_Details
                        {
                            Date = t1,
                            Day = t1.DayOfWeek,
                            ScratchSells = v.TotDateScratchsells.ToString(),
                            ScratchPayout = v.TotDateScratchPayout.ToString(),
                            OnlineSells = v.TotDateOnlineSells.ToString(),
                            OnlinePayout = v.TotDateOnlinePayout.ToString(),
                            TrackedAmount = v.TotDateTrackedAmount.ToString(),
                            CashOnHand = v.TotDateCashOnHand.ToString()
                        });

                        v.TotScratchsell = v.TotScratchsell + v.TotDateScratchsells;
                        v.TotScratchPayout = v.TotScratchPayout + v.TotDateScratchPayout;
                        v.TotOnlineSells = v.TotOnlineSells + v.TotDateOnlineSells;
                        v.TotOnlinePayout = v.TotOnlinePayout + v.TotDateOnlinePayout;
                        v.TotTrackedAmount = v.TotTrackedAmount + v.TotDateTrackedAmount;
                        v.TotCashOnHand = v.TotCashOnHand + v.TotDateCashOnHand;


                    }

                }



            }
        }

        private void GetShiftDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (v.ShiftReportSelectedData != null)
            {
                v.IsReportPopup = true;
                v.getHamburgerShiftReport();
            }
        }

        private void HamburgerFromCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (HamburgerFromCalendar.Date != null)
            {
                ShiftHamburgerFromDate = (HamburgerFromCalendar.Date.Value.DateTime);
                ShiftHamburgerFromDate = ShiftHamburgerFromDate.AddDays(-1);
            }
            else
            {
                ShiftHamburgerFromDate = System.DateTime.Now;
                ShiftHamburgerFromDate = ShiftHamburgerFromDate.AddDays(-1);
            }
        }

        private void HamburgerFromCalendar_Loaded(object sender, RoutedEventArgs e)
        {

            HamburgerFromCalendar.Date = System.DateTime.Now;
        }

        private void HamburgerToCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (HamburgerToCalendar.Date != null)
            {
                ShiftHamburgerToDate = (HamburgerToCalendar.Date.Value.DateTime);

            }
            else
            {
                ShiftHamburgerToDate = System.DateTime.Now;

            }

        }

        private void HamburgerToCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            HamburgerToCalendar.Date = System.DateTime.Now;
        }

        private async void btnShow2_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as HomeVM;

            if (HamburgerFromCalendar.Date == null || HamburgerToCalendar.Date == null)
            {
                if (HamburgerFromCalendar.Date == null)
                {
                    var dialog = new MessageDialog("Please Select From Date");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Please Select To Date");
                    await dialog.ShowAsync();
                }
            }

            //else if (ShiftHamburgerFromDate >= ShiftHamburgerToDate)
            // {
            //     var dialog = new MessageDialog("To Date must be greater than or same as From Date ");
            //     await dialog.ShowAsync();
            //     HamburgerFromCalendar.Date = System.DateTime.Today;
            //     HamburgerToCalendar.Date = System.DateTime.Today;
            //     vm.OnTBShiftReport();
            // }
            else if (ShiftHamburgerToDate >= System.DateTime.Now || ShiftHamburgerFromDate >= System.DateTime.Now)
            {
                if (ShiftHamburgerToDate >= System.DateTime.Now)
                {
                    var dialog = new MessageDialog("To Date must be less than Current Date");
                    await dialog.ShowAsync();
                    HamburgerFromCalendar.Date = System.DateTime.Today;
                    HamburgerToCalendar.Date = System.DateTime.Today;
                    vm.OnTBShiftReport();
                }
                else
                {
                    var dialog = new MessageDialog("From Date must be less than Current Date");
                    await dialog.ShowAsync();
                }

            }
            else if (HamburgerFromCalendar.Date.Value.Date <= System.DateTime.Now.Date.AddDays(-7))
            {
                var dialog = new MessageDialog("Date range cannot exceed 7 days");
                await dialog.ShowAsync();
                HamburgerFromCalendar.Date = System.DateTime.Today;
                HamburgerToCalendar.Date = System.DateTime.Today;
                vm.OnTBShiftReport();
            }
            else
            {
                vm.IsReportPopup = false;
                vm.ShiftObj = new Shift_Details();
                vm.ShiftReport = new ObservableCollection<Shift_Details>();
                vm.MainShiftReportColl = new ObservableCollection<Main_Shift_Collection>();
                vm.ShiftObj.StoreId = ApplicationData.Store_Id;
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(vm.ShiftObj);
                var response = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {

                    var w = response.Content.ReadAsStringAsync().Result;
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(w);
                    vm.Temp1Coll = list;

                    ObservableCollection<DateTime> tempdate = new ObservableCollection<DateTime>();

                    // DateTime temp = System.DateTime.Now.AddDays(-7);
                    foreach (var i in list)
                    {
                        if (i.Date > ShiftHamburgerFromDate && i.Date <= ShiftHamburgerToDate)
                        {
                            if (tempdate.Count > 0)
                            {
                                var v = tempdate.Where(x => x.Date == i.Date).ToList();

                                if (v.Count == 0)
                                    tempdate.Add(i.Date);
                            }
                            else
                                tempdate.Add(i.Date);
                        }
                    }

                    if (tempdate.Count == 0)
                    {
                        vm.IsShiftRecordNotFound = Visibility.Visible;
                        vm.IsShiftRecordFound = Visibility.Collapsed;
                    }
                    else
                    {
                        vm.IsShiftRecordFound = Visibility.Visible;
                        vm.IsShiftRecordNotFound = Visibility.Collapsed;
                    }

                    foreach (var i in tempdate)
                    {

                        vm.MainShiftReportColl.Add(new Main_Shift_Collection
                        {
                            Date = i.Date,
                            ShiftReport = vm.GetShiftDetails(i.Date),
                            GetDate = i.Date.ToString("MMM dd, yyyy"),

                        });

                    }

                }
            }

        }

        private void GetDailyHamburgerDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (v.HamburgerSelectedData != null)
            {
                v.IsDailyReportPopup = true;
                v.GetDailyReport();
            }
        }

        private async void btnDailyReportExportAll_Click(object sender, RoutedEventArgs e)
        {
            //PdfSrollviewer.Visibility = Visibility.Visible;
            var v = this.DataContext as HomeVM;
            if (v.HamburgerSelectedData != null)
            {
                try
                {
                    string json = "";
                    v.SoldOutObj = new SoldOut_Details();
                    v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                    v.SoldOutObj.Created_Date = v.HamburgerSelectedData.Date;

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                    var response = client.PostAsync("api/CloseShift/CreatePDF", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    //var res1 = client.GetStreamAsync("api/CloseShift/CreatePDF").Result;

                    //var memStream = new MemoryStream();
                    //await res.CopyToAsync(memStream);
                    //memStream.Position = 0;
                    //PdfDocument pdfdoc = await PdfDocument.LoadFromStreamAsync(memStream.AsRandomAccessStream());
                    //Load(pdfdoc);

                    Uri source = new Uri("http://63.142.245.165///Pdf/DailyReport.pdf");
                    string destination = "DailyReport.pdf";

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(source);
                    WebResponse res = await req.GetResponseAsync();
                    Stream str = res.GetResponseStream();

                    StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                    StorageFile destinationFile = await local.CreateFileAsync(destination, CreationCollisionOption.ReplaceExisting);
                    //if (destinationFile != null)
                    //{
                    //    Windows.Storage.Streams.IRandomAccessStream fileStream = await destinationFile.OpenAsync(FileAccessMode.ReadWrite);
                    //    Stream st = fileStream.AsStreamForWrite();
                    //    st.Write((str as MemoryStream).ToArray(), 0, (int)str.Length);
                    //    st.Flush();
                    //    st.Dispose();
                    //    fileStream.Dispose();
                    //}

                    await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, ReadStream(str));

                    await Windows.System.Launcher.LaunchFileAsync(destinationFile);

                    v.IsDailyReportPopup = false;
                    v.HamburgerSelectedData = null;
                    v.SingleShiftReportDate = null;


                }
                catch (Exception ex)
                {
                    var dialog = new MessageDialog("Please close previous Daily report pdf");
                    await dialog.ShowAsync();
                }
            }
            else
            {
                var dialog = new MessageDialog("Please select Record ");
                await dialog.ShowAsync();
            }
        }
        private byte[] ReadStream(Stream str)
        {
            byte[] buf = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = str.Read(buf, 0, buf.Length)) > 0)
                {
                    ms.Write(buf, 0, read);
                }
                return ms.ToArray();
            }
        }


        private async void btnExportAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var v = this.DataContext as HomeVM;
                if (v.MainShiftReportColl != null && v.SingleShiftReportDate == null && v.ShiftReportSelectedDate == null)
                {
                    var dialog = new MessageDialog("Please select Record");
                    await dialog.ShowAsync();
                }
                else
                {
                    string json = "";
                    v.SoldOutObj = new SoldOut_Details();
                    v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                    if (v.ShiftReportSelectedData != null)

                    {
                        v.SoldOutObj.EmployeeID = v.ShiftReportSelectedData.EmployeeId;
                        v.SoldOutObj.ShiftID = Convert.ToInt32(v.ShiftReportSelectedData.ShiftId);
                        v.SoldOutObj.Created_Date = Convert.ToDateTime(v.ShiftReportSelectedData.Date);
                    }
                    else
                    {
                        v.SoldOutObj.Created_Date = Convert.ToDateTime(v.ShiftReportSelectedDate.Date);
                    }
                    if (v.IsReportPopup == true)
                    {
                        v.SoldOutObj.ShiftReportGenerate = true;
                    }
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                    var response = client.PostAsync("api/CloseShift/ShiftReportCreatePDF", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    Uri source = new Uri("http://63.142.245.165///Pdf/ShiftReport.pdf");
                    string destination = "ShiftReport.pdf";

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(source);
                    WebResponse res = await req.GetResponseAsync();
                    Stream str = res.GetResponseStream();

                    StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                    StorageFile destinationFile = await local.CreateFileAsync(destination, CreationCollisionOption.ReplaceExisting);

                    await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, ReadStream(str));

                    await Windows.System.Launcher.LaunchFileAsync(destinationFile);

                    v.SingleShiftReportDate = null;
                    v.IsReportPopup = false;
                    v.ShiftReportSelectedData = null;
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog("Please close previous Shift report pdf");
                await dialog.ShowAsync();
            }


        }

        private async void btnDailyReportSend_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (v.HamburgerSelectedData != null)
            {
                try
                {
                    string json = "";
                    v.SoldOutObj = new SoldOut_Details();
                    v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                    v.SoldOutObj.Created_Date = v.HamburgerSelectedData.Date;

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                    var response = client.PostAsync("api/Sendmail/NewDailyReport_Mail", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var dialog = new MessageDialog("Daily Shift Report Send");
                        await dialog.ShowAsync();
                    }
                    if (response.ReasonPhrase == "Not Found")
                    {
                        var dialog = new MessageDialog("Report can't send,Please notification on.");
                        await dialog.ShowAsync();
                    }
                    v.IsDailyReportPopup = false;
                    v.HamburgerSelectedData = null;
                    v.SingleShiftReportDate = null;


                }
                catch (Exception ex)
                {
                    var dialog = new MessageDialog("Please close previous Daily report pdf");
                    await dialog.ShowAsync();
                }
            }
            else
            {
                var dialog = new MessageDialog("Please select Record ");
                await dialog.ShowAsync();
            }

        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var v = this.DataContext as HomeVM;

                if (v.MainShiftReportColl != null && v.SingleShiftReportDate == null && v.ShiftReportSelectedDate == null)
                {
                    var dialog = new MessageDialog("Please select Record");
                    await dialog.ShowAsync();
                }
                else
                {
                    string json = "";
                    v.SoldOutObj = new SoldOut_Details();
                    v.SoldOutObj.Store_Id = ApplicationData.Store_Id;
                    if (v.ShiftReportSelectedData != null)

                    {
                        v.SoldOutObj.EmployeeID = v.ShiftReportSelectedData.EmployeeId;
                        v.SoldOutObj.ShiftID = Convert.ToInt32(v.ShiftReportSelectedData.ShiftId);
                        v.SoldOutObj.Created_Date = Convert.ToDateTime(v.ShiftReportSelectedData.Date);
                    }
                    else
                    {
                        v.SoldOutObj.Created_Date = Convert.ToDateTime(v.ShiftReportSelectedDate.Date);
                    }
                    if (v.IsReportPopup == true)
                    {
                        v.SoldOutObj.ShiftReportGenerate = true;
                    }
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(v.SoldOutObj);
                    var response = client.PostAsync("api/Sendmail/Send_Mail", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var dialog = new MessageDialog("Shift Report Send.");
                        await dialog.ShowAsync();
                    }
                    if (response.ReasonPhrase == "Not Found")
                    {
                        var dialog = new MessageDialog("Report can't send,Please notification on.");
                        await dialog.ShowAsync();
                    }
                    v.SingleShiftReportDate = null;
                    v.IsReportPopup = false;
                    v.ShiftReportSelectedData = null;
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog("Please close previous Shift report pdf");
                await dialog.ShowAsync();
            }


            //string json = "";
            //var v = this.DataContext as HomeVM;
            //Login LoginObj = new Login();
            //LoginObj.StoreId = ApplicationData.Store_Id;
            //json = Newtonsoft.Json.JsonConvert.SerializeObject(LoginObj);
            //var responce = client.PostAsync("api/Sendmail/Send_Mail", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            //if (responce.IsSuccessStatusCode)
            //{
            //    var dialog = new MessageDialog("Mail Send");
            //    await dialog.ShowAsync();
            //}
            //EmailMessage emailMessage = new EmailMessage();
            //emailMessage.To.Add(new EmailRecipient("mainkarharshada@gmail.com"));
            //emailMessage.Subject = "Daily report pdf mail";
            //string messageBody = "Daily report";
            //emailMessage.Body = messageBody;
            //StorageFolder MyFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //StorageFile attachmentFile = await MyFolder.GetFileAsync("DailyReport.pdf"); //I understand that the attached file variable needs to go in these brackets.
            //if (attachmentFile != null)
            //{
            //    var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);
            //    var attachment = new Windows.ApplicationModel.Email.EmailAttachment(attachmentFile.Name, stream);
            //    emailMessage.Attachments.Add(attachment);
            //}
            //else
            //{
            //    var dialog = new MessageDialog("No Attachment Found");
            //    await dialog.ShowAsync();
            //}

            //await EmailManager.ShowComposeNewEmailAsync(emailMessage);

            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //mail.From = new MailAddress("chaudharivikcy56@gmail.com");
            //mail.To.Add("chaudharinilesh56@gmail.com");
            //mail.Subject = "Test Mail - 1";
            //mail.Body = "mail with attachment";

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("c:/Users/logique/Desktop/bugRaj.txt");
            //mail.Attachments.Add(attachment);

            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("chaudharivicky56@gmail.com", "sohamVkey10");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);
        }

        private void TBUsers_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            GDMainDashboard.Visibility = Visibility.Collapsed;
            GDHambugerDailyReport.Visibility = Visibility.Collapsed;
            GDHamburgerShiftReport.Visibility = Visibility.Collapsed;
            GDUser.Visibility = Visibility.Visible;
            GDEmail.Visibility = Visibility.Collapsed;
            GDPassword.Visibility = Visibility.Collapsed;
            GDStoreSettings.Visibility = Visibility.Collapsed;
            //GDSupportSettings.Visibility = Visibility.Collapsed;
            ////v.IsUserPopup = true;
            v.IsChangePasswordPopup = false;
            //v.IsStoreSettingsPopup = false;
            //v.IsSupportSettingsPopup = false;
        }
        private void BtCloseAddUser_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsAddUserPopup = false;
            v.IsHitTestVisible = true;
            v.OnUsersMgmt();
        }

        //private void btAddUser_Click(object sender, RoutedEventArgs e)
        //{
        //    var v = this.DataContext as HomeVM;
        //    v.IsAddUserPopup = true;
        //    v.IsHitTestVisible = false;
        //}

        private async void BtDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationData.Manager == false)
            {
                var dialog = new MessageDialog("You are not allowed to access the user creation");
                await dialog.ShowAsync();
            }
            else
            {
                var a = ((sender as Button).DataContext as Login).Name;
                var b = ((sender as Button).DataContext as Login).EmailId;
                var c = ((sender as Button).DataContext as Login).Contactno;


                string json = "";
                var v = this.DataContext as HomeVM;
                Login LoginObj = new Login();


                LoginObj.StoreId = ApplicationData.Store_Id;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(LoginObj);
                var responce = client.PostAsync("api/Login/NewGetEmpRecords", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (responce.IsSuccessStatusCode)
                {
                    var res = responce.Content.ReadAsStringAsync().Result;
                    var coll = JsonConvert.DeserializeObject<ObservableCollection<Login>>(res);

                    var i = coll.Where(x => x.Name == a && x.EmailId == b && x.Contactno == c).ToList().FirstOrDefault();

                    if (i.EmployeeId == ApplicationData.Emp_Id)
                    {
                        var dialog1 = new MessageDialog("You are not allowed to delete Logged in user");
                        await dialog1.ShowAsync();
                    }
                    else
                    {
                        v.Username = i.Username;

                        var dialog = new MessageDialog("Do you really want to Remove this user ? ");
                        dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                        dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                        var result = await dialog.ShowAsync();

                        if ((int)result.Id == 0)
                        {
                            string json1 = "";
                            v.Emp_Details_Obj = new Employee_Details();

                            v.Emp_Details_Obj.StoreId = ApplicationData.Store_Id;
                            v.Emp_Details_Obj.Username = v.Username;

                            json1 = Newtonsoft.Json.JsonConvert.SerializeObject(v.Emp_Details_Obj);
                            var response = client.PostAsync("api/Login/Employee_Registration", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var dialog1 = new MessageDialog("User Deleted Successfully");
                                await dialog1.ShowAsync();
                                v.OnUsersMgmt();
                            }
                        }
                    }

                }
            }

        }

        private void BtCloseEmailSettings_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsEmailSettingsPopup = false;
            v.IsHitTestVisible = true;
        }
        private void BtEditEmailPopup_Click(object sender, RoutedEventArgs e)
        {
            //EditEmailGrid.Visibility = Visibility.Collapsed;
            //BtEditEmailPopup.Visibility = Visibility.Collapsed;
            //BtEditEmail.IsEnabled = true;
        }
        private void BtDeleteEmailPopup_Click(object sender, RoutedEventArgs e)
        {
            //DeleteEmailGrid.Visibility = Visibility.Collapsed;
            //BtDeleteEmailPopup.Visibility = Visibility.Collapsed;
            //BtDeleteEmail.IsEnabled = true;
        }
        private async void BtAddEmail_Click(object sender, RoutedEventArgs e)
        {


            var v = this.DataContext as HomeVM;

            HttpResponseMessage get = client.GetAsync("api/StoreSetting/GetNewStoreHistory").Result;
            var i = get.Content.ReadAsStringAsync().Result;
            var coll = JsonConvert.DeserializeObject<ObservableCollection<Store_Info>>(i);

            var j = coll.Where(x => x.StoreID == ApplicationData.Store_Id).ToList().FirstOrDefault();


            if (j.EmailId1 == null || j.EmailId2 == null || j.EmailId3 == null)
            {
                v.IsAddEmailPopup = true;
                v.AddEmailId = "";
                v.IsHitTestVisible = false;
            }
            else
            {
                var dialog = new MessageDialog("Sorry! You are not allowed to add more than 3 Email Id");
                await dialog.ShowAsync();
            }


        }
        private void BtEditEmail_Click(object sender, RoutedEventArgs e)
        {
            var a = ((sender as Button).DataContext as Store_Info).EmailId1;
            var b = ((sender as Button).DataContext as Store_Info).Index;
            var v = this.DataContext as HomeVM;

            v.IsEditEmailPopup = true;
            v.IsHitTestVisible = false;
            v.AddEmailId = a;
            v.Index = b;
        }
        private async void BtDeleteEmail_Click(object sender, RoutedEventArgs e)
        {
            var a = ((sender as Button).DataContext as Store_Info).EmailId1;
            var b = ((sender as Button).DataContext as Store_Info).Index;
            var v = this.DataContext as HomeVM;

            var dialog1 = new MessageDialog("Do you really want to delete this Email Id ?");
            dialog1.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog1.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var i = await dialog1.ShowAsync();

            if ((int)i.Id == 0)
            {
                string json = "";
                Store_Info LoginObj = new Store_Info();
                LoginObj.StoreID = ApplicationData.Store_Id;
                LoginObj.EmployeeId = ApplicationData.Emp_Id;
                LoginObj.EmailId1 = v.AddEmailId;
                LoginObj.Index = b;
                LoginObj.StoreName = ApplicationData.StoreName;

                json = Newtonsoft.Json.JsonConvert.SerializeObject(LoginObj);

                var response = client.PostAsync("api/Login/ChangePassword", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Email Id deleted successfully");
                    await dialog.ShowAsync();
                    v.AddEmailId = null;
                    v.Index = 0;
                    v.IsHitTestVisible = true;
                    v.OnNotification();
                }
            }

        }
        private void btEmailSettings_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsEmailSettingsPopup = true;
            v.IsHitTestVisible = false;

        }

        private void TBAccount_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            GDEmail.Visibility = Visibility.Collapsed;
            GDMainDashboard.Visibility = Visibility.Collapsed;
            GDHamburgerShiftReport.Visibility = Visibility.Collapsed;
            GDHambugerDailyReport.Visibility = Visibility.Collapsed;
            GDUser.Visibility = Visibility.Collapsed;
            GDPassword.Visibility = Visibility.Visible;
            GDStoreSettings.Visibility = Visibility.Collapsed;
            v.IsChangePasswordPopup = true;
        }

        private void TBStoreSettings_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            GDEmail.Visibility = Visibility.Collapsed;
            GDMainDashboard.Visibility = Visibility.Collapsed;
            GDHamburgerShiftReport.Visibility = Visibility.Collapsed;
            GDHambugerDailyReport.Visibility = Visibility.Collapsed;
            GDUser.Visibility = Visibility.Collapsed;
            GDPassword.Visibility = Visibility.Collapsed;
            GDStoreSettings.Visibility = Visibility.Visible;
            v.IsChangePasswordPopup = false;
        }

        private void TBSupport_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            GDEmail.Visibility = Visibility.Collapsed;
            GDMainDashboard.Visibility = Visibility.Collapsed;
            GDHamburgerShiftReport.Visibility = Visibility.Collapsed;
            GDHambugerDailyReport.Visibility = Visibility.Collapsed;
            GDUser.Visibility = Visibility.Collapsed;
            GDPassword.Visibility = Visibility.Visible;
            GDStoreSettings.Visibility = Visibility.Collapsed;
            //v.IsNewStoreSetupScanExistingBoxesInOrder = true;
        }

        private void BtNextuser_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as HomeVM;
            string temp = "";
            string temp1 = "";
            int t = 0;
            Objlogin = new Login();
            vm.UserColl = new ObservableCollection<Login>();
            string json = "";
            Objlogin.StoreId = ApplicationData.Store_Id;
            // Objlogin.Index = UserIndex;
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Objlogin);
            int count = 0;
            var responce = client.PostAsync("api/Login/NewGetEmpRecords", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (responce.IsSuccessStatusCode)
            {
                var v = responce.Content.ReadAsStringAsync().Result;
                var coll = JsonConvert.DeserializeObject<ObservableCollection<Login>>(v);
                //count = UserIndex + 1;
                if (coll != null)
                {
                    while (count < 5)
                    {
                        if (vm.UserIndex == coll.Count)
                        {
                            break;
                        }

                        //t = vm.UserIndex - 1; 

                        var i = coll.ElementAt(vm.UserIndex);

                        temp = "";
                        temp1 = "";


                        if (i.IsManager == true && i.IsEmployee == true)
                        {
                            temp = "Manager";
                            vm.IsManagerShow = Visibility.Collapsed;
                            temp1 = "Employee";
                            vm.IsEmployeeShow = Visibility.Collapsed;
                            //vm.Both = Visibility.Visible;
                            //vm.OnlyOne = Visibility.Collapsed;
                            vm.IsBothManagerShow = Visibility.Visible;
                            vm.IsBothEmployeeShow = Visibility.Visible;
                        }
                        else if (i.IsManager == true)
                        {
                            temp = "Manager";
                            vm.IsManagerShow = Visibility.Visible;
                            vm.IsEmployeeShow = Visibility.Collapsed;
                            //vm.Both = Visibility.Collapsed;
                            //vm.OnlyOne = Visibility.Visible;
                            vm.IsBothManagerShow = Visibility.Collapsed;
                            vm.IsBothEmployeeShow = Visibility.Collapsed;
                        }
                        else if (i.IsEmployee == true)
                        {
                            temp1 = "Employee";
                            vm.IsEmployeeShow = Visibility.Visible;
                            vm.IsManagerShow = Visibility.Collapsed;
                            //vm.Both = Visibility.Collapsed;
                            //vm.OnlyOne = Visibility.Visible;
                            vm.IsBothManagerShow = Visibility.Collapsed;
                            vm.IsBothEmployeeShow = Visibility.Collapsed;
                        }


                        vm.UserColl.Add(new Login
                        {
                            EmployeeId = vm.UserIndex + 1,
                            Name = i.Name,
                            EmailId = i.EmailId,
                            Contactno = i.Contactno,
                            Manager = temp,
                            Employee = temp1
                        });
                        vm.UserIndex = vm.UserIndex + 1;
                        //t = t - 1;
                        count = count + 1;
                    }
                }
                if (vm.UserIndex == coll.Count)
                {
                    vm.IsVisibleBtNext = Visibility.Collapsed;
                    vm.IsVisibleBtPrevious = Visibility.Visible;
                }
                else
                {
                    vm.IsVisibleBtPrevious = Visibility.Visible;
                    vm.IsVisibleBtNext = Visibility.Visible;
                }
            }
        }

        private void BtPrevoius_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as HomeVM;
            Objlogin = new Login();
            vm.UserColl = new ObservableCollection<Login>();
            string json = "";

            string tempM = "";
            string tempE = "";
            Objlogin.StoreId = ApplicationData.Store_Id;

            json = Newtonsoft.Json.JsonConvert.SerializeObject(Objlogin);
            int count = 0;
            int temp = 0;
            var responce = client.PostAsync("api/Login/NewGetEmpRecords", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (responce.IsSuccessStatusCode)
            {
                var v = responce.Content.ReadAsStringAsync().Result;
                var coll = JsonConvert.DeserializeObject<ObservableCollection<Login>>(v);

                if ((vm.UserIndex % 5) == 0)
                {
                    count = vm.UserIndex - 10;
                }
                else if ((vm.UserIndex % 5) == 1)
                {
                    count = vm.UserIndex - 6;
                }
                else if ((vm.UserIndex % 5) == 2)
                {
                    count = vm.UserIndex - 7;
                }
                else if ((vm.UserIndex % 5) == 3)
                {
                    count = vm.UserIndex - 8;
                }
                else if ((vm.UserIndex % 5) == 4)
                {
                    count = vm.UserIndex - 9;
                }

                if (count == 0)
                {
                    vm.IsVisibleBtPrevious = Visibility.Collapsed;
                    vm.IsVisibleBtNext = Visibility.Visible;
                }
                else
                {
                    vm.IsVisibleBtNext = Visibility.Visible;
                    vm.IsVisibleBtPrevious = Visibility.Visible;
                }

                if (coll != null)
                {
                    while (temp < 5)
                    {
                        var i = coll.ElementAt(count);

                        tempM = "";
                        tempE = "";


                        if (i.IsManager == true && i.IsEmployee == true)
                        {
                            tempM = "Manager";
                            vm.IsManagerShow = Visibility.Collapsed;
                            tempE = "Employee";
                            vm.IsEmployeeShow = Visibility.Collapsed;
                            //vm.Both = Visibility.Visible;
                            //vm.OnlyOne = Visibility.Collapsed;
                            vm.IsBothManagerShow = Visibility.Visible;
                            vm.IsBothEmployeeShow = Visibility.Visible;
                        }
                        else if (i.IsManager == true)
                        {
                            tempM = "Manager";
                            vm.IsManagerShow = Visibility.Visible;
                            vm.IsEmployeeShow = Visibility.Collapsed;
                            //vm.Both = Visibility.Collapsed;
                            //vm.OnlyOne = Visibility.Visible;
                            vm.IsBothManagerShow = Visibility.Collapsed;
                            vm.IsBothEmployeeShow = Visibility.Collapsed;
                        }
                        else if (i.IsEmployee == true)
                        {
                            tempE = "Employee";
                            vm.IsEmployeeShow = Visibility.Visible;
                            vm.IsManagerShow = Visibility.Collapsed;
                            //vm.Both = Visibility.Collapsed;
                            //vm.OnlyOne = Visibility.Visible;
                            vm.IsBothManagerShow = Visibility.Collapsed;
                            vm.IsBothEmployeeShow = Visibility.Collapsed;
                        }


                        vm.UserColl.Add(new Login
                        {
                            EmployeeId = count + 1,
                            Name = i.Name,
                            EmailId = i.EmailId,
                            Contactno = i.Contactno,
                            Manager = tempM,
                            Employee = tempE
                        });

                        count = count + 1;

                        temp = temp + 1;

                    }

                    if ((vm.UserIndex % 5) == 0)
                    {
                        vm.UserIndex = vm.UserIndex - 5;
                    }
                    else if ((vm.UserIndex % 5) == 1)
                    {
                        vm.UserIndex = vm.UserIndex - 1;
                    }
                    else if ((vm.UserIndex % 5) == 2)
                    {
                        vm.UserIndex = vm.UserIndex - 2;
                    }
                    else if ((vm.UserIndex % 5) == 3)
                    {
                        vm.UserIndex = vm.UserIndex - 3;
                    }
                    else if ((vm.UserIndex % 5) == 4)
                    {
                        vm.UserIndex = vm.UserIndex - 4;
                    }

                }

            }
        }

        private async void BtCreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationData.Manager == false)
            {
                var dialog = new MessageDialog("You are not allowed to access the user creation");
                await dialog.ShowAsync();
            }
            else
            {
                var v = this.DataContext as HomeVM;
                v.IsAddUserPopup = true;
                v.IsHitTestVisible = false;
                v.Username = "";
                v.Password = "";
                v.Name = "";
                v.EmailId = "";
                v.PhoneNo = "";
                v.IsManagerChecked = false;
                v.IsEmployeeChecked = false;
                v.IsAssignThisStore = false;
            }

        }

        private async void BtEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationData.Manager == false)
            {
                var dialog = new MessageDialog("You are not allowed to access the user creation");
                await dialog.ShowAsync();
            }
            else
            {
                var a = ((sender as Button).DataContext as Login).Name;
                var b = ((sender as Button).DataContext as Login).EmailId;
                var c = ((sender as Button).DataContext as Login).Contactno;
                string json = "";
                var v = this.DataContext as HomeVM;
                Login LoginObj = new Login();
                v.IsManagerChecked = false;
                v.IsEmployeeChecked = false;
                v.IsAssignThisStore = false;

                LoginObj.StoreId = ApplicationData.Store_Id;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(LoginObj);
                var responce = client.PostAsync("api/Login/NewGetEmpRecords", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (responce.IsSuccessStatusCode)
                {
                    var res = responce.Content.ReadAsStringAsync().Result;
                    var coll = JsonConvert.DeserializeObject<ObservableCollection<Login>>(res);

                    var i = coll.Where(x => x.Name == a && x.EmailId == b && x.Contactno == c).ToList().FirstOrDefault();
                    v.IsEditUserPopup = true;
                    v.IsHitTestVisible = false;

                    v.Name = i.Name;
                    v.EmailId = i.EmailId;
                    v.PhoneNo = i.Contactno;

                    v.Username = i.Username;

                    if (i.IsManager == true)
                    {
                        v.IsManagerChecked = true;
                    }

                    if (i.IsEmployee == true)
                    {
                        v.IsEmployeeChecked = true;
                    }

                    if (i.IsAssignStore == true)
                    {
                        v.IsAssignThisStore = true;
                    }
                }
            }

        }

        private void BtCloseEditUser_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsEditUserPopup = false;
            v.IsHitTestVisible = true;

        }

        private void TBNotification_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            GDEmail.Visibility = Visibility.Visible;
            GDMainDashboard.Visibility = Visibility.Collapsed;
            GDHamburgerShiftReport.Visibility = Visibility.Collapsed;
            GDHambugerDailyReport.Visibility = Visibility.Collapsed;
            GDUser.Visibility = Visibility.Collapsed;
            GDPassword.Visibility = Visibility.Collapsed;
            GDStoreSettings.Visibility = Visibility.Collapsed;
            v.IsChangePasswordPopup = false;

        }

        private void BtCloseAddEmail_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            v.IsAddEmailPopup = false;
            v.IsHitTestVisible = true;
        }

        private void BtAddEmailPopup_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtCloseEditEmail_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            v.IsEditEmailPopup = false;
            v.IsHitTestVisible = true;
        }

        private void BtCloseGeneralStore_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsStoreSettingsPopup = false;
            v.IsHitTestVisible = true;
        }

        private void BtCloseChangeBox_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsChangeBoxPopup = false;
            v.IsHitTestVisible = true;
        }

        private void BtClosePasswordConfirmation_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;

            v.IsPasswordConfirmShow = false;
            v.IsHitTestVisible = true;
        }

        private void BtCloseResetTreacker_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsResetTrackerPopup = false;
            v.IsResetPasswordConfirmShow = false;
            v.PasswordCheck = "";
            v.IsHitTestVisible = true;
        }

        private void BtCloseResetPasswordConfirmation_Click(object sender, RoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            v.IsResetPasswordConfirmShow = false;
            v.IsHitTestVisible = true;
        }

        private void BtCloseText_Click(object sender, RoutedEventArgs e)
        {
            IsGenerateReport = true;
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

        private void BtCloseGeneralStoreSetup_Click(object sender, RoutedEventArgs e)

        {
            var v = this.DataContext as HomeVM;
            v.IsNewStoreSetupScanExistingBoxesInOrder = false;
            v.IsHitTestVisible = true;
        }

        private void BtNext1_Click(object sender, RoutedEventArgs e)
        {
            //LBEmptyTicket1.SelectedItem = LBEmptyTicket1.Items.IndexOf(2); ;

        }

        private async void txtNewstoreActiveBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (txtNewstoreActiveBarcode.Text == "")
            {
                txtNewstoreActiveBarcode.Text = "";
                txtReceivedPacketId.Text = "";
            }
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // IsValidBarcode = SwitchCase(last);
                // isValidBarcode = true;
                if (txtNewstoreActiveBarcode.Text != null)
                    if (!regex.IsMatch(last.ToString()))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Barcode must be numeric.");
                        await dialog.ShowAsync();
                        try
                        {
                            txtNewstoreActiveBarcode.Text = txtReceiveBarcode.Text.Substring(0, txtReceiveBarcode.Text.Length - 1);
                            txtNewstoreActiveBarcode.SelectionStart = (txtReceiveBarcode.Text.Length + 1) - 1;
                        }
                        catch (Exception ex)
                        {

                        }

                    }
            }
        }

        private async void txtNewstoreActiveBarcode_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount1++;
                if (eventCount1 == 2)
                {
                    eventCount1 = 0;
                    string s = txtNewstoreActiveBarcode.Text.ToString();
                    foreach (Char i in s)
                    {
                        Char c = i;
                        IsValidBarcode = SwitchCase(c);
                    }
                    if (IsValidBarcode)
                    {
                        if (GetBarcodeFormat != null)
                        {

                            Words = txtNewstoreActiveBarcode.Text;

                            if (txtNewstoreActiveBarcode.Text.Length < GetBarcodeFormat.BarCodeLength || txtNewstoreActiveBarcode.Text.Length > GetBarcodeFormat.BarCodeLength)
                            {
                                if (Flag == 0)
                                {
                                    Flag = 1;
                                    var dialog = new MessageDialog("Barcode must be Equal to " + GetBarcodeFormat.BarCodeLength + " Digit For " + ApplicationData.SelectedState + " State.");
                                    await dialog.ShowAsync();
                                }
                            }
                            else
                            {
                                int GameidRange = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
                                int PacketidRange = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                                int SequencenoRange = GetBarcodeFormat.SequenceIDTo - GetBarcodeFormat.SequenceNoFrom;


                                string GameId = Words.Substring(GetBarcodeFormat.GameIDFrom, GameidRange);
                                string PacketID = Words.Substring(GetBarcodeFormat.PacketIDFrom, PacketidRange);
                                string Startno = Words.Substring(GetBarcodeFormat.SequenceNoFrom, SequencenoRange);


                                //v.Active_StatusObj.Game_Id = GameId;
                                v.Active_StatusObj.Packet_No = PacketID;
                                v.Active_StatusObj.Start_No = Startno;

                                v.OnMasterHistory();
                                // var s1 = v.MasterColl.Where(x => x.Game_Id == a).FirstOrDefault();
                                var s1 = v.MasterColl.Where(x => x.State == ApplicationData.SelectedState).ToList();
                                var v1 = s1.Where(p => p.Game_Id == GameId).FirstOrDefault();

                                if (v1 != null)
                                {
                                    v.Active_StatusObj.Game_Id = v1.Game_Id;
                                    v.Active_StatusObj.Ticket_Name = v1.Ticket_Name;
                                    v.Active_StatusObj.Price = Convert.ToInt32(v1.Rate);
                                    v.Active_StatusObj.End_No = v1.End_No;
                                    if (Convert.ToInt32(v.Active_StatusObj.Start_No) > Convert.ToInt32(v.Active_StatusObj.End_No))
                                    {
                                        IsValid = false;
                                        var dialog = new MessageDialog("Start no. must be less than End no.");
                                        await dialog.ShowAsync();
                                        IsHitTestVisible = false;
                                    }
                                    else
                                    {
                                        v.Active_StatusObj.Game_Id = GameId;
                                        //v.Active_StatusObj.Packet_No = PacketID;
                                        //v.Active_StatusObj.Start_No = Startno;
                                        v.OnNewStoreStatusUpdate();
                                    }
                                }
                                else
                                {
                                    txtNewstoreActiveBarcode.Text = "";
                                    v.Active_StatusObj.Packet_No = "";
                                    v.Active_StatusObj.Start_No = "";
                                    var dialog = new MessageDialog("Not Available in Master List , Do you want to Add to Mater List ?");
                                    dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                                    dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                                    var res = await dialog.ShowAsync();
                                    if ((int)res.Id == 0)
                                    {
                                        v.IsHitTestVisiblePopup = false;
                                        v.IsAddInventoryPopup = true;

                                    }
                                }
                            }
                        }
                        else
                        {
                            var dialog1 = new MessageDialog("Barcode Format was Not Set for " + ApplicationData.SelectedState + " State.");
                            await dialog1.ShowAsync();
                        }

                        Flag = 0;
                    }
                }
            }
        }

        private async void txtNewstoreActivePacketId_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var v = this.DataContext as HomeVM;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                eventCount1++;
                if (eventCount1 == 2)
                {
                    eventCount1 = 0;
                    string s = txtNewstoreActivePacketId.Text.ToString();
                    foreach (Char i in s)
                    {
                        Char c = i;
                        IsValidBarcode = SwitchCase(c);
                    }
                    if (IsValidBarcode)
                    {
                        if (GetBarcodeFormat != null)
                        {
                            Words = txtNewstoreActivePacketId.Text;
                            int PacketidRange = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                            if (Words.Length > PacketidRange || Words.Length < PacketidRange)
                            {
                                var dialog1 = new MessageDialog("Packet Id must be " + PacketidRange + " Digit");
                                await dialog1.ShowAsync();
                            }
                            else if (Convert.ToInt32(v.Active_StatusObj.Start_No) > Convert.ToInt32(v.Active_StatusObj.End_No))
                            {
                                var dialog = new MessageDialog("Start no. must be less than End no.");
                                await dialog.ShowAsync();
                            }
                            else
                            {
                                v.OnNewStoreStatusUpdate();
                            }
                        }
                    }
                }
            }

        }

        private async void txtNewstoreActivePacketId_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            string s = t.Text.ToString();
            if (s != string.Empty)
            {
                char last = s[s.Length - 1];
                // IsValidBarcode = SwitchCase(last);
                // isValidBarcode = true;
                //if (txtBarcodeActive.Text != null)
                if (!regex.IsMatch(last.ToString()))
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Barcode must be numeric.");
                    await dialog.ShowAsync();
                    try
                    {
                        txtNewstoreActivePacketId.Text = txtNewstoreActivePacketId.Text.Substring(0, txtNewstoreActivePacketId.Text.Length - 1);
                        txtNewstoreActivePacketId.SelectionStart = (txtNewstoreActivePacketId.Text.Length + 1) - 1;
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }

        private async void txtNewstoreActiveGameIDAutoSuggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var v = this.DataContext as HomeVM;
            v.OnMasterHistory();

            //if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            //{

            var coll = v.MasterColl.Where(x => x.State == ApplicationData.SelectedState && x.Store_Id == ApplicationData.Store_Id).Select(x => x.Game_Id).ToList();
            //var coll = v.MasterColl.Where(x => x.State).ToList();
            //var coll = v.MasterColl.Select(x => x.Game_Id).ToList();
            _listSuggestion1 = (coll.Where(x => x.StartsWith(sender.Text)).Distinct()).ToList();
            if (-_listSuggestion1.Count == 0 && v.Active_StatusObj.Game_Id != "")
            {
                var dialog = new MessageDialog("Not Available in Master List.Please Add in Master List.");
                await dialog.ShowAsync();
                txtNewstoreActiveBarcode.Text = "";
                v.Active_StatusObj.Game_Id = "";
                v.Active_StatusObj.Packet_No = "";
                v.Active_StatusObj.Ticket_Name = "";
                v.Active_StatusObj.Price = 0;
                v.Active_StatusObj.Start_No = "";
                v.Active_StatusObj.End_No = "";
            }
            else
            {
                sender.ItemsSource = _listSuggestion1;

                if (txtNewstoreActiveGameIDAutoSuggest.Text == "")
                {
                    v.Active_StatusObj.Game_Id = "";
                    v.Active_StatusObj.Packet_No = "";
                    v.Active_StatusObj.Ticket_Name = "";
                    v.Active_StatusObj.Price = 0;
                    v.Active_StatusObj.Start_No = "";
                    v.Active_StatusObj.End_No = "";

                }
            }
        }

        private void txtNewstoreActiveGameIDAutoSuggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                var v = this.DataContext as HomeVM;
                string GameId = args.QueryText.ToString();
                v.OnMasterHistory();
                var s = v.MasterColl.Where(x => x.Game_Id == GameId && x.Store_Id == ApplicationData.Store_Id).FirstOrDefault();
                if (args.QueryText != null)
                {
                    v.Active_StatusObj.Game_Id = s.Game_Id;
                    v.Active_StatusObj.Packet_No = s.Packet_No;
                    v.Active_StatusObj.Ticket_Name = s.Ticket_Name;
                    v.Active_StatusObj.Price = Convert.ToInt32(s.Rate);
                    v.Active_StatusObj.Start_No = s.Start_No;
                    v.Active_StatusObj.End_No = s.End_No;

                    if (v.IsNewStoreSetupScanExistingBoxesInOrder == true)
                    {
                        v.Active_StatusObj.Game_Id = s.Game_Id;
                        v.Active_StatusObj.Ticket_Name = s.Ticket_Name;
                        v.Active_StatusObj.Price = Convert.ToInt32(s.Rate);
                        v.Active_StatusObj.Start_No = s.Start_No;
                        v.Active_StatusObj.End_No = s.End_No;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void NewStoreCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void txtPackId_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            int PacketIdRange = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;

            if (txtPackId.Text.Length > PacketIdRange)
            {
                var dialog = new MessageDialog("PacketId Must be " + PacketIdRange + " Digit.");
                await dialog.ShowAsync();
                try
                {
                    txtPackId.Text = "";
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async void BtReset_Click(object sender, RoutedEventArgs e)
        {
            if (btnResetThree.IsChecked == true)
            {
                var dialog1 = new MessageDialog("Store Reset successfully done");
                await dialog1.ShowAsync();
                Frame.Navigate(typeof(LoginWindow));
            }
        }

        private void EmailIdSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var A = ((sender as ToggleSwitch).DataContext as Store_Info).EmailId1;
            var b = ((sender as ToggleSwitch).DataContext as Store_Info).Index;
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            var v = this.DataContext as HomeVM;
            string json = "";
            Store_Info LoginObj = new Store_Info();
            LoginObj.StoreID = ApplicationData.Store_Id;
            LoginObj.EmployeeId = ApplicationData.Emp_Id;
            LoginObj.Index = b;
            v.IsEmail_On_Off = toggleSwitch.IsOn;
            LoginObj.IsEmail_On_Off = v.IsEmail_On_Off;
            //LoginObj.IsON_Off = toggleSwitch.IsOn;
            LoginObj.StoreName = ApplicationData.StoreName;
            json = Newtonsoft.Json.JsonConvert.SerializeObject(LoginObj);
            var response = client.PostAsync("api/Login/EmailID_ON_Off", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                v.IsHitTestVisible = true;
            }
        }
    }
}
