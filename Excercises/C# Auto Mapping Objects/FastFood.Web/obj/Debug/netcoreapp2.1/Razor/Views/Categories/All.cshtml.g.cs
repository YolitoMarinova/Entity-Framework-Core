#pragma checksum "C:\Users\ioan4\Desktop\SoftUni\Entity Framework Core\Excercises\C# Auto Mapping Objects\FastFood.Web\Views\Categories\All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a69687e0b6fe4859e6fda5005ed48f002e301d00"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Categories_All), @"mvc.1.0.view", @"/Views/Categories/All.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Categories/All.cshtml", typeof(AspNetCore.Views_Categories_All))]
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
#line 1 "C:\Users\ioan4\Desktop\SoftUni\Entity Framework Core\Excercises\C# Auto Mapping Objects\FastFood.Web\Views\_ViewImports.cshtml"
using FastFood.Web;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a69687e0b6fe4859e6fda5005ed48f002e301d00", @"/Views/Categories/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e2355b4d2dd102d586b09f0f668ac669855f614", @"/Views/_ViewImports.cshtml")]
    public class Views_Categories_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<FastFood.Web.ViewModels.Categories.CategoryAllViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Categories", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(71, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\ioan4\Desktop\SoftUni\Entity Framework Core\Excercises\C# Auto Mapping Objects\FastFood.Web\Views\Categories\All.cshtml"
  
    ViewData["Title"] = "All Categories";

#line default
#line hidden
            BeginContext(123, 267, true);
            WriteLiteral(@"<h1 class=""text-center"">All Categories</h1>
<hr class=""hr-2"" />
<table class=""table mx-auto"">
    <thead>
        <tr class=""row"">
            <th class=""col-md-1"">#</th>
            <th class=""col-md-2"">Category</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 16 "C:\Users\ioan4\Desktop\SoftUni\Entity Framework Core\Excercises\C# Auto Mapping Objects\FastFood.Web\Views\Categories\All.cshtml"
         for(var i = 0; i < Model.Count(); i++)
    {

#line default
#line hidden
            BeginContext(446, 59, true);
            WriteLiteral("        <tr class=\"row\">\r\n            <th class=\"col-md-1\">");
            EndContext();
            BeginContext(507, 3, false);
#line 19 "C:\Users\ioan4\Desktop\SoftUni\Entity Framework Core\Excercises\C# Auto Mapping Objects\FastFood.Web\Views\Categories\All.cshtml"
                             Write(i+1);

#line default
#line hidden
            EndContext();
            BeginContext(511, 40, true);
            WriteLiteral("</th>\r\n            <td class=\"col-md-2\">");
            EndContext();
            BeginContext(552, 13, false);
#line 20 "C:\Users\ioan4\Desktop\SoftUni\Entity Framework Core\Excercises\C# Auto Mapping Objects\FastFood.Web\Views\Categories\All.cshtml"
                            Write(Model[i].Name);

#line default
#line hidden
            EndContext();
            BeginContext(565, 22, true);
            WriteLiteral("</td>\r\n        </tr>\r\n");
            EndContext();
#line 22 "C:\Users\ioan4\Desktop\SoftUni\Entity Framework Core\Excercises\C# Auto Mapping Objects\FastFood.Web\Views\Categories\All.cshtml"
    }

#line default
#line hidden
            BeginContext(594, 50, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n<div align=\"center\">\r\n    ");
            EndContext();
            BeginContext(644, 101, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9f75136d94434e28a346231a29a8e4c1", async() => {
                BeginContext(733, 8, true);
                WriteLiteral("Register");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(745, 8, true);
            WriteLiteral("\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<FastFood.Web.ViewModels.Categories.CategoryAllViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
