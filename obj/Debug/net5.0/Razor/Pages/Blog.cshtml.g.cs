#pragma checksum "/home/gbowne1/Documents/MyBlog/Pages/Blog.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1478fec7670f9eeb97bbf05a7f44574d00e0fae7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(MyBlog.Pages.Pages_Blog), @"mvc.1.0.razor-page", @"/Pages/Blog.cshtml")]
namespace MyBlog.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/home/gbowne1/Documents/MyBlog/Pages/_ViewImports.cshtml"
using MyBlog;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1478fec7670f9eeb97bbf05a7f44574d00e0fae7", @"/Pages/Blog.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"11caa52ebb30469fc59d2758059afce6ca72001e", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Blog : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "/home/gbowne1/Documents/MyBlog/Pages/Blog.cshtml"
  
    ViewData["Title"] = "Blog";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 7 "/home/gbowne1/Documents/MyBlog/Pages/Blog.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<div>\r\n    <p>Hi this is the blog!<p>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MyBlog.Pages.Blog> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<MyBlog.Pages.Blog> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<MyBlog.Pages.Blog>)PageContext?.ViewData;
        public MyBlog.Pages.Blog Model => ViewData.Model;
    }
}
#pragma warning restore 1591
