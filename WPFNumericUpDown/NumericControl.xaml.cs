using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public abstract partial class NumericControl : UserControl
    {

        #region members
        ICommand command;
        Timer timer;

        internal abstract string _regexPattern { get; }
        #endregion
        /// <summary>
        /// main constructor
        /// </summary>
        public NumericControl()
        {
            InitializeComponent();
            timer = new Timer()
            {
                Interval = 400
            };
            timer.Elapsed += Timer_Elapsed;
        }


        /// <summary>
        /// run the current command every 100 milliseconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher?.Invoke(() =>
            {
            if (command != null && command.CanExecute(null))
                    command.Execute(null);
            });
            timer.Interval = 100;
        }
        /// <summary>
        /// sets the command and starts the timer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void incBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //command = ((Button)sender).Command;
            Button btn = sender as Button;

            command = btn.Name == incBTN.Name? IncrementCommand : DecrementCommand ;
            if (command.CanExecute(null))
                command.Execute(null);
            timer.Start();
        }
        /// <summary>
        /// stops the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void incBTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            //command = ((Button)sender).Command;
            timer.Stop();
            timer.Interval = 400;


        }
        /// <summary>
        /// check if the content of the text box is valid. only if valid then it is pushed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !Regex.IsMatch(e.Text,_regexPattern);
        }

        public abstract ICommand IncrementCommand { get;}
        public abstract ICommand DecrementCommand { get; }

        
    }
}