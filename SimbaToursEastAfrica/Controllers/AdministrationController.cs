using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbaToursEastAfrica.Services.EmailServices.Concretes;
using SimbaToursEastAfrica.Services.EmailServices.Interfaces;
using SimbaToursEastAfrica.UnitOfWork.Interfaces;
using SimbaToursEastAfrica;
using SimbaToursEastAfrica.Domain.Models;
using SimbaToursEastAfrica.Models;
using Microsoft.AspNetCore.Authorization;

namespace SimbaToursEastAfrica.Controllers
{
    [Produces("application/json")]
    [Route("api/Administration")]
    public class AdministrationController : Controller
    {
        private IMailService _emailService;
        private IUnitOfWork _simbaToursUnitOfWork;
        private ServicesEndPoint.GeneralSevices.ServicesEndPoint _serviceEndPoint;

        public AdministrationController(IMailService emailService, IUnitOfWork simbaToursUnitOfWork)
        {
            _emailService = emailService;
            _simbaToursUnitOfWork = simbaToursUnitOfWork;

        }
        
       [HttpGet]
        [Route("GetHotelAddressById")]
        public JsonResult GetHotelAddressById(int addressId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            Address result = _serviceEndPoint.GetHotelAddressById(addressId);

            return Json(result);
        }

        [HttpGet]
        [Route("GetHotelPricingById")]
        public JsonResult GetHotelPricingById(int hotelPricingId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            HotelPricing result = _serviceEndPoint.GetHotelPricingById(hotelPricingId);

            return Json(result);
        }
        
        [HttpGet]
        [Route("GetLocationByHotelId")]
        public JsonResult GetHotelLocationById(int locationId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            Location result = _serviceEndPoint.GetLocationById(locationId);

            return Json(result);
        }
        [HttpGet]
        [Route("GetLaguagePricingById")]
        public JsonResult GetLaguagePricingById(int laguagePricingId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            var result = _serviceEndPoint.GetLaguagePricing().FirstOrDefault(p => p.LaguagePricingId == laguagePricingId);

            return Json(result);
        }

        [HttpGet]
        [Route("GetMealPricingById")]
        public JsonResult GetMealPricingById(int mealPricingId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            var result = _serviceEndPoint.GetMealPricing().FirstOrDefault(p => p.MealPricingId == mealPricingId);

            return Json(result);
        }
        [HttpGet]
        [Route("GetDealsPricingById")]
        public JsonResult GetDealsPricingById(int dealPricingId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            var result = _serviceEndPoint.GetDealsPricing().FirstOrDefault(p => p.DealsPricingId == dealPricingId);

            return Json(result);
        }
        [HttpGet]
        [Route("GetSchedulesPricingById")]
        public JsonResult GetSchedulesPricingById(int schedulesPricingId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            var result = _serviceEndPoint.GetSchedulesPricing().FirstOrDefault(p => p.SchedulesPricingId == schedulesPricingId);

            return Json(result);
        }
        [HttpGet]
        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        [Route("PostDealPricing")]
        public IActionResult PostDealPricing([FromBody] DealsPricing dealsPricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.PostDealPricing(dealsPricing);

            return Json(result);
        }
        [HttpPost]
        [Route("UpdateDealPricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult UpdateDealPricing([FromBody] DealsPricing dealsPricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.UpdateDealPricing(dealsPricing);

            return Json(result);
        }
        [HttpPost]
        [Route("PostMealPricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult PostMealPricing([FromBody] MealPricing mealPricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.PostMealPricing(mealPricing);
             
            return Json(new { Result = result });
        }

        [HttpPost]
        [Route("UpdateMealPricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult UpdateMealPricing([FromBody] MealPricing mealPricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.UpdateMealPricing(mealPricing);

            return Json(new { Result = result });

        }
        [HttpPost]
        [Route("PostLaguagePricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult PostLaguagePricing([FromBody] LaguagePricing laguagePricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.PostLaguagePricing(laguagePricing);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("UpdateLaguagePricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult UpdateLaguagePricing([FromBody] LaguagePricing laguagePricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.UpdateLaguagePricing(laguagePricing);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("PostSchedulesPricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult PostSchedulesPricing([FromBody] SchedulesPricing schedulesPricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.PostSchedulesPricing(schedulesPricing);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("UpdateSchedulesPricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult UpdateSchedulesPricing([FromBody] SchedulesPricing schedulesPricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.UpdateSchedulesPricing(schedulesPricing);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("PostHotelPricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult PostHotelPricing([FromBody] HotelPricing hotelPricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.PostHotelPricing(hotelPricing);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("UpdateHotelPricing")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult UpdateHotelPricing([FromBody] HotelPricing hotelPricing)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.UpdateHotelPricing(hotelPricing);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("PostLocation")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult PostLocation([FromBody] Location location)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.PostLocation(location);

            return Json(new { Result = result });
        }

        [HttpPost]
        [Route("PostLocation")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult UpdateLocation([FromBody] Location location)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.UpdateLocation(location);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("PostVehicle")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult PostVehicle([FromBody] Vehicle vehicle)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.PostVehicle(vehicle);

            return Json(new { Result = result });
        }

        [HttpPost]
        [Route("UpdateVehicle")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult UpdateVehicle([FromBody] Vehicle vehicle)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.UpdateVehicle(vehicle);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("PostHotel")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult PostHotel([FromBody] Hotel hotel)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.PostHotel(hotel);

            return Json(new { Result = result });
        }
        [HttpPost]
        [Route("UpdateHotel")]
        [Authorize(Roles = ("Administrator"))]
        public IActionResult UpdateHotel([FromBody] Hotel hotel)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            bool result = _serviceEndPoint.UpdateHotel(hotel);

            return Json(new { Result = result });
        }
        // GET: api/Administration
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Administration/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Administration
        [HttpPost]
        [Route("PostEmail")]
        public void PostEmail(IMailService emailService, IUnitOfWork simbaToursUnitOfWork)
        {
            _emailService = emailService;
            _simbaToursUnitOfWork = simbaToursUnitOfWork;
        }

        // PUT: api/Administration/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
