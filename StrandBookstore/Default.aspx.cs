using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrandBookstore
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Image1Button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("p3.aspx?title=" + "gilmore");
        }
        protected void Image2Button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("p3.aspx?title=" + "the hate u give");
        }
    }
}