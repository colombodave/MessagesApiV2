using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesApi.Models
{
    public class MessagesItem
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string Secret { get; set; }
    }

    public class MessagesItemDTO
    {
        public long Id { get; set; }
        public string Message { get; set; }
    }
}
