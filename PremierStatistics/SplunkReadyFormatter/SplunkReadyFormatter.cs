using System.IO;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;

namespace SplunkReadyFormatter
{
    public class SplunkReadyFormatter : IEventTextFormatter
    {
        public SplunkReadyFormatter(string prefix, string dateTimeFormat)
        {
            Prefix = prefix;
            DateTimeFormat = dateTimeFormat;
        }

        public string Prefix { get; set; }
        public string DateTimeFormat { get; set; }

        public void WriteEvent(EventEntry eventEntry, TextWriter writer)
        {
            writer.Write("{0}Timestamp : {1} ",
            Prefix, eventEntry.GetFormattedTimestamp(DateTimeFormat));
            writer.Write("{0}SourceId : {1} ",
            Prefix, eventEntry.ProviderId);
            writer.Write("{0}EventId : {1} ",
            Prefix, eventEntry.EventId);
            writer.Write("{0}Keywords : {1} ",
            Prefix, eventEntry.Schema.Keywords);
            writer.Write("{0}Level : {1} ",
            Prefix, eventEntry.Schema.Level);
            writer.Write("{0}Message : {1} ",
            Prefix, eventEntry.FormattedMessage);
            writer.Write("{0}Opcode : {1} ",
            Prefix, eventEntry.Schema.Opcode);
            writer.Write("{0}Task : {1} {2} ",
            Prefix, eventEntry.Schema.Task, eventEntry.Schema.TaskName);
            writer.Write("{0}Version : {1} ",
            Prefix, eventEntry.Schema.Version);
            writer.WriteLine("{0}Payload :{1} ",
            Prefix, FormatPayload(eventEntry));
        }

        private static string FormatPayload(EventEntry entry)
        {
            var eventSchema = entry.Schema;
            var sb = new StringBuilder();
            for (int i = 0; i < entry.Payload.Count; i++)
            {
                // Any errors will be handled in the sink
                sb.AppendFormat(" [{0} : {1}]", eventSchema.Payload[i], entry.Payload[i]);
            }
            return sb.ToString();
        }

    }
}