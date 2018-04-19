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
    public class FarmerAbstractViewModel :BaseViewModel
    {
        //private ObservableCollection<FieldModel> _fieldList { get; set; }
        //public ObservableCollection<FieldModel> FieldList { get { return _fieldList; } set { _fieldList = value;OnPropertyChanged("FieldList"); } }


        public ICommand RefreshCommand { get; set; }

        public ICommand GetFieldList { get; set; }
        public ICommand GetFieldMap { get; set; }
        public ICommand AddFieldCommand { get; set; }
        public INavigation Navigation { get; set; }

        private FarmerModel _farmer { get; set; }
        public FarmerModel Farmer { get { return _farmer; }
            set { _farmer = value;OnPropertyChanged("Farmer"); }
        }

        private AddressFarmerModel _farmerAddressmodel { get; set; }
        public AddressFarmerModel FarmerAddress {
            get { return _farmerAddressmodel; }
            set { _farmerAddressmodel = value;
                OnPropertyChanged("FarmerAddress"); } }

        private string _isActiveTo = string.Empty;
        public string IsActiveTo
        {
            get { return _isActiveTo; }
            set
            {
                _isActiveTo = value;
                OnPropertyChanged(nameof(IsActiveTo));
            }
        }

        private bool _isRefreshing { get; set; }
        public bool IsRefreshing {
            get { return _isRefreshing; }
            set { _isRefreshing = value;OnPropertyChanged("IsRefreshing"); }
        }

        public FarmerAbstractViewModel()
        {
            //FieldList = new ObservableCollection<FieldModel>();

            //RefreshCommand = new Command(async () => await LoadFieldListCommand());
            GetFieldList = new Command( () =>  GetFieldListCommand());
            GetFieldMap = new Command(() =>  GetFieldMapCommand());
            AddFieldCommand = new Command(async() => await AddFieldHandler());

        }



        void GetFieldMapCommand()
        {
           IsActiveTo = "MapView";
        }


        void GetFieldListCommand()
        {
            if (Farmer != null)
            {
                
                IsActiveTo = "FieldList";
            }
            //await LoadFieldListCommand();


        }

        async Task AddFieldHandler()
        {
            await Navigation.PushAsync(new FieldPage(Farmer,FarmerAddress));

        }




    }
}
