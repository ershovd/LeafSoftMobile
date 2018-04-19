using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Validator
{
    public class BirthDateValidatorBehavior : Behavior<DatePicker>
    {
       
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(BirthDateValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;


        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            DatePicker obj = bindable as DatePicker;
            obj.DateSelected += HandleTextChanged;
        }

        void HandleTextChanged(object sender, EventArgs e)
        {
            IsValid = ((DatePicker)sender).Date >= DateTime.Now.Date ? false : true;
           
            ((DatePicker)sender).TextColor = IsValid ? Color.Default : Color.Red;  
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            DatePicker obj = bindable as DatePicker;
            obj.DateSelected -= HandleTextChanged;

        }
    }
}
