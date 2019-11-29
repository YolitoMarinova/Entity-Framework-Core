namespace CommandPattern
{
    using CommandPattern.Commands;
    using CommandPattern.Commands.Enums;
    using CommandPattern.Commands.Interfaces;
    using CommandPattern.Data.Models;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var modifyPrice = new ModifyPrice();

            var product = new Product("Shampoo", 50);

            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 100));

            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 50));

            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Decrease, 25));

            Console.WriteLine(product);
        }

        private static void Execute(Product product, ModifyPrice modifyPrice, ICommand productCommand)
        {
            modifyPrice.SetCommand(productCommand);

            modifyPrice.Invoke();
        }
    }
}
