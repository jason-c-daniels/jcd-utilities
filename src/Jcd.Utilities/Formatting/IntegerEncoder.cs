using System;
using System.Collections.Generic;
using System.Numerics;

using Jcd.Utilities.Extensions;
using Jcd.Utilities.Validations;

namespace Jcd.Utilities.Formatting
{
   /// <summary>
   ///    A class that performs integer encoding to text in an arbitrary base, as well as parsing
   ///    text encoded in the same manner.
   /// </summary>
   public class IntegerEncoder : CustomFormatterBase, IIntegerFormatter, IIntegerParser
   {
      #region Public Fields

      /// <summary>
      ///    The numeric base of the encoder
      /// </summary>
      public readonly ushort Base;

      /// <summary>
      ///    Indicates if the characters that are decoded/encoded are case sensitive.
      /// </summary>
      public readonly bool CaseSensitive;

      /// <summary>
      ///    The character set used for encoding and decoding (for simple decoders)
      /// </summary>
      public readonly string CharacterSet;

      /// <summary>
      ///    A flag that indicates if the values of the characterset are numerically increasing. Using this can allow for faster
      ///    sorts of short numbers by NOT decoding first. (i.e. For positive numbers the text will sort the same as the number)
      /// </summary>
      public readonly bool CharacterSetValuesAlwaysIncrease;

      #endregion Public Fields

      #region Private Fields

      private static readonly Type[] FormattableTypes =
      {
         typeof(byte),
         typeof(sbyte),
         typeof(ushort),
         typeof(short),
         typeof(int),
         typeof(uint),
         typeof(long),
         typeof(ulong)
      };

      private readonly Dictionary<char, int> _charToValue = new Dictionary<char, int>();

      #endregion Private Fields

      #region Public Constructors

      /// <summary>
      ///    Constructs an encoder when given a character set to encode to, and an array of decode
      ///    mappings. (This is to support Crockford encoding/decoding)
      /// </summary>
      /// <param name="encodeCharacterSet">
      ///    The set of characters to use when encoding a number to text.
      /// </param>
      /// <param name="decodeCharacterSet">
      ///    The set of decode character mappings (i.e. which sets of characters map to which numeric
      ///    base value.)
      /// </param>
      public IntegerEncoder(string encodeCharacterSet, string[] decodeCharacterSet)
         : base(FormattableTypes, Format)
      {
         Argument.IsNotNullWhitespaceOrEmpty(encodeCharacterSet, nameof(encodeCharacterSet));
         Argument.IsNotNull(decodeCharacterSet, nameof(decodeCharacterSet));
         Argument.HasItems(decodeCharacterSet, nameof(decodeCharacterSet));

         Argument.AreEqual(decodeCharacterSet.Length,
                           encodeCharacterSet.Length,
                           message: "decodeCharacterSet and encodeCharacterSet must be the same length.");

         CaseSensitive = true;
         CharacterSet = encodeCharacterSet;
         Base = (ushort) CharacterSet.Length;

         for (var i = 0; i < decodeCharacterSet.Length; i++)
         {
            foreach (var c in decodeCharacterSet[i])
            {
               _charToValue.Add(c, i);
            }
         }

         // now validate that there is the ability to decode that which we encode.
         for (var i = 0; i < encodeCharacterSet.Length; i++)
         {
            var ec = encodeCharacterSet[i];

            if (!_charToValue.ContainsKey(ec))
            {
               throw new ArgumentException($"Encoding char: {ec} was not found in the decode set.",
                                           nameof(encodeCharacterSet));
            }

            var dv = _charToValue[ec];

            if (dv != i)
            {
               throw new ArgumentException(
                                           $"Encoding char: {ec} was expected to decode to {i} but decoded to {dv}.",
                                           nameof(decodeCharacterSet));
            }
         }
      }

      /// <summary>
      ///    Constructs an encoder when given an alphabet with exact encoding to decoding matching.
      /// </summary>
      /// <param name="characterSet">
      ///    The character set to use for encoding and decoding. (where length = n, char at index 0=0,
      ///    char at n-1=n-1)
      /// </param>
      /// <param name="caseSensitive">indicates if the characters are case sensitive for encoding/decoding.</param>
      public IntegerEncoder(string characterSet, bool caseSensitive = false)
         : base(FormattableTypes, Format)
      {
         Argument.IsNotNullWhitespaceOrEmpty(characterSet, nameof(characterSet));
         Argument.IsGreaterThan(characterSet.Length, 0, "characterSet.Length");
         CaseSensitive = caseSensitive;
         CharacterSet = caseSensitive ? characterSet : characterSet.ToLowerInvariant();
         Base = (ushort) CharacterSet.Length;
         var i = 0;
         var pc = '\0';
         CharacterSetValuesAlwaysIncrease = true;

         foreach (var c in CharacterSet)
         {
            _charToValue.Add(c, i);

            if (CharacterSetValuesAlwaysIncrease && (pc > c)) CharacterSetValuesAlwaysIncrease = false;

            pc = c;
            i++;
         }
      }

