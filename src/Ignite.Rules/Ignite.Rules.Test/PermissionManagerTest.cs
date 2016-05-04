using System;
using NUnit.Framework;

namespace Ignite.Rules.Test
{
    [TestFixture]
    public class PermissionManagerTest
    {
        private PermissionManager _permissionManager;

        [TestFixtureSetUp]
        public void Setup()
        {
            var webLoader = new WebLoader();
            var settings = new SettingsStub();
            var repos = new SecurityRepository(webLoader, settings);
            var useraccessDto = repos.LoadAccessLevel().Result;
            _permissionManager = new PermissionManager(useraccessDto);
        }

        [Test]
        [TestCase("non existanct", false, ExpectedException = typeof(InvalidOperationException))]
        [TestCase("empty", false, ExpectedException = typeof(InvalidOperationException))]
        [TestCase(null, false, ExpectedException = typeof(ArgumentException))]
        [TestCase("", false, ExpectedException = typeof(ArgumentException))]
        [TestCase("Anonymous", false)]
        [TestCase("Attendee Customer & Partner", true)]
        [TestCase("Attendee Exhibitor", true)]
        [TestCase("Attendee Sponsor", true)]
        [TestCase("Attendee Faculty/Staff", true)]
        [TestCase("Attendee Student", true)]
        [TestCase("Attendee Microsoft", true)]
        [TestCase("Attendee TEF", true)]
        [TestCase("Attendee TEF Microsoft", true)]
        [TestCase("Media", true)]
        [TestCase("Day Pass Attendee Customer & Partner", true)]
        [TestCase("Expo Only", true)]
        [TestCase("Speaker External", true)]
        [TestCase("Speaker Microsoft", true)]
        [TestCase("Booth Staff Exhibitor", true)]
        [TestCase("Booth Staff Sponsor", true)]
        [TestCase("Booth Staff Microsoft", true)]
        [TestCase("Staff External", true)]
        [TestCase("Staff Microsoft", true)]
        public void CanAccessMyIgnite(string userType, bool expected)
        {
            Assert.That(_permissionManager.CanAccessIgnite(userType),  Is.EqualTo(expected));

            if (userType != null)
            {
                Assert.That(_permissionManager.CanAccessIgnite(userType.ToLower()), Is.EqualTo(expected));
                Assert.That(_permissionManager.CanAccessIgnite(userType.ToUpper()), Is.EqualTo(expected));
            }
        }

        [Test]
        [TestCase("non existanct", false, false, false, false,  ExpectedException = typeof(InvalidOperationException))]
        [TestCase("empty", false, false, false, false, ExpectedException = typeof(InvalidOperationException))]
        [TestCase(null, false, false, false, false, ExpectedException = typeof(ArgumentException))]
        [TestCase("", false, false, false, false, ExpectedException = typeof(ArgumentException))]
        [TestCase("Anonymous", true, false, false, false)]
        [TestCase("Attendee Customer & Partner", true, true, false, false)]
        [TestCase("Attendee Exhibitor", true, true, false, false)]
        [TestCase("Attendee Sponsor", true, true, false, false)]
        [TestCase("Attendee Faculty/Staff", true, true, false, false)]
        [TestCase("Attendee Student", true, true, false, false)]
        [TestCase("Attendee Microsoft", true, true, false, false)]
        [TestCase("Attendee TEF", false, true, true, false)]
        [TestCase("Attendee TEF Microsoft", false, true, true, false)]
        [TestCase("Media", false, false, false, true)]
        [TestCase("Day Pass Attendee Customer & Partner", true, true, false, false)]
        [TestCase("Expo Only", true, true, false, false)]
        [TestCase("Speaker External", true, true, false, false)]
        [TestCase("Speaker Microsoft", true, true, false, false)]
        [TestCase("Booth Staff Exhibitor", true, true, false, false)]
        [TestCase("Booth Staff Sponsor", true, true, false, false)]
        [TestCase("Booth Staff Microsoft", true, true, false, false)]
        [TestCase("Staff External", true, true, false, false)]
        [TestCase("Staff Microsoft", true, true, false, false)]
        [TestCase("Crew", true, true, false, false)]
        public void LookupSessionSetAccessLevelAtStartOfConference(string userType, bool schedulerStandard, bool lab, bool schedulerPressTlf, bool schedulerPress)
        {
            var startOfConference = new DateTimeOffset(new DateTime(2016, 9, 26));
            var access = _permissionManager.LookupSessionSetAccess(userType, startOfConference);
            AssertAccessIsExpected(schedulerStandard, schedulerPressTlf, lab, schedulerPress, access);
            if (userType != null)
            {
                var lowerAccess = _permissionManager.LookupSessionSetAccess(userType.ToLower(), startOfConference);
                var uppAccess = _permissionManager.LookupSessionSetAccess(userType.ToUpper(), startOfConference);
                AssertAccessIsExpected(schedulerStandard, schedulerPressTlf, lab, schedulerPress, lowerAccess);
                AssertAccessIsExpected(schedulerStandard, schedulerPressTlf, lab, schedulerPress, uppAccess);
            }
        }

        [Test]
        public void LookupSessionSetAccessLevelConditionalOnDateReturnsExpected()
        {
            var tlf = "Attendee TEF";
            var monday = new DateTimeOffset(new DateTime(2016, 9, 26));
            var tuesday = monday.AddDays(1);
            var wednesday = tuesday.AddDays(1);
            var thursday = wednesday.AddDays(1);
            var friday = thursday.AddDays(1);

            var accessMonday = _permissionManager.LookupSessionSetAccess(tlf, monday);
            var accessTuesday = _permissionManager.LookupSessionSetAccess(tlf, tuesday);
            var accessWednesday = _permissionManager.LookupSessionSetAccess(tlf, wednesday);
            var accessThursday = _permissionManager.LookupSessionSetAccess(tlf, thursday);
            var accessFriday = _permissionManager.LookupSessionSetAccess(tlf, friday);

            AssertAccessIsExpected(false, true, true, false, accessMonday);
            AssertAccessIsExpected(true, true, true, false, accessTuesday);
            AssertAccessIsExpected(true, true, true, false, accessWednesday);
            AssertAccessIsExpected(true, true, true, false, accessThursday);
            AssertAccessIsExpected(true, true, true, false, accessFriday);   
        }

        private static void AssertAccessIsExpected(bool schedulerStandard, bool schedulerPressTlf, bool lab, 
            bool schedulerPress, SessionsetAccess access)
        {
            Assert.That(access.SchedulerStandard, Is.EqualTo(schedulerStandard));
            Assert.That(access.SchedulerPress, Is.EqualTo(schedulerPress));
            Assert.That(access.SchedulerTLF, Is.EqualTo(schedulerPressTlf));
            Assert.That(access.Lab, Is.EqualTo(lab));
        }
    }
}