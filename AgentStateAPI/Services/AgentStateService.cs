using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgentStateAPI.Data;
using AgentStateAPI.Models;
using AgentStateAPI.Exceptions;

namespace AgentStateAPI.Services
{
    public class AgentStateService
    {
        private readonly AppDbContext _context;

        public AgentStateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ProcessEventAsync(AgentEvent agentEvent)
        {
            if (agentEvent.TimestampUtc < DateTime.UtcNow.AddHours(-1))
                throw new LateEventException("Event is more than 1 hour old.");

            var agent = await _context.Agents
                .Include(a => a.Skills)
                .FirstOrDefaultAsync(a => a.AgentId == agentEvent.AgentId);

            if (agent == null)
            {
                agent = new Agent
                {
                    AgentId = agentEvent.AgentId,
                    AgentName = agentEvent.AgentName,
                    Skills = new List<AgentSkill>()
                };
                _context.Agents.Add(agent);
            }

            if (agentEvent.Action == "START_DO_NOT_DISTURB" &&
                agentEvent.TimestampUtc.Hour >= 11 && agentEvent.TimestampUtc.Hour < 13)
                agent.CurrentState = AgentState.ON_LUNCH;
            else if (agentEvent.Action == "CALL_STARTED")
                agent.CurrentState = AgentState.ON_CALL;

            agent.Skills = agentEvent.QueueIds
                .Distinct()
                .Select(q => new AgentSkill { QueueId = q, AgentId = agent.AgentId })
                .ToList();

            await _context.SaveChangesAsync();
        }
    }
}