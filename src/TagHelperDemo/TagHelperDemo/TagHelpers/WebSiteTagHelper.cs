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
    [HtmlTargetElement("website")]
    public class WebSiteTagHelper : TagHelper
    {
        [HtmlAttributeName("url")]
        public string Url { get; set; }

        [HtmlAttributeName("open-new-browser")]
        public bool NewBrowser { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }    // Injected
        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="generator">Injected IHtmlGenerator</param>
        public WebSiteTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("url");
            output.Attributes.RemoveAll("open-new-browser");

            if (Url.IsEmpty())  // Custom Helper function
            {
                output.TagName = "span";
                return;
            }

            output.TagName = "a";

            if (Url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) || Url.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                output.Attributes.SetAttribute("href", Url);
            }
            else
            {
                output.Attributes.SetAttribute("href", $"https://{Url}");
            }

            if (NewBrowser == true)
            {
                output.Attributes.SetAttribute("target", "_blank");
            }

            await base.ProcessAsync(context, output);
        }
    }
}
