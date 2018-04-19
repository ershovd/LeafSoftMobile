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
	public partial class FieldListView : ContentView
	{
        /*
         The Placeholder for the BindableProperty 'CheckboxProperty' should always match the name without 'Property'.

So instead of 'Checked' it should be 'Checkbox'.

Its a naming convention that Xamarin goes by. Took me a while to figure out too as it wasn't well documented.
     */

        public static  BindableProperty ContentExistProperty =
BindableProperty.Create("ContentExist", typeof(string), typeof(FieldListView),string.Empty,propertyChanged:OnEventFired);

        public static BindableProperty SelectedFarmerProperty =
BindableProperty.Create("SelectedFarmer", typeof(FarmerModel), typeof(FieldListView),null, propertyChanged: OnFarmerChanged);

        public static BindableProperty SelectedFarmerAddressProperty =
BindableProperty.Create("SelectedFarmerAddress", typeof(AddressFarmerModel), typeof(FieldListView), null);

        public FieldListView ()
		{
			InitializeComponent ();
          
		}

        private static FarmerModel thisFarmer;

        public FarmerModel SelectedFarmer
        {
            get { return (FarmerModel)GetValue(SelectedFarmerProperty); }
            set {
                
                SetValue(SelectedFarmerProperty, value); }
        }

        public AddressFarmerModel SelectedFarmerAddress
        {
            get { return (AddressFarmerModel)GetValue(SelectedFarmerAddressProperty); }
            set
            {

                SetValue(SelectedFarmerAddressProperty, value);
            }
        }



        public string ContentExist
        {
            get { return (string)GetValue(ContentExistProperty); }
            set
            {
                SetValue(ContentExistProperty, value);
            }
        }


        public static void OnEventFired(BindableObject obj,object old,object newitem)
        {
            string item = (obj as FieldListView).ContentExist;
            View content = (obj as FieldListView).Content;
            FarmerModel a = (obj as FieldListView).SelectedFarmer;
            AddressFarmerModel b = (obj as FieldListView).SelectedFarmerAddress;

            if (item == "MapView")
            {
                //https://forums.xamarin.com/discussion/87415/how-to-display-content-from-one-xaml-inside-another
                (obj as FieldListView).FindByName<ContentView>("farmerDetailView").Content  = new MapView();
              


            }
            else if(item == "FieldList")
            {
                (obj as FieldListView).FindByName<ContentView>("farmerDetailView").Content = new TemplateFieldListView(a,b);
            }
        }


        public static void OnFarmerChanged(BindableObject obj, object old, object newitem)
        {
            thisFarmer = (obj as FieldListView).SelectedFarmer;
          
        }





    }
}