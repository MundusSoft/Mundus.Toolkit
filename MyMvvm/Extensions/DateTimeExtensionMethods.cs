using System;

namespace MyMvvm
{
    public static class DateTimeExtensionMethods
    {
        /// <summary>
        ///     Gets the totals years for a time span.
        /// </summary>
        /// <param name="timeSpan">The time span to get the total years.</param>
        /// <returns>The total years of the time span.</returns>
        public static int TotalYears(this TimeSpan timeSpan)
        {
            return DateTime.MinValue.AddSeconds(timeSpan.TotalSeconds).Year - 1;
        }

        /// <summary>
        ///     Changes the kind of to local.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime ChangeToLocalKind(this DateTime date)
        {
            return date.Kind != DateTimeKind.Local ? DateTime.SpecifyKind(date, DateTimeKind.Local) : date;
        }

        /// <summary>
        ///     Changes the kind of to UTC.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime ChangeToUtcKind(this DateTime date)
        {
            return date.Kind != DateTimeKind.Utc ? DateTime.SpecifyKind(date, DateTimeKind.Utc) : date;
        }

        /// <summary>
        ///     Changes the kind of to unspecified.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime ChangeToUnspecifiedKind(this DateTime date)
        {
            return date.Kind != DateTimeKind.Unspecified ? DateTime.SpecifyKind(date, DateTimeKind.Unspecified) : date;
        }

        /// <summary>
        ///     Changes the kind of to local.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime? ChangeToLocalKind(this DateTime? date)
        {
            return date?.ChangeToLocalKind();
        }

        /// <summary>
        ///     Changes the kind of to UTC.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime? ChangeToUtcKind(this DateTime? date)
        {
            return date?.ChangeToUtcKind();
        }

        /// <summary>
        ///     Changes the kind of to unspecified.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime? ChangeToUnspecifiedKind(this DateTime? date)
        {
            return date?.ChangeToUnspecifiedKind();
        }
    }
}
