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
    public partial class OrderList : ContentPage
    {
        public OrderList()
        {
            InitializeComponent();

            GetViewModel.Navigation = Navigation;
        }


        OrderListViewModel GetViewModel
        {

            get
            {
                OrderListViewModel model = this.BindingContext as OrderListViewModel;
                return model;
            }
        }

      

        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (GetViewModel.OrderList != null && GetViewModel.OrderList.Count == 0)
                GetViewModel.RefreshCommand.Execute(null);
        }


        protected override bool OnBackButtonPressed()
        {
            // If you want to stop the back button
            return true;
        }
    }
}