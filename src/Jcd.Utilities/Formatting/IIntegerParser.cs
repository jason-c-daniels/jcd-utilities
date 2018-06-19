using System.Numerics;

namespace Jcd.Utilities
{
   /// <summary>
   ///     An interface specification for parsing integers.
   /// </summary>
   public interface IIntegerParser
   {
      #region Public Methods

      /// <summary>
      ///     Parses a string as a BigInteger
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      /// <exception cref="OutOfMemoryException">
      ///     If the text cannot be parse because the resultant value cause the application to exahaust
      ///     its memory.
      /// </exception>
      BigInteger ParseBigInteger(string value);

      /// <summary>
      ///     Parses a string as a Byte
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      /// <exception cref="OverflowException">
      ///     If the text cannot be parse because the resultant value can't be stored in a Byte
      /// </exception>
      byte ParseByte(string value);

      /// <summary>
      ///     Parses a string as an Int16
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="OverflowException">
      ///     If the text cannot be parse because the resultant value can't be stored in an Int16
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      short ParseInt16(string value);

      /// <summary>
      ///     Parses a string as an Int32
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="OverflowException">
      ///     If the text cannot be parse because the resultant value can't be stored in an Int32
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      int ParseInt32(string value);

      /// <summary>
      ///     Parses a string as an Int64
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="OverflowException">
      ///     If the text cannot be parse because the resultant value can't be stored in an Int64
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      long ParseInt64(string value);

      /// <summary>
      ///     Parses a string as an SByte
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="OverflowException">
      ///     If the text cannot be parse because the resultant value can't be stored in an SByte
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      sbyte ParseSByte(string value);

      /// <summary>
      ///     Parses a string as an UInt16
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="OverflowException">
      ///     If the text cannot be parse because the resultant value can't be stored in an UInt16
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      ushort ParseUInt16(string value);

      /// <summary>
      ///     Parses a string as an UInt32
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="OverflowException">
      ///     If the text cannot be parse because the resultant value can't be stored in an UInt32
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      uint ParseUInt32(string value);

      /// <summary>
      ///     Parses a string as an UInt64
      /// </summary>
      /// <param name="value">The text to decode</param>
      /// <returns>the decoded value</returns>
      /// <exception cref="ArgumentNullException">If the value parameter was null</exception>
      /// <exception cref="ArgumentException">
      ///     If the text is empty or whitespace.
      /// </exception>
      /// <exception cref="OverflowException">
      ///     If the text cannot be parse because the resultant value can't be stored in an UInt64
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      ///     If the provided characters cannot be decoded per the current encoding..
      /// </exception>
      ulong ParseUInt64(string value);

      /// <summary>
      ///     Tries to parse a BigInteger from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseBigInteger(string value, ref BigInteger result);

      /// <summary>
      ///     Tries to parse a Byte from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseByte(string value, ref byte result);

      /// <summary>
      ///     Tries to parse a Int16 from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseInt16(string value, ref short result);

      /// <summary>
      ///     Tries to parse a Int32 from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseInt32(string value, ref int result);

      /// <summary>
      ///     Tries to parse a Int64 from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseInt64(string value, ref long result);

      /// <summary>
      ///     Tries to parse a SByte from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseSByte(string value, ref sbyte result);

      /// <summary>
      ///     Tries to parse a UInt16 from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseUInt16(string value, ref ushort result);

      /// <summary>
      ///     Tries to parse a UInt32 from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseUInt32(string value, ref uint result);

      /// <summary>
      ///     Tries to parse a UInt64 from the provided text.
      /// </summary>
      /// <param name="value">the text to parse</param>
      /// <param name="result">the resultant value</param>
      /// <returns>true if successfully parsed, false otherwise</returns>
      bool TryParseUInt64(string value, ref ulong result);

      #endregion Public Methods
   }
}