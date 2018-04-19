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
	public partial class TemplateFieldListView : ContentView
	{
		public TemplateFieldListView (FarmerModel farmer = null,AddressFarmerModel address=null)
		{
			InitializeComponent();

            GetViewModel.SelectedFarmer = farmer;
            GetViewModel.SelectedAddress = address;
		}

        TemplateFieldListViewModel GetViewModel
        {

            get
            {
                TemplateFieldListViewModel model = this.BindingContext as TemplateFieldListViewModel;
                return model;
            }
        }

        async void SelectedField(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FieldModel;
            if (item == null)
                return;

            await Navigation.PushAsync(new FieldPage(item,GetViewModel.SelectedFarmer,GetViewModel.SelectedAddress));

        }






    }
}