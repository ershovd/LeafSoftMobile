
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace ExLeafSoftApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderFarmerContent : ContentView
	{
    
        public OrderFarmerContent()
		{
			InitializeComponent ();
            
          
        }

       

         void FarmerSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FarmerModel;
            if (item == null)
                return;

            OrderPage a = (OrderPage)Navigation.NavigationStack.Where(k => k.Title == "Order").FirstOrDefault();
            FarmerModel farmer = item;
            FieldModel field = null;

            a.GetViewModel.SelectedFarmer = farmer is null ? new FarmerModel { FirstName = "Select Farmer" } : farmer;
            a.GetViewModel.BtnFarmerSelection = farmer != null ? "Change Farmer" : "Select Farmer";
            a.GetViewModel.SelectedField = field ?? field;
            a.GetViewModel.BtnFieldSelection = field != null ? "Change Field" : "Select Field";

            Navigation.NavigationStack.LastOrDefault().SendBackButtonPressed();


            //Detail Page is not ready to show
            //await Navigation.PushAsync(new OrderPage(item));

           
        }


    }
}