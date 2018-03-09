using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Oetcker.Models.Constants;

namespace Oetcker.Models.Converters
{
    public class QualityToColorConverter : IValueConverter
    {
        #region Methods

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var quality = (ItemConstants.Quality?)value;
            switch (quality)
            {
                case ItemConstants.Quality.Artifact:
                    return new SolidColorBrush(Colors.OrangeRed);
                case ItemConstants.Quality.Common:
                    return new SolidColorBrush(Colors.White);
                case ItemConstants.Quality.Epic:
                    return new SolidColorBrush(Colors.BlueViolet);
                case ItemConstants.Quality.Rare:
                    return new SolidColorBrush(Colors.Blue);
                case ItemConstants.Quality.Legendary:
                    return new SolidColorBrush(Colors.Orange);
                case ItemConstants.Quality.Poor:
                    return new SolidColorBrush(Colors.Gray);
                case ItemConstants.Quality.Uncommon:
                    return new SolidColorBrush(Colors.LawnGreen);
                default:
                    return new SolidColorBrush(Colors.Black);

            }
        }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
