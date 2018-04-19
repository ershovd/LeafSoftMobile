using ExLeafSoftApplication.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ExLeafSoftApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FarmerAbstractPage : ContentPage
	{

        public FarmerAbstractPage(FarmerModel farmer,AddressFarmerModel address)
		{
			InitializeComponent ();

            GetViewModel.Farmer = farmer;
            GetViewModel.FarmerAddress = address;
            GetViewModel.Navigation = Navigation;
        }

        async void OnFarmerClicked(object sender, EventArgs e)
        {


            AddressFarmerModel address = GetViewModel.FarmerAddress;
            await Navigation.PushAsync(new FarmerPage(GetViewModel.Farmer,address));
            //GetViewModel.FarmerList.Clear();
        }




        public FarmerAbstractViewModel GetViewModel
        {
            get
            {
                FarmerAbstractViewModel model = this.BindingContext as FarmerAbstractViewModel;
                return model;
            }
        }

    

      

    }
}