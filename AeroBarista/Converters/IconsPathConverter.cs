using AeroBarista.Shared.Enums;
using AeroBarista.Shared.Models;
using System.Globalization;

namespace AeroBarista.Converters
{
    public class IconsPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string def = "blank.png";
            if (value == null)
            {
                return def;
            }
            StepType? category = (StepType)value;
            if (category == null)
            {
                return def;
            }

            var path = def;
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
