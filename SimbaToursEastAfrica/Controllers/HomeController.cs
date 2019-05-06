using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SimbaToursEastAfrica.Services.EmailServices.Concretes;
using SimbaToursEastAfrica.Services.EmailServices.Interfaces;
using SimbaToursEastAfrica.UnitOfWork.Interfaces;
using SimbaToursEastAfrica;
using SimbaToursEastAfrica.Models;
using SimbaToursEastAfrica.Domain.Models;
using SimbaToursEastAfrica.PaymentGateWay;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using UPAEventsPayPal;
using Microsoft.AspNetCore.Cors;
using System.Net.Mail;
using ApplicationConstants;
using System.IO;

namespace SimbaToursEastAfrica.Controllers
{
    //[Route("api/Home/Tours")]
    public class HomeController : Controller
    {
        private IMailService _emailService;
        private IUnitOfWork _simbaToursUnitOfWork;
        private ServicesEndPoint.GeneralSevices.ServicesEndPoint _serviceEndPoint;
        public readonly IOptions<ApplicationConstants.ApplicationConstants> _applicationConstants;
        public readonly IOptions<ApplicationConstants.twitterProfileFiguration> _twitterProfileFiguration;
        public readonly IOptions<ApplicationConstants.BusinessEmailDetails> _businessSmtpDetails;

        public HomeController(IMailService emailService, IUnitOfWork simbaToursUnitOfWork, IOptions<ApplicationConstants.ApplicationConstants> applicationConstants, IOptions<ApplicationConstants.twitterProfileFiguration> twitterProfileFiguration, IOptions<ApplicationConstants.BusinessEmailDetails> businessSmtpDetails)
        {
            _emailService = emailService;
            _simbaToursUnitOfWork = simbaToursUnitOfWork;
            _applicationConstants = applicationConstants;
            _twitterProfileFiguration = twitterProfileFiguration;
            _businessSmtpDetails = businessSmtpDetails;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize()]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult SendEmail()
        {
            try
            {
                _emailService.BusinessEmailDetails = _businessSmtpDetails; 
                //Send Email:
                _emailService.SendEmail(new EmailDao { Attachment = Request.Form.Files.Any()? Request.Form.Files[0]:null, EmailBody = Request.Form["emailBody"], EmailFrom = Request.Form["emailFrom"], EmailSubject = Request.Form["emailSubject"], EmailTo = Request.Form["emailTo"] });
                return View("Index");
            }
            catch(Exception e)
            {
                return Json(new { Result = false });
            }
        }
        [Authorize()]
        [HttpPost]
        [EnableCors(PolicyName = "CorsPolicy")]
        public IActionResult BookTour([FromBody] TourClientViewModel tourClientModel)
        {
            try
            {
                _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
                var tourClient = AutoMapper.Mapper.Map<TourClientViewModel, TourClient>(tourClientModel);

                List<Item> mealItems = new List<Item>();
                List<Item> laguageItems = new List<Item>();
                List<VehicleViewModel> vehicles = new List<VehicleViewModel>();

                var unitPayment = _serviceEndPoint.GetLaguagePricing()[0];
                var unitPaymentMeal = _serviceEndPoint.GetMealPricing()[0];
                var vehiclePayment = _serviceEndPoint.GetTransportPricing()[0];
                decimal runningCostItems =  0.00M;
                var listExtraCharges = new List<TourClientExtraCharge>();
                Array.ForEach(tourClientModel.ExtraCharges, (p) =>
                {
                    runningCostItems += p.ExtraCharges;
                    listExtraCharges.Add(new TourClientExtraCharge { ExtraCharges = p.ExtraCharges, Description = p.Description});
                });


                IterateThroughMeals(mealItems, ref runningCostItems, unitPaymentMeal, tourClientModel, tourClient);
                IterateThroughLaguageAssortments(laguageItems, mealItems, ref runningCostItems, unitPaymentMeal, unitPayment, tourClientModel, tourClient);
                IterateThroughVehicles(tourClientModel,vehiclePayment,ref runningCostItems);
               
                tourClient.GrossTotalCosts += runningCostItems;
                _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
                tourClient.DateCreated = DateTime.Now;
                tourClient.DateUpdated = DateTime.Now;
                tourClient.ExtraCharges = listExtraCharges.ToArray();

                tourClient.HasFullyPaid = false;
                var isBooked = _serviceEndPoint.BookSafariPackage(tourClient, mealItems.ToArray(), laguageItems.ToArray());
                if (isBooked)
                {
                    var tourClientFullView = _serviceEndPoint.GetTourClientById(tourClient.TourClientId);
                    var payPalRedirectUrl = ValidatePayment(tourClientFullView, tourClient.CurrentPayment);
                    payPalRedirectUrl += "&clientId=" + tourClient.TourClientId;
                    return Json(new { Result = isBooked, PayPalRedirectUrl = payPalRedirectUrl, });
                    //return Redirect(payPalRedirectUrl);
                }
                else return Json(new { Result = false, Message = "Failed To Book Tour. Please contact the administrators of the site!" });

            }
            catch(Exception e)
            {
                return Json(new {Result=false, Message=e.Message, StackTrace=e.StackTrace});
            }
        }

