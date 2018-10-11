using Microsoft.EntityFrameworkCore.Storage;
using SimbaToursEastAfrica.Domain.Models;
using SimbaToursEastAfrica.Services.EmailServices.Interfaces;
using SimbaToursEastAfrica.UnitOfWork.Concretes;
using SimbaToursEastAfrica.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimbaToursEastAfrica.ServicesEndPoint.GeneralSevices
{
    public class ServicesEndPoint
    {
        SimbaToursEastAfricaUnitOfWork _simbaToursUnitOfWork;
        IMailService _emailServices;
        public ServicesEndPoint(IUnitOfWork simbaToursUnitOfWork, IMailService emailServices)
        {
            _simbaToursUnitOfWork = simbaToursUnitOfWork as SimbaToursEastAfricaUnitOfWork;
            _emailServices = emailServices;
        }

        public bool BookSafariPackage(TourClient tourClient, Item[] combinedMeal, Item[] combinedLaguage)
        {
            using (IDbContextTransaction transaction = _simbaToursUnitOfWork.SimbaToursEastAfricaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var tourClientModel = new TourClient {DateCreated = DateTime.Now, ClientFirstName = tourClient.ClientFirstName, ClientLastName = tourClient.ClientLastName, HotelId = tourClient.Hotel.HotelId, GrossTotalCosts = tourClient.GrossTotalCosts, HasRequiredVisaStatus = tourClient.HasRequiredVisaStatus, Nationality = tourClient.Nationality, NumberOfIndividuals = tourClient.NumberOfIndividuals, EmailAddress = tourClient.EmailAddress };
                    _simbaToursUnitOfWork._tourClientRepository.Insert(tourClientModel);
                    _simbaToursUnitOfWork.SaveChanges();
                    tourClient.TourClientId = tourClientModel.TourClientId;
                    foreach (var vehicle in tourClient.Vehicles)
                    {
                        _simbaToursUnitOfWork._vehicleRepository.Insert(vehicle);
                        vehicle.TourClientId = tourClientModel.TourClientId;
                        _simbaToursUnitOfWork.SaveChanges();
                    }
                    var hotel = tourClient.Hotel;
                    var location = hotel.Location;
                    var ht = new HotelBooking
                    {
                        TourClientId = tourClientModel.TourClientId,
                        LocationId = location.LocationId,
                        HotelName = hotel.HotelName,
                        AccomodationCost = tourClient.GrossTotalCosts,
                        HasMealsIncluded = hotel.HasMealsIncluded,
                        HotelPricingId = hotel.HotelPricing.HotelPricingId,
                    };
                    _simbaToursUnitOfWork._hotelBookingRepository.Insert(ht);
                    _simbaToursUnitOfWork.SaveChanges();

                    var laguage = new Laguage { TourClientId = tourClientModel.TourClientId, LaguagePricingId = (combinedLaguage.Length > 0) ? (int)combinedLaguage[0].laguagePricing.LaguagePricingId : 1 };
                    _simbaToursUnitOfWork._laguageRepository.Insert(laguage);
                    _simbaToursUnitOfWork.SaveChanges();


                    var invoice = new Invoice { InvoiceName = string.Format("Combined Laguage Invoice for Client {0}", tourClientModel.TourClientId), TourClientId = tourClientModel.TourClientId };

                    _simbaToursUnitOfWork._invoiceRepository.Insert(invoice);
                    _simbaToursUnitOfWork.SaveChanges();

                    foreach (var item in combinedLaguage)
                    {
                        var it = new Item();
                        it.Quantity = item.Quantity;
                        it.MealId = null;
                        it.LaguageId = laguage.LaguageId;
                        it.ItemType = item.ItemType;
                        it.InvoiceId = invoice.InvoiceId;
                        it.laguagePricingId = laguage.LaguagePricingId;
                        it.mealPricingId = null;
                        _simbaToursUnitOfWork._itemRepository.Insert(it);
                        _simbaToursUnitOfWork.SaveChanges();

                        invoice.InvoicedItems.Add(it);
                        _simbaToursUnitOfWork.SaveChanges();
                    }


                    var meal = new Meal { TourClientId = tourClientModel.TourClientId, MealPricingId = combinedMeal.Length > 0 ?combinedMeal[0].mealPricing.MealPricingId : 1 };
                    _simbaToursUnitOfWork._mealRepository.Insert(meal);
                    _simbaToursUnitOfWork.SaveChanges();
                    var mealInvoice = new Invoice { InvoiceName = string.Format("Combined Meal Invoice for Client {0}", tourClientModel.TourClientId), TourClientId = tourClientModel.TourClientId };

                    _simbaToursUnitOfWork._invoiceRepository.Insert(mealInvoice);
                    _simbaToursUnitOfWork.SaveChanges();
                    foreach (var mealItem in combinedMeal)
                    {
                        var mealIt = new Item();
                        mealIt.MealId = meal.MealId;
                        mealIt.LaguageId = null;
                        mealIt.ItemType = ItemType.Meal;
                        mealIt.Quantity = mealItem.Quantity;
                        mealIt.InvoiceId = mealInvoice.InvoiceId;
                        mealIt.mealPricingId = meal.MealPricingId;
                        mealIt.laguagePricingId = null;
                        _simbaToursUnitOfWork._itemRepository.Insert(mealIt);
                        _simbaToursUnitOfWork.SaveChanges();
                        mealInvoice.InvoicedItems.Add(mealIt);
                        _simbaToursUnitOfWork.SaveChanges();
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public bool UpdateHotelPricing(HotelPricing hotelPricing)
        {
            try
            {
                _simbaToursUnitOfWork._hotelPricingRepository.Update(hotelPricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Location GetHotelLocationByHotelId(int hotelId)
        {
            var hotel = _simbaToursUnitOfWork._hotelRepository.GetById(hotelId);
            var location = _simbaToursUnitOfWork._locationRepository.GetById(hotel.LocationId);
            return location;
        }

        public Location[] GetAllHotelLocations()
        {
            var location = _simbaToursUnitOfWork._locationRepository.GetAll().ToArray();
            return location;
        }

        public bool UpdateSchedulesPricing(SchedulesPricing schedulesPricing)
        {
            try
            {
                _simbaToursUnitOfWork._schedulesPricingRepository.Update(schedulesPricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateDealPricing(DealsPricing dealsPricing)
        {
            try
            {
                _simbaToursUnitOfWork._dealsPricingRepository.Update(dealsPricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateLocation(Location location)
        {
            try
            {
                _simbaToursUnitOfWork._locationRepository.Update(location);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public TourClient GetTourClient(string emailAddress)
        {
            var tourClient = (from t in _simbaToursUnitOfWork.SimbaToursEastAfricaDbContext.TourClients.Include("Hotel").
               Include("Hotel.Location").Include("Hotel.Location.Address")
                              where t.EmailAddress.ToLower().Equals(emailAddress.ToLower())
                              select t).FirstOrDefault<TourClient>();
            return tourClient;
        }
        public TourClient GetTourClientById(int id)
        {
            var tourClient = (from t in _simbaToursUnitOfWork.SimbaToursEastAfricaDbContext.TourClients.Include("Hotel").
                Include("Hotel.Location").Include("Hotel.Location.Address")
                              where t.TourClientId == id
                              select t).FirstOrDefault<TourClient>();
          
            return tourClient;
        }
        public bool UpdateMealPricing(MealPricing mealPricing)
        {
            try
            {
                _simbaToursUnitOfWork._mealsPricingRepository.Update(mealPricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                _simbaToursUnitOfWork._vehicleRepository.Update(vehicle);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void SavePayment(TourClient tourClient, decimal amountToPay)
        {
            tourClient.CurrentPayment = amountToPay;
            tourClient.DateUpdated = DateTime.Now;
            _simbaToursUnitOfWork._tourClientRepository.Update(tourClient);
            _simbaToursUnitOfWork.SaveChanges();
        }

        public bool UpdateLaguagePricing(LaguagePricing laguagePricing)
        {
            try
            {
                _simbaToursUnitOfWork._laguagePricingRepository.Update(laguagePricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public TransportPricing[] GetTransportPricing()
        {
            return _simbaToursUnitOfWork._transportPricingRepository.GetAll().ToList().ToArray();
        }

        public Address GetHotelAddressById(int addressId)
        {
            return _simbaToursUnitOfWork._addressRepository.GetById(addressId);
        }

        public HotelPricing GetHotelPricingById(int hotelPricingId)
        {
            return _simbaToursUnitOfWork._hotelPricingRepository.GetById(hotelPricingId);
        }

        public bool UpdateHotel(Hotel hotel)
        {
            try
            {
                _simbaToursUnitOfWork._hotelRepository.Update(hotel);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Location GetLocationById(int locationId)
        {
            return _simbaToursUnitOfWork._locationRepository.GetById(locationId);
        }

        public Hotel GetHotelDetails(int hotelId)
        {
            var hotel = _simbaToursUnitOfWork._hotelRepository.GetById(hotelId);
            return hotel;
        }
        public Hotel[] GetAllHotelDetails()
        {
            var hotels = _simbaToursUnitOfWork._hotelRepository.GetAll().ToArray<Hotel>();
            return hotels;
        }

        public bool PostHotel(Hotel hotel)
        {
            try
            {
                using (IDbContextTransaction transaction = _simbaToursUnitOfWork.SimbaToursEastAfricaDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (hotel.HotelPricing.HotelPricingId < 1)
                        {
                            _simbaToursUnitOfWork._hotelPricingRepository.Insert(hotel.HotelPricing);
                            _simbaToursUnitOfWork.SaveChanges();
                            hotel.HotelPricingId = hotel.HotelPricing.HotelPricingId;
                        }
                        if (hotel.Location.Address.AddressId < 1)
                        {
                            _simbaToursUnitOfWork._addressRepository.Insert(hotel.Location.Address);
                            _simbaToursUnitOfWork.SaveChanges();
                        }
                        if (hotel.Location.LocationId < 1)
                        {
                            _simbaToursUnitOfWork._locationRepository.Insert(hotel.Location);
                            _simbaToursUnitOfWork.SaveChanges();
                            hotel.LocationId = hotel.Location.LocationId;
                        }
                        if (hotel.HotelId < 1)
                        {
                            var tmpHotel = new Hotel { HotelName = hotel.HotelName, HotelPricingId = hotel.HotelPricingId, LocationId = hotel.LocationId, HasMealsIncluded = hotel.HasMealsIncluded };
                            _simbaToursUnitOfWork._hotelRepository.Insert(tmpHotel);
                            _simbaToursUnitOfWork.SaveChanges();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PostVehicle(Vehicle vehicle)
        {
            try
            {
                _simbaToursUnitOfWork._vehicleRepository.Insert(vehicle);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool PostLocation(Location location)
        {
            try
            {
                _simbaToursUnitOfWork._addressRepository.Insert(location.Address);
                _simbaToursUnitOfWork.SaveChanges();
                location.AddressId = location.Address.AddressId;
                _simbaToursUnitOfWork._locationRepository.Insert(location);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PostDealPricing(DealsPricing dealsPricing)
        {
            try
            {
                _simbaToursUnitOfWork._dealsPricingRepository.Insert(dealsPricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PostMealPricing(MealPricing mealPricing)
        {
            try
            {
                _simbaToursUnitOfWork._mealsPricingRepository.Insert(mealPricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PostLaguagePricing(LaguagePricing laguagePricing)
        {
            try
            {
                _simbaToursUnitOfWork._laguagePricingRepository.Insert(laguagePricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PostSchedulesPricing(SchedulesPricing schedulesPricing)
        {
            try
            {
                _simbaToursUnitOfWork._schedulesPricingRepository.Insert(schedulesPricing);
                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PostHotelPricing(HotelPricing hotelPricing)
        {
            try
            {
                _simbaToursUnitOfWork._hotelPricingRepository.Insert(hotelPricing);

                _simbaToursUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void UpdateClientPayments(TourClient client)
        {
            _simbaToursUnitOfWork._tourClientRepository.Update(client);
            _simbaToursUnitOfWork.SaveChanges();
        }

        public HotelPricing[] GetHotelPricing()
        {
            return _simbaToursUnitOfWork._hotelPricingRepository.GetAll().ToArray();
        }

        public DealsPricing[] GetDealsPricing()
        {
            return _simbaToursUnitOfWork._dealsPricingRepository.GetAll().ToArray();
        }


        public SchedulesPricing[] GetSchedulesPricing()
        {
            return _simbaToursUnitOfWork._schedulesPricingRepository.GetAll().ToArray();
        }

        public LaguagePricing[] GetLaguagePricing()
        {
            return _simbaToursUnitOfWork._laguagePricingRepository.GetAll().ToArray();
        }
        public MealPricing[] GetMealPricing()
        {
            return _simbaToursUnitOfWork._mealsPricingRepository.GetAll().ToArray();
        }
    }
}
