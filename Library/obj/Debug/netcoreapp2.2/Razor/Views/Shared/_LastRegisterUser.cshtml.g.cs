#pragma checksum "C:\Users\Ghost\RiderProjects\Library\Library\Views\Shared\_LastRegisterUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "db68a7a87d5e1000de9105d60b44e4bafc99168c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__LastRegisterUser), @"mvc.1.0.view", @"/Views/Shared/_LastRegisterUser.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_LastRegisterUser.cshtml", typeof(AspNetCore.Views_Shared__LastRegisterUser))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Ghost\RiderProjects\Library\Library\Views\_ViewImports.cshtml"
using Library;

#line default
#line hidden
#line 2 "C:\Users\Ghost\RiderProjects\Library\Library\Views\_ViewImports.cshtml"
using Library.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db68a7a87d5e1000de9105d60b44e4bafc99168c", @"/Views/Shared/_LastRegisterUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dadb7a731bfbb305c411bc5eb7a307dbd6008a89", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__LastRegisterUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ApplicationUser>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(37, 124, true);
            WriteLiteral("<div id=\"NewsPartial\">\r\n    <span class=\"div-header\">آخرین افراد ثبت نام شده</span>\r\n    <div class=\"hr\"></div>\r\n    <div>\r\n");
            EndContext();
#line 6 "C:\Users\Ghost\RiderProjects\Library\Library\Views\Shared\_LastRegisterUser.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(210, 19, true);
            WriteLiteral("            <div>\r\n");
            EndContext();
#line 9 "C:\Users\Ghost\RiderProjects\Library\Library\Views\Shared\_LastRegisterUser.cshtml"
                 if (item.gender==1)
                {

#line default
#line hidden
            BeginContext(286, 49, true);
            WriteLiteral("                <img src=\"images/manicon.png\"/>\r\n");
            EndContext();
#line 12 "C:\Users\Ghost\RiderProjects\Library\Library\Views\Shared\_LastRegisterUser.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(395, 52, true);
            WriteLiteral("                <img src=\"images/woman-icon.png\"/>\r\n");
            EndContext();
#line 16 "C:\Users\Ghost\RiderProjects\Library\Library\Views\Shared\_LastRegisterUser.cshtml"
                }

#line default
#line hidden
            BeginContext(466, 39, true);
            WriteLiteral("                <span class=\"spannews\">");
            EndContext();
            BeginContext(506, 14, false);
#line 17 "C:\Users\Ghost\RiderProjects\Library\Library\Views\Shared\_LastRegisterUser.cshtml"
                                  Write(item.FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(520, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(522, 13, false);
#line 17 "C:\Users\Ghost\RiderProjects\Library\Library\Views\Shared\_LastRegisterUser.cshtml"
                                                  Write(item.Lastname);

#line default
#line hidden
            EndContext();
            BeginContext(535, 69, true);
            WriteLiteral("</span>\r\n            </div>\r\n            <div class=\"minihr\"></div>\r\n");
            EndContext();
#line 20 "C:\Users\Ghost\RiderProjects\Library\Library\Views\Shared\_LastRegisterUser.cshtml"
        }

#line default
#line hidden
            BeginContext(615, 18, true);
            WriteLiteral("    </div>\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ApplicationUser>> Html { get; private set; }
    }
}
#pragma warning restore 1591