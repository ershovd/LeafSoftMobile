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
	public partial class OrderFieldContent : ContentView
	{
       
        public OrderFieldContent(FarmerModel farmer = null)
		{
			InitializeComponent ();
            GetViewModel.SelectedFarmer = farmer;

        }

      

            OrderFieldContentViewModel GetViewModel
        {

            get
            {
                OrderFieldContentViewModel model = this.BindingContext as OrderFieldContentViewModel;
                return model;
            }
        }

         void FieldSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FieldModel;
            if (item == null)
                return;


            OrderPage a = (OrderPage)Navigation.NavigationStack.Where(k => k.Title == "Order").FirstOrDefault();
            FarmerModel farmer = GetViewModel.SelectedFarmer;
            FieldModel field = item;
            a.GetViewModel.SelectedFarmer = farmer is null ? new FarmerModel { FirstName = "Select Farmer" } : farmer;
            a.GetViewModel.BtnFarmerSelection = farmer != null ? "Change Farmer" : "Select Farmer";
            a.GetViewModel.SelectedField = field ?? field;
            a.GetViewModel.BtnFieldSelection = field != null ? "Change Field" : "Select Field";

            Navigation.NavigationStack.LastOrDefault().SendBackButtonPressed();
            
            //Detail Page is not ready to show
            //await Navigation.PushAsync(new OrderPage(GetViewModel.SelectedFarmer, item));


        }
    }
}