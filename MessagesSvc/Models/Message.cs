using System;
using Newtonsoft.Json;

namespace MessagesSvc.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Value { get; set; }


        public override string ToString() =>
            $"{Id}: {Value}";
    }
}
