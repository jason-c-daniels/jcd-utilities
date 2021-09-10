using System.Diagnostics.CodeAnalysis;

namespace Jcd.Utilities.Formatting
{
   /// <summary>
   ///     A class with a set of predefined integer encoders.
   /// </summary>
   [SuppressMessage("ReSharper", "UnusedMember.Global")]
   public static class IntegerEncoders
   {
      #region Public Constructors

      static IntegerEncoders()
      {
         Base32CrockfordDecodeCharacterSet = new[]
                                             {
                                                "0oO", "1iIlL", "2", "3", "4", "5", "6", "7", "8", "9", "aA", "bB",
                                                "cC", "dD", "eE", "fF", "gG", "hH",
                                                "jJ", "kK", "mM", "nN", "pP", "qQ", "rR", "sS", "tT", "vV", "wW", "xX",
                                                "yY", "zZ"
                                             };

         Base32Crockford =
            new IntegerEncoder("0123456789ABCDEFGHJKMNPQRSTVWXYZ", Base32CrockfordDecodeCharacterSet);
      }

      #endregion Public Constructors

      #region Public Fields

      /// <summary>
      ///     ASCII 85 formatter, possibly under patent protection, need to check.
      /// </summary>
      public static readonly IntegerEncoder Ascii85 = new
         IntegerEncoder("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#",
                        true);

      /// <summary>
      ///     Base 128 formatting, first 128 characters from the ISO8859_15_EncodingCharacters alphabet, starting at "0"
      /// </summary>
      public static readonly IntegerEncoder Base1280MonotonicIso885915 = new IntegerEncoder(
         Iso885915EncodingCharacters.Substring(Iso885915EncodingCharacters.IndexOf('0'), 128), true);

      /// <summary>
      ///     Base 128 formatting, first 128 characters from the ISO8859_15_EncodingCharacters alphabet, starting at "A"
      /// </summary>
      public static readonly IntegerEncoder Base128AMonotonicIso885915 = new IntegerEncoder(
         Iso885915EncodingCharacters.Substring(Iso885915EncodingCharacters.IndexOf('A'), 128), true);

      /// <summary>
      ///     Base 128 formatting, first 128 characters from the ISO8859_15_EncodingCharacters alphabet.
      /// </summary>
      public static readonly IntegerEncoder Base128MonotonicIso885915 = new IntegerEncoder(
         Iso885915EncodingCharacters.Substring(
            0, 128), true);

      /// <summary>
      ///     A base 32 formatter for Crockford numnbers, special parsing rules.
      ///     TODO: Fully implement the "check code symbol" ala https://www.crockford.com/wrmg/base32.html
      /// </summary>
      public static readonly IntegerEncoder Base32Crockford;

      /// <summary>
      ///     A base 32 formatter for RFC4648 numnbers, case sensitive
      /// </summary>
      public static readonly IntegerEncoder Base32Rfc4648 = new IntegerEncoder("ABCDEFGHIJKLMNOPQRSTUVXYZ234567",
                                                                               true);

      /// <summary>
      ///     A base 32 formatter for ZRTP encoded numbers, case sensitive
      /// </summary>
      public static readonly IntegerEncoder
      Base32Zrtp = new IntegerEncoder("ybndrfg8ejkmcpqxot1uwisza345h769", true);

      /// <summary>
      ///     A base 32 formatter using 0-9A-MappedDFSVisitor, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Base32Hex = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUV");

      /// <summary>
      ///     A base 36 formatter using 0-9A-Z, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Base36 = new IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");

      /// <summary>
      ///     A base 58 formatter IPFS compliant, case sensitive, alphabet:
      ///     123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz
      /// </summary>
      public static readonly IntegerEncoder Base58Ipfs = new
         IntegerEncoder("123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz", true);

      /// <summary>
      ///     Base 62 formatter using: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz
      /// </summary>
      public static readonly IntegerEncoder Base62 = new
         IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", true);

      /// <summary>
      ///     Base 63 formatter using: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+
      /// </summary>
      public static readonly IntegerEncoder Base63 = new
         IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+", true);

      /// <summary>
      ///     Base 64 formatter using the standard: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/
      /// </summary>
      public static readonly IntegerEncoder Base64 = new
         IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/", true);

      /// <summary>
      ///     Base 64 formatter: Bcrypt compliant
      /// </summary>
      public static readonly IntegerEncoder Base64Bcrypt = new
         IntegerEncoder("./ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", true);

      /// <summary>
      ///     Base 64 formatter: BinHex4 compliant
      /// </summary>
      public static readonly IntegerEncoder Base64BinHex4 = new
         IntegerEncoder("!\"#$%&'()*+,-012345689@ABCDEFGHIJKLMNPQRSTUVXYZ[`abcdefhijklmpqr", true);

      /// <summary>
      ///     Base 64 formatter: Radix64 compliant
      /// </summary>
      public static readonly IntegerEncoder Base64Radix64 = new
         IntegerEncoder(" !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_", true);

      /// <summary>
      ///     Base 64 formatter: Unix B64 compliant
      /// </summary>
      public static readonly IntegerEncoder Base64UnixB64 = new
         IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/", true);

      /// <summary>
      ///     Base 64 formatter: Uuencoding compliant
      /// </summary>
      public static readonly IntegerEncoder Base64Uuencoding = new
         IntegerEncoder("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/", true);

      /// <summary>
      ///     Base 64 formatter: Xxencoding compliant
      /// </summary>
      public static readonly IntegerEncoder Base64Xxencoding = new
         IntegerEncoder("+-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", true);

      /// <summary>
      ///     Base 91 formatter, basE91 compliant (see http://base91.sourceforge.net/)
      /// </summary>
      public static readonly IntegerEncoder Base91 = new
         IntegerEncoder("ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz!#$%&()*+,./:;<=>?@[]^_`{|}~\"", true);

      /// <summary>
      ///     Base 93 formatting, first 93 characters from the ISO8859_15_EncodingCharacters alphabet, starting at "0"
      /// </summary>
      public static readonly IntegerEncoder Base930MonotonicIso885915 = new IntegerEncoder(
         Iso885915EncodingCharacters.Substring(Iso885915EncodingCharacters.IndexOf('0'), 93), true);

      /// <summary>
      ///     Base 93 formatting, first 93 characters from the ISO8859_15_EncodingCharacters alphabet, starting at "a"
      /// </summary>
      public static readonly IntegerEncoder Base93StartAtLowercaseAMonotonicIso885915 = new IntegerEncoder(
         Iso885915EncodingCharacters.Substring(Iso885915EncodingCharacters.IndexOf('a'), 93), true);

      /// <summary>
      ///     Base 93 formatting, first 93 characters from the ISO8859_15_EncodingCharacters alphabet, starting at "A"
      /// </summary>
      public static readonly IntegerEncoder Base93StartAtUppercaseAMonotonicIso885915 = new IntegerEncoder(
         Iso885915EncodingCharacters.Substring(Iso885915EncodingCharacters.IndexOf('A'), 93), true);

      /// <summary>
      ///     Base 93 formatting, first 93 characters from the ISO8859_15_EncodingCharacters alphabet"
      /// </summary>
      public static readonly IntegerEncoder Base93MonotonicIso885915 = new IntegerEncoder(
         Iso885915EncodingCharacters.Substring(0, 93), true);

      /// <summary>
      ///     A base 2 formatter using 01
      /// </summary>
      public static readonly IntegerEncoder Binary = new IntegerEncoder("01", true);

      /// <summary>
      ///     A base 10 formatter using 0-9
      /// </summary>
      public static readonly IntegerEncoder Decimal = new IntegerEncoder("0123456789", true);

      /// <summary>
      ///     A base 12 formatter using 0-9AB, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Duodecimal = new IntegerEncoder("0123456789AB");

      /// <summary>
      ///     A base 58 formatter Flickr short URL compliant, case sensitive, alphabet:
      ///     123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ
      /// </summary>
      public static readonly IntegerEncoder FlickrBase58 = new
         IntegerEncoder("123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ", true);

      /// <summary>
      ///     A base 17 formatter using 0-9A-G, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Heptadecimal = new IntegerEncoder("0123456789ABCDEFG");

      /// <summary>
      ///     A base 16 formatter using 0-9A-F, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Hexadecimal = new IntegerEncoder("0123456789ABCDEF");

      /// <summary>
      ///     A base 9 formatter using 0-8
      /// </summary>
      public static readonly IntegerEncoder Nonary = new IntegerEncoder("012345678", true);

      /// <summary>
      ///     A base 8 formatter using 0-7
      /// </summary>
      public static readonly IntegerEncoder Octal = new IntegerEncoder("01234567", true);

      /// <summary>
      ///     A base 15 formatter using 0-9A-E, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Pentadecimal = new IntegerEncoder("0123456789ABCDE");

      /// <summary>
      ///     A quaternary formatter using 0123
      /// </summary>
      public static readonly IntegerEncoder Quaternary = new IntegerEncoder("0123", true);

      /// <summary>
      ///     A quinary (base 5) formatter using 0-4
      /// </summary>
      public static readonly IntegerEncoder Quinary = new IntegerEncoder("01234", true);

      /// <summary>
      ///     A base 6 formatter using 0-5
      /// </summary>
      public static readonly IntegerEncoder Senary = new IntegerEncoder("012345", true);

      /// <summary>
      ///     A base 7 formatter using 0-6
      /// </summary>
      public static readonly IntegerEncoder Septenary = new IntegerEncoder("0123456", true);

      /// <summary>
      ///     Base 60 formatter using this alphabet: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx
      /// </summary>
      public static readonly IntegerEncoder Sexagesimal = new
         IntegerEncoder("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx", true);

      /// <summary>
      ///     A base 3 formatter 012
      /// </summary>
      public static readonly IntegerEncoder Ternary = new IntegerEncoder("012", true);

      /// <summary>
      ///     A base 14 formatter using 0-9A-D, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Tetradecimal = new IntegerEncoder("0123456789ABCD");

      /// <summary>
      ///     A base 13 formatter using 0-9A-C, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Tridecimal = new IntegerEncoder("0123456789ABC");

      /// <summary>
      ///     A base 11 formatter using 0-9A, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Undecimal = new IntegerEncoder("0123456789A");

      /// <summary>
      ///     A base 20 formatter using 0-9A-J, case insensitive
      /// </summary>
      public static readonly IntegerEncoder Vegesimal = new IntegerEncoder("0123456789ABCDEFGHIJ");

      #endregion Public Fields

      #region special constants

      /// <summary>A base alphabet for various numeric encoders with whitespace and certain special characters removed.</summary>
      /// <remarks>
      ///     Readable/non-whitespace subset of ISO8859. The following characters have been removed. This gives an alphabet of
      ///     166 characters in numerical order.
      ///     Characters: `¯°²³¹º·
      ///     0x2D (-) and numerically lower characters.
      ///     0x7F (DEL)
      ///     0xA0 (NBSP)
      ///     0xAD (SHY)
      /// </remarks>
      public const string Iso885915EncodingCharacters =
         "./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~¡¢£€¥Š§š©«¬®±Žµ¶ž»ŒœŸ¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ";

      /// <summary>
      ///     The decoding mappings for Crockford base 32 decoding.
      /// </summary>
      public static readonly string[] Base32CrockfordDecodeCharacterSet;

      #endregion special constants
   }
}
