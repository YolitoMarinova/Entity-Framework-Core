namespace Facade
{
    using Facade.Data.Models;

    public class CarAddressBuilder : CarBuilderFacade
    {
        public CarAddressBuilder(Car car)
        {
            Car = car;
        }

        public CarAddressBuilder InCity(string cityName)
        {
            Car.City = cityName;

            return this;
        }

        public CarAddressBuilder AtAddress(string address)
        {
            Car.Address = address;

            return this;
        }
    }
}
