using ExLeafSoftApplication.Models;
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
    public class OrderCropViewModel : BaseViewModel
    {
        private ObservableCollection<CropModel> _cropList { get; set; }
        public ObservableCollection<CropModel> CropList { get { return _cropList; } set { _cropList = value; OnPropertyChanged("CropList"); } }

        public OrderRegistrationViewModel Parent { get;set; }

        private CropModel _selectedCrop;
        public CropModel SelectedCrop { get { return _selectedCrop; }
            set {
                _selectedCrop = value;
                OnPropertyChanged("SelectedCrop"); } }
       


        public OrderCropViewModel()
        {
            GetCrops();
        }


        async void GetCrops()
        {
            List<CropModel> cropList = await App.CropTable.GetCrops();
            CropList = new ObservableCollection<CropModel>(cropList);
        }



      

    }
}
