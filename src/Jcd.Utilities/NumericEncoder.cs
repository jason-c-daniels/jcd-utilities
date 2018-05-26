using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcd.Utilities
{
    public class NumericEncoder
    {
        private readonly Dictionary<char, int> charToValue = new Dictionary<char, int>();
        public readonly string CharacterSet;
        public readonly string Prefix;
        public readonly string Suffix;
        public readonly bool CaseSensitive;
        public readonly int Base;
        public NumericEncoder(string encodeCharacterSet, string[] decodeCharacterSet, string prefix = "", string suffix = "")
        {
            if (decodeCharacterSet.Length != encodeCharacterSet.Length) throw new ArgumentException("decodeCharacterSet and encodeCharacterSet must be the same length.");
            this.CaseSensitive = true;
            this.Prefix = prefix;
            this.Suffix = suffix;
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
        public NumericEncoder(string characterSet, string prefix = "", string suffix = "", bool caseSensitive = false)
        {
            this.CaseSensitive = caseSensitive;
            this.Prefix = prefix;
            this.Suffix = suffix;
            this.CharacterSet = caseSensitive ? characterSet : characterSet.ToLowerInvariant();
            Base = CharacterSet.Length;
            int i = 0;
            foreach (char c in CharacterSet)
            {
                charToValue.Add(c, i);
                i++;
            }
        }

        public string ToString(uint value)
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

        public string ToString(ulong value)
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

        public string ToString(ushort value)
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

        public string ToString(byte value)
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

        public string ToString(int value)
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

        public string ToString(long value)
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

        public string ToString(short value)
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

        public string ToString(sbyte value)
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
        public Int64 ToInt64(string value)
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
        public Int32 ToInt32(string value)
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

        public Int16 ToInt16(string value)
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

        public SByte ToSByte(string value)
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

        public UInt64 ToUInt64(string value)
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
        public UInt32 ToUInt32(string value)
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

        public UInt16 ToUInt16(string value)
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
        public Byte ToByte(string value)
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

        private string ValidateAndExtractCoreDigits(string value)
        {
            if (!value.StartsWith(Prefix) || !value.EndsWith(Suffix)) throw new FormatException("Incorrect value format");
            var isNeg = (value[0] == '-');
            int s = isNeg ? 1 : 0;
            var l = value.Length;
            s += Prefix.Length;
            l -= Suffix.Length + s;
            value = value.Substring(s, l);
            return value;
        }

        private string FormatResult(List<char> sb)
        {
            if (sb.Count == 0) sb.Add(CharacterSet[0]);
            sb.Reverse();
            return (string.IsNullOrWhiteSpace(Prefix) ? "" : Prefix) + string.Join("", sb) + (string.IsNullOrWhiteSpace(Suffix) ? "" : Suffix);
        }
    }
}
