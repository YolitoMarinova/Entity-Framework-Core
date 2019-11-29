namespace TemplatePattern
{
    using System;
    using TemplatePattern.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            Sourdough sourdoughBread = new Sourdough();
            sourdoughBread.Make();

            Console.WriteLine();

            TwelveGrain twelveGrainBread = new TwelveGrain();
            twelveGrainBread.Make();

            Console.WriteLine();

            WholeWheat wholeWheatBread = new WholeWheat();
            wholeWheatBread.Make();
        }
    }
}
