using SimbaToursEastAfrica.DataAccess;
using SimbaToursEastAfrica.Domain.Models;
using SimbaToursEastAfrica.Services.RepositoryServices.Abstracts;
using SimbaToursEastAfrica.Services.RepositoryServices.Concretes;
using SimbaToursEastAfrica.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.UnitOfWork.Concretes
{
    public class SimbaToursEastAfricaUnitOfWork : IUnitOfWork
    {
        public AddressRepository _addressRepository;
        public DestinationRepository _destinationRepository;
        public DriverRepository _driverRepository;
        public HotelBookingRepository _hotelBookingRepository;
        public InAndOutBoundAirTravelRepository _inAndOutBoundAirTravelRepository;
        public InternalVehicleTravelRepository _internalVehicleTravelRepository;
        public InvoiceRepository _invoiceRepository;
        public ItemRepository _itemRepository;
        public ItinaryRepository _itinaryRepository;
        public LaguageRepository _laguageRepository;
        public LocationRepository _locationRepository;
        public MealRepository _mealRepository;
        public ScheduleRepository _scheduleRepository;
        public TourClientRepository _tourClientRepository;
        public VehicleRepository _vehicleRepository;
        public MealsPricingRepository _mealsPricingRepository;
        public DealsPricingRepository _dealsPricingRepository;
        public LaguagePricingRepository _laguagePricingRepository;
        public SchedulesPricingRepository _schedulesPricingRepository;
        public HotelPricingRepository _hotelPricingRepository;
        public HotelRepository _hotelRepository;
        public SimbaToursEastAfricaDbContext SimbaToursEastAfricaDbContext { get; set; }
        public SimbaToursEastAfricaUnitOfWork(
            AbstractRepository<Address> addressRepository,
            AbstractRepository<Destination> destinationRepository,
            AbstractRepository<Driver> driverRepository,
            AbstractRepository<HotelBooking> hotelBookingRepository,
            AbstractRepository<InAndOutBoundAirTravel> inAndOutBoundAirTravelRepository,
            AbstractRepository<InternalVehicleTravel> internalVehicleTravelRepository, 
            AbstractRepository<Invoice> invoiceRepository, 
            AbstractRepository<Item> itemRepository, 
            AbstractRepository<Itinary> itinaryRepository, 
            AbstractRepository<Laguage> laguageRepository, 
            AbstractRepository<Location> locationRepository, 
            AbstractRepository<Meal> mealRepository, 
            AbstractRepository<Schedule> scheduleRepository, 
            AbstractRepository<TourClient> tourClientRepository, 
            AbstractRepository<Vehicle> vehicleRepository,
            AbstractRepository<SchedulesPricing> schedulesPricingRepository,
            AbstractRepository<MealPricing> mealPricingRepository,
            AbstractRepository<DealsPricing> dealsPricingRepository,
            AbstractRepository<LaguagePricing> laguagePricingRepository,
            AbstractRepository<HotelPricing> hotelPricingRepostory,
            AbstractRepository<Hotel> hotelRepostory,
            Microsoft.EntityFrameworkCore.DbContext simbaToursEastAfricaDbContext)
        {
            SimbaToursEastAfricaDbContext = simbaToursEastAfricaDbContext as SimbaToursEastAfricaDbContext;
            _addressRepository = addressRepository as AddressRepository;
            _addressRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _destinationRepository = destinationRepository as DestinationRepository;
            _destinationRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _driverRepository = driverRepository as DriverRepository;
            _driverRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _hotelBookingRepository = hotelBookingRepository as HotelBookingRepository;
            _hotelBookingRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _inAndOutBoundAirTravelRepository = inAndOutBoundAirTravelRepository as InAndOutBoundAirTravelRepository;
            _inAndOutBoundAirTravelRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _internalVehicleTravelRepository = internalVehicleTravelRepository as InternalVehicleTravelRepository;
            _internalVehicleTravelRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _invoiceRepository = invoiceRepository as InvoiceRepository;
            _invoiceRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _itemRepository = itemRepository as ItemRepository;
            _itemRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _itinaryRepository = itinaryRepository as ItinaryRepository;
            _itinaryRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _laguageRepository = laguageRepository as LaguageRepository;
            _laguageRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _locationRepository = locationRepository as LocationRepository;
            _locationRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _mealRepository = mealRepository as MealRepository;
            _mealRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _scheduleRepository = scheduleRepository as ScheduleRepository;
            _scheduleRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _tourClientRepository = tourClientRepository as TourClientRepository;
            _tourClientRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _vehicleRepository = vehicleRepository as VehicleRepository;
            _vehicleRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _mealsPricingRepository = mealPricingRepository as MealsPricingRepository;
            _mealsPricingRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _dealsPricingRepository = dealsPricingRepository as DealsPricingRepository;
            _dealsPricingRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _laguagePricingRepository = laguagePricingRepository as LaguagePricingRepository;
            _laguagePricingRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _schedulesPricingRepository = schedulesPricingRepository as SchedulesPricingRepository;
            _schedulesPricingRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _hotelPricingRepository = hotelPricingRepostory as HotelPricingRepository;
            _hotelPricingRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
            _hotelRepository = hotelRepostory as HotelRepository;
            _hotelRepository.SimbaToursEastAfricaDbContext = SimbaToursEastAfricaDbContext;
        }
        public void SaveChanges()
        {
            SimbaToursEastAfricaDbContext.SaveChanges();
        }
    }
}
