using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrandBookstore
{
    public class BusinessLogic
    {
        public static Model1 MydbContext = new Model1();

        public static List<Category> ListCategories()
        {
            return MydbContext.Categories.ToList();
        }
        public static List<string> ListAuthor()
        {
            return MydbContext.Books.Select(x => x.Author).ToList();
        }
        public static List<Book> ListBooks(string author)
        {
            if (author == null)
            {
                return MydbContext.Books.ToList();
            }
            else
            {
                return MydbContext.Books.Where(x => x.Author == author).ToList();
            }
        }
        public static void Updatebook(int bookid, string ISBN, int categoryID, int stock, decimal price)
        {
            Book book = MydbContext.Books.Where(p => p.BookID == bookid).First<Book>();
            book.ISBN = ISBN;
            book.CategoryID = categoryID;
            book.Stock = stock;
            book.Price = price;
            MydbContext.SaveChanges();
        }

        public static void Deletebook(int bookid)
        {
            Book book = MydbContext.Books.Where(p => p.BookID == bookid).First<Book>();
            MydbContext.Books.Remove(book);
            MydbContext.SaveChanges();

        }
        public static void AddBook(string title, string author, string ISBN, int categoryID, int stock, decimal price)
        {
            Book book = new Book
            {
                Title = title,
                CategoryID = categoryID,
                ISBN = ISBN,
                Author = author,
                Stock = stock,
                Price = price,

            };
            MydbContext.Books.Add(book);
            MydbContext.SaveChanges();
        }
        public static List<int> getQtyList()
        {
            var qtyList = MydbContext.Books.Select(x => x.Stock).ToList();
            return qtyList;
        }

        public static bool DeleteTheOrder(int id)
        {
            var order = MydbContext.Orders.FirstOrDefault(x => x.OrderID == id);
            if (order != null)
            {
                MydbContext.Orders.Remove(order);
                MydbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}