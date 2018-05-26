namespace Jcd.Utilities
{
    public static class NumericEncoders
    {
        static NumericEncoders()
        {
            Base32_Crockford_DecodeCharacterSet = new[] { "0oO", "1iIlL", "2", "3", "4", "5", "6", "7", "8", "9", "aA", "bB", "cC", "dD", "eE", "fF", "gG", "hH", "jJ", "kK", "mM", "nN", "pP", "qQ", "rR", "sS", "tT", "vV", "wW", "xX", "yY", "zZ" };
            Base32_Crockford = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUV", Base32_Crockford_DecodeCharacterSet);
        }
        public readonly static NumericEncoder Binary = new NumericEncoder("01", "0b");
        public readonly static NumericEncoder Ternary = new NumericEncoder("012", "0t");
        public readonly static NumericEncoder Quaternary = new NumericEncoder("0123", "0q");
        public readonly static NumericEncoder Pentary = new NumericEncoder("01234", "0p");
        public readonly static NumericEncoder Senary = new NumericEncoder("012345", "0s");
        public readonly static NumericEncoder Septenary = new NumericEncoder("0123456", "0sp");
        public readonly static NumericEncoder Octal = new NumericEncoder("01234567", "0o");        
        public readonly static NumericEncoder Duodecimal = new NumericEncoder("0123456789↊↋");
        public readonly static NumericEncoder Hexdecimal = new NumericEncoder("0123456789ABCDEF", "0x");
        public readonly static NumericEncoder Heptadecimal = new NumericEncoder("0123456789ABCDEFG");

        public readonly static NumericEncoder Base32Hex = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUV");
        public readonly static NumericEncoder Base32_RFC4648 = new NumericEncoder("ABCDEFGHIJKLMNOPQRSTUVXYZ234567", caseSensitive: true);
        public readonly static NumericEncoder Base32_Zrtp = new NumericEncoder("ybndrfg8ejkmcpqxot1uwisza345h769", caseSensitive: true);
        public readonly static NumericEncoder Base32_Crockford;

        public readonly static NumericEncoder Base36 = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        
        public readonly static NumericEncoder Base58 = new NumericEncoder("123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz", caseSensitive: true);
        public readonly static NumericEncoder FlickrBase58 = new NumericEncoder("123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ", caseSensitive: true);

        public readonly static NumericEncoder Sexagesimal = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx", caseSensitive: true);
        public readonly static NumericEncoder Duosexagesimal = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", caseSensitive: true);
        public readonly static NumericEncoder Base63 = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+", caseSensitive: true);
        public readonly static NumericEncoder Base64 = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/", caseSensitive: true);

        public readonly static NumericEncoder Base64_Bcrypt = new NumericEncoder("./ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", caseSensitive: true);
        public readonly static NumericEncoder Base64_Xxencoding = new NumericEncoder("+-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", caseSensitive: true);
        public readonly static NumericEncoder Base64_Uuencoding = new NumericEncoder("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/", caseSensitive: true);
        public readonly static NumericEncoder Base64_UnixB64 = new NumericEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/", caseSensitive: true);
        public readonly static NumericEncoder Base64_Radix64 = new NumericEncoder(" !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_", caseSensitive: true);
        public readonly static NumericEncoder Base64_BinHex4 = new NumericEncoder("!\"#$%&'()*+,-012345689@ABCDEFGHIJKLMNPQRSTUVXYZ[`abcdefhijklmpqr", caseSensitive: true);
        public readonly static NumericEncoder Base64_Ascii85 = new NumericEncoder("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#", caseSensitive: true);

        #region special constants
        public readonly static string[] Base32_Crockford_DecodeCharacterSet;
        #endregion

    }
}