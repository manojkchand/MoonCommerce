﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreSite1.Data;
using CoreSite1.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreSite1.Pages.Product
{
    [Authorize(Roles = "ThisSiteAdmin")]
    public class DetailsModel : PageModel
    {
        private readonly CoreSite1.Data.ApplicationDbContext _context;

        public DetailsModel(CoreSite1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CoreSite1.Models.Product Product { get; set; }
        public IList<CoreSite1.Models.Variant> Variants { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                .Include(p => p.Category).FirstOrDefaultAsync(m => m.ProductId == id);

            Variants = await _context.Variants
               .Where(v => v.Product == Product).ToListAsync();

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
