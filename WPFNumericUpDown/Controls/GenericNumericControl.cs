using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
/*
MIT License
Copyright (c) 2021 ALI TORABI (ali@parametriczoo.com)
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
namespace WPFNumericUpDown
{
    /// <summary>
    /// class represent generic number. any other type must be sub-class of the this class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericNumericControl<T> : NumericControl where T : struct, IFormattable, IComparable<T>
    {
        //internal GenericNumber<T> genericNumber;
        /// <summary>
        /// return the regex pattern provided in data source
        /// </summary>
        internal override string _regexPattern {
            get
            {
                if (typeof(T).Equals(typeof(int)))
                {
                    return (toInt(Minimum) < 0) ? signedInteger : unSignedInteger;
                }
                else if (typeof(T).Equals(typeof(decimal)))
                {
                    return (toDecimal(Minimum) < 0) ? signedDecimal : unSignedDecimal;
                }
                else if (typeof(T).Equals(typeof(double)))
                {
                    return (toDouble(Minimum) < 0) ? signedDecimal : unSignedDecimal;
                }

                else
                {
                    return signedDecimal;
                }

            }
        }

        #region properties
        /// <summary>
        /// data source for the textbox 
        /// </summary>
        public T DATA
        {
            get { return (T)GetValue(DATAProperty); }
            set {
                
                SetValue(DATAProperty, value);
                
            }
        }

        /// <summary>
        /// dependency property for binding
        /// </summary>

        public static readonly DependencyProperty DATAProperty =
            DependencyProperty.Register("DATA", typeof(T), typeof(GenericNumericControl<T>), new PropertyMetadata(default(T)));




        public T Minimum
        {
            get { return (T)GetValue(MinimumProperty); }
            set 
            {
               
                SetValue(MinimumProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(T), typeof(GenericNumericControl<T>), new PropertyMetadata(default(T)));



        public T Maximum
        {
            get { return (T)GetValue(MaximumProperty); }
            set {
               
                SetValue(MaximumProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(T), typeof(GenericNumericControl<T>), new PropertyMetadata(default(T)));



        public T Inc
        {
            get { return (T)GetValue(IncProperty); }
            set 
            {
               
                SetValue(IncProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for Inc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncProperty =
            DependencyProperty.Register("Inc", typeof(T), typeof(GenericNumericControl<T>), new PropertyMetadata(default(T)));



        public int Decimals
        {
            get { return (int)GetValue(DecimalsProperty); }
            set 
            {
               
                SetValue(DecimalsProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for Decimals.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecimalsProperty =
            DependencyProperty.Register("Decimals", typeof(int), typeof(GenericNumericControl<T>), new PropertyMetadata(0));




        public string Suffix
        {
            get { return (string)GetValue(SuffixProperty); }
            set { SetValue(SuffixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Suffix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuffixProperty =
            DependencyProperty.Register("Suffix", typeof(string), typeof(GenericNumericControl<T>), new PropertyMetadata(""));





        #endregion

        #region Helper methods 

        int toInt(T val)
        {
            return (int)Convert.ChangeType(val, typeof(int));
        }
        decimal toDecimal(T val)
        {
            return (decimal)Convert.ChangeType(val, typeof(decimal));
        }
        double toDouble(T val)
        {
            return (double)Convert.ChangeType(val, typeof(double));
        }
        T toT(int val)
        {
            return (T)Convert.ChangeType(val, typeof(int));
        }
        T toT(decimal val)
        {
            return (T)Convert.ChangeType(val, typeof(decimal));
        }
        T toT(double val)
        {
            return (T)Convert.ChangeType(val, typeof(double));
        }
        #endregion

        #region internal abstartc and visrtula methods 

        /// <summary>
        /// returns <c>_value + _increment</c>
        /// </summary>
        /// <returns></returns>
        internal virtual T increment(T value)
        {
            if (typeof(T).Equals(typeof(int)))
            {
                return toT(toInt(value) + toInt(Inc));
            }
            else if (typeof(T).Equals(typeof(decimal)))
            {
                return toT(toDecimal(value) + toDecimal(Inc));
            }
            else if (typeof(T).Equals(typeof(double)))
            {
                return toT(toDouble(value) + toDouble(Inc));
            }
            else
            {
                throw new Exception("T expected int,decimal and double");
            }
        }



        /// <summary>
        /// returns <c>_value - _increment</c>
        /// </summary>
        /// <returns></returns>
        internal virtual T decrement(T value)
        {
            if (typeof(T).Equals(typeof(int)))
            {
                return toT(toInt(value) - toInt(Inc));
            }
            else if (typeof(T).Equals(typeof(decimal)))
            {
                return toT(toDecimal(value) - toDecimal(Inc));
            }
            else if (typeof(T).Equals(typeof(double)))
            {
                return toT(toDouble(value) - toDouble(Inc));
            }
            else
            {
                throw new Exception("T expected int,decimal and double");
            }
        }

        /// <summary>
        /// override to provide the format string used to format the string in the text box
        /// </summary>
        internal virtual string FormatString
        {
            get
            {
                if (typeof(T).Equals(typeof(int)))
                {
                    return "";
                }
                else if (typeof(T).Equals(typeof(decimal)))
                {
                    return $"F{Decimals}";
                }
                else if (typeof(T).Equals(typeof(double)))
                {
                    return $"N{Decimals}";
                }
                else
                {
                    throw new Exception("T expected int,decimal and double");
                }
            }

        }

        /// <summary>
        /// override this method to parse the current string to type (T)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public virtual bool ConvertToValue(string text, out T val)
        {
            bool result = false;
            if (typeof(T).Equals(typeof(int)))
            {
                result = int.TryParse(text, out int x);
                val = toT(x);
            }
            else if (typeof(T).Equals(typeof(decimal)))
            {
                result = decimal.TryParse(text, out decimal x);
                val = toT(x);
            }
            else if (typeof(T).Equals(typeof(double)))
            {
                result = double.TryParse(text, out double x);
                val = toT(x);
            }
            else
            {
                throw new Exception("T expected int,decimal and double");
            }
            return result;
        }
        #endregion


        /// <summary>
        /// binds the model to UI.
        /// </summary>
        internal void dataBinding()
        {
            //genericNumber = new GenericNumber<T>( Minimum, Maximum, Inc, Decimals);
            //genericNumber._min = Minimum;
            //genericNumber._max = Maximum;
            //genericNumber._increment = Inc;
            //genericNumber._decimals = Decimals;
            // binding the text box 
            Binding text_binding = new Binding("DATA");
            text_binding.Source = this;
            text_binding.Converter = new NumberConverter<T>();
            text_binding.ConverterParameter = this;
            text_binding.Mode = BindingMode.TwoWay;
            text_binding.UpdateSourceTrigger = UpdateSourceTrigger.LostFocus; // value updtes as text being modified
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, text_binding);


          

        }
        /// <summary>
        /// empty constructor is require for XML code
        /// </summary>
        public GenericNumericControl() 
        {
            // binding the increment button
            
            Loaded += GenericNumericControl_Loaded;
        }

        #region evenst

        private void GenericNumericControl_Loaded(object sender, RoutedEventArgs e)
        {
            dataBinding();
        }
        #endregion

        #region public methods

        /// <summary>
        /// increment the current value by adding the <c>_increment</c> value to the current value
        /// </summary>
        public T Increment()
        {
            T temp = increment(DATA);
            if (temp.CompareTo(Maximum) > 0)
            {
                return Maximum;
            }
            else
            {
                return temp;
            }
        }
        /// <summary>
        /// decrement the current value by subtracting the <c>_increment</c> value from the current value
        /// </summary>
        public T Decrement()
        {
            T temp = decrement(DATA);
            if (temp.CompareTo(Minimum) < 0)
            {
                return Minimum;
            }
            else
            {
                return temp;
            }
        }
        #endregion

        #region override properties 
        public override ICommand IncrementCommand => new IncrementCommand<T>(this);

        public override ICommand DecrementCommand => new DecrementCommmand<T>(this);
        #endregion

        #region static members (Regex)

        /// <summary>
        /// constructor can be used from code.
        /// </summary>
        /// <param name="data">data to be binded with text box</param>
        //public GenericNumericControl(GenericNumber<T> data )
        //{
        //    DATA = data;        
        //}

        /// <summary>
        /// filters the signed decimals
        /// </summary>
        internal string signedDecimal = @"^(-?)([0-9]*)(\.?)([0-9]*)$"; 
        /// <summary>
        /// filters the unsigned decimals
        /// </summary>
        internal string unSignedDecimal = @"^[0-9]*(\.?)[0-9]*$";
        /// <summary>
        /// filters the signed integers
        /// </summary>
        internal string signedInteger = @"^-?[0-9]+$";
        /// <summary>
        /// filters the unsigned integers
        /// </summary>
        internal string unSignedInteger = @"^[0-9]+$";
        #endregion
    }
}
