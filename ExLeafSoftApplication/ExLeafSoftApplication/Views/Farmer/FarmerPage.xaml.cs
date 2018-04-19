
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FarmerPage : ContentPage
    {
        public FarmerPage(FarmerModel farmer = null,AddressFarmerModel address = null)
        {
            InitializeComponent();
            GetViewModel.farmerModel = farmer;
            GetViewModel.addressModel = address;
            GetViewModel.BtnName = farmer.FarmerId > 0 ? "Update" : "Save";
            GetViewModel.OperationType = farmer.FarmerId > 0 ? true : false;
            GetViewModel.FarmerID = farmer.FarmerId;
            GetViewModel.FirstName = farmer.FirstName;
            GetViewModel.LastName = farmer.LastName;
            GetViewModel.GuidId = farmer.GuidId;
            GetViewModel.Email = farmer.Email;
            GetViewModel.Phone = farmer.Phone;
           
            GetViewModel.RowHash = farmer.RowHash;
           
            GetViewModel.GetCountryList();
            if(address != null)
            UpdateCountryCity(farmer.FarmerId,address);

            string format = "yyyy-MM-dd'T'hh:mm:ss";
            //string format = string.Empty;
            //if (Device.RuntimePlatform == Device.Android)
            //    format = "M/d/yyyy hh:mm:ss tt";
            //else
            //    format = "M/d/yyyy hh:mm:ss";

           

            GetViewModel.BirthDate = string.IsNullOrEmpty(farmer.BirthDate) ? DateTime.Now :
                DateTime.ParseExact(farmer.BirthDate,format, CultureInfo.InvariantCulture);
            GetViewModel.Navigation = Navigation;
            GetViewModel.IsCountry = false;


            

        }

         FarmerViewModel GetViewModel {
            get
            {
                FarmerViewModel model = this.BindingContext as FarmerViewModel;
                return model;
            }
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    FarmerModel farmer = GetViewModel.farmerModel;
        //    AddressFarmerModel address = GetViewModel.addressModel;
        //    Navigation.PushAsync(new FarmerAbstractPage(farmer, address));
        //    return false;
        //}

        protected override void OnDisappearing()
        {
            FarmerModel farmer = GetViewModel.farmerModel;
            AddressFarmerModel address = GetViewModel.addressModel;

            if (farmer != null && farmer.FarmerId > 0)
            {
                FarmerAbstractPage a = (FarmerAbstractPage) Navigation.NavigationStack.Where(k => k.Title == "FarmerDetail").FirstOrDefault();
                a.GetViewModel.Farmer = farmer;
                a.GetViewModel.FarmerAddress = address;
                
              

            }

        }

        async void UpdateCountryCity(int farmerid, AddressFarmerModel address)
        {
            if (farmerid > 0)
            {
                CountryPicker.SelectedIndexChanged -= Country_SelectedIndexChanged;
                GetViewModel.Address = string.IsNullOrEmpty(address.Address) ? string.Empty : address.Address;
                GetViewModel.SelectedCountry = GetViewModel.CountryList.Where(k => k.CountryID == address.CountryID).FirstOrDefault();
                GetViewModel.RawCityList =  await App.CountryCityTable.GetCityList(address.CountryID);
                //await Task.Delay(TimeSpan.FromMilliseconds(400));
                GetViewModel.SelectedCity = GetViewModel.CityList.Where(k => k.City_ID == address.CityID).FirstOrDefault();
                GetViewModel.IsCountry = true;
                CountryPicker.SelectedIndexChanged += Country_SelectedIndexChanged;
            }
        }
           
            


        private void Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                GetViewModel.IsCountry = true;
            }
        }
        private void City_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        
    }
}