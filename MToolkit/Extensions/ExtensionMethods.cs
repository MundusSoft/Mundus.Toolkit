using System;
using System.Linq;
using System.Text;

namespace MyMvvm
{
    /// <summary>
    /// </summary>
    public static class ExtensionMethods
    {
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
        ///     Trims a string if not null, if the string is null then an empty string is returned.
        /// </summary>
        /// <param name="str">The string to be trimmed.</param>
        /// <returns>A trimmed or empty string.</returns>
        public static string TrimIfNotNull(this string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : str.Trim();
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
            var output = new char[input.Length];
            Array.Reverse(input.ToCharArray());
            return new string(output);
        }

        /// <summary>
        ///     Determines whether the string holds a valid credit card number .
        /// </summary>
        /// <param name="cardNumber">The string to validate.</param>
        /// <returns><c>true</c> if <see cref="cardNumber" /> holds a valid credit card number.</returns>
        public static bool IsCreditCardNumber(this string cardNumber)
        {
            var retVal = false;
            try
            {
                int number;
                if (int.TryParse(cardNumber.Trim().PadRight(16, '0').Substring(0, 6), out number))
                {
                    if ((number >= 340000 && number <= 349999) || (number >= 370000 && number <= 379999))
                    {
                        retVal = true;
                    }
                    else if ((number >= 300000 && number <= 305999) || (number >= 360000 && number <= 369999) ||
                             (number >= 380000 && number <= 389999))
                    {
                        retVal = true;
                    }
                    else if ((number >= 601100 && number <= 601199))
                    {
                        retVal = true;
                    }
                    else if ((number >= 352800 && number <= 358999))
                    {
                        retVal = true;
                    }
                    else if ((number >= 510000 && number <= 559999))
                    {
                        retVal = true;
                    }
                    else if ((number >= 400000 && number <= 499999))
                    {
                        retVal = true;
                    }
                }
            }
            catch
            {
                // ignored
            }
            return retVal;
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
    }
}
