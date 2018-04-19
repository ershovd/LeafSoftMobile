using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Validator
{
    //https://blog.xamarin.com/behaviors-in-xamarin-forms/
    //https://www.davidbritch.com/2017/03/validating-user-input-in-xamarinforms-ii.html

    public class EmailValidatorBehavior : Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty FarmerGuidProperty = BindableProperty.Create("FarmerGuid",typeof(string),typeof(EmailValidatorBehavior),null);

        public static readonly BindableProperty FarmerIdProperty = BindableProperty.Create("FarmerId", typeof(int), typeof(EmailValidatorBehavior), 0);




        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
      




        public string FarmerGuid
        {
            get { return (string) GetValue(FarmerGuidProperty); }
            set {
                SetValue(FarmerGuidProperty, value);
            }
        }

        public int FarmerId
        {
            get { return (int)GetValue(FarmerIdProperty); }
            set { SetValue(FarmerIdProperty, value); }
        }

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }


       

        protected override void OnAttachedTo(BindableObject bindable)
        {
            Entry obj = bindable as Entry;

            obj.TextChanged += HandleTextChanged;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
           
            if (IsValid)
            {
                CheckEmail(e.NewTextValue);
                
                
            }

            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            Entry obj = bindable as Entry;
            obj.TextChanged -= HandleTextChanged;

        }

       

        private async void CheckEmail(string Email)
        {
            try
            {
                List<ValidateEmail> isemailExist = await App.FarmerTable.CheckEmail(Email.ToLower(),FarmerGuid,FarmerId);
                IsValid = true;

                if (isemailExist != null && isemailExist.Count > 0)
                {
                    foreach (ValidateEmail item in isemailExist)
                    {
                        int count = item.Count;
                        string optype = item.OpType;
                        int anotherRecordHash = item.AnotherRecordHas;
                        if (optype == "insert" && count > 0)
                            IsValid = false;
                        else if (optype == "update" && count > 1 && anotherRecordHash == 0)
                            IsValid = false;
                        else if (optype == "update" && count == 1 && anotherRecordHash == 1)
                            IsValid = false;
                    }
                    


                }
            }
            catch (Exception ex)
            {

            }

        }
    }

    public class ExtendedEmailValidatorBehavior : Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        static readonly BindablePropertyKey ErrorMessagePropertyKey = BindableProperty.CreateReadOnly("ErrorMessage", typeof(string), typeof(EmailValidatorBehavior), null);

        public static BindableProperty ErrorMessageProperty = ErrorMessagePropertyKey.BindableProperty;


        static readonly BindablePropertyKey SmrdayKey = BindableProperty.CreateReadOnly("Smrda", typeof(int), typeof(EmailValidatorBehavior), 1);

        public static readonly BindableProperty SmrdaProperty = SmrdayKey.BindableProperty;
        public string Words { get; set; }

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { Words = value.ToString(); base.SetValue(IsValidPropertyKey, value); }
        }
        public int Smrda
        {
            get { return (int)base.GetValue(SmrdaProperty); }
            private set { base.SetValue(SmrdayKey, value); }

        }

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }

            set { SetValue(ErrorMessageProperty, value); }
        }


        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {


            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;

            if (String.IsNullOrEmpty(e.OldTextValue))
                Smrda = 0;
            else
            {
                Smrda = 1;

                ErrorMessage = "Please enter email address";
                return;
            }
            if (!IsValid)
                ErrorMessage = "Please enter valid email address";
            else
                ErrorMessage = "";

        }


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }

}
