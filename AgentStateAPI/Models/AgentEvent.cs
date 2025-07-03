using System;
using System.Collections.Generic;

namespace AgentStateAPI.Models
{
    public class AgentEvent
    {
        public Guid AgentId { get; set; }
        public string AgentName { get; set; }
        public DateTime TimestampUtc { get; set; }
        public string Action { get; set; }
        public List<Guid> QueueIds { get; set; }
    }
}