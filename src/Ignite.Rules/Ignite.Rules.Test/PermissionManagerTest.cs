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
    }
}