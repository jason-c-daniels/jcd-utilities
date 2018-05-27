using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Jcd.Utilities.Extensions
{
    public static class NumericExtensions
    {
        public static bool IsNumericType(this object self)
        {
            switch (Type.GetTypeCode(self.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return self.GetType() == typeof(BigInteger);
            }
        }

        public static bool IsFloatType(this object self)
        {
            switch (Type.GetTypeCode(self.GetType()))
            {
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsDecimalType(this object self)
        {
            return Type.GetTypeCode(self.GetType()) == TypeCode.Decimal;
        }

        public static bool IsIntegerType(this object self)
        {
            switch (Type.GetTypeCode(self.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return true;
                default:
                    {
                        return self.GetType()==typeof(BigInteger);
                    }
            }
        }

        public static bool IsSignedType(this object self)
        {
            switch (Type.GetTypeCode(self.GetType()))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Single:
                case TypeCode.Double:
                    return true;
                default:
                    return self.GetType() == typeof(BigInteger);
            }
        }

        public static bool IsUnsignedType(this object self)
        {
            switch (Type.GetTypeCode(self.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }
    }
}
