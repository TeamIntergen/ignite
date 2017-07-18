[![Build Status](https://travis-ci.org/intergenignite/ignite.svg?branch=master)](https://travis-ci.org/intergenignite/ignite)

# Ignite Rules
Rules for Ignite platform

# Changes since original.
 - Added ability to have session sets with applicable from and applicable to dates. (DS - 2016-04-14)
 - Flattened out the JSON file, updated the attendee types and based on whats been approved. (DS - 2016-05-04)
 - Session set access is now a list of identifiers and dates to assist with looking up permissions (DS 2016-05-09)
 - Session set access and assignment to session types re-worked to adhere to the requirements from Jen (GB - 2016-07-26)
 - Added a Lab session set to allow for unbounded access to these sessions for all users (GB - 2016-07-26)
 - Changed Staff External Customer & Partner to Staff External (GB - 2016-08-29)

 ## How to use these files

 There are a collection of rules file for each environment that is being managed. There is a folder per set of files under ```/resource```

 The session\_length\_map.json file exits to provide a fall-back for the lenght of a session (determined by it's type) while the session has no assigned timeslot.

 The central rules are enclosed in the rules.json and session\_map.json files.
 The rules in these files are used to determine access to sessions, users, evaluations and system features.

 The rules.json file is simply array of UserAccess objects:
 ```
 {
    "Identifiers": ["Attendee Customer & Partner", "Attendee Microsoft", "Attendee Exhibitor"],
    "Access": {
        "SessionCatalog": true,
        "MyIgnite": true,
        "ScheduleBuilder": true,
        "MobileApp": true,
        "MeetingScheduler": true,
        "MobileAppMessaging": true,
        "LocationBasedMessaging": true
    },
    "VisibleAttendeeTypes": [
        "Attendee Customer & Partner",
        "Attendee Exhibitor",
        "Attendee Microsoft",
        ...
    ],
    "Reporting": {
        "SessionCounts": true
    },
    "EvalAccess": [{
        "Identifier": "PostEventSurvey",
        "SessionTypes": [null],
        "Mobile": true,
        "WebSite": true
    },{
        "Identifier": "RegularSessionEvaluations",
        "SessionTypes": [ "General Session", "Breakout - 45 minute", "Breakout - 75 minute", "Partner Led Session", "Women in Technology", "Theater: Community", "Theater: Microsoft", "Theater: Partner", "Pre-Day Training"],
        "Mobile": true,
        "WebSite": true
    },{
        "Identifier": "KeynoteAfternoonEvaluation",
        "SessionTypes": ["Afternon Keynote"],
        "Mobile": true,
        "WebSite": true
    }],
    "SessionSetAccess": [{
        "ApplicableFrom": null,
        "ApplicableTo": null,
        "Identifier": "Schedule Builder - Attendee"
    },{
        "ApplicableFrom": "2016-09-25T04:00:00Z",
        "ApplicableTo": "2016-09-28T04:00:00Z",
        "Identifier": "Schedule Builder - TEF"
    }]
}
 ```
 To find the UserAccess object that applies to a given user use the RegistrantType for the user and find the object which has an match within the array of Identifiers. If no match is found then the UserAccess object for the Anonymous RegistrantType should be used.

 The Access sub-object is used to determine which features the user can access based upon their boolean flags.

 The VisibleAttendeeTypes array is used to determine which other attendeess are visible in the Attendee Networking tools.

 Reporting can safely be ignored

 EvalAccess is used to control visibility of Evaluations and map the Evaluation Question Set to the Session Types that they should be used for. It's worth noting that the Post Event Evaluation is not mapped to a session type as it hangs off the Event on the Hubb API.

 SessionSetAccess controls visibility of sessions according to their type for a user.
 Access to a session set can also be restricted by time. A null value for the ApplicableTo/From should be considered as DateTime.Min or DateTime.Max
 If a session set has a start or end set then only the sessions that have no time or a start and end time which falls within the range should be shown to the user.

 The session sets which given session are part of are determined by the session\_map.json file. This contains an array of objects which map the session type to it's set by name or id.