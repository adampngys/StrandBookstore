using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrandBookstore
{
    public partial class p1 : System.Web.UI.Page
    {
        Model1 context = new Model1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var price = Request.QueryString["price"];
                Total.Text = String.Format("${0:C}", price);
            }
        }
    }
}