using System.Numerics;

namespace Jcd.Utilities.Formatting
{
   /// <summary>
   ///     An interface for formatting integers as text.
   /// </summary>
   public interface IIntegerFormatter
   {
      #region Public Methods

      /// <summary>
      /// Formats a byte as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(byte value);

      /// <summary>
      /// Formats an int as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(int value);

      /// <summary>
      /// Formats a long as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(long value);

      /// <summary>
      /// Formats an sbyte as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(sbyte value);

      /// <summary>
      /// Formats a short as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(short value);

      /// <summary>
      /// Formats a uint as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(uint value);

      /// <summary>
      /// Formats a ulong as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(ulong value);

      /// <summary>
      /// Formats a ushort as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(ushort value);

      /// <summary>
      /// Formats a BigInteger as a string.
      /// </summary>
      /// <param name="value">the value to format</param>
      /// <returns>the formatted value</returns>
      string Format(BigInteger value);

      #endregion Public Methods
   }
}