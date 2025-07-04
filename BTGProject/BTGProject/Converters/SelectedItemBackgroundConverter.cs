using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace BTGProject.Converters
{
    public class SelectedItemBackgroundConverter : IValueConverter
    {
        public Color SelectedColor { get; set; } = Color.FromArgb("#D1E8FF");
        public Color DefaultColor { get; set; } = Colors.Transparent;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // value: o item atual do DataTemplate
            // parameter: o CollectionView (passado via ConverterParameter)
            if (parameter is CollectionView collectionView && collectionView.SelectedItem != null)
            {
                if (ReferenceEquals(value, collectionView.SelectedItem))
                    return SelectedColor;
            }
            return DefaultColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}