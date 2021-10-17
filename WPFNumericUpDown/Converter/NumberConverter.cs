using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFNumericUpDown
{
    public class NumberConverter<T> : IValueConverter where T : struct, IFormattable, IComparable<T> 
    {
     
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // converting to string 
            
            GenericNumericControl<T> control = (GenericNumericControl<T>)parameter;
            T val = (T)value;
            string text = val.ToString(control.FormatString, culture);
            if (string.IsNullOrEmpty(control.Suffix))
                return text;
            else 
                return  text + " " +control.Suffix;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // coverting to T 
            GenericNumericControl<T> control = (GenericNumericControl<T>)parameter;
            string text = (string)value;
            if (!string.IsNullOrEmpty( control.Suffix))
            {
                
                var t= text.GetEnumerator();
                //t.MoveNext();
                string current = "";// t.Current.ToString();
                string filtered = "";
                while (t.MoveNext())
                {
                    current += t.Current;
                    if (Regex.Match(current, control._regexPattern).Success)
                        filtered = current;
                    else
                        break;
                    
                }
                text = filtered;
            }
            if (control.ConvertToValue(text, out T result))
                return result;
            else
                return default(T);
        }
    }
}
