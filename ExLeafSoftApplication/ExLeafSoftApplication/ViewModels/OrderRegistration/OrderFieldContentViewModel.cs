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
    public class OrderFieldContentViewModel : BaseViewModel
    {
        private ObservableCollection<FieldModel> _FieldList { get; set; }
        public ObservableCollection<FieldModel> FieldList { get { return _FieldList; } set { _FieldList = value; OnPropertyChanged("FieldList"); } }

        private FarmerModel _selectedFarmer;
        public FarmerModel SelectedFarmer { get { return _selectedFarmer; }
            set { _selectedFarmer = value;
                Task.Run(async () => await LoadFieldListCommand());
                OnPropertyChanged("SelectedFarmer"); } }
       

        public ICommand RefreshCommand { get; set; }

    
        private bool _isRefreshing { get; set; }
        public bool IsRefreshing {
            get { return _isRefreshing; }
            set { _isRefreshing = value;OnPropertyChanged("IsRefreshing"); }
        }

        public OrderFieldContentViewModel()
        {
            FieldList = new ObservableCollection<FieldModel>();

            RefreshCommand = new Command(async () => await LoadFieldListCommand());

           
        
           

        }


        async Task LoadFieldListCommand()
        {
            if (IsRefreshing)
                return;

            IsRefreshing = true;

            try
            {
                FieldList.Clear();
                var items =  await App.FieldTable.GetItemsAsync(SelectedFarmer);
                FieldList = new ObservableCollection<FieldModel>(items);
                //foreach (var item in items)
                //{
                //    //item.IsDeleted = item.FarmerId > 0  && item.FarmerId < 11 ?  false : true;
                //    FieldList.Add(item);
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
