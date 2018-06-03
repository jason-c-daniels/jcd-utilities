﻿using System.Numerics;

namespace Jcd.Utilities
{
   /// <summary>
   /// An interface specification for parsing integers.
   /// </summary>
   public interface IIntegerParser
   {
      #region Public Methods

      BigInteger ParseBigInteger(string value);

      byte ParseByte(string value);

      short ParseInt16(string value);

      int ParseInt32(string value);

      long ParseInt64(string value);

      sbyte ParseSByte(string value);

      ushort ParseUInt16(string value);

      uint ParseUInt32(string value);

      ulong ParseUInt64(string value);

      bool TryParseBigInteger(string value, ref BigInteger result);

      bool TryParseByte(string value, ref byte result);

      bool TryParseInt16(string value, ref short result);

      bool TryParseInt32(string value, ref int result);

      bool TryParseInt64(string value, ref long result);

      bool TryParseSByte(string value, ref sbyte result);

      bool TryParseUInt16(string value, ref ushort result);

      bool TryParseUInt32(string value, ref uint result);

      bool TryParseUInt64(string value, ref ulong result);

      #endregion Public Methods
   }
}