using System;
using System.Collections.Generic;
using Calendario.Core.Dates.Utils;

namespace Calendario.Core.Dates.Reccurent
{
    /// <summary>
    /// Calendario representation of the <c>RRULE</c> property.
    /// https://tools.ietf.org/html/rfc5545#section-3.3.10
    /// </summary>
    public record ReccurenceRule : Calendario.Core.Base.ValueObject
    {
        public FrequencyType Frequency { get; }
        public DateTime? Until { get; }
        /// <summary> Defines the number of occurrences </summary>
        public int? Count { get; }
        /// <summary> At which intervals repeats </summary>
        public int Interval { get; } = 1;
        /// <summary> Associated seconds of a minute. Valid values are 0-59</summary>
        [InRange(0, 59)]
        public IEnumerable<int> BySecond { get; } = new List<int>();
        /// <summary> Associated minutes of an hour. Valid values are 0-59</summary>
        [InRange(0, 59)]
        public IEnumerable<int> ByMinute { get; } = new List<int>();
        /// <summary> Associated hours of an day. Valid values are 0-23</summary>
        [InRange(0, 23)]
        public IEnumerable<int> ByHour { get; } = new List<int>();
        /// <summary> Associated days of an week. Valid values are 0-59</summary>
        [InRange(0, 6)]
        public IEnumerable<int> ByWeekDay { get; } = new List<int>();
        /// <summary> Associated days of an month. Valid values are 1-31</summary>
        [InRange(1, 31)]
        public IEnumerable<int> ByMonthDay { get; } = new List<int>();
        /// <summary> The ordinal days of a year. Valid values are 1-366</summary>
        [InRange(1, 366)]
        public IEnumerable<int> ByYearDay { get; } = new List<int>();
        /// <summary> The ordinal weeks of a year. Valid values are -53-53. Negative values count backwards from the end of the specified year</summary>
        [InRange(-53, 53)]
        public IEnumerable<int> ByWeekNo { get; } = new List<int>();
        /// <summary> Associated month of an year. Valid values are 1-12</summary>
        [InRange(1, 12)]
        public IEnumerable<int> ByMonth { get; } = new List<int>();
        /// <summary>"Indexes" into the result set that you should keep </summary>
        public IEnumerable<int> BySetPosition { get; } = new List<int>();

        [InRange(0, 6)]
        public int FirstDayOfWeek { get; } = 1;
        public ReccurenceRule(
            FrequencyType frequency,
            DateTime? until = null,
            int? count = null,
            int? interval = null,
            IEnumerable<int> bySecond = null,
            IEnumerable<int> byMinute = null,
            IEnumerable<int> byHour = null,
            IEnumerable<int> byWeekDay = null,
            IEnumerable<int> byMonthDay = null,
            IEnumerable<int> byYearDay = null,
            IEnumerable<int> byWeekNo = null,
            IEnumerable<int> byMonth = null,
            IEnumerable<int> bySetPosition = null,
            int? firstDayOfWeek = null)
        {
            Frequency = frequency;
            Until = until;
            Count = count ?? Count;
            Interval = interval ?? Interval;
            BySecond = bySecond ?? BySecond;
            ByMinute = byMinute ?? ByMinute;
            ByHour = byHour ?? ByHour;
            ByWeekDay = byWeekDay ?? ByWeekDay;
            ByMonthDay = byMonthDay ?? ByMonthDay;
            ByYearDay = byYearDay ?? ByYearDay;
            ByWeekNo = byWeekNo ?? ByWeekNo;
            ByMonth = byMonth ?? ByMonth;
            BySetPosition = bySetPosition ?? BySetPosition;
            FirstDayOfWeek = firstDayOfWeek ?? FirstDayOfWeek;
            this.ValidateProperties();
        }
    }

    public enum FrequencyType
    {
        None,
        Secondly,
        Minutely,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}