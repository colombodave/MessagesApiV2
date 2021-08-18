using MessagesApi.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesApi.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly MessagesContext _context;

        public BaseRepository(MessagesContext context)
        {
            _context = context;
        }
    }
}
