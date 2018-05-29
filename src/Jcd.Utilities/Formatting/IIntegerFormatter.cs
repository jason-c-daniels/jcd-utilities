using System.Numerics;

namespace Jcd.Utilities
{
    /// <summary>
    /// An interface for formatting integers as text.
    /// </summary>
    public interface IIntegerFormatter
    {
        string Format(byte value);
        string Format(int value);
        string Format(long value);
        string Format(sbyte value);
        string Format(short value);
        string Format(uint value);
        string Format(ulong value);
        string Format(ushort value);
        string Format(BigInteger value);
    }
}