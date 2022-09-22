using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMqProducer.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageString { get; set; }
        public string Ddate { get; set; }
    }
}
