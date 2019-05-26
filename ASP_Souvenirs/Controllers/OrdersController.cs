using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_Souvenirs.Data;
using ASP_Souvenirs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace ASP_Souvenirs.Controllers
{
    [Authorize(Roles ="Admin, Member")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Orders.ToListAsync());
            return View(await _context.Orders.Include(i => i.User).OrderByDescending(i => i.OrderID).AsNoTracking().ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Sourvenir)
                .ThenInclude(b => b.Category)
                .SingleOrDefaultAsync(o => o.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles ="Admin, Member")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Member")]
        public async Task<IActionResult> CreatePost(/*[Bind("OrderID,Date,OrderStatus,GST,GrandTotal,SubTotal")] Order order*/)
        {
            var order = new Order();
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {

                ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
                List<CartItem> items = cart.GetCartItems(_context);
                List<OrderItem> details = new List<OrderItem>();
                foreach (CartItem item in items)
                {

                    OrderItem detail = CreateOrderDetailForThisItem(item);
                    detail.Order = order;
                    details.Add(detail);
                    _context.Add(detail);

                }

                order.User = user;
                order.Date = DateTime.Today;
                order.OrderStatus = OrderStatus.Waiting;
                order.SubTotal = ShoppingCart.GetCart(this.HttpContext).GetSubtotal(_context);
                order.GST = ShoppingCart.GetCart(this.HttpContext).GetTotalGST(_context);
                order.GrandTotal = ShoppingCart.GetCart(this.HttpContext).GetGrandTotal(_context);
                order.OrderItems = details;
                _context.SaveChanges();


                return RedirectToAction("Purchased", new RouteValueDictionary(
                new { action = "Purchased", id = order.OrderID }));
            }

            return View(order);
        }

        private OrderItem CreateOrderDetailForThisItem(CartItem item)
        {

            OrderItem oi = new OrderItem();
            oi.Quantity = item.ItemCount;
            oi.Sourvenir = item.Souvenir;
            oi.OrderItemPrice = item.Souvenir.Price;

            return oi;
        }

        public async Task<IActionResult> Purchased(int? id)
        {
            //await CreatePost();

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            var orderItems = _context.OrderItems
                .Where(oi => oi.Order.OrderID == order.OrderID)
                .Include(oi => oi.Sourvenir)
                .ThenInclude(b => b.Category)
                .ToList();

            order.OrderItems = orderItems;
            ShoppingCart.GetCart(this.HttpContext).ClearCart(_context);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderItems = _context.OrderItems
                .Where(oi => oi.Order.OrderID == order.OrderID)
                .Include(oi => oi.Sourvenir)
                .ToList();

            order.OrderItems = orderItems;

            return View(order);
        }


        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Orders
        [Authorize(Roles = "Admin, Member")]
        //public async Task<IActionResult> CustomerIndex()
        //{
        //    return View(await _context.Orders.Include(i => i.User).AsNoTracking().ToListAsync());
        //}

        public async Task<IActionResult> CustomerIndex()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return View(await _context.Orders.Where(o => o.User == user).Include(o => o.User).OrderByDescending(o => o.OrderID).AsNoTracking().ToListAsync());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin, Member")]
        public async Task<IActionResult> CustomerDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Sourvenir)
                .ThenInclude(b => b.Category)
                .SingleOrDefaultAsync(o => o.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
