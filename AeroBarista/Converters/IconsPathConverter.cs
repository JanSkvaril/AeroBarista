using AeroBarista.Enums;
using AeroBarista.Models;
using System.Globalization;

namespace AeroBarista.Converters
{
    public class IconsPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            StepType? category = (StepType)value;
            if (category == null)
            {
                return string.Empty;
            }

            var path = "";
            if (category == StepType.Water)
            {
                path = "droplet.png";
            }
            else if (category == StepType.Grounds)
            {
                path = "coffeebean.png";
            }
            else if (category == StepType.Wait)
            {
                path = "clock.png";
            }
            else if (category == StepType.Movement)
            {
                path = "arrowsspin.png";
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
