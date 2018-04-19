using ExLeafSoftApplication.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExLeafSoftApplication.ViewModels
{ 
    public class AllFarmerViewModel :BaseViewModel
    {
        private ObservableCollection<FarmerModel> _farmerList { get; set; }
        public ObservableCollection<FarmerModel> FarmerList { get { return _farmerList; } set { _farmerList = value;OnPropertyChanged("FarmerList"); } }

        public object MapPage { get; set; }
        public INavigation Navigation { get; set; }


        public ICommand RefreshCommand { get; set;  }

        private string _farmerSearch { get; set; }
        public string FarmerSearch { get { return _farmerSearch; }
            set {
                _farmerSearch = value;
                OnPropertyChanged("FarmerSearch"); }
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand GoOrder { get; set; }
        public ICommand OrderList { get; set; }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public AllFarmerViewModel()
        {
            FarmerList = new ObservableCollection<FarmerModel>();
            RefreshCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SubmitCommand = new Command(async () =>  await SearchItemsCommand());
            GoOrder = new Command(async () => await GoOrderCommand());
            OrderList = new Command(async () => await GetOrderListCommand());

        }

       

        public async Task<AddressFarmerModel> GetAddressFarmerModel(int farmerID, string FarmerGuid)
        {
            if (farmerID == 0)
            {
                farmerID = -1;
            }

            Guid tmp = Guid.Empty;

            if (!string.IsNullOrEmpty(FarmerGuid))
            {
                tmp = new Guid(FarmerGuid);
            }
          
            AddressFarmerModel selectedFarmerAddress = await App.AddressFarmerTable.GetItemAsync(farmerID,tmp);
            return selectedFarmerAddress;
        }

        async Task GoOrderCommand()
        {
            await Navigation.PushAsync(new OrderPage());
        }

        async Task GetOrderListCommand()
        {
            await Navigation.PushAsync(new OrderList());
        }

            async Task SearchItemsCommand()
        {
            FarmerList.Clear();
          List<FarmerModel> tmplist = await App.FarmerTable.GetFarmersAsync(FarmerSearch.ToLower());
          FarmerList = new ObservableCollection<FarmerModel>(tmplist);
        }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsRefreshing)
                return;

            IsRefreshing = true;

            try
            {
                FarmerList.Clear();
                var items = await App.FarmerTable.GetAllFarmersAsync();
                FarmerList = new ObservableCollection<FarmerModel>(items);
                //foreach (var item in items)
                //{
                //    //item.IsDeleted = item.FarmerId > 0  && item.FarmerId < 11 ?  false : true;
                //    FarmerList.Add(item);
                //}
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
            }
            finally
            {
                await Task.Delay(500);
                IsRefreshing = false;
            }
        }

    }
}
