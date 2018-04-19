
using ExLeafSoftApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExLeafSoftApplication.Views
{
    //https://grialkit.com/videos/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldPage : ContentPage
    {
        public FieldPage(FarmerModel farmer,AddressFarmerModel address)
        {
            InitializeComponent();

            GetViewModel.Farmer = farmer;
            GetViewModel.FarmerGuid = farmer.GuidId;
            GetViewModel.address = address;

            GetViewModel.Navigation = Navigation;
            GetViewModel.ButtonName = "Done";
        }


        public FieldPage(FieldModel item, FarmerModel farmer, AddressFarmerModel address)
        {
            InitializeComponent();

            GetViewModel.Farmer = farmer;
            GetViewModel.FarmerGuid = farmer.GuidId;
            GetViewModel.address = address;
            GetViewModel.Navigation = Navigation;
            GetViewModel.ButtonName = "Update";
            GetViewModel.FieldID = item.FieldId;
            GetViewModel.FieldGuid = item.FieldGuid;
            GetViewModel.FieldName = item.FieldName;
            GetViewModel.GpsPosition = item.FieldGps;
            GetViewModel.SampleAreaSize = item.FieldAreaSize.ToString();
            Array abc = Enum.GetValues(typeof(FieldViewModel.SampleAreaSizeUnit));
            foreach (FieldViewModel.SampleAreaSizeUnit i in abc)
            {
                if (i.ToString() == item.FieldUnit)
                {
                    GetViewModel.AreaUnit = i;
                }
            }
            

        }


        FieldViewModel GetViewModel {
            get
            {
                FieldViewModel model = this.BindingContext as FieldViewModel;
                return model;
            }
        }

        protected override void OnDisappearing()
        {

            // If you want to continue going back
            FarmerModel selectedFarmer = GetViewModel.Farmer;
            AddressFarmerModel address = GetViewModel.address;
           
            FarmerAbstractPage a = (FarmerAbstractPage)Navigation.NavigationStack.Where(k => k.Title == "FarmerDetail").FirstOrDefault();
            a.GetViewModel.Farmer = selectedFarmer;
            a.GetViewModel.FarmerAddress = address;
            

            //// If you want to stop the back button
            //return true;

        }


       
           
            


     
        
    }
}