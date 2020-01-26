using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagHelperDemo.Library;

namespace TagHelperDemo.TagHelpers
{
    [HtmlTargetElement("email")]
    public class EmailTagHelper : TagHelper
    {
        [HtmlAttributeName("mail-to")]
        public string EmailAddress { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }    // Injected
        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="generator">Injected IHtmlGenerator</param>
        public EmailTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <summary>
        /// Process Tag
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("mail-to");

            if (EmailAddress.IsEmpty()) // Custom Helper function
            {
                output.TagName = "span";
                return;
            }

            if (ValidateEmail(EmailAddress) == true)
            {
                output.TagName = "a";
                output.Attributes.SetAttribute("href", $"mailto:{EmailAddress}");
            }
            else
            {
                output.TagName = "span";
            }

            await base.ProcessAsync(context, output);
        }

        /// <summary>
        /// Email validation
        /// TODO: Make a better one. This is too basic
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool ValidateEmail(string email)
        {
            return !email.StartsWith("-") &&
                !email.EndsWith("-") &&
                !email.StartsWith(".") &&
                !email.EndsWith(".") &&
                !email.Contains("..") &&
                !email.Contains(".@") &&
                !email.Contains("@.") &&
                email.CountOf("@") == 1;    // Custom Helper function
        }
    }
}
