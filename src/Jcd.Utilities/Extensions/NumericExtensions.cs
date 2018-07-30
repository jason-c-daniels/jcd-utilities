using System;
using System.Numerics;

namespace Jcd.Utilities.Extensions
{
   /// <summary>
   ///     An extension method class providing numeric type information detection.
   /// </summary>
   public static class NumericExtensions
   {
      #region Public Methods

      /// <summary>
      ///     Indicates if an object is of an integer data type.
      /// </summary>
      /// <param name="self">The object to check</param>
      /// <returns>true if the object is of an integer data type</returns>
      public static bool IsBigIntegerType(this object self)
      {
         return self is BigInteger;
      }

      /// <summary>
      ///     Indicates if an object is of a decimal data type.
      /// </summary>
      /// <param name="self">The object to check</param>
      /// <returns>true if the object is of a decimal data type</returns>
      public static bool IsDecimalType(this object self)
      {
         return Type.GetTypeCode(self.GetType()) == TypeCode.Decimal;
      }

      /// <summary>
      ///     Indicates if an object is of a floating point data type.
      /// </summary>
      /// <param name="self">The object to check</param>
      /// <returns>true if the object is of a floating point data type</returns>
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

      /// <summary>
      ///     Indicates if an object is of an integer data type.
      /// </summary>
      /// <param name="self">The object to check</param>
      /// <returns>true if the object is of an integer data type</returns>
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
            return self is BigInteger;
         }
         }
      }

      /// <summary>
      ///     Indicates if an object is of a numeric data type.
      /// </summary>
      /// <param name="self">The object to check</param>
      /// <returns>true if the object is of a numeric data type</returns>
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
            return self is BigInteger;
         }
      }

      /// <summary>
      ///     Indicates if an object is of a signed data type.
      /// </summary>
      /// <param name="self">The object to check</param>
      /// <returns>true if the object is of a signed data type</returns>
      public static bool IsSignedType(this object self)
      {
         var type = self.GetType();
         var tc = Type.GetTypeCode(type);
         switch (tc)
         {
            case TypeCode.SByte:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Single:
            case TypeCode.Double:
               return true;

            case TypeCode.Boolean:
            case TypeCode.Byte:
            case TypeCode.Char:
            case TypeCode.DateTime:
            case TypeCode.DBNull:
            case TypeCode.Empty:
            case TypeCode.String:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
               return false;
            
            default:
               return self is BigInteger;
         }
      }

      /// <summary>
      ///     Indicates if an object is of an unsigned data type.
      /// </summary>
      /// <param name="self">The object to check</param>
      /// <returns>true if the object is of an unsigned data type</returns>
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

      #endregion Public Methods
   }
}