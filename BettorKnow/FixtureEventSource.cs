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
            public const EventKeywords DataBase = (EventKeywords)2;
            public const EventKeywords Diagnostic = (EventKeywords)4;
            public const EventKeywords Perf = (EventKeywords)8;
        }

        private static readonly Lazy<FixtureEventSource> Instance = new Lazy<FixtureEventSource>(() => new FixtureEventSource());
        private FixtureEventSource() { }
        public static FixtureEventSource Log { get { return Instance.Value; } }


        public class Tasks
        {
            public const EventTask Fixtures = (EventTask)1;
        }

        [Event(1, Message = "Application Failure: {0}", Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
        internal void Failure(string message)
        {
            WriteEvent(1, message);
        }

        [Event(2, Message = "loading page {1} activity={0}", Opcode = EventOpcode.Start, Task = Tasks.Fixtures, Keywords = Keywords.Page, Level = EventLevel.Informational)]
        internal void GetFixtures(string request, string page)
        {
            if (IsEnabled()) WriteEvent(2, request, page);
        }


    }
}