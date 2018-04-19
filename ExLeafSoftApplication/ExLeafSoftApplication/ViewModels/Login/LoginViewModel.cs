
using System.Windows.Input;
using Xamarin.Forms;

namespace ExLeafSoftApplication.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
        }

        private string _userName;
        public string UserName {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged("UserName"); }
        }



        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }


        public ICommand _openPageOneCommand { get; set; }
        public ICommand LoginCommand
        {
            get
            {
                if (_openPageOneCommand == null)
                {

                    // Call action
                    _openPageOneCommand = new Command<object>(k =>
                        OpenPageOne(k)

                    );
                }
                return _openPageOneCommand;
            }
        }


       public async void Login()
        {
            await App.TodoManager.GetTaskLoginResult("hasa","hasana");
        }

        private void OpenPageOne(object id)
        {
           

        }
    }
}
