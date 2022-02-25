using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IList<DeskQuote> DeskQuote { get;set; }

        public async Task OnGetAsync(string sortOrder)
        {

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            DateSort = sortOrder == "Date" ? "date_desc" : "Date";


            IQueryable<DeskQuote> deskQuotes = from dq in _context.DeskQuote
                                               select dq;


            switch (sortOrder)

            {
                case "name_desc":
                    deskQuotes = deskQuotes.OrderByDescending(dq => dq.CustomerName);
                    break;
                case "Date":
                    deskQuotes = deskQuotes.OrderBy(dq => dq.DeskQuoteDate);
                    break;
                case "date_desc":
                    deskQuotes = deskQuotes.OrderByDescending(dq => dq.DeskQuoteDate);
                    break;
                default:
                    deskQuotes = deskQuotes.OrderBy(dq => dq.CustomerName);
                    break;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                deskQuotes = deskQuotes.Where(dq => dq.CustomerName.Contains(SearchString));
            }

            DeskQuote = await deskQuotes.AsNoTracking().ToListAsync();
        }
    }
}
