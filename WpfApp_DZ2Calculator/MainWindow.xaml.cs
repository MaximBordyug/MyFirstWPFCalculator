using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp_DZ2Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        string numbText;
        double rez;
        bool isPoint;
        string textBlock;
        bool numberEnter;
        bool operatorTrue;
        int currentTextLength;
        string currentOperand;
        

        public MainWindow()
        {
            InitializeComponent();
            numbText = string.Empty;
            textBlock= string.Empty;
            rez = 0;

            textBlockInfo.Text = "0";
            operatorTrue = false;
        }

        private void clickCE(object sender, RoutedEventArgs e)
        {
            
            
            
            

            char lastChar = textBlockInfo.Text[textBlockInfo.Text.Length - 1];
           
           
            if (lastChar == '/' || lastChar == '*' || lastChar == '-' || lastChar == '+')
            {
                
            }
            else
            {
                if (textBlockInfo.Text != "0")
                {

                    textBlockInfo.Text = textBlockInfo.Text.Remove(textBlockInfo.Text.Length - BaseInfo.Text.Length);
                    
                    if (string.IsNullOrEmpty(textBlockInfo.Text))
                    {
                        textBlockInfo.Text = "0";
                    }
                   
                }
                
            }
            

            BaseInfo.Text = "0";
            numberEnter = false;






        }
        private void clickC(object sender, RoutedEventArgs e)
        {
            numbText = string.Empty;
            textBlockInfo.Text = "0"; 
            BaseInfo.Text = "0";
        }

        private void clickNumb(object sender, RoutedEventArgs e)
        {
            
            if (operatorTrue == true)
            {
                BaseInfo.Text = string.Empty;
                numbText = string.Empty;
                operatorTrue = false;
            }
            Button btn = sender as Button;

            if ((BaseInfo.Text == "0") )
            {
                BaseInfo.Text = BaseInfo.Text.Remove(BaseInfo.Text.Length - 1);
            }
            if ((BaseInfo.Text == "0") && (numberEnter == true))
            {
                textBlockInfo.Text = textBlockInfo.Text.Remove(textBlockInfo.Text.Length - 1);
                BaseInfo.Text = BaseInfo.Text.Remove(BaseInfo.Text.Length - 1);
            }
            else
            {
                
            }
            if (textBlockInfo.Text == "0")
                BaseInfo.Text = btn.Content.ToString();
            else
                BaseInfo.Text += btn.Content.ToString();

            if (textBlockInfo.Text == "0")
                textBlockInfo.Text = btn.Content.ToString();
            else
            textBlockInfo.Text += btn.Content.ToString();
            
            numbText += btn.Content.ToString();
            rez += Convert.ToDouble(numbText);

            if (textBlockInfo.Text.ToString().Contains("/0"))
            {
                MessageBox.Show("На ноль делить нельзя");
                textBlockInfo.Text = textBlockInfo.Text.Remove(textBlockInfo.Text.Length - 1);
                operatorTrue = true;
                return;
                
            }
            string pattern = @"\*(\d)";
            string replacement = "$1";
            if (Regex.IsMatch(textBlockInfo.Text, @"\*0\d")
              || Regex.IsMatch(textBlockInfo.Text, @"\+0\d")
              || Regex.IsMatch(textBlockInfo.Text, @"\/0\d")
               || Regex.IsMatch(textBlockInfo.Text, @"\-0\d"))
            {

                textBlockInfo.Text = Regex.Replace(textBlockInfo.Text, @"\*0(\d)", "*$1");
                textBlockInfo.Text = Regex.Replace(textBlockInfo.Text, @"\-0(\d)", "-$1");
                textBlockInfo.Text = Regex.Replace(textBlockInfo.Text, @"\+0(\d)", "+$1");
                textBlockInfo.Text = Regex.Replace(textBlockInfo.Text, @"\/0(\d)", "/$1");


                operatorTrue = true;
                return;

            }
            if (textBlockInfo.Text.ToString().Contains("/0"))
            {
                MessageBox.Show("На ноль делить нельзя");
                textBlockInfo.Text = textBlockInfo.Text.Remove(textBlockInfo.Text.Length - 1);
                operatorTrue = true;
                return;

            }

            numberEnter = true;

        }

        private void clickRez(object sender, RoutedEventArgs e)
        {
            
            if (numbText != string.Empty)
            {

                if (operatorTrue == true)
                {
                    textBlockInfo.Text = textBlockInfo.Text.Remove(textBlockInfo.Text.Length - 1);
                    operatorTrue = false;
                }

                try
                {
                    DataTable dt = new DataTable();
                    var v = dt.Compute(textBlockInfo.Text.ToString(), "");
                    MessageBox.Show("Результат: " + v.ToString());
                    textBlockInfo.Text = "0";
                    BaseInfo.Text = "0";
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Число слишком большое.");
                }
            }
        }

        private void clickPointer(object sender, RoutedEventArgs e)
        {
            
            
                if (!BaseInfo.Text.Contains("."))
                {
                if (numberEnter != false)
                {
                    BaseInfo.Text += ".";
                    textBlockInfo.Text += ".";
                    numberEnter = false;
                }
                 }
            
        }

        private void clickBackSpace(object sender, RoutedEventArgs e)
        {
            if(textBlockInfo.Text == "0")
            {
                numbText = "0";
            }

            
            char lastChar = textBlockInfo.Text[textBlockInfo.Text.Length - 1];

            
            if (Char.IsDigit(lastChar) || lastChar == '.')
            {
                if (!string.IsNullOrEmpty(numbText))
                {
                numbText = BaseInfo.Text;
                    numbText = numbText.Remove(numbText.Length - 1);
                    string pattern = @"\d+$";

                    if (Regex.Match("/", pattern).Success
                   || Regex.Match("*", pattern).Success
                   || Regex.Match("+", pattern).Success
                   || Regex.Match("-", pattern).Success)
                    {


                    }
                    else
                    {
                        textBlockInfo.Text = textBlockInfo.Text.Remove(textBlockInfo.Text.Length - 1);
                        numberEnter = false;
                    }
                    if (string.IsNullOrEmpty(textBlockInfo.Text))
                    {
                        textBlockInfo.Text = "0";

                    }

                    BaseInfo.Text = numbText;

                    if (string.IsNullOrEmpty(BaseInfo.Text))
                    {
                        BaseInfo.Text = "0";

                    }
                }
           
            }
        }

        private void clickOperation(object sender, RoutedEventArgs e)
        {

            operatorTrue = true;
            isPoint = false;
            Button btn = sender as Button;
            string value = btn.Content.ToString();
            if (value == ".")
            {
                isPoint = true;
                MessageBox.Show("sdfsdf");
            }
           
            if ( BaseInfo.Text != "0")
            { 
            double doubleNumber = Convert.ToDouble(numbText);
            
            if (BaseInfo.Text == "0")
            {
                textBlockInfo.Text += "0";
            }
                if (isPoint == false)
                { 
               if (numberEnter)
                  { 
           
                currentOperand = btn.Content.ToString();


                textBlockInfo.Text += btn.Content.ToString();
                numberEnter = false;
                        isPoint = true;
                    }
                }

            }

        }

        //private void clickZero(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(currentOperand) && (currentOperand == "\\"))
        //    {
        //        MessageBox.Show("На ноль делить нельзя");
        //    }
        //    else
        //    {
        //        if (operatorTrue == true)
        //        {
        //            BaseInfo.Text = string.Empty;
        //            numbText = string.Empty;
        //            operatorTrue = false;
        //        }
        //        if (BaseInfo.Text != "0")
        //        { 
        //            textBlockInfo.Text += "0";
        //            BaseInfo.Text += "0";
        //        }
        //        //if (textBlockInfo.Text.ToString().Contains("\\0"))
        //        //{
        //        //    textBlockInfo.Text = textBlockInfo.Text.Remove(textBlockInfo.Text.Length - 1);
        //        //    return;
        //        //}


        //    }
        //}
    }


}
