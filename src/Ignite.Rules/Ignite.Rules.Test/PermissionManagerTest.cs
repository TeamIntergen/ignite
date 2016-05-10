using System;
using System.Collections.Generic;
using System.Linq;
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
        [TestCase("non existanct", new string[]{},  ExpectedException = typeof(InvalidOperationException))]
        [TestCase("empty", new string[] { }, ExpectedException = typeof(InvalidOperationException))]
        [TestCase(null, new string[] { }, ExpectedException = typeof(ArgumentException))]
        [TestCase("", new string[] { }, ExpectedException = typeof(ArgumentException))]
        [TestCase("Anonymous", new [] { "Scheduler-Standard" })]
        [TestCase("Attendee Customer & Partner", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Attendee Exhibitor", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Attendee Sponsor", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Attendee Faculty/Staff", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Attendee Student", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Attendee Microsoft", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Attendee TEF", new[] { "Lab", "Scheduler-TEF" })]
        [TestCase("Attendee TEF Microsoft", new[] { "Lab", "Scheduler-TEF" })]
        [TestCase("Media", new[] { "Scheduler-Press" })]
        [TestCase("Day Pass Attendee Customer & Partner", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Expo Only", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Speaker External", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Speaker Microsoft", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Booth Staff Exhibitor", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Booth Staff Sponsor", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Booth Staff Microsoft", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Staff External", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Staff Microsoft", new[] { "Scheduler-Standard", "Lab" })]
        [TestCase("Crew", new[] { "Scheduler-Standard", "Lab" })]
        public void LookupSessionSetAccessLevelAtStartOfConference(string userType, string[] sessionSetAcess)
        {
            var startOfConference = new DateTimeOffset(new DateTime(2016, 9, 26));
            var access = _permissionManager.LookupSessionSetAccess(userType, startOfConference);
            IEnumerable<string> accessId = access.Select(s => s.Identifier).ToList();
            CollectionAssert.AreEquivalent(accessId, sessionSetAcess);
            if (userType != null)
            {
                var lowerAccess = _permissionManager.LookupSessionSetAccess(userType.ToLower(), startOfConference);
                var upAccess = _permissionManager.LookupSessionSetAccess(userType.ToUpper(), startOfConference);
                IEnumerable<string> lowerAccessList = lowerAccess.Select(s => s.Identifier).ToList();
                IEnumerable<string> upperAccessList = upAccess.Select(s => s.Identifier).ToList();
                CollectionAssert.AreEquivalent(lowerAccessList, sessionSetAcess);
                CollectionAssert.AreEquivalent(upperAccessList, sessionSetAcess);
            }
        }

        [Test]
        public void LookupSessionSetAccessLevelConditionalOnDateReturnsExpected()
        {
            var tlf = "Attendee TEF";
            var offsetToAtlanta = TimeSpan.FromHours(-5);
            var mondayInAtlanta = new DateTimeOffset(new DateTime(2016, 9, 26), offsetToAtlanta);
            var tuesday = mondayInAtlanta.AddDays(1);
            var wednesday = tuesday.AddDays(1);
            var thursday = wednesday.AddDays(1);
            var friday = thursday.AddDays(1);

            var accessMonday = _permissionManager.LookupSessionSetAccess(tlf, mondayInAtlanta);
            var accessTuesday = _permissionManager.LookupSessionSetAccess(tlf, tuesday);
            var accessWednesday = _permissionManager.LookupSessionSetAccess(tlf, wednesday);
            var accessThursday = _permissionManager.LookupSessionSetAccess(tlf, thursday);
            var accessFriday = _permissionManager.LookupSessionSetAccess(tlf, friday);

            var tefList = new[] { "Scheduler-TEF", "Lab" };
            var standardList = new[] { "Scheduler-TEF", "Scheduler-Standard", "Lab" };
            CollectionAssert.AreEquivalent(accessMonday.Select(s => s.Identifier), tefList);
            CollectionAssert.AreEquivalent(accessTuesday.Select(s => s.Identifier), standardList);
            CollectionAssert.AreEquivalent(accessWednesday.Select(s => s.Identifier), standardList);
            CollectionAssert.AreEquivalent(accessThursday.Select(s => s.Identifier), standardList);
            CollectionAssert.AreEquivalent(accessFriday.Select(s => s.Identifier), standardList);
        }
    }
}