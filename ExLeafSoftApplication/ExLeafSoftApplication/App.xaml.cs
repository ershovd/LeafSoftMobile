

using ExLeafSoftApplication.Common;
using ExLeafSoftApplication.Data;
using ExLeafSoftApplication.SqlLiteEntities;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExLeafSoftApplication
{
    public partial class App : Application
    {
        //Sqlite-net-pcl stable version is : https://www.nuget.org/packages/sqlite-net-pcl/

        public static ServiceManager TodoManager { get; private set; }

        static FieldTable _fieldTable;
        static CropTable _cropTable;
        static FarmerTable _farmertable;
        static OrderTable _orderTable;
        static OrderDetailTable _orderDetailTable;
        static AddressFarmerTable _addressFarmerTable;
        static CountryTable _countryTable;
        static CountryCityTable _cityCountryTable;

        public static GeneralTimer timer = null;

        public App()
        {
            InitializeComponent();

            TodoManager = new ServiceManager(new CustomerService());
           

            //MainPage = new NavigationPage(new Views.TestDemoPage());

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new Views.LoginPage();
            else
                MainPage = new Views.LoginPage();


            GetCountryList();

            //MainPage = new Views.LongRunningPage();
            timer = new GeneralTimer(TimeSpan.FromSeconds(7867868), StartService);
            timer.Start();

            HandleReceivedMessages();

          



        }


        void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<TickedMessage>(this, "TickedMessage", message => {
                Device.BeginInvokeOnMainThread(() => {
                    //ticker.Text = "Synch";

                    //await Task.Delay(TimeSpan.FromMilliseconds(2));

                    //if (!pg.IsShowing)
                    //    pg.Show();


                    //await Task.Delay(TimeSpan.FromMilliseconds(5));
                    //pg.PercentComplete += 1;



                    //if (message.Message == "1")
                    //{
                    //    pg.Show();
                    //}


                    //if (pg.PercentComplete == 100)
                    //    {

                    //    pg.Hide();
                    //    pg.PercentComplete = 0;
                    //    }










                });
            });
           
            MessagingCenter.Subscribe<CancelledMessage>(this, "CancelledMessage", message => {
                
                Device.BeginInvokeOnMainThread(() => {
                    timer.Start();
                });
            });
        }



        private void StartService()
        {
            var message = new StartLongRunningTaskMessage();
            MessagingCenter.Send(message, "StartLongRunningTaskMessage");
            timer.Stop();
           

           
        }


      
        async void GetCountryList()
        {
            CountryTable.CountryList = await CountryTable.GetCompactCountrylist();
        }


        public static OrderTable OrderTable
        {
            get
            {
                if (_orderTable == null)
                {
                    _orderTable = new OrderTable(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _orderTable;
            }
        }

        public static OrderDetailTable OrderDetailTable
        {
            get
            {
                if (_orderDetailTable == null)
                {
                    _orderDetailTable = new OrderDetailTable(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _orderDetailTable;
            }
        }

        public static FarmerTable FarmerTable
        {
            get
            {
                if (_farmertable == null)
                {
                    _farmertable = new FarmerTable(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _farmertable;
            }
        }

        public static FieldTable FieldTable
        {
            get
            {
                if (_fieldTable == null)
                {
                    _fieldTable = new FieldTable(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _fieldTable;
            }
        }

        public static CropTable CropTable
        {
            get
            {
                if (_cropTable == null)
                {
                    _cropTable = new CropTable(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _cropTable;
            }
        }


        public static AddressFarmerTable AddressFarmerTable
        {
            get
            {
                if (_addressFarmerTable == null)
                {
                    _addressFarmerTable = new AddressFarmerTable(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _addressFarmerTable;
            }
        }


        public static CountryTable CountryTable
        {
            get
            {
                if (_countryTable == null)
                {
                    _countryTable = new CountryTable(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _countryTable;
            }
        }

        public static CountryCityTable CountryCityTable
        {
            get
            {
                if (_cityCountryTable == null)
                {
                    _cityCountryTable = new CountryCityTable(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _cityCountryTable;
            }
        }
    }
}