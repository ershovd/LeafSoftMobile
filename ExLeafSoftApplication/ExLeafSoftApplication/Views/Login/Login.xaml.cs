

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExLeafSoftApplication.ViewModels;

namespace ExLeafSoftApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private static Login handle;

        public Login()
        {
            InitializeComponent();
            handle = this;


            //MessagingCenter.Subscribe<object, string>(this, "UpdateLabel", (s, e) =>
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        BackgroundServiceLabel.Text = e;
            //    });
            //});
        }


         LoginViewModel GetViewModel
        {           
            
            get{
            LoginViewModel model = this.BindingContext as LoginViewModel;
                return model;
                }
        }


      


        

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            INavigation a = Navigation;
            //BindingContext = null;
            //Content = null;
            //base.OnDisappearing();
            //handle = null;

            ////Application.Current.MainPage.Navigation.RemovePage(this);

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
           NavigationPage.SetBackButtonTitle(this, "");

            //await App.TodoManager.GetTaskLoginResult(GetViewModel.UserName,GetViewModel.Password);

          //  //GetViewModel.Login();
          //  List<FarmerModel> listofFarmer = await App.FarmerTable.GetItemsAsync();
          //  List<OrderModel> listofORder = await App.OrderTable.GetItemsAsync();


          //  await App.FarmerTable.SaveItemAsync(new FarmerModel { FarmerId = 1, Name = "TestField",Done= false });
          // await App.OrderTable.SaveItemAsync(new OrderModel { OrderId = 1, Name = "Testorder", Done = false});
          // listofFarmer =  await App.FarmerTable.GetItemsAsync();
          //listofORder = await App.OrderTable.GetItemsAsync();

          //  for (int i = 1; i <= listofFarmer.Count; i++)
          //  {
          //      FarmerModel item = await App.FarmerTable.GetItemAsync(i);
          //      item.Name = "Updated";
          //      item.Done = true;
          //      await App.FarmerTable.UpdateItemAsync(item);

          //      OrderModel Oitem = await App.OrderTable.GetItemAsync(i);
          //      Oitem.Name = "OrderUpdated";
          //      Oitem.Done = true;
          //      await App.OrderTable.UpdateItemAsync(Oitem);


          //  }

          // FarmerModel fmodel = await App.FarmerTable.GetItemAsync(12);
          //  int farmerid = await App.FarmerTable.DeleteItemAsync(fmodel);

          //   fmodel = await App.FarmerTable.GetItemAsync(12);
          //   farmerid = await App.FarmerTable.DeleteItemAsync(fmodel);


            

            await Navigation.PushModalAsync(new NavigationPage(new AllFarmerPage()));
            //Navigation.PushModalAsync(new View.TabControlPage());


        }
    }
}