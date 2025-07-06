using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Extensions;
using System.Data;
using webapi.Models.Context._Interface_;
using webapi.Models.Persons;
using webapi.Models.Persons._Service_;
using webapi.Models.Products;
using webapi.Models.Products._Service_;

namespace webapi.Controllers
{
	[ApiController, Route("api/home")]
	public class HomeController : Controller
	{
        private readonly IMemoryCache _cache;
        private readonly IServ<Ticket> _ticket;
		private readonly IServ<Service> _service;
		private readonly IBookedServ _booking;
		private readonly ICustomerServ _customer;

		public HomeController(IServ<Ticket> ticket,
			IServ<Service> service,IBookedServ booking,
			ICustomerServ customer, IMemoryCache cache)
		{
			_ticket = ticket;
			_service = service;
			_booking = booking;
			_customer = customer;
            _cache = cache;
        }
        
        // *** Секция Бронь ***
        [Authorize(Roles ="Клиент"), 
			HttpGet("get/booking")]
		public async Task<IActionResult> GetBooking() {...}
	...

}