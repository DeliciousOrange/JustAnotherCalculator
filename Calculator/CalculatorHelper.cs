using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorHelper
    {

        private CalculatorHelper() { }

        private static CalculatorHelper _instance = null;

        public static CalculatorHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CalculatorHelper();
            }
            return _instance;
        }

        public string Evaluate(string expr)
        {
            try
            {
                return new Expression(expr).Evaluate().ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
