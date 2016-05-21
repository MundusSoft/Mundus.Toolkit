using System;

namespace MyMvvm
{
    /// <summary>
    /// Static class responsible for holding the extension methods for <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensionMethods
    {

        /// <summary>
        ///     Converts from <see cref="Nullable{T}"/> into <see cref="int"/>
        /// </summary>
        /// <param name="intObject">The int object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The <see cref="int"/> value or <paramref name="defaultValue"/> if <paramref name="intObject"/> is null.</returns>
        public static int GetInt(this int? intObject, int defaultValue)
        {
            try
            {
                return intObject ?? defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj"/> into a <see cref="Boolean"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="Boolean"/>converted from <paramref name="obj"/>, or the <c>default(bool)</c> case the conversion throws an exception.</returns>
        public static bool ToBoolean(this object obj)
        {
            try
            {
                return obj != null && Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return default(bool);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj"/> into a <see cref="int"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="int"/>converted from <paramref name="obj"/>, or the <c>default(int)</c> case the conversion throws an exception.</returns>
        public static int ToInt(this object obj)
        {
            try
            {
                return obj == null ? default(int) : Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return default(int);
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj"/> into a <see cref="short"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="short"/>converted from <paramref name="obj"/>, or the <c>default(short)</c> case the conversion throws an exception.</returns>
        public static short ToShort(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="long"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="long"/>converted from <paramref name="obj"/>, or the <c>default(long)</c> case the conversion throws an exception.</returns>
        public static long ToLong(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="float"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="float"/>converted from <paramref name="obj"/>, or the <c>default(float)</c> case the conversion throws an exception.</returns>
        public static float ToFloat(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="double"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="double"/>converted from <paramref name="obj"/>, or the <c>default(double)</c> case the conversion throws an exception.</returns>
        public static double ToDouble(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="byte"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="byte"/>converted from <paramref name="obj"/>, or the <c>default(byte)</c> case the conversion throws an exception.</returns>
        public static byte ToByte(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="byte"/> array.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="byte"/>array converted from <paramref name="obj"/>, or the <c>default(int)</c> case the conversion throws an exception.</returns>
        public static byte[] ToByteArray(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="char"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="char"/>converted from <paramref name="obj"/>, or the <c>default(char)</c> case the conversion throws an exception.</returns>
        public static char ToChar(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="sbyte"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="sbyte"/>converted from <paramref name="obj"/>, or the <c>default(sbyte)</c> case the conversion throws an exception.</returns>
        public static double ToSByte(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="ushort"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="ushort"/>converted from <paramref name="obj"/>, or the <c>default(ushort)</c> case the conversion throws an exception.</returns>
        public static ushort ToUShort(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="uint"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="uint"/>converted from <paramref name="obj"/>, or the <c>default(uint)</c> case the conversion throws an exception.</returns>
        public static uint ToUInt(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="ulong"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="ulong"/>converted from <paramref name="obj"/>, or the <c>default(ulong)</c> case the conversion throws an exception.</returns>
        public static ulong ToULong(this object obj)
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
        ///     Converts <paramref name="obj"/> into a <see cref="DateTime"/>.
        /// <para>If <paramref name="obj"/> is a <see cref="TimeSpan"/> then the <see cref="DateTime"/> returned will be the current date plus the <paramref name="obj"/></para>
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="DateTime"/>converted from <paramref name="obj"/>, or the <see cref="DateTime.MinValue"/> case the conversion to <see cref="DateTime"/> isn't possible.</returns>
        public static DateTime ToDateTime(this object obj)
        {
            try
            {
                if (obj is TimeSpan)
                {
                    obj = DateTime.Now.Date + (TimeSpan)obj;
                }
                var temp = obj as DateTime?;
                return DateTime.SpecifyKind(temp ?? DateTime.MinValue,
                                            DateTimeKind.Unspecified);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        ///     Converts <paramref name="obj"/> into a <see cref="Nullable{DateTime}"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="Nullable{DateTime}"/>converted from <paramref name="obj"/>.</returns>
        public static DateTime? ToDateTimeNullable(this object obj)
        {
            if (obj is TimeSpan)
            {
                obj = DateTime.Now.Date + (TimeSpan)obj;
            }
            var temp = obj as DateTime?;
            return temp.HasValue ? DateTime.SpecifyKind(temp.Value, DateTimeKind.Unspecified) : temp;
        }

        /// <summary>
        ///     Converts <paramref name="obj"/> into a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="DateTimeOffset"/>converted from <paramref name="obj"/>, or the <see cref="DateTimeOffset.MinValue"/> case the conversion to <see cref="DateTimeOffset"/> isn't possible.</returns>
        public static DateTimeOffset ToDateTimeOffset(this object obj)
        {
            var date = obj as DateTimeOffset?;
            return date ?? DateTimeOffset.MinValue;
        }

        /// <summary>
        ///     Converts <paramref name="obj"/> into a <see cref="Nullable{DateTimeOffset}"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="Nullable{DateTimeOffset}"/>converted from <paramref name="obj"/>.</returns>
        public static DateTimeOffset? ToDateTimeOffsetNullable(this object obj)
        {
            return obj as DateTimeOffset?;
        }

        /// <summary>
        ///     Converts <paramref name="obj"/> into a <see cref="string"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A <see cref="string"/>converted from <paramref name="obj"/>, if <paramref name="obj"/> is null then <see cref="string.Empty"/> gets returned.</returns>
        public static string ToStringNotNull(this object obj)
        {
            return obj == null ? string.Empty : Convert.ToString(obj).TrimIfNotNull();
        }

        /// <summary>
        ///     Converts <paramref name="obj"/> into a instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">the type of the instance that will be return.</typeparam>
        /// <param name="obj">The <see cref="Object"/> to be converted.</param>
        /// <returns>A instance of <typeparamref name="T"/></returns>
        public static T ToInstanceOfType<T>(this object obj)
            where T : class
        {
            return obj as T;
        }
    }
}
