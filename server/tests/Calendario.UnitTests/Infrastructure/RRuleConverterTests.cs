using NUnit.Framework;
using Calendario.Core.Dates.Reccurent;
using Calendario.Infrastructure;
using System;

namespace Calendario.UnitTests.Infrastructure
{
    public class RRuleConverterTests
    {

        [Test]
        public void SimpleSerialization_Success()
        {
            var rrule = new ReccurenceRule(FrequencyType.Yearly, until: new DateTime(2020, 05, 01));
            var str = RRuleConverter.Serialize(rrule);
            Assert.AreEqual("FREQ=YEARLY;UNTIL=20200501T000000", str);
        }

        [Test]
        public void SimpleDeserialization_Success()
        {
            var rrule = new ReccurenceRule(FrequencyType.Yearly, until: new DateTime(2020, 05, 01));
            var deserialized = RRuleConverter.Deserialize("FREQ=YEARLY;UNTIL=20200501T000000");
            Assert.AreEqual(new { rrule.Frequency, rrule.Until }, new { deserialized.Frequency, deserialized.Until });
        }
    }
}