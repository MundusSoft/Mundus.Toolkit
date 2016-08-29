using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MToolkit
{
    /// <summary>
    ///     Static class responsible for holding the extension methods for <see cref="object" />.
    /// </summary>
    public static class ObjectExtensionMethods
    {
        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="bool" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="bool" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="bool" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static bool ToBoolean(this object obj, bool defaultValue = default(bool))
        {
            try
            {
                return obj != null && Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="int" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a <see cref="int" />
        ///     type.
        /// </param>
        /// <returns>
        ///     A <see cref="int" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static int ToInt(this object obj, int defaultValue = default(int))
        {
            try
            {
                return obj == null ? default(int) : Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="short" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="short" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="short" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static short ToShort(this object obj, short defaultValue = default(short))
        {
            try
            {
                return obj == null ? default(short) : Convert.ToInt16(obj);
            }
            catch (Exception)
            {
                return default(short);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="long" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="long" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="long" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static long ToLong(this object obj, long defaultValue = default(long))
        {
            try
            {
                return obj == null ? default(long) : Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return default(long);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="float" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="float" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="float" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static float ToFloat(this object obj, float defaultValue = default(float))
        {
            try
            {
                return obj == null ? default(float) : Convert.ToSingle(obj);
            }
            catch (Exception)
            {
                return default(float);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="double" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="double" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="double" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static double ToDouble(this object obj, double defaultValue = default(double))
        {
            try
            {
                return obj == null ? default(double) : Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return default(double);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="decimal" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="decimal" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="decimal" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static double ToDecimal(this object obj, decimal defaultValue = default(decimal))
        {
            try
            {
                return obj == null ? default(double) : Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return default(double);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="byte" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="byte" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="byte" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static byte ToByte(this object obj, byte defaultValue = default(byte))
        {
            try
            {
                return obj == null ? default(byte) : Convert.ToByte(obj);
            }
            catch (Exception)
            {
                return default(byte);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="byte" /> array.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into an array of
        ///     <see cref="byte" />.
        /// </param>
        /// <returns>
        ///     An array of  <see cref="byte" /> converted from <paramref name="obj" />, or the <paramref name="defaultValue" />
        ///     case the conversion fails.
        /// </returns>
        public static byte[] ToByteArray(this object obj, byte[] defaultValue = default(byte[]))
        {
            try
            {
                return (byte[]) obj;
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="char" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="char" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="char" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static char ToChar(this object obj, char defaultValue = default(char))
        {
            try
            {
                return obj == null ? default(char) : Convert.ToChar(obj);
            }
            catch (Exception)
            {
                return default(char);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="sbyte" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="sbyte" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="sbyte" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static sbyte ToSByte(this object obj, sbyte defaultValue = default(sbyte))
        {
            try
            {
                return obj == null ? default(sbyte) : Convert.ToSByte(obj);
            }
            catch (Exception)
            {
                return default(sbyte);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="ushort" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="ushort" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="ushort" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static ushort ToUShort(this object obj, ushort defaultValue = default(ushort))
        {
            try
            {
                return obj == null ? default(ushort) : Convert.ToUInt16(obj);
            }
            catch (Exception)
            {
                return default(ushort);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="uint" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="uint" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="uint" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static uint ToUInt(this object obj, uint defaultValue = default(uint))
        {
            try
            {
                return obj == null ? default(uint) : Convert.ToUInt32(obj);
            }
            catch (Exception)
            {
                return default(uint);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="ulong" />.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to be converted.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="ulong" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="ulong" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static ulong ToULong(this object obj, ulong defaultValue = default(ulong))
        {
            try
            {
                return obj == null ? default(ulong) : Convert.ToUInt64(obj);
            }
            catch (Exception)
            {
                return default(ulong);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="DateTime" />.
        ///     <para>
        ///         If <paramref name="obj" /> is a <see cref="TimeSpan" /> then the <see cref="DateTime" /> returned will be the
        ///         current date plus the <paramref name="obj" />
        ///     </para>
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to be converted.</param>
        /// <param name="timeZone">The time zone info that the<paramref name="obj" /> should be converted to.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="DateTime" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="DateTime" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the
        ///     conversion fails.
        /// </returns>
        public static DateTime ToDateTime(this object obj,
                                          TimeZoneInfo timeZone = null,
                                          DateTime defaultValue = default(DateTime))
        {
            try
            {
                if (obj is TimeSpan)
                {
                    return DateTime.SpecifyKind(DateTime.Now.Date.Add((TimeSpan) obj), DateTimeKind.Unspecified);
                }
                if (obj is DateTime && ((DateTime) obj).Kind != DateTimeKind.Utc)
                {
                    return DateTime.SpecifyKind((DateTime) obj, DateTimeKind.Unspecified);
                }
                if (obj is DateTimeOffset && ((DateTimeOffset) obj).Offset == TimeSpan.Zero)
                {
                    return ((DateTimeOffset) obj).DateTime.ChangeToUtcKind();
                }
                if (obj is DateTimeOffset && timeZone == null)
                {
                    return ((DateTimeOffset) obj).DateTime.ChangeToUnspecifiedKind();
                }
                if (obj is DateTimeOffset)
                {
                    return TimeZoneInfo.ConvertTime((DateTimeOffset) obj, timeZone).DateTime.ChangeToUnspecifiedKind();
                }
                var str = obj.ToString();
                DateTimeOffset result;
                //(Z|[+-]\d{1,2}:{0,1}\d{0,2}|GMT)$
                //This Regex checks if the string has OffSet in the end of it.
                //The various offsets can be Z (which stands for zero offset) to represent UTC,
                //An real Offset like -05:00 or +1:00,
                //Or GMT, that refers to Greenwich Mean Time
                if (Regex.IsMatch(str, @"(Z|[+-]\d{1,2}:{0,1}\d{0,2}|GMT)$")
                    && DateTimeOffset.TryParse(str,
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.AllowWhiteSpaces,
                                               out result))
                {
                    return timeZone == null
                               ? result.Offset == TimeSpan.Zero
                                     ? result.DateTime.ChangeToUtcKind()
                                     : result.DateTime.ChangeToUnspecifiedKind()
                               : TimeZoneInfo.ConvertTime(result, timeZone).DateTime.ChangeToUnspecifiedKind();
                }
                return DateTime.Parse(str,
                                      CultureInfo.InvariantCulture,
                                      DateTimeStyles.AllowWhiteSpaces)
                               .ChangeToUnspecifiedKind();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="Nullable{DateTime}" />.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to be converted.</param>
        /// <param name="timeZone">The time zone info that the<paramref name="obj" /> should be converted to.</param>
        /// <returns>
        ///     A <see cref="Nullable{DateTime}" /> converted from <paramref name="obj" />, or null case the conversion fails.
        /// </returns>
        public static DateTime? ToDateTimeNullable(this object obj, TimeZoneInfo timeZone = null)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                var result = obj.ToDateTime(timeZone, DateTime.MinValue.AddMinutes(1));
                return DateTime.Compare(result, DateTime.MinValue.AddMinutes(1)) == 0 ? (DateTime?) null : result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="DateTimeOffset" />.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to be converted.</param>
        /// <param name="timeZone">The time zone info that the<paramref name="obj" /> should be converted to.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="DateTimeOffset" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="DateTimeOffset" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" />
        ///     case the conversion to <see cref="DateTimeOffset" /> isn't possible.
        /// </returns>
        public static DateTimeOffset ToDateTimeOffset(this object obj,
                                                      TimeZoneInfo timeZone = null,
                                                      DateTimeOffset defaultValue = default(DateTimeOffset))
        {
            try
            {
                if (obj is TimeSpan && timeZone == null)
                {
                    return new DateTimeOffset(DateTime.Now.Date).Add((TimeSpan) obj);
                }
                DateTime date;
                if (obj is TimeSpan)
                {
                    date = DateTime.Now.Date.ChangeToUnspecifiedKind();
                    return new DateTimeOffset(date, timeZone.GetUtcOffset(date)).Add((TimeSpan) obj);
                }
                if (obj is DateTime && timeZone == null)
                {
                    return new DateTimeOffset((DateTime) obj);
                }
                if (obj is DateTime && ((DateTime) obj).Kind != DateTimeKind.Utc)
                {
                    date = (DateTime) obj;
                    date = date.ChangeToUnspecifiedKind();
                    return new DateTimeOffset(date, timeZone.GetUtcOffset(date));
                }
                if (obj is DateTimeOffset && (timeZone == null || ((DateTimeOffset) obj).Offset == TimeSpan.Zero))
                {
                    return (DateTimeOffset) obj;
                }
                if (obj is DateTimeOffset)
                {
                    return TimeZoneInfo.ConvertTime((DateTimeOffset) obj, timeZone);
                }
                var str = obj.ToString();
                DateTimeOffset result;
                //(Z|[+-]\d{1,2}:{0,1}\d{0,2}|GMT)$
                //This Regex checks if the string has OffSet in the end of it.
                //The various offsets can be Z (which stands for zero offset) to represent UTC,
                //An real Offset like -05:00 or +1:00,
                //Or GMT, that refers to Greenwich Mean Time
                if (Regex.IsMatch(str, @"(Z|[+-]\d{1,2}:{0,1}\d{0,2}|GMT)$")
                    && DateTimeOffset.TryParse(str,
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.AllowWhiteSpaces,
                                               out result))
                {
                    return timeZone == null
                               ? result
                               : TimeZoneInfo.ConvertTime(result, timeZone);
                }
                date = str.ToDateTime();
                result = timeZone == null
                             ? new DateTimeOffset(date)
                             : new DateTimeOffset(date, timeZone.GetUtcOffset(date));
                return result;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="Nullable{DateTimeOffset}" />.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to be converted.</param>
        /// <param name="timeZone">The time zone info that the<paramref name="obj" /> should be converted to.</param>
        /// <returns>A <see cref="Nullable{DateTimeOffset}" />converted from <paramref name="obj" />.</returns>
        public static DateTimeOffset? ToDateTimeOffsetNullable(this object obj, TimeZoneInfo timeZone = null)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                var result = obj.ToDateTimeOffset(timeZone, DateTimeOffset.MinValue.AddMinutes(1));
                return DateTimeOffset.Compare(result, DateTimeOffset.MinValue.AddMinutes(1)) == 0
                           ? (DateTimeOffset?) null
                           : result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj" /> into a <see cref="string" />.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to be converted.</param>
        /// <returns>
        ///     A <see cref="string" />converted from <paramref name="obj" />, if <paramref name="obj" /> is null then
        ///     <see cref="string.Empty" /> gets returned.
        /// </returns>
        public static string ToStringNotNull(this object obj)
        {
            return obj == null ? string.Empty : Convert.ToString(obj).TrimIfNotNull();
        }

        /// <summary>
        ///     Converts the <paramref name="obj" /> into a <see langword="null" /> string if his representation is null, empty
        ///     consists only of white-space characters.
        /// </summary>
        /// <param name="obj">The object to be converted.</param>
        /// <returns>
        ///     <see langword="null" /> string case <paramref name="obj" />
        ///     string representation is null, empty consists only of white-space characters,
        ///     trimmed <paramref name="obj" /> string representation is null otherwise
        /// </returns>
        public static string ToNullStringIfEmptyOrWhiteSpaced(this object obj)
        {
            return obj == null ? null : Convert.ToString(obj).ToNullIfEmptyOrWhiteSpaced();
        }

        /// <summary>
        ///     Converts the <paramref name="obj" /> into a <see cref="decimal" />.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">
        ///     the value to be use if <paramref name="obj" /> cannot be converted into a
        ///     <see cref="Guid" /> type.
        /// </param>
        /// <returns>
        ///     A <see cref="Guid" />converted from <paramref name="obj" />, or the <paramref name="defaultValue" /> case the conversion fails.
        /// </returns>
        public static Guid ToGuid(this object obj, Guid defaultValue = default(Guid))
        {
            if (obj.IsNumber())
            {
                return new Guid(obj.ToString().PadLeft(32, '0'));
            }
            try
            {
                return obj == null ? Guid.Empty : new Guid(Convert.ToString(obj));
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public static bool IsNumber(this object value)
        {
            return value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong
                   || value is float
                   || value is double
                   || value is decimal;
        }
    }
}
