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
    [HtmlTargetElement("renderchildsection")]
    public class RenderChildSectionTagHelper : TagHelper
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
        public RenderChildSectionTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            if (ViewContext.HttpContext.Items[SectionName] == null)
            {
                output.SuppressOutput();
            }
            else
            {
                List<string> scripts = (List<string>)ViewContext.HttpContext.Items[SectionName];
                StringBuilder sb = new StringBuilder();
                foreach (string script in scripts)
                {
                    sb.AppendLine(script);
                }
                output.Content.SetHtmlContent(sb.ToString());
            }

            base.Process(context, output);
        }
    }
}
