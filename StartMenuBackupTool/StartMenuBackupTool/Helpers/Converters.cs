using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using StartMenuBackupTool.Properties;

namespace StartMenuBackupTool.Helpers
{
    // Boolean을 Visibility로 변환
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public static readonly BooleanToVisibilityConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }
            return false;
        }
    }

    // 문자열이 비어있지 않으면 Visible
    public class StringToVisibilityConverter : IValueConverter
    {
        public static readonly StringToVisibilityConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && !string.IsNullOrWhiteSpace(str))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // 파일 크기를 KB로 변환
    public class FileSizeConverter : IValueConverter
    {
        public static readonly FileSizeConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long bytes)
            {
                return bytes / 1024.0; // Convert to KB
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // 파일 경로에서 파일 이름만 추출
    public class FileNameConverter : IValueConverter
    {
        public static readonly FileNameConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string filePath && !string.IsNullOrWhiteSpace(filePath))
            {
                return $"{Resources.File}{Path.GetFileName(filePath)}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Date format converter for localization
    public class DateFormatConverter : IMultiValueConverter
    {
        public static readonly DateFormatConverter Instance = new();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is DateTime date && values[1] is double sizeKb)
            {
                var dateFormat = Resources.DateFormat;
                var formattedDate = string.Format(dateFormat, date);
                return $"{formattedDate} • {sizeKb:N0} KB";
            }
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}