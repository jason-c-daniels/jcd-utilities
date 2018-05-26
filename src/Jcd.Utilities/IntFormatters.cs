namespace Jcd.Utilities
{
    public static class IntFormatters
    {
        static IntFormatters()
        {
            Base32_Crockford_DecodeCharacterSet = new[] { "0oO", "1iIlL", "2", "3", "4", "5", "6", "7", "8", "9", "aA", "bB", "cC", "dD", "eE", "fF", "gG", "hH", "jJ", "kK", "mM", "nN", "pP", "qQ", "rR", "sS", "tT", "vV", "wW", "xX", "yY", "zZ" };
            Base32_Crockford = new IntFormatter("0123456789ABCDEFGHIJKLMNOPQRSTUV", Base32_Crockford_DecodeCharacterSet);
        }
        public readonly static IntFormatter Binary = new IntFormatter("01", "0b");
        public readonly static IntFormatter Ternary = new IntFormatter("012", "0t");
        public readonly static IntFormatter Quaternary = new IntFormatter("0123", "0q");
        public readonly static IntFormatter Pentary = new IntFormatter("01234", "0p");
        public readonly static IntFormatter Senary = new IntFormatter("012345", "0s");
        public readonly static IntFormatter Septenary = new IntFormatter("0123456", "0sp");
        public readonly static IntFormatter Octal = new IntFormatter("01234567", "0o");        
        public readonly static IntFormatter Duodecimal = new IntFormatter("0123456789↊↋");
        public readonly static IntFormatter Hexdecimal = new IntFormatter("0123456789ABCDEF", "0x");
        public readonly static IntFormatter Heptadecimal = new IntFormatter("0123456789ABCDEFG");

        public readonly static IntFormatter Vegesimal = new IntFormatter("0123456789ABCDEFGHIJ");
        

        public readonly static IntFormatter Base32Hex = new IntFormatter("0123456789ABCDEFGHIJKLMNOPQRSTUV");
        public readonly static IntFormatter Base32_RFC4648 = new IntFormatter("ABCDEFGHIJKLMNOPQRSTUVXYZ234567", caseSensitive: true);
        public readonly static IntFormatter Base32_Zrtp = new IntFormatter("ybndrfg8ejkmcpqxot1uwisza345h769", caseSensitive: true);
        public readonly static IntFormatter Base32_Crockford;

        public readonly static IntFormatter Base36 = new IntFormatter("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        
        public readonly static IntFormatter Base58 = new IntFormatter("123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz", caseSensitive: true);
        public readonly static IntFormatter FlickrBase58 = new IntFormatter("123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ", caseSensitive: true);

        public readonly static IntFormatter Sexagesimal = new IntFormatter("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx", caseSensitive: true);
        public readonly static IntFormatter Duosexagesimal = new IntFormatter("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", caseSensitive: true);
        public readonly static IntFormatter Base63 = new IntFormatter("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+", caseSensitive: true);

        public readonly static IntFormatter Base64 = new IntFormatter("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/", caseSensitive: true);
        public readonly static IntFormatter Base64_Bcrypt = new IntFormatter("./ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", caseSensitive: true);
        public readonly static IntFormatter Base64_Xxencoding = new IntFormatter("+-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", caseSensitive: true);
        public readonly static IntFormatter Base64_Uuencoding = new IntFormatter("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/", caseSensitive: true);
        public readonly static IntFormatter Base64_UnixB64 = new IntFormatter("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/", caseSensitive: true);
        public readonly static IntFormatter Base64_Radix64 = new IntFormatter(" !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_", caseSensitive: true);
        public readonly static IntFormatter Base64_BinHex4 = new IntFormatter("!\"#$%&'()*+,-012345689@ABCDEFGHIJKLMNPQRSTUVXYZ[`abcdefhijklmpqr", caseSensitive: true);
        public readonly static IntFormatter Base64_Ascii85 = new IntFormatter("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#", caseSensitive: true);

        #region special constants
        public readonly static string[] Base32_Crockford_DecodeCharacterSet;
        #endregion

    }
}