using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Html;
using TagHelperDemo.Library;

namespace TagHelperDemo.TagHelpers
{
    [HtmlTargetElement("telephone")]
    public class TelephoneTagHelper : TagHelper
    {
        [HtmlAttributeName("tel")]
        public string TelephoneNumber { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }    // Injected
        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="generator">Injected IHtmlGenerator</param>
        public TelephoneTagHelper(IHtmlGenerator generator)
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
            if (TelephoneNumber.IsEmpty())
            {
                output.TagName = "span";
                output.Attributes.RemoveAll("telephone");
                return;
            }

            // Check to see if the number start with + ?
            if (!TelephoneNumber.StartsWith("+"))
            {
                output.TagName = "span";
                output.Attributes.RemoveAll("telephone");
                return;
            }

            // Remove space hyphen and (0)
            TelephoneNumber = TelephoneNumber.Replace("-", "").Replace(" ", "").Replace("(0)", "");

            // Find non-numeric character and chop it off
            int index = 1;
            while (index < TelephoneNumber.Length)
            {
                if (!(TelephoneNumber[index] >= '0' && TelephoneNumber[index] <= '9'))
                {
                    TelephoneNumber = TelephoneNumber.Substring(0, index);
                }
                index++;
            }

            // Telephone link
            output.TagName = "a";
            output.Attributes.RemoveAll("telephone");
            output.Attributes.SetAttribute("href", $"tel:{TelephoneNumber}");

            await base.ProcessAsync(context, output);
        }
    }
}
