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
	public partial class CameraPage : ContentPage
	{
		public CameraPage(OrderRegistrationViewModel parent )
		{
			InitializeComponent ();
            GetViewModel.Parent = parent;
		}

        private void Crop_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
              
            }
        }


        protected override void OnDisappearing()
        {
            //(GetViewModel.Parent.ParentPage as OrderPage).GetViewModel.CropSelectionIsEnabled = true;
            if((GetViewModel.Parent.ParentPage as OrderPage).GetViewModel.SelectedField != null)
            (GetViewModel.Parent.ParentPage as OrderPage).GetViewModel.LoadImages();
        }

        public OrderCropViewModel GetViewModel
        {
            get
            {
                OrderCropViewModel model = this.BindingContext as OrderCropViewModel;
                return model;
            }
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    // If you want to stop the back button
        //    return true;
        //}

    }
}