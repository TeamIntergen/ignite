using System;
using System.Collections.Generic;
using System.Linq;
using Ignite.Rules.Dto;

namespace Ignite.Rules
{
    public class PermissionManager
    {
        private readonly Lazy<Dictionary<string, ProfileDto>> _permission;

        public PermissionManager(RulesDto rules)
        {
            rules.UserAccess = new List<ProfileDto>(
            rules
            .UserAccess
            .Where(access => access.Identifiers != null && access.Identifiers.Length > 0)
            .SelectMany(access => access.Identifiers.Select(identifier => new ProfileDto
            {
                Identifier = identifier,
                Identifiers = access.Identifiers ?? new string[] {},
                Access = access.Access,
                EvalAccess = access.EvalAccess,
                Reporting = access.Reporting,
                SessionSetAccess = access.SessionSetAccess,
                VisibleAttendeeTypes = access.VisibleAttendeeTypes
            }))
            .Union(rules.UserAccess.Where(access => !string.IsNullOrEmpty(access.Identifier))));
            _permission = new Lazy<Dictionary<string, ProfileDto>>(() => rules.UserAccess.ToDictionary(rule => rule.Identifier.ToLowerInvariant()));
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

        public IEnumerable<SessionsetAccess> LookupSessionSetAccess(string userType, DateTimeOffset now)
        {
            var profile = LookupUserProfile(userType);
            var sessionSetAccessList = profile.SessionSetAccess;

            foreach (var access in sessionSetAccessList)
            {
                if ((access.ApplicableFrom <= now) && (access.ApplicableTo > now))
                {
                    yield return new SessionsetAccessBuilder().Build(access);
                }
            }
        }
    }
}