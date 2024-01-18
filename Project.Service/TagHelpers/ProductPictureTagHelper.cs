using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.TagHelpers
{   

    public class ProductPictureTagHelper:TagHelper
    {
        public string? ProductPicture { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            if (string.IsNullOrWhiteSpace(ProductPicture))
            {
                output.Attributes.SetAttribute("src", "~/ProductPicture/DefaultProductPicture.png");
            }
            else
            {
                output.Attributes.SetAttribute("src", $"~/ProductPicture/{ProductPicture}");
            }
        }

    }
}
