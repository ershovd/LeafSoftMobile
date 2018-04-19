using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExLeafSoftApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderSelectionsPage : ContentPage
	{

        public static BindableProperty SelectedItemProperty =
BindableProperty.Create("SelectedItem", typeof(string), typeof(OrderSelectionsPage), string.Empty, propertyChanged: OnEventFired);

        public static BindableProperty SelectedFarmerProperty =
BindableProperty.Create("SelectedFarmer", typeof(FarmerModel), typeof(OrderSelectionsPage), null);



        public FarmerModel SelectedFarmer
        {
            get { return (FarmerModel)GetValue(SelectedFarmerProperty); }
            set
            {

                SetValue(SelectedFarmerProperty, value);
            }
        }

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }


        public static void OnEventFired(BindableObject obj, object old, object newitem)
        {
            string item = (obj as OrderSelectionsPage).SelectedItem;
            FarmerModel farmer = (obj as OrderSelectionsPage).SelectedFarmer;


            if (item == "SelectFarmer")
            {
                //https://forums.xamarin.com/discussion/87415/how-to-display-content-from-one-xaml-inside-another
                (obj as OrderSelectionsPage).FindByName<ContentPage>("OrderSelection").Content = new OrderFarmerContent();

            }
            else if (item == "SelectField")
            {
                (obj as OrderSelectionsPage).FindByName<ContentPage>("OrderSelection").Content = new OrderFieldContent(farmer);
            }
        }
        protected override bool OnBackButtonPressed()
        {
            OrderSelectionsPage s = (OrderSelectionsPage) Navigation.NavigationStack.Where(a => a.Title == "Choose").FirstOrDefault();
            Navigation.RemovePage(s);
            return false;
        }

        //protected override void OnDisappearing()
        //{
        //    OrderSelectionsPage a = (OrderSelectionsPage)Navigation.NavigationStack.Where(k => k.Title == "Choose").FirstOrDefault();
        //    Navigation.RemovePage(a);
        //}

        public OrderSelectionsPage (string style= "SelectFarmer",FarmerModel farmer=null)
		{
			InitializeComponent ();
            SelectedFarmer = farmer;
            SelectedItem = style;
            
		}

      
    }
}