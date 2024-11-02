using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace crudd.Pages
{
    public class EditData : PageModel
    {
        private readonly ILogger<EditData> _logger;

        public EditData(ILogger<EditData> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}