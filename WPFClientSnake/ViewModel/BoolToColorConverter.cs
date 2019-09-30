//-----------------------------------------------------------------------
// <copyright file="BoolToColorConverter.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace WPFClientSnake
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// The <see cref="BoolToColorConverter"/> class.
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        /// <summary>
        /// This method converts a boolean to a color.
        /// </summary>
        /// <param name="value"> The object value. </param>
        /// <param name="targetType"> The target type. </param>
        /// <param name="parameter"> The object parameter. </param>
        /// <param name="culture"> The culture info. </param>
        /// <returns> It returns an object. </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool == false)
            {
                throw new ArgumentException("Error cant convert value.");
            }

            if (!(bool)value)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.Green;
            }
        }

        /// <summary>
        /// This method converts a color to a boolean.
        /// </summary>
        /// <param name="value"> The object value. </param>
        /// <param name="targetType"> The target type. </param>
        /// <param name="parameter"> The object parameter. </param>
        /// <param name="culture"> The culture info. </param>
        /// <returns> It returns an object. </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not implemented");
        }
    }
}
