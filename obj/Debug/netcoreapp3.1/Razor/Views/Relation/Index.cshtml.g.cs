#pragma checksum "C:\Users\aytakin.agayeva\Desktop\Lombard\Lombard\Views\Relation\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6788f1d6585936220291e8139b1e8eeae24ebea1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Relation_Index), @"mvc.1.0.view", @"/Views/Relation/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\aytakin.agayeva\Desktop\Lombard\Lombard\Views\_ViewImports.cshtml"
using Lombard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\aytakin.agayeva\Desktop\Lombard\Lombard\Views\_ViewImports.cshtml"
using Lombard.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6788f1d6585936220291e8139b1e8eeae24ebea1", @"/Views/Relation/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17e4511d6e62fbc7d9cdefc5c461ed9c3b1d48f8", @"/Views/_ViewImports.cshtml")]
    public class Views_Relation_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Lombard.Domain.Entities.RelationType>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("elm-with-tooltip"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-tooltip-on-right", new global::Microsoft.AspNetCore.Html.HtmlString("Silmək üçün klikləyin"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\aytakin.agayeva\Desktop\Lombard\Lombard\Views\Relation\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!-- Table holder starts here -->
<div class=""main-table-holder-inside-page-cont d-f"">
    <div class=""label-holder-inside-table-holder label-holder-with-add-svg d-f"">
        <p class=""f-s-24 f-w-bold""
        id=""PageIndicator"" data-page=""Qohumluq"" data-indicator=""Qohumluq növlərinin siyahısına baxırsınız""
        >Qohumluq növləri</p>
        
        <!--Create svg starts here-->
        <div class=""elm-with-tooltip add-button-svg-holder"" data-tooltip-on-right=""Yeni qohumluq növü əlavə etmək üçün klikləyin"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6788f1d6585936220291e8139b1e8eeae24ebea15221", async() => {
                WriteLiteral(@"
                <svg width=""68"" height=""68"" viewBox=""0 0 68 68"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                    <rect width=""68"" height=""68"" rx=""34"" fill=""#E3EBFF""/>
                    <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M34 23C27.925 23 23 27.925 23 34C23 40.075 27.925 45 34 45C40.075 45 45 40.075 45 34C45 27.925 40.075 23 34 23ZM35 38C35 38.2652 34.8946 38.5196 34.7071 38.7071C34.5196 38.8946 34.2652 39 34 39C33.7348 39 33.4804 38.8946 33.2929 38.7071C33.1054 38.5196 33 38.2652 33 38V35H30C29.7348 35 29.4804 34.8946 29.2929 34.7071C29.1054 34.5196 29 34.2652 29 34C29 33.7348 29.1054 33.4804 29.2929 33.2929C29.4804 33.1054 29.7348 33 30 33H33V30C33 29.7348 33.1054 29.4804 33.2929 29.2929C33.4804 29.1054 33.7348 29 34 29C34.2652 29 34.5196 29.1054 34.7071 29.2929C34.8946 29.4804 35 29.7348 35 30V33H38C38.2652 33 38.5196 33.1054 38.7071 33.2929C38.8946 33.4804 39 33.7348 39 34C39 34.2652 38.8946 34.5196 38.7071 34.7071C38.5196 34.8946 38.2652 35 38 35H35V38Z"" fill=""#3B72FF");
                WriteLiteral("\"/>\r\n                </svg>     \r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
        <!--Create svg ends here-->
    </div>

    <!--Empty space div starts here-->
    <div class=""empty-space-div-20""></div>
    <!--Empty space div ends here-->

    <div class=""search-bar-holder-inside-table-holder"">
        <div class=""search-bar-div-inside-table-holder"">
            <svg width=""22"" height=""22"" viewBox=""0 0 22 22"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M20.1666 8.7085C20.1666 12.5055 17.0885 15.5835 13.2916 15.5835C11.7043 15.5835 10.2426 15.0455 9.07888 14.142C9.03505 14.2348 8.97479 14.3217 8.8981 14.3983L3.3981 19.8983C3.04012 20.2563 2.45972 20.2563 2.10174 19.8983C1.74376 19.5404 1.74376 18.96 2.10174 18.602L7.60174 13.102C7.67843 13.0253 7.76533 12.965 7.85806 12.9212C6.95453 11.7575 6.41659 10.2958 6.41659 8.7085C6.41659 4.91154 9.49463 1.8335 13.2916 1.8335C17.0885 1.8335 20.1666 4.91154 20.1666 8.7085ZM18.3333 8.7085C18.3333 5.92406 16.076 3.66683 13.2916 3.66683C10.5071 3.66");
            WriteLiteral(@"683 8.24992 5.92406 8.24992 8.7085C8.24992 11.4929 10.5071 13.7502 13.2916 13.7502C16.076 13.7502 18.3333 11.4929 18.3333 8.7085Z"" fill=""#C4C8D4""/>
            </svg>                            
            <input class=""f-s-14"" type=""text"" name=""search_inside_table"" id=""search_inside_table"" placeholder=""Axtar"">
        </div>
    </div>
    <div class=""table-inside-table-holder"">
        <table>
            <thead>
                <tr class=""table-row-inside-table"">
                    <th class=""background-gray-th-inside-table"">№</th>
                    <th onclick=""sortTable(1)"">
                        <div class=""d-f"">
                            Növ
                            <svg width=""14"" height=""14"" viewBox=""0 0 14 14"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M3.67085 4.83764C3.44305 5.06545 3.44305 5.43479 3.67085 5.6626C3.89866 5.89041 4.26801 5.89041 4.49581 5.6626L7 3.15841L9.50419 5.6626C9.73");
            WriteLiteral(@"199 5.89041 10.1013 5.89041 10.3291 5.6626C10.557 5.4348 10.557 5.06545 10.3291 4.83764L7.41248 1.92098C7.30308 1.81158 7.15471 1.75012 7 1.75012C6.84529 1.75012 6.69692 1.81158 6.58752 1.92098L3.67085 4.83764Z"" fill=""#8992A9""/>
                                <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M10.3291 9.16256C10.557 8.93475 10.557 8.56541 10.3291 8.3376C10.1013 8.1098 9.73199 8.1098 9.50419 8.3376L7 10.8418L4.49581 8.3376C4.26801 8.1098 3.89866 8.1098 3.67085 8.3376C3.44305 8.56541 3.44305 8.93476 3.67085 9.16256L6.58752 12.0792C6.81533 12.307 7.18467 12.307 7.41248 12.0792L10.3291 9.16256Z"" fill=""#8992A9""/>
                            </svg> 
                        </div>                                 
                    </th>
                    <th>
                        <div class=""d-f"">
                            <p>Sil</p>
                            <svg width=""14"" height=""14"" viewBox=""0 0 14 14"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                           ");
            WriteLiteral(@"     <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M3.67085 4.83764C3.44305 5.06545 3.44305 5.43479 3.67085 5.6626C3.89866 5.89041 4.26801 5.89041 4.49581 5.6626L7 3.15841L9.50419 5.6626C9.73199 5.89041 10.1013 5.89041 10.3291 5.6626C10.557 5.4348 10.557 5.06545 10.3291 4.83764L7.41248 1.92098C7.30308 1.81158 7.15471 1.75012 7 1.75012C6.84529 1.75012 6.69692 1.81158 6.58752 1.92098L3.67085 4.83764Z"" fill=""#8992A9""/>
                                <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M10.3291 9.16256C10.557 8.93475 10.557 8.56541 10.3291 8.3376C10.1013 8.1098 9.73199 8.1098 9.50419 8.3376L7 10.8418L4.49581 8.3376C4.26801 8.1098 3.89866 8.1098 3.67085 8.3376C3.44305 8.56541 3.44305 8.93476 3.67085 9.16256L6.58752 12.0792C6.81533 12.307 7.18467 12.307 7.41248 12.0792L10.3291 9.16256Z"" fill=""#8992A9""/>
                            </svg> 
                        </div>                                 
                    </th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 64 "C:\Users\aytakin.agayeva\Desktop\Lombard\Lombard\Views\Relation\Index.cshtml"
                 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr class=\"table-row-inside-table\">\r\n                        <td class=\"background-gray-th-inside-table table-row-count\"></td>\r\n                        <td>");
#nullable restore
#line 67 "C:\Users\aytakin.agayeva\Desktop\Lombard\Lombard\Views\Relation\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Type));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6788f1d6585936220291e8139b1e8eeae24ebea112543", async() => {
                WriteLiteral(@"
                                <svg width=""24"" height=""24"" viewBox=""0 0 24 24"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                    <path d=""M20.25 6H17.25V4.125C17.25 3.29766 16.5773 2.625 15.75 2.625H8.25C7.42266 2.625 6.75 3.29766 6.75 4.125V6H3.75C3.33516 6 3 6.33516 3 6.75V7.5C3 7.60313 3.08437 7.6875 3.1875 7.6875H4.60312L5.18203 19.9453C5.21953 20.7445 5.88047 21.375 6.67969 21.375H17.3203C18.1219 21.375 18.7805 20.7469 18.818 19.9453L19.3969 7.6875H20.8125C20.9156 7.6875 21 7.60313 21 7.5V6.75C21 6.33516 20.6648 6 20.25 6ZM15.5625 6H8.4375V4.3125H15.5625V6Z"" fill=""#3B72FF""/>
                                </svg>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 69 "C:\Users\aytakin.agayeva\Desktop\Lombard\Lombard\Views\Relation\Index.cshtml"
                                                     WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("                                        \r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 76 "C:\Users\aytakin.agayeva\Desktop\Lombard\Lombard\Views\Relation\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n<!-- Table holder ends here -->\r\n\r\n<!--Empty space div starts here-->\r\n<div class=\"empty-space-div-30\"></div>\r\n<!--Empty space div ends here-->\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Lombard.Domain.Entities.RelationType>> Html { get; private set; }
    }
}
#pragma warning restore 1591