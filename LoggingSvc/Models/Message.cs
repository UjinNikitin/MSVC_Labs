using System;

namespace LoggingSvc.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Value { get; set; }


        public override string ToString() =>
            $"{Id}: {Value}";
    }
}
