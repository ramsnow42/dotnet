using System;
using System.Collections.Generic;

namespace AgentStateAPI.Models
{
    public enum AgentState { ON_CALL, ON_LUNCH, AVAILABLE, OFFLINE }

    public class Agent
    {
        public Guid AgentId { get; set; }
        public string AgentName { get; set; }
        public AgentState CurrentState { get; set; }
        public List<AgentSkill> Skills { get; set; }
    }

    public class AgentSkill
    {
        public int Id { get; set; }
        public Guid QueueId { get; set; }
        public Guid AgentId { get; set; }
    }
}