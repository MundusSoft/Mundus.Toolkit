using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MToolkit
{
    /// <summary>
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Converts the <paramref name="guid" /> into sequential at end Guid, where the last 6 bytes are sequential.
        /// </summary>
        /// <param name="guid">The <see cref="Guid" /> to be converted.</param>
        /// <returns>
        ///     <see cref="Guid" /> that is sequential at end, where the last 6 bytes are sequential, equivalent to the sql server
        ///     NEWSEQUENTIALID function.
        ///     <para>
        ///         This type of guid is perfect for UUID keys in MySQL and PostgreSQL
        ///     </para>
        /// </returns>
        public static Guid ToSequentialAtEnd(this Guid guid)
        {
            byte[] guidArray = guid.ToByteArray();

            DateTime now = DateTime.UtcNow;
            var baseDate = new DateTime(1900, 1, 1);

            // Get the days and milliseconds which will be used to build the byte string 
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.33333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long) (msecs.TotalMilliseconds / 3.33333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

        /// <summary>
        ///     Converts the <paramref name="guid" /> into a string sequential Guid, where the first 6 bytes are sequential.
        /// </summary>
        /// <param name="guid">The <see cref="Guid" /> to be converted.</param>
        /// <returns>
        ///     <see cref="Guid" /> that is sequential as a string, where the first 6 bytes are sequential.
        ///     <para>
        ///         This type of guid is perfect for UUID keys in MySQL and PostgreSQL
        ///     </para>
        /// </returns>
        public static Guid ToSequentialAsString(this Guid guid)
        {
            guid = guid.ToSequentialAsBinary();

            if (!BitConverter.IsLittleEndian)
                return guid;

            byte[] guidArray = guid.ToByteArray();
            Array.Reverse(guidArray, 0, 4);
            Array.Reverse(guidArray, 4, 2);

            return new Guid(guidArray);
        }

        /// <summary>
        ///     Converts the <paramref name="guid" /> into a Sql server sequential Guid.
        /// </summary>
        /// <param name="guid">The <see cref="Guid" /> to be converted.</param>
        /// <returns>
        ///     <see cref="Guid" /> that is binary sequential, where the first 6 bytes are sequential.
        ///     <para>
        ///         This type of guid is perfect for UUID keys in Oracle.
        ///     </para>
        /// </returns>
        public static Guid ToSequentialAsBinary(this Guid guid)
        {
            byte[] guidArray = guid.ToByteArray();

            DateTime now = DateTime.UtcNow;
            var baseDate = new DateTime(1900, 1, 1);

            // Get the days and milliseconds which will be used to build the byte string 
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long) (msecs.TotalMilliseconds));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, 0, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, 2, 4);

            return new Guid(guidArray);
        }

        /// <summary>
        ///     Gets a numeric hash of the given string.
        ///     This uses the Jenkins one a a time hash implementation, so it can re reused across multiple platforms.
        /// </summary>
        /// <param name="str">The string to be hashed.</param>
        /// <returns>The numeric hash value.</returns>
        public static uint GetOneAtTimeHash(this String str)
        {
            uint hash = 0;
            foreach (var chr in str)
            {
                hash += Convert.ToUInt32(chr);
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }

        /// <summary>
        ///     Gets a numeric hash of the given string.
        ///     This uses the Jenkins one a a time hash implementation, so it can re reused across multiple platforms.
        /// </summary>
        /// <param name="str">The string to be hashed.</param>
        /// <returns>The numeric hash value.</returns>
        public static string GetOneAtTimeHashString(this String str)
        {
            uint hash = 0;
            foreach (var chr in str)
            {
                hash += Convert.ToUInt32(chr);
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash.ToString("X");
        }

        /// <summary>
        ///     Trims a string if not null, if the string is null then an empty string is returned.
        /// </summary>
        /// <param name="str">The string to be trimmed.</param>
        /// <returns>A trimmed or empty string.</returns>
        public static string TrimIfNotNull(this string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : str.Trim();
        }

        /// <summary>
        ///     Returns a <see langword="null" /> string in case <paramref name="source" /> is null, empty consists only of
        ///     white-space characters.
        /// </summary>
        /// <param name="source">The string to be check.</param>
        /// <returns>
        ///     <see langword="null" /> string case <paramref name="source" /> is null, empty consists only of white-space
        ///     characters, <paramref name="source" /> trimmed otherwise
        /// </returns>
        public static string ToNullIfEmptyOrWhiteSpaced(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? null : source.Trim();
        }

        /// <summary>
        ///     converts a string into a enumeration of her text elements.
        ///     <para>A text element can be more that 1 char, in case of unicoded or accent chars.</para>
        /// </summary>
        /// <param name="source">The <see cref="string" /> of whom the text elements will be returned.</param>
        /// <returns><see cref="IEnumerable{T}" /> where T is of type string, containing all the text elements as strings.</returns>
        /// <remarks>This is usefull for example to revert a string <see cref="M:ReverseString" /> for a usage example.</remarks>
        public static IEnumerable<string> ToTextElements(this string source)
        {
            var enumerator = StringInfo.GetTextElementEnumerator(source);
            while (enumerator.MoveNext())
            {
                yield return enumerator.GetTextElement();
            }
        }

        /// <summary>
        ///     Reverses the specified string.
        /// </summary>
        /// <param name="input">The string to reverse.</param>
        /// <returns>The input string, reversed.</returns>
        /// <remarks>
        ///     This method correctly reverses strings containing supplementary characters
        ///     (which are encoded with two surrogate code units).
        /// </remarks>
        public static string ReverseString(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return string.Concat(input.ToTextElements().Reverse());
        }

        /// <summary>
        ///     Determines whether the string holds a valid credit card number .
        /// </summary>
        /// <param name="cardNumber">The string to validate.</param>
        /// <returns><c>true</c> if <see cref="cardNumber" /> holds a valid credit card number.</returns>
        public static bool IsCreditCardNumber(this string cardNumber)
        {
            long number;
            if (cardNumber.Trim().Length < 13
                || cardNumber.Trim().Length > 19
                || !long.TryParse(cardNumber.Trim().PadRight(19, '0').Substring(0, 6), out number))
            {
                return false;
            }
            if ((number >= 340000 && number <= 349999) || (number >= 370000 && number <= 379999))
            {
                return true;
            }
            else if ((number >= 300000 && number <= 305999) || (number >= 360000 && number <= 369999) ||
                     (number >= 380000 && number <= 389999))
            {
                return true;
            }
            else if ((number >= 601100 && number <= 601199))
            {
                return true;
            }
            else if ((number >= 352800 && number <= 358999))
            {
                return true;
            }
            else if ((number >= 510000 && number <= 559999) || (number >= 222100 && number <= 272099))
            {
                return true;
            }
            else if ((number >= 400000 && number <= 499999))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Gets the name of the file where the <paramref name="exception" /> was thrown.
        /// </summary>
        /// <param name="exception">The <see cref="Exception" /> to extract the file name.</param>
        /// <returns>
        ///     <see cref="string" /> with the name of the file that thrown the <paramref name="exception" />, "Unavailable",
        ///     if it wasn't possible to determine the file name.
        /// </returns>
        public static string GetFileName(this Exception exception)
        {
            if (exception.StackTrace == null)
            {
                return "Unavailable";
            }
            var originalLineIndex = exception.StackTrace.IndexOf(":line", StringComparison.OrdinalIgnoreCase);
            if (originalLineIndex == -1)
            {
                return "Unavailable";
            }
            var originalLine = exception.StackTrace.Substring(0, originalLineIndex);
            var sections = originalLine.Split('\\');
            return sections[sections.Length - 1];
        }

        /// <summary>
        ///     Gets the line number from the .
        /// </summary>
        /// <param name="exception">The <see cref="Exception" /> to extract the line number.</param>
        /// <returns>
        ///     <see cref="int" /> with the line number where the <paramref name="exception" /> was thrown, 0 if it wasn't
        ///     possible to determine the line number.
        /// </returns>
        public static int GetLineNumber(this Exception exception)
        {
            if (exception.StackTrace == null)
            {
                return 0;
            }
            var sections = exception.StackTrace.Split(' ');
            var index = sections.TakeWhile(section => !section.EndsWith(":line")).Count();
            if (index == sections.Length)
            {
                return 0;
            }
            var lineNumber = sections[index + 1];
            int number;
            try //Strip the /r/n if present
            {
                number = Convert.ToInt32(lineNumber.Substring(0, lineNumber.Length - 2));
            }
            catch (FormatException)
            {
                number = Convert.ToInt32(lineNumber);
            }

            return number;
        }

        /// <summary>
        ///     Gets the class name with the method name.
        /// </summary>
        /// <param name="type">The <see cref="Type" /> to extract the class name.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>
        ///     <see cref="string" /> compose of <paramref name="type" />.Name, method <paramref name="methodName" />
        /// </returns>
        public static string GetClassNameWithMethod(this Type type, string methodName)
        {
            return $"{type.Name}, method {methodName}";
        }

        /// <summary>
        ///     Converts the 1st letter of <paramref name="source" /> into uppercase.
        /// </summary>
        /// <param name="source">The string to be converted.</param>
        /// <returns><paramref name="source" /> where the 1st character is in uppercase</returns>
        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            // convert to char array of the string
            var letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

        /// <summary>
        ///     Converts <paramref name="source" /> into proper format, where the 1st character and all the characters after a
        ///     space are in uppercase.
        /// </summary>
        /// <param name="source">The string to be converted to proper.</param>
        /// <returns>The <paramref name="source" /> <see cref="string" /> in Proper format.</returns>
        public static string ToProper(this string source)
        {
            if (string.IsNullOrEmpty(source.TrimIfNotNull()))
            {
                return string.Empty;
            }

            var sb = new StringBuilder(source.TrimIfNotNull());
            sb[0] = char.ToUpper(sb[0]);
            for (var i = 1; i < sb.Length - 1; i++)
            {
                if (i < sb.Length - 2 && (char.IsWhiteSpace(sb[i]) && char.IsLetter(sb[i + 1])))
                {
                    sb[i + 1] = char.ToUpper(sb[i + 1]);
                    i++;
                }
                else
                {
                    sb[i] = char.ToLower(sb[i]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Formats the <paramref name="strToFormat" /> with the <paramref name="formatStr" />.
        /// </summary>
        /// <param name="strToFormat">The <see cref="string" /> to format.</param>
        /// <param name="formatStr">The format string.</param>
        /// <returns><see cref="string" /> formatted based on <paramref name="formatStr" /></returns>
        public static string FormatString(this string strToFormat, string formatStr)
        {
            if (string.IsNullOrEmpty(strToFormat) || string.IsNullOrEmpty(formatStr))
            {
                return strToFormat;
            }
            var retVal = new StringBuilder(strToFormat).Append(" ");
            var formatChars = formatStr.Trim().ToCharArray();
            int i;
            var addit = true;
            for (i = 0; i <= formatChars.Length - 1; i++)
            {
                if ((retVal[i] == ' '))
                {
                    addit = false;
                }

                switch (formatChars[i])
                {
                    case 'X':
                        if ((addit && !(char.IsLetterOrDigit(retVal[i]))))
                        {
                            retVal.Insert(i, formatChars[i].ToString());
                        }
                        break;
                    case '9':
                        if ((addit && !(char.IsDigit(retVal[i]))))
                        {
                            retVal.Insert(i, formatChars[i].ToString());
                        }
                        break;
                    case 'A':
                        if (((addit && !char.IsLetter(retVal[i]))))
                        {
                            retVal.Insert(i, formatChars[i].ToString());
                        }
                        break;
                    default:
                        retVal.Insert(i, formatChars[i].ToString());
                        break;
                }
            }
            retVal.Length = formatChars.Length;
            return retVal.ToString();
        }

        /// <summary>
        /// Encodes <paramref name="value"/> from <see cref="Encoding"/> <paramref name="from"/> into <see cref="Encoding"/> <paramref name="to"/>.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to be encoded.</param>
        /// <param name="from">The source <see cref="Encoding"/> of <paramref name="value"/>.</param>
        /// <param name="to">The <see cref="Encoding"/> of <paramref name="value"/> final <see cref="string"/>.</param>
        /// <returns><see cref="string"/> corresponding to <paramref name="value"/> encode with <paramref name="to"/>.</returns>
        public static string EncodeTo(this string value, Encoding from, Encoding to)
        {
            try
            {
                byte[] bytes = Encoding.Convert(from, to, from.GetBytes(value));
                return to.GetString(bytes, 0, bytes.Length);
            }
            catch (Exception)
            {
                return value;
            }
        }

        /// <summary>
        /// Gets the bytes of <paramref name="source"/> without any encoding.
        /// The length of the resulting byte array will be source.Length * sizeof(char).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to get the bytes.</param>
        /// <returns><see cref="byte"/> array with a length of source.Length * sizeof(char)</returns>
        public static byte[] GetBytes(this string source)
        {
            var bytes = new byte[source.Length * sizeof(char)];
            Buffer.BlockCopy(source.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="defaultEncoding">The default <see cref="Encoding"/> that will be return if not able to determine the encoding of <paramref name="source"/>.</param>
        ///     The number of bytes to check of the <paramref name="source"/>. 
        ///     Higher value is slower, but more reliable (especially UTF-8 with special characters
        ///     later on may appear to be ASCII initially). If bytesToCheck = 0, then bytesToCheck
        ///     becomes the length of the <paramref name="source"/> (for maximum reliability).
        /// <returns></returns>
        public static Encoding GetEncoding(this string source, Encoding defaultEncoding, int bytesToCheck = 1000)
        {
            return source.GetBytes().GetEncoding(defaultEncoding, bytesToCheck);
        }

        /// <summary>
        ///     Gets the encoding for UTF-7, UTF-8/16/32 (bom, no bom, little
        ///     & big endian), and local default codepage, and potentially other codepages.
        /// </summary>
        /// <param name="source">The <see cref="byte"/> array to determine <see cref="Encoding"/>.</param>
        /// <param name="defaultEncoding">The default <see cref="Encoding"/> that will be return if not able to determine the encoding of <paramref name="source"/>.</param>
        /// <param name="bytesToCheck">
        ///     The number of bytes to check of the <paramref name="source"/>. 
        ///     Higher value is slower, but more reliable (especially UTF-8 with special characters
        ///     later on may appear to be ASCII initially). If bytesToCheck = 0, then bytesToCheck
        ///     becomes the length of the <paramref name="source"/> (for maximum reliability).
        /// </param>
        /// <returns><see cref="Encoding"/> of <paramref name="source"/></returns>
        public static Encoding GetEncoding(this byte[] source, Encoding defaultEncoding, int bytesToCheck = 1000)
        {
            //////////////// First check the low hanging fruit by checking if a
            //////////////// BOM/signature exists (sourced from http://www.unicode.org/faq/utf_bom.html#bom4)
            if (source.Length >= 4 && source[0] == 0x00 && source[1] == 0x00 && source[2] == 0xFE && source[3] == 0xFF)
            {
                return Encoding.GetEncoding("UTF-32BE"); // UTF-32, big-endian 
            }
            else if (source.Length >= 4 && source[0] == 0xFF && source[1] == 0xFE && source[2] == 0x00 &&
                     source[3] == 0x00)
            {
                return Encoding.GetEncoding("utf-32"); // UTF-32, little-endian
            }
            else if (source.Length >= 2 && source[0] == 0xFE && source[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode; // UTF-16, big-endian
            }
            else if (source.Length >= 2 && source[0] == 0xFF && source[1] == 0xFE)
            {
                return Encoding.Unicode; // UTF-16, little-endian / unicode
            }
            else if (source.Length >= 3 && source[0] == 0xEF && source[1] == 0xBB && source[2] == 0xBF)
            {
                return Encoding.UTF8; // UTF-8
            }
            else if (source.Length >= 3 && source[0] == 0x2b && source[1] == 0x2f && source[2] == 0x76)
            {
                return Encoding.GetEncoding("utf-7"); // UTF-7
            }

            //////////// If the code reaches here, no BOM/signature was found, so now
            //////////// we need to 'taste' the byte array to see if can manually discover
            //////////// the encoding. A high bytesToCheck value is desired for UTF-8
            bytesToCheck = bytesToCheck > 0 && bytesToCheck < source.Length ? bytesToCheck : source.Length;

            // Some text files are encoded in UTF8, but have no BOM/signature. Hence
            // the below manually checks for a UTF8 pattern. This code is based off
            // the top answer at: http://stackoverflow.com/questions/6555015/check-for-invalid-utf8
            // For our purposes, an unnecessarily strict (and terser/slower)
            // implementation is shown at: http://stackoverflow.com/questions/1031645/how-to-detect-utf-8-in-plain-c
            // For the below, false positives should be exceedingly rare (and would
            // be either slightly malformed UTF-8 (which would suit our purposes
            // anyway) or 8-bit extended ASCII/UTF-16/32 at a vanishingly long shot).
            var i = 0;
            var utf8 = false;
            while (i < bytesToCheck - 4)
            {
                if (source[i] <= 0x7F)
                {
                    i += 1;
                    continue;
                }
                // If all characters are below 0x80, then it is valid UTF8, but UTF8 is not 'required' (and therefore the text is more desirable to be treated as the default codepage of the computer). Hence, there's no "utf8 = true;" code unlike the next three checks.
                if (source[i] >= 0xC2 && source[i] <= 0xDF && source[i + 1] >= 0x80 && source[i + 1] < 0xC0)
                {
                    i += 2;
                    utf8 = true;
                    continue;
                }
                if (source[i] >= 0xE0 && source[i] <= 0xF0 && source[i + 1] >= 0x80 && source[i + 1] < 0xC0 &&
                    source[i + 2] >= 0x80 && source[i + 2] < 0xC0)
                {
                    i += 3;
                    utf8 = true;
                    continue;
                }
                if (source[i] >= 0xF0 && source[i] <= 0xF4 && source[i + 1] >= 0x80 && source[i + 1] < 0xC0 &&
                    source[i + 2] >= 0x80 && source[i + 2] < 0xC0 && source[i + 3] >= 0x80 && source[i + 3] < 0xC0)
                {
                    i += 4;
                    utf8 = true;
                    continue;
                }
                utf8 = false;
                break;
            }
            if (utf8)
            {
                return Encoding.UTF8;
            }

            // The next check is a heuristic attempt to detect UTF-16 without a BOM.
            // We simply look for zeroes in odd or even byte places, and if a certain
            // threshold is reached, the code is 'probably' UTF-16.          
            var threshold = 0.3; // proportion of chars step 2 which must be zeroed to be diagnosed as utf-16. 0.3 = 60%
            var count = 0.0;
            for (var n = 0; n < bytesToCheck; n += 2)
                if (source[n] == 0) count++;
            if (count / bytesToCheck > threshold)
            {
                return Encoding.BigEndianUnicode;
            }
            count = 0.0;
            for (var n = 1; n < bytesToCheck; n += 2)
                if (source[n] == 0) count++;
            if (count / bytesToCheck > threshold)
            {
                return Encoding.Unicode;
            } // (little-endian)

            // Finally, a long shot - let's see if we can find "charset=xyz" or
            // "encoding=xyz" to identify the encoding:
            for (var index = 0; index < bytesToCheck - 9; index++)
            {
                if (((source[index + 0] != 'c' && source[index + 0] != 'C') || (source[index + 1] != 'h' && source[index + 1] != 'H') ||
                     (source[index + 2] != 'a' && source[index + 2] != 'A') || (source[index + 3] != 'r' && source[index + 3] != 'R') ||
                     (source[index + 4] != 's' && source[index + 4] != 'S') || (source[index + 5] != 'e' && source[index + 5] != 'E') ||
                     (source[index + 6] != 't' && source[index + 6] != 'T') || (source[index + 7] != '=')) &&
                    ((source[index + 0] != 'e' && source[index + 0] != 'E') || (source[index + 1] != 'n' && source[index + 1] != 'N') ||
                     (source[index + 2] != 'c' && source[index + 2] != 'C') || (source[index + 3] != 'o' && source[index + 3] != 'O') ||
                     (source[index + 4] != 'd' && source[index + 4] != 'D') || (source[index + 5] != 'i' && source[index + 5] != 'I') ||
                     (source[index + 6] != 'n' && source[index + 6] != 'N') || (source[index + 7] != 'g' && source[index + 7] != 'G') ||
                     (source[index + 8] != '=')))
                {
                    continue;
                }
                if (source[index + 0] == 'c' || source[index + 0] == 'C')
                {
                    index += 8;
                }
                else
                {
                    index += 9;
                }
                if (source[index] == '"' || source[index] == '\'')
                    index++;
                int oldIndex = index;
                while (index < bytesToCheck &&
                       (source[index] == '_' || source[index] == '-' || (source[index] >= '0' && source[index] <= '9') ||
                        (source[index] >= 'a' && source[index] <= 'z') || (source[index] >= 'A' && source[index] <= 'Z')))
                {
                    index++;
                }
                var newSource = new byte[index - oldIndex];
                Array.Copy(source, oldIndex, newSource, 0, index - oldIndex);
                try
                {
                    string internalEnc = Encoding.GetEncoding("us-ascii").GetString(newSource, 0, newSource.Length);
                    return Encoding.GetEncoding(internalEnc);
                }
                catch
                {
                    break;
                } // If C# doesn't recognize the name of the encoding, break.
            }

            // If all else fails, just returns the passed in default encoding.
            return defaultEncoding;
        }

        public static string GetPropertyNameFromExpression<T>(
        this INotifyPropertyChanged target,
        Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("The expression is not a member access expression.", nameof(expression));

            var member = memberExpression.Member as PropertyInfo;
            if (member == null)
                throw new ArgumentException("The member access expression does not access  property.", nameof(expression));

            if (member.DeclaringType == null)
                throw new ArgumentException("The Member is a global member", nameof(expression));


            if (member.DeclaringType.GetTypeInfo().ImplementedInterfaces.All(i=> i != typeof(INotifyPropertyChanged)))
            {
                throw new ArgumentException("The referenced property belongs to a different type.", nameof(expression));
            }

            if (member.GetMethod.IsStatic)
            {
                throw new ArgumentException("The referenced property is a static property.", nameof(expression));
            }

            return member.Name;
        }
    }
}
