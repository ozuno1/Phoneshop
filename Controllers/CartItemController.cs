using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
        [Route("cart")]
        public class CartController : Controller
        {

        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("index")]
            public IActionResult Index()
            {
                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
                return View();
            }

            [Route("buy/{id}")]
            public IActionResult Buy(string id)
            {
                Product product = _context.Product.Find(int.Parse(id));
                if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
                {
                    List<CartItem> cart = new List<CartItem>();
                    cart.Add(new CartItem { Product = product, Quantity = 1 });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                else
                {
                    List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                    int index = isExist(id);
                    if (index != -1)
                    {
                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new CartItem { Product = product, Quantity = 1 });
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                return RedirectToAction("Index");
            }

            [Route("remove/{id}")]
            public IActionResult Remove(string id)
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return RedirectToAction("Index");
            }

            private int isExist(string id)
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Product.Id.Equals(int.Parse(id)))
                    {
                        return i;
                    }
                }
                return -1;
            }
        [Route("checkout")]
        public IActionResult CheckOut()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }

        public ActionResult ProcessOrder(IFormCollection frc)
        {
            List<CartItem> stCart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            var order = new Order()
            {
                CustomerName = frc["cusName"],
                CustomerPhone = frc["cusPhone"],
                CustomerEmail = frc["cusEmail"],
                CustomerAddress = frc["cusAddress"],
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (CartItem cart in stCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.Id,
                    ProductId = cart.Product.Id,
                    Quantity = cart.Quantity,
                    Price = (float)cart.Product.Price
                };
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
                ViewBag.Msg = "Thank you for your order, we will contact you as soon as possible!";
            }
            HttpContext.Session.Remove("Cart");
            return View("CheckOut");
        }
    }
    }
