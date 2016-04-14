using System;
using System.Collections.Generic;
using Ignite.Rules.Dto;

namespace Ignite.Rules
{
    public class PermissionManager
    {
        private readonly Lazy<Dictionary<string, ProfileDto>> _permission;

        public PermissionManager(UseraccessDto access)
        {
            _permission = new Lazy<Dictionary<string, ProfileDto>>(() => Build(access));
        }

        private static Dictionary<string, ProfileDto> Build(UseraccessDto access)
        {
            var dict = new Dictionary<string, ProfileDto>
            {
                { nameof(access.NonRegistered.Anonymous).ToLower(),  access.NonRegistered.Anonymous},
                { nameof(access.NonRegistered.AuthenticatedNonAttendee).ToLower(),  access.NonRegistered.AuthenticatedNonAttendee},
                { nameof(access.ConferenceAttendee.AttendeeExhibitor).ToLower(),  access.ConferenceAttendee.AttendeeExhibitor},
                { nameof(access.ConferenceAttendee.AttendeeExternalCustomerAndPartner).ToLower(),  access.ConferenceAttendee.AttendeeExternalCustomerAndPartner},
                { nameof(access.ConferenceAttendee.AttendeeFaculty).ToLower(),  access.ConferenceAttendee.AttendeeFaculty},
                { nameof(access.ConferenceAttendee.AttendeeMicrosoft).ToLower(),  access.ConferenceAttendee.AttendeeMicrosoft},
                { nameof(access.ConferenceAttendee.AttendeeSponsor).ToLower(),  access.ConferenceAttendee.AttendeeSponsor},
                { nameof(access.ConferenceAttendee.AttendeeStudent).ToLower(),  access.ConferenceAttendee.AttendeeStudent},
                { nameof(access.ConferenceAttendee.AttendeeVIP).ToLower(),  access.ConferenceAttendee.AttendeeVIP},
                { nameof(access.ConferenceAttendee.AttendeeVIPMicrosoft).ToLower(),  access.ConferenceAttendee.AttendeeVIPMicrosoft},
                { nameof(access.Crew.Crew).ToLower(),  access.Crew.Crew},
                { nameof(access.DayPass.DayPassAttendee).ToLower(),  access.DayPass.DayPassAttendee},
                { nameof(access.ExpoOnly.ExpoOnly).ToLower(),  access.ExpoOnly.ExpoOnly},
                { nameof(access.Press.Press).ToLower(),  access.Press.Press},
                { nameof(access.Speaker.SpeakerExternalCustomerPartnerHOTEL).ToLower(),  access.Speaker.SpeakerExternalCustomerPartnerHOTEL},
                { nameof(access.Speaker.SpeakerExternalCustomerPartnerNOHOTEL).ToLower(),  access.Speaker.SpeakerExternalCustomerPartnerNOHOTEL},
                { nameof(access.Speaker.SpeakerMicrosoftNOHOTEL).ToLower(),  access.Speaker.SpeakerMicrosoftNOHOTEL},
                { nameof(access.Speaker.SpeakerandExpertMicrosoftNOHOTEL).ToLower(),  access.Speaker.SpeakerandExpertMicrosoftNOHOTEL},
                { nameof(access.Staff.ExternalCustomerAndPartner).ToLower(),  access.Staff.ExternalCustomerAndPartner},
                { nameof(access.Staff.Microsoft).ToLower(),  access.Staff.Microsoft},
                { nameof(access.BoothStaff.Exhibitor).ToLower(),  access.BoothStaff.Exhibitor},
                { nameof(access.BoothStaff.Sponsor).ToLower(),  access.BoothStaff.Sponsor},
            };
            return dict;
        }

        public bool CanAccessIgnite(string userType)
        {
            var profile = LookupUserProfile(userType);
            return profile.Access.MyIgnite;
        }

        private ProfileDto LookupUserProfile(string userType)
        {
            if (string.IsNullOrEmpty(userType))
            {
                throw new ArgumentException("Unable to lookup empty user type");
            }

            var key = userType.ToLower();
            if (!_permission.Value.ContainsKey(key))
            {
                throw new InvalidOperationException($"user type {userType} not found");
            }
            return _permission.Value[key];
        }

        public SessionsetAccess LookupSessionSetAccess(string userType, DateTimeOffset now)
        {
            var profile = LookupUserProfile(userType);
            var sessionSetAccessList = profile.SessionSetAccess;

            foreach (var access in sessionSetAccessList)
            {
                if ((access.ApplicableFrom <= now) && (access.ApplicableTo > now))
                {
                    return new SessionsetAccessBuilder().Build(access);
                }
            }
            
            throw  new InvalidOperationException($"Unable to find access set for '{now.ToString("s")}' and userType {userType}.");
        }
    }
}