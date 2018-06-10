using System;
using System.Collections.Generic;
using System.Text;

namespace Jcd.Utilities.Test.TestHelpers
{
   public static class StringExtensions
   {
      public static string TrimLeadingZeros(this string text)
      {
         var sb = new StringBuilder();
         bool isLeadingZero = true;

         foreach (char c in text)
         {
            isLeadingZero = isLeadingZero && c == '0';

            if (!isLeadingZero) {
               sb.Append(c);
            }
         }

         return sb.ToString();
      }
   }
}
