// -----------------------------------------------------------------------
// <copyright file="AddSampleRequest.cs" company="Drawbridge Partners, LLC">
// Copyright (c) Drawbridge Partners, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace O365Automation.Models
{
    /// <summary>
    /// Contains data used to add a <see cref="Sample" />.
    /// </summary>
    public class AddSampleRequest
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public string Color { get; set; }
    }
}