using Xamarin.Forms;
using ExLeafSoftApplication.Common;
using Acr.UserDialogs;
using System.Threading.Tasks;
using System;

namespace ExLeafSoftApplication.Views
{
	public partial class LongRunningPage : ContentPage
	{
        private IUserDialogs Dialogs { get { return UserDialogs.Instance; } }
        private static IProgressDialog pg;
        // How to run a method in the background https://stackoverflow.com/questions/45209784/how-can-i-run-a-method-in-the-background-on-my-xamarin-app/45210187

        public LongRunningPage ()
		{
			InitializeComponent ();

            //Wire up XAML buttons
            //pg = this.Dialogs.Progress("Progress (No Cancel)");
            HandleReceivedMessages();
          

        }


         void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<TickedMessage>(this, "TickedMessage", message => {
                Device.BeginInvokeOnMainThread(() => {
                    ticker.Text = "Synch";

                    //await Task.Delay(TimeSpan.FromMilliseconds(2));

                    //if (!pg.IsShowing)
                    //    pg.Show();


                    //await Task.Delay(TimeSpan.FromMilliseconds(5));
                    //pg.PercentComplete += 1;
                    
                    
                   
                    //if (message.Message == "1")
                    //{
                    //    pg.Show();
                    //}


                    //if (pg.PercentComplete == 100)
                    //    {

                    //    pg.Hide();
                    //    pg.PercentComplete = 0;
                    //    }
                   
                   
                    
                    
                  
                    
                     
                    
                        
                    
                });
            });

            MessagingCenter.Subscribe<CancelledMessage>(this, "CancelledMessage", message => {
                Device.BeginInvokeOnMainThread(() => {
                    ticker.Text = "Cancelled";

                    //pg.PercentComplete = 0;
                    //pg.Hide();
                    
                   
                });
            });
        }
    }
}