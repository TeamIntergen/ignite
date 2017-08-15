using System;
using System.Collections.Generic;
using System.IO;
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
//            var webLoader = new WebLoader();
//            var settings = new SettingsStub();
//            var repos = new SecurityRepository(webLoader, settings);
            var fileLoader = new FileLoader();
            var settings = new SettingsStub()
                .WithRulesPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../../../resource/test/ignite_rules.json"))
                .WithSessionMapPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../../../resource/test/ignite_session_map.json"));
            var repos = new SecurityRepository(fileLoader, settings);

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
        [TestCase("Attendee Sponsor", true)]
        [TestCase("Attendee Exhibitor", true)]
        [TestCase("Attendee Microsoft",         true)]
        [TestCase("Attendee Student",           true)]
        [TestCase("Attendee Faculty/Staff",     true)]
        [TestCase("Attendee Data Science", true)]
        [TestCase("Microsoft Envision Attendee", true)]
        [TestCase("Microsoft Envision Attendee Microsoft",true)]
        [TestCase("Microsoft Envision Attendee Sponsor",  true)]
        [TestCase("Substituted Attendee",                 true)]
        [TestCase("Microsoft Envision Day Pass ",         true)]
        [TestCase("Speaker External",                     true)]
        [TestCase("Speaker Microsoft",                    true)]
        [TestCase("Speaker Day Pass External",            true)]
        [TestCase("Speaker Day Pass Microsoft",           true)]
        [TestCase("Microsoft Envision Speaker Customer",  true)]
        [TestCase("Microsoft Envision Speaker External",  true)]
        [TestCase("Microsoft Envision Speaker Internal",  true)]
        [TestCase("Media",                                true)]
        [TestCase("Microsoft Envision Media",             true)]
        [TestCase("Day Pass Attendee Customer & Partner", true)]
        [TestCase("Staff Day Pass External", true)]
        [TestCase("Staff Day Pass Microsoft", true)]
        [TestCase("Staff External", true)]
        [TestCase("Staff Microsoft", true)]
        [TestCase("Staff External - GA converted", true)]
        [TestCase("Staff External - Sponsor/Exhibitor converted", true)]
        [TestCase("Staff Microsoft - GA converted", true)]
        [TestCase("Crew", true)]
        [TestCase("Microsoft Envision Crew", true)]
        [TestCase("Microsoft Envision Experiences Staff External", true)]
        [TestCase("Microsoft Envision Experiences Staff Internal", true)]
        [TestCase("Microsoft Envision Staff Microsoft", true)]
        [TestCase("Booth Staff Sponsor", true)]
        [TestCase("Booth Staff Exhibitor", true)]
        [TestCase("Booth Staff Microsoft", true)]
        [TestCase("Expo Only",true)]
        public void CanAccessMyIgnite(string userType, bool expected)
        {
            Assert.That(_permissionManager.CanAccessIgnite(userType), Is.EqualTo(expected));

            if (userType != null)
            {
                Assert.That(_permissionManager.CanAccessIgnite(userType.ToLower()), Is.EqualTo(expected));
                Assert.That(_permissionManager.CanAccessIgnite(userType.ToUpper()), Is.EqualTo(expected));
            }
        }

        [Test]
        [TestCase("non existanct", new string[] { }, ExpectedException = typeof(InvalidOperationException))]
        [TestCase("empty", new string[] { }, ExpectedException = typeof(InvalidOperationException))]
        [TestCase(null, new string[] { }, ExpectedException = typeof(ArgumentException))]
        [TestCase("", new string[] { }, ExpectedException = typeof(ArgumentException))]
        [TestCase("Anonymous", new[] { "Session Catalog (Mktg Site)", "Lab" })]
        [TestCase("Attendee Customer & Partner", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Attendee Exhibitor", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Attendee Sponsor", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Attendee Faculty/Staff", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Attendee Student", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Attendee Microsoft", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Media", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Day Pass Attendee Customer & Partner", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Expo Only", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Speaker External", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Speaker Microsoft", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Booth Staff Exhibitor", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Booth Staff Sponsor", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Booth Staff Microsoft", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Staff External", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Staff Microsoft", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Staff External - GA converted", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Staff External - Sponsor/Exhibitor converted", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Staff Microsoft - GA converted", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        [TestCase("Crew", new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - Attendee" })]
        public void LookupSessionSetAccessLevelAtStartOfConference(string userType, string[] sessionSetAcess)
        {
            var startOfConference = new DateTimeOffset(2017, 9, 23, 0, 0, 0, new TimeSpan(-4, 0, 0));
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

//        [Test]
//        public void LookupSessionSetAccessLevelConditionalOnDateReturnsExpected()
//        {
//            var tlf = "Attendee TEF";
//            var offsetToAtlanta = TimeSpan.FromHours(-4);
//            var sundayInAtlanta = new DateTimeOffset(new DateTime(2016, 9, 25), offsetToAtlanta).ToUniversalTime();
//            var mondayInAtlanta = new DateTimeOffset(new DateTime(2016, 9, 26), offsetToAtlanta).ToUniversalTime();
//            var tuesday = mondayInAtlanta.AddDays(1);
//            var wednesday = tuesday.AddDays(1);
//            var thursday = wednesday.AddDays(1);
//            var friday = thursday.AddDays(1);
//
//            var accessSunday = _permissionManager.LookupSessionSetAccess(tlf, sundayInAtlanta);
//            var accessMonday = _permissionManager.LookupSessionSetAccess(tlf, mondayInAtlanta);
//            var accessTuesday = _permissionManager.LookupSessionSetAccess(tlf, tuesday);
//            var accessWednesday = _permissionManager.LookupSessionSetAccess(tlf, wednesday);
//            var accessThursday = _permissionManager.LookupSessionSetAccess(tlf, thursday);
//            var accessFriday = _permissionManager.LookupSessionSetAccess(tlf, friday);
//
//            var tefSundayList = new[] { "Schedule Builder - TEF", "Lab", "My Schedule - TEF" };
//            var tefList = new[] { "Schedule Builder - TEF", "Schedule Builder - TEF Extended", "Lab", "My Schedule - TEF" };
//            var standardList = new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - TEF" };
//            var fridayList = new[] { "Schedule Builder - Attendee", "Lab", "My Schedule - TEF" };
//            CollectionAssert.AreEquivalent(accessSunday.Select(s => s.Identifier), tefSundayList);
//            CollectionAssert.AreEquivalent(accessMonday.Select(s => s.Identifier), tefList);
//            CollectionAssert.AreEquivalent(accessTuesday.Select(s => s.Identifier), tefSundayList);
//            CollectionAssert.AreEquivalent(accessWednesday.Select(s => s.Identifier), standardList);
//            CollectionAssert.AreEquivalent(accessThursday.Select(s => s.Identifier), standardList);
//            CollectionAssert.AreEquivalent(accessFriday.Select(s => s.Identifier), fridayList);
//        }
    }
}