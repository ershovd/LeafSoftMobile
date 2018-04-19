using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExLeafSoftApplication.ViewModels
{
    public class CommandViewModel
    {
        public string Text { get; set; }
        public ICommand Command { get; set; }
    }

    public class MainViewModel : BaseViewModel

    {
        private IUserDialogs Dialogs { get { return UserDialogs.Instance; } }
        private List<CommandViewModel> _container;
        public List<CommandViewModel> Container
        {
            get { return _container; }
            set { _container = value; OnPropertyChanged("Container"); }

        }


        public MainViewModel()
        {


            Container = new List<CommandViewModel>
            {
                    new CommandViewModel
                {
                    Text = "Progress",
                    Command = new Command(async () =>
                    {
                        var cancelled = false;

                        using (var dlg = this.Dialogs.Progress("Test Progress", () => cancelled = true))
                        {
                            while (!cancelled && dlg.PercentComplete < 100)
                            {
                                await Task.Delay(TimeSpan.FromMilliseconds(500));
                                dlg.PercentComplete += 2;
                            }
                        }
                        this.Dialogs.Alert(cancelled ? "Progress Cancelled" : "Progress Complete");
                    })
                }
            };



            //Task a = new Task(() =>
            //{ Container[0].Command.Execute(null); });
            //a.Wait(3000);

            //a.Start();
            //a.Wait(2000);
            //a.Dispose();

        }
    }


}
