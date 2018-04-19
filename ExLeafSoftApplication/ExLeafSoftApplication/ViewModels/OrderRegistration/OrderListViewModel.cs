using ExLeafSoftApplication.Models;
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
    public class OrderListViewModel :BaseViewModel
    {
        private ObservableCollection<CompactOrderModel> _orderList { get; set; }
        public ObservableCollection<CompactOrderModel> OrderList { get { return _orderList; } set { _orderList = value;OnPropertyChanged("OrderList"); } }

        public object MapPage { get; set; }
        public INavigation Navigation { get; set; }


        public ICommand RefreshCommand { get; set;  }

        private string _orderSearch { get; set; }
        public string OrderSearch { get { return _orderSearch; }
            set {
                _orderSearch = value;
                OnPropertyChanged("OrderSearch"); }
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand OpenMap { get; set; }

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

        public OrderListViewModel()
        {
            OrderList = new ObservableCollection<CompactOrderModel>();
            RefreshCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SubmitCommand = new Command(async () =>  await SearchItemsCommand());
          

        }

            async Task SearchItemsCommand()
        {
            OrderList.Clear();
          List<CompactOrderModel> tmplist = await App.OrderTable.GetOrdersAsync(OrderSearch.ToLower());
          OrderList = new ObservableCollection<CompactOrderModel>(tmplist);
        }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsRefreshing)
                return;

            IsRefreshing = true;

            try
            {
                OrderList.Clear();
                var items = await App.OrderTable.GetAllOrdersAsync();
                OrderList = new ObservableCollection<CompactOrderModel>(items);
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
