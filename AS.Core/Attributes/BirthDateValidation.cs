using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Faisalman.AgeCalc;

namespace AS.Core.Attributes
{
    public class BirthDateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var bd = (DateTime) value;
            var calc = new Age(bd,DateTime.Now);
            return bd < DateTime.Now && calc.Years < 150;
        }
    }
}