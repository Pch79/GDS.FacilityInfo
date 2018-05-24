using System;

using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;

public partial class LoginPage : BaseXafPage
{
    public override System.Web.UI.Control InnerContentPlaceHolder
    {
        get
        {
            return Content;
        }
    }

    protected void Page_Init()
    {
        CustomizeTemplateContent += delegate (object sender, CustomizeTemplateContentEventArgs e)
        {

            DefaultVerticalTemplateContent content = TemplateContent as DefaultVerticalTemplateContent;
            if (content == null) return;
            
            content.HeaderImageControl.BorderColor = System.Drawing.Color.BurlyWood;

            content.HeaderImageControl.DefaultThemeImageLocation = @"~\Images";

            content.HeaderImageControl.ImageName = "FI_Icon.ico";
            content.HeaderImageControl.Width = 200;
            content.HeaderImageControl.Height = 50;

        };
    }
}
