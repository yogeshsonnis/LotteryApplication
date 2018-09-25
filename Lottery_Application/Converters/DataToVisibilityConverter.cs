using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Lottery_Application.Converters
{
    public class DataToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility dataVisible = Visibility.Collapsed;
            if(value != null)
            {
                dataVisible = (value.ToString() == "Settle") ? Visibility.Visible : Visibility.Collapsed;
                return dataVisible;
            }
            else
            {
                return dataVisible;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
