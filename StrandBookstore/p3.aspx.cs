using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrandBookstore
{
    public partial class p3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model1 context = new Model1();
                string title = Request.QueryString["title"];

                if (title == null)
                {
                    title = "Set for Life";
                }

                var query = (from x in context.Books
                             join y in context.Categories on
                             x.CategoryID equals y.CategoryID
                             where x.Title.ToUpper().Contains(title.ToUpper())
                             select new
                             {
                                 BookID = x.BookID,
                                 Title = x.Title,
                                 Category = y.Name,
                                 ISBN = x.ISBN,
                                 Author = x.Author,
                                 Stock = x.Stock,
                                 Price = x.Price
                             }).ToList();
                GridView1.DataSource = query;
                GridView1.DataBind();
                Label2.Text = String.Format("{0} book(s) found.", query.Count.ToString());
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddtoCart")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                int bookid = Convert.ToInt32(row.Cells[0].Text);
                string title = row.Cells[1].Text;
                string category = row.Cells[2].Text;
                string isbn = row.Cells[3].Text;
                string author = row.Cells[4].Text;
                decimal price = Convert.ToDecimal(row.Cells[6].Text.Substring(1, row.Cells[6].Text.Length - 1));
                bool purchased = false;
                int quantity = 0;

                string userid1 = User.Identity.Name;
                Model1 entities1 = new Model1();
                string userid = entities1.AspNetUsers.Where(y => y.Email == userid1).Select(x => x.Id).FirstOrDefault();

                if (userid == null)
                {
                    Response.Redirect("/Account/Login.aspx");
                }
                else
                {
                    try
                    {
                        Model1 entities = new Model1();

                        string check = entities.Orders.Where(x => x.Id == userid && x.Title == title && x.Purchased == false).Select(x => x.Id).FirstOrDefault();
                        if (check == null)
                        {

                            int CID = Convert.ToInt32(entities.Categories.Where(x => x.Name == category).Select(y => y.CategoryID).First());
                            Order order = new Order
                            {
                                Id = userid,
                                BookID = bookid,
                                Title = title,
                                CategoryID = CID,
                                ISBN = isbn,
                                Author = author,
                                Price = price,
                                Purchased = purchased,
                                Quantity = quantity
                            };
                            entities.Orders.Add(order);
                            entities.SaveChanges();
                            Label1.Text = String.Format("{0} added sucessfully!", title);
                            Label1.Visible = true;
                            Label1.ForeColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            Label1.Text = String.Format("{0} is already in your cart!", title);
                            Label1.Visible = true;
                            Label1.ForeColor = System.Drawing.Color.Red;
                        }

                    }


                    catch (Exception exp)
                    {
                        Response.Write(exp.ToString());
                    }

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string title = TextBox1.Text;

            Model1 context = new Model1();

            var query = (from x in context.Books
                         join y in context.Categories on
                         x.CategoryID equals y.CategoryID
                         where x.Title.ToUpper().Contains(title.ToUpper())
                         select new
                         {
                             BookID = x.BookID,
                             Title = x.Title,
                             Category = y.Name,
                             ISBN = x.ISBN,
                             Author = x.Author,
                             Stock = x.Stock,
                             Price = x.Price
                         }).ToList();
            GridView1.DataSource = query;
            GridView1.DataBind();
            Label2.Text = String.Format("{0} book(s) found.", query.Count.ToString());
        }
    }
}