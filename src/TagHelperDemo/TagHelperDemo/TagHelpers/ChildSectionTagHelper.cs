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


namespace TagHelperDemo.TagHelpers
{
    [HtmlTargetElement("childsection")]
    public class ChildSectionTagHelper : TagHelper
    {
        [HtmlAttributeName("name")]
        public string SectionName { get; set; } = "childsection";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }    // Injected

        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="generator">Injected IHtmlGenerator</param>
        public ChildSectionTagHelper(IHtmlGenerator generator)
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
            TagHelperContent childContent = await output.GetChildContentAsync();
            string content = childContent.GetContent();

            output.SuppressOutput();

            if (ViewContext.HttpContext.Items[SectionName] == null)
            {
                List<string> scripts = new List<string>()
                {
                    content
                };
                ViewContext.HttpContext.Items[SectionName] = scripts;
            }
            else
            {
                List<string> scripts = (List<string>)ViewContext.HttpContext.Items[SectionName];
                if (!scripts.Contains(content)) { scripts.Add(content); }
            }

            await base.ProcessAsync(context, output);
        }
    }
}
