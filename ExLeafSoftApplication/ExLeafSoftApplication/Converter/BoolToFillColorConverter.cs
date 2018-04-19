using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Converter
{


    public class BoolToFillColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool b = (bool)value;
            Style st = null;
            if (b)
            {
                st = new Style(typeof(Image))
                {
                    Setters = {
                        new Setter{ Property= Image.SourceProperty, Value="tick.png"}
                    }

                };

            }
            else
            {
                st = new Style(typeof(Image))
                {
                    Setters = {
                        new Setter{ Property= Image.SourceProperty, Value="cross.png"}
                    }

                };
            }

            return st;



        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
