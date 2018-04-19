using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Validator
{
    public class FieldSizeValidatorBehavior : Behavior<Entry>
    {
        const string numberRegex = "^[0-9]*$";

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool),
            typeof(FieldSizeValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;


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
            bool islen = e.NewTextValue.Length == 5 ? true : false;
            bool isnumber = (Regex.IsMatch(e.NewTextValue, numberRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            IsValid = islen && isnumber;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            ((Entry)sender).Text = CheckLength(e.NewTextValue, 5);
        }

        private string CheckLength(string InputValue, int len)
        {
            if (InputValue.Length > len)
                InputValue = InputValue.Remove(InputValue.Length - 1);

            return InputValue;
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            Entry obj = bindable as Entry;
            obj.TextChanged -= HandleTextChanged;

        }
    }
}
