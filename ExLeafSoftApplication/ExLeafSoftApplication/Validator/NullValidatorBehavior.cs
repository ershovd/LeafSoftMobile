using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Validator
{
    public class NullValidatorBehavior : Behavior<Picker>
    {
       
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NullValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;


        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            Picker obj = bindable as Picker;

            obj.SelectedIndexChanged += HandleTextChanged;
        }

        void HandleTextChanged(object sender, EventArgs e)
        {
            IsValid = ((Picker)sender).SelectedItem == null ? false : true;
            ((Picker)sender).TextColor = IsValid ? Color.Default : Color.Red;
           
        }

     

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            Picker obj = bindable as Picker;
            obj.SelectedIndexChanged -= HandleTextChanged;

        }
    }
}