      #endregion Public Constructors

      #region Public Methods

      /// <inheritdoc />
      public string Format(uint value)
      {
         var sb = new List<char>();
         var cv = value;

         while (cv > 0)
         {
            var r = (int) (cv % Base);
            sb.Add(CharacterSet[r]);
            cv = cv / Base;
         }

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public string Format(ulong value)
      {
         var sb = new List<char>();
         var cv = value;

         while (cv > 0)
         {
            var r = (int) (cv % Base);
            sb.Add(CharacterSet[r]);
            cv = cv / Base;
         }

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public string Format(ushort value)
      {
         var sb = new List<char>();
         var cv = value;

         while (cv > 0)
         {
            var r = cv % Base;
            sb.Add(CharacterSet[r]);
            cv = (ushort) (cv / Base);
         }

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public string Format(byte value)
      {
         var sb = new List<char>();
         var cv = value;

         while (cv > 0)
         {
            var r = cv % Base;
            sb.Add(CharacterSet[r]);
            cv = (byte) (cv / Base);
         }

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public string Format(int value)
      {
         var sb = new List<char>();
         var cv = value;

         if (cv < 1)
         {
            while (cv < 0)
            {
               var r = cv % Base;
               sb.Add(CharacterSet[Math.Abs(r)]);
               cv = cv / Base;
            }
         }
         else
         {
            while (cv > 0)
            {
               var r = cv % Base;
               sb.Add(CharacterSet[r]);
               cv = cv / Base;
            }
         }

         if (value < 0) sb.Add('-');

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public string Format(long value)
      {
         var sb = new List<char>();
         var cv = value;

         if (cv < 1)
         {
            while (cv < 0)
            {
               var r = (int) (cv % Base);
               sb.Add(CharacterSet[Math.Abs(r)]);
               cv = cv / Base;
            }
         }
         else
         {
            while (cv > 0)
            {
               var r = (int) (cv % Base);
               sb.Add(CharacterSet[r]);
               cv = cv / Base;
            }
         }

         if (value < 0) sb.Add('-');

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public string Format(short value)
      {
         var sb = new List<char>();
         var cv = value;

         if (cv < 1)
         {
            while (cv < 0)
            {
               var r = cv % Base;
               sb.Add(CharacterSet[Math.Abs(r)]);
               cv = (short) (cv / Base);
            }
         }
         else
         {
            while (cv > 0)
            {
               var r = cv % Base;
               sb.Add(CharacterSet[r]);
               cv = (short) (cv / Base);
            }
         }

         if (value < 0) sb.Add('-');

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public string Format(sbyte value)
      {
         var sb = new List<char>();
         var cv = value;

         if (cv < 1)
         {
            while (cv < 0)
            {
               var r = cv % Base;
               sb.Add(CharacterSet[Math.Abs(r)]);
               cv = (sbyte) (cv / Base);
            }
         }
         else
         {
            while (cv > 0)
            {
               var r = cv % Base;
               sb.Add(CharacterSet[r]);
               cv = (sbyte) (cv / Base);
            }
         }

         if (value < 0) sb.Add('-');

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public string Format(BigInteger value)
      {
         var sb = new List<char>();
         var cv = value;

         if (cv < 1) cv *= -1;

         while (cv > 0)
         {
            var br = cv % (int) Base;
            var r = (int) br;
            sb.Add(CharacterSet[r]);
            cv = cv / Base;
         }

         if (value < 0) sb.Add('-');

         return FormatResult(sb);
      }

      /// <inheritdoc />
      public BigInteger ParseBigInteger(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = (BigInteger) 0;
         var isNeg = value[0] == '-';
         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            result *= Base;

            if (!_charToValue.ContainsKey(digit))
            {
               throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
            }

            result += _charToValue[digit];
         }

         return isNeg ? -1 * result : result;
      }

      /// <inheritdoc />
      public byte ParseByte(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = (byte) 0;

         if (value[0] == '-')
         {
            throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
         }

         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            checked
            {
               result *= (byte) Base;

               if (!_charToValue.ContainsKey(digit))
               {
                  throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
               }

               result += (byte) _charToValue[digit];
            }
         }

         return result;
      }

      /// <inheritdoc />
      public short ParseInt16(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = (short) 0;
         var isNeg = value[0] == '-' ? (short) -1 : (short) 1;
         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            checked
            {
               result *= (short) Base;

               if (!_charToValue.ContainsKey(digit))
               {
                  throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
               }

               result += (short) (_charToValue[digit] * isNeg);
            }
         }

         return result;
      }

      /// <inheritdoc />
      public int ParseInt32(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = 0;
         var isNeg = value[0] == '-' ? -1 : 1;
         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            checked
            {
               result *= Base;

               if (!_charToValue.ContainsKey(digit))
               {
                  throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
               }

               result += _charToValue[digit] * isNeg;
            }
         }

         return result;
      }

      /// <inheritdoc />
      public long ParseInt64(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = (long) 0;
         long isNeg = value[0] == '-' ? -1 : 1;
         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            checked
            {
               result *= Base;

               if (!_charToValue.ContainsKey(digit))
               {
                  throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
               }

               result += _charToValue[digit] * isNeg;
            }
         }

         return result;
      }

      /// <inheritdoc />
      public sbyte ParseSByte(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = (sbyte) 0;
         var isNeg = (sbyte) (value[0] == '-' ? -1 : 1);
         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            checked
            {
               result *= (sbyte) Base;

               if (!_charToValue.ContainsKey(digit))
               {
                  throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
               }

               result += (sbyte) (_charToValue[digit] * isNeg);
            }
         }

         return result;
      }

      /// <inheritdoc />
      public ushort ParseUInt16(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = (ushort) 0;

         if (value[0] == '-')
         {
            throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
         }

         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            checked
            {
               result *= Base;

               if (!_charToValue.ContainsKey(digit))
               {
                  throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
               }

               result += (ushort) _charToValue[digit];
            }
         }

         return result;
      }

      /// <inheritdoc />
      public uint ParseUInt32(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = (uint) 0;

         if (value[0] == '-')
         {
            throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
         }

         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            checked
            {
               result *= Base;

               if (!_charToValue.ContainsKey(digit))
               {
                  throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
               }

               result += (uint) _charToValue[digit];
            }
         }

         return result;
      }

