using System;
using System.Collections.Generic;
using System.Linq;
using EFCore.Models;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {
                var customer = new Customer() { Name="John Snow", Address = "Vesteros", Phone = "+77023453764"};
                var seller = new Seller(){Name="Deyneris Targarien", Address = "Essos", Phone = "+77015557777"};
                var products = new List<Product>()
                {
                    new Product {Name = "Apple Iphone XS", Code = "0001", Quantity = 10, Price = 700, Seller = seller},
                    new Product { Name= "Samsung Galaxy 10s", Code = "0001", Quantity= 43, Price = 900, Seller = seller},
                    new Product { Name= "Apple Mac Book Pro 2017", Code = "0001", Quantity= 5, Price = 1400, Seller = seller},
                    new Product { Name= "Asus Vivo Book", Code = "0001", Quantity= 10, Price = 1000, Seller = seller},
                    new Product { Name= "Acer ZenBook", Code = "0001", Quantity= 8, Price = 900, Seller = seller}
                };
                var comments = new List<Comment>()
                {
                    new Comment(){Product = products[0],Customer = customer, Content = "Very good phone!"},
                    new Comment(){Product = products[0],Customer = customer, Content = "Nice one!"},
                    new Comment(){Product = products[2],Customer = customer, Content = "Mac is the best:) Except for .NET programming :("},
                };
                var carts = new List<Cart>()
                {
                    new Cart(){Customer = customer},
                    new Cart(){Customer = customer}
                };
                var prodscarts = new List<ProductsCarts>()
                {
                    new ProductsCarts(){Product = products[0], Cart = carts[0]},
                    new ProductsCarts(){Product = products[2], Cart = carts[1]}
                };
                var orders = new List<Order>()
                {
                    new Order(){Amount = 700, Cart = carts[0], OrderDate = DateTime.Now},
                    new Order(){Amount = 1400, Cart = carts[1], OrderDate = DateTime.Now}
                };
                db.Add(customer);
                db.Add(seller);
                db.SaveChanges();
                db.AddRange(carts);
                db.AddRange(products);
                db.SaveChanges();
                db.AddRange(prodscarts);
                db.AddRange(comments);
                db.SaveChanges();
                products[0].Comments.Add(comments[0]);
                products[0].Comments.Add(comments[1]);
                products[2].Comments.Add(comments[2]);
                db.AddRange(orders);
                db.SaveChanges();

                
                Console.WriteLine("Orders made by John Snow");
                foreach (var order in db.Orders.Where(b => b.Cart.Customer.Name == "John Snow").OrderBy(b=>b.OrderDate))
                {
                    Console.WriteLine($"Amount - {order.Amount}\nDate - {order.OrderDate}\n Products:");
                    var prods = from pc in order.Cart.ProductsCarts
                        join p in db.Products on pc.ProductId equals p.Id
                        select p;
                    prods.ToList().ForEach(x => Console.WriteLine($"Name: {x.Name}, Price: {x.Price}"));
                }

                var john_prod = from c in db.Comments
                    where c.Customer.Name == "John Snow"
                    select c.Product;
                
                Console.WriteLine("Products where customer left at least one comment");
                foreach (var product in john_prod.Distinct())
                {
                    Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
                }
            }
        }
    }
}