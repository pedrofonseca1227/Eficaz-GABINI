using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Views.Users
{
    public class Create.cshtml : PageModel
    {
        private readonly ILogger<Create.cshtml> _logger;

        public Create.cshtml(ILogger<Create.cshtml> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}