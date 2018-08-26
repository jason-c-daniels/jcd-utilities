using System.Text;

namespace Jcd.Utilities.Test.TestHelpers
{
    /// <summary>
    ///    A validation helper. BigInteger formats hex numbers inconsistent from other int types (Zero Pads)
    /// </summary>
    public static class StringExtensions
   {
       /// <summary>
       ///    Removes leading zeros from a given bit of text.
       /// </summary>
       /// <param name="text">The text to remove leading zeros from.</param>
       public static string TrimLeadingZeros(this string text)
      {
         var sb = new StringBuilder();
         var isLeadingZero = true;

         foreach (var c in text)
         {
            isLeadingZero = isLeadingZero && (c == '0');

            if (!isLeadingZero) sb.Append(c);
         }

         if ((sb.Length == 0) &&
             (text.Length > 0)) sb.Append('0');

         return sb.ToString();
      }
   }
}