      /// <inheritdoc />
      public ulong ParseUInt64(string value)
      {
         Argument.IsNotNullWhitespaceOrEmpty(value, nameof(value));

         if (!CaseSensitive) value = value.ToLowerInvariant();

         var result = (ulong) 0;

         if (value[0] == '-')
         {
            throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
         }

         var digits = ExtractCoreDigits(value);

         foreach (var digit in digits)
         {
            checked
            {
               result *= Base;

               if (!_charToValue.ContainsKey(digit))
               {
                  throw new ArgumentOutOfRangeException($"{digit} cannont be decoded.");
               }

               result += (ulong) _charToValue[digit];
            }
         }

         return result;
      }

      /// <inheritdoc />
      public bool TryParseBigInteger(string value, ref BigInteger result)
      {
         try
         {
            result = ParseBigInteger(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      /// <inheritdoc />
      public bool TryParseByte(string value, ref byte result)
      {
         try
         {
            result = ParseByte(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      /// <inheritdoc />
      public bool TryParseInt16(string value, ref short result)
      {
         try
         {
            result = ParseInt16(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      /// <inheritdoc />
      public bool TryParseInt32(string value, ref int result)
      {
         try
         {
            result = ParseInt32(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      /// <inheritdoc />
      public bool TryParseInt64(string value, ref long result)
      {
         try
         {
            result = ParseInt64(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      /// <inheritdoc />
      public bool TryParseSByte(string value, ref sbyte result)
      {
         try
         {
            result = ParseSByte(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      /// <inheritdoc />
      public bool TryParseUInt16(string value, ref ushort result)
      {
         try
         {
            result = ParseUInt16(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      /// <inheritdoc />
      public bool TryParseUInt32(string value, ref uint result)
      {
         try
         {
            result = ParseUInt32(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      /// <inheritdoc />
      public bool TryParseUInt64(string value, ref ulong result)
      {
         try
         {
            result = ParseUInt64(value);

            return true;
         }
         catch
         {
            return false;
         }
      }

      #endregion Public Methods

      #region Private Methods

      private static string Format(ICustomFormatter formatter,
                                   string fmt,
                                   object value,
                                   IFormatProvider formatProvider)
      {
         if (formatter is IntegerEncoder intFormatter) return intFormatter.FormatObject(value);

         return formatter.Format(fmt, value, formatProvider);
      }

      private string ExtractCoreDigits(string value)
      {
         var isNeg = value[0] == '-';
         var s = isNeg ? 1 : 0;
         var l = value.Length - s;
         value = value.Substring(s, l);

         return value;
      }

      private string FormatObject(object value)
      {
         switch (Type.GetTypeCode(value.GetType()))
         {
            case TypeCode.Byte:

               return Format((byte) value);

            case TypeCode.SByte:

               return Format((sbyte) value);

            case TypeCode.UInt16:

               return Format((ushort) value);

            case TypeCode.UInt32:

               return Format((uint) value);

            case TypeCode.UInt64:

               return Format((ulong) value);

            case TypeCode.Int16:

               return Format((short) value);

            case TypeCode.Int32:

               return Format((int) value);

            case TypeCode.Int64:

               return Format((long) value);
         }

         if (value.IsIntegerType()) return Format((BigInteger) value);

         return value.ToString();
      }

      private string FormatResult(List<char> sb)
      {
         if (sb.Count == 0) sb.Add(CharacterSet[0]);

         sb.Reverse();

         return string.Join("", sb);
      }

      #endregion Private Methods
   }
}