        private void IterateThroughMeals(IList<Item> mealItems, ref decimal runningCostItems, MealPricing unitPaymentMeal, TourClientViewModel tourClientModel, TourClient tourClient)
        {
            foreach (var it in tourClientModel.CombinedMeals.MealItems)
            {
                var actualItemCost = 0.00M;

                var itemType = (Domain.Models.ItemType)Enum.Parse<Domain.Models.ItemType>(it.ItemType.ToString());
                if (it.Quantity > 0)
                {
                    CalculateRunningItemCostMeal(itemType, tourClientModel, it, unitPaymentMeal, ref runningCostItems, ref actualItemCost);
                    mealItems.Add(new Item { mealPricing = unitPaymentMeal, mealPricingId = unitPaymentMeal.MealPricingId, ItemCost = actualItemCost, ItemId = it.ItemId,
                        ItemType = (Domain.Models.ItemType)Enum.Parse<Domain.Models.ItemType>(it.ItemType.ToString()),
                        Meal = new Meal { MealId = 0, TourClient = tourClient, TourClientId = tourClient.TourClientId }, Quantity = it.Quantity });
                }
            }
        }
        private void IterateThroughLaguageAssortments(IList<Item> laguageItems, IList<Item> mealItems, ref decimal runningCostItems, MealPricing unitPaymentMeal, LaguagePricing unitPayment, TourClientViewModel tourClientModel, TourClient tourClient)
        {
            foreach (var it in tourClientModel.CombinedLaguage.Items)
            {
                var actualItemCost = 0.00M;
                var itemType = (Domain.Models.ItemType)Enum.Parse<Domain.Models.ItemType>(it.ItemType.ToString());
                if (it.Quantity > 0)
                {
                    if (it.ItemType == Models.ItemType.Meal)
                    {
                        CalculateRunningItemCostMeal(itemType, tourClientModel, it, unitPaymentMeal, ref runningCostItems, ref actualItemCost);
                        mealItems.Add(new Item { mealPricing = unitPaymentMeal, mealPricingId = unitPaymentMeal.MealPricingId, ItemCost = actualItemCost, ItemId = it.ItemId, ItemType = (Domain.Models.ItemType)Enum.Parse<Domain.Models.ItemType>(it.ItemType.ToString()), Meal = new Meal { MealId = 0, TourClientId = tourClient.TourClientId }, Quantity = it.Quantity });
                    }
                    else
                    {
                        CalculateRunningItemCostLaguage(itemType, tourClientModel, it, unitPayment, ref runningCostItems, ref actualItemCost);
                        laguageItems.Add(new Item { laguagePricing = unitPayment, laguagePricingId = unitPayment.LaguagePricingId, ItemCost = actualItemCost, ItemId = it.ItemId, ItemType = (Domain.Models.ItemType)Enum.Parse<Domain.Models.ItemType>(it.ItemType.ToString()), Laguage = new Laguage { LaguageId = 0, TourClientId = tourClient.TourClientId }, Quantity = it.Quantity });
                    }
                }
            }
        }

        private void IterateThroughVehicles(TourClientViewModel tourClientModel, TransportPricing vehiclePayment, ref decimal runningCostItems)
        {
            foreach (var vh in tourClientModel.Vehicles)
            {
                switch (vh.VehicleType)
                {
                    case Models.VehicleType.Taxi:
                        runningCostItems += vh.ActualNumberOfPassengersAllocated * vehiclePayment.TaxiPricing;
                        break;
                    case Models.VehicleType.TourBus:
                        runningCostItems += vh.ActualNumberOfPassengersAllocated * vehiclePayment.TourBusPricing;
                        break;
                    case Models.VehicleType.MiniBus:
                        runningCostItems += vh.ActualNumberOfPassengersAllocated * vehiclePayment.MiniBusPricing;
                        break;
                    case Models.VehicleType.PickUpTrack:
                        runningCostItems += vh.ActualNumberOfPassengersAllocated * vehiclePayment.PickupTruckPricing;
                        break;
                    case Models.VehicleType.FourWheelDriveCar:
                        runningCostItems += vh.ActualNumberOfPassengersAllocated * vehiclePayment.FourByFourPricing;
                        break;

                }
            }
        }
        private void CalculateRunningItemCostMeal(Domain.Models.ItemType itemType, TourClientViewModel tourClient, ItemViewModel it, MealPricing unitPaymentMeal, ref decimal runningCostOfItems, ref decimal actualItemCost)
        {
            switch (itemType)
            {
                case Domain.Models.ItemType.Meal:
                    actualItemCost = unitPaymentMeal.Price * it.Quantity;
                    runningCostOfItems += actualItemCost;
                    break;
                default:
                    actualItemCost = 0.00M;
                    break;
            }
        }
        private void CalculateRunningItemCostLaguage(Domain.Models.ItemType itemType, TourClientViewModel tourClient, ItemViewModel it, LaguagePricing unitPayment, ref decimal runningCostOfItems, ref decimal actualItemCost)
        {
            switch (itemType)
            {
                case Domain.Models.ItemType.Laguage:
                    actualItemCost = unitPayment.UnitLaguagePrice * it.Quantity ;
                    runningCostOfItems += actualItemCost;
                    break;
                case Domain.Models.ItemType.Meal:
                    actualItemCost = unitPayment.unitMealPrice * it.Quantity - tourClient.NumberOfIndividuals;
                    runningCostOfItems += actualItemCost;
                    break;
                case Domain.Models.ItemType.MedicalTreatment:
                    actualItemCost = unitPayment.UnitMedicalPrice * it.Quantity;
                    runningCostOfItems += actualItemCost;
                    break;
                case Domain.Models.ItemType.TravelDocuments:
                    actualItemCost = unitPayment.UnitTravelDocumentPrice * it.Quantity;
                    runningCostOfItems += actualItemCost;
                    break;
                default:
                    actualItemCost = 0.00M;
                    break;
            }
        }
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
        public JsonResult GetHotelLocationByHotelId(int hotelId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            Location result = _serviceEndPoint.GetHotelLocationByHotelId(hotelId);
            Address address = _serviceEndPoint.GetHotelAddressById(result.AddressId);

            return Json(new LocationViewModel { LocationName = result.LocationName, Address = new AddressViewModel { AddressId = address.AddressId, AddressLine1 = address.AddressLine1, AddressLine2 = address.AddressLine2, Country = address.Country, PostCode = address.PostCode, Town = address.Town, PhoneNumber = address.PhoneNumber }, AddressId = address.AddressId, LocationId = result.LocationId, Country = address.Country });
        }
        public JsonResult GetHotelDetails(int hotelId)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);

            var hotel = _serviceEndPoint.GetHotelDetails(hotelId);

            return Json(hotel);
        }

        public JsonResult GetTransportPricing()
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);

            TransportPricing[] pricing = _serviceEndPoint.GetTransportPricing();

            return Json(pricing);
        }
        public JsonResult GetHotelLocations()
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            Location[] locations = _serviceEndPoint.GetAllHotelLocations();

            var locationsViewModel = new List<LocationViewModel>();
            Array.ForEach(locations,result =>
            {
                Address address = _serviceEndPoint.GetHotelAddressById(result.AddressId);
                locationsViewModel.Add(new LocationViewModel { LocationName = result.LocationName,
                    Address = new AddressViewModel { AddressId = address.AddressId, AddressLine1 = address.AddressLine1,
                        AddressLine2 = address.AddressLine2, Country = address.Country,
                        PostCode = address.PostCode, Town = address.Town, PhoneNumber = address.PhoneNumber },
                    AddressId = address.AddressId, LocationId = result.LocationId, Country = address.Country });
            });
            return Json(locationsViewModel.ToArray());
        }
        public JsonResult GetAllHotelDetails()
        {

            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);

            var hotel = _serviceEndPoint.GetAllHotelDetails();

            return Json(hotel);
        }
        public JsonResult GetDealsPricing()
        {

            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);

            DealsPricing[] deals = _serviceEndPoint.GetDealsPricing();

            return Json(deals);

        }

        public JsonResult GetLaguagePricing()
        {

            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);

            LaguagePricing[] deals = _serviceEndPoint.GetLaguagePricing();

            return Json(deals);

        }

        public JsonResult GetMealPricing()
        {

            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);

            MealPricing[] deals = _serviceEndPoint.GetMealPricing();

            return Json(deals);

        }

        public JsonResult GetSchedulesPricing(int schedulesPricingId = -1)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);

            if (schedulesPricingId < 1)
            { 
                var deal = _serviceEndPoint.GetSchedulesPricing();

                return Json(deal);
            }
            else
            {
                var deal = _serviceEndPoint.GetSchedulesPricing().SingleOrDefault(p => p.SchedulesPricingId == schedulesPricingId);

                return Json(deal);
            }

        }
        public JsonResult GetHotelPricing()
        {

            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);

            HotelPricing[] deals = _serviceEndPoint.GetHotelPricing();

            return Json(deals);

        }
        [EnableCors(PolicyName = "CorsPolicy")]
        public IActionResult MakePayment([FromBody] UserDetailViewModel userDetail)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            TourClient tourClient = _serviceEndPoint.GetTourClient(userDetail.EmailAddress);
            var payPalRedirectUrl = ValidatePayment(tourClient, userDetail.CurrentPayment);
            payPalRedirectUrl += "&clientId=" + tourClient.TourClientId;
            //return Redirect(payPalRedirectUrl);
            return Json(new { PayPalRedirectUrl= payPalRedirectUrl, PaymentCompletion = "Success", Message = "The payment will be acquired by Paypal reporting, and you will be informed by email whether successful. Wait for the email." });
        }

        private string ValidatePayment(TourClient tourClient, decimal amountToPay)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            var productArray = new List<Product>();
            if (tourClient != null)
            {
                productArray.Add(new Product
                {
                    ProductName = tourClient.Hotel.Location.LocationName + "-" + tourClient.Hotel.HotelName,
                    Amount = amountToPay,
                    HasPaidInfull = amountToPay == tourClient.GrossTotalCosts - tourClient.PaidInstallments
                });

            }
            _serviceEndPoint.SavePayment(tourClient, amountToPay);
            var paymentGateway = new PaymentGateway(_applicationConstants.Value.BaseUrl, _applicationConstants.Value.BusinessEmail, _applicationConstants.Value.SuccessUrl, _applicationConstants.Value.CancelUrl, _applicationConstants.Value.NotifyUrl, tourClient.EmailAddress);
            return paymentGateway.MakePaymentByPaypal(productArray);
        }

        public JsonResult CheckAndValidatePaypalPayments(FormCollection formsCollection)
        {
            decimal amountPaid = Decimal.Parse(formsCollection["amount"]);
            int clientId = Int32.Parse(formsCollection["clientId"]);
            try
            {
                _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
                var client = _serviceEndPoint.GetTourClientById(clientId);

                client.CurrentPayment = amountPaid;
                client.PaidInstallments += amountPaid;
                client.HasFullyPaid = client.GrossTotalCosts <= client.PaidInstallments;

                _serviceEndPoint.UpdateClientPayments(client);
                return Json(new { Result = "Success" });
            }
            catch(Exception e)
            {
                //email exception to admin email
                return Json(new { Result = "Failed" });
            }
        }
        public JsonResult GetTourClientByEmail(string emailAddress)
        {
            _serviceEndPoint = new ServicesEndPoint.GeneralSevices.ServicesEndPoint(_simbaToursUnitOfWork, _emailService);
            var tourClient = _serviceEndPoint.GetTourClient(emailAddress);
            return Json(tourClient);
        }
        public JsonResult TwitterProfileFeeds()
        {
            var caching = new SimbaToursEastAfrica.Caching.Concretes.SimbaToursEastAfricaCahing();
            
            var twitterEngine = new MartinLayooInc.SocialMedia.TwitterProfileFeed<Twitter.WidgetGroupItemList>();
            twitterEngine.TwitterProfileFiguration = this._twitterProfileFiguration;
            Twitter.WidgetGroupItemList tweets = new Twitter.WidgetGroupItemList();

            lock (_locker)
            {
                tweets = caching.GetOrSaveToCache(tweets,this._twitterProfileFiguration.Value.cachKey, this._twitterProfileFiguration.Value.cacheTimeSecs, twitterEngine.GetFeeds);


                if (tweets != null && tweets.Any())
                {
                    ViewBag.TwitterProfileFeeds = tweets;
                }
                else if (tweets == null || !tweets.Any())
                    tweets = new Twitter.WidgetGroupItemList();
            }
            return Json(tweets);

        }
        private readonly static object _locker = new object();
    }

}
