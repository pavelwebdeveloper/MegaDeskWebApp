using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDeskWebApp.Data;
using MegaDeskWebApp.Models;

namespace MegaDeskWebApp.Pages.DeskQuotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDeskWebApp.Data.MegaDeskWebAppContext _context;
        public const decimal BASE_DESK_PRICE = 200;
        public const decimal PRICE_PER_DRAWER = 50;
        private int desktopSurfaceArea;
        private int additionalPrice;
        private int rushOrderPrice;
        private int desktopMaterialPrice;


        public CreateModel(MegaDeskWebApp.Data.MegaDeskWebAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            desktopSurfaceArea = DeskQuote.DeskWidth * DeskQuote.DeskDepth;

            if (desktopSurfaceArea > 1000) 
            {
                additionalPrice = desktopSurfaceArea - 1000;
            }

            switch (DeskQuote.RushOrder)
            {
                case 3:
                    if (desktopSurfaceArea < 1000)
                    {
                        rushOrderPrice = 60;
                    }
                    else if (desktopSurfaceArea >= 1000 && desktopSurfaceArea <= 2000)
                    {
                        rushOrderPrice = 70;
                    }
                    else if (desktopSurfaceArea > 2000)
                    {
                        rushOrderPrice = 80;
                    }
                    break;
                case 5:
                    if (desktopSurfaceArea < 1000)
                    {
                        rushOrderPrice = 40;
                    }
                    else if (desktopSurfaceArea >= 1000 && desktopSurfaceArea <= 2000)
                    {
                        rushOrderPrice = 50;
                    }
                    else if (desktopSurfaceArea > 2000)
                    {
                        rushOrderPrice = 60;
                    }
                    break;
                case 7:
                    if (desktopSurfaceArea < 1000)
                    {
                        rushOrderPrice = 30;
                    }
                    else if (desktopSurfaceArea >= 1000 && desktopSurfaceArea <= 2000)
                    {
                        rushOrderPrice = 35;
                    }
                    else if (desktopSurfaceArea > 2000)
                    {
                        rushOrderPrice = 40;
                    }
                    break;
                default:
                    rushOrderPrice = 0;
                    break;
            }

            switch (DeskQuote.DesktopMaterial)
            {
                case "Pine":
                    desktopMaterialPrice = 50;
                    break;
                case "Laminate":
                    desktopMaterialPrice = 100;
                    break;
                case "Veneer":
                    desktopMaterialPrice = 125;
                    break;
                case "Oak":
                    desktopMaterialPrice = 200;
                    break;
                case "Rosewood":
                    desktopMaterialPrice = 300;
                    break;
            }

            DeskQuote.DeskQuotePrice = BASE_DESK_PRICE + additionalPrice + DeskQuote.NumberOfDrawers * PRICE_PER_DRAWER + desktopMaterialPrice + rushOrderPrice;

            DeskQuote.DeskQuoteDate = DateTime.Today;

            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
