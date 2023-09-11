// -----------------------------------------------------------------------
// <copyright file="Index.cshtml.cs" company="Drawbridge Partners, LLC">
// Copyright (c) Drawbridge Partners, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using NJsonSchema;
using StackExchange.Profiling.Internal;

namespace WebApplication1.Pages
{
    /// <summary>
    /// .
    /// </summary>
    public class Index : PageModel
    {
        private readonly ILogger<Index> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Index"/> class.
        /// </summary>
        /// <param name="logger">.</param>
        public Index(ILogger<Index> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// pulls up login page for microsopht and gets the desired queries.
        /// </summary>
        /// <param name="args">.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task OnGetMyOnClick(string[] args)
        {
            var scopes = new[] { "User.Read" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            // fill in with tenant ID from Azure portal
            var tenantId = "";

            // Value from app registration
            // fill in with client ID from Azure portal
            var clientId = "";

           // https://learn.microsoft.com/dotnet/api/azure.identity.devicecodecredential
            var deviceCodeCredential = new InteractiveBrowserCredential(tenantId, clientId);

            var graphClient = new GraphServiceClient(deviceCodeCredential, scopes);

            // GET https://graph.microsoft.com/beta/security/secureScores
            var scores = await graphClient.Security.SecureScores.GetAsync();

            foreach (var score in scores.Value)
            {
                foreach (var valuer in score.ControlScores)
                {
                    if (valuer.ControlName.Equals("PWAgePolicyNew") && !valuer.Description.IsNullOrEmpty())
                    {
                        this.ViewData["PWAgePolicyNew_ControlName"] = valuer.ControlName;
                        this.ViewData["PWAgePolicyNew_Description"] = valuer.Description;
                        foreach (var addDetails in valuer.AdditionalData)
                        {
                            if (addDetails.Key.Equals("implementationStatus"))
                            {
                                this.ViewData["PWAgePolicyNew_implementationStatus"] = addDetails.Value;
                                if (addDetails.Value.Equals("Your current policy is set to let passwords expire."))
                                {
                                    this.ViewData["PWAgePolicyNew_Severity"] = "n/a";
                                }
                                else
                                {
                                    this.ViewData["PWAgePolicyNew_Severity"] = "high";
                                }
                            }
                            else if (addDetails.Key.Equals("AADControlsSource"))
                            {
                                this.ViewData["PWAgePolicyNew_AADControlsSource"] = addDetails.Value;
                            }
                        }

                        this.ViewData["PWAgePolicyNew_AdditionalData"] = valuer.AdditionalData.ToJson();
                    }
                    else if (valuer.ControlName.Equals("OneAdmin") && !valuer.Description.IsNullOrEmpty())
                    {
                        this.ViewData["OneAdmin_ControlName"] = valuer.ControlName;
                        this.ViewData["OneAdmin_Description"] = valuer.Description;
                        foreach (var addDetails in valuer.AdditionalData)
                        {
                            if (addDetails.Key.Equals("implementationStatus"))
                            {
                                this.ViewData["OneAdmin_implementationStatus"] = addDetails.Value;
                            }
                            else if (addDetails.Key.Equals("AADControlsSource"))
                            {
                                this.ViewData["OneAdmin_AADControlsSource"] = addDetails.Value;
                            }
                            else if (addDetails.Key.Equals("count"))
                            {
                                if (addDetails.Value.Equals("2") || addDetails.Value.Equals("3") || addDetails.Value.Equals("4"))
                                {
                                    this.ViewData["OneAdmin_Severity"] = "n/a";
                                }
                                else
                                {
                                    this.ViewData["OneAdmin_Severity"] = "high";
                                }
                            }
                        }

                        this.ViewData["OneAdmin_AdditionalData"] = valuer.AdditionalData.ToJson();
                    }
                }
            }
        }
    }
}
