using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;


namespace StrandBookstore
{
    public partial class Cart : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model1 context = new Model1();

                string userid1 = User.Identity.Name;
                string userid = context.AspNetUsers.Where(y => y.Email == userid1).Select(x => x.Id).FirstOrDefault();

                var q = context.Orders.Where(x => x.Id == userid && x.Purchased == false).ToList();
                if (q.FirstOrDefault() == null)
                {
                    Label1.Text = "You have not added any books to your cart.";
                }
                else
                {
                    GridView1.DataSource = q;
                    GridView1.DataBind();
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            int index = row.RowIndex;

            Model1 context = new Model1();

            // SelectedIndex is the zero-based index of the selected row in a GridView 
            // control. The default is -1, which indicates that no row is currently selected.
            if (index != -1)
            {
                DropDownList qty = (DropDownList)row.FindControl("DropDownList1");
                int orderqty = Int32.Parse((row.FindControl("DropDownList1") as DropDownList).SelectedValue);

                //verify this is a valid book order
                int theorderID = Int32.Parse((row.FindControl("Label_myOrderID") as Label).Text);
                Order order = context.Orders.Where(x => x.OrderID == theorderID).FirstOrDefault();

                if (order != null)
                {
                    //check book stock
                    Book thebook = context.Books.Where(x => x.BookID == order.BookID).FirstOrDefault();
                    int bookqty = thebook.Stock;
                    //int bookISBN = Int32.Parse((row.FindControl("Label_myISBN") as TextBox).Text);
                    //string bookISBN2 = bookISBN.ToString();
                    //int bookqty = context.Books.Where(x => x.ISBN == bookISBN2).Select(x => x.Stock).FirstOrDefault();

                    if (bookqty == 0)
                    {
                        Label1.Text = String.Format("Sorry, {0} is out of stock.", thebook.Title);
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (bookqty > orderqty)
                    {
                        Label1.Text = String.Format("You are about to purchase {0} copy/copies of \"{1}\".", orderqty, thebook.Title);
                        Label1.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (orderqty > bookqty)
                    {
                        Label1.Text = String.Format("Sorry, there are only {0} copy/copies of \"{1}\" left.", thebook.Stock, thebook.Title);
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                    else if(orderqty == bookqty)
                    {
                        Label1.Text = String.Format("You are about to purchase the last {0} copy/copies of \"{1}\".", thebook.Stock, thebook.Title);
                        Label1.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {

                }
            }

            TallyPrice();
        }

        protected void TallyPrice()
        {
            decimal total = 0;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                int orderqty1 = Int32.Parse((gvr.FindControl("DropDownList1") as DropDownList).SelectedValue);
                string theISBN = ((gvr.FindControl("Label_myISBN") as Label).Text);  
                Model1 context = new Model1();
                decimal price1 = context.Books.Where(x => x.ISBN == theISBN).Select(x => x.Price).FirstOrDefault();
                total += (decimal)orderqty1 * price1;
                Label2.Text = String.Format("Subtotal: ${0}", total.ToString());
            }
        }

        protected void Button_DeleteItem_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            int index = row.RowIndex;

            if (index != -1) // != -1 >>> a GridViewRow has been selected
            {
                int theorderID = Int32.Parse((row.FindControl("Label_myOrderID") as Label).Text);

                using (Model1 entities = new Model1())
                {
                    Order order = entities.Orders.Where(p => p.OrderID == theorderID).First<Order>();
                    entities.Orders.Remove(order);
                    entities.SaveChanges();
                }
            }

            Response.Redirect("Cart.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool nothingChosen = true;
            decimal total = 0;
            Model1 context = new Model1();

            
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                // get the order id
                int orderid = Convert.ToInt32(GridView1.DataKeys[gvr.RowIndex].Values[0]);
                Order o = context.Orders.Where(x => x.OrderID == orderid).First();
                Book b = context.Books.Where(x => x.BookID == o.BookID).First();
                int orderqty = Int32.Parse((gvr.FindControl("DropDownList1") as DropDownList).SelectedValue);
                if (b.Stock > 0)
                {
                    if(orderqty <= b.Stock)
                    {
                        // update database
                        o.Quantity = Convert.ToInt32(orderqty);
                        o.Purchased = true;
                        b.Stock = b.Stock - o.Quantity;
                        context.SaveChanges();
                        total += orderqty * b.Price;
                        nothingChosen = false;
                    }
                    else
                    {
                        Label1.Text = String.Format("Sorry, \"{0}\" is out of stock.", b.Title);
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            if (nothingChosen)
            {
                Label1.Text = "Please select at least 1 item to checkout.";
            }
            else
            {
                Response.Redirect($"~/p1.aspx?Price={total}");
            }
        }
    }
}