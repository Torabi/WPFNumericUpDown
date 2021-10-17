using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    /// Class represent a numeric up down control for double vlaue
    /// </summary>
    public class DoubleNumericControl : GenericNumericControl<double>
    {
        /// <summary>
        /// default constructor for double numbder with 4 decimals (default value 0)
        /// </summary>
        public DoubleNumericControl()
        {
            // assign a default value 
            //DATA = new DoubleNumber(0, double.NegativeInfinity, double.PositiveInfinity, 1,4);
            //Minimum = double.NegativeInfinity;
            //Maximum = double.PositiveInfinity;
            //Inc = 1;
            //genericNumber = new DoubleNumber(double.NegativeInfinity, double.PositiveInfinity, 1, 6);
        }

        internal override string FormatString => $"N{Decimals}";
        internal override double increment(double value)
        {
            return value + Inc;
        }
        internal override double decrement(double value)
        {
            return value - Inc;
        }
        internal override string _regexPattern => (Minimum < 0) ? signedDecimal : unSignedDecimal;

        public override bool ConvertToValue(string text, out double val)
        {
            return double.TryParse(text, out val);
        }
    }
}
