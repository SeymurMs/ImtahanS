using ImtahanLast.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanLast.Services
{
    public class LayoutServices
    {
        public readonly AppDbContext _context;
        public LayoutServices(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetSettings()
        {
            return _context.services.ToDictionary(a=>a.Key,b=>b.Value);
        }
    }
}
