namespace TemplatePattern.Data.Models
{
    using System;

    public class WholeWheat : Bread
    {
        public override void MixIngredients()
        {
            Console.WriteLine("Gathering Ingredients for Whole Wheat Bread.");
        }

        public override void Bake()
        {
            Console.WriteLine("Baking Whole Wheat Bread. (15 minutes)");
        }
    }
}
