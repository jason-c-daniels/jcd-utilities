using Jcd.Utilities.Extensions;
using Jcd.Utilities.Formatting;
using Jcd.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Jcd.Utilities
{
    public class IntegerEncoder : CustomFormatterBase, IIntegerFormatter, IIntegerParser
    {
        private readonly Dictionary<char, int> charToValue = new Dictionary<char, int>();
        public readonly string CharacterSet;
        public readonly bool CaseSensitive;
        public readonly bool CharacterSetValuesAlwaysIncrease;
        public readonly int Base;
        static Type[] formattableTypes = { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(int), typeof(uint), typeof(long), typeof(ulong) };
        public IntegerEncoder(string encodeCharacterSet, string[] decodeCharacterSet)
            : base(formattableTypes, Format)
        {
            if (decodeCharacterSet.Length != encodeCharacterSet.Length) throw new ArgumentException("decodeCharacterSet and encodeCharacterSet must be the same length.");
            this.CaseSensitive = true;
            this.CharacterSet = encodeCharacterSet;
            Base = CharacterSet.Length;
            for (int i = 0; i < decodeCharacterSet.Length; i++)
            {
                foreach (char c in decodeCharacterSet[i])
                {
                    charToValue.Add(c, i);
                }
            }
        }
        public IntegerEncoder(string characterSet, bool caseSensitive = false)
            : base(formattableTypes, Format)
        {
            this.CaseSensitive = caseSensitive;
            this.CharacterSet = caseSensitive ? characterSet : characterSet.ToLowerInvariant();
            Base = CharacterSet.Length;
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
        
        private static string Format(ICustomFormatter formatter, string fmt, object value, IFormatProvider formatProvider)
        {
            if (formatter is IntegerEncoder intFormatter)
            {
                return intFormatter.FormatObject(value);
            }
            return formatter.Format(fmt, value, formatProvider);
        }

        public string Format(uint value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (cv / (uint)Base);
            }
            return FormatResult(sb);
        }

        public string Format(ulong value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (cv / (ulong)Base);
            }
            return FormatResult(sb);
        }

        public string Format(ushort value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (ushort)(cv / Base);
            }
            return FormatResult(sb);
        }

        public string Format(byte value)
        {
            var sb = new List<char>();
            var cv = Math.Abs(value);
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (byte)(cv / Base);
            }
            return FormatResult(sb);
        }

        public string Format(int value)
        {
            var sb = new List<char>();
            var cv = Math.Abs(value);
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (cv / (int)Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        public string Format(long value)
        {
            var sb = new List<char>();
            var cv = Math.Abs(value);
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (cv / (long)Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        public string Format(short value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (short)(cv / Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        public string Format(sbyte value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (sbyte)(cv / Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        public string Format(BigInteger value)
        {
            var sb = new List<char>();
            var cv = value;
            while (cv > 0)
            {
                var r = (int)cv % Base;
                sb.Add(CharacterSet[r]);
                cv = (cv / Base);
            }
            if (value < 1) sb.Add('-');
            return FormatResult(sb);
        }

        public Int64 ParseInt64(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();

            //TODO: Check for over/underflow
            var result = (Int64)0;
            var isNeg = (value[0] == '-');
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= Base;
                result += charToValue[digit];
            }
            return isNeg ? -1 * result : result;
        }

        public Int32 ParseInt32(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (Int32)0;
            var isNeg = (value[0] == '-');
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= Base;
                result += charToValue[digit];
            }
            return isNeg ? -1 * result : result;
        }

        public Int16 ParseInt16(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (Int16)0;
            var isNeg = (value[0] == '-');
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (Int16)Base;
                result += (Int16)charToValue[digit];
            }
            return (Int16)(isNeg ? -1 * result : result);
        }

        public SByte ParseSByte(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (SByte)0;
            var isNeg = (value[0] == '-');
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (SByte)Base;
                result += (SByte)charToValue[digit];
            }
            return (SByte)(isNeg ? -1 * result : result);
        }

        public UInt64 ParseUInt64(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (UInt64)0;
            if (value[0] == '-') throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (UInt64)Base;
                result += (UInt64)charToValue[digit];
            }
            return result;
        }

        public UInt32 ParseUInt32(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (UInt32)0;
            if (value[0] == '-') throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (UInt32)Base;
                result += (UInt32)charToValue[digit];
            }
            return result;
        }

        public UInt16 ParseUInt16(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (UInt16)0;
            if (value[0] == '-') throw new ArgumentException("A negative number cannot be converted into unsigned.", nameof(value));
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (UInt16)Base;
                result += (UInt16)charToValue[digit];
            }
            return result;
        }

        public Byte ParseByte(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (Byte)0;
            var isNeg = (value[0] == '-');
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= (Byte)Base;
                result += (Byte)charToValue[digit];
            }
            return result;
        }

        public BigInteger ParseBigInteger(string value)
        {
            if (!CaseSensitive) value = value.ToLowerInvariant();
            //TODO: Check for over/underflow
            var result = (BigInteger)0;
            var isNeg = (value[0] == '-');
            var digits = ValidateAndExtractCoreDigits(value);
            foreach (var digit in digits)
            {
                result *= Base;
                result += charToValue[digit];
            }
            return isNeg ? -1 * result : result;
        }

        public bool TryParseSByte(string value, ref sbyte result)
        {
            try { result=ParseSByte(value); return true; } catch { return false; }
        }

        public bool TryParseInt16(string value, ref short result)
        {
            try { result = ParseInt16(value); return true; } catch { return false; }
        }

        public bool TryParseInt32(string value, ref int result)
        {
            try { result = ParseInt32(value); return true; } catch { return false; }
        }

        public bool TryParseInt64(string value, ref long result)
        {
            try { result = ParseInt64(value); return true; } catch { return false; }
        }

        public bool TryParseBigInteger(string value, ref BigInteger result)
        {
            try { result = ParseBigInteger(value); return true; } catch { return false; }
        }

        public bool TryParseByte(string value, ref byte result)
        {
            try { result = ParseByte(value); return true; } catch { return false; }
        }

        public bool TryParseUInt16(string value, ref ushort result)
        {
            try { result = ParseUInt16(value); return true; } catch { return false; }
        }

        public bool TryParseUInt32(string value, ref uint result)
        {
            try { result = ParseUInt32(value); return true; } catch { return false; }
        }

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

        private string ValidateAndExtractCoreDigits(string value)
        {
            var isNeg = (value[0] == '-');
            int s = isNeg ? 1 : 0;
            var l = value.Length;
            value = value.Substring(s, l);
            return value;
        }

        private string FormatResult(List<char> sb)
        {
            if (sb.Count == 0) sb.Add(CharacterSet[0]);
            sb.Reverse();
            return string.Join("", sb);
        }

    }
}
