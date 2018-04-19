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
    public class TemplateFieldListViewModel : BaseViewModel
    {
        private ObservableCollection<FieldModel> _fieldList { get; set; }
        public ObservableCollection<FieldModel> FieldList { get { return _fieldList; } set { _fieldList = value; OnPropertyChanged("FieldList"); } }

        public FarmerModel SelectedFarmer { get; set; }
        public AddressFarmerModel SelectedAddress { get; set; }

        public ICommand RefreshCommand { get; set; }

    
        private bool _isRefreshing { get; set; }
        public bool IsRefreshing {
            get { return _isRefreshing; }
            set { _isRefreshing = value;OnPropertyChanged("IsRefreshing"); }
        }

        public TemplateFieldListViewModel()
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
                var items = await App.FieldTable.GetItemsAsync(SelectedFarmer);
                FieldList = new ObservableCollection<FieldModel>(items);
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
