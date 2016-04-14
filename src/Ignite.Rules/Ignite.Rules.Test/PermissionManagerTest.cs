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
        [TestCase("AuthenticatedNonAttendee", false)]
        [TestCase("AttendeeExternalCustomerAndPartner", true)]
        [TestCase("AttendeeSponsor", true)]
        [TestCase("AttendeeExhibitor", true)]
        [TestCase("AttendeeMicrosoft", true)]
        [TestCase("AttendeeStudent", true)]
        [TestCase("AttendeeFaculty", true)]
        [TestCase("AttendeeVIP", true)]
        [TestCase("AttendeeVIPMicrosoft", true)]
        [TestCase("ExternalCustomerAndPartner", true)]
        [TestCase("Microsoft", true)]
        [TestCase("Sponsor", true)]
        [TestCase("Exhibitor", true)]
        [TestCase("SpeakerExternalCustomerPartnerHOTEL", true)]
        [TestCase("SpeakerExternalCustomerPartnerNOHOTEL", true)]
        [TestCase("SpeakerandExpertMicrosoftNOHOTEL", true)]
        [TestCase("SpeakerMicrosoftNOHOTEL", true)]
        [TestCase("Press", true)]
        [TestCase("Crew", true)]
        [TestCase("ExpoOnly", true)]
        [TestCase("DayPassAttendee", true)]
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
        [TestCase("AuthenticatedNonAttendee", true, false, false, false)]
        [TestCase("AttendeeExternalCustomerAndPartner", true, true, false, false)]
        [TestCase("AttendeeSponsor", true, true, false, false)]
        [TestCase("AttendeeExhibitor", true, true, false, false)]
        [TestCase("AttendeeMicrosoft", true, true, false, false)]
        [TestCase("AttendeeStudent", true, true, false, false)]
        [TestCase("AttendeeFaculty", true, true, false, false)]
        [TestCase("AttendeeVIP", false, true, true, false)]
        [TestCase("AttendeeVIPMicrosoft", true, true, false, false)]
        [TestCase("ExternalCustomerAndPartner", true, true, false, false)]
        [TestCase("Microsoft", true, true, false, false)]
        [TestCase("Sponsor", true, true, false, false)]
        [TestCase("Exhibitor", true, true, false, false)]
        [TestCase("SpeakerExternalCustomerPartnerHOTEL", true, true, false, false)]
        [TestCase("SpeakerExternalCustomerPartnerNOHOTEL", true, true, false, false)]
        [TestCase("SpeakerandExpertMicrosoftNOHOTEL", true, true, false, false)]
        [TestCase("SpeakerMicrosoftNOHOTEL", true, true, false, false)]
        [TestCase("Press", false, false, false, true)]
        [TestCase("Crew", true, true, false, false)]
        [TestCase("ExpoOnly", true, true, false, false)]
        [TestCase("DayPassAttendee", true, true, false, false)]
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
            var tlf = "AttendeeVIP";
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