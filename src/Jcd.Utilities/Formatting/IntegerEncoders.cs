namespace Jcd.Utilities
{
    public static class IntegerEncoders
    {
        static IntegerEncoders()
        {
            Base32_Crockford_DecodeCharacterSet = new[] { "0oO", "1iIlL", "2", "3", "4", "5", "6", "7", "8", "9", "aA", "bB", "cC", "dD", "eE", "fF", "gG", "hH", "jJ", "kK", "mM", "nN", "pP", "qQ", "rR", "sS", "tT", "vV", "wW", "xX", "yY", "zZ" };
            Base32_Crockford = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUV", Base32_Crockford_DecodeCharacterSet);
        }
        /// <summary>
        /// A base 2 formatter using 01
        /// </summary>
        public readonly static IntegerEncoder Binary = new IntegerEncoder("01", caseSensitive: true);
        /// <summary>
        /// A base 3 formatter  012
        /// </summary>
        public readonly static IntegerEncoder Ternary = new IntegerEncoder("012", caseSensitive: true);
        /// <summary>
        /// A quaternary formatter using 0123
        /// </summary>
        public readonly static IntegerEncoder Quaternary = new IntegerEncoder("0123", caseSensitive: true);
        /// <summary>
        /// A quinary (base 5) formatter using 0-4
        /// </summary>
        public readonly static IntegerEncoder Quinary = new IntegerEncoder("01234", caseSensitive: true);
        /// <summary>
        /// A base 6 formatter using 0-5
        /// </summary>
        public readonly static IntegerEncoder Senary = new IntegerEncoder("012345", caseSensitive: true);
        /// <summary>
        /// A base 7 formatter using 0-6
        /// </summary>
        public readonly static IntegerEncoder Septenary = new IntegerEncoder("0123456", caseSensitive: true);
        /// <summary>
        /// A base 8 formatter using 0-7
        /// </summary>
        public readonly static IntegerEncoder Octal = new IntegerEncoder("01234567", caseSensitive: true);
        /// <summary>
        /// A base 9 formatter using 0-8
        /// </summary>
        public readonly static IntegerEncoder Nonary = new IntegerEncoder("012345678", caseSensitive: true);
        /// <summary>
        /// A base 11 formatter using 0-9A, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Undecimal = new IntegerEncoder("0123456789A");
        /// <summary>
        /// A base 12 formatter using 0-9AB, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Duodecimal = new IntegerEncoder("0123456789AB");
        /// <summary>
        /// A base 13 formatter using 0-9A-C, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Tridecimal = new IntegerEncoder("0123456789ABC");
        /// <summary>
        /// A base 14 formatter using 0-9A-D, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Tetradecimal = new IntegerEncoder("0123456789ABCD");
        /// <summary>
        /// A base 15 formatter using 0-9A-E, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Pentadecimal = new IntegerEncoder("0123456789ABCDE");
        /// <summary>
        /// A base 16 formatter using 0-9A-F, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Hexdecimal = new IntegerEncoder("0123456789ABCDEF");
        /// <summary>
        /// A base 17 formatter using 0-9A-G, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Heptadecimal = new IntegerEncoder("0123456789ABCDEFG");

        /// <summary>
        /// A base 20 formatter using 0-9A-J, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Vegesimal = new IntegerEncoder("0123456789ABCDEFGHIJ");

        /// <summary>
        /// A base 32 formatter using 0-9A-V, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Base32Hex = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUV");
        /// <summary>
        /// A base 32 formatter for RFC4648 numnbers, case sensitive
        /// </summary>
        public readonly static IntegerEncoder Base32_RFC4648 = new IntegerEncoder("ABCDEFGHIJKLMNOPQRSTUVXYZ234567", caseSensitive: true);

        /// <summary>
        /// A base 32 formatter for ZRTP encoded numbers, case sensitive
        /// </summary>
        public readonly static IntegerEncoder Base32_Zrtp = new IntegerEncoder("ybndrfg8ejkmcpqxot1uwisza345h769", caseSensitive: true);

        /// <summary>
        /// A base 32 formatter for Crockford numnbers, special parsing rules.
        /// </summary>
        public readonly static IntegerEncoder Base32_Crockford;

        /// <summary>
        /// A base 36 formatter using 0-9A-Z, case insensitive
        /// </summary>
        public readonly static IntegerEncoder Base36 = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        /// <summary>
        /// A base 58 formatter IPFS compliant, case sensitive, alphabet: 123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz
        /// </summary>
        public readonly static IntegerEncoder Base58_IPFS = new IntegerEncoder("123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz", caseSensitive: true);

        /// <summary>
        /// A base 58 formatter Flickr short URL compliant, case sensitive, alphabet: 123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ
        /// </summary>
        public readonly static IntegerEncoder FlickrBase58 = new IntegerEncoder("123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ", caseSensitive: true);

        /// <summary>
        /// Base 60 formatter using this alphabet: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx
        /// </summary>
        public readonly static IntegerEncoder Sexagesimal = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx", caseSensitive: true);

        /// <summary>
        /// Base 62 formatter using: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz
        /// </summary>
        public readonly static IntegerEncoder Base62 = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", caseSensitive: true);

        /// <summary>
        /// Base 63 formatter using: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+
        /// </summary>
        public readonly static IntegerEncoder Base63 = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+", caseSensitive: true);

        /// <summary>
        /// Base 64 formatter using the standard: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/
        /// </summary>
        public readonly static IntegerEncoder Base64 = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/", caseSensitive: true);

        /// <summary>
        /// Base 64 formatter: Bcrypt compliant
        /// </summary>
        public readonly static IntegerEncoder Base64_Bcrypt = new IntegerEncoder("./ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", caseSensitive: true);
        
        /// <summary>
        /// Base 64 formatter: Xxencoding compliant
        /// </summary>
        public readonly static IntegerEncoder Base64_Xxencoding = new IntegerEncoder("+-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", caseSensitive: true);

        /// <summary>
        /// Base 64 formatter: Uuencoding compliant
        /// </summary>
        public readonly static IntegerEncoder Base64_Uuencoding = new IntegerEncoder("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/", caseSensitive: true);

        /// <summary>
        /// Base 64 formatter: Unix B64 compliant
        /// </summary>
        public readonly static IntegerEncoder Base64_UnixB64 = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/", caseSensitive: true);

        /// <summary>
        /// Base 64 formatter: Radix64 compliant
        /// </summary>
        public readonly static IntegerEncoder Base64_Radix64 = new IntegerEncoder(" !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_", caseSensitive: true);

        /// <summary>
        /// Base 64 formatter: BinHex4 compliant
        /// </summary>
        public readonly static IntegerEncoder Base64_BinHex4 = new IntegerEncoder("!\"#$%&'()*+,-012345689@ABCDEFGHIJKLMNPQRSTUVXYZ[`abcdefhijklmpqr", caseSensitive: true);

        /// <summary>
        /// ASCII 85 formatter, possibly under patent protection, need to check.
        /// </summary>
        public readonly static IntegerEncoder Ascii85 = new IntegerEncoder("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#", caseSensitive: true);

        /// <summary>
        /// Base 91 formatter, basE91 compliant (see http://base91.sourceforge.net/)
        /// </summary>
        public readonly static IntegerEncoder Base91 = new IntegerEncoder("ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz!#$%&()*+,./:;<=>?@[]^_`{|}~\"", caseSensitive: true);

        /// <summary>
        /// Base 93 formatting, first 93 characters from this alphabet: ./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ
        /// </summary>
        public readonly static IntegerEncoder Base93_Monotonic_ISO8859_15 = new IntegerEncoder(ISO8859_15_EncodingCharacters.Substring(0, 93), caseSensitive: true);

        /// <summary>
        /// Base 93 formatting, 93 characters from this alphabet starting at 0: ./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ
        /// </summary>
        public readonly static IntegerEncoder Base93_0_Monotonic_ISO8859_15 = new IntegerEncoder(ISO8859_15_EncodingCharacters.Substring(ISO8859_15_EncodingCharacters.IndexOf('0'), 93), caseSensitive: true);

        /// <summary>
        /// Base 93 formatting, 93 characters from this alphabet starting at A: ./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ
        /// </summary>
        public readonly static IntegerEncoder Base93_A_Monotonic_ISO8859_15 = new IntegerEncoder(ISO8859_15_EncodingCharacters.Substring(ISO8859_15_EncodingCharacters.IndexOf('A'), 93), caseSensitive: true);

        /// <summary>
        /// Base 93 formatting, 93 characters from this alphabet starting at a: ./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ
        /// </summary>
        public readonly static IntegerEncoder Base93_a_Monotonic_ISO8859_15 = new IntegerEncoder(ISO8859_15_EncodingCharacters.Substring(ISO8859_15_EncodingCharacters.IndexOf('a'), 93), caseSensitive: true);

        /// <summary>
        /// Base 128 formatting, first 128 characters from this alphabet: ./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ
        /// </summary>
        public readonly static IntegerEncoder Base128_Monotonic_ISO8859_15 = new IntegerEncoder(ISO8859_15_EncodingCharacters.Substring(0,128), caseSensitive: true);

        /// <summary>
        /// base 128 formatting, 128 characters from this alphabet starting at 0: ./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ
        /// </summary>
        public readonly static IntegerEncoder Base128_0_Monotonic_ISO8859_15 = new IntegerEncoder(ISO8859_15_EncodingCharacters.Substring(ISO8859_15_EncodingCharacters.IndexOf('0'), 128), caseSensitive: true);

        /// <summary>
        /// base 128 formatting, 128 characters from this alphabet starting at A: ./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ
        /// </summary>
        public readonly static IntegerEncoder Base128_A_Monotonic_ISO8859_15 = new IntegerEncoder(ISO8859_15_EncodingCharacters.Substring(ISO8859_15_EncodingCharacters.IndexOf('A'), 128), caseSensitive: true);

        #region special constants

        ///<summary>A base alphabet for various numeric encoders with whitespace and certain special characters removed.</summary>
        /// <remarks>
        /// Readable/non-whitespace subset of ISO8859. The following characters have been removed. This gives an alphabet of 166 characters in numerical order.
        ///    Characters: `¯°²³¹º·
        ///    0x2D (-) and numerically lower characters.
        ///    0x7F (DEL)
        ///    0xA0 (NBSP)
        ///    0xAD (SHY)
        /// </remarks>
        public const string ISO8859_15_EncodingCharacters = "./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ";

        /// <summary>
        /// The decoding mappings for Crockford base 32 decoding.
        /// </summary>
        public readonly static string[] Base32_Crockford_DecodeCharacterSet;
        #endregion

    }
}