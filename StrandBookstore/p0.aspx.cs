using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrandBookstore
{
    public partial class p0 : System.Web.UI.Page
    {
        Model1 context = new Model1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userid1 = User.Identity.Name;

                string userid = context.AspNetUsers.Where(y => y.Email == userid1).Select(x => x.Id).FirstOrDefault();
                if (User.Identity.IsAuthenticated)
                {
                    Status.Text = String.Format(userid);
                    dbBind();
                }
            }
        }
        protected void dbBind()
        {
            var q = context.Orders.Where(x => x.Purchased == false).ToList();
            GridView1.DataSource = q;
            GridView1.DataBind();
        }

        protected void tallyTotalPrice()
        {
            double num = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                DropDownList currentDropDownQty = (DropDownList)(row.FindControl("DropDownList1"));
                Label currentLabelPrice = (Label)(row.FindControl("Label_myPrice"));

                if (Int32.TryParse(currentDropDownQty.SelectedValue, out int qty)
                    && double.TryParse(currentLabelPrice.Text.Substring(1, currentLabelPrice.Text.Length-1), out double price))
                {
                    num += (double)qty * price;
                }
            }

            Label1.Text = num.ToString();
        }

        protected void CheckOut_Click(object sender, EventArgs e)
        {
            decimal num = 0;
            bool bChoose = false;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                // get the order id
                int orderid = Convert.ToInt32(GridView1.DataKeys[gvr.RowIndex].Values[0]);

                // Retrieve info
                Order o = BusinessLogic.MydbContext.Orders.Where(x => x.OrderID == orderid).First();
                Book b = BusinessLogic.MydbContext.Books.Where(x => x.BookID == o.BookID).First();

                // get the quantity of item
                DropDownList ddl_quantityTxt = gvr.FindControl("DropDownList1") as DropDownList;
                int quantity = Convert.ToInt32(ddl_quantityTxt.Text);

                // validate
                if (quantity > 0)
                {
                    if (quantity <= b.Stock)
                    {
                        
                        // update database
                        o.Quantity = Convert.ToInt32(quantity);
                        o.Purchased = true;
                        b.Stock = b.Stock - o.Quantity;
                        BusinessLogic.MydbContext.SaveChanges();
                        ddl_quantityTxt.Text = "0";

                        num += quantity * b.Price;

                        //Label2.Text = string.Format("Purchase successful");
                        bChoose = true;
                    }
                    else
                    {
                        Label2.Text = string.Format("{0} is out of stock.", b.Title);
                    }
                }
            }

            if (!bChoose)
            {
                Label2.Text = "Please select at least 1 item to checkout.";
            }
            else
            {
                //Label1.Text = "0";
                dbBind();
                Response.Redirect($"~/p1.aspx?Price={num}");
            }
        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gridViewRow.RowIndex;

            if (index != -1)
            {
                Label lbID = (Label)GridView1.Rows[index].FindControl("Label_myOrderID");
                int orderID = Int32.Parse(lbID.Text);
                BusinessLogic.DeleteTheOrder(orderID);
                dbBind();
                tallyTotalPrice();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gridViewRow.RowIndex;

            if (index != -1)
            {
                var q = GridView1.Rows[index];

                Label lbID = (Label)GridView1.Rows[index].FindControl("Label_myOrderID");
                int orderID = Int32.Parse(lbID.Text);

                DropDownList ddl_qty = (DropDownList)q.FindControl("DropDownList1");

                int qty = Int32.Parse(ddl_qty.SelectedValue);

                var order = context.Orders.FirstOrDefault(x => x.OrderID == orderID);
                if (order != null)
                {
                    var book = context.Books.FirstOrDefault(x => x.BookID == order.BookID);

                    if (book != null)
                    {
                        // get the stock
                        var stock = book.Stock;

                        // Validate the input quantity
                        if (stock < qty)
                        {
                            ddl_qty.Text = stock > 5 ? "5" : stock.ToString();
                            Label2.Text = $"{book.Title} is not enough, the current quentity is {stock}";
                        }
                        else
                        {
                            Label2.Text = "";
                        }
                    }
                }
            }

            tallyTotalPrice();
        }
    }
}