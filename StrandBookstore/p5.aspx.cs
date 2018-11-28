using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using StrandBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrandBookstore
{
    public partial class p5 : System.Web.UI.Page
    {
        void Init_Roles() //a method to populate the roles in EF
        {
            ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>(); //get the EF context in (Models/IdentityModels.cs)
            if (DbContext.Roles.ToList<IdentityRole>().Count == 0) //There is no roles in the list
            {
                string[] roles = { "Owner", "User" };
                foreach (string r in roles)
                {
                    DbContext.Roles.Add(new IdentityRole() { Name = r }); // add the role Owner and User in the roles array into the EF
                }
                DbContext.SaveChanges();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init_Roles(); //populate roles in EF

                ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>();//get the EF context in (Models/IdentityModels.cs)
                var l1 = DbContext.Users.ToList<IdentityUser>(); //populate dropdownlist1
                ListBox1.DataSource = l1;
                ListBox1.DataTextField = "UserName";
                ListBox1.DataValueField = "UserName";
                ListBox1.DataBind();
                if (l1.Count > 0)
                {
                    ListBox1.SelectedIndex = 0;
                }

                var r1 = DbContext.Roles.ToList<IdentityRole>();//populate dropdownlist2
                RadioButtonList1.DataSource = r1;
                RadioButtonList1.DataTextField = "Name";
                RadioButtonList1.DataValueField = "Name";
                RadioButtonList1.DataBind();
                if (r1.Count > 0)
                {
                    RadioButtonList1.SelectedIndex = 0;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>(); //create EF object
            string username = ListBox1.SelectedValue;
            string rolename = RadioButtonList1.SelectedValue;
            IdentityUser user = DbContext.Users.FirstOrDefault
                                   (u => u.UserName.Equals(username,
                                    StringComparison.CurrentCultureIgnoreCase));//allows case insensitive input in the name
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); //create manager object
            manager.AddToRole(user.Id, rolename); //add the rolename to the user
        }
    }
}