using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Validator
{
    public class AddressValidatorBehavior : Behavior<Entry>
    {
       

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(AddressValidatorBehavior), false);

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
            IsValid = e.NewTextValue.Length > 30 || e.NewTextValue.Length <3 ? false : true;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            ((Entry)sender).Text = CheckLength(e.NewTextValue, 30);
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
