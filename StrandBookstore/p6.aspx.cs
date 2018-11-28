using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrandBookstore
{
    public partial class p6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAuthorDropDownList();

                // set the grid view data source
                // BindGrid shows all authors and their books if no author is provided
                BindGrid();

                // set the drop down list of category for inserting a new book
                insert_ddl_categoryID.DataSource = BusinessLogic.ListCategories();
                insert_ddl_categoryID.DataTextField = "Name";
                insert_ddl_categoryID.DataValueField = "CategoryID";
                insert_ddl_categoryID.DataBind();
            }         
        }

        // Bind the author list
        protected void BindAuthorDropDownList()
        {
            // drop down list for searching the particular author
            var authorNameList = BusinessLogic.ListAuthor();
            ddl_author.DataSource = authorNameList;
            ddl_author.DataBind();
            ddl_author.Items.Insert(0, new ListItem("", ""));
            //if (authorList.Count > 0 && author != null)
            //{
            //    ddl_author.SelectedValue = author;
            //}
        }

        // DataBind GridView1 with specific book
        private void BindGrid()
        {
            string author = Request.QueryString["author"];
            GridView1.DataSource = BusinessLogic.ListBooks(author);
            GridView1.DataBind();
        }

        //Initialization of editable DataRow controls
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
           (e.Row.RowState & DataControlRowState.Edit) > 0) //data row selected for edit is going to be edited
            {
                Book book = (Book)e.Row.DataItem; //get the particular editable row we are updating

                // Category
                DropDownList dp = (DropDownList)e.Row.FindControl("DropDownList1"); //at this row i want to find the particular control then cast it as (dropdownlist)
                if (dp != null)
                {
                    dp.DataSource = BusinessLogic.ListCategories(); //populate the dropdownlist
                    dp.DataTextField = "Name";
                    dp.DataValueField = "CategoryID";
                    dp.DataBind();
                    dp.SelectedValue = book.CategoryID.ToString(); //set the current selection
                }

                // ISBN
                TextBox dp2 = (TextBox)e.Row.FindControl("TextBox7");
                if (dp2 != null)
                {
                    dp2.Text = book.ISBN.ToString();
                }

                // Stock
                TextBox dp3 = (TextBox)e.Row.FindControl("TextBox5"); 
                if (dp3 != null)
                {
                    dp3.Text = book.Stock.ToString();
                }

                // Price
                TextBox dp4 = (TextBox)e.Row.FindControl("TextBox6"); 
                if (dp4 != null)
                {
                    dp4.Text = book.Price.ToString();
                }
            }
        }

        // Edit a GridView row
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //get into edit mode, editindex is based on the row which edit is selected
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            row.BackColor = System.Drawing.Color.Yellow;
        }

        //Cancel editing a GridView row
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();
            GridViewRow row = GridView1.Rows[e.RowIndex];
            row.BackColor = System.Drawing.Color.White;
        }

        //Updating changes made to a GridView row
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];

            try
            {
                int bookid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]); //e.rowindex is the row we are editing, we get the orderid based on the row. .Values[0] means the first key, .Values[1] would mean a second key
                int categoryid = Int32.Parse((row.FindControl("DropDownList1") as DropDownList).SelectedValue);//parse is convert string to int
                string ISBN = (row.FindControl("TextBox7") as TextBox).Text; //use find control as RadioButtonList1 is a dynamically inserted control, something not known at design time
                int stock = Int32.Parse((row.FindControl("TextBox5") as TextBox).Text);
                decimal price = decimal.Parse((row.FindControl("TextBox6") as TextBox).Text);

                BusinessLogic.Updatebook(bookid, ISBN, categoryid, stock, price);
            }
            catch
            {

            }
            GridView1.EditIndex = -1; //coming out of the edit mode, as when its -1, should not be pointing to any row with edit selected.
            BindGrid();
            row.BackColor = System.Drawing.Color.White;
        }

        //Delete a GridView row
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            BusinessLogic.Deletebook(bookid);
            BindGrid();
        }

        // Drop down list selected index changed
        protected void ddl_author_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddl_author.SelectedValue))
            {
                string authorName = ddl_author.SelectedValue;

                Response.Redirect($"~/p6.aspx?author={authorName}");
            }
            else
            {
                Response.Redirect("~/p6.aspx");
            }
        }

        // Add a new book
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    string title = insert_tb_title.Text;
                    string author = insert_tb_author.Text;
                    string ISBN = insert_tb_ISBN.Text;
                    int category = Int32.Parse(insert_ddl_categoryID.SelectedValue);
                    int stock = Convert.ToInt32(insert_tb_stock.Text);
                    decimal price = Convert.ToDecimal(insert_tb_price.Text);

                    BusinessLogic.AddBook(title, author, ISBN, category, stock, price);
                    Label_Validation.Text = $"insert [{title}] successful";

                    Response.Redirect("~/p6.aspx?Author=" + author);
                }
                catch
                {
                    Label_Validation.Text = "insert failed";
                }
            }
            else
            {
                Label_Validation.Text = "input error";
            }
        }

        //Validate new book details input
        protected bool ValidateInput()
        {
            bool ret = true;

            try
            {
                string title = insert_tb_title.Text;
                string author = insert_tb_author.Text;
                string ISBN = insert_tb_ISBN.Text;

                if (string.IsNullOrEmpty(title)
                    || string.IsNullOrEmpty(author)
                    || string.IsNullOrEmpty(ISBN))
                {
                    ret = false;
                }
                else
                {
                    int category = Int32.Parse(insert_ddl_categoryID.SelectedValue);
                    int stock = Convert.ToInt32(insert_tb_stock.Text);
                    decimal price = Convert.ToDecimal(insert_tb_price.Text);
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        protected void ButtonViewAllBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/p6.aspx");
        }
    }
}