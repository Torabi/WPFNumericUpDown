using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        /// <summary>
        /// return the regex pattern provided in data source
        /// </summary>
        internal override Regex _regexPattern {
            get{
                if (DATA!=null)
                {
                    return DATA._regex; // return the regex defined in subclass
                }
                else
                {
                    return new Regex("[0-9]+"); // by defualt get only number chars 
                }
            }
        }
        /// <summary>
        /// data source for the textbox 
        /// </summary>
        public GenericNumber<T> DATA
        {
            get { return (GenericNumber<T>)GetValue(DATAProperty); }
            set {
                
                SetValue(DATAProperty, value);
                dataBinding();
            }
        }

        /// <summary>
        /// dependency property for binding
        /// </summary>

        public static readonly DependencyProperty DATAProperty =
            DependencyProperty.Register("DATA", typeof(GenericNumber<T>), typeof(GenericNumericControl<T>),null);



        /// <summary>
        /// binds the model to UI.
        /// </summary>
        internal void dataBinding()
        {
            // binding the text box 
            Binding text_binding = new Binding("StringValue");
            text_binding.Source = DATA;
            text_binding.Mode = BindingMode.TwoWay;
            text_binding.UpdateSourceTrigger = UpdateSourceTrigger.LostFocus; // value updtes as text being modified
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, text_binding);

            // binding the increment button
            incBTN.Command = DATA.IncrementCommand;
            decBTN.Command = DATA.DecrementCommand;
        }
        /// <summary>
        /// empty constructor is require for XML code
        /// </summary>
        public GenericNumericControl() 
        {
            
        }

        /// <summary>
        /// constructor can be used from code.
        /// </summary>
        /// <param name="data">data to be binded with text box</param>
        public GenericNumericControl(GenericNumber<T> data )
        {
            DATA = data;        
        }
    }
}
