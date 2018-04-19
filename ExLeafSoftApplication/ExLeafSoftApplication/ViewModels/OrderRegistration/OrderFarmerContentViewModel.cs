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
    public class OrderFarmerContentViewModel : BaseViewModel
    {
        private ObservableCollection<FarmerModel> _FarmerList { get; set; }
        public ObservableCollection<FarmerModel> FarmerList { get { return _FarmerList; } set { _FarmerList = value; OnPropertyChanged("FarmerList"); } }

        public FarmerModel SelectedFarmer { get; set; }
        public AddressFarmerModel SelectedAddress { get; set; }

        public ICommand RefreshCommand { get; set; }

    
        private bool _isRefreshing { get; set; }
        public bool IsRefreshing {
            get { return _isRefreshing; }
            set { _isRefreshing = value;OnPropertyChanged("IsRefreshing"); }
        }

        public OrderFarmerContentViewModel()
        {
            FarmerList = new ObservableCollection<FarmerModel>();

            RefreshCommand = new Command(async () => await LoadFarmerListCommand());
            Task.Run(async()=> await LoadFarmerListCommand());
        
           

        }


        async Task LoadFarmerListCommand()
        {
            if (IsRefreshing)
                return;

            IsRefreshing = true;

            try
            {
                FarmerList.Clear();
                var items =  await App.FarmerTable.GetFarmersHasFieldsAsync();
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
