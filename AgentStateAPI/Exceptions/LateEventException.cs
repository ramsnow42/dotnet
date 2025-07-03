using System;

namespace AgentStateAPI.Exceptions
{
    public class LateEventException : Exception
    {
        public LateEventException(string message) : base(message) { }
    }
}