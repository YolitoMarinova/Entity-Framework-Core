namespace Composite
{
    using System;

    using Composite.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            var eyeshadow = new SingleGift("Eyeshadow", 370);
            eyeshadow.CalculateTotalPrice();

            Console.WriteLine();

            var rootBox = new CompositeGift("RootBox", 0);

            var truckToy = new SingleGift("Trucky Toy", 289);
            var plainToy = new SingleGift("Plain Toy", 587);

            rootBox.Add(truckToy);
            rootBox.Add(plainToy);

            var childBox = new CompositeGift("ChildBox", 0);

            var soliderToy = new SingleGift("Solider Toy", 200);

            childBox.Add(soliderToy);

            rootBox.Add(childBox);

            Console.WriteLine($"Total price of this composite present is: {rootBox.CalculateTotalPrice()}");
        }
    }
}
