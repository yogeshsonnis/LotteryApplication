using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;


namespace Lottery_Application.Converters
{
    //[ValueConversion(typeof(string), typeof(SolidColorBrush))]
    public class CallStatusEnumToBackgroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((string)value)
            {
                case "Active":
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 153, 204, 51)); //Brushes.Beige;153, 204, 51, 100
                //return Brushes.Red;
                case "Empty":
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 127, 127, 127)); //Brushes.Beige; 
                case "Deactivated":
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 127, 127, 127)); //Brushes.Beige;
                case "SoldOut":
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 237, 32, 37)); //Brushes.Beige; 
                case "Settle":
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 153, 204, 51));
                case "Close":
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 165, 0));
                default:
                    return null;
            }
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

       


    }
}
