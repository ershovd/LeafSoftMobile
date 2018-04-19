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
    public partial class AllFarmerPage : ContentPage
    {
        public AllFarmerPage()
        {
            InitializeComponent();

            GetViewModel.Navigation = Navigation;
        }


        AllFarmerViewModel GetViewModel
        {

            get
            {
                AllFarmerViewModel model = this.BindingContext as AllFarmerViewModel;
                return model;
            }
        }

        async void FarmerSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FarmerModel;
            if (item == null)
                return;

            AddressFarmerModel address = await GetViewModel.GetAddressFarmerModel(item.FarmerId,item.GuidId);

            //Detail Page is not ready to show
            await Navigation.PushAsync(new FarmerAbstractPage(item,address));

            //GetViewModel.FarmerList.Clear();
            FarmerlistView.SelectedItem = null;
        }

        async void OnFarmerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FarmerPage(new FarmerModel { FarmerId = 0, FirstName = "", LastName = "",BirthDate ="" , Phone ="" , Email = "" }));
            //GetViewModel.FarmerList.Clear();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (GetViewModel.FarmerList != null && GetViewModel.FarmerList.Count == 0)
                GetViewModel.RefreshCommand.Execute(null);
        }


        protected override bool OnBackButtonPressed()
        {
            // If you want to stop the back button
            return true;
        }
    }
}