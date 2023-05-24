using System.Globalization;
 
namespace MessengerApp.Converters;
public class FromRefToIcon : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value == null)
        {
            return "dotnet_bot.svg";
        }
        return value;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
