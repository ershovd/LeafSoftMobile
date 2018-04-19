using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Validator
{
    public class NameValidatorBehavior : Behavior<Entry>
    {
        const string numberRegex = "^[A-Z][a-z]*$";

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NameValidatorBehavior), false);

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
            bool islen = e.NewTextValue.Length > 2 ? true : false;
            bool isalphanumeric = (Regex.IsMatch(e.NewTextValue, numberRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            IsValid = islen && isalphanumeric;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            ((Entry)sender).Text = CheckLength(e.NewTextValue, 20);
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
