using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;

namespace BettorKnow
{
    [EventSource(Name = "BettorKnow-Web-Fixtures")]
    public class FixtureEventSource : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Page = (EventKeywords)1;
            public const EventKeywords Diagnostic = (EventKeywords)4;
            public const EventKeywords Perf = (EventKeywords)8;
        }

        private static readonly Lazy<FixtureEventSource> Instance = new Lazy<FixtureEventSource>(() => new FixtureEventSource());
        private FixtureEventSource() { }
        public static FixtureEventSource Log { get { return Instance.Value; } }


        public class Tasks
        {
            public const EventTask AllFixtures = (EventTask)1;
        }

        [Event(1, Message = "Application Failure: {0}", Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
        internal void Failure(string message)
        {
            WriteEvent(1, message);
        }

        [Event(2, Message = "loading page GetFixtures, request={0}", Opcode = EventOpcode.Start, Task = Tasks.AllFixtures, Keywords = Keywords.Page, Level = EventLevel.Informational)]
        internal void GetFixtures(string request)
        {
            if (IsEnabled()) WriteEvent(2, request);
        }


    }
}