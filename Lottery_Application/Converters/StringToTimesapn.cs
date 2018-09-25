using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Lottery_Application.Converters
{
    public class StringToTimesapn : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TimeSpan dt;
            if(value!=null)
            {
                dt = TimeSpan.Parse(value.ToString());
                return dt;
            }
            return dt;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
