namespace Facade
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var car = new CarBuilderFacade()
                .Info
                .WithType("BMW")
                .WithColor("grey")
                .WithNumberOfDoors(4)
                .Built
                .InCity("Malibu")
                .AtAddress("Happy House 333")
                .Build();

            Console.WriteLine(car);
        }
    }
}
