using AeroBarista.Shared.Models;
using System.Globalization;

namespace AeroBarista.Converters
{
    public class TotalTimeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            List<RecipeStepModel> steps = (List<RecipeStepModel>)value;
            if (steps == null)
            {
                return new TimeSpan();
            }

            TimeSpan result = new TimeSpan();

            foreach (var step in steps)
            {
                if (step.Time != null)
                {
                    result += (TimeSpan)step.Time;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
