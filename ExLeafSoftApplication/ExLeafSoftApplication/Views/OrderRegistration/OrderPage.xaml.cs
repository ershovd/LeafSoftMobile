using ExLeafSoftApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExLeafSoftApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderPage : ContentPage
    {
        //http://www.fabiocozzolino.eu/a-little-and-simple-bindable-horizontal-scroll-view/

        public OrderPage (FarmerModel farmer = null,FieldModel field = null)
		{
			InitializeComponent ();
            GetViewModel.Navigation = Navigation;
            GetViewModel.ParentPage = this;
        
        }

        public OrderRegistrationViewModel GetViewModel
        {
            get
            {

                OrderRegistrationViewModel model = this.BindingContext as OrderRegistrationViewModel;
                return model;
            }

            
        }

        //async void OnChartTapGestureTap(object sender, EventArgs args)
        //{
           
        //    await Navigation.PushModalAsync(new CameraPage(GetViewModel));
        //}
    }
}