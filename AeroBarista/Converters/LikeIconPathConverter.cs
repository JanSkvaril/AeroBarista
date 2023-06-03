using System.Globalization;

namespace AeroBarista.Converters
{
    public class LikeIconPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string def = "blank.png";
            if (value == null)
            {
                return def;
            }

            bool isLiked = (bool)value;
            
            if (isLiked)
            {
                return "heart.png";
            }
            return "heartempty.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
