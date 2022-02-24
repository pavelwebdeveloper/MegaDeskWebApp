using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskWebApp.Data;
using MegaDeskWebApp.Models;

namespace MegaDeskWebApp.Pages.DeskQuotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDeskWebApp.Data.MegaDeskWebAppContext _context;

        public IndexModel(MegaDeskWebApp.Data.MegaDeskWebAppContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        //public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<DeskQuote> DeskQuote { get;set; }

        public async Task OnGetAsync(string sortOrder)
        {

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            DateSort = sortOrder == "Date" ? "date_desc" : "Date";


            IQueryable<DeskQuote> descQuotes = from dq in _context.DeskQuote
                                               select dq;


            switch (sortOrder)

            {
                case "name_desc":
                    descQuotes = descQuotes.OrderByDescending(dq => dq.CustomerName);
                    break;
                case "Date":
                    descQuotes = descQuotes.OrderBy(dq => dq.DeskQuoteDate);
                    break;
                case "date_desc":
                    descQuotes = descQuotes.OrderByDescending(dq => dq.DeskQuoteDate);
                    break;
                default:
                    descQuotes = descQuotes.OrderBy(dq => dq.CustomerName);
                    break;
            }

            DeskQuote = await descQuotes.AsNoTracking().ToListAsync();
        }
    }
}
