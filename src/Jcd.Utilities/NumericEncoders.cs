namespace Jcd.Utilities
{
    public static class NumericEncoders
    {
        public readonly static NumericEncoder Binary = new NumericEncoder("01", "0b");
        public readonly static NumericEncoder Ternary = new NumericEncoder("012", "b3:");
        public readonly static NumericEncoder Quaternary = new NumericEncoder("0123", "b4:");
        public readonly static NumericEncoder Octal = new NumericEncoder("01234567", "0o");
        public readonly static NumericEncoder Hexdecimal = new NumericEncoder("0123456789ABCDEF", "0x");
        public readonly static NumericEncoder Heptadecimal = new NumericEncoder("0123456789ABCDEFG", "b17:");
        public readonly static NumericEncoder Base32 = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUV", "b32:");
        public readonly static NumericEncoder Base36 = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", "b36:");
        public readonly static NumericEncoder Base63 = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", "b63:", caseSensitive: true);
        public readonly static NumericEncoder Base64 = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!", "b64:", caseSensitive: true);
    }
}