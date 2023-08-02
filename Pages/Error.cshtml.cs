// -----------------------------------------------------------------------
// <copyright file="Error.cshtml.cs" company="Drawbridge Partners, LLC">
// Copyright (c) Drawbridge Partners, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
#nullable enable
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages
{
    /// <summary>
    /// Error logging.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]

    public class Error : PageModel
    {
        private readonly ILogger<Error> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        public Error(ILogger<Error> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets or sets RequestId.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether request ID is null or empty.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        /// <summary>
        /// On get.
        /// </summary>
        public void OnGet()
        {
            this.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
        }
    }
}