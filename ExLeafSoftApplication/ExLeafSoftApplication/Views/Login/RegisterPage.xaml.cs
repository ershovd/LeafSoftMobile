using System;

using Xamarin.Forms;

namespace ExLeafSoftApplication.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Register_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ShowImages());
        }
            
    }
}
