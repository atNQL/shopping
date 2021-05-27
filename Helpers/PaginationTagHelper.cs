using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalShop
{
    public class PaginationTagHelper: TagHelper
    {
        public object RouteID { get; set; }

        public string Url { get; set; }
        public int Total { get; set; }

        public int Size { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            int n = (Total-1)/ Size + 1;
            StringBuilder sb = new StringBuilder();

            int p = 1;

            if (RouteID != null)
            {
                p = Convert.ToInt32(RouteID);
            }

            if (p > 1)
            {
                string uri = string.Format(Url, p - 1);
                sb.AppendFormat("<li class=\"page-item\"><a class=\"page-link\" href=\"{1}\">Previous</a></li>", p - 1, uri);
            }
            else
            {
                sb.AppendFormat("<li class=\"page-item\"><a class=\"page-link\" href=\"#\">Previous</a></li>", p - 1);
            }
            for (int i = 1; i <= n; i++)
            {
                string uri = string.Format(Url, i);
                if (p == i)
                {
                    sb.AppendFormat("<li class=\"page-item active\"><a class=\"page-link\" href=\"{1}\">{0}</a></li>", i, uri);
                }
                else
                {
                    sb.AppendFormat("<li class=\"page-item\"><a class=\"page-link\" href=\"{1}\">{0}</a></li>", i, uri);
                }
                
            }

            if (p < n)
            {
                string uri = string.Format(Url, p + 1);
                sb.AppendFormat("<li class=\"page-item\"><a class=\"page-link\" href=\"{1}\">Next</a></li>", p + 1, uri);
            }
            else
            {
                sb.AppendFormat("<li class=\"page-item\"><a class=\"page-link\" href=\"#\">Next</a></li>", p - 1);
            }

            //base.Process(context, output);
            output.TagName = "ul";
            output.Attributes.SetAttribute("class", "pagination");
            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
