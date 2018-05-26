using System.Numerics;

namespace Jcd.Utilities
{
    public interface IIntegerParser
    {
        sbyte ParseSByte(string value);
        short ParseInt16(string value);
        int ParseInt32(string value);
        long ParseInt64(string value);
        BigInteger ParseBigInteger(string value);
        byte ParseByte(string value);
        ushort ParseUInt16(string value);
        uint ParseUInt32(string value);
        ulong ParseUInt64(string value);

        bool TryParseSByte(string value, ref sbyte result);
        bool TryParseInt16(string value, ref short result);
        bool TryParseInt32(string value, ref int result);
        bool TryParseInt64(string value, ref long result);
        bool TryParseBigInteger(string value, ref BigInteger result);
        bool TryParseByte(string value, ref byte result);
        bool TryParseUInt16(string value, ref ushort result);
        bool TryParseUInt32(string value, ref uint result);
        bool TryParseUInt64(string value, ref ulong result);

    }
}