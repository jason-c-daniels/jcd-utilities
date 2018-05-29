using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Jcd.Utilities.Formatting
{
    /// <summary>
    /// A base class to simplify custom formatter implementation by requiring the implementer to only provide an array of handled types, and a formatting function.
    /// </summary>
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

        /// <summary>
        /// This is the signature which custom formatting functions must abide by.
        /// </summary>
        /// <param name="customFormatter">The custom formatter object.</param>
        /// <param name="formatString">the format string.</param>
        /// <param name="argToFormat">The item to format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns></returns>
        public delegate string CustomFormattingFunction(ICustomFormatter customFormatter, string formatString, object argToFormat, IFormatProvider formatProvider);

        /// <summary>
        /// Constructs a custom formatter, and enforces some common rules.
        /// </summary>
        /// <param name="handledTypes">The data types the derived type will handle.</param>
        /// <param name="formatFunction">The formatting function, provided by the derived type, abiding by the CustomFormattingFunction signature</param>
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

        /// <summary>
        /// Gets the format object. (this)
        /// </summary>
        /// <param name="formatType">The data type for the format type</param>
        /// <returns>this if custom formatting requested.</returns>
        public virtual object GetFormat(Type formatType)
        {
            Argument.IsNotNull(formatType);
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fmt"></param>
        /// <param name="arg"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
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
