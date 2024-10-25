using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Views.Users
{
    public class Delete.cshtml : PageModel
    {
        private readonly ILogger<Delete.cshtml> _logger;

        public Delete.cshtml(ILogger<Delete.cshtml> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}