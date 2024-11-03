using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace crudd.Pages.Home
{
    public class Center : PageModel
    {
        private readonly ILogger<Center> _logger;

        public Center(ILogger<Center> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}