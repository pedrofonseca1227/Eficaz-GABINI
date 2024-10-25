using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Views.Users
{
    public class Edit.cshtml : PageModel
    {
        private readonly ILogger<Edit.cshtml> _logger;

        public Edit.cshtml(ILogger<Edit.cshtml> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}