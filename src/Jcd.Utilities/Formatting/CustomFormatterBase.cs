using Jcd.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Jcd.Utilities.Formatting
{
    public abstract class CustomFormatterBase : IFormatProvider, ICustomFormatter, IComparer<Type>
    {
        private readonly Type[] handledTypes;
        private readonly Func<ICustomFormatter,string, object, IFormatProvider, string> formatFunction;
        protected CustomFormatterBase(IEnumerable<Type> handledTypes, Func<ICustomFormatter,string, object, IFormatProvider, string> formatFunction)
        {
            Argument.IsNotNull(handledTypes, nameof(handledTypes));
            Argument.IsNotNull(formatFunction, nameof(formatFunction));
            this.formatFunction = formatFunction;
            var ht = handledTypes.ToList();
            ht.Sort(this);
            this.handledTypes = ht.ToArray();
        }

        public virtual object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public virtual string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
            if (!ReferenceEquals(this,formatProvider))
                return null;

            if (Array.BinarySearch(handledTypes, arg.GetType(),this) >= 0){
                return formatFunction(this, fmt, arg, formatProvider);
            }
            return HandleOtherFormats(fmt, arg);
        }

        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            else if (arg != null)
                return arg.ToString();
            else
                return String.Empty;
        }

        public int Compare(Type x, Type y)
        {
            return x.ToString().CompareTo(y.ToString());
        }
    }
}
