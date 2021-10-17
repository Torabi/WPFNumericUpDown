﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    
    class IncrementCommand<T> :ICommand where T : struct, IFormattable, IComparable<T>
    {
        public event EventHandler CanExecuteChanged;

        public IncrementCommand  (GenericNumericControl<T> control) 
        {
            _control = control;
        }
        GenericNumericControl<T> _control; 
        public bool CanExecute(object parameter)
        {
            return _control.DATA.CompareTo(_control.Maximum) < 0;
        }

        public void Execute(object parameter)
        {
            _control.DATA = _control.Increment();   
        }
    }
}
