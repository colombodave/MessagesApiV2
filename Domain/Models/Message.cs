using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesApi.Domain.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
