using Jcd.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Jcd.Utilities.Formatting
{
    public abstract class CustomFormatterBase : IFormatProvider, ICustomFormatter
    {
        protected class MyTypeComparer : IComparer<Type>
        {
            public int Compare(Type x, Type y)
            {
                return x.ToString().CompareTo(y.ToString());
            }
        }
        protected MyTypeComparer typeComparer = new MyTypeComparer();
        private readonly Type[] handledTypes;
        private readonly Func<ICustomFormatter,string, object, IFormatProvider, string> formatFunction;
        protected CustomFormatterBase(IEnumerable<Type> handledTypes, Func<ICustomFormatter,string, object, IFormatProvider, string> formatFunction)
        {
            Argument.IsNotNull(handledTypes, nameof(handledTypes));
            Argument.HasItems(handledTypes, nameof(handledTypes));
            Argument.IsNotNull(formatFunction, nameof(formatFunction));
            this.formatFunction = formatFunction;
            var ht = handledTypes.ToList();
            ht.Sort(typeComparer);
            this.handledTypes = ht.ToArray();
        }

        public virtual object GetFormat(Type formatType)
        {
            Argument.IsNotNull(formatType);
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public virtual string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
            Argument.IsNotNull(formatProvider,nameof(formatProvider));
            Argument.IsNotNull(arg,nameof(arg));
            Argument.IsNotNull(fmt, nameof(fmt));
            if (!ReferenceEquals(this,formatProvider))
                return null;

            if (Array.BinarySearch(handledTypes, arg.GetType(), typeComparer) >= 0){
                return formatFunction(this, fmt, arg, formatProvider);
            }
            return HandleOtherFormats(fmt, arg);
        }

        private string HandleOtherFormats(string format, object arg)
        {
#if DEBUG // this shouldn't ever be violated if the UTs are done correctly. For performance reasons it's omitted from release mode code.
            Argument.IsNotNull(arg, nameof(arg));
#endif
            if (arg is IFormattable formattable)
                return formattable.ToString(format, CultureInfo.CurrentCulture);
            else if (arg != null)
                return arg.ToString();
            else
                return String.Empty;
        }

    }
}
