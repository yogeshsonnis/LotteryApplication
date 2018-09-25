using Lottery_Application.HelperClasses;
using Lottery_Application.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lottery_Application.ViewModel
{

    public class HomeVM : BaseHandler
    {

        Regex regex = new Regex("^0*([0-9]*)$");
        Regex regex1 = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        public BarcodeFormat GetBarcodeFormat { get; set; }

        public static Frame RootFrame;

        public int NewStoreBoxIndex = 0;

        ObservableCollection<BarcodeFormat> barcodeFormatCollection;
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

        Login objlogin;
        #region privateFields
        DateTime openDate;
        DateTime t1;
        string openTime;
        string closeTime;
        string date;
        string time;
        string hamburgerTime;
        string location;
        string selectedItem;
        string selectedStoreName;
        string user;
        string shiftdate;
        string dailyEndTime;
        string dailyDate;
        int displayReportAtHamburger;
        string username;
        string password;
        bool isRememberMe;
        string name;
        string emailId;
        string phoneNo;
        string oldPassword;
        string newPassword;
        string confirmPassword;
        string addEmailId;
        string userId;
        string userName;
        string userEmail;
        string userPhoneNo;
        string userRole;
        string manager;
        string employee;
        string makeNull;
        public int UserIndex = 0;
        public int FlagAutoGameID = 0;
        string changeFromBox;
        string changeToBox;
        string passwordCheck;

        public string SelectedStoreName
        {
            get
            {
                return selectedStoreName;
            }
            set
            {
                selectedStoreName = value;
                NotifyPropertyChanged("SelectedStoreName");
            }
        }

        Visibility isVisibleReceivedDataGrid;
        Visibility isVisibleDeactivateDataGrid;
        Visibility isVisibleActivateDataGrid;
        Visibility isVisibleSoldoutDataGrid;
        Visibility isVisibleReturnDataGrid;
        Visibility isVisibleSettledDataGrid;
        Visibility isVisibleComboBox;
        Visibility isVisibleTextBox;
        Visibility isVisibleActiveBoxNo;
        Visibility isVisibleActiveComboBox;
        Visibility isVisiblebtnGrid;
        Visibility isVisiblecalendarGrid;
        Visibility isVisibleMainPage;
        Visibility isVisibleShiftReport;
        Visibility isVisibleGenerateReport;
        Visibility isBtnSaveVisible;
        Visibility isVisibleShiftSubmit;
        Visibility isShiftReportDetail;
        Visibility isShiftReportHamburger;
        Visibility isDataFromLotteryApp;
        Visibility isDataFromTerminal;
        Visibility isActiveAndStockInfo;
        Visibility isDataFromLotteryAppHamburger;
        Visibility isDataFromTerminalHamburger;
        Visibility isActiveAndStockInfoHamburger;
        Visibility isUserShiftReport;
        Visibility isUserShiftReportHamburger;
        Visibility isDateShiftReport;
        Visibility isDateshiftReportHamburger;
        Visibility isCloseTimeShiftReport;
        Visibility isCloseTimeShiftReportHamburger;
        Visibility isTotalStockActiveInventory;
        Visibility isTotalStockActiveInventoryHamburger;
        Visibility isDailyReportUserName;
        Visibility isDailyReportHamburgerName;
        Visibility isDailyReportDate;
        Visibility isDailyReportDateHamburger;
        Visibility isDailyReportTime;
        Visibility isDailyReportTimeHamburger;
        Visibility isShiftRecordNotFound;
        Visibility isShiftRecordFound;
        Visibility isDailyRecordNotFound;
        Visibility isDailyRecordFound;
        Visibility isVisibleBtNext;
        Visibility isVisibleBtPrevious;
        Visibility isManagerShow;
        Visibility isEmployeeShow;
        Visibility isBothManagerShow;
        Visibility isBothEmployeeShow;
        Visibility onlyOne;
        Visibility both;
        Visibility isNewstoreCheckbox;

        List<string> stateCollection;
        List<string> _listSuggestion;

        bool isCloseShiftActivateBox;
        bool isReceiveChecked;
        bool isActivateChecked;
        bool isDeactivateChecked;
        bool isSoldoutChecked;
        bool isReturnChecked;
        bool isSettledChecked;
        bool isContentChecked;
        bool isSingupPopup;
        bool isReportPopup;
        bool isDailyReportPopup;
        bool isShowActivateReturnPopup;
        bool isPopupEmptybox;
        bool isReceiveManuallyPopup;
        bool isSettlePopup;
        bool isActivatePopup;
        bool isDeactivatePopup;
        bool isHistoryPopup;
        bool isactivateHistoryPopup;
        bool isSoldOutBox;
        bool isCloseshitfPopup;
        bool isAddInventoryPopup;
        bool isSoldOutPopup;
        bool isBoxClosePopup;
        bool isActivateBox;
        bool isActivedChecked;
        bool isValid;
        bool isReturnPopup;
        bool isCloseBoxReopen;
        bool isDataTerminalPopup;
        bool isHitTestVisible;
        bool isHitTestVisiblePopup;
        bool isUserPopup;
        bool isDeleteUserPopup;
        bool isEmailSettingsPopup;
        bool isLastShiftChecked;
        bool isAddUserPopup;
        bool isChangePasswordPopup;
        bool isStoreSettingsPopup;
        bool isSupportSettingsPopup;
        bool isManagerChecked;
        bool isNewStoreChecked;
        bool isEmployeeChecked;
        bool isassignThisStore;
        bool isAutoSettle;
        bool isEmail_On_Off;
        bool isEditUserPopup;
        bool isAddEmailPopup;
        bool isEditEmailPopup;
        bool isChangeBoxPopup;
        bool isPasswordConfirmShow;
        bool isResetTrackerPopup;
        bool isResetPasswordConfirmShow;
        bool isResetOne;
        bool isNewStoreSettingsPopup;
        bool isResetTwo;
        bool isResetThree;
        bool isNewStoreSetupScanExistingBoxesInOrder;
        public bool IsDeactClicked { get; set; }


        //LotteryInfo l_Info;
        ObservableCollection<Receive_Inventory> getpackId;
        ObservableCollection<TicketCollection> tic_Coll;
        ObservableCollection<string> empty_Ticket;
        ObservableCollection<LotteryInfo> lotteryColl;
        ObservableCollection<Activation_Box> historyColl;
        ObservableCollection<Employee_Details> employeeHistory;
        ObservableCollection<StateClass> stateColl;
        ObservableCollection<Activation_Box> historyCollReceive;
        ObservableCollection<Master_List_Inventory> masterColl;
        ObservableCollection<Activation_Box> activatehistoryColl;
        ObservableCollection<Shift_Details> shiftCollection;
        ObservableCollection<Shift_Details> shiftReport;
        ObservableCollection<Shift_Details> storeDate;
        ObservableCollection<Activation_Box> dailyReportActiveCreoll;
        ObservableCollection<Receive_Inventory> getreceiveandactive;
        ObservableCollection<Activation_Box> deactivatehistoryColl;
        ObservableCollection<SoldoutHistory> soldhistorycoll;
        ObservableCollection<SoldoutHistory> soldOutReportHistory;
        ObservableCollection<Activation_Box> soldouthistoryColl;
        ObservableCollection<Activation_Box> returnhistoryColl;
        ObservableCollection<Activation_Box> settleHistoryColl;
        ObservableCollection<Activation_Box> active_BoxCollection_forSoldOut;
        ObservableCollection<Activation_Box> active_BoxCollection;
        ObservableCollection<Activation_Box> emptyBoxCollction;
        ObservableCollection<Activation_Box> boxCollection;
        ObservableCollection<Activation_Box> active_BoxCollection_forReturn;
        ObservableCollection<Activation_Box> active_BoxCollection_forSettle;
        ObservableCollection<Terminal_Details> terminalDataCollection;
        ObservableCollection<Terminal_Details> terminalDailyReport;
        ObservableCollection<Activate_Ticket> activeTicketTotal;
        ObservableCollection<Store_Info> storeCollection;
        ObservableCollection<Terminal_Details> terminalColl;
        ObservableCollection<Main_Shift_Collection> mainShiftReportColl;
        Main_Shift_Collection shiftReportSelectedDate;
        ObservableCollection<Shift_Details> tempColl;
        ObservableCollection<Shift_Details> temp1Coll;
        ObservableCollection<Terminal_Details> dailyReport;
        ObservableCollection<Terminal_Details> mainDailyReportColl;
        ObservableCollection<Login> userColl;


        Master_List_Inventory masterListObj;
        TicketCollection tic_Object;
        Activation_Box selectedData;
        Activation_Box receiveselectedData;
        Receive_Inventory receiveObj;
        Settle_Details settle_Obj;
        LotteryInfo single_Record;
        Activation_Box selectedBox;
        Activate_Ticket active_statusObj;
        Activation_Box selectedActived_Box;
        LotteryInfo selectedIndexBoxno;
        Activate_Ticket activeBoxObj;
        Employee_Details emp_Details_Obj;
        Store_Info store_Info_Obj;
        StateClass state_Details_Obj;
        Shift_Details shift_dt;
        SoldOut_Details soldOutObj;
        Activation_Box selectedActived_Box_ForSoldOut;
        Return_Details return_Obj;
        Activation_Box selectedActived_Box_ForReturn;
        LotteryInfo lotteryInfoObj;
        StateClass state;
        Activation_Box selectedActived_Box_ForSettle;
        Close_Box objCloseBox;
        Terminal_Details terminalObj;
        Terminal_Details dailyTotal;
        Store_Info storeObj;
        Store_Info selectedStore;
        Shift_Details shiftObj;
        Terminal_Details hamburgerSelectedData;
        Shift_Details shiftReportSelectedData;
        Activation_Box activationBoxObj;
        Store_Info store_Details;

        RelayCommand settleCommand;
        RelayCommand recieveCommand;
        RelayCommand refresh_BoxCollection;
        RelayCommand activate_TicketCommand;
        RelayCommand _next;
        RelayCommand _previous;
        RelayCommand soldOutCommand;
        RelayCommand settle_Ticket;
        RelayCommand returnCommand;
        RelayCommand return_Ticket;
        RelayCommand soldOut_Ticket;
        RelayCommand update_Lottery;
        RelayCommand update_ActivatePackets;
        RelayCommand activateCommand;
        RelayCommand deactivate_Ticket;
        RelayCommand soldOutHistoryCommand;
        RelayCommand deactivateCommand;
        RelayCommand deActivatehistoryCommand;
        RelayCommand activateHistoryCommand;
        RelayCommand receiveHistoryCommand;
        RelayCommand onEmployeeHistory;
        RelayCommand returnHistoryCommand;
        RelayCommand settelmentHistoryCommand;
        RelayCommand emptyHistoryCommand;
        RelayCommand deleteSelectedCommand;
        RelayCommand saveEmployeeDetails;
        RelayCommand signupcommand;
        RelayCommand closeshiftCommand;
        RelayCommand saveNewInventory;
        RelayCommand showDailyReport;
        RelayCommand closeBoxCommand;
        RelayCommand closeBox;
        RelayCommand reopenBox;
        RelayCommand saveTerminalData;
        RelayCommand tbDashboard;
        RelayCommand tBDailyReport;
        RelayCommand tBShiftReport;
        RelayCommand addUserCommand;
        RelayCommand generateReport;
        RelayCommand addEmailCommand;
        RelayCommand changePwdCommand;
        RelayCommand usersCommand;
        RelayCommand editUserCommand;
        RelayCommand emailCommand;
        RelayCommand editEmailCommand;
        RelayCommand generalSettingCommand;
        RelayCommand storeSettingCommand;
        RelayCommand changeBoxCommand;
        RelayCommand saveChangeBoxCommand;
        RelayCommand removeShiftCommand;
        RelayCommand checkPwdCommand;
        RelayCommand resetTrackerCommand;
        RelayCommand resetCheckPwdCommand;
        RelayCommand resetCommand;
        RelayCommand removeboxCommand;
        RelayCommand finishSetupCommand;

        //public int Flag = 0;
        int selectedActiveBox_ForSettle;
        int selectedActiveBox;
        int activateCount;
        int totalPrice;
        int? activetotalPrice;
        int totalPackets;
        int selectedBoxNo;
        int? displayboxno;
        int countActiveBox;
        int countReceiveBox;
        int countDeactiveBox;
        int selectedActiveBox_ForSoldOut;
        int selectedActiveBox_ForReturn;
        int countSoldOutBox;
        int countReturnBox;
        int countSettleBox;
        int countEmptyBox;
        int? total;
        int _selectedIndex;
        int countActiveReceive;
        string countTerminalActiveReceive;
        int? totalSells;
        int totalPayOut;
        int totScratchsell;
        int totCashOnHand;
        int totScratchPayout;
        int totOnlineSells;
        int totOnlinePayout;
        int totTrackedAmount;
        int totDateScratchsells;
        int totDateScratchPayout;
        int totDateOnlineSells;
        int totDateOnlinePayout;
        int totDateTrackedAmount;
        int totDateCashOnHand;
        public int Temp = 0;
        public int Dailytemp = 0;
        public int DailytempHamburger = 0;
        public int Id = 0;
        public int StoreId;
        public int EmpId;
        string shiftReportDate;
        DateTime? singleShiftReportDate;
        int islastShift;
        int index;
        //int settleTemp;

        #endregion


        #region WebApifields

        HttpClient client = new HttpClient();

        #endregion

        #region Constructor
        public HomeVM()
        {
            Empty_Ticket = new ObservableCollection<string>();
            IsVisibleGenerateReport = Visibility.Collapsed;
            InitalizeVariable();
            LoadBoxCount();
            getState();
            LoadStoreCollection();
            LoadLotteryCollection();
            LoadBoxCollection();
            BarcodeFormatDetails();
            InitializeRelayCommands();
            // MasterColl = new ObservableCollection<Master_List_Inventory>();
            SelectedIndex = 0;
            SelectedIndexBoxno = new LotteryInfo();
            SelectedIndexBoxno.Box_No = 1;
            SelectedIndexBoxno.Status = "Empty";
            ReceiveObj = new Receive_Inventory();
            Store_Details = new Store_Info();
            IsHitTestVisible = true;
            IsHitTestVisiblePopup = true;
            IsReceiveManuallyPopup = false;
            LoadActive_BoxCollection();
            LoadEmptyBoxes();
            //Refresh_BoxCollection = new RelayCommand(OnRefreshBoxCollection);
            IsActivatePopup = false;
            Active_StatusObj = new Activate_Ticket();
            IsDeactivatePopup = false;
            ActiveBoxObj = new Activate_Ticket();
            IsSoldOutPopup = false;
            SoldOutObj = new SoldOut_Details();
            //LoadActive_BoxCollection_ForSoldOut();
            IsReturnPopup = false;
            Return_Obj = new Return_Details();
            //LoadActive_BoxCollection_ForReturn();
            IsSettlePopup = false;
            IsHistoryPopup = false;
            IsactivateHistoryPopup = false;
            IsVisibleTextBox = Visibility.Collapsed;
            Settle_Obj = new Settle_Details();
            IsVisiblecalendarGrid = Visibility.Collapsed;
            LotteryInfoObj = new LotteryInfo();
            IsVisibleActiveComboBox = Visibility.Visible;
            IsVisibleActiveBoxNo = Visibility.Collapsed;
            OpenDate = System.DateTime.Now;
            SaveEmployeeDetails = new RelayCommand(OnSaveEmployeeDetails);
            Signupcommand = new RelayCommand(OnShowSignUpPopup);
            CloseshiftCommand = new RelayCommand(OnCloseshitf);
            SaveNewInventory = new RelayCommand(OnSaveNewInventory);
            Date = OpenDate.ToString("MM/dd/yyyy");
            Time = OpenDate.ToString("hh:mm tt");
            GetShiftDetails();
            State = new StateClass();
            Emp_Details_Obj = new Employee_Details();
            SingleShiftReportDate = null;
            Store_Info_Obj = new Store_Info();
            State = new StateClass();
            MasterListObj = new Master_List_Inventory();
            OnMasterHistory();
            Single_Record = new LotteryInfo();
            //LoadActive_BoxCollection_ForSettle();
            Location = ApplicationData.SelectedState;
            ShowDailyReport = new RelayCommand(OnShowDailyReport);
            ObjCloseBox = new Close_Box();
            TerminalObj = new Terminal_Details();
            DailyTotal = new Terminal_Details();
            TbDashboard = new RelayCommand(OnTbDashboard);
            ShiftObj = new Shift_Details();
            TBDailyReport = new RelayCommand(OnTBDailyReport);
            TBShiftReport = new RelayCommand(OnTBShiftReport);
            HamburgerSelectedData = new Terminal_Details();
            User = ApplicationData.Username;
            IsDailyReportPopup = false;
            ShiftReportSelectedData = new Shift_Details();
            IsReportPopup = false;
            HamburgerSelectedData = null;
            ShiftReportSelectedData = null;
            ShiftReportSelectedDate = new Main_Shift_Collection();
            AddUserCommand = new RelayCommand(OnSaveEmployeeDetails);
            AddEmailCommand = new RelayCommand(OnAddEmail);
            ChangePwdCommand = new RelayCommand(OnChangePwd);
            UsersCommand = new RelayCommand(OnUsersMgmt);
            EditUserCommand = new RelayCommand(OnEditUser);
            EmailCommand = new RelayCommand(OnNotification);
            EditEmailCommand = new RelayCommand(OnEditEmail);
            GeneralSettingCommand = new RelayCommand(OnGeneralSetting);
            StoreSettingCommand = new RelayCommand(OnStoreSetting);
            ChangeBoxCommand = new RelayCommand(OnChangeBox);
            SaveChangeBoxCommand = new RelayCommand(OnSaveChangeBox);
            ActivationBoxObj = new Activation_Box();
            RemoveShiftCommand = new RelayCommand(OnRemoveShift);
            CheckPwdCommand = new RelayCommand(OnCheckPwd);
            ResetTrackerCommand = new RelayCommand(OnResetTracker);
            ResetCheckPwdCommand = new RelayCommand(OnResetPasswordCheck);
            ResetCommand = new RelayCommand(OnReset);
            RemoveboxCommand = new RelayCommand(OnRemoveBox);
            FinishSetupCommand = new RelayCommand(OnFinishSetup);
            //SettleTemp = 0;
        }

        private async void OnFinishSetup()
        {
            var dialog = new MessageDialog("Do you want to Finish this Setup?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var res = await dialog.ShowAsync();
            if ((int)res.Id == 0)
            {
                IsNewStoreSetupScanExistingBoxesInOrder = false;
                IsHitTestVisible = true;
            }
        }

        private async void OnRemoveBox()
        {
            Active_StatusObj.Box_No = NewStoreBoxIndex + 1;
            Active_StatusObj.State = ApplicationData.SelectedState;
            Active_StatusObj.EmployeeID = ApplicationData.Emp_Id;
            Active_StatusObj.Store_Id = ApplicationData.Store_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Active_StatusObj);
            var response = client.PostAsync("api/Activate/NewStoreRemoveBox", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var dialog = new MessageDialog("Box " + Active_StatusObj.Box_No + " remove Successfully...");
                await dialog.ShowAsync();
                GetActivedBoxCount();
                LoadLotteryCollection();
                SelectedIndexBoxno = LotteryColl[NewStoreBoxIndex];
                //OnNext();
                //GetActivedBoxCount();

            }
            else
            {
                var dialog = new MessageDialog("Box already Empty.");
                await dialog.ShowAsync();
            }
        }
        public async void OnNewStoreStatusUpdate()
        {
            //IsHitTestVisible = true;
            SelectedItem = "Active";
            int PacketIdRange = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
            int GameIdRange = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;

            if (IsNewStoreSetupScanExistingBoxesInOrder == true)
            {

                Active_StatusObj.Box_No = NewStoreBoxIndex + 1;
            }
            Active_StatusObj.State = ApplicationData.SelectedState;
            Active_StatusObj.EmployeeID = ApplicationData.Emp_Id;
            Active_StatusObj.Store_Id = ApplicationData.Store_Id;
            if(Active_StatusObj.Packet_No.Length > PacketIdRange || Active_StatusObj.Packet_No.Length < PacketIdRange)
            {
                var dialog = new MessageDialog("PacketId Must be " + PacketIdRange + " Digit.");
                await dialog.ShowAsync();
            }
            else if(Active_StatusObj.Game_Id.Length > GameIdRange || Active_StatusObj.Game_Id.Length < GameIdRange)
            {
                var dialog = new MessageDialog("GameId Must be " + GameIdRange + " Digit.");
                await dialog.ShowAsync();
            }
            else
            {
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(Active_StatusObj);
                var response = client.PostAsync("api/Activate/NewStoreActivateTicket", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Ticket Activated Successfully..");
                    await dialog.ShowAsync();
                  //  IsActivatePopup = false;
                    IsHitTestVisible = true;
                    FlagAutoGameID = 1;
                    MakeNull = "";
                    //GetRefreshGrid();
                    //GetReceiveBoxCount();
                    //GetSoldOutBoxCount();
                    //GetEmptyBoxCount();
                    
                    LoadBoxCollection();
                    LoadLotteryCollection();
                    //OnReceiveHistory();
                    LoadEmptyBoxes();
                    LoadActive_BoxCollection();
                    GetActivedBoxCount();
                    OnNext();
                    Active_StatusObj = new Activate_Ticket();
                }
                else if (response.ReasonPhrase == "Not Found")
                {

                    var dialog = new MessageDialog("This PacketID or Box is Already Activate.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                else
                {
                    var dialog = new MessageDialog("Please Select Proper Box No AND Packet ID.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
            }
        }

        //       private  async void OnNewStoreSetting()
        //{

        //           string json = "";
        //           //   Store_Details = new Store_Info();
        //           Store_Info tempObj = new Store_Info();
        //           tempObj.NoOfBoxes = Store_Details.NoOfBoxes;
        //           tempObj.EmailId1 = Store_Details.EmailId1;
        //           tempObj.EmailId2 = Store_Details.EmailId2;
        //           tempObj.EmailId3= Store_Details.EmailId3;
        //           ApplicationData.Store_Id = Convert.ToInt32(SelectedStore.StoreID);
        //           tempObj.StoreID = ApplicationData.Store_Id;
        //           tempObj.StoreName = ApplicationData.StoreName;
        //           tempObj.StoreStatus = Store_Details.StoreStatus;
        //           tempObj.OpenTime = OpenTime;
        //           tempObj.CloseTime = CloseTime;

        //           json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);

        //           var response = client.PostAsync("api/StoreSetting/OnGeneralSetting", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
        //           if (response.IsSuccessStatusCode)
        //           {
        //               LoadLotteryCollection();
        //               GetEmptyBoxCount();
        //               LoadEmptyBoxes();
        //               IsNewStoreSettingsPopup = false;

        //           }
        //       }
        private async void OnStoreSetting()
        {
            string json = "";
            //   Store_Details = new Store_Info();
            Store_Info tempObj = new Store_Info();
            tempObj.SettlementDays = Store_Details.SettlementDays;
            tempObj.NoOfBoxes = Store_Details.NoOfBoxes;
            tempObj.AutoSettle = IsAutoSettle;
            tempObj.StoreID = ApplicationData.Store_Id;
            tempObj.OpenTime = OpenTime;
            tempObj.CloseTime = CloseTime;


            json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);
            var response = client.PostAsync("api/StoreSetting/OnGeneralSetting", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var dialog = new MessageDialog("Store Info Save Successfully");
                await dialog.ShowAsync();
                LoadLotteryCollection();
                GetEmptyBoxCount();
                LoadEmptyBoxes();
                // LoadBoxCollection();
            }
            else
            {
                var dialog = new MessageDialog("Can't change box count..");
                await dialog.ShowAsync();
            }


        }

        private async void OnReset()
        {
            string json = "";
            Store_Info tempObj = new Store_Info();
            tempObj.StoreID = ApplicationData.Store_Id;
            tempObj.EmployeeId = ApplicationData.Emp_Id;
            if (IsResetOne == false && IsResetTwo == false && IsResetThree == false)
            {
                var dialog = new MessageDialog("Please select one option");
                await dialog.ShowAsync();
            }
            else
            {

                if (IsResetOne == true)
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);

                    var response = client.PostAsync("api/Login/ResetTracker", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {

                        IsResetTrackerPopup = false;
                        IsResetPasswordConfirmShow = false;
                        PasswordCheck = "";
                        IsResetOne = false;
                        IsResetTwo = false;
                        IsHitTestVisible = true;
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);
                        var response1 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            LoadLotteryCollection();
                            GetReceiveBoxCount();
                            OnReceiveHistory();
                            GetActivedBoxCount();
                            OnActivateHistory();
                            GetDeactivedBoxCount();
                            OnDeActivateHistory();
                            GetSoldOutBoxCount();
                            OnSoldOutHistory();
                            GetReturnedBoxCount();
                            OnReturnHistory();
                            GetSettledBoxCount();
                            OnSettelementHistory();
                            GetEmptyBoxCount();
                            if (IsResetThree != true)
                            {
                                var dialog1 = new MessageDialog("Reset Tracker is successfully done");
                                await dialog1.ShowAsync();
                            }
                        }
                        //LoadLotteryCollection();
                        //GetReceiveBoxCount();
                        //OnReceiveHistory();
                        //GetActivedBoxCount();
                        //OnActivateHistory();
                        //GetDeactivedBoxCount();
                        //OnDeActivateHistory();
                        //GetSoldOutBoxCount();
                        //OnSoldOutHistory();
                        //GetReturnedBoxCount();
                        //OnReturnHistory();
                        //GetSettledBoxCount();
                        //OnSettelementHistory();
                        //GetEmptyBoxCount();
                    }
                }
                else if(IsResetTwo == true)
                {

                    tempObj.StoreName = ApplicationData.StoreName;

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);

                    var response = client.PostAsync("api/Login/ResetTracker", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {


                        IsResetTrackerPopup = false;
                        IsResetPasswordConfirmShow = false;
                        PasswordCheck = "";
                        IsResetOne = false;
                        IsResetTwo = false;
                        IsHitTestVisible = true;
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);
                        var response1 = client.PostAsync("api/Shift/SaveShiftInfo", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            LoadLotteryCollection();
                            GetReceiveBoxCount();
                            OnReceiveHistory();
                            GetActivedBoxCount();
                            OnActivateHistory();
                            GetDeactivedBoxCount();
                            OnDeActivateHistory();
                            GetSoldOutBoxCount();
                            OnSoldOutHistory();
                            GetReturnedBoxCount();
                            OnReturnHistory();
                            GetSettledBoxCount();
                            OnSettelementHistory();
                            GetEmptyBoxCount();
                            if (IsResetThree != true)
                            {
                                var dialog1 = new MessageDialog("Reset Tracker is successfully done");
                                await dialog1.ShowAsync();
                            }
                        }
                    }
                }
                else
                {

                    tempObj.StoreStatus = "New Active";

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);

                    var response = client.PostAsync("api/Login/ResetTracker", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IsResetTrackerPopup = false;
                        IsResetPasswordConfirmShow = false;
                        PasswordCheck = "";
                        IsResetOne = false;
                        IsResetTwo = false;
                        IsHitTestVisible = true;
                    }
                }
            }

        }

        private async void OnResetPasswordCheck()
        {
            StoreCollection = new ObservableCollection<Store_Info>();

            if (PasswordCheck == null || PasswordCheck == "")
            {
                var dialog = new MessageDialog("Please enter password");
                await dialog.ShowAsync();
            }

            else if (PasswordCheck == ApplicationData.Password)
            {
                var dialog = new MessageDialog("Are you sure you want to Reset the tracker");
                dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                var res = await dialog.ShowAsync();

                if ((int)res.Id == 0)
                {
                    IsResetTrackerPopup = true;
                    IsResetPasswordConfirmShow = false;
                    PasswordCheck = "";
                    IsResetOne = false;
                    IsResetTwo = false;
                    IsHitTestVisible = false;
                }
                else if ((int)res.Id == 1)
                {
                    PasswordCheck = "";
                    IsResetPasswordConfirmShow = false;
                    IsHitTestVisible = true;
                }
            }
            else
            {
                var dialog2 = new MessageDialog("Password is Incorrect");
                await dialog2.ShowAsync();
                PasswordCheck = "";
            }


        }
        private async void OnResetTracker()
        {
            string json = "";
            Objlogin = new Login();
            ObservableCollection<Login> LoginColl = new ObservableCollection<Login>();
            Objlogin.StoreId = ApplicationData.Store_Id;
            Objlogin.Username = ApplicationData.Username;
            Objlogin.Password = ApplicationData.Password;
            Objlogin.StoreAddress = ApplicationData.SelectedState;

            json = Newtonsoft.Json.JsonConvert.SerializeObject(Objlogin);

            var responce = client.PostAsync("api/Login/NewGetEmpRecords", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (responce.IsSuccessStatusCode)
            {
                var v = responce.Content.ReadAsStringAsync().Result;
                LoginColl = JsonConvert.DeserializeObject<ObservableCollection<Login>>(v);

                var i = LoginColl.Where(x => x.StoreAddress == ApplicationData.SelectedState && x.EmployeeId == ApplicationData.Emp_Id).ToList().FirstOrDefault();

                if (i != null)
                {
                    if (i.IsManager == true)
                    {
                        PasswordCheck = "";
                        IsResetPasswordConfirmShow = true;
                        IsHitTestVisible = false;
                    }

                    else if (i.IsManager == false)
                    {
                        var dialog = new MessageDialog("Only Manager can Reset the tracker");
                        await dialog.ShowAsync();
                    }
                }
            }
        }

        private async void OnCheckPwd()
        {
            if (PasswordCheck == null || PasswordCheck == "")
            {
                var dialog = new MessageDialog("Please enter password");
                await dialog.ShowAsync();
            }
            else
            {
                Login tempObj = new Login();
                string json = "";
                tempObj.StoreAddress = ApplicationData.SelectedState;
                tempObj.StoreId = ApplicationData.Store_Id;
                tempObj.EmployeeId = ApplicationData.Emp_Id;
                tempObj.Password = PasswordCheck;

                json = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj);
                var response = client.PostAsync("api/Login/RemoveShift", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Are you sure you want to remove previous shift");
                    dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                    dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });

                    var res = await dialog.ShowAsync();

                    if ((int)res.Id == 0)
                    {
                        Login tempObj1 = new Login();
                        string json1 = "";

                        tempObj1.StoreId = ApplicationData.Store_Id;
                        tempObj1.StoreAddress = ApplicationData.SelectedState;
                        tempObj1.EmployeeId = ApplicationData.Emp_Id;

                        json1 = Newtonsoft.Json.JsonConvert.SerializeObject(tempObj1);
                        var response1 = client.PostAsync("api/Login/RemoveShift", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            var dialog1 = new MessageDialog("Shift removed successfully");
                            await dialog1.ShowAsync();
                            PasswordCheck = "";
                            IsPasswordConfirmShow = false;
                            IsHitTestVisible = true;
                            LoadLotteryCollection();
                            GetReceiveBoxCount();
                            OnReceiveHistory();
                            GetActivedBoxCount();
                            OnActivateHistory();
                            GetDeactivedBoxCount();
                            OnDeActivateHistory();
                            GetSoldOutBoxCount();
                            OnSoldOutHistory();
                            GetReturnedBoxCount();
                            OnReturnHistory();
                            GetSettledBoxCount();
                            OnSettelementHistory();
                            GetEmptyBoxCount();

                        }
                        else if (response1.ReasonPhrase == "Not Found")
                        {
                            var dialog2 = new MessageDialog("Sorry, No any previous shift ");
                            await dialog2.ShowAsync();
                            PasswordCheck = "";
                            IsPasswordConfirmShow = false;
                            IsHitTestVisible = true;
                        }

                    }
                    else if ((int)res.Id == 1)
                    {
                        PasswordCheck = "";
                        IsPasswordConfirmShow = false;
                        IsHitTestVisible = true;
                    }

                }
                else if (response.ReasonPhrase == "Not Found")
                {
                    var dialog = new MessageDialog("Password is incorrect");
                    await dialog.ShowAsync();
                    PasswordCheck = "";
                }
            }



        }

        private void OnRemoveShift()
        {
            IsPasswordConfirmShow = true;
            IsHitTestVisible = false;
        }

        private async void OnSaveChangeBox()
        {
            //   HistoryColl = new ObservableCollection<Activation_Box>();

            if (ChangeFromBox == null || ChangeFromBox == "")
            {
                var dialog = new MessageDialog("Enter From Box number");
                await dialog.ShowAsync();
            }
            else if (ChangeToBox == null || ChangeToBox == "")
            {
                var dialog = new MessageDialog("Enter To Box number");
                await dialog.ShowAsync();
            }
            else if (!regex.IsMatch(ChangeFromBox) || !regex.IsMatch(ChangeToBox))
            {
                var dialog = new MessageDialog("Box number must be numeric");
                await dialog.ShowAsync();
            }
            else
            {
                string json = "";
                ActivationBoxObj = new Activation_Box();
                //Activation_Box temp = new Activation_Box();

                Activate_Ticket temp = new Activate_Ticket();
                temp.Store_Id = ApplicationData.Store_Id;
                temp.Box_No = Convert.ToInt32(ChangeFromBox);
                temp.ChangeToBox = Convert.ToInt32(ChangeToBox);
                json = Newtonsoft.Json.JsonConvert.SerializeObject(temp);
                var response = client.PostAsync("api/CloseBox/ChangeBox", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Box has changed successfully");
                    await dialog.ShowAsync();
                    LoadLotteryCollection();
                    ChangeFromBox = "";
                    ChangeToBox = "";
                }
                if (response.ReasonPhrase == "Not Acceptable")
                {
                    var dialog = new MessageDialog("Box No  " + changeToBox + "  is not Available");
                    await dialog.ShowAsync();
                    ChangeFromBox = "";
                    ChangeToBox = "";
                }
                if (response.ReasonPhrase == "Not Found")
                {
                    var dialog = new MessageDialog("You can not change box to  " + ChangeToBox);
                    await dialog.ShowAsync();
                    ChangeFromBox = "";
                    ChangeToBox = "";
                }
                if (response.ReasonPhrase == "Conflict")
                {
                    var dialog = new MessageDialog("Box No  " + ChangeFromBox + "  is empty");
                    await dialog.ShowAsync();
                    ChangeFromBox = "";
                    ChangeToBox = "";
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    var dialog = new MessageDialog("Box No  " + ChangeFromBox + "  is not active");
                    await dialog.ShowAsync();
                    ChangeFromBox = "";
                    ChangeToBox = "";
                }
            }

        }

        private void OnChangeBox()
        {
            IsChangeBoxPopup = true;
            IsHitTestVisible = false;
            ChangeFromBox = "";
            ChangeToBox = "";
        }

        private void OnGeneralSetting()
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
                var res = responce.Content.ReadAsStringAsync().Result;
                var coll = JsonConvert.DeserializeObject<ObservableCollection<Store_Info>>(res);
                var e = coll.Where(j => j.StoreID == ApplicationData.Store_Id).FirstOrDefault();
                Store_Details.SettlementDays = e.SettlementDays;
                Store_Details.NoOfBoxes = e.NoOfBoxes;
                IsAutoSettle = e.AutoSettle;
                OpenTime = e.OpenTime;
                CloseTime = e.CloseTime;
            }
            IsStoreSettingsPopup = true;
            IsHitTestVisible = false;
        }

        private async void OnEditEmail()
        {
            string json = "";
            Store_Info LoginObj = new Store_Info();
            LoginObj.StoreID = ApplicationData.Store_Id;
            LoginObj.EmployeeId = ApplicationData.Emp_Id;
            LoginObj.EmailId1 = AddEmailId;
            LoginObj.Index = Index;

            json = Newtonsoft.Json.JsonConvert.SerializeObject(LoginObj);

            var response = client.PostAsync("api/Login/ChangePassword", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
            {
                var dialog = new MessageDialog("Email Id updated successfully");
                await dialog.ShowAsync();
                IsEditEmailPopup = false;
                AddEmailId = null;
                Index = 0;
                IsHitTestVisible = true;
                OnNotification();
            }
        }

        public void OnNotification()
        {
            StoreCollection = new ObservableCollection<Store_Info>();
            ObservableCollection<Store_Info> tempEmailcoll = new ObservableCollection<Store_Info>();
            StoreObj = new Store_Info();
            StoreObj.EmployeeId = ApplicationData.Emp_Id;
            StoreObj.StoreName = ApplicationData.StoreName;
            StoreObj.StoreAddress = ApplicationData.SelectedState;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(StoreObj);
            var response = client.PostAsync("api/StoreSetting/NewGetStoreHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                tempEmailcoll = JsonConvert.DeserializeObject<ObservableCollection<Store_Info>>(res);
                var e = tempEmailcoll.Where(k => k.StoreID == ApplicationData.Store_Id).ToList().FirstOrDefault();
                int count = 1;
                int temp = 1;
                var j = tempEmailcoll.Where(x => x.StoreID == ApplicationData.Store_Id).ToList().FirstOrDefault();
                while (count <= 3)
                {
                    if (count == 1)
                    {
                        if (j.EmailId1 != null)
                        {
                            StoreCollection.Add(new Store_Info
                            {
                                EmailId1 = j.EmailId1,
                                IsEmail_On_Off = Convert.ToBoolean(e.Email1_On_Off),
                                Index = temp
                            });
                            temp = temp + 1;
                        }
                    }
                    else if (count == 2)
                    {
                        if (j.EmailId2 != null)
                        {
                            StoreCollection.Add(new Store_Info
                            {
                                EmailId1 = j.EmailId2,
                                IsEmail_On_Off = Convert.ToBoolean(e.Email2_On_Off),
                                Index = temp
                            });
                            temp = temp + 1;
                        }
                    }
                    else if (count == 3)
                    {
                        if (j.EmailId3 != null)
                        {
                            StoreCollection.Add(new Store_Info
                            {
                                EmailId1 = j.EmailId3,
                                IsEmail_On_Off = Convert.ToBoolean(e.Email3_On_Off),
                                Index = temp
                            });
                            temp = temp + 1;
                        }
                    }
                    count = count + 1;

                }
            }
        }

        private async void OnEditUser()
        {
            string json = "";
            Emp_Details_Obj = new Employee_Details();
            double n;
            var Result = double.TryParse(PhoneNo, out n);
            if (IsEditUserPopup == true)
            {
                if (Name == null || PhoneNo == null || EmailId == null || EmailId == "" ||
                     Name == "" || PhoneNo == "" || (IsManagerChecked == false && IsEmployeeChecked == false))

                {
                    var dialog = new MessageDialog("Please Fill All Details.");
                    await dialog.ShowAsync();
                }
                else if (Result == false)
                {
                    var dialog = new MessageDialog("Phone number must be numeric");
                    await dialog.ShowAsync();
                }
                else
                {
                    Emp_Details_Obj.Address = ApplicationData.SelectedState;
                    Emp_Details_Obj.StoreId = ApplicationData.Store_Id;
                    Emp_Details_Obj.Username = Username;
                    Emp_Details_Obj.EmailId = EmailId;
                    Emp_Details_Obj.Contactno = PhoneNo;
                    Emp_Details_Obj.Name = Name;
                    Emp_Details_Obj.IsManager = IsManagerChecked;
                    Emp_Details_Obj.IsEmployee = IsEmployeeChecked;
                    Emp_Details_Obj.IsAssignStore = IsAssignThisStore;

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(Emp_Details_Obj);
                    var response = client.PostAsync("api/Login/Employee_Registration", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var dialog = new MessageDialog("User Updated Successfully");
                        await dialog.ShowAsync();
                        Name = "";
                        PhoneNo = "";
                        Username = "";
                        EmailId = "";
                        IsManagerChecked = false;
                        IsEmployeeChecked = false;
                        IsAssignThisStore = false;
                        IsEditUserPopup = false;
                        IsHitTestVisible = true;
                        OnUsersMgmt();
                    }
                }
            }
        }

        public void OnUsersMgmt()
        {
            Objlogin = new Login();
            UserColl = new ObservableCollection<Login>();
            string json = "";
            Objlogin.EmployeeId = ApplicationData.Emp_Id;
            Objlogin.StoreId = ApplicationData.Store_Id;
            // Objlogin.Index = UserIndex;
            string temp = "";
            string temp1 = "";
            UserIndex = 0;
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
                        if (count < coll.Count)
                        {
                            var i = coll.ElementAt(UserIndex);
                            temp = "";
                            temp1 = "";

                            if (i.IsManager == true && i.IsEmployee == true)
                            {
                                temp = "Manager";
                                //IsBothManagerShow = Visibility.Visible;
                                temp1 = "Employee";
                                //IsBothEmployeeShow = Visibility.Visible;
                                Both = Visibility.Visible;
                                OnlyOne = Visibility.Collapsed;
                            }

                            else if (i.IsManager == true)
                            {
                                temp = "Manager";


                                Both = Visibility.Collapsed;
                                OnlyOne = Visibility.Visible;
                            }

                            else if (i.IsEmployee == true)
                            {
                                temp1 = "Employee";

                                Both = Visibility.Collapsed;
                                OnlyOne = Visibility.Visible;
                            }


                            UserIndex = UserIndex + 1;
                            UserColl.Add(new Login
                            {
                                EmployeeId = UserIndex,
                                Name = i.Name,
                                EmailId = i.EmailId,
                                Contactno = i.Contactno,
                                Username = i.Username,
                                Manager = temp,
                                Employee = temp1
                            });

                        }
                        count = count + 1;
                    }
                }

                IsVisibleBtPrevious = Visibility.Collapsed;

                if (coll.Count == UserIndex)
                {
                    IsVisibleBtNext = Visibility.Collapsed;
                }
                else
                {
                    IsVisibleBtNext = Visibility.Visible;
                }

            }
        }

        private async void OnChangePwd()
        {
            string json = "";
            ObservableCollection<Login> tempColl = new ObservableCollection<Login>();
            Login LoginObj = new Login();

            if (OldPassword == null || OldPassword == "" || NewPassword == null || NewPassword == "" || ConfirmPassword == null ||
                ConfirmPassword == "")
            {
                var dialog = new MessageDialog("Please fill all details");
                await dialog.ShowAsync();
            }
            else if (NewPassword != ConfirmPassword)
            {
                var dialog = new MessageDialog("Please enter correct confirm password");
                await dialog.ShowAsync();
                ConfirmPassword = "";
            }
            else if (OldPassword == NewPassword)
            {
                var dialog = new MessageDialog("Old password and New password can not be same");
                await dialog.ShowAsync();
            }
            else
            {
                LoginObj.StoreId = ApplicationData.Store_Id;
                LoginObj.EmployeeId = ApplicationData.Emp_Id;
                LoginObj.Password = OldPassword;
                LoginObj.NewPassword = NewPassword;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(LoginObj);

                var response = client.PostAsync("api/Login/ChangePassword", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Password is changed");
                    await dialog.ShowAsync();
                    OldPassword = "";
                    NewPassword = "";
                    ConfirmPassword = "";
                }
                else if (response.ReasonPhrase == "Not Found")
                {
                    var dialog = new MessageDialog("Password is incorrect");
                    await dialog.ShowAsync();
                    OldPassword = "";
                    NewPassword = "";
                    ConfirmPassword = "";
                }
            }

        }

        private async void OnAddEmail()
        {

            //if (AddEmailId != null)
            //{
            //    EmailAddressAttribute e = new EmailAddressAttribute();
            //    if (e.IsValid(AddEmailId))
            //    {
            //        var dialog = new MessageDialog("Right");
            //        await dialog.ShowAsync();
            //    }   
            //    else
            //    {
            //        var dialog = new MessageDialog("Wrong");
            //        await dialog.ShowAsync();
            //    }
                    
            //}


            if (AddEmailId == null || AddEmailId == ""||!regex1.IsMatch(AddEmailId))
            {
                if(AddEmailId == null || AddEmailId == "")
                {
                    var dialog = new MessageDialog("Please enter Email Id");
                    await dialog.ShowAsync();
                }
                else if(!regex1.IsMatch(AddEmailId))
                {
                    var dialog = new MessageDialog("Please enter Email Id in valid format");
                    await dialog.ShowAsync();
                }
              
            }
            else
            {
                string json = "";
                Store_Info LoginObj = new Store_Info();
                LoginObj.StoreID = ApplicationData.Store_Id;
                LoginObj.EmployeeId = ApplicationData.Emp_Id;
                LoginObj.EmailId1 = AddEmailId;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(LoginObj);

                var response = client.PostAsync("api/Login/ChangePassword", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Email Id added successfully");
                    await dialog.ShowAsync();
                    IsAddEmailPopup = false;
                    AddEmailId = null;
                    IsHitTestVisible = true;
                    OnNotification();
                }

            }
        }

        public void OnTBShiftReport()
        {
            IsReportPopup = false;
            ShiftObj = new Shift_Details();
            ShiftReport = new ObservableCollection<Shift_Details>();
            MainShiftReportColl = new ObservableCollection<Main_Shift_Collection>();
            ShiftObj.StoreId = ApplicationData.Store_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ShiftObj);
            var response = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {

                var w = response.Content.ReadAsStringAsync().Result;
                var list = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(w);
                Temp1Coll = list;

                ObservableCollection<DateTime> tempdate = new ObservableCollection<DateTime>();

                // DateTime temp = System.DateTime.Now.AddDays(-7);
                foreach (var i in list)
                {
                    if (i.Date == System.DateTime.Now.Date)
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
                    IsShiftRecordNotFound = Visibility.Visible;
                    IsShiftRecordFound = Visibility.Collapsed;
                }
                else
                {
                    IsShiftRecordFound = Visibility.Visible;
                    IsShiftRecordNotFound = Visibility.Collapsed;
                }

                // var list = ShiftCollection.ToList();

                foreach (var i in tempdate)
                {

                    MainShiftReportColl.Add(new Main_Shift_Collection
                    {
                        Date = i.Date,
                        ShiftReport = GetShiftDetails(i.Date),
                        GetDate = i.Date.ToString("MMM dd, yyyy"),

                    });

                }

            }
        }

        public ObservableCollection<Shift_Details> GetShiftDetails(DateTime date)
        {
            TempColl = new ObservableCollection<Shift_Details>();
            ObservableCollection<Login> empcoll = new ObservableCollection<Login>();
            int count = 1;
            HttpResponseMessage empdet = client.GetAsync("api/Login/GetEmployeeDetails").Result;
            var emp = empdet.Content.ReadAsStringAsync().Result;
            empcoll = JsonConvert.DeserializeObject<ObservableCollection<Login>>(emp);

            var v = Temp1Coll.Where(x => x.Date == date).ToList();
            foreach (var j in v)
            {
                var c = empcoll.Where(x => x.EmployeeId == j.EmployeeId).FirstOrDefault();

                if (j.IsClose == false)
                {
                    TempColl.Add(new Shift_Details
                    {
                        ShiftId = count,
                        Date = j.Date,
                        EmployeeId = c.EmployeeId,
                        Empname = c.Username
                    });
                    count = count + 1;
                }
                else if (j.IsClose != false)
                {
                    TempColl.Add(new Shift_Details
                    {
                        ShiftId = count,
                        EndTime = j.EndTime,
                        Date = j.Date,
                        CloseDate = j.CloseDate,
                        EmployeeId = c.EmployeeId,
                        Empname = c.Username
                    });
                    count = count + 1;
                }

            }

            return TempColl;
        }


        /// <summary>
        /// 
        /// </summary>
        public void OnTBDailyReport()
        {
            TerminalDailyReport = new ObservableCollection<Terminal_Details>();
            TerminalObj = new Terminal_Details();
            ShiftObj = new Shift_Details();
            MainDailyReportColl = new ObservableCollection<Terminal_Details>();
            DailyReport = new ObservableCollection<Terminal_Details>();
            ObservableCollection<DateTime> tempdate = new ObservableCollection<DateTime>();
            ObservableCollection<Terminal_Details> tempcoll = new ObservableCollection<Terminal_Details>();
            ObservableCollection<Shift_Details> tempshift = new ObservableCollection<Shift_Details>();
            TotScratchsell = 0;
            TotScratchPayout = 0;
            TotOnlineSells = 0;
            TotOnlinePayout = 0;
            TotTrackedAmount = 0;
            TotCashOnHand = 0;
            TotDateScratchsells = 0;
            TotDateOnlineSells = 0;
            TotDateScratchPayout = 0;
            TotDateOnlinePayout = 0;
            TotDateTrackedAmount = 0;
            TotDateCashOnHand = 0;
            TerminalObj.Store_Id = ApplicationData.Store_Id;
            TerminalObj.EmployeeID = ApplicationData.Emp_Id;

            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(TerminalObj);
            var response = client.PostAsync("api/CloseShift/NewGetTerminalDataRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                TerminalDailyReport = JsonConvert.DeserializeObject<ObservableCollection<Terminal_Details>>(res);


                ShiftObj.StoreId = ApplicationData.Store_Id;
                ShiftObj.EmployeeId = ApplicationData.Emp_Id;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(ShiftObj);
                var get = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (get.IsSuccessStatusCode)
                {
                    var w = get.Content.ReadAsStringAsync().Result;
                    tempshift = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(w);
                }

                var g = tempshift.LastOrDefault();
                if (TerminalDailyReport != null)
                {
                    foreach (var i in TerminalDailyReport)
                    {
                        if (tempshift != null)
                        {
                            var e = tempshift.Where(j => j.Date == System.DateTime.Today && g.IsReportGenerated == true).ToList();
                            if (e.Count != 0)
                            {
                                if (tempdate.Count > 0)
                                {
                                    var v = tempdate.Where(x => x.Date == i.Date).ToList();

                                    if (v.Count == 0)

                                        tempdate.Add(i.Date);
                                }
                                else
                                {

                                    tempdate.Add(i.Date);
                                }
                            }
                        }

                    }
                }

                if (tempdate.Count == 0)
                {
                    IsDailyRecordNotFound = Visibility.Visible;
                    IsDailyRecordFound = Visibility.Collapsed;
                }
                else
                {
                    IsDailyRecordNotFound = Visibility.Collapsed;
                    IsDailyRecordFound = Visibility.Visible;
                }
                foreach (var j in tempdate)
                {
                    var v = TerminalDailyReport.Where(x => x.Date == j.Date).ToList();
                    foreach (var i in v)
                    {
                        t1 = i.Date;
                        int a1 = Convert.ToInt32(i.ScratchSells) + Convert.ToInt32(i.OnlineSells) - Convert.ToInt32(i.OnlinePayout) - Convert.ToInt32(i.ScratchPayout);

                        TotDateScratchsells = TotDateScratchsells + Convert.ToInt32(i.ScratchSells);
                        TotDateOnlineSells = TotDateOnlineSells + Convert.ToInt32(i.OnlineSells);
                        TotDateScratchPayout = TotDateScratchPayout + Convert.ToInt32(i.ScratchPayout);
                        TotDateOnlinePayout = TotDateOnlinePayout + Convert.ToInt32(i.OnlinePayout);
                        TotDateTrackedAmount = TotDateTrackedAmount + a1;
                        TotDateCashOnHand = TotDateCashOnHand + Convert.ToInt32(i.CashOnHand);
                    }

                    MainDailyReportColl.Add(new Terminal_Details
                    {
                        Date = t1,
                        Day = t1.DayOfWeek,
                        ScratchSells = TotDateScratchsells.ToString(),
                        ScratchPayout = TotDateScratchPayout.ToString(),
                        OnlineSells = TotDateOnlineSells.ToString(),
                        OnlinePayout = TotDateOnlinePayout.ToString(),
                        TrackedAmount = TotDateTrackedAmount.ToString(),
                        CashOnHand = TotDateCashOnHand.ToString()
                    });

                    TotScratchsell = TotScratchsell + TotDateScratchsells;
                    TotScratchPayout = TotScratchPayout + TotDateScratchPayout;
                    TotOnlineSells = TotOnlineSells + TotDateOnlineSells;
                    TotOnlinePayout = TotOnlinePayout + TotDateOnlinePayout;
                    TotTrackedAmount = TotTrackedAmount + TotDateTrackedAmount;
                    TotCashOnHand = TotCashOnHand + TotDateCashOnHand;


                }

            }

        }
        public void LoadStoreCollection()
        {
            StoreCollection = new ObservableCollection<Store_Info>();
            StoreObj = new Store_Info();
            StoreObj.EmployeeId = ApplicationData.Emp_Id;
            StoreObj.StoreName = ApplicationData.StoreName;
            StoreObj.StoreAddress = ApplicationData.SelectedState;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(StoreObj);
            var response = client.PostAsync("api/StoreSetting/NewGetStoreHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {

                var v = response.Content.ReadAsStringAsync().Result;
                StoreCollection = JsonConvert.DeserializeObject<ObservableCollection<Store_Info>>(v);
                //GetBarcodeFormat = StoreCollection.FirstOrDefault();
            }
        }


        #endregion


        
        
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
        public void OnTbUser()
        {


            //IsUserPopup = true;
            //IsHitTestVisible = false;
        }
        public void OnTbDashboard()
        {
            Dailytemp = 0;
            IsCloseshitfPopup = false;
            IsReportPopup = false;
            IsVisibleMainPage = Visibility.Visible;
        }
        public async void OnShowDailyReport()
        {

            IsDataTerminalPopup = false;
            if (Temp == 1)
            {
                IsHitTestVisiblePopup = false;
               
                string json = "";
                SoldOutObj = new SoldOut_Details();
                SoldOutObj.Store_Id = ApplicationData.Store_Id;
                SoldOutObj.EmployeeID = ApplicationData.Emp_Id;
                SoldOutObj.Created_Date = System.DateTime.Now;
                SoldOutObj.CloseTime = System.DateTime.Now.ToString();
                json = Newtonsoft.Json.JsonConvert.SerializeObject(SoldOutObj);
                var response = client.PostAsync("api/Sendmail/NewDailyReport_Mail", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Daily Shift Report Send");
                    await dialog.ShowAsync();
                }
                GetDailyReport();
                IsDailyReportPopup = true;

            }
            else
            {
                var dialog = new MessageDialog("First generate Last shift report");
                await dialog.ShowAsync();
            }

        }
        //It returns state collection
        public void getState()
       {
            StateColl = new ObservableCollection<StateClass>();
            StateColl.Add(new StateClass { Name = "California" });
            StateColl.Add(new StateClass { Name = "Florida" });
            StateColl.Add(new StateClass { Name = "Georgia" });
            StateColl.Add(new StateClass { Name = "Hawaii" });
            StateColl.Add(new StateClass { Name = "Texas" });
            StateColl.Add(new StateClass { Name = "Alaska" });
            StateColl.Add(new StateClass { Name = "Alabama" });
            StateColl.Add(new StateClass { Name = "New Jersey" });
            StateColl.Add(new StateClass { Name = "Ohio" });

        }
        public void GetReport()
        {
            //int? Total;
            IsShiftReportHamburger = Visibility.Collapsed;
            IsShiftReportDetail = Visibility.Visible;
            IsDataFromLotteryAppHamburger = Visibility.Collapsed;
            IsDataFromLotteryApp = Visibility.Visible;
            IsDataFromTerminalHamburger = Visibility.Collapsed;
            IsDataFromTerminal = Visibility.Visible;
            IsActiveAndStockInfoHamburger = Visibility.Collapsed;
            IsActiveAndStockInfo = Visibility.Visible;
            IsUserShiftReport = Visibility.Visible;
            IsUserShiftReportHamburger = Visibility.Collapsed;
            IsDateShiftReport = Visibility.Visible;
            IsDateshiftReportHamburger = Visibility.Collapsed;
            IsCloseTimeShiftReport = Visibility.Visible;
            IsCloseTimeShiftReportHamburger = Visibility.Collapsed;
            IsTotalStockActiveInventory = Visibility.Visible;
            IsTotalStockActiveInventoryHamburger = Visibility.Collapsed;
            CountActiveReceive = CountActiveBox + CountReceiveBox;
            ObservableCollection<Activation_Box> tempCollection = new ObservableCollection<Activation_Box>();
            SoldOutObj = new SoldOut_Details();
            SoldOutObj.EmployeeID = ApplicationData.Emp_Id;
            SoldOutObj.Store_Id = ApplicationData.Store_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(SoldOutObj);
            HistoryColl = new ObservableCollection<Activation_Box>();
            Soldhistorycoll = new ObservableCollection<SoldoutHistory>();
            ActivatehistoryColl = new ObservableCollection<Activation_Box>();
            ReturnhistoryColl = new ObservableCollection<Activation_Box>();
            //HttpResponseMessage response = client.GetAsync("api/CloseShift/GetSoldOutHistory").Result;
            var response = client.PostAsync("api/CloseShift/NewGetSoldOutRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var w = response.Content.ReadAsStringAsync().Result;
                SoldOutReportHistory = JsonConvert.DeserializeObject<ObservableCollection<SoldoutHistory>>(w);
                var list = SoldOutReportHistory.ToList();
                foreach (var i in list)
                {
                    Soldhistorycoll.Add(new SoldoutHistory
                    {
                        Game_Id = i.Game_Id,
                        Box_No = i.Box_No,
                        Packet_No = i.Packet_No,
                        Ticket_Name = i.Ticket_Name,
                        Price = i.Price,
                        Created_Date = i.Created_Date,
                        Start_No = i.Start_No,
                        End_No = i.End_No,
                        No_of_Tickets_Sold = i.No_of_Tickets_Sold,
                        Total_Price = i.Total_Price
                    });

                }
            }

            var response1 = client.PostAsync("api/CloseShift/NewGetShiftActivateRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response1.IsSuccessStatusCode)
            {
                var w = response1.Content.ReadAsStringAsync().Result;
                tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
                var list = tempCollection.ToList();
                foreach (var i in list)
                {
                    ActivatehistoryColl.Add(new Activation_Box
                    {
                        Game_Id = i.Game_Id,
                        Box_No = i.Box_No,
                        Packet_No = i.Packet_No,
                        Ticket_Name = i.Ticket_Name,
                        Price = i.Price,
                        Created_Date = i.Created_Date,
                        Start_No = i.Start_No,
                        End_No = i.End_No,
                        Count = i.Count,
                        Total_Price = i.Total_Price
                    });
                }
            }

            var response2 = client.PostAsync("api/CloseShift/NewGetReturnRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response2.IsSuccessStatusCode)
            {
                var w = response2.Content.ReadAsStringAsync().Result;
                ReturnhistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
            }

            var response3 = client.PostAsync("api/CloseShift/NewGetAllRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response3.IsSuccessStatusCode)
            {
                var w = response3.Content.ReadAsStringAsync().Result;
                HistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
                Total = HistoryColl.Sum(x => x.Total_Price);

                TerminalObj.ScratchSells = Total.ToString();
                TerminalObj.Store_Id = ApplicationData.Store_Id;
                TerminalObj.EmployeeID = ApplicationData.Emp_Id;
                string json1 = "";
                json1 = Newtonsoft.Json.JsonConvert.SerializeObject(TerminalObj);
                var response4 = client.PostAsync("api/TerminalDetails/Save_TeminalData", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

            }

            var active = client.PostAsync("api/Activate/NewGetActivateTicketTotalPrice", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            // HttpResponseMessage receive = client.GetAsync("api/Receive/NewGetActivateTicketTotalPrice").Result;
            if (active.IsSuccessStatusCode)
            {
                var rec = active.Content.ReadAsStringAsync().Result;
                if (rec != "null")
                {
                    ActiveTicketTotal = JsonConvert.DeserializeObject<ObservableCollection<Activate_Ticket>>(rec);
                    var abc = ActiveTicketTotal.Select(i => i.Total_Price).Sum();
                    ActiveTotalPrice = abc;
                }
                else
                {
                    ActiveTotalPrice = 0;
                }
            }

            OnTerminalDataCollection();
        }
        public void GetDailyReport()
        {
            CountActiveReceive = CountActiveBox + CountReceiveBox;
            SoldOutObj = new SoldOut_Details();
            IsDailyReportUserName = Visibility.Visible;
            IsDailyReportHamburgerName = Visibility.Collapsed;
            IsDailyReportDateHamburger = Visibility.Collapsed;
            IsDailyReportDate = Visibility.Visible;
            IsDailyReportTimeHamburger = Visibility.Collapsed;
            IsDailyReportTime = Visibility.Visible;

            if (HamburgerSelectedData != null)
            {
                SoldOutObj.Store_Id = ApplicationData.Store_Id;
                SoldOutObj.Created_Date = t1;
                IsDailyReportUserName = Visibility.Collapsed;
                IsDailyReportHamburgerName = Visibility.Visible;
                IsDailyReportDateHamburger = Visibility.Visible;
                IsDailyReportDate = Visibility.Collapsed;
                IsDailyReportTimeHamburger = Visibility.Visible;
                IsDailyReportTime = Visibility.Collapsed;
            }
            else
            {
                SoldOutObj.Store_Id = ApplicationData.Store_Id;
                SoldOutObj.EmployeeID = ApplicationData.Emp_Id;
            }



            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(SoldOutObj);

            HistoryColl = new ObservableCollection<Activation_Box>();
            Soldhistorycoll = new ObservableCollection<SoldoutHistory>();
            ActivatehistoryColl = new ObservableCollection<Activation_Box>();
            DailyReportActiveColl = new ObservableCollection<Activation_Box>();
            ReturnhistoryColl = new ObservableCollection<Activation_Box>();
            //HttpResponseMessage response = client.GetAsync("api/CloseShift/GetSoldOutHistory").Result;

            var response = client.PostAsync("api/CloseShift/NewGetDailySoldOutRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var w = response.Content.ReadAsStringAsync().Result;
                Soldhistorycoll = JsonConvert.DeserializeObject<ObservableCollection<SoldoutHistory>>(w);
            }

            HttpResponseMessage response1 = client.PostAsync("api/CloseShift/NewGetDailyActivateRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response1.IsSuccessStatusCode)
            {
                var w = response1.Content.ReadAsStringAsync().Result;
                ActivatehistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
                var abc = ActivatehistoryColl.Where(x => x.Created_Date == System.DateTime.Today).ToList();
                foreach (var i in abc)
                {
                    DailyReportActiveColl.Add(new Activation_Box
                    {
                        Game_Id = i.Game_Id,
                        Box_No = i.Box_No,
                        Created_Date = i.Created_Date,
                        Packet_No = i.Packet_No,
                        Ticket_Name = i.Ticket_Name,
                        Price = i.Price,
                        Start_No = i.Start_No,
                        End_No = i.End_No,
                        Status = i.Status,
                        EmployeeId = i.EmployeeId,
                        Stopped_At = i.Stopped_At,
                        State = i.State,
                        Count = i.Count,
                        Total_Price = i.Price * Convert.ToInt32(i.Count)
                    });
                }
                OnTerminalDataCollection();
            }

            var response2 = client.PostAsync("api/Return/NewGetDailyReturnHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response2.IsSuccessStatusCode)
            {
                var w = response2.Content.ReadAsStringAsync().Result;
                ReturnhistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
            }

            var response3 = client.PostAsync("api/CloseShift/NewGetDailyReportRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response3.IsSuccessStatusCode)
            {
                var w = response3.Content.ReadAsStringAsync().Result;
                HistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
                Total = HistoryColl.Sum(x => x.Total_Price);
            }
        }
        public void getHamburgerShiftReport()
        {
            ObservableCollection<Activation_Box> tempCollection = new ObservableCollection<Activation_Box>();
            ObservableCollection<Login> empcoll = new ObservableCollection<Login>();
            TerminalDataCollection = new ObservableCollection<Terminal_Details>();
            SoldOutObj = new SoldOut_Details();
            HttpResponseMessage empdet = client.GetAsync("api/Login/GetEmployeeDetails").Result;
            var emp = empdet.Content.ReadAsStringAsync().Result;

            empcoll = JsonConvert.DeserializeObject<ObservableCollection<Login>>(emp);

            var c = empcoll.Where(x => x.Username == ShiftReportSelectedData.Empname && x.EmployeeId == ShiftReportSelectedData.EmployeeId).FirstOrDefault();

            if (c != null)
            {
                SoldOutObj.EmployeeID = c.EmployeeId;
                SoldOutObj.Store_Id = ApplicationData.Store_Id;
                SoldOutObj.Created_Date = ShiftReportSelectedData.Date;
                SoldOutObj.ShiftID = Convert.ToInt32(ShiftReportSelectedData.ShiftId);
                SoldOutObj.CloseTime = ShiftReportSelectedData.EndTime;
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(SoldOutObj);

                HistoryColl = new ObservableCollection<Activation_Box>();
                Soldhistorycoll = new ObservableCollection<SoldoutHistory>();
                ActivatehistoryColl = new ObservableCollection<Activation_Box>();
                ReturnhistoryColl = new ObservableCollection<Activation_Box>();

                var response = client.PostAsync("api/CloseShift/NewGetSoldOutRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var w = response.Content.ReadAsStringAsync().Result;
                    SoldOutReportHistory = JsonConvert.DeserializeObject<ObservableCollection<SoldoutHistory>>(w);
                    var list = SoldOutReportHistory.ToList();
                    foreach (var i in list)
                    {
                        Soldhistorycoll.Add(new SoldoutHistory
                        {
                            Game_Id = i.Game_Id,
                            Box_No = i.Box_No,
                            Packet_No = i.Packet_No,
                            Ticket_Name = i.Ticket_Name,
                            Price = i.Price,
                            Created_Date = i.Created_Date,
                            Start_No = i.Start_No,
                            End_No = i.End_No,
                            No_of_Tickets_Sold = i.No_of_Tickets_Sold,
                            Total_Price = i.Total_Price
                        });
                    }
                }

                var response1 = client.PostAsync("api/CloseShift/NewGetShiftActivateRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var w = response1.Content.ReadAsStringAsync().Result;
                    tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
                    var list = tempCollection.ToList();
                    foreach (var i in list)
                    {
                        ActivatehistoryColl.Add(new Activation_Box
                        {
                            Game_Id = i.Game_Id,
                            Box_No = i.Box_No,
                            Packet_No = i.Packet_No,
                            Ticket_Name = i.Ticket_Name,
                            Price = i.Price,
                            Created_Date = i.Created_Date,
                            Start_No = i.Start_No,
                            End_No = i.End_No,
                            Count = i.Count,
                            Total_Price = i.Total_Price
                        });
                    }
                }

                var response2 = client.PostAsync("api/CloseShift/NewGetReturnRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response2.IsSuccessStatusCode)
                {
                    var w = response2.Content.ReadAsStringAsync().Result;
                    ReturnhistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
                }
                var response3 = client.PostAsync("api/CloseShift/NewGetAllRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response3.IsSuccessStatusCode)
                {
                    var w = response3.Content.ReadAsStringAsync().Result;
                    HistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(w);
                    Total = HistoryColl.Sum(x => x.Total_Price);

                    //TerminalObj.ScratchSells = Total.ToString();
                    //TerminalObj.Store_Id = ApplicationData.Store_Id;
                    //TerminalObj.EmployeeID = ApplicationData.Emp_Id;
                    //string json1 = "";
                    //json1 = Newtonsoft.Json.JsonConvert.SerializeObject(TerminalObj);
                    //var response4 = client.PostAsync("api/TerminalDetails/Save_TeminalData", new StringContent(json1, System.Text.Encoding.UTF8, "application/json")).Result;

                }

                var active = client.PostAsync("api/Activate/NewGetActivateTicketTotalPrice", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                // HttpResponseMessage receive = client.GetAsync("api/Receive/NewGetActivateTicketTotalPrice").Result;
                if (active.IsSuccessStatusCode)
                {
                    var rec = active.Content.ReadAsStringAsync().Result;
                    if (rec != "null")
                    {
                        ActiveTicketTotal = JsonConvert.DeserializeObject<ObservableCollection<Activate_Ticket>>(rec);
                        var abc = ActiveTicketTotal.Select(i => i.Total_Price).Sum();
                        ActiveTotalPrice = abc;
                    }
                    else
                    {
                        ActiveTotalPrice = 0;
                    }
                }
            }
        }
        public void GetShiftDetails()
        {
            ShiftCollection = new ObservableCollection<Shift_Details>();

            HttpResponseMessage response = client.GetAsync("api/Login/GetShiftDetails").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                ShiftCollection = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(v);
            }
        }
        public async void OnSaveEmployeeDetails()
        {

            string json = "";
            Emp_Details_Obj = new Employee_Details();
            if (IsAddUserPopup == true)
            {
                if (Name == null || PhoneNo == null || Username == null || Password == null || EmailId == null || EmailId == "" ||
                    Username == "" || Password == "" || Name == "" || PhoneNo == "" || (IsManagerChecked == false && IsEmployeeChecked == false))

                {
                    var dialog = new MessageDialog("Please Fill All Details.");
                    await dialog.ShowAsync();
                }
                else if (!regex.IsMatch(PhoneNo))
                {
                    var dialog = new MessageDialog("Phone number must be numeric");
                    await dialog.ShowAsync();
                }
                else if(!regex1.IsMatch(EmailId))
                {
                    var dialog = new MessageDialog("Please enter Email Id in valid format");
                    await dialog.ShowAsync();
                }
                else
                {
                    Emp_Details_Obj.StoreId = ApplicationData.Store_Id;
                    Emp_Details_Obj.Username = Username;
                    Emp_Details_Obj.Password = Password;
                    Emp_Details_Obj.EmailId = EmailId;
                    Emp_Details_Obj.Contactno = PhoneNo;
                    Emp_Details_Obj.Name = Name;
                    Emp_Details_Obj.IsManager = IsManagerChecked;
                    Emp_Details_Obj.IsEmployee = IsEmployeeChecked;
                    Emp_Details_Obj.IsAssignStore = IsAssignThisStore;
                    Emp_Details_Obj.Address = ApplicationData.SelectedState;

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(Emp_Details_Obj);
                    var response = client.PostAsync("api/Login/Employee_Registration", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var dialog = new MessageDialog("Employee Registration Successfully Done.");
                        await dialog.ShowAsync();
                        Name = "";
                        PhoneNo = "";
                        Username = "";
                        Password = "";
                        EmailId = "";
                        IsManagerChecked = false;
                        IsEmployeeChecked = false;
                        IsAssignThisStore = false;
                        IsAddUserPopup = false;
                        IsHitTestVisible = true;
                        OnUsersMgmt();
                    }
                    else if (response.ReasonPhrase == "Not Found")
                    {
                        var dialog = new MessageDialog("Username already exist");
                        await dialog.ShowAsync();
                        Name = "";
                        PhoneNo = "";
                        Username = "";
                        Password = "";
                        EmailId = "";
                        IsManagerChecked = false;
                        IsEmployeeChecked = false;
                        IsAssignThisStore = false;
                    }
                }
            }
            else
            {
                json = Newtonsoft.Json.JsonConvert.SerializeObject(Emp_Details_Obj);
                if (Emp_Details_Obj.Name == null || Emp_Details_Obj.Contactno == "" || Emp_Details_Obj.Username == "" || Emp_Details_Obj.Password == "" ||
                    Emp_Details_Obj.Dob == null || Emp_Details_Obj.Address == "")
                {
                    var dialog = new MessageDialog("Please Fill All Details.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var response = client.PostAsync("api/Login/Employee_Registration", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var dialog = new MessageDialog("Employee Registration Successfully Done.");
                        await dialog.ShowAsync();
                        Emp_Details_Obj.Name = "";
                        Emp_Details_Obj.Address = "";
                        Emp_Details_Obj.Contactno = "";
                        Emp_Details_Obj.Username = "";
                        // Emp_Details_Obj.Dob = "";
                        Emp_Details_Obj.Password = "";
                        IsSingupPopup = false;

                    }
                }
            }

        }
        public void OnEmployeeRemember()
        {
            string json = "";
            EmployeeHistory = new ObservableCollection<Employee_Details>();
            HttpResponseMessage empdet = client.GetAsync("api/Login/GetEmployeeDetails").Result;
            var emp = empdet.Content.ReadAsStringAsync().Result;
            EmployeeHistory = JsonConvert.DeserializeObject<ObservableCollection<Employee_Details>>(emp);
        }
        public void OnShowSignUpPopup()
        {
            // IsSingupPopup = true;
        }
        public void GetRefreshGrid()
        {
            String SelectedButton = SelectedItem;
            switch (SelectedButton)
            {
                case "Receive":

                    if (SelectedItem == "Receive")
                    {
                        GetReceiveBoxCount();
                        OnReceiveHistory();
                    }
                    break;

                case "Active":

                    if (SelectedItem == "Active")
                    {
                        GetActivedBoxCount();
                        // OnDeActivateHistory();
                        OnActivateHistory();
                    }
                    break;

                case "Deactive":

                    if (SelectedItem == "Deactive")
                    {
                        GetDeactivedBoxCount();
                        OnDeActivateHistory();
                        OnActivateHistory();
                        OnReceiveHistory();
                        OnSoldOutHistory();
                    }
                    break;

                case "Return":

                    if (SelectedItem == "Return")
                    {
                        GetReturnedBoxCount();
                        GetReceiveBoxCount();
                        GetActivedBoxCount();
                        OnActivateHistory();
                        OnReturnHistory();
                        OnReceiveHistory();
                    }
                    break;

                case "Settle":

                    if (SelectedItem == "Settle")
                    {
                        GetSettledBoxCount();
                        OnSettelementHistory();
                        OnActivateHistory();
                        OnSoldOutHistory();
                        OnReceiveHistory();
                    }
                    break;

                case "SoldOut":

                    if (SelectedItem == "SoldOut")
                    {
                        GetSoldOutBoxCount();
                        OnSoldOutHistory();
                        OnActivateHistory();
                        GetSettledBoxCount();
                        OnSettelementHistory();
                    }
                    break;

                case "Empty":

                    if (SelectedItem == "Empty")
                    {
                        GetEmptyBoxCount();

                    }
                    break;

            }
        }
        public void GetRecords()
        {
            if (SelectedData != null)
            {
                Single_Record = LotteryColl.Where<LotteryInfo>(i => i.Packet_No == SelectedData.Packet_No && i.Game_Id == SelectedData.Game_Id).SingleOrDefault();
                if (IsDeactivatePopup == true && Single_Record != null)
                {

                    ActiveBoxObj.Game_Id = Single_Record.Game_Id;
                    ActiveBoxObj.Packet_No = Single_Record.Packet_No;
                    ActiveBoxObj.Ticket_Name = Single_Record.Ticket_Name;
                    ActiveBoxObj.Price = Single_Record.Price;
                    ActiveBoxObj.Box_No = Single_Record.Box_No;
                    ActiveBoxObj.Stopped_At = Single_Record.Stopped_At;
                    //SelectedActived_Box.Box_No = Single_Record.Box_No;
                }
            }
        }
        public void ShowBoxNo()
        {
            if (IsReturnPopup == true && Single_Record != null)
            {
                Return_Obj.Game_Id = Single_Record.Game_Id;
                Return_Obj.Packet_No = Single_Record.Packet_No;
                Return_Obj.Ticket_Name = Single_Record.Ticket_Name;
                Return_Obj.Price = Single_Record.Price;
                Return_Obj.Box_No = Single_Record.Box_No;
                Return_Obj.Start_No = Single_Record.Start_No;
                // Return_Obj.End_No = Single_Record.End_No;
            }
            else if (IsSoldOutPopup == true && Single_Record != null)
            {
                SoldOutObj.Game_Id = Single_Record.Game_Id;
                SoldOutObj.Packet_No = Single_Record.Packet_No;
                SoldOutObj.Ticket_Name = Single_Record.Ticket_Name;
                SoldOutObj.Price = Single_Record.Price;
                SoldOutObj.Box_No = Single_Record.Box_No;
                SoldOutObj.Start_No = Single_Record.Start_No;
                SoldOutObj.End_No = Single_Record.End_No;

            }
            else if (IsSettlePopup == true && Single_Record != null)
            {
                Settle_Obj.Game_Id = Single_Record.Game_Id;
                Settle_Obj.Packet_No = Single_Record.Packet_No;
                Settle_Obj.Ticket_Name = Single_Record.Ticket_Name;
                Settle_Obj.Price = Single_Record.Price;
                Settle_Obj.Box_No = Single_Record.Box_No;
                Settle_Obj.Start_No = Single_Record.Start_No;
                Settle_Obj.End_No = Single_Record.End_No;
            }
            else if (IsBoxClosePopup == true && Single_Record != null)
            {
                ObjCloseBox.Game_Id = Single_Record.Game_Id;
                ObjCloseBox.Packet_Id = Single_Record.Packet_No;
                ObjCloseBox.Ticket_Name = Single_Record.Ticket_Name;
                ObjCloseBox.Price = Single_Record.Price;
                ObjCloseBox.Box_No = Single_Record.Box_No;
                ObjCloseBox.Start_No = Single_Record.Start_No;
                ObjCloseBox.End_No = Single_Record.End_No;
            }
        }
        public void GetBoxRecord(string Content)
        {
            Single_Record = LotteryColl.Where<LotteryInfo>(i => i.Box_No == Convert.ToInt32(Content)).SingleOrDefault();
            ActiveBoxObj.Game_Id = Single_Record.Game_Id;
            ActiveBoxObj.Status = Single_Record.Status;
            ActiveBoxObj.Count = Single_Record.Count;
            ActiveBoxObj.Packet_No = Single_Record.Packet_No;
            ActiveBoxObj.Ticket_Name = Single_Record.Ticket_Name;
            ActiveBoxObj.Price = Single_Record.Price;
            ActiveBoxObj.Start_No = Single_Record.Start_No;
            ActiveBoxObj.End_No = Single_Record.End_No;
            ActiveBoxObj.Box_No = Single_Record.Box_No;
            ActiveBoxObj.Stopped_At = Single_Record.Stopped_At;
        }
        public void CollapseAllDatagrids()
        {
            IsVisibleReceivedDataGrid = Visibility.Collapsed;
            IsVisibleActivateDataGrid = Visibility.Collapsed;
            IsVisibleDeactivateDataGrid = Visibility.Collapsed;
            IsVisibleSoldoutDataGrid = Visibility.Collapsed;
            IsVisibleReturnDataGrid = Visibility.Collapsed;
            IsVisibleSettledDataGrid = Visibility.Collapsed;

        }
        public void InitializeRelayCommands()
        {
            Next = new RelayCommand(OnNext);
            Previous = new RelayCommand(OnPrevious);
            Update_Lottery = new RelayCommand(OnUpdate_Lottery);
            RecieveCommand = new RelayCommand(OnRecieveCommand);
            Update_ActivatePackets = new RelayCommand(OnUpdate_ActivatePackets);
            ActivateCommand = new RelayCommand(OnActivate);
            Activate_TicketCommand = new RelayCommand(OnStatusUpdate);
            Deactivatecommand = new RelayCommand(OnDeactivate);
            Deactivate_Ticket = new RelayCommand(OnDeactivate_Tickets);
            SoldOutCommand = new RelayCommand(OnSoldOut);
            SoldOut_Ticket = new RelayCommand(OnSoldOut_Ticket);
            ReturnCommand = new RelayCommand(OnReturn);
            Return_Ticket = new RelayCommand(OnReturn_Ticket);
            SettleCommand = new RelayCommand(OnSettle);
            DeActivateHistoryCommand = new RelayCommand(OnDeActivateHistory);
            ActivateHistoryCommand = new RelayCommand(OnActivateHistory);
            ReceiveHistoryCommand = new RelayCommand(OnReceiveHistory);
            ReturnHistoryCommand = new RelayCommand(OnReturnHistory);
            SettelementHistoryCommand = new RelayCommand(OnSettelementHistory);
            //DeleteSelectedCommand = new RelayCommand(OnDeleteSelectedRecord);
            Settle_Ticket = new RelayCommand(OnSettle_Ticket);
            SoldOutHistoryCommand = new RelayCommand(OnSoldOutHistory);
            EmptyHistoryCommand = new RelayCommand(OnEmptyHistory);
            CloseBoxCommand = new RelayCommand(OnCloseBox_Command);
            CloseBox = new RelayCommand(OnCloseBox);
            ReopenBox = new RelayCommand(OnReopenBox);
            SaveTerminalData = new RelayCommand(OnSaveTerminalDetails);
            GenerateReport = new RelayCommand(OnGenerateReport);
            OnEmployeeHistory = new RelayCommand(OnEmployeeRemember);
        }

        public async void OnGenerateReport()
            {
            // OnSaveTerminalDetails();
            ValidateTerminalData();
            if (IsValid)
            {
                var dialog = new MessageDialog("Record Save Successfully, Are you sure you want to generate report and send out to recipients?");
                dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                var res = await dialog.ShowAsync();
                if ((int)res.Id == 0)
                {
                    OnSaveTerminalDetails();
                    IsDataTerminalPopup = false;
                    string json = "";
                    SoldOutObj = new SoldOut_Details();
                    SoldOutObj.Store_Id = ApplicationData.Store_Id;
                    SoldOutObj.EmployeeID = ApplicationData.Emp_Id;
                    SoldOutObj.ShiftReportGenerate = true;
                    SoldOutObj.Created_Date = System.DateTime.Now;
                    SoldOutObj.CloseTime = System.DateTime.Now.ToString();
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(SoldOutObj);
                    var response = client.PostAsync("api/Sendmail/Send_Mail", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var dialog1 = new MessageDialog("Shift Report Send");
                        await dialog1.ShowAsync();
                    }
                    GetReport();
                    IsReportPopup = true;
                }

            }
        }

        public async void ValidateTerminalData()
        {
            IsValid = true;
            if (TerminalObj.OnlineSells == null || TerminalObj.OnlinePayout == null || TerminalObj.ScratchPayout == null || TerminalObj.IssuedInventory == null ||
                TerminalObj.InstockInventory == null || TerminalObj.ActiveInventory == null || TerminalObj.Loan == null || TerminalObj.Total == null || TerminalObj.OnlineSells == "" || TerminalObj.OnlinePayout == "" || TerminalObj.ScratchPayout == "" || TerminalObj.IssuedInventory == "" ||
                TerminalObj.InstockInventory == "" || TerminalObj.ActiveInventory == "" || TerminalObj.Loan == "" || TerminalObj.Total == "")
            {
                IsValid = false;
                var dialog = new MessageDialog("Please Fill All Information.");
                await dialog.ShowAsync();
            }
        }
        public void OnSaveTerminalDetails()

        {
            //ValidateTerminalData();
            if (IsValid)
            {
                TerminalObj.Store_Id = ApplicationData.Store_Id;
                TerminalObj.EmployeeID = ApplicationData.Emp_Id;

                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(TerminalObj);
                var response = client.PostAsync("api/TerminalDetails/Save_TeminalData", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            }
        }
        public async void OnReopenBox()
        {
            IsHitTestVisible = true;
            IsHitTestVisiblePopup = true;
            ObjCloseBox.EmployeeID = ApplicationData.Emp_Id;
            ObjCloseBox.State = ApplicationData.SelectedState;
            ObjCloseBox.Store_Id = ApplicationData.Store_Id;
            ObjCloseBox.Game_Id = ActiveBoxObj.Game_Id;
            ObjCloseBox.Packet_Id = ActiveBoxObj.Packet_No;
            ObjCloseBox.Box_No = ActiveBoxObj.Box_No;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjCloseBox);
            var response = client.PostAsync("api/CloseBox/ReopenBox", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
            {
                // OnGetAutoSettled();
                var dialog = new MessageDialog("Box Marked Reopen Successfully..");
                await dialog.ShowAsync();
                IsCloseBoxReopen = false;
                OnActivateHistory();
                GetActivedBoxCount();
                GetEmptyBoxCount();
                LoadLotteryCollection();
                LoadActive_BoxCollection();
            }
            else
            {
                var dialog = new MessageDialog("This PacketId Already Active.");
                await dialog.ShowAsync();
                IsHitTestVisiblePopup = false;
            }
        }
        public async void OnCloseBox()
        {
            IsHitTestVisible = false;
            IsHitTestVisiblePopup = true;
            ObjCloseBox.EmployeeID = ApplicationData.Emp_Id;
            ObjCloseBox.State = ApplicationData.SelectedState;
            ObjCloseBox.Store_Id = ApplicationData.Store_Id;
            if (ObjCloseBox.Close_At == "" || ObjCloseBox.Close_At == null)
            {
                var dialog = new MessageDialog("Please Entered the Close Position.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
                IsHitTestVisiblePopup = false;
            }
            else
            {
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjCloseBox);
                var response = client.PostAsync("api/CloseBox/OnClose_Box", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    // OnGetAutoSettled();
                    var dialog = new MessageDialog("Box Marked Closed Successfully..");
                    await dialog.ShowAsync();
                    IsBoxClosePopup = false;
                    GetRefreshGrid();
                    LoadActive_BoxCollection();
                    GetSoldOutBoxCount();
                    GetSettledBoxCount();
                    GetActivedBoxCount();
                    LoadBoxCollection();
                    LoadLotteryCollection();
                    LoadEmptyBoxes();
                }
                else if (response.ReasonPhrase == "Not Found")
                {
                    var dialog = new MessageDialog("Please Check Total Tickets.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                    IsHitTestVisiblePopup = false;
                }
                else if (response.ReasonPhrase != "OK")
                {
                    var dialog = new MessageDialog("This Box is Already Closed.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                    IsHitTestVisiblePopup = false;
                }
                ObjCloseBox.Close_At = "";
            }
        }
        public void OnCloseBox_Command()
        {
            IsCloseShiftActivateBox = false;
            if (ActiveBoxObj.Box_No != null)
            {
                IsContentChecked = true;
            }
            else
            {
                IsContentChecked = false;
            }
            // LoadActive_BoxCollection();
            IsBoxClosePopup = true;
            ObjCloseBox.Close_At = null;
            if (IsContentChecked == true)
            {
                ShowBoxNo();
            }
            //ShowBoxNo();
        }
        public void OnCloseshitf()
        {
            ObservableCollection<Shift_Details> tempshift = new ObservableCollection<Shift_Details>();

            LoadActive_BoxCollection();

            IsCloseshitfPopup = true;

            //string json = "";
            //ShiftObj = new Shift_Details();
            //ShiftObj.StoreId = ApplicationData.Store_Id;
            //ShiftObj.EmployeeId = ApplicationData.Emp_Id;
            //json = Newtonsoft.Json.JsonConvert.SerializeObject(ShiftObj);
            //var result = client.PostAsync("api/Shift/NewGetShiftDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            //if (result.IsSuccessStatusCode)
            //{
            //    var w = result.Content.ReadAsStringAsync().Result;
            //    tempshift = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(w);
            //}
            //var g = tempshift.LastOrDefault();
            //if(g.IsLastShift == true)
            //{
            //    IsLastShiftChecked = true;
            //}

            IsHitTestVisible = false;
            IsHitTestVisible = false;
            IsVisibleShiftSubmit = Visibility.Visible;
            IsActivatePopup = false;
            IsDeactivatePopup = false;
            IsActivatePopup = false;
            IsReturnPopup = false;
            IsSoldOutPopup = false;
            IsReceiveManuallyPopup = false;
            IsSettlePopup = false;

        }
        public void InitalizeVariable()
        {
      //client.BaseAddress = new Uri("http://63.142.245.165:1519/");
     client.BaseAddress = new Uri("http://localhost:5133/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public void LoadLotteryCollection()
        {
            Single_Record = new LotteryInfo();
            Single_Record.Employee_Id = ApplicationData.Emp_Id;
            Single_Record.Store_Id = ApplicationData.Store_Id;


            LotteryColl = new ObservableCollection<LotteryInfo>();

            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Single_Record);
            var response = client.PostAsync("api/Lottery/NewGetLotteryCollection", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            // HttpResponseMessage response = client.GetAsync("api/Lottery/GetLotteryCollection").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                LotteryColl = JsonConvert.DeserializeObject<ObservableCollection<LotteryInfo>>(v);
            }
            OnReceiveHistory();
            //var x = HistoryColl;
            foreach (var i in HistoryColl)
            {
                if (i.Created_Date < System.DateTime.Today)
                {
                    Settle_Obj = new Settle_Details();
                    Settle_Obj.Game_Id = i.Game_Id;
                    Settle_Obj.Packet_No = i.Packet_No;
                    Settle_Obj.Ticket_Name = i.Ticket_Name;
                    Settle_Obj.Price = i.Price;
                    Settle_Obj.Start_No = i.Start_No;
                    Settle_Obj.End_No = i.End_No;
                    Settle_Obj.Created_Date = i.Created_Date;
                    i.State = ApplicationData.SelectedState;
                    Settle_Obj.State = i.State;

                    TimeSpan totaldays = System.DateTime.Today - i.Created_Date;
                    int Days = totaldays.Days;
                    if (Days >= 90)
                    {
                        //string json = "";
                        Settle_Obj.Store_Id = ApplicationData.Store_Id;
                        Settle_Obj.EmployeeID = ApplicationData.Emp_Id;
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(Settle_Obj);
                        HttpResponseMessage response1 = client.PostAsync("api/Settle/Settle_Ticket", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                        //if (response1.IsSuccessStatusCode)
                        //{
                        //    var dialog = new MessageDialog("Ticket Marked Settle Successfully..");
                        //   await dialog.ShowAsync();
                        //}
                    }
                }
            }

            OnActivateHistory();
            foreach (var i in ActivatehistoryColl)
            {
                if (i.Activation_Date < System.DateTime.Today)
                {
                    Settle_Obj = new Settle_Details();
                    Settle_Obj.Game_Id = i.Game_Id;
                    Settle_Obj.Packet_No = i.Packet_No;
                    Settle_Obj.Ticket_Name = i.Ticket_Name;
                    Settle_Obj.Price = i.Price;
                    Settle_Obj.Start_No = i.Start_No;
                    Settle_Obj.End_No = i.End_No;
                    Settle_Obj.Created_Date = i.Created_Date;
                    Settle_Obj.Store_Id = ApplicationData.Store_Id;
                    Settle_Obj.EmployeeID = ApplicationData.Emp_Id;
                    i.State = ApplicationData.SelectedState;
                    Settle_Obj.State = i.State;
                    Settle_Obj.Box_No = i.Box_No;

                    TimeSpan totaldays = System.DateTime.Today - i.Activation_Date;
                    int Days = totaldays.Days;
                    if (Days >= i.SettlementDays && i.SettlementDays > 0)
                    {
                        //string json = "";
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(Settle_Obj);
                        HttpResponseMessage response1 = client.PostAsync("api/Settle/Settle_Ticket", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            GetRefreshGrid();
                            // IsSettlePopup = false;
                            GetSettledBoxCount();
                            //Settle_Obj = new Settle_Details();
                            //BoxCollection = new ObservableCollection<Activation_Box>();
                            LoadBoxCollection();
                            LoadLotteryCollection();
                            LoadActive_BoxCollection();
                            LoadEmptyBoxes();
                        }

                        //if (response1.IsSuccessStatusCode)
                        //{
                        //    var dialog = new MessageDialog("Ticket Marked Settle Successfully..");
                        //   await dialog.ShowAsync();
                        //}
                    }
                }
            }
        }
        public void GetAutoSearchPackId()
        {
            GetPackId = new ObservableCollection<Receive_Inventory>();
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ReceiveObj);
            HttpResponseMessage response = client.PostAsync("api/Receive/OnlyGetPackId", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                GetPackId = JsonConvert.DeserializeObject<ObservableCollection<Receive_Inventory>>(v);
            }
        }

        #region TopButtonSection
        public void OnRecieveCommand()
        {
            IsHitTestVisible = false;
            IsCloseshitfPopup = false;
            IsReceiveManuallyPopup = true;
            IsActivatePopup = false;
            IsDeactivatePopup = false;
            IsReturnPopup = false;
            IsSoldOutPopup = false;
            IsSettlePopup = false;
            IsVisibleActiveBoxNo = Visibility.Collapsed;
            IsVisibleActiveComboBox = Visibility.Visible;
            //ReceiveObj = null;
        }
        public async void OnUpdate_Lottery()
        {
            //IsHitTestVisible = true;
            SelectedItem = "Receive";
            ValidateRecordToReceive();
            if (IsValid)
            {
                ReceiveObj.EmployeeId = ApplicationData.Emp_Id;
                ReceiveObj.State = ApplicationData.SelectedState;
                ReceiveObj.Store_Id = ApplicationData.Store_Id;
              //  BarcodeFormatDetails();
                int PacketidRange = GetBarcodeFormat.PacketIDTo - GetBarcodeFormat.PacketIDFrom + 1;
                if (ReceiveObj.Packet_No.Length < PacketidRange || ReceiveObj.Packet_No.Length > PacketidRange)
                {
                    var dialog = new MessageDialog("PacketId must be equal to " + PacketidRange + " Digit");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                else
                {
                    string json = "";
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(ReceiveObj);
                    var response = client.PostAsync("api/Receive/InsertInventoryRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var dialog = new MessageDialog("Record Added Successfully..");
                        await dialog.ShowAsync();
                        IsReceiveManuallyPopup = true;
                        GetRefreshGrid();
                        GetEmptyBoxCount();
                        GetReceiveBoxCount();
                        ReceiveObj.Game_Id = "";
                        ReceiveObj.Packet_No = "";
                        ReceiveObj.Ticket_Name = "";
                        ReceiveObj.Rate = "";
                        ReceiveObj.Start_No = "";
                        ReceiveObj.End_No = "";
                        IsHitTestVisible = true;
                    }

                    else if (response.ReasonPhrase == "Conflict")
                    {
                        var dialog = new MessageDialog("PacketId Already Active.");
                        await dialog.ShowAsync();
                        //ReceiveObj = null;
                        ReceiveObj.Game_Id = "";
                        ReceiveObj.Packet_No = "";
                        ReceiveObj.Ticket_Name = "";
                        ReceiveObj.Rate = "";
                        ReceiveObj.Start_No = "";
                        ReceiveObj.End_No = "";
                        IsHitTestVisible = false;
                    }

                    else
                    {
                        var dialog = new MessageDialog("Packet Already Exist.");
                        await dialog.ShowAsync();
                        //ReceiveObj = null;
                        ReceiveObj.Game_Id = "";
                        ReceiveObj.Packet_No = "";
                        ReceiveObj.Ticket_Name = "";
                        ReceiveObj.Rate = "";
                        ReceiveObj.Start_No = "";
                        ReceiveObj.End_No = "";
                        IsHitTestVisible = false;
                    }
                }
            }

        }
        public async void ValidateRecodToMasterList()
        {
            //BarcodeFormatDetails();
            int GameIdRange = GetBarcodeFormat.GameIDTo - GetBarcodeFormat.GameIDFrom + 1;
            IsValid = true;
            if (MasterListObj.Game_Id == null || MasterListObj.Ticket_Name == null || MasterListObj.Rate == null || MasterListObj.End_No == null || MasterListObj.Game_Id == "" || MasterListObj.Ticket_Name == "" || MasterListObj.Rate == "" || MasterListObj.End_No == "")
            {
                IsValid = false;
                var dialog = new MessageDialog("Please Fill All Information.");
                await dialog.ShowAsync();
            }
            else
            {
                if (MasterListObj.Game_Id.Length < GameIdRange || MasterListObj.Game_Id.Length > GameIdRange)
                {
                    IsValid = false;
                    var dialog = new MessageDialog("GameId must be equal to " + GameIdRange + " Digit");
                    await dialog.ShowAsync();
                }

                else if (!regex.IsMatch(MasterListObj.Game_Id))
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Game_Id must be numeric.");
                    await dialog.ShowAsync();
                }
                else if (!regex.IsMatch(MasterListObj.Rate))
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Value must be numeric.");
                    await dialog.ShowAsync();
                }
                //else if (!regex.IsMatch(MasterListObj.Packet_No))
                //{
                //    IsValid = false;
                //    var dialog = new MessageDialog("Packet No. must be numeric.");
                //    await dialog.ShowAsync();
                //}
                else if (MasterListObj.Start_No != "000")
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Start no. must be 000.");
                    await dialog.ShowAsync();
                }
                else if (!regex.IsMatch(MasterListObj.End_No))
                {
                    IsValid = false;
                    var dialog = new MessageDialog("End no. must be numeric.");
                    await dialog.ShowAsync();
                }
                else if (MasterListObj.Rate != "" && Convert.ToInt32(MasterListObj.Rate) <= 0)
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Value must be greater than 0.");
                    await dialog.ShowAsync();
                }

                else if (MasterListObj.End_No != "" && Convert.ToInt32(MasterListObj.End_No) <= 0)
                {
                    IsValid = false;
                    var dialog = new MessageDialog("End no. must be greater than 0.");
                    await dialog.ShowAsync();
                }
                //else if (regex.IsMatch(ReceiveObj.Ticket_Name))
                //{
                //    IsValid = false;
                //    var dialog = new MessageDialog("Ticket Name must be in character. ");
                //    await dialog.ShowAsync();
                //}

                else if (MasterListObj.Start_No != "" && Convert.ToInt32(MasterListObj.Start_No) > 0 && MasterListObj.End_No != "" && Convert.ToInt32(MasterListObj.End_No) > 0)
                {
                    if (Convert.ToInt32(MasterListObj.Start_No) > Convert.ToInt32(MasterListObj.End_No))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Start no. must be less than End no.");
                        await dialog.ShowAsync();
                    }
                }
            }

        }
        public async void ValidateRecordToReceive()
        {
            IsValid = true;
            if (ReceiveObj.Game_Id != null && ReceiveObj.Packet_No != null && ReceiveObj.Ticket_Name != null && ReceiveObj.Rate != null && ReceiveObj.End_No != null)
            {
                if (!regex.IsMatch(ReceiveObj.Rate))
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Rate must be numeric.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                else if (!regex.IsMatch(ReceiveObj.Packet_No))
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Packet No. must be numeric.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                //else if (ReceiveObj.Start_No != "000")
                //{
                //    IsValid = false;
                //    var dialog = new MessageDialog("Start no. must be 000.");
                //    await dialog.ShowAsync();
                //    IsHitTestVisible = false;
                //    IsHitTestVisible = false;
                //}
                else if (!regex.IsMatch(ReceiveObj.End_No))
                {
                    IsValid = false;
                    var dialog = new MessageDialog("End no. must be numeric.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                else if (ReceiveObj.Rate != "" && Convert.ToInt32(ReceiveObj.Rate) <= 0)
                {
                    IsValid = false;
                    var dialog = new MessageDialog("Value must be greater than 0.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }

                else if (ReceiveObj.End_No != "" && Convert.ToInt32(ReceiveObj.End_No) <= 0)
                {
                    IsValid = false;
                    var dialog = new MessageDialog("End no. must be greater than 0.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }

                //else if (regex.IsMatch(ReceiveObj.Ticket_Name))
                //{
                //    IsValid = false;
                //    var dialog = new MessageDialog("Ticket Name must be in character. ");
                //    await dialog.ShowAsync();
                //}

                else if (ReceiveObj.Start_No != "" && Convert.ToInt32(ReceiveObj.Start_No) > 0 && ReceiveObj.End_No != "" && Convert.ToInt32(ReceiveObj.End_No) > 0)
                {
                    if (Convert.ToInt32(ReceiveObj.Start_No) > Convert.ToInt32(ReceiveObj.End_No))
                    {
                        IsValid = false;
                        var dialog = new MessageDialog("Start no. must be less than End no.");
                        await dialog.ShowAsync();
                        IsHitTestVisible = false;
                    }

                }
            }
            else
            {
                IsValid = false;
                var dialog = new MessageDialog("Please Fill All Information.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
            }

        }
        public async void ValidateRecordToDeactivate()
        {
            if (ActiveBoxObj.Stopped_At == "0")
            {
                var dialog = new MessageDialog("Please Enter Stopped_At No.");
                await dialog.ShowAsync();
            }
        }
        public void OnActivate()
        {
            //ReceiveselectedData = null;
            if(ReceiveselectedData!=null)
            {
                IsNewstoreCheckbox = Visibility.Collapsed;
            }
            else
            {
                IsNewstoreCheckbox = Visibility.Visible;
            }
            IsHitTestVisible = false;
            ObservableCollection<Activation_Box> temp = new ObservableCollection<Activation_Box>();
            IsCloseshitfPopup = false;
            IsShowActivateReturnPopup = false;
            IsActivateBox = false;
            LoadBoxCollection();
            LoadActive_BoxCollection();
            //Active_BoxCollection = new ObservableCollection<Activation_Box>();
            //temp = LoadActive_BoxCollection();

            //var a = temp.Where(x => x.Status == "Active").ToList();

            //foreach (var i in a)
            //{
            //    Active_BoxCollection.Add(new Activation_Box
            //    {
            //        Box_No = i.Box_No,
            //        Status = i.Status,
            //        Price = i.Price,
            //        Game_Id = i.Game_Id,
            //        Packet_No = i.Packet_No,
            //        Ticket_Name = i.Ticket_Name,
            //        Start_No = i.Start_No,
            //        Stopped_At = i.Stopped_At,
            //        End_No = i.End_No,
            //        ShiftID = i.ShiftID,
            //        EmployeeId = i.EmployeeId,
            //        Total_Price = i.Total_Price,
            //        Store_Id = i.Store_Id
            //    });
            //}
            IsActivatePopup = true;
            IsDeactivatePopup = false;
            IsReturnPopup = false;
            IsSoldOutPopup = false;
            IsReceiveManuallyPopup = false;
            IsSettlePopup = false;
            if (IsReceiveChecked == true && IsActivatePopup == true && ReceiveselectedData != null)
            {
                IsVisibleComboBox = Visibility.Collapsed;
            }
            //Active_StatusObj = null;
        }
        public ObservableCollection<Activation_Box> LoadBoxCollection()
        {
            ActiveBoxObj = new Activate_Ticket();
            ActiveBoxObj.Store_Id = ApplicationData.Store_Id;

            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ActiveBoxObj);
            var response = client.PostAsync("api/Lottery/NewGetBoxCollection", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            BoxCollection = new ObservableCollection<Activation_Box>();
            // HttpResponseMessage response = client.GetAsync("api/Lottery/GetBoxCollection").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                BoxCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(v);
            }
            return BoxCollection;
        }
        public async void OnStatusUpdate()
        {
            //IsHitTestVisible = true;
            if(IsNewStoreChecked != true)
            {
                SelectedItem = "Active";
                Active_StatusObj.State = ApplicationData.SelectedState;
                Active_StatusObj.EmployeeID = ApplicationData.Emp_Id;
                Active_StatusObj.Store_Id = ApplicationData.Store_Id;
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(Active_StatusObj);
                var response = client.PostAsync("api/Activate/ActivateTicket", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Ticket Activated Successfully..");
                    await dialog.ShowAsync();
                    //IsActivatePopup = false;
                    IsHitTestVisible = true;
                    FlagAutoGameID = 1;
                    MakeNull = "";
                    GetRefreshGrid();
                    GetReceiveBoxCount();
                    GetSoldOutBoxCount();
                    GetActivedBoxCount();
                    GetEmptyBoxCount();
                    LoadBoxCollection();
                    LoadLotteryCollection();
                    OnReceiveHistory();
                    LoadEmptyBoxes();
                    LoadActive_BoxCollection();
                    Active_StatusObj = new Activate_Ticket();
                }
                else if (response.ReasonPhrase == "Not Found")
                {

                    var dialog = new MessageDialog("This PacketID Already Activate.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                else
                {
                    var dialog = new MessageDialog("Please Select Proper Box No AND Packet ID.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }

            }
            else
            {
                OnNewStoreStatusUpdate();
            }

        }

        public async void OnUpdate_ActivatePackets()
        {
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Active_StatusObj);
            var response = client.PostAsync("api/Edit_Ticket/Edit_TicketDetails", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
            {
                var dialog = new MessageDialog("Ticket Updated Successfully..");
                await dialog.ShowAsync();
                GetEmptyBoxCount();
                Active_StatusObj = null;
                BoxCollection = new ObservableCollection<Activation_Box>();
                //LoadBoxCollection();
                LoadLotteryCollection();
                LoadEmptyBoxes();
                //LoadActive_BoxCollection();
            }
            else
            {
                //var dialog = new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                var dialog = new MessageDialog("Please enter valid Game Id and Packet Id...");
                await dialog.ShowAsync();
            }
        }
        public void OnDeactivate()
        {
            IsHitTestVisible = false;
            IsCloseshitfPopup = false;
            IsActivateBox = false;
            // ActiveBoxObj = new Activate_Ticket();
            if (ActiveBoxObj.Box_No != null)
            {
                IsContentChecked = true;
            }
            else
            {
                IsContentChecked = false;
            }
            LoadActive_BoxCollection();
            IsDeactivatePopup = true;
            if (IsContentChecked == true)
            {
                GetRecords();
            }

            IsActivatePopup = false;
            IsReturnPopup = false;
            IsSoldOutPopup = false;
            IsReceiveManuallyPopup = false;
            IsSettlePopup = false;

            // ActiveBoxObj = null;

        }
        public ObservableCollection<Activation_Box> LoadActive_BoxCollection()
        {
            //SelectedData = new Activation_Box();
            //Active_StatusObj = new Activate_Ticket();
            Active_StatusObj.Store_Id = ApplicationData.Store_Id;
            Active_StatusObj.EmployeeID = ApplicationData.Emp_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Active_StatusObj);
            var response = client.PostAsync("api/Activate/NewGetActiveBoxCollection", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            Active_BoxCollection = new ObservableCollection<Activation_Box>();
            // HttpResponseMessage response = client.GetAsync("api/Activate/NewGetActiveBoxCollection").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                Active_BoxCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(v);
            }

            return Active_BoxCollection;
        }
        public async void OnDeactivate_Tickets()
        {
            //IsHitTestVisible = true;
            ActiveBoxObj.EmployeeID = ApplicationData.Emp_Id;
            ActiveBoxObj.State = ApplicationData.SelectedState;
            ActiveBoxObj.Store_Id = ApplicationData.Store_Id;
            SelectedItem = "Deactive";
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ActiveBoxObj);

            if (ActiveBoxObj.Stopped_At != null)
            {
                int StoppedAt = ActiveBoxObj.Stopped_At.Length;

                if (ActiveBoxObj.Stopped_At == "0" || ActiveBoxObj.Stopped_At == "00" || StoppedAt >= 4)
                {
                    var dialog = new MessageDialog("Stopped_At No. must be 000 or Greater Than 000.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
            }
            if (ActiveBoxObj.Box_No == null)
            {
                var dialog = new MessageDialog("Please Select Box No.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
            }
            else if (ActiveBoxObj.Stopped_At == null || ActiveBoxObj.Stopped_At == "")
            {
                var dialog = new MessageDialog("Please enter Stopped_At Value");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
            }
            //if (ActiveBoxObj==null)
            //{
            //    var dialog = new MessageDialog("Please enter valid Game Id and Packet Id...");
            //    await dialog.ShowAsync();
            //}
            else
            {
                var response = client.PostAsync("api/Deactivate/Deactivate_Ticket", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Ticket Deactivated Successfully..");
                    await dialog.ShowAsync();
                    GetRefreshGrid();
                    MakeNull = "";
                    //IsDeactivatePopup = false;
                    GetEmptyBoxCount();
                    GetReceiveBoxCount();
                    GetSoldOutBoxCount();
                    GetDeactivedBoxCount();
                    GetSettledBoxCount();
                    GetActivedBoxCount();
                    ActiveBoxObj = new Activate_Ticket();
                    LoadLotteryCollection();
                    LoadEmptyBoxes();
                    //IsVisiblecalendarGrid = Visibility.Collapsed;
                }
                else if (response.ReasonPhrase == "Conflict")
                {
                    var dialog = new MessageDialog("This GameID or PacketID not available in Store Inventory.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                else
                {
                    var dialog = new MessageDialog("Please Check Total Tickets.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
            }

        }
        public void OnSoldOut()
        {
            IsHitTestVisible = false;
            SoldOutObj = new SoldOut_Details();
            IsActivateBox = false;
            if (ActiveBoxObj.Box_No != null)
            {
                IsContentChecked = true;
            }
            else
            {
                IsContentChecked = false;
            }
            LoadActive_BoxCollection();
            IsSoldOutPopup = true;
            if (IsContentChecked == true)
            {
                ShowBoxNo();
            }
            IsActivatePopup = false;
            IsDeactivatePopup = false;
            IsReturnPopup = false;
            IsReceiveManuallyPopup = false;
            IsSettlePopup = false;
            IsCloseShiftActivateBox = false;
            //SoldOutObj = null;

        }
        public async void OnSoldOut_Ticket()
        {
            if (IsCloseshitfPopup == true)
            {
                IsHitTestVisible = false;
            }
            else { IsHitTestVisible = true; }
            //IsHitTestVisiblePopup = true;
            SelectedItem = "SoldOut";
            SoldOutObj.EmployeeID = ApplicationData.Emp_Id;
            //SoldOutObj.ShiftID = Emp_Details_Obj.Shiftid;
            SoldOutObj.State = ApplicationData.SelectedState;
            SoldOutObj.Store_Id = ApplicationData.Store_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(SoldOutObj);
            if (SoldOutObj.End_No == null)
            {
                var dialog = new MessageDialog("Please Enter Valid Close Position.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
                IsHitTestVisiblePopup = false;
            }
            else if (SoldOutObj.Box_No == null)
            {
                var dialog = new MessageDialog("Please Select Box No.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
                IsHitTestVisiblePopup = false;
            }
            else
            {
                var response = client.PostAsync("api/SoldOut/SoldOut_Ticket", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    // OnGetAutoSettled();
                    var dialog = new MessageDialog("Ticket Marked Sold Out Successfully..");
                    await dialog.ShowAsync();
                    MakeNull = "";
                    //IsSoldOutPopup = false;
                    GetRefreshGrid();
                    LoadActive_BoxCollection();
                    GetSoldOutBoxCount();
                    GetSettledBoxCount();
                    GetActivedBoxCount();
                    SoldOutObj = new SoldOut_Details();
                    LoadBoxCollection();
                    LoadLotteryCollection();
                    LoadEmptyBoxes();

                }
                else if (response.ReasonPhrase == "Not Found")
                {
                    var dialog = new MessageDialog("Please Check Total Tickets.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                    IsHitTestVisiblePopup = false;
                }
                else
                {
                    var dialog = new MessageDialog("This GameID or PacketID not Available in Store Inventory.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                    IsHitTestVisiblePopup = false;
                }
            }
        }
        public void OnGetAutoSettled()
        {
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(SoldOutObj);
            var response = client.PostAsync("api/SoldOut/GetAutoIsSettled", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSettledBoxCount();
                GetSoldOutBoxCount();
            }

        }
        public void OnReturn()
        {
            IsHitTestVisible = false;
            // Return_Obj = new Return_Details();
            //if (IsReceiveChecked == true) { ReceiveselectedData = null; }
            IsShowActivateReturnPopup = false;
            // ReceiveselectedData = null;
            IsActivateBox = false;
            if (ActiveBoxObj.Box_No != null)
            {
                IsContentChecked = true;
            }
            else
            {
                IsContentChecked = false;
            }
            LoadActive_BoxCollection();
            IsReturnPopup = true;
            if (IsContentChecked == true)
            {
                ShowBoxNo();
            }
            IsSoldOutPopup = false;
            IsActivatePopup = false;
            IsReceiveManuallyPopup = false;
            IsDeactivatePopup = false;
            IsSettlePopup = false;
            if (IsReceiveChecked == true && IsReturnPopup == true && ReceiveselectedData != null)
            {
                IsVisibleComboBox = Visibility.Collapsed;
            }
            IsCloseShiftActivateBox = false;
            // Return_Obj = null;
        }
        public async void OnReturn_Ticket()
        {
            if (IsCloseshitfPopup == true)
            {
                IsHitTestVisible = false;
            }
            else { IsHitTestVisible = true; }
            //IsHitTestVisiblePopup = true;
            SelectedItem = "Return";
            Return_Obj.EmployeeID = ApplicationData.Emp_Id;
            Return_Obj.State = ApplicationData.SelectedState;
            Return_Obj.Store_Id = ApplicationData.Store_Id;

            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Return_Obj);

            if (Return_Obj.End_No != null)
            {
                int ReturnAt = Return_Obj.End_No.Length;

                if (Return_Obj.End_No == "00" || ReturnAt >= 4)
                {
                    var dialog = new MessageDialog("Returned_At must be 000 or Greater Than 000.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                    IsHitTestVisiblePopup = false;
                }
            }

            if (Return_Obj.End_No == "0" || Return_Obj.End_No == null || Return_Obj.End_No == "")
            {
                var dialog = new MessageDialog("Please Enter Valid Close Position No.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
                IsHitTestVisiblePopup = false;
            }

            else if (Return_Obj.Box_No == null && Return_Obj.Game_Id == null)
            {
                var dialog = new MessageDialog("Please Select Box No.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
                IsHitTestVisiblePopup = false;
            }

            else
            {
                var response = client.PostAsync("api/Return/Return_Ticket", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Ticket Marked Returned Successfully..");
                    await dialog.ShowAsync();
                    GetRefreshGrid();
                    MakeNull = "";
                    //IsReturnPopup = false;
                    GetReturnedBoxCount();
                    GetReceiveBoxCount();
                    GetSoldOutBoxCount();
                    GetEmptyBoxCount();
                    GetActivedBoxCount();
                    GetSettledBoxCount();
                    //GetSettledBoxCount();
                    //if(IsReceiveChecked == true) { OnReceiveHistory(); }
                    //else if(IsActivateChecked==true) { GetActivedBoxCount(); }
                    //else { GetDeactivedBoxCount(); }
                    Return_Obj = new Return_Details();
                    BoxCollection = new ObservableCollection<Activation_Box>();
                    LoadBoxCollection();
                    LoadLotteryCollection();
                    LoadActive_BoxCollection();
                    LoadEmptyBoxes();
                    //IsVisiblecalendarGrid = Visibility.Collapsed;

                }
                else if (response.ReasonPhrase == "Not Found")
                {
                    var dialog = new MessageDialog("Please check Total Tickets.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                    IsHitTestVisiblePopup = false;
                }
                else
                {
                    var dialog = new MessageDialog("This GameID or PacketID not Available in Store Inventory.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                    IsHitTestVisiblePopup = false;
                }
            }
        }
        public void OnSettle()
        {
            IsHitTestVisible = false;
            IsVisibleComboBox = Visibility.Visible;
            IsCloseshitfPopup = false;
            Settle_Obj = new Settle_Details();
            IsActivateBox = false;
            if (ActiveBoxObj.Box_No != null)
            {
                IsContentChecked = true;
            }
            else
            {
                IsContentChecked = false;
            }
            LoadActive_BoxCollection();

            IsSettlePopup = true;

            if (IsContentChecked == true)
            {
                ShowBoxNo();
            }
            IsReturnPopup = false;
            IsSoldOutPopup = false;
            IsActivatePopup = false;
            // IsVisibleReceivedDataGrid = Visibility.Collapsed;


            //if(IsSettlePopup==false)
            //{
            //    Settle_Obj.Ticket_Name = "";
            //    Settle_Obj.Price = 0;
            //    Settle_Obj.Start_No = "";
            //    Settle_Obj.End_No = "";
            //}

        }
        public async void OnSettle_Ticket()
        {
            // IsHitTestVisible = true;
            SelectedItem = "Settle";
            Settle_Obj.EmployeeID = ApplicationData.Emp_Id;
            Settle_Obj.State = ApplicationData.SelectedState;
            Settle_Obj.Store_Id = ApplicationData.Store_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Settle_Obj);

            if (Settle_Obj.Packet_No != "")
            {
                var response = client.PostAsync("api/Settle/Settle_Ticket", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Ticket Marked Settle Successfully..");
                    await dialog.ShowAsync();
                    MakeNull = "";
                    //SettleTemp = 1;
                    GetRefreshGrid();
                    // IsSettlePopup = false;
                    GetSettledBoxCount();
                    GetEmptyBoxCount();
                    GetReceiveBoxCount();
                    GetActivedBoxCount();
                    Settle_Obj = new Settle_Details();
                    //BoxCollection = new ObservableCollection<Activation_Box>();
                    LoadBoxCollection();
                    LoadLotteryCollection();
                    LoadActive_BoxCollection();
                    LoadEmptyBoxes();
                    // IsVisiblecalendarGrid = Visibility.Collapsed;
                }

                else if (response.ReasonPhrase == "Not Found")
                {
                    var dialog = new MessageDialog("The Packets in this Box has Already been Settled.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                else if (response.ReasonPhrase == "Conflict")
                {
                    var dialog = new MessageDialog("Please Check Total Tickets.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }

                else if (response.ReasonPhrase == "Bad Request")
                {
                    var dialog = new MessageDialog("Please Enter Proper Packet Id");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
                else
                {
                    var dialog = new MessageDialog("This GameID or PacketID not Available in Store Inventory.");
                    await dialog.ShowAsync();
                    IsHitTestVisible = false;
                }
            }

            else if (Settle_Obj.Box_No == null)
            {
                var dialog = new MessageDialog("Please Select Box No.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
            }
            else if (Settle_Obj.Packet_No == "")
            {
                var dialog = new MessageDialog("Please Enter Packet Id.");
                await dialog.ShowAsync();
                IsHitTestVisible = false;
            }
        }
        public void DisplayInfo(string Content)
        {
            Single_Record = LotteryColl.Where<LotteryInfo>(i => i.Box_No == Convert.ToInt32(Content)).SingleOrDefault();
            if (Single_Record.Status == "Active")
            {
                LotteryInfoObj.Ticket_Name = Single_Record.Ticket_Name;
                LotteryInfoObj.Packet_No = Single_Record.Packet_No;
                LotteryInfoObj.Game_Id = Single_Record.Game_Id;
                LotteryInfoObj.Price = Single_Record.Price;
                LotteryInfoObj.Start_No = Single_Record.Start_No;
                LotteryInfoObj.End_No = Single_Record.End_No;
                LotteryInfoObj.Count = Single_Record.Count;
                LotteryInfoObj.Status = Single_Record.Status;
                LotteryInfoObj.Stopped_At = Single_Record.Stopped_At;
                IsPopupEmptybox = false;
                IsActivateBox = true;
            }
            else if (Single_Record.Status == "Empty" || Single_Record.Status == "Deactivated")
            {
                //IsPopupEmptybox = true;
                //IsActivateBox = false;
                IsActivatePopup = true;
                // if(IsActivatePopup==true)
                // {
                IsVisibleActiveBoxNo = Visibility.Visible;
                IsVisibleActiveComboBox = Visibility.Collapsed;
                Displayboxno = Single_Record.Box_No;
                Active_StatusObj.Box_No = Displayboxno;
                // }

            }
            else if (Single_Record.Status == "SoldOut")
            {
                //IsSoldOutBox = true;
                IsActivatePopup = true;
                IsVisibleActiveBoxNo = Visibility.Visible;
                IsVisibleActiveComboBox = Visibility.Collapsed;
                Displayboxno = Single_Record.Box_No;
                Active_StatusObj.Box_No = Displayboxno;
            }

            else if (Single_Record.Status == "Settle")
            {
                //IsPopupEmptybox = true; 
                IsActivateBox = true;
                LotteryInfoObj.Ticket_Name = Single_Record.Ticket_Name;
                LotteryInfoObj.Packet_No = Single_Record.Packet_No;
                LotteryInfoObj.Game_Id = Single_Record.Game_Id;
                LotteryInfoObj.Price = Single_Record.Price;
                LotteryInfoObj.Start_No = Single_Record.Start_No;
                LotteryInfoObj.End_No = Single_Record.End_No;
                LotteryInfoObj.Count = Single_Record.Count;
                LotteryInfoObj.Status = Single_Record.Status;
                Active_StatusObj.Box_No = Displayboxno;
            }
            else if (Single_Record.Status == "Close")
            {
                //IsPopupEmptybox = true; 
                IsCloseBoxReopen = true;
                ActiveBoxObj.Box_No = Single_Record.Box_No;
                ActiveBoxObj.Ticket_Name = Single_Record.Ticket_Name;
                ActiveBoxObj.Packet_No = Single_Record.Packet_No;
                ActiveBoxObj.Game_Id = Single_Record.Game_Id;
                ActiveBoxObj.Price = Single_Record.Price;
                ActiveBoxObj.Start_No = Single_Record.Start_No;
                ActiveBoxObj.End_No = Single_Record.End_No;
                ActiveBoxObj.Stopped_At = Single_Record.Stopped_At;
                ActiveBoxObj.Count = Single_Record.Count;
                ActiveBoxObj.Status = Single_Record.Status;
                Active_StatusObj.Box_No = Displayboxno;
            }


        }
        #endregion

        #region RightButtonSection
        public void LoadBoxCount()
        {
            GetReceiveBoxCount();
            GetActivedBoxCount();

            GetDeactivedBoxCount();
            GetSoldOutBoxCount();
            GetReturnedBoxCount();
            GetSettledBoxCount();
            GetEmptyBoxCount();
        }
        //Method for Receive Box Count
        public void GetReceiveBoxCount()
        {
            ReceiveObj = new Receive_Inventory();
            ReceiveObj.Store_Id = ApplicationData.Store_Id;
            ReceiveObj.EmployeeId = ApplicationData.Emp_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ReceiveObj);
            HttpResponseMessage response = client.PostAsync("api/Receive/NewGetReceiveBoxCount", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                // HttpResponseMessage receive = client.GetAsync("api/Receive/GetReceiveBoxCount").Result;
                var rec = response.Content.ReadAsStringAsync().Result;
                CountReceiveBox = JsonConvert.DeserializeObject<int>(rec);
            }
        }
        //Method for Active Box Count
        public void GetActivedBoxCount()
        {
            Active_StatusObj = new Activate_Ticket();
            Active_StatusObj.Store_Id = ApplicationData.Store_Id;
            Active_StatusObj.EmployeeID = ApplicationData.Emp_Id;
            Active_BoxCollection = new ObservableCollection<Activation_Box>();
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Active_StatusObj);
            var response = client.PostAsync("api/Activate/NewGetActiveBoxCollection", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                // HttpResponseMessage active = client.GetAsync("api/Activate/GetActiveBoxCount").Result;
                var act = response.Content.ReadAsStringAsync().Result;

                Active_BoxCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(act);
                CountActiveBox = 0;
                foreach (var i in Active_BoxCollection)
                {
                    if (i.Status == "Active")
                    {
                        CountActiveBox = CountActiveBox + 1;
                    }
                }

            }

        }
        public void GetDeactivedBoxCount()
        {

            //HttpResponseMessage deactive = client.GetAsync("api/Deactivate/GetDeactiveBoxCount").Result;
            //var deact = deactive.Content.ReadAsStringAsync().Result;
            //CountDeactiveBox = JsonConvert.DeserializeObject<int>(deact);
            OnDeActivateHistory();
        }
        public void GetUpdatedsoldCount()
        {
            HttpResponseMessage soldout = client.GetAsync("api/SoldOut/GetSoldOutBoxCount").Result;
            var sold = soldout.Content.ReadAsStringAsync().Result;
            CountSoldOutBox = JsonConvert.DeserializeObject<int>(sold);
        }
        public void GetSoldOutBoxCount()
        {
            //HttpResponseMessage soldout = client.GetAsync("api/SoldOut/GetSoldOutBoxCount").Result;
            //var sold = soldout.Content.ReadAsStringAsync().Result;
            //CountSoldOutBox = JsonConvert.DeserializeObject<int>(sold);
            OnSoldOutHistory();
        }
        public void GetReturnedBoxCount()
        {
            //HttpResponseMessage returnticket = client.GetAsync("api/Return/GetReturnBoxCount").Result;
            //var ret = returnticket.Content.ReadAsStringAsync().Result;
            //CountReturnBox = JsonConvert.DeserializeObject<int>(ret);
            OnReturnHistory();
        }
        public void GetSettledBoxCount()
        {
            //HttpResponseMessage settle = client.GetAsync("api/Settle/GetSettleBoxCount").Result;
            //var set = settle.Content.ReadAsStringAsync().Result;
            //CountSettleBox = JsonConvert.DeserializeObject<int>(set);
            OnSettelementHistory();
        }
        public void GetEmptyBoxCount()
        {
            Settle_Obj = new Settle_Details();
            Settle_Obj.Store_Id = ApplicationData.Store_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Settle_Obj);
            var empty = client.PostAsync("api/Empty/NewGetEmptyBoxCount", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (empty.IsSuccessStatusCode)
            {
                // HttpResponseMessage empty = client.GetAsync("api/Empty/GetEmptyBoxCount").Result;
                var empt = empty.Content.ReadAsStringAsync().Result;
                CountEmptyBox = JsonConvert.DeserializeObject<int>(empt);
            }

        }
        public void OnDeActivateHistory()
        {
            IsReportPopup = false;
            IsDailyReportPopup = false;
            if (IsDeactivateChecked == true)
            {
                IsVisiblebtnGrid = Visibility.Collapsed;
                IsVisiblecalendarGrid = Visibility.Visible;
            }
            ActiveBoxObj = new Activate_Ticket();
            ActiveBoxObj.Store_Id = ApplicationData.Store_Id;
            ActiveBoxObj.EmployeeID = ApplicationData.Emp_Id;
            ObservableCollection<Activation_Box> tempCollection = new ObservableCollection<Activation_Box>();
            //IsVisiblebtnGrid = Visibility.Collapsed;
            DeactivatehistoryColl = new ObservableCollection<Activation_Box>();

            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ActiveBoxObj);
            var response = client.PostAsync("api/Deactivate/NewGetDeactivateHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            // HttpResponseMessage response = client.GetAsync("api/Deactivate/GetDeactivateHistory").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
            }
            var list = tempCollection.ToList();
            CountDeactiveBox = list.Count;
            foreach (var i in list)
            {
                DeactivatehistoryColl.Add(new Activation_Box
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
            //GetReceiveBoxCount();
        }
        public void OnActivateHistory()
        {
            IsReportPopup = false;
            IsDailyReportPopup = false;
            if (IsActivateChecked == true)
            {
                IsVisiblebtnGrid = Visibility.Collapsed;
                IsVisiblecalendarGrid = Visibility.Collapsed;
            }
            Active_StatusObj = new Activate_Ticket();
            ActivatehistoryColl = new ObservableCollection<Activation_Box>();
            Active_StatusObj.Store_Id = ApplicationData.Store_Id;
            Active_StatusObj.EmployeeID = ApplicationData.Emp_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Active_StatusObj);
            var response = client.PostAsync("api/Activate/NewGetActivateHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            // HttpResponseMessage response = client.GetAsync("api/Activate/GetActivateHistory").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                ActivatehistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(v);
            }

            //var active = client.PostAsync("api/Activate/NewGetActivateTicketTotalPrice", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            //// HttpResponseMessage receive = client.GetAsync("api/Receive/NewGetActivateTicketTotalPrice").Result;
            //if (active.IsSuccessStatusCode)
            //{
            //    var rec = active.Content.ReadAsStringAsync().Result;
            //    if (rec != "null")
            //    {
            //        ActiveTotalPrice = JsonConvert.DeserializeObject<int>(rec);
            //    }
            //    else
            //    {
            //        ActiveTotalPrice = 0;
            //    }
            //}
            GetReceiveBoxCount();
        }
        public void OnGetReceiveAndActive()
        {
            GetrRceiveandActive = new ObservableCollection<Receive_Inventory>();
            HttpResponseMessage response = client.GetAsync("api/Receive/GetReceiveAndActiveCollection").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                GetrRceiveandActive = JsonConvert.DeserializeObject<ObservableCollection<Receive_Inventory>>(v);
            }
        }
        public void OnReceiveHistory()
        {
            IsReportPopup = false;
            IsDailyReportPopup = false;
            if (IsReceiveChecked == true)
            {
                IsVisiblebtnGrid = Visibility.Visible;
                IsVisiblecalendarGrid = Visibility.Collapsed;
            }
            HistoryColl = new ObservableCollection<Activation_Box>();
            HistoryCollReceive = new ObservableCollection<Activation_Box>();
            ReceiveObj = new Receive_Inventory();
            ReceiveObj.Store_Id = ApplicationData.Store_Id;
            ReceiveObj.EmployeeId = ApplicationData.Emp_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ReceiveObj);
            var response = client.PostAsync("api/Receive/NewGetReceiveHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            //  HttpResponseMessage response = client.GetAsync("api/Receive/GetReceiveHistory").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                HistoryColl = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(v);
                // HistoryCollReceive = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(v);
                // HistoryColl = HistoryCollReceive.Where(x => x.State == ApplicationData.SelectedState).ToList();
                // HistoryColl = s;

            }

            var receive = client.PostAsync("api/Receive/NewGetActivateTicketTotalPrice", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            // HttpResponseMessage receive = client.GetAsync("api/Receive/NewGetActivateTicketTotalPrice").Result;
            if (receive.IsSuccessStatusCode)
            {
                var rec = receive.Content.ReadAsStringAsync().Result;
                if (rec != "null")
                {
                    TotalPrice = JsonConvert.DeserializeObject<int>(rec);
                }
                else
                {
                    TotalPrice = 0;
                }
            }


            // HttpResponseMessage totalpackets = client.GetAsync("api/Receive/NewGetTotalPackets").Result;
            var totalpackets = client.PostAsync("api/Receive/NewGetTotalPackets", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (totalpackets.IsSuccessStatusCode)
            {
                var packetcount = totalpackets.Content.ReadAsStringAsync().Result;
                if (packetcount != "null")
                {
                    TotalPackets = JsonConvert.DeserializeObject<int>(packetcount);
                }
            }

        }
        public void OnTerminalDataCollection()
        {
            TerminalDataCollection = new ObservableCollection<Terminal_Details>();
            TerminalObj = new Terminal_Details();
            DailyTotal = new Terminal_Details();

            if (HamburgerSelectedData != null)
            {
                TerminalObj.Store_Id = ApplicationData.Store_Id;
                TerminalObj.Date = t1;
            }
            else if (ShiftReportSelectedData != null)
            {
                TerminalObj.ShiftID = Convert.ToInt32(ShiftReportSelectedData.ShiftId);
                TerminalObj.Store_Id = ApplicationData.Store_Id;
                TerminalObj.EmployeeID = ApplicationData.Emp_Id;
                TerminalObj.CloseTime = ShiftReportSelectedData.EndTime;
                TerminalObj.Date = ShiftReportSelectedData.Date;

            }
            else if (HamburgerSelectedData == null)
            {
                TerminalObj.Store_Id = ApplicationData.Store_Id;
                TerminalObj.EmployeeID = ApplicationData.Emp_Id;
            }

            int count = 1;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(TerminalObj);
            var response = client.PostAsync("api/CloseShift/NewGetTerminalDataRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                TerminalDataCollection = JsonConvert.DeserializeObject<ObservableCollection<Terminal_Details>>(res);
                foreach (var i in TerminalDataCollection)
                {
                    if (DailyTotal.ScratchSells != null && DailyTotal.Credit >= 0 && DailyTotal.NetCash != null)
                    {
                        DailyTotal.ScratchSells = (int.Parse(i.ScratchSells) + int.Parse(DailyTotal.ScratchSells)).ToString();
                        DailyTotal.OnlineSells = (int.Parse(i.OnlineSells) + int.Parse(DailyTotal.OnlineSells)).ToString();
                        DailyTotal.TotalSells = (int.Parse(i.TotalSells) + int.Parse(DailyTotal.TotalSells)).ToString();
                        DailyTotal.ScratchPayout = (int.Parse(i.ScratchPayout) + int.Parse(DailyTotal.ScratchPayout)).ToString();
                        DailyTotal.OnlinePayout = (int.Parse(i.OnlinePayout) + int.Parse(DailyTotal.OnlinePayout)).ToString();
                        DailyTotal.TotalPayout = (int.Parse(i.TotalPayout) + int.Parse(DailyTotal.TotalPayout)).ToString();
                        DailyTotal.Loan = (int.Parse(i.Loan) + int.Parse(DailyTotal.Loan)).ToString();
                        DailyTotal.Credit = DailyTotal.Credit + i.Credit;
                        DailyTotal.Debit = DailyTotal.Debit + i.Debit;
                        DailyTotal.TopUp = DailyTotal.TopUp + i.TopUp;
                        DailyTotal.CashOnHand = (int.Parse(DailyTotal.CashOnHand) + int.Parse(i.CashOnHand)).ToString();
                        DailyTotal.TopUPCancel = DailyTotal.TopUPCancel + i.TopUPCancel;
                        DailyTotal.NetCash = (int.Parse(i.NetCash) + int.Parse(DailyTotal.NetCash)).ToString();
                        DailyTotal.Short1 = (int.Parse(i.Short1) + int.Parse(DailyTotal.Short1)).ToString();
                        DailyTotal.Over = (int.Parse(i.Over) + int.Parse(DailyTotal.Over)).ToString();
                        // DailyTotal.ShortOver = (int.Parse(i.ShortOver) + int.Parse(DailyTotal.ShortOver)).ToString();
                        DailyTotal.TotalActiveInventory = (int.Parse(i.TotalActiveInventory) + int.Parse(DailyTotal.TotalActiveInventory)).ToString();
                        DailyTotal.TotalStockInventory = (int.Parse(i.TotalStockInventory) + int.Parse(DailyTotal.TotalStockInventory)).ToString();
                        DailyTotal.CountActive = i.CountActive + DailyTotal.CountActive;
                        DailyTotal.CountRecevied = i.CountRecevied + DailyTotal.CountRecevied;
                        i.ShiftID = count;
                    }
                    else
                    {
                        DailyTotal.ScratchSells = i.ScratchSells;
                        DailyTotal.OnlineSells = i.OnlineSells;
                        DailyTotal.TotalSells = i.TotalSells;
                        DailyTotal.ScratchPayout = i.ScratchPayout;
                        DailyTotal.OnlinePayout = i.OnlinePayout;
                        DailyTotal.TotalPayout = i.TotalPayout;
                        DailyTotal.Loan = i.Loan;
                        DailyTotal.Credit = i.Credit;
                        DailyTotal.Debit = i.Debit;
                        DailyTotal.TopUp = i.TopUp;
                        DailyTotal.TopUPCancel = i.TopUPCancel;
                        DailyTotal.NetCash = i.NetCash;
                        DailyTotal.ShortOver = i.ShortOver;
                        DailyTotal.TotalStockInventory = i.TotalStockInventory;
                        DailyTotal.TotalActiveInventory = i.TotalActiveInventory;
                        DailyTotal.CountActive = i.CountActive;
                        DailyTotal.CountRecevied = i.CountRecevied;
                        DailyTotal.CashOnHand = i.CashOnHand;
                        DailyTotal.Short1 = i.Short1;
                        DailyTotal.Over = i.Over;
                        i.ShiftID = count;
                    }
                    ShiftReportDate = i.Date.ToString("MM/dd/yyyy");
                    SingleShiftReportDate = i.Date;
                    count = count + 1;
                }
            }
            var v = TerminalDataCollection.LastOrDefault();
            if (v != null)
            {
                TerminalObj.OnlinePayout = v.OnlinePayout;
                TerminalObj.OnlineSells = v.OnlineSells;
                TerminalObj.ScratchPayout = v.ScratchPayout;
                TerminalObj.ScratchSells = v.ScratchSells;
                TerminalObj.Loan = v.Loan;
                //TerminalObj.ActualCash = (Convert.ToInt32(v.ScratchSells) + Convert.ToInt32(v.OnlineSells) - Convert.ToInt32(v.ScratchPayout) - Convert.ToInt32(v.OnlinePayout) - Convert.ToInt32(v.Loan)).ToString();
                TerminalObj.Credit = v.Credit;
                TerminalObj.Debit = v.Debit;
                TerminalObj.TopUp = v.TopUp;
                TerminalObj.TopUPCancel = v.TopUPCancel;
                TerminalObj.TrackedAmount = (Total + Convert.ToInt32(v.OnlineSells) - Convert.ToInt32(v.ScratchPayout) - Convert.ToInt32(v.OnlinePayout)).ToString();
                TerminalObj.Date = v.Date;
                TerminalObj.CashOnHand = v.CashOnHand;
                TerminalObj.NetCash = v.NetCash;
                TerminalObj.Short1 = v.Short1;
                TerminalObj.Over = v.Over;
            }

            //TotalSells = Convert.ToInt32(v.ScratchSells) + Convert.ToInt32(v.OnlineSells);
            //TotalPayOut = Convert.ToInt32(v.OnlinePayout) + Convert.ToInt32(v.ScratchPayout);
            //TerminalObj.InstockInventory = v.InstockInventory;
            //TerminalObj.ActiveInventory = v.ActiveInventory;
            //CountTerminalActiveReceive = Convert.ToInt32(v.InstockInventory) + Convert.ToInt32(v.ActiveInventory);
            //TerminalObj.ShortOver = (Total - Convert.ToInt32(TerminalObj.CashOnHand)).ToString();
            //TerminalObj.ShortOverActive = (CountReceiveBox - Convert.ToInt32(TerminalObj.InstockInventory)).ToString();
            //TerminalObj.ShortOverStock = (CountActiveBox - Convert.ToInt32(TerminalObj.ActiveInventory)).ToString();
            //TerminalObj.TotalStockInventory = CountReceiveBox.ToString();
            //TerminalObj.TotalActiveInventory = CountActiveBox.ToString();
            //TerminalObj.TrackedAmount = (Total + Convert.ToInt32(v.OnlineSells) - Convert.ToInt32(v.ScratchPayout) - Convert.ToInt32(v.OnlinePayout)).ToString();
        }
        public void OnReturnHistory()
        {
            IsReportPopup = false;
            IsDailyReportPopup = false;
            if (IsReturnChecked == true)
            {
                IsVisiblebtnGrid = Visibility.Collapsed;
                IsVisiblecalendarGrid = Visibility.Visible;
            }

            ObservableCollection<Activation_Box> tempCollection = new ObservableCollection<Activation_Box>();
            // IsVisiblebtnGrid = Visibility.Collapsed;
            ReturnhistoryColl = new ObservableCollection<Activation_Box>();
            Return_Obj = new Return_Details();
            Return_Obj.Store_Id = ApplicationData.Store_Id;
            Return_Obj.EmployeeID = ApplicationData.Emp_Id;

            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Return_Obj);

            // HttpResponseMessage response = client.GetAsync("api/Return/GetReturnHistory").Result;
            var response = client.PostAsync("api/CloseShift/NewGetReturnRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
            }
            var list = tempCollection.ToList();
            CountReturnBox = list.Count;
            foreach (var i in list)
            {
                ReturnhistoryColl.Add(new Activation_Box
                {
                    Game_Id = i.Game_Id,
                    Box_No = i.Box_No,
                    Packet_No = i.Packet_No,
                    Ticket_Name = i.Ticket_Name,
                    Price = i.Price,
                    Created_Date = i.Created_Date,
                    Start_No = i.Start_No,
                    End_No = i.End_No,
                    Return_At = i.Return_At
                });
            }
            //GetReceiveBoxCount();
        }
        public void OnSettelementHistory()
        {
            IsReportPopup = false;
            IsDailyReportPopup = false;
            if (IsSettledChecked == true)
            {
                IsVisiblebtnGrid = Visibility.Collapsed;
                IsVisiblecalendarGrid = Visibility.Visible;
            }
            ObservableCollection<Activation_Box> tempCollection = new ObservableCollection<Activation_Box>();
            // IsVisiblebtnGrid = Visibility.Collapsed;
            SettleHistoryColl = new ObservableCollection<Activation_Box>();
            Settle_Obj = new Settle_Details();
            Settle_Obj.Store_Id = ApplicationData.Store_Id;
            Settle_Obj.EmployeeID = ApplicationData.Emp_Id;
            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(Settle_Obj);

            //  HttpResponseMessage response = client.GetAsync("api/Settle/GetSettleHistory").Result;
            var response = client.PostAsync("api/Settle/NewGetSettleHistory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
            }
            var list = tempCollection.ToList();
            CountSettleBox = list.Count;
            foreach (var i in list)
            {
                SettleHistoryColl.Add(new Activation_Box
                {
                    Game_Id = i.Game_Id,
                    Box_No = i.Box_No,
                    Packet_No = i.Packet_No,
                    Ticket_Name = i.Ticket_Name,
                    Price = i.Price,
                    Created_Date = i.Created_Date,
                    Start_No = i.Start_No,
                    End_No = i.End_No,
                    Stopped_At = i.Stopped_At
                });
            }

            GetReceiveBoxCount();
        }
        public async void OnDeleteSelectedRecord()
        {

            var v = SelectedData;

            //string val = v.Game_Id;
            if (v == null)
            {
                var dialog1 = new MessageDialog("Please Select the Record.");
                await dialog1.ShowAsync();
            }

            else
            {
                var dialog = new MessageDialog("Are You Sure!Want to Delete the Record?");
                dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
                var res = await dialog.ShowAsync();

                string json = "";

                if ((int)res.Id == 0)
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(SelectedData);
                    var response = client.PostAsync("api/Receive/DeleteSelectedRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        GetReceiveBoxCount();
                        OnReceiveHistory();
                        //Return_Obj = null;
                        BoxCollection = new ObservableCollection<Activation_Box>();
                        LoadBoxCollection();
                        LoadLotteryCollection();
                        LoadEmptyBoxes();
                    }
                    else
                    {
                        var dialog1 = new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                        await dialog1.ShowAsync();
                    }
                }
            }


        }
        public void OnSoldOutHistory()
        {
            IsReportPopup = false;
            IsDailyReportPopup = false;
            if (IsSoldoutChecked == true)
            {
                IsVisiblebtnGrid = Visibility.Collapsed;
                IsVisiblecalendarGrid = Visibility.Visible;
            }
            ObservableCollection<Activation_Box> tempCollection = new ObservableCollection<Activation_Box>();
            //  IsVisiblebtnGrid = Visibility.Collapsed;
            SoldOutObj = new SoldOut_Details();
            SoldouthistoryColl = new ObservableCollection<Activation_Box>();
            SoldOutObj.Store_Id = ApplicationData.Store_Id;
            SoldOutObj.EmployeeID = ApplicationData.Emp_Id;

            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(SoldOutObj);
            var response = client.PostAsync("api/CloseShift/NewGetSoldoutRecord", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            // HttpResponseMessage response = client.GetAsync("api/SoldOut/GetSoldOutHistory").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(res);
            }
            var list = tempCollection.ToList();
            CountSoldOutBox = list.Count;
            foreach (var i in list)
            {
                SoldouthistoryColl.Add(new Activation_Box
                {
                    Game_Id = i.Game_Id,
                    Packet_No = i.Packet_No,
                    Ticket_Name = i.Ticket_Name,
                    Price = i.Price,
                    Created_Date = i.Created_Date,
                    Start_No = i.Start_No,
                    End_No = i.End_No,
                    Box_No = i.Box_No,
                    Stopped_At = i.Stopped_At,
                    Partial_Packet = i.Partial_Packet
                });
            }
            GetReceiveBoxCount();
        }
        public void OnEmptyHistory()
        {
            IsReportPopup = false;
            IsDailyReportPopup = false;
            IsVisiblebtnGrid = Visibility.Collapsed;
            IsVisiblecalendarGrid = Visibility.Collapsed;
            HistoryColl = new ObservableCollection<Activation_Box>();
            GetReceiveBoxCount();
        }
        public void OnMasterHistory()
        {
            MasterColl = new ObservableCollection<Master_List_Inventory>();
            HttpResponseMessage response = client.GetAsync("api/MasterInventory/GetMasterHistory").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                MasterColl = JsonConvert.DeserializeObject<ObservableCollection<Master_List_Inventory>>(v);
            }
        }

        #endregion
        public async void OnSaveNewInventory()
        {
            ValidateRecodToMasterList();

            if (IsValid)
            {
                string json;
                MasterListObj.State = ApplicationData.SelectedState;
                MasterListObj.Store_Id = ApplicationData.Store_Id;
                MasterListObj.Employee_Id = ApplicationData.Emp_Id;
                json = Newtonsoft.Json.JsonConvert.SerializeObject(MasterListObj);
                var response = client.PostAsync("api/MasterInventory/SaveNewInventory", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Record Save Successfully.");
                    await dialog.ShowAsync();
                    //v.IsHitTestVisiblePopup = true;
                    //IsAddInventoryPopup = false;
                    //IsReceiveManuallyPopup = true;
                    MasterListObj.Game_Id = "";
                    MasterListObj.Packet_No = "";
                    MasterListObj.Ticket_Name = "";
                    MasterListObj.Rate = "";
                    MasterListObj.Start_No = "";
                    MasterListObj.End_No = "";
                    MasterListObj.Count = "";
                }
                else
                {
                    var dialog = new MessageDialog("This GameId or Ticket Name Already Exist.");
                    await dialog.ShowAsync();
                }
            }
        }
        public void CountEmptyBoxes()
        {
            //HttpResponseMessage countreponse = client.GetAsync("api/BoxCount/GetCountEmptyBox").Result;
            //var v = countreponse.Content.ReadAsStringAsync().Result;
            //int countBox = JsonConvert.DeserializeObject<int>(v);
            //if (countBox != 0)
            //{
            //    LoadEmptyBoxes();
            //}
            //else
            //{
            //    var dialog = new MessageDialog("Please first setup the empty boxes...");
            //    await dialog.ShowAsync();
            //}
        }
        public ObservableCollection<Activation_Box> LoadEmptyBoxes()
        {
            ActiveBoxObj = new Activate_Ticket();
            ActiveBoxObj.Store_Id = ApplicationData.Store_Id;

            string json = "";
            json = Newtonsoft.Json.JsonConvert.SerializeObject(ActiveBoxObj);
            var response = client.PostAsync("api/Empty/NewGetEmptyBoxCollection", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;

            EmptyBoxCollction = new ObservableCollection<Activation_Box>();
            // HttpResponseMessage response = client.GetAsync("api/Empty/GetEmptyBoxCollection").Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                EmptyBoxCollction = JsonConvert.DeserializeObject<ObservableCollection<Activation_Box>>(v);
            }

            return EmptyBoxCollction;
        }
        public void OnNext()
        {
            //CanNext();

            if (NewStoreBoxIndex < LotteryColl.Count - 2)
            {
                NewStoreBoxIndex += 1;
                SelectedIndexBoxno = LotteryColl[NewStoreBoxIndex];
            }
            else
            {
                NewStoreBoxIndex = LotteryColl.Count - 1;
                SelectedIndexBoxno = LotteryColl[NewStoreBoxIndex];
            }
            Active_StatusObj.Game_Id = SelectedIndexBoxno.Game_Id;
            Active_StatusObj.Packet_No = SelectedIndexBoxno.Packet_No;
            Active_StatusObj.Ticket_Name = SelectedIndexBoxno.Ticket_Name;
            Active_StatusObj.Price = SelectedIndexBoxno.Price;
            Active_StatusObj.Start_No = SelectedIndexBoxno.Start_No;
            Active_StatusObj.End_No = SelectedIndexBoxno.End_No;
        }
        public bool CanNext()
        {
            return NewStoreBoxIndex != LotteryColl.Count;
        }
        public void OnPrevious()
        {
            //CanPrevious();
            if (NewStoreBoxIndex > 0)
            {
                NewStoreBoxIndex -= 1;
                //NewStoreBoxIndex = LotteryColl.Count;
                SelectedIndexBoxno = LotteryColl[NewStoreBoxIndex];
            }
            else
            {
                NewStoreBoxIndex = 0;
                SelectedIndexBoxno = LotteryColl[NewStoreBoxIndex];
            }
            Active_StatusObj.Game_Id = SelectedIndexBoxno.Game_Id;
            Active_StatusObj.Packet_No = SelectedIndexBoxno.Packet_No;
            Active_StatusObj.Ticket_Name = SelectedIndexBoxno.Ticket_Name;
            Active_StatusObj.Price = SelectedIndexBoxno.Price;
            Active_StatusObj.Start_No = SelectedIndexBoxno.Start_No;
            Active_StatusObj.End_No = SelectedIndexBoxno.End_No;
        }
        public bool CanPrevious()
        {
            return SelectedIndex != 0;
        }

        #endregion

        #region Properties 
        public ObservableCollection<Receive_Inventory> GetPackId
        {
            get
            {
                return getpackId;
            }
            set
            {
                getpackId = value;
                NotifyPropertyChanged("GetPackId");
            }
        }
        public RelayCommand SaveNewInventory
        {
            get
            {
                return saveNewInventory;
            }

            set
            {
                saveNewInventory = value;
                NotifyPropertyChanged("SaveNewInventory");
            }
        }
        public Master_List_Inventory MasterListObj
        {
            get
            {
                return masterListObj;
            }

            set
            {
                masterListObj = value;
                NotifyPropertyChanged("MasterListObj");
            }
        }
        public ObservableCollection<SoldoutHistory> Soldhistorycoll
        {
            get
            {
                return soldhistorycoll;
            }

            set
            {
                soldhistorycoll = value;
                NotifyPropertyChanged("Soldhistorycoll");
            }
        }
        public ObservableCollection<SoldoutHistory> SoldOutReportHistory
        {
            get
            {
                return soldOutReportHistory;
            }

            set
            {
                soldOutReportHistory = value;
                NotifyPropertyChanged("SoldOutReportHistory");
            }
        }
        public Shift_Details Shift_dt
        {
            get
            {
                return shift_dt;
            }

            set
            {
                shift_dt = value;
                NotifyPropertyChanged("Shift_dt");
            }
        }
        public bool IsCloseshitfPopup
        {
            get
            {
                return isCloseshitfPopup;
            }

            set
            {
                isCloseshitfPopup = value;
                NotifyPropertyChanged("IsCloseshitfPopup");
            }
        }
        public RelayCommand CloseshiftCommand
        {
            get
            {
                return closeshiftCommand;
            }

            set
            {
                closeshiftCommand = value;
                NotifyPropertyChanged("CloseshiftCommand");
            }
        }
        public RelayCommand Signupcommand
        {
            get
            {
                return signupcommand;
            }

            set
            {
                signupcommand = value;
                NotifyPropertyChanged("Signupcommand");
            }
        }
        public RelayCommand SaveEmployeeDetails
        {
            get
            {
                return saveEmployeeDetails;
            }

            set
            {
                saveEmployeeDetails = value;
                NotifyPropertyChanged("SaveEmployeeDetails");
            }
        }
        public bool IsSingupPopup
        {
            get
            {
                return isSingupPopup;
            }

            set
            {
                isSingupPopup = value;
                NotifyPropertyChanged("IsSingupPopup");
            }
        }
        public Employee_Details Emp_Details_Obj
        {
            get
            {
                return emp_Details_Obj;
            }

            set
            {
                emp_Details_Obj = value;
                NotifyPropertyChanged("Emp_Details_Obj");
            }
        }
        public ObservableCollection<Shift_Details> ShiftCollection
        {
            get
            {
                return shiftCollection;
            }

            set
            {
                shiftCollection = value;
                NotifyPropertyChanged("ShiftCollection");
            }
        }
        public bool IsReceiveChecked
        {
            get
            {
                return isReceiveChecked;
            }

            set
            {
                isReceiveChecked = value;
                NotifyPropertyChanged("IsReceiveChecked");
                if (IsReceiveChecked)
                {
                    CollapseAllDatagrids();
                    IsVisibleReceivedDataGrid = Visibility.Visible;

                }
            }
        }
        public bool IsActivateChecked
        {
            get
            {
                return isActivateChecked;
            }

            set
            {
                isActivateChecked = value;
                NotifyPropertyChanged("IsActivateChecked");
                if (IsActivateChecked)
                {
                    CollapseAllDatagrids();
                    IsVisibleActivateDataGrid = Visibility.Visible;

                }
            }
        }
        public bool IsDeactivateChecked
        {
            get
            {
                return isDeactivateChecked;
            }

            set
            {
                isDeactivateChecked = value;
                NotifyPropertyChanged("IsDeactivateChecked");
                if (IsDeactivateChecked)
                {
                    CollapseAllDatagrids();
                    IsVisibleDeactivateDataGrid = Visibility.Visible;

                }
            }
        }
        public bool IsSoldoutChecked
        {
            get
            {
                return isSoldoutChecked;
            }

            set
            {
                isSoldoutChecked = value;
                NotifyPropertyChanged("IsSoldoutChecked");
                if (IsSoldoutChecked)
                {
                    CollapseAllDatagrids();
                    IsVisibleSoldoutDataGrid = Visibility.Visible;

                }
            }
        }
        public bool IsReturnChecked
        {
            get
            {
                return isReturnChecked;
            }

            set
            {
                isReturnChecked = value;
                NotifyPropertyChanged("IsReturnChecked");
                if (IsReturnChecked)
                {
                    CollapseAllDatagrids();
                    IsVisibleReturnDataGrid = Visibility.Visible;

                }
            }
        }
        public bool IsSettledChecked
        {
            get
            {
                return isSettledChecked;
            }

            set
            {
                isSettledChecked = value;
                NotifyPropertyChanged("IsSettledChecked");
                if (IsSettledChecked)
                {
                    CollapseAllDatagrids();
                    IsVisibleSettledDataGrid = Visibility.Visible;

                }
            }
        }
        public Visibility IsVisibleReceivedDataGrid
        {
            get
            {
                return isVisibleReceivedDataGrid;
            }
            set
            {
                isVisibleReceivedDataGrid = value;
                NotifyPropertyChanged("IsVisibleReceivedDataGrid");
            }
        }
        public Visibility IsVisibleActivateDataGrid
        {
            get
            {
                return isVisibleActivateDataGrid;
            }
            set
            {
                isVisibleActivateDataGrid = value;
                NotifyPropertyChanged("IsVisibleActivateDataGrid");
            }
        }
        public Visibility IsVisibleDeactivateDataGrid
        {
            get
            {
                return isVisibleDeactivateDataGrid;
            }
            set
            {
                isVisibleDeactivateDataGrid = value;
                NotifyPropertyChanged("IsVisibleDeactivateDataGrid");
            }
        }
        public Visibility IsVisibleSoldoutDataGrid
        {
            get
            {
                return isVisibleSoldoutDataGrid;
            }
            set
            {
                isVisibleSoldoutDataGrid = value;
                NotifyPropertyChanged("IsVisibleSoldoutDataGrid");
            }
        }
        public Visibility IsVisibleReturnDataGrid
        {
            get
            {
                return isVisibleReturnDataGrid;
            }
            set
            {
                isVisibleReturnDataGrid = value;
                NotifyPropertyChanged("IsVisibleReturnDataGrid");
            }
        }
        public Visibility IsVisibleSettledDataGrid
        {
            get
            {
                return isVisibleSettledDataGrid;
            }
            set
            {
                isVisibleSettledDataGrid = value;
                NotifyPropertyChanged("IsVisibleSettledDataGrid");
            }
        }
        public bool IsSoldOutBox
        {

            get
            {
                return isSoldOutBox;
            }
            set
            {
                isSoldOutBox = value;
                NotifyPropertyChanged("IsSoldOutBox");
            }
        }
        public bool IsActivedChecked
        {

            get
            {
                return isActivedChecked;
            }
            set
            {
                isActivedChecked = value;
                NotifyPropertyChanged("IsActivedChecked");
            }
        }
        public bool IsActivateBox
        {
            get
            {
                return isActivateBox;
            }
            set
            {
                isActivateBox = value;

                if (IsActivateBox == true)
                {

                    IsVisibleComboBox = Visibility.Collapsed;
                }

                else if (IsActivateBox == false)
                {
                    IsVisibleComboBox = Visibility.Visible;
                }


                NotifyPropertyChanged("IsActivateBox");
            }
        }
        public LotteryInfo LotteryInfoObj
        {
            get
            {
                return lotteryInfoObj;
            }
            set
            {
                lotteryInfoObj = value;
                NotifyPropertyChanged("LotteryInfoObj");
            }

        }
        public bool IsPopupEmptybox
        {
            get
            {
                return isPopupEmptybox;
            }
            set
            {
                isPopupEmptybox = value;
                NotifyPropertyChanged("IsPopupEmptybox");
            }
        }
        public LotteryInfo Single_Record
        {
            get
            {
                return single_Record;
            }
            set
            {
                single_Record = value;
                NotifyPropertyChanged("Single_Record");
            }
        }
        public int CountEmptyBox
        {
            get
            {
                return countEmptyBox;
            }
            set
            {
                countEmptyBox = value;
                NotifyPropertyChanged("CountEmptyBox");
            }
        }
        public int CountSettleBox
        {
            get
            {
                return countSettleBox;
            }
            set
            {
                countSettleBox = value;
                NotifyPropertyChanged("CountSettleBox");
            }
        }
        public int CountReturnBox
        {
            get
            {
                return countReturnBox;
            }
            set
            {
                countReturnBox = value;
                NotifyPropertyChanged("CountReturnBox");
            }
        }
        public int CountSoldOutBox
        {
            get
            {
                return countSoldOutBox;
            }
            set
            {
                countSoldOutBox = value;
                NotifyPropertyChanged("CountSoldOutBox");
            }
        }
        int countsoldoutbox1;
        public int CountSoldOutBox1
        {
            get
            {
                return countsoldoutbox1;
            }
            set
            {
                countsoldoutbox1 = value;
                NotifyPropertyChanged("CountSoldOutBox1");
            }
        }
        public int CountDeactiveBox
        {
            get
            {
                return countDeactiveBox;
            }
            set
            {
                countDeactiveBox = value;
                NotifyPropertyChanged("CountDeactiveBox");
            }
        }
        public int CountReceiveBox
        {
            get
            {
                return countReceiveBox;
            }
            set
            {
                countReceiveBox = value;
                NotifyPropertyChanged("CountReceiveBox");
            }
        }
        public int CountActiveBox
        {
            get
            {
                return countActiveBox;
            }
            set
            {
                countActiveBox = value;
                NotifyPropertyChanged("CountActiveBox");
            }
        }
        public int SelectedActiveBox_ForSettle
        {
            get
            {
                return selectedActiveBox_ForSettle;
            }
            set
            {
                selectedActiveBox_ForSettle = value;
                NotifyPropertyChanged("SelectedActiveBox_ForSettle");
            }
        }
        public int TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                NotifyPropertyChanged("TotalPrice");
            }
        }

        public int? ActiveTotalPrice
        {
            get
            {
                return activetotalPrice;
            }
            set
            {
                activetotalPrice = value;
                NotifyPropertyChanged("ActiveTotalPrice");
            }
        }
        public int TotalPackets
        {
            get
            {
                return totalPackets;
            }
            set
            {
                totalPackets = value;
                NotifyPropertyChanged("TotalPackets");
            }
        }
        public Activation_Box SelectedActived_Box_ForSettle
        {
            get
            {
                return selectedActived_Box_ForSettle;
            }
            set
            {
                selectedActived_Box_ForSettle = value;
                if (SelectedActived_Box_ForSettle != null)
                {
                    Settle_Obj = new Settle_Details();
                    Settle_Obj.Box_No = SelectedActived_Box_ForSettle.Box_No;
                    Settle_Obj.Game_Id = SelectedActived_Box_ForSettle.Game_Id;
                    Settle_Obj.Packet_No = SelectedActived_Box_ForSettle.Packet_No;
                    Settle_Obj.Price = SelectedActived_Box_ForSettle.Price;
                    Settle_Obj.Ticket_Name = SelectedActived_Box_ForSettle.Ticket_Name;
                    Settle_Obj.Created_Date = SelectedActived_Box_ForSettle.Created_Date;
                    Settle_Obj.Start_No = SelectedActived_Box_ForSettle.Start_No;
                    Settle_Obj.End_No = SelectedActived_Box_ForSettle.End_No;
                    Settle_Obj.Return_At = Convert.ToInt32(SelectedActived_Box_ForSettle.Return_At);
                }
                NotifyPropertyChanged("SelectedActived_Box_ForSettle");
            }
        }
        public ObservableCollection<Activation_Box> Active_BoxCollection_forSettle
        {
            get
            {
                return active_BoxCollection_forSettle;
            }
            set
            {
                active_BoxCollection_forSettle = value;
                NotifyPropertyChanged("Active_BoxCollection_forSettle");
            }
        }
        public ObservableCollection<Receive_Inventory> GetrRceiveandActive
        {
            get
            {
                return getreceiveandactive;
            }
            set
            {
                getreceiveandactive = value;
                NotifyPropertyChanged("GetrRceiveandActive");
            }
        }
        public Settle_Details Settle_Obj
        {
            get
            {
                return settle_Obj;
            }
            set
            {
                settle_Obj = value;
                NotifyPropertyChanged("Settle_Obj");
            }
        }
        public RelayCommand Settle_Ticket
        {
            get
            {
                return settle_Ticket;
            }
            set
            {
                settle_Ticket = value;
                NotifyPropertyChanged("Settle_Ticket");
            }
        }
        public bool IsSettlePopup
        {
            get
            {
                return isSettlePopup;
            }
            set
            {
                isSettlePopup = value;
                NotifyPropertyChanged("IsSettlePopup");
            }
        }
        public RelayCommand SettleCommand
        {
            get
            {
                return settleCommand;
            }
            set
            {
                settleCommand = value;
                NotifyPropertyChanged("SettleCommand");
            }
        }
        public RelayCommand SoldOutHistoryCommand
        {
            get
            {
                return soldOutHistoryCommand;
            }
            set
            {
                soldOutHistoryCommand = value;
                NotifyPropertyChanged("SoldOutHistoryCommand");
            }
        }
        public RelayCommand DeActivateHistoryCommand
        {
            get
            {
                return deActivatehistoryCommand;
            }
            set
            {
                deActivatehistoryCommand = value;
                NotifyPropertyChanged("DeActivateHistoryCommand");
            }
        }
        public RelayCommand ReturnHistoryCommand
        {
            get
            {
                return returnHistoryCommand;
            }
            set
            {
                returnHistoryCommand = value;
                NotifyPropertyChanged("ReturnHistoryCommand");
            }
        }
        public RelayCommand SettelementHistoryCommand
        {
            get
            {
                return settelmentHistoryCommand;
            }
            set
            {
                settelmentHistoryCommand = value;
                NotifyPropertyChanged("SettelementHistoryCommand");
            }
        }
        public RelayCommand EmptyHistoryCommand
        {
            get
            {
                return emptyHistoryCommand;
            }
            set
            {
                emptyHistoryCommand = value;
                NotifyPropertyChanged("EmptyHistoryCommand");
            }
        }
        public RelayCommand ActivateHistoryCommand
        {
            get
            {
                return activateHistoryCommand;
            }
            set
            {
                activateHistoryCommand = value;
                NotifyPropertyChanged("ActivateHistoryCommand");
            }
        }
        public RelayCommand ReceiveHistoryCommand
        {
            get
            {
                return receiveHistoryCommand;
            }
            set
            {
                receiveHistoryCommand = value;
                NotifyPropertyChanged("ReceiveHistoryCommand");
            }
        }
        public RelayCommand DeleteSelectedCommand
        {
            get
            {
                return deleteSelectedCommand;
            }
            set
            {
                deleteSelectedCommand = value;
                NotifyPropertyChanged("DeleteSelectedCommand");
            }
        }



        public ObservableCollection<Activation_Box> Active_BoxCollection_forReturn
        {
            get
            {
                return active_BoxCollection_forReturn;
            }
            set
            {
                active_BoxCollection_forReturn = value;
                NotifyPropertyChanged("Active_BoxCollection_forReturn");
            }
        }
        public ObservableCollection<Activation_Box> Active_BoxCollection_forSoldOut
        {
            get
            {
                return active_BoxCollection_forSoldOut;
            }
            set
            {
                active_BoxCollection_forSoldOut = value;
                NotifyPropertyChanged("Active_BoxCollection_forSoldOut");
            }
        }
        public TicketCollection Tic_Object
        {
            get
            {
                return tic_Object;
            }
            set
            {
                tic_Object = value;
                NotifyPropertyChanged("Tic_Object");
            }
        }
        public ObservableCollection<TicketCollection> Tic_Coll
        {
            get
            {
                return tic_Coll;
            }
            set
            {
                tic_Coll = value;
                NotifyPropertyChanged("Tic_Coll");
            }
        }
        public ObservableCollection<string> Empty_Ticket
        {
            get
            {
                return empty_Ticket;
            }
            set
            {
                empty_Ticket = value;
                NotifyPropertyChanged("Empty_Ticket");
            }
        }
        public ObservableCollection<LotteryInfo> LotteryColl
        {
            get
            {

                return lotteryColl;
            }
            set
            {
                lotteryColl = value;
                NotifyPropertyChanged("LotteryColl");
            }
        }
        public ObservableCollection<Activation_Box> HistoryColl
        {
            get
            {
                return historyColl;
            }
            set
            {
                historyColl = value;
                NotifyPropertyChanged("HistoryColl");
            }
        }

        public ObservableCollection<Employee_Details> EmployeeHistory
        {
            get
            {
                return employeeHistory;
            }
            set
            {
                employeeHistory = value;
                NotifyPropertyChanged("EmployeeHistory");
            }
        }

        ObservableCollection<SoldOut_Details> allinfocoll;
        public ObservableCollection<SoldOut_Details> AllInfoColl
        {
            get
            {
                return allinfocoll;
            }
            set
            {
                allinfocoll = value;
                NotifyPropertyChanged("AllInfoColl");
            }
        }
        public Activation_Box SelectedData
        {
            get
            {
                return selectedData;
            }
            set
            {
                selectedData = value;
                if (IsActivateChecked == true)
                {
                    if (SelectedData != null)
                    {
                        Single_Record = LotteryColl.Where<LotteryInfo>(i => i.Packet_No == SelectedData.Packet_No && i.Game_Id == SelectedData.Game_Id).SingleOrDefault();
                        if (Single_Record.Status == "Active" || Single_Record.Status == "Settle")
                        {
                            LotteryInfoObj.Ticket_Name = Single_Record.Ticket_Name;
                            LotteryInfoObj.Packet_No = Single_Record.Packet_No;
                            LotteryInfoObj.Game_Id = Single_Record.Game_Id;
                            LotteryInfoObj.Price = Single_Record.Price;
                            LotteryInfoObj.Start_No = Single_Record.Start_No;
                            LotteryInfoObj.End_No = Single_Record.End_No;
                            LotteryInfoObj.Count = Single_Record.Count;
                            LotteryInfoObj.Status = Single_Record.Status;
                            LotteryInfoObj.Box_No = Single_Record.Box_No;
                            LotteryInfoObj.Stopped_At = Single_Record.Stopped_At;

                            ActiveBoxObj.Game_Id = LotteryInfoObj.Game_Id;
                            ActiveBoxObj.Ticket_Name = LotteryInfoObj.Ticket_Name;
                            ActiveBoxObj.Packet_No = LotteryInfoObj.Packet_No;
                            ActiveBoxObj.Price = LotteryInfoObj.Price;
                            ActiveBoxObj.Start_No = LotteryInfoObj.Start_No;
                            ActiveBoxObj.Box_No = LotteryInfoObj.Box_No;
                            ActiveBoxObj.End_No = LotteryInfoObj.End_No;
                            ActiveBoxObj.Stopped_At = LotteryInfoObj.Stopped_At;

                            Return_Obj.Game_Id = LotteryInfoObj.Game_Id;
                            Return_Obj.Ticket_Name = LotteryInfoObj.Ticket_Name;
                            Return_Obj.Packet_No = LotteryInfoObj.Packet_No;
                            Return_Obj.Price = LotteryInfoObj.Price;
                            Return_Obj.Start_No = LotteryInfoObj.Start_No;
                            // Return_Obj.End_No = LotteryInfoObj.End_No;
                            Return_Obj.Box_No = LotteryInfoObj.Box_No;
                            Return_Obj.Count = LotteryInfoObj.Count;
                            Return_Obj.Status = "Active";

                            SoldOutObj.Game_Id = LotteryInfoObj.Game_Id;
                            SoldOutObj.Ticket_Name = LotteryInfoObj.Ticket_Name;
                            SoldOutObj.Packet_No = LotteryInfoObj.Packet_No;
                            SoldOutObj.Price = LotteryInfoObj.Price;
                            SoldOutObj.Start_No = LotteryInfoObj.Start_No;
                            SoldOutObj.Box_No = LotteryInfoObj.Box_No;

                            Settle_Obj.Game_Id = LotteryInfoObj.Game_Id;
                            Settle_Obj.Ticket_Name = LotteryInfoObj.Ticket_Name;
                            Settle_Obj.Packet_No = LotteryInfoObj.Packet_No;
                            Settle_Obj.Price = LotteryInfoObj.Price;
                            Settle_Obj.Start_No = LotteryInfoObj.Start_No;
                            Settle_Obj.Box_No = LotteryInfoObj.Box_No;
                            Settle_Obj.End_No = LotteryInfoObj.End_No;


                            IsPopupEmptybox = false;
                            IsHitTestVisible = false;
                            IsActivateBox = true;
                        }
                    }
                }

                //else if(IsReceiveChecked==true)
                //{
                //    if(SelectedData!=null)
                //    {
                //        IsActivatePopup = true;
                //        if (IsActivatePopup == true)
                //        {
                //            Active_StatusObj.Game_Id = SelectedData.Game_Id;
                //            Active_StatusObj.Packet_No = SelectedData.Packet_No;
                //            Active_StatusObj.Ticket_Name = SelectedData.Ticket_Name;
                //            Active_StatusObj.Price = SelectedData.Price;
                //            Active_StatusObj.Start_No = SelectedData.Start_No;
                //            Active_StatusObj.End_No = SelectedData.End_No;
                //        }

                //    }

                //}


                GetActivedBoxCount();

                NotifyPropertyChanged("SelectedData");
            }
        }
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                NotifyPropertyChanged("SelectedIndex");
            }
        }
        public RelayCommand Next
        {
            get
            {
                return _next;
            }

            set
            {
                _next = value;
                NotifyPropertyChanged("Next");
            }
        }
        public RelayCommand Previous
        {
            get
            {
                return _previous;
            }
            set
            {
                _previous = value;
                NotifyPropertyChanged("Previous");
            }
        }
        public RelayCommand RecieveCommand
        {
            get
            {
                return recieveCommand;
            }
            set
            {
                recieveCommand = value;
                NotifyPropertyChanged("RecieveCommand");
            }
        }
        public bool IsReceiveManuallyPopup
        {
            get
            {
                return isReceiveManuallyPopup;
            }
            set
            {
                isReceiveManuallyPopup = value;

                NotifyPropertyChanged("IsReceiveManuallyPopup");

            }
        }
        public RelayCommand Update_Lottery
        {
            get
            {
                return update_Lottery;
            }
            set
            {
                update_Lottery = value;
                NotifyPropertyChanged("Update_Lottery");
            }
        }
        public Receive_Inventory ReceiveObj
        {
            get
            {
                return receiveObj;
            }
            set
            {
                receiveObj = value;
                NotifyPropertyChanged("ReceiveObj");
            }
        }
        public ObservableCollection<Activation_Box> EmptyBoxCollction
        {
            get
            {
                return emptyBoxCollction;
            }
            set
            {
                emptyBoxCollction = value;
                NotifyPropertyChanged("EmptyBoxCollction");
            }
        }
        public ObservableCollection<Activation_Box> BoxCollection
        {
            get
            {
                return boxCollection;
            }
            set
            {
                boxCollection = value;
                NotifyPropertyChanged("BoxCollection");
            }
        }
        public RelayCommand Refresh_BoxCollection
        {
            get
            {
                return refresh_BoxCollection;
            }
            set
            {
                refresh_BoxCollection = value;
                NotifyPropertyChanged("Refresh_BoxCollection");
            }
        }
        public RelayCommand ActivateCommand
        {
            get
            {
                return activateCommand;
            }
            set
            {
                activateCommand = value;
                NotifyPropertyChanged("ActivateCommand");
            }
        }
        public RelayCommand Update_ActivatePackets
        {
            get
            {
                return update_ActivatePackets;
            }
            set
            {
                update_ActivatePackets = value;
                NotifyPropertyChanged("Update_ActivatePackets");
            }
        }
        public bool IsActivatePopup
        {
            get
            {
                return isActivatePopup;
            }
            set
            {
                isActivatePopup = value;
                NotifyPropertyChanged("IsActivatePopup");
            }
        }
        public RelayCommand Deactivatecommand
        {
            get
            {
                return deactivateCommand;
            }
            set
            {
                deactivateCommand = value;
                NotifyPropertyChanged("DeactivateCommand");
            }
        }
        public bool IsDeactivatePopup
        {
            get
            {
                return isDeactivatePopup;
            }
            set
            {
                isDeactivatePopup = value;
                NotifyPropertyChanged("IsDeactivatePopup");
            }
        }
        public bool IsHistoryPopup
        {
            get
            {
                return isHistoryPopup;
            }
            set
            {
                isHistoryPopup = value;
                NotifyPropertyChanged("IsHistoryPopup");
            }
        }
        public bool IsactivateHistoryPopup
        {
            get
            {
                return isactivateHistoryPopup;
            }
            set
            {
                isactivateHistoryPopup = value;
                NotifyPropertyChanged("IsactivateHistoryPopup");
            }
        }
        public ObservableCollection<Activation_Box> Active_BoxCollection
        {
            get
            {
                return active_BoxCollection;
            }
            set
            {
                active_BoxCollection = value;
                NotifyPropertyChanged("Active_BoxCollection");
            }
        }
        public Activation_Box SelectedActived_Box_ForReturn
        {
            get
            {
                return selectedActived_Box_ForReturn;
            }
            set
            {
                selectedActived_Box_ForReturn = value;
                if (SelectedActived_Box_ForReturn != null)
                {
                    Return_Obj = new Return_Details();

                    Return_Obj.Box_No = SelectedActived_Box_ForReturn.Box_No;
                    Return_Obj.Game_Id = SelectedActived_Box_ForReturn.Game_Id;
                    Return_Obj.Packet_No = SelectedActived_Box_ForReturn.Packet_No;
                    Return_Obj.Price = SelectedActived_Box_ForReturn.Price;
                    Return_Obj.Ticket_Name = SelectedActived_Box_ForReturn.Ticket_Name;
                    Return_Obj.Start_No = SelectedActived_Box_ForReturn.Start_No;
                    //Return_Obj.End_No = SelectedActived_Box_ForReturn.End_No;
                }
                NotifyPropertyChanged("SelectedActived_Box_ForReturn");
            }
        }
        public int SelectedActiveBox_ForReturn
        {
            get
            {
                return selectedActiveBox_ForReturn;
            }
            set
            {
                selectedActiveBox_ForReturn = value;
                NotifyPropertyChanged("SelectedActiveBox_ForReturn");
            }
        }
        public Return_Details Return_Obj
        {
            get
            {
                return return_Obj;
            }
            set
            {
                return_Obj = value;
                NotifyPropertyChanged("Return_Obj");
            }
        }
        public RelayCommand Return_Ticket
        {
            get
            {
                return return_Ticket;
            }
            set
            {
                return_Ticket = value;
                NotifyPropertyChanged("Return_Ticket");
            }
        }
        public RelayCommand ReturnCommand
        {
            get
            {
                return returnCommand;
            }
            set
            {
                returnCommand = value;
                NotifyPropertyChanged("ReturnCommand");
            }
        }
        public bool IsReturnPopup
        {
            get
            {
                return isReturnPopup;
            }
            set
            {
                isReturnPopup = value;
                NotifyPropertyChanged("IsReturnPopup");
            }
        }
        public SoldOut_Details SoldOutObj
        {
            get
            {
                return soldOutObj;
            }
            set
            {
                soldOutObj = value;
                NotifyPropertyChanged("SoldOutObj");
            }
        }
        public RelayCommand SoldOut_Ticket
        {
            get
            {
                return soldOut_Ticket;
            }
            set
            {
                soldOut_Ticket = value;
                NotifyPropertyChanged("SoldOut_Ticket");
            }
        }
        public RelayCommand SoldOutCommand
        {
            get
            {
                return soldOutCommand;
            }
            set
            {
                soldOutCommand = value;
                NotifyPropertyChanged("SoldOutCommand");
            }
        }
        public Activation_Box SelectedActived_Box_ForSoldOut
        {
            get
            {
                return selectedActived_Box_ForSoldOut;
            }
            set
            {
                selectedActived_Box_ForSoldOut = value;
                if (SelectedActived_Box_ForSoldOut != null)
                {
                    SoldOutObj = new SoldOut_Details();
                    SoldOutObj.Box_No = SelectedActived_Box_ForSoldOut.Box_No;
                    SoldOutObj.Game_Id = SelectedActived_Box_ForSoldOut.Game_Id;
                    SoldOutObj.Packet_No = SelectedActived_Box_ForSoldOut.Packet_No;
                    SoldOutObj.Price = SelectedActived_Box_ForSoldOut.Price;
                    SoldOutObj.Ticket_Name = SelectedActived_Box_ForSoldOut.Ticket_Name;
                    SoldOutObj.Start_No = SelectedActived_Box_ForSoldOut.Start_No;
                    SoldOutObj.End_No = SelectedActived_Box_ForSoldOut.End_No;

                }
                NotifyPropertyChanged("SelectedActived_Box_ForSoldOut");
            }
        }
        public int SelectedActiveBox_ForSoldOut
        {
            get
            {
                return selectedActiveBox_ForSoldOut;
            }
            set
            {
                selectedActiveBox_ForSoldOut = value;
                NotifyPropertyChanged("SelectedActiveBox_ForSoldOut");
            }
        }
        public bool IsSoldOutPopup
        {
            get
            {
                return isSoldOutPopup;
            }
            set
            {
                isSoldOutPopup = value;
                NotifyPropertyChanged("IsSoldOutPopup");
            }
        }
        public bool IsBoxClosePopup
        {
            get
            {
                return isBoxClosePopup;
            }
            set
            {
                isBoxClosePopup = value;
                NotifyPropertyChanged("IsBoxClosePopup");
            }
        }
        public Activation_Box SelectedBox
        {
            get
            {
                return selectedBox;
            }
            set
            {
                selectedBox = value;
                // Active_StatusObj = new Activate_Ticket();
                if (SelectedBox != null)
                {
                    Active_StatusObj.Box_No = SelectedBox.Box_No;
                    //Active_StatusObj.Game_Id = SelectedBox.Game_Id;
                    //Active_StatusObj.Packet_No = SelectedBox.Packet_No;
                    //Active_StatusObj.Price = SelectedBox.Price;
                    //Active_StatusObj.Ticket_Name = SelectedBox.Ticket_Name;
                    //Active_StatusObj.Stopped_At = SelectedBox.Stopped_At;
                }
                NotifyPropertyChanged("SelectedBox");
            }
        }
        public int SelectedBoxNo
        {
            get
            {
                return selectedBoxNo;
            }
            set
            {
                selectedBoxNo = value;
                NotifyPropertyChanged("SelectedBoxNo");
            }
        }
        public Activation_Box SelectedActived_Box
        {
            get
            {
                return selectedActived_Box;
            }
            set
            {
                selectedActived_Box = value;
                if (SelectedActived_Box != null)
                {
                    ActiveBoxObj = new Activate_Ticket();
                    ActiveBoxObj.Box_No = SelectedActived_Box.Box_No;
                    ActiveBoxObj.Game_Id = SelectedActived_Box.Game_Id;
                    ActiveBoxObj.Packet_No = SelectedActived_Box.Packet_No;
                    ActiveBoxObj.Price = SelectedActived_Box.Price;
                    ActiveBoxObj.Ticket_Name = SelectedActived_Box.Ticket_Name;
                    ActiveBoxObj.Start_No = SelectedActived_Box.Start_No;
                    ActiveBoxObj.End_No = SelectedActived_Box.End_No;
                    ActiveBoxObj.Stopped_At = SelectedActived_Box.Stopped_At;
                }
                NotifyPropertyChanged("SelectedActived_Box");
            }
        }
        public int SelectedActiveBox
        {
            get
            {
                return selectedActiveBox;
            }
            set
            {
                selectedActiveBox = value;
                NotifyPropertyChanged("SelectedActiveBox");
            }
        }
        public RelayCommand Activate_TicketCommand
        {
            get
            {
                return activate_TicketCommand;
            }
            set
            {
                activate_TicketCommand = value;
                NotifyPropertyChanged("Activate_TicketCommand");
            }
        }
        public Activate_Ticket Active_StatusObj
        {
            get
            {
                return active_statusObj;
            }
            set
            {
                active_statusObj = value;
                NotifyPropertyChanged("Active_StatusObj");
            }
        }
        public Activate_Ticket ActiveBoxObj
        {
            get
            {
                return activeBoxObj;
            }
            set
            {
                activeBoxObj = value;
                NotifyPropertyChanged("ActiveBoxObj");
            }
        }
        public RelayCommand Deactivate_Ticket
        {
            get
            {
                return deactivate_Ticket;
            }
            set
            {
                deactivate_Ticket = value;
                NotifyPropertyChanged("Deactivate_Ticket");
            }
        }
        public int ActivateCount
        {
            get
            {
                return activateCount;
            }
            set
            {
                activateCount = value;
                NotifyPropertyChanged("ActivateCount");
            }
        }
        public Visibility IsVisiblebtnGrid
        {
            get
            {
                return isVisiblebtnGrid;
            }
            set
            {
                isVisiblebtnGrid = value;
                NotifyPropertyChanged("IsVisiblebtnGrid");
            }
        }
        public Visibility IsVisiblecalendarGrid
        {
            get
            {
                return isVisiblecalendarGrid;
            }
            set
            {
                isVisiblecalendarGrid = value;
                NotifyPropertyChanged("IsVisiblecalendarGrid");
            }
        }
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
                NotifyPropertyChanged("IsValid");
            }
        }
        public Visibility IsVisibleComboBox
        {
            get
            {
                return isVisibleComboBox;
            }

            set
            {
                isVisibleComboBox = value;
                NotifyPropertyChanged("IsVisibleComboBox");
            }
        }
        public Visibility IsVisibleTextBox
        {
            get
            {
                return isVisibleTextBox;
            }

            set
            {
                isVisibleTextBox = value;
                NotifyPropertyChanged("IsVisibleTexetBox");
            }
        }
        public int? Displayboxno
        {
            get
            {
                return displayboxno;
            }

            set
            {
                displayboxno = value;
                NotifyPropertyChanged("Displayboxno");
            }
        }
        public Visibility IsVisibleActiveBoxNo
        {
            get
            {
                return isVisibleActiveBoxNo;
            }

            set
            {
                isVisibleActiveBoxNo = value;
                NotifyPropertyChanged("IsVisibleActiveBoxNo");
            }
        }
        public Visibility IsVisibleActiveComboBox
        {
            get
            {
                return isVisibleActiveComboBox;
            }

            set
            {
                isVisibleActiveComboBox = value;
                NotifyPropertyChanged("IsVisibleActiveComboBox");
            }
        }
        public bool IsContentChecked
        {
            get
            {
                return isContentChecked;
            }

            set
            {
                isContentChecked = value;

                if (IsContentChecked == true)
                {
                    IsVisibleComboBox = Visibility.Collapsed;
                }

                else
                {
                    IsVisibleComboBox = Visibility.Visible;
                }

                NotifyPropertyChanged("IsContentChecked");
            }
        }
        public DateTime OpenDate
        {
            get
            {
                return openDate;
            }

            set
            {
                openDate = value;
                NotifyPropertyChanged("OpenDate");
            }
        }
        public string Date
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
        public string Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
                NotifyPropertyChanged("Time");
            }
        }
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }
        public bool IsAddInventoryPopup
        {
            get
            {
                return isAddInventoryPopup;
            }

            set
            {
                isAddInventoryPopup = value;
                NotifyPropertyChanged("IsAddInventoryPopup");
            }
        }
        public ObservableCollection<Master_List_Inventory> MasterColl
        {
            get
            {
                return masterColl;
            }

            set
            {
                masterColl = value;
                NotifyPropertyChanged("MasterColl");
            }
        }
        public Activation_Box ReceiveselectedData
        {
            get
            {
                return receiveselectedData;
            }

            set
            {
                receiveselectedData = value;
                if (ReceiveselectedData != null)
                {
                   
                    Active_StatusObj.Game_Id = ReceiveselectedData.Game_Id;
                    Active_StatusObj.Packet_No = ReceiveselectedData.Packet_No;
                    Active_StatusObj.Ticket_Name = ReceiveselectedData.Ticket_Name;
                    Active_StatusObj.Price = ReceiveselectedData.Price;
                    Active_StatusObj.Start_No = ReceiveselectedData.Start_No;
                    Active_StatusObj.End_No = ReceiveselectedData.End_No;


                    Return_Obj.Game_Id = ReceiveselectedData.Game_Id;
                    Return_Obj.Packet_No = ReceiveselectedData.Packet_No;
                    Return_Obj.Ticket_Name = ReceiveselectedData.Ticket_Name;
                    Return_Obj.Price = ReceiveselectedData.Price;
                    Return_Obj.Start_No = ReceiveselectedData.Start_No;
                    Return_Obj.End_No = ReceiveselectedData.End_No;
                    LotteryInfoObj.Count = ReceiveselectedData.Count;

                    Return_Obj.Status = "Receive";
                    IsShowActivateReturnPopup = true;
                    IsHitTestVisible = false;
                }

                NotifyPropertyChanged("ReceiveselectedData");
            }
        }
        public bool IsShowActivateReturnPopup
        {
            get
            {
                return isShowActivateReturnPopup;
            }

            set
            {
                isShowActivateReturnPopup = value;

                NotifyPropertyChanged("IsShowActivateReturnPopup");
            }
        }
        public List<string> StateCollection
        {
            get
            {
                return stateCollection;
            }

            set
            {
                stateCollection = value;
                NotifyPropertyChanged("StateCollection");
            }
        }
        public ObservableCollection<StateClass> StateColl
        {
            get
            {
                return stateColl;
            }

            set
            {
                stateColl = value;
                NotifyPropertyChanged("StateColl");
            }
        }
        public StateClass State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
                NotifyPropertyChanged("State");
                if (State.Name != null)
                {
                    ApplicationData.SelectedState = State.Name;
                }
            }
        }
        public ObservableCollection<Activation_Box> ActivatehistoryColl
        {
            get
            {
                return activatehistoryColl;
            }

            set
            {
                activatehistoryColl = value;
                NotifyPropertyChanged("ActivatehistoryColl");
            }
        }
        public ObservableCollection<Activation_Box> DeactivatehistoryColl
        {
            get
            {
                return deactivatehistoryColl;
            }

            set
            {
                deactivatehistoryColl = value;
                NotifyPropertyChanged("DeactivatehistoryColl");
            }
        }
        public ObservableCollection<Activation_Box> SoldouthistoryColl
        {
            get
            {
                return soldouthistoryColl;
            }

            set
            {
                soldouthistoryColl = value;
                NotifyPropertyChanged("SoldouthistoryColl");
            }
        }
        public ObservableCollection<Activation_Box> ReturnhistoryColl
        {
            get
            {
                return returnhistoryColl;
            }

            set
            {
                returnhistoryColl = value;
                NotifyPropertyChanged("ReturnhistoryColl");
            }
        }
        public ObservableCollection<Activation_Box> SettleHistoryColl
        {
            get
            {
                return settleHistoryColl;
            }

            set
            {
                settleHistoryColl = value;
                NotifyPropertyChanged("SettleHistoryColl");
            }
        }
        public StateClass State_Details_Obj
        {
            get
            {
                return state_Details_Obj;
            }

            set
            {
                state_Details_Obj = value;
                // State_Details_Obj.Name = State.Name;
                NotifyPropertyChanged("State_Details_Obj");
            }
        }
        public ObservableCollection<Activation_Box> HistoryCollReceive
        {
            get
            {
                return historyCollReceive;
            }

            set
            {
                historyCollReceive = value;
                NotifyPropertyChanged("HistoryCollReceive");
            }
        }

        public bool IsCloseShiftActivateBox
        {
            get
            {
                return isCloseShiftActivateBox;
            }

            set
            {
                isCloseShiftActivateBox = value;
                NotifyPropertyChanged("IsCloseShiftActivateBox");
            }
        }

        public bool IsReportPopup
        {
            get
            {
                return isReportPopup;
            }

            set
            {
                isReportPopup = value;
                NotifyPropertyChanged("IsReportPopup");
            }
        }

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                NotifyPropertyChanged("Location");
            }
        }

        public bool IsDailyReportPopup
        {
            get
            {
                return isDailyReportPopup;
            }

            set
            {
                isDailyReportPopup = value;
                NotifyPropertyChanged("IsDailyReportPopup");
            }
        }

        public RelayCommand ShowDailyReport
        {
            get
            {
                return showDailyReport;
            }

            set
            {
                showDailyReport = value;
                NotifyPropertyChanged("ShowDailyReport");
            }
        }

        public ObservableCollection<Activation_Box> DailyReportActiveColl
        {
            get
            {
                return dailyReportActiveCreoll;
            }

            set
            {
                dailyReportActiveCreoll = value;
                NotifyPropertyChanged("DailyReportActiveCreoll");
            }
        }

        public int? Total
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

        public RelayCommand CloseBoxCommand
        {
            get
            {
                return closeBoxCommand;
            }

            set
            {
                closeBoxCommand = value;
                NotifyPropertyChanged("CloseBoxCommand");
            }
        }

        public RelayCommand CloseBox
        {
            get
            {
                return closeBox;
            }

            set
            {
                closeBox = value;
                NotifyPropertyChanged("CloseBox");
            }
        }

        public Close_Box ObjCloseBox
        {
            get
            {
                return objCloseBox;
            }

            set
            {
                objCloseBox = value;
                NotifyPropertyChanged("ObjCloseBox");
            }
        }

        public bool IsCloseBoxReopen
        {
            get
            {
                return isCloseBoxReopen;
            }

            set
            {
                isCloseBoxReopen = value;
                NotifyPropertyChanged("IsCloseBoxReopen");
            }
        }

        public RelayCommand ReopenBox
        {
            get
            {
                return reopenBox;
            }

            set
            {
                reopenBox = value;
                NotifyPropertyChanged("ReopenBox");
            }
        }

        public RelayCommand SaveTerminalData
        {
            get
            {
                return saveTerminalData;
            }

            set
            {
                saveTerminalData = value;
                NotifyPropertyChanged("SaveTerminalData");
            }
        }

        public Terminal_Details TerminalObj
        {
            get
            {
                return terminalObj;
            }

            set
            {
                terminalObj = value;
                NotifyPropertyChanged("TerminalObj");
            }
        }

        public bool IsDataTerminalPopup
        {
            get
            {
                return isDataTerminalPopup;
            }

            set
            {
                isDataTerminalPopup = value;
                NotifyPropertyChanged("IsDataTerminalPopup");
            }
        }

        public ObservableCollection<Terminal_Details> TerminalDataCollection
        {
            get
            {
                return terminalDataCollection;
            }

            set
            {
                terminalDataCollection = value;
                NotifyPropertyChanged("TerminalDataCollection");
            }
        }

        public int CountActiveReceive
        {
            get
            {
                return countActiveReceive;
            }

            set
            {
                countActiveReceive = value;
                NotifyPropertyChanged("CountActiveReceive");
            }
        }

        public int? TotalSells
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

        public int TotalPayOut
        {
            get
            {
                return totalPayOut;
            }

            set
            {
                totalPayOut = value;
                NotifyPropertyChanged("TotalPayOut");
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

        public RelayCommand TbDashboard
        {
            get
            {
                return tbDashboard;
            }

            set
            {
                tbDashboard = value;
                NotifyPropertyChanged("TbDashboard");
            }
        }

        public Visibility IsVisibleMainPage
        {
            get
            {
                return isVisibleMainPage;
            }

            set
            {
                isVisibleMainPage = value;
                NotifyPropertyChanged("IsVisibleMainPage");
            }
        }



        public Visibility IsVisibleShiftReport
        {
            get
            {
                return isVisibleShiftReport;
            }

            set
            {
                isVisibleShiftReport = value;
                NotifyPropertyChanged("IsVisibleShiftReport");
            }
        }

        public Visibility IsVisibleGenerateReport
        {
            get
            {
                return isVisibleGenerateReport;
            }

            set
            {
                isVisibleGenerateReport = value;
                NotifyPropertyChanged("IsVisibleGenerateReport");
            }
        }

        public Visibility IsBtnSaveVisible
        {
            get
            {
                return isBtnSaveVisible;
            }

            set
            {
                isBtnSaveVisible = value;
                NotifyPropertyChanged("IsBtnSaveVisible");
            }
        }

        public RelayCommand GenerateReport
        {
            get
            {
                return generateReport;
            }

            set
            {
                generateReport = value;
                NotifyPropertyChanged("GenerateReport");
            }
        }

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

        public ObservableCollection<Activate_Ticket> ActiveTicketTotal
        {
            get
            {
                return activeTicketTotal;
            }

            set
            {
                activeTicketTotal = value;
                NotifyPropertyChanged("ActiveTicketTotal");
            }
        }

        public ObservableCollection<Store_Info> StoreCollection
        {
            get
            {
                return storeCollection;
            }

            set
            {
                storeCollection = value;
                NotifyPropertyChanged("StoreCollection");
            }
        }

        public Store_Info StoreObj
        {
            get
            {
                return storeObj;
            }

            set
            {
                storeObj = value;
                NotifyPropertyChanged("StoreObj");
            }
        }

        public Store_Info SelectedStore
        {
            get
            {
                return selectedStore;
            }

            set
            {
                selectedStore = value;
                NotifyPropertyChanged("SelectedStore");
            }
        }

        public Store_Info Store_Info_Obj
        {
            get
            {
                return store_Info_Obj;
            }

            set
            {
                int flag = 0;
                if (flag == 0 && Store_Info_Obj != null)
                {
                    flag = 1;
                    store_Info_Obj = value;
                    NotifyPropertyChanged("Store_Info_Obj");
                }
            }
        }

        public Shift_Details ShiftObj
        {
            get
            {
                return shiftObj;
            }

            set
            {
                shiftObj = value;
                NotifyPropertyChanged("ShiftObj");
            }
        }

        public Terminal_Details DailyTotal
        {
            get
            {
                return dailyTotal;
            }

            set
            {
                dailyTotal = value;
                NotifyPropertyChanged("DailyTotal");
            }
        }

        public Visibility IsVisibleShiftSubmit
        {
            get
            {
                return isVisibleShiftSubmit;
            }

            set
            {
                isVisibleShiftSubmit = value;
                NotifyPropertyChanged("IsVisibleShiftSubmit");
            }
        }

        public RelayCommand TBDailyReport
        {
            get
            {
                return tBDailyReport;
            }

            set
            {
                tBDailyReport = value;
                NotifyPropertyChanged("TBDailyReport");
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
                NotifyPropertyChanged("TerminalColl");
            }
        }

        public int TotScratchsell
        {
            get
            {
                return totScratchsell;
            }

            set
            {
                totScratchsell = value;
                NotifyPropertyChanged("TotScratchsell");
            }
        }

        public int TotScratchPayout
        {
            get
            {
                return totScratchPayout;
            }

            set
            {
                totScratchPayout = value;
                NotifyPropertyChanged("TotScratchPayout");
            }
        }

        public int TotOnlineSells
        {
            get
            {
                return totOnlineSells;
            }

            set
            {
                totOnlineSells = value;
                NotifyPropertyChanged("TotOnlineSells");
            }
        }

        public int TotOnlinePayout
        {
            get
            {
                return totOnlinePayout;
            }

            set
            {
                totOnlinePayout = value;
                NotifyPropertyChanged("TotOnlinePayout");
            }
        }

        public int TotTrackedAmount
        {
            get
            {
                return totTrackedAmount;
            }

            set
            {
                totTrackedAmount = value;
                NotifyPropertyChanged("TotTrackedAmount");
            }
        }


        public Terminal_Details HamburgerSelectedData
        {
            get
            {
                return hamburgerSelectedData;
            }

            set
            {
                hamburgerSelectedData = value;
                HamburgerTime = null;
                if (HamburgerSelectedData != null)
                {

                    IsDailyReportUserName = Visibility.Collapsed;
                    IsDailyReportHamburgerName = Visibility.Visible;
                    IsDailyReportDateHamburger = Visibility.Visible;
                    IsDailyReportDate = Visibility.Collapsed;
                    IsDailyReportTimeHamburger = Visibility.Visible;
                    IsDailyReportTime = Visibility.Collapsed;
                    SoldOutObj = new SoldOut_Details();
                    t1 = HamburgerSelectedData.Date;

                    DailyDate = HamburgerSelectedData.Date.ToString("MM/dd/yyyy");

                    ObservableCollection<Login> empcoll = new ObservableCollection<Login>();
                    ObservableCollection<Shift_Details> shiftcoll = new ObservableCollection<Shift_Details>();

                    HttpResponseMessage shiftdet = client.GetAsync("api/Login/GetShiftDetails").Result;
                    var emp1 = shiftdet.Content.ReadAsStringAsync().Result;
                    shiftcoll = JsonConvert.DeserializeObject<ObservableCollection<Shift_Details>>(emp1);

                    var a = shiftcoll.Where(x => x.Date == HamburgerSelectedData.Date).ToList().LastOrDefault();
                    if (a != null)
                    {
                        if (a.IsClose == false)
                        {
                            HamburgerTime = System.DateTime.Now.ToString("hh:mm tt");
                        }
                        else
                        {
                            HamburgerTime = a.EndTime;
                        }

                    }
                    HttpResponseMessage empdet = client.GetAsync("api/Login/GetEmployeeDetails").Result;
                    var emp = empdet.Content.ReadAsStringAsync().Result;
                    empcoll = JsonConvert.DeserializeObject<ObservableCollection<Login>>(emp);
                    if (a != null)
                    {
                        var c = empcoll.Where(x => x.EmployeeId == a.EmployeeId).FirstOrDefault();
                        if (c != null)
                        {
                            User = c.Username;
                        }
                    }
                }
                NotifyPropertyChanged("HamburgerSelectedData");
            }
        }

        public string User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
                NotifyPropertyChanged("User");
            }
        }

        public RelayCommand TBShiftReport
        {
            get
            {
                return tBShiftReport;
            }

            set
            {
                tBShiftReport = value;
                NotifyPropertyChanged("TBShiftReport");
            }
        }

        public ObservableCollection<Shift_Details> ShiftReport
        {
            get
            {
                return shiftReport;
            }

            set
            {
                shiftReport = value;
                NotifyPropertyChanged("ShiftReport");
            }
        }

        public ObservableCollection<Shift_Details> StoreDate
        {
            get
            {
                return storeDate;
            }

            set
            {
                storeDate = value;
                NotifyPropertyChanged("StoreDate");
            }
        }

        public ObservableCollection<Main_Shift_Collection> MainShiftReportColl
        {
            get
            {
                return mainShiftReportColl;
            }

            set
            {
                mainShiftReportColl = value;
                NotifyPropertyChanged("MainShiftReportColl");
            }
        }

        public ObservableCollection<Shift_Details> TempColl
        {
            get
            {
                return tempColl;
            }

            set
            {
                tempColl = value;
                NotifyPropertyChanged("TempColl");
            }
        }

        public ObservableCollection<Shift_Details> Temp1Coll
        {
            get
            {
                return temp1Coll;
            }

            set
            {
                temp1Coll = value;
                NotifyPropertyChanged("Temp1Coll");
            }
        }
        public string Shiftdate
        {
            get
            {
                return shiftdate;
            }

            set
            {
                shiftdate = value;
                NotifyPropertyChanged("Shiftdate");
            }
        }

        public Shift_Details ShiftReportSelectedData
        {
            get
            {
                return shiftReportSelectedData;
            }

            set
            {
                shiftReportSelectedData = value;
                if (ShiftReportSelectedData != null)
                {
                    IsShiftReportDetail = Visibility.Collapsed;
                    IsShiftReportHamburger = Visibility.Visible;
                    IsDataFromLotteryAppHamburger = Visibility.Visible;
                    IsDataFromLotteryApp = Visibility.Collapsed;
                    IsDataFromTerminalHamburger = Visibility.Visible;
                    IsDataFromTerminal = Visibility.Collapsed;
                    IsActiveAndStockInfoHamburger = Visibility.Visible;
                    IsActiveAndStockInfo = Visibility.Collapsed;
                    IsUserShiftReport = Visibility.Collapsed;
                    IsUserShiftReportHamburger = Visibility.Visible;
                    IsDateShiftReport = Visibility.Collapsed;
                    IsDateshiftReportHamburger = Visibility.Visible;
                    IsCloseTimeShiftReport = Visibility.Collapsed;
                    IsCloseTimeShiftReportHamburger = Visibility.Visible;
                    IsTotalStockActiveInventory = Visibility.Collapsed;
                    IsTotalStockActiveInventoryHamburger = Visibility.Visible;
                    TerminalObj = new Terminal_Details();
                    Id = Convert.ToInt32(ShiftReportSelectedData.ShiftId);
                    OnTerminalDataCollection();
                    var result = TerminalDataCollection.Where(x => x.EmployeeID == ShiftReportSelectedData.EmployeeId && x.Date == ShiftReportSelectedData.Date).LastOrDefault();

                    if (result != null)
                    {
                        TerminalObj.ShiftID = Convert.ToInt32(ShiftReportSelectedData.ShiftId);
                        TerminalObj.OnlinePayout = result.OnlinePayout;
                        TerminalObj.OnlineSells = result.OnlineSells;
                        TerminalObj.ScratchPayout = result.ScratchPayout;
                        TerminalObj.ScratchSells = result.ScratchSells;
                        TerminalObj.TotalSells = result.TotalSells;
                        TerminalObj.TotalPayout = result.TotalPayout;
                        TerminalObj.Loan = result.Loan;
                        TerminalObj.CashOnHand = result.CashOnHand;
                        TerminalObj.NetCash = result.NetCash;
                        //TerminalObj.ActualCash = (Convert.ToInt32(result.ScratchSells) + Convert.ToInt32(result.OnlineSells) - Convert.ToInt32(result.ScratchPayout) - Convert.ToInt32(result.OnlinePayout) - Convert.ToInt32(result.Loan)).ToString();
                        TerminalObj.Credit = result.Credit;
                        TerminalObj.Debit = result.Debit;
                        TerminalObj.TopUp = result.TopUp;
                        TerminalObj.TopUPCancel = result.TopUPCancel;
                        TerminalObj.TrackedAmount = (Total + Convert.ToInt32(result.OnlineSells) - Convert.ToInt32(result.ScratchPayout) - Convert.ToInt32(result.OnlinePayout)).ToString();

                        TerminalObj.ShortOver = result.ShortOver;
                        TerminalObj.Date = result.Date;
                        TerminalObj.CountRecevied = result.CountRecevied;
                        TerminalObj.CountActive = result.CountActive;
                        TerminalObj.TotalActiveReceviedStock = result.TotalActiveReceviedStock;
                        TerminalObj.InstockInventory = result.InstockInventory;
                        TerminalObj.ActiveInventory = result.ActiveInventory;
                        TerminalObj.CountTerminalActiveReceive = result.CountTerminalActiveReceive;
                        TerminalObj.ShortOverStock = result.ShortOverStock;
                        TerminalObj.ShortOverActive = result.ShortOverActive;
                        TerminalObj.TotalStockInventory = result.TotalStockInventory;
                        TerminalObj.TotalActiveInventory = result.TotalActiveInventory;
                    }
                }
                NotifyPropertyChanged("ShiftReportSelectedData");
            }
        }

        public ObservableCollection<Terminal_Details> TerminalDailyReport
        {
            get
            {
                return terminalDailyReport;
            }

            set
            {
                terminalDailyReport = value;
                NotifyPropertyChanged("TerminalDailyReport");
            }
        }

        public Visibility IsShiftReportDetail
        {
            get
            {
                return isShiftReportDetail;
            }

            set
            {
                isShiftReportDetail = value;
                NotifyPropertyChanged("IsShiftReportDetail");
            }
        }

        public Visibility IsShiftReportHamburger
        {
            get
            {
                return isShiftReportHamburger;
            }

            set
            {
                isShiftReportHamburger = value;
                NotifyPropertyChanged("IsShiftReportHamburger");
            }
        }

        public Visibility IsDataFromLotteryApp
        {
            get
            {
                return isDataFromLotteryApp;
            }

            set
            {
                isDataFromLotteryApp = value;
                NotifyPropertyChanged("IsDataFromLotteryApp");
            }
        }

        public Visibility IsDataFromTerminal
        {
            get
            {
                return isDataFromTerminal;
            }

            set
            {
                isDataFromTerminal = value;
                NotifyPropertyChanged("IsDataFromTerminal");
            }
        }

        public Visibility IsActiveAndStockInfo
        {
            get
            {
                return isActiveAndStockInfo;
            }

            set
            {
                isActiveAndStockInfo = value;
                NotifyPropertyChanged("IsActiveAndStockInfo");
            }
        }

        public Visibility IsDataFromLotteryAppHamburger
        {
            get
            {
                return isDataFromLotteryAppHamburger;
            }

            set
            {
                isDataFromLotteryAppHamburger = value;
                NotifyPropertyChanged("IsDataFromLotteryAppHamburger");
            }
        }

        public Visibility IsDataFromTerminalHamburger
        {
            get
            {
                return isDataFromTerminalHamburger;
            }

            set
            {
                isDataFromTerminalHamburger = value;
                NotifyPropertyChanged("IsDataFromTerminalHamburger");
            }
        }

        public Visibility IsActiveAndStockInfoHamburger
        {
            get
            {
                return isActiveAndStockInfoHamburger;
            }

            set
            {
                isActiveAndStockInfoHamburger = value;
                NotifyPropertyChanged("IsActiveAndStockInfoHamburger");
            }
        }

        public Visibility IsUserShiftReport
        {
            get
            {
                return isUserShiftReport;
            }

            set
            {
                isUserShiftReport = value;
                NotifyPropertyChanged("IsUserShiftReport");
            }
        }

        public Visibility IsUserShiftReportHamburger
        {
            get
            {
                return isUserShiftReportHamburger;
            }

            set
            {
                isUserShiftReportHamburger = value;
                NotifyPropertyChanged("IsUserShiftReportHamburger");
            }
        }

        public Visibility IsDateShiftReport
        {
            get
            {
                return isDateShiftReport;
            }

            set
            {
                isDateShiftReport = value;
                NotifyPropertyChanged("IsDateShiftReport");
            }
        }

        public Visibility IsDateshiftReportHamburger
        {
            get
            {
                return isDateshiftReportHamburger;
            }

            set
            {
                isDateshiftReportHamburger = value;
                NotifyPropertyChanged("IsDateshiftReportHamburger");
            }
        }

        public Visibility IsCloseTimeShiftReport
        {
            get
            {
                return isCloseTimeShiftReport;
            }

            set
            {
                isCloseTimeShiftReport = value;
                NotifyPropertyChanged("IsCloseTimeShiftReport");
            }
        }

        public Visibility IsCloseTimeShiftReportHamburger
        {
            get
            {
                return isCloseTimeShiftReportHamburger;
            }

            set
            {
                isCloseTimeShiftReportHamburger = value;
                NotifyPropertyChanged("IsCloseTimeShiftReportHamburger");
            }
        }

        public bool IsHitTestVisible
        {
            get
            {
                return isHitTestVisible;
            }

            set
            {
                isHitTestVisible = value;
                NotifyPropertyChanged("IsHitTestVisible");
            }
        }

        public bool IsHitTestVisiblePopup
        {
            get
            {
                return isHitTestVisiblePopup;
            }

            set
            {
                isHitTestVisiblePopup = value;
                NotifyPropertyChanged("IsHitTestVisiblePopup");
            }
        }

        public Visibility IsTotalStockActiveInventory
        {
            get
            {
                return isTotalStockActiveInventory;
            }

            set
            {
                isTotalStockActiveInventory = value;
                NotifyPropertyChanged("IsTotalStockActiveInventory");
            }
        }

        public Visibility IsTotalStockActiveInventoryHamburger
        {
            get
            {
                return isTotalStockActiveInventoryHamburger;
            }

            set
            {
                isTotalStockActiveInventoryHamburger = value;
                NotifyPropertyChanged("IsTotalStockActiveInventoryHamburger");
            }
        }

        public ObservableCollection<Terminal_Details> DailyReport
        {
            get
            {
                return dailyReport;
            }

            set
            {
                dailyReport = value;
                NotifyPropertyChanged("DailyReport");
            }
        }

        public ObservableCollection<Terminal_Details> MainDailyReportColl
        {
            get
            {
                return mainDailyReportColl;
            }

            set
            {
                mainDailyReportColl = value;
                NotifyPropertyChanged("MainDailyReportColl");
            }
        }

        public int TotDateScratchsells
        {
            get
            {
                return totDateScratchsells;
            }

            set
            {
                totDateScratchsells = value;
                NotifyPropertyChanged("TotDateScratchsells");
            }
        }

        public int TotDateScratchPayout
        {
            get
            {
                return totDateScratchPayout;
            }

            set
            {
                totDateScratchPayout = value;
                NotifyPropertyChanged("TotDateScratchPayout");
            }
        }

        public int TotDateOnlineSells
        {
            get
            {
                return totDateOnlineSells;
            }

            set
            {
                totDateOnlineSells = value;
                NotifyPropertyChanged("TotDateOnlineSells");
            }
        }

        public int TotDateOnlinePayout
        {
            get
            {
                return totDateOnlinePayout;
            }

            set
            {
                totDateOnlinePayout = value;
                NotifyPropertyChanged("TotDateOnlinePayout");
            }
        }

        public int TotDateTrackedAmount
        {
            get
            {
                return totDateTrackedAmount;
            }

            set
            {
                totDateTrackedAmount = value;
                NotifyPropertyChanged("TotDateTrackedAmount");
            }
        }

        public Visibility IsDailyReportUserName
        {
            get
            {
                return isDailyReportUserName;
            }

            set
            {
                isDailyReportUserName = value;
                NotifyPropertyChanged("IsDailyReportUserName");
            }
        }

        public Visibility IsDailyReportHamburgerName
        {
            get
            {
                return isDailyReportHamburgerName;
            }

            set
            {
                isDailyReportHamburgerName = value;
                NotifyPropertyChanged("IsDailyReportHamburgerName");
            }
        }

        public Visibility IsDailyReportDate
        {
            get
            {
                return isDailyReportDate;
            }

            set
            {
                isDailyReportDate = value;
                NotifyPropertyChanged("IsDailyReportDate");
            }
        }

        public Visibility IsDailyReportDateHamburger
        {
            get
            {
                return isDailyReportDateHamburger;
            }

            set
            {
                isDailyReportDateHamburger = value;
                NotifyPropertyChanged("IsDailyReportDateHamburger");
            }
        }

        public Visibility IsDailyReportTime
        {
            get
            {
                return isDailyReportTime;
            }

            set
            {
                isDailyReportTime = value;
                NotifyPropertyChanged("IsDailyReportTime");
            }
        }

        public Visibility IsDailyReportTimeHamburger
        {
            get
            {
                return isDailyReportTimeHamburger;
            }

            set
            {
                isDailyReportTimeHamburger = value;
                NotifyPropertyChanged("IsDailyReportTimeHamburger");
            }
        }

        public string DailyEndTime
        {
            get
            {
                return dailyEndTime;
            }

            set
            {
                dailyEndTime = value;
                NotifyPropertyChanged("DailyEndTime");
            }
        }

        public string DailyDate
        {
            get
            {
                return dailyDate;
            }

            set
            {
                dailyDate = value;
                NotifyPropertyChanged("DailyDate");
            }
        }

        public int TotCashOnHand
        {
            get
            {
                return totCashOnHand;
            }

            set
            {
                totCashOnHand = value;
                NotifyPropertyChanged("TotCashOnHand");
            }
        }

        public int TotDateCashOnHand
        {
            get
            {
                return totDateCashOnHand;
            }

            set
            {
                totDateCashOnHand = value;
                NotifyPropertyChanged("TotDateCashOnHand");
            }
        }

        public Visibility IsShiftRecordNotFound
        {
            get
            {
                return isShiftRecordNotFound;
            }

            set
            {
                isShiftRecordNotFound = value;
                NotifyPropertyChanged("IsShiftRecordNotFound");
            }
        }

        public Visibility IsShiftRecordFound
        {
            get
            {
                return isShiftRecordFound;
            }

            set
            {
                isShiftRecordFound = value;
                NotifyPropertyChanged("IsShiftRecordFound");
            }
        }

        public Visibility IsDailyRecordNotFound
        {
            get
            {
                return isDailyRecordNotFound;
            }

            set
            {
                isDailyRecordNotFound = value;
                NotifyPropertyChanged("IsDailyRecordNotFound");
            }
        }

        public Visibility IsDailyRecordFound
        {
            get
            {
                return isDailyRecordFound;
            }

            set
            {
                isDailyRecordFound = value;
                NotifyPropertyChanged("IsDailyRecordFound");
            }
        }

        public string HamburgerTime
        {
            get
            {
                return hamburgerTime;
            }

            set
            {
                hamburgerTime = value;
                NotifyPropertyChanged("HamburgerTime");
            }
        }

        public string ShiftReportDate
        {
            get
            {
                return shiftReportDate;
            }

            set
            {
                shiftReportDate = value;
                NotifyPropertyChanged("ShiftReportDate");
            }
        }

        public Main_Shift_Collection ShiftReportSelectedDate
        {
            get
            {
                return shiftReportSelectedDate;
            }

            set
            {
                shiftReportSelectedDate = value;
                NotifyPropertyChanged("ShiftReportSelectedDate");
            }
        }

        public int IslastShift
        {
            get
            {
                return islastShift;
            }

            set
            {
                islastShift = value;
                NotifyPropertyChanged("IslastShift");
            }
        }

        public int DisplayReportAtHamburger
        {
            get
            {
                return displayReportAtHamburger;
            }

            set
            {
                displayReportAtHamburger = value;
                NotifyPropertyChanged("DisplayReportAtHamburger");
            }
        }




        public bool IsLastShiftChecked
        {
            get
            {
                return isLastShiftChecked;
            }

            set
            {
                isLastShiftChecked = value;
                NotifyPropertyChanged("IsLastShiftChecked");
            }
        }

        public bool IsAddUserPopup
        {
            get
            {
                return isAddUserPopup;
            }

            set
            {
                isAddUserPopup = value;
                NotifyPropertyChanged("IsAddUserPopup");
            }
        }

        public bool IsUserPopup
        {
            get
            {
                return isUserPopup;
            }

            set
            {
                isUserPopup = value;
                NotifyPropertyChanged("IsUserPopup");
            }
        }

        public bool IsDeleteUserPopup
        {
            get
            {
                return isDeleteUserPopup;
            }

            set
            {
                isDeleteUserPopup = value;
                NotifyPropertyChanged("IsDeleteUserPopup");
            }
        }

        public bool IsEmailSettingsPopup
        {
            get
            {
                return isEmailSettingsPopup;
            }

            set
            {
                isEmailSettingsPopup = value;
                NotifyPropertyChanged("IsEmailSettingsPopup");
            }
        }

        public bool IsChangePasswordPopup
        {
            get
            {
                return isChangePasswordPopup;
            }

            set
            {
                isChangePasswordPopup = value;
                NotifyPropertyChanged("IsChangePasswordPopup");
            }
        }

        public bool IsStoreSettingsPopup
        {
            get
            {
                return isStoreSettingsPopup;
            }

            set
            {
                isStoreSettingsPopup = value;
                NotifyPropertyChanged("IsStoreSettingsPopup");
            }
        }

        public bool IsSupportSettingsPopup
        {
            get
            {
                return isSupportSettingsPopup;
            }

            set
            {
                isSupportSettingsPopup = value;
                NotifyPropertyChanged("IsSupportSettingsPopup");
            }
        }
        public bool IsNewStoreSetupScanExistingBoxesInOrder
        {
            get
            {
                return isNewStoreSetupScanExistingBoxesInOrder;
            }

            set
            {
                isNewStoreSetupScanExistingBoxesInOrder = value;
                NotifyPropertyChanged("IsNewStoreSetupScanExistingBoxesInOrder");

            }
        }


        public RelayCommand AddUserCommand
        {
            get
            {
                return addUserCommand;
            }

            set
            {
                addUserCommand = value;
                NotifyPropertyChanged("AddUserCommand");
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

        public string PhoneNo
        {
            get
            {
                return phoneNo;
            }

            set
            {
                phoneNo = value;
                NotifyPropertyChanged("PhoneNo");
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



        public RelayCommand AddEmailCommand
        {
            get
            {
                return addEmailCommand;
            }

            set
            {
                addEmailCommand = value;
                NotifyPropertyChanged("AddEmailCommand");
            }
        }

        public RelayCommand ChangePwdCommand
        {
            get
            {
                return changePwdCommand;
            }

            set
            {
                changePwdCommand = value;
                NotifyPropertyChanged("ChangePwdCommand");
            }
        }

        public string OldPassword
        {
            get
            {
                return oldPassword;
            }

            set
            {
                oldPassword = value;
                NotifyPropertyChanged("OldPassword");
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

        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }

            set
            {
                confirmPassword = value;
                NotifyPropertyChanged("ConfirmPassword");
            }
        }

        public string AddEmailId
        {
            get
            {
                return addEmailId;
            }

            set
            {
                addEmailId = value;
                NotifyPropertyChanged("AddEmailId");
            }
        }

        public RelayCommand UsersCommand
        {
            get
            {
                return usersCommand;
            }

            set
            {
                usersCommand = value;
                NotifyPropertyChanged("UsersCommand");
            }
        }

        public ObservableCollection<Login> UserColl
        {
            get
            {
                return userColl;
            }

            set
            {
                userColl = value;
                NotifyPropertyChanged("UserColl");
            }
        }

        public string UserRole
        {
            get
            {
                return userRole;
            }

            set
            {
                userRole = value;
                NotifyPropertyChanged("UserRole");
            }
        }

        public string UserPhoneNo
        {
            get
            {
                return userPhoneNo;
            }

            set
            {
                userPhoneNo = value;
                NotifyPropertyChanged("UserPhoneNo");
            }
        }

        public string UserEmail
        {
            get
            {
                return userEmail;
            }

            set
            {
                userEmail = value;
                NotifyPropertyChanged("UserEmail");
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
                NotifyPropertyChanged("UserId");
            }
        }

        public Visibility IsVisibleBtNext
        {
            get
            {
                return isVisibleBtNext;
            }

            set
            {
                isVisibleBtNext = value;
                NotifyPropertyChanged("IsVisibleBtNext");
            }
        }

        public Visibility IsVisibleBtPrevious
        {
            get
            {
                return isVisibleBtPrevious;
            }

            set
            {
                isVisibleBtPrevious = value;
                NotifyPropertyChanged("IsVisibleBtPrevious");
            }
        }

        public bool IsManagerChecked
        {
            get
            {
                return isManagerChecked;
            }

            set
            {
                isManagerChecked = value;
                NotifyPropertyChanged("IsManagerChecked");
            }
        }

        public bool IsEmployeeChecked
        {
            get
            {
                return isEmployeeChecked;
            }

            set
            {
                isEmployeeChecked = value;
                NotifyPropertyChanged("IsEmployeeChecked");
            }
        }

        public bool IsAssignThisStore
        {
            get
            {
                return isassignThisStore;
            }

            set
            {
                isassignThisStore = value;
                NotifyPropertyChanged("IsAssignThisStore");
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

        public Visibility IsManagerShow
        {
            get
            {
                return isManagerShow;
            }

            set
            {
                isManagerShow = value;
                NotifyPropertyChanged("IsManagerShow");
            }
        }

        public Visibility IsEmployeeShow
        {
            get
            {
                return isEmployeeShow;
            }

            set
            {
                isEmployeeShow = value;
                NotifyPropertyChanged("IsEmployeeShow");
            }
        }

        public RelayCommand EditUserCommand
        {
            get
            {
                return editUserCommand;
            }

            set
            {
                editUserCommand = value;
                NotifyPropertyChanged("EditUserCommand");
            }
        }

        public bool IsEditUserPopup
        {
            get
            {
                return isEditUserPopup;
            }

            set
            {
                isEditUserPopup = value;
                NotifyPropertyChanged("IsEditUserPopup");
            }
        }

        public bool IsAddEmailPopup
        {
            get
            {
                return isAddEmailPopup;
            }

            set
            {
                isAddEmailPopup = value;
                NotifyPropertyChanged("IsAddEmailPopup");
            }
        }

        public RelayCommand EmailCommand
        {
            get
            {
                return emailCommand;
            }

            set
            {
                emailCommand = value;
                NotifyPropertyChanged("EmailCommand");
            }
        }

        public bool IsEditEmailPopup
        {
            get
            {
                return isEditEmailPopup;
            }

            set
            {
                isEditEmailPopup = value;
                NotifyPropertyChanged("IsEditEmailPopup");
            }
        }

        public RelayCommand EditEmailCommand
        {
            get
            {
                return editEmailCommand;
            }

            set
            {
                editEmailCommand = value;
                NotifyPropertyChanged("EditEmailCommand");
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

        public Visibility OnlyOne
        {
            get
            {
                return onlyOne;
            }

            set
            {
                onlyOne = value;
                NotifyPropertyChanged("OnlyOne");
            }
        }

        public Visibility Both
        {
            get
            {
                return both;
            }

            set
            {
                both = value;
                NotifyPropertyChanged("Both");
            }
        }

        public Visibility IsBothManagerShow
        {
            get
            {
                return isBothManagerShow;
            }

            set
            {
                isBothManagerShow = value;
                NotifyPropertyChanged("IsBothManagerShow");
            }
        }

        public Visibility IsBothEmployeeShow
        {
            get
            {
                return isBothEmployeeShow;
            }

            set
            {
                isBothEmployeeShow = value;
                NotifyPropertyChanged("IsBothEmployeeShow");
            }
        }

        public string MakeNull
        {
            get
            {
                return makeNull;
            }

            set
            {
                makeNull = value;
                NotifyPropertyChanged("MakeNull");
            }
        }

        public RelayCommand GeneralSettingCommand
        {
            get
            {
                return generalSettingCommand;
            }

            set
            {
                generalSettingCommand = value;
                NotifyPropertyChanged("GeneralSettingCommand");
            }
        }

        public bool IsChangeBoxPopup
        {
            get
            {
                return isChangeBoxPopup;
            }

            set
            {
                isChangeBoxPopup = value;
                NotifyPropertyChanged("IsChangeBoxPopup");
            }
        }

        public RelayCommand ChangeBoxCommand
        {
            get
            {
                return changeBoxCommand;
            }

            set
            {
                changeBoxCommand = value;
                NotifyPropertyChanged("ChangeBoxCommand");
            }
        }

        public string ChangeToBox
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

        public string ChangeFromBox
        {
            get
            {
                return changeFromBox;
            }

            set
            {
                changeFromBox = value;
                NotifyPropertyChanged("ChangeFromBox");
            }
        }

        public RelayCommand SaveChangeBoxCommand
        {
            get
            {
                return saveChangeBoxCommand;
            }

            set
            {
                saveChangeBoxCommand = value;
                NotifyPropertyChanged("SaveChangeBoxCommand");
            }
        }

        public Activation_Box ActivationBoxObj
        {
            get
            {
                return activationBoxObj;
            }

            set
            {
                activationBoxObj = value;
                NotifyPropertyChanged("ActivationBoxObj");
            }
        }

        public RelayCommand RemoveShiftCommand
        {
            get
            {
                return removeShiftCommand;
            }

            set
            {
                removeShiftCommand = value;
                NotifyPropertyChanged("RemoveShiftCommand");
            }
        }

        public bool IsPasswordConfirmShow
        {
            get
            {
                return isPasswordConfirmShow;
            }

            set
            {
                isPasswordConfirmShow = value;
                NotifyPropertyChanged("IsPasswordConfirmShow");
            }
        }

        public RelayCommand CheckPwdCommand
        {
            get
            {
                return checkPwdCommand;
            }

            set
            {
                checkPwdCommand = value;
                NotifyPropertyChanged("CheckPwdCommand");
            }
        }

        public string PasswordCheck
        {
            get
            {
                return passwordCheck;
            }

            set
            {
                passwordCheck = value;
                NotifyPropertyChanged("PasswordCheck");
            }
        }

        public RelayCommand ResetTrackerCommand
        {
            get
            {
                return resetTrackerCommand;
            }

            set
            {
                resetTrackerCommand = value;
                NotifyPropertyChanged("ResetTrackerCommand");
            }
        }

        public bool IsResetTrackerPopup
        {
            get
            {
                return isResetTrackerPopup;
            }

            set
            {
                isResetTrackerPopup = value;
                NotifyPropertyChanged("IsResetTrackerPopup");
            }
        }

        public bool IsResetPasswordConfirmShow
        {
            get
            {
                return isResetPasswordConfirmShow;
            }

            set
            {
                isResetPasswordConfirmShow = value;
                NotifyPropertyChanged("IsResetPasswordConfirmShow");
            }
        }

        public RelayCommand ResetCheckPwdCommand
        {
            get
            {
                return resetCheckPwdCommand;
            }

            set
            {
                resetCheckPwdCommand = value;
                NotifyPropertyChanged("ResetCheckPwdCommand");
            }
        }

        //public int SettleTemp
        //{
        //    get
        //    {
        //        return settleTemp;
        //    }

        //    set
        //    {
        //        settleTemp = value;
        //        NotifyPropertyChanged("SettleTemp");
        //    }
        //}

        public bool IsResetOne
        {
            get
            {
                return isResetOne;
            }

            set
            {
                isResetOne = value;
                NotifyPropertyChanged("IsResetOne");
            }
        }

        public bool IsResetTwo
        {
            get
            {
                return isResetTwo;
            }

            set
            {
                isResetTwo = value;
                NotifyPropertyChanged("IsResetTwo");
            }
        }

        public RelayCommand ResetCommand
        {
            get
            {
                return resetCommand;
            }

            set
            {
                resetCommand = value;
                NotifyPropertyChanged("ResetCommand");
            }
        }

        public DateTime? SingleShiftReportDate
        {
            get
            {
                return singleShiftReportDate;
            }

            set
            {
                singleShiftReportDate = value;
            }
        }

        public RelayCommand StoreSettingCommand
        {
            get
            {
                return storeSettingCommand;
            }

            set
            {
                storeSettingCommand = value;
                NotifyPropertyChanged("StoreSettingCommand");
            }
        }

        public Store_Info Store_Details
        {
            get
            {
                return store_Details;
            }

            set
            {
                store_Details = value;
                NotifyPropertyChanged("Store_Details");
            }
        }

        public bool IsAutoSettle
        {
            get
            {
                return isAutoSettle;
            }

            set
            {
                isAutoSettle = value;
                NotifyPropertyChanged("IsAutoSettle");
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

        public bool IsNewStoreSettingsPopup
        {
            get
            {
                return isNewStoreSettingsPopup;
            }

            set
            {
                isNewStoreSettingsPopup = value;
                NotifyPropertyChanged("IsNewStoreSettingsPopup");

            }
        }

        public bool IsRememberMe
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

        public RelayCommand OnEmployeeHistory
        {
            get
            {
                return onEmployeeHistory;
            }

            set
            {
                onEmployeeHistory = value;
            }
        }

        public LotteryInfo SelectedIndexBoxno
        {
            get
            {
                return selectedIndexBoxno;
            }

            set
            {
                selectedIndexBoxno = value;
                NotifyPropertyChanged("SelectedIndexBoxno");
            }
        }

        public RelayCommand RemoveboxCommand
        {
            get
            {
                return removeboxCommand;
            }

            set
            {
                removeboxCommand = value;
                NotifyPropertyChanged("RemoveboxCommand");
            }
        }

        public RelayCommand FinishSetupCommand
        {
            get
            {
                return finishSetupCommand;
            }

            set
            {
                finishSetupCommand = value;
                NotifyPropertyChanged("FinishSetupCommand");
            }
        }

        public bool IsNewStoreChecked
        {
            get
            {
                return isNewStoreChecked;
            }

            set
            {
                isNewStoreChecked = value;
                NotifyPropertyChanged("IsNewStoreChecked");
            }
        }

        public Visibility IsNewstoreCheckbox
        {
            get
            {
                return isNewstoreCheckbox;
            }

            set
            {
                isNewstoreCheckbox = value;
                NotifyPropertyChanged("IsNewstoreCheckbox");
            }
        }

        public bool IsResetThree
        {
            get
            {
                return isResetThree;
            }

            set
            {
                isResetThree = value;
                NotifyPropertyChanged("IsResetThree");
            }
        }

        public bool IsEmail_On_Off
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



        #endregion
    }
}
