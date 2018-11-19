namespace Reroll.Mobile.Droid.Converters
{
    using Reroll.Models.Enums;  
    using MvvmCross.Converters;
    using Android.App;
    using Android.Graphics.Drawables;
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts enum name to proper string name 
    /// </summary>
    public class DiceTypeNameConverter : MvxValueConverter<DiceTypeEnum, string>
    {
        protected override string Convert(DiceTypeEnum value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
