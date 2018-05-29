using Jcd.Utilities.Extensions;
using Jcd.Utilities.Formatting;
using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Jcd.Utilities
{
    /// <summary>
    /// A class that will perform integer encoding to text in an arbitrary base, as well as parsing text encoded in the same manner.
    /// </summary>
    public class IntegerEncoder : CustomFormatterBase, IIntegerFormatter, IIntegerParser
    {
        private readonly Dictionary<char, int> charToValue = new Dictionary<char, int>();
        public readonly string CharacterSet;
        public readonly bool CaseSensitive;
        public readonly bool CharacterSetValuesAlwaysIncrease;
        public readonly ushort Base;
        static Type[] formattableTypes = { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(int), typeof(uint), typeof(long), typeof(ulong) };

        /// <summary>
        /// Constructs an encoder when given a character set to encode to, and an array of decode mappings. (This is to support Crockford encoding/decoding)
        /// </summary>
        /// <param name="encodeCharacterSet">The set of characters to use when encoding a number to text.</param>
        /// <param name="decodeCharacterSet">The set of decode character mappings (i.e. which sets of characters map to which numeric base value.)</param>
        public IntegerEncoder(string encodeCharacterSet, string[] decodeCharacterSet)
            : base(formattableTypes, Format)
        {
            Argument.IsNotNullWhitespaceOrEmpty(encodeCharacterSet, nameof(encodeCharacterSet));
            Argument.IsNotNull(decodeCharacterSet, nameof(decodeCharacterSet));
            Argument.HasItems(decodeCharacterSet, nameof(decodeCharacterSet));
            Argument.AreEqual(decodeCharacterSet.Length, encodeCharacterSet.Length, message: "decodeCharacterSet and encodeCharacterSet must be the same length.");
            this.CaseSensitive = true;
            this.CharacterSet = encodeCharacterSet;
            Base = (ushort)CharacterSet.Length;
            for (int i = 0; i < decodeCharacterSet.Length; i++)
            {
                foreach (char c in decodeCharacterSet[i])
                {
                    charToValue.Add(c, i);
                }
            }
        }

        /// <summary>
        /// Constructs an encoder when given an alphabet with exact encoding to decoding matching.
        /// </summary>
        /// <param name="characterSet">The character set to use for encoding and decoding. (where length = n, char at index 0=0, char at n-1=n-1)</param>
        /// <param name="caseSensitive">indicates if the characters are case sensitive for encoding/decoding.</param>
        public IntegerEncoder(string characterSet, bool caseSensitive = false)
            : base(formattableTypes, Format)
        {
            Argument.IsNotNullWhitespaceOrEmpty(characterSet, nameof(characterSet));
            Argument.IsGreaterThan(characterSet.Length, 0, "characterSet.Length");
            this.CaseSensitive = caseSensitive;
            this.CharacterSet = caseSensitive ? characterSet : characterSet.ToLowerInvariant();
            Base = (ushort)CharacterSet.Length;
            int i = 0;
            char pc='\0';
            CharacterSetValuesAlwaysIncrease = true;
            foreach (char c in CharacterSet)
            {
                charToValue.Add(c, i);
                if (CharacterSetValuesAlwaysIncrease && pc >= c) CharacterSetValuesAlwaysIncrease = false;
                i++;
            }
        }
        
        /// <summary>
        /// Formats an unsigned int using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>
        public string Format(uint value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)(cv % Base);
                sb.Add(CharacterSet[r]);
                cv = (cv / (uint)Base);
            }
            return FormatResult(sb);
        }

        /// <summary>
        /// Formats an unsigned long using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>
        public string Format(ulong value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)(cv % Base);
                sb.Add(CharacterSet[r]);
                cv = (cv / (ulong)Base);
            }
            return FormatResult(sb);
        }

        /// <summary>
        /// Formats an unsigned short using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>
        public string Format(ushort value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)(cv % Base);
                sb.Add(CharacterSet[r]);
                cv = (ushort)(cv / Base);
            }
            return FormatResult(sb);
        }

        /// <summary>
        /// Formats a byte using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>
        public string Format(byte value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)(cv % Base);
                sb.Add(CharacterSet[r]);
                cv = (byte)(cv / Base);
            }
            return FormatResult(sb);
        }

        /// <summary>
        /// Formats an int using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>
        public string Format(int value)
        {
            var sb = new List<char>();
            var cv = value;
            if (cv < 1) cv *= -1;
            while (cv > 0)
            {
                var r = (int)(cv % Base);
                sb.Add(CharacterSet[r]);
                cv = (cv / (int)Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        /// <summary>
        /// Formats a long using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>
        public string Format(long value)
        {
            var sb = new List<char>();
            var cv = value;
            if (cv < 1) cv *= -1;
            while (cv > 0)
            {
                var r = (int)(cv % Base);
                sb.Add(CharacterSet[r]);
                cv = (cv / (long)Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        /// <summary>
        /// Formats a short using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>        
        public string Format(short value)
        {
            var sb = new List<char>();
            var cv = value;
            if (cv < 1) cv *= -1;
            while (cv > 0)
            {
                var r = (int)(cv % Base);
                sb.Add(CharacterSet[r]);
                cv = (short)(cv / Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        /// <summary>
        /// Formats a signed byte using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>    
        public string Format(sbyte value)
        {
            var sb = new List<char>();
            var cv = value;
            if (cv < 1) cv *= -1;
            while (cv > 0)
            {
                var r = (int)(cv % Base);
                sb.Add(CharacterSet[r]);
                cv = (sbyte)(cv / Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        /// <summary>
        /// Formats a BigInteger using the encoding character set.
        /// </summary>
        /// <param name="value">the value to encode</param>
        /// <returns>The encoded value</returns>    
        public string Format(BigInteger value)
        {
            var sb = new List<char>();
            var cv = value;
            if (cv < 1) cv *= -1;
            while (cv > 0)
            {
                var br = (cv % (int)Base);
                var r = (int)br;
                sb.Add(CharacterSet[r]);
                cv = (cv / Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        /// <summary>
        /// Parses a string as an Int64
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OverflowException">If the text cannot be parse because the resultant value can't be stored in an Int64</exception>
        public Int64 ParseInt64(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();

            //TODO: Check for over/underflow
            var result = (Int64)0;
            var isNeg = (value[0] == '-');
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= Base;
                result += charToValue[digit];
            }
            return isNeg ? -1 * result : result;
        }

        /// <summary>
        /// Parses a string as an Int32
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OverflowException">If the text cannot be parse because the resultant value can't be stored in an Int32</exception>
        public Int32 ParseInt32(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (Int32)0;
            var isNeg = (value[0] == '-');
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (int)Base;
                result += charToValue[digit];
            }
            return isNeg ? -1 * result : result;
        }

        /// <summary>
        /// Parses a string as an Int16
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OverflowException">If the text cannot be parse because the resultant value can't be stored in an Int16</exception>
        public Int16 ParseInt16(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (Int16)0;
            var isNeg = (value[0] == '-');
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (Int16)Base;
                result += (Int16)charToValue[digit];
            }
            return (Int16)(isNeg ? -1 * result : result);
        }

        /// <summary>
        /// Parses a string as an SByte
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OverflowException">If the text cannot be parse because the resultant value can't be stored in an SByte</exception>
        public SByte ParseSByte(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (SByte)0;
            var isNeg = (value[0] == '-');
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (SByte)Base;
                result += (SByte)charToValue[digit];
            }
            return (SByte)(isNeg ? -1 * result : result);
        }

        /// <summary>
        /// Parses a string as a UInt64
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OverflowException">If the text cannot be parse because the resultant value can't be stored in a UInt64</exception>
        public UInt64 ParseUInt64(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (UInt64)0;
            if (value[0] == '-') throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (UInt64)Base;
                result += (UInt64)charToValue[digit];
            }
            return result;
        }

        /// <summary>
        /// Parses a string as a UInt32
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OverflowException">If the text cannot be parse because the resultant value can't be stored in a UInt32</exception>
        public UInt32 ParseUInt32(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (UInt32)0;
            if (value[0] == '-') throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (UInt32)Base;
                result += (UInt32)charToValue[digit];
            }
            return result;
        }

        /// <summary>
        /// Parses a string as a UInt16
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OverflowException">If the text cannot be parse because the resultant value can't be stored in a UInt16</exception>
        public UInt16 ParseUInt16(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (UInt16)0;
            if (value[0] == '-') throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (UInt16)Base;
                result += (UInt16)charToValue[digit];
            }
            return result;
        }

        /// <summary>
        /// Parses a string as a Byte
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OverflowException">If the text cannot be parse because the resultant value can't be stored in a Byte</exception>
        public Byte ParseByte(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (Byte)0;
            var isNeg = (value[0] == '-');
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (Byte)Base;
                result += (Byte)charToValue[digit];
            }
            return result;
        }

        /// <summary>
        /// Parses a string as a BigInteger
        /// </summary>
        /// <param name="value">The text to decode</param>
        /// <returns>the decoded value</returns>
        /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
        /// <exception cref="ArgumentException">If the text cannot be parsed (i.e. includes non-decodable characters)</exception>
        /// <exception cref="OutOfMemoryException">If the text cannot be parse because the resultant value cause the application to exahaust its memory.</exception>
        public BigInteger ParseBigInteger(string value)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (BigInteger)0;
            var isNeg = (value[0] == '-');
            var digits = ExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= Base;
                result += charToValue[digit];
            }
            return isNeg ? -1 * result : result;
        }

        /// <summary>
        /// Tries to parse an SByte from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseSByte(string value, ref sbyte result)
        {
            try { result=ParseSByte(value); return true; } catch { return false; }
        }

        /// <summary>
        /// Tries to parse an Int16 from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseInt16(string value, ref short result)
        {
            try { result = ParseInt16(value); return true; } catch { return false; }
        }

        /// <summary>
        /// Tries to parse an Int32 from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseInt32(string value, ref int result)
        {
            try { result = ParseInt32(value); return true; } catch { return false; }
        }

        /// <summary>
        /// Tries to parse an Int64 from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseInt64(string value, ref long result)
        {
            try { result = ParseInt64(value); return true; } catch { return false; }
        }

        /// <summary>
        /// Tries to parse a BigInteger from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseBigInteger(string value, ref BigInteger result)
        {
            try { result = ParseBigInteger(value); return true; } catch { return false; }
        }

        /// <summary>
        /// Tries to parse a Byte from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseByte(string value, ref byte result)
        {
            try { result = ParseByte(value); return true; } catch { return false; }
        }

        /// <summary>
        /// Tries to parse a UInt16 from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseUInt16(string value, ref ushort result)
        {
            try { result = ParseUInt16(value); return true; } catch { return false; }
        }

        /// <summary>
        /// Tries to parse a UInt32 from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseUInt32(string value, ref uint result)
        {
            try { result = ParseUInt32(value); return true; } catch { return false; }
        }

        /// <summary>
        /// Tries to parse a UInt64 from the provided text.
        /// </summary>
        /// <param name="value">the text to parse</param>
        /// <param name="result">the resultant value</param>
        /// <returns>true if successfully parsed, false otherwise</returns>
        public bool TryParseUInt64(string value, ref ulong result)
        {
            try { result = ParseUInt64(value); return true; } catch { return false; }
        }

        private string FormatObject(object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Byte:
                    return Format((Byte)value);
                case TypeCode.SByte:
                    return Format((SByte)value);
                case TypeCode.UInt16:
                    return Format((UInt16)value);
                case TypeCode.UInt32:
                    return Format((UInt32)value);
                case TypeCode.UInt64:
                    return Format((UInt64)value);
                case TypeCode.Int16:
                    return Format((Int16)value);
                case TypeCode.Int32:
                    return Format((Int32)value);
                case TypeCode.Int64:
                    return Format((Int64)value);
            }
            if (value.IsIntegerType()) return Format((BigInteger)value);
            return value.ToString();
        }

        private string ExtractCoreDigits(string value)
        {
            var isNeg = (value[0] == '-');
            int s = isNeg ? 1 : 0;
            var l = value.Length - s;
            value = value.Substring(s, l);
            return value;
        }

        private string FormatResult(List<char> sb)
        {
            if (sb.Count == 0) sb.Add(CharacterSet[0]);
            sb.Reverse();
            return string.Join("", sb);
        }

        private static string Format(ICustomFormatter formatter, string fmt, object value, IFormatProvider formatProvider)
        {
            if (formatter is IntegerEncoder intFormatter)
            {
                return intFormatter.FormatObject(value);
            }
            return formatter.Format(fmt, value, formatProvider);
        }

    }
}
