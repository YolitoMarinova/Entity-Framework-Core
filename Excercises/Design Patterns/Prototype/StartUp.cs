namespace Prototype
{
    using Prototype.Data.Models;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            SandwichMenu sandwichMenu = new SandwichMenu();

            sandwichMenu["BLT"] = new Sandwich("Wheat", "Bacon", "", "Lettuce, Tomato");
            sandwichMenu["PB&J"] = new Sandwich("White", "", "", "Peanut Butter, Jelly");
            sandwichMenu["Turkey"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");

            sandwichMenu["LoadedBLT"] = new Sandwich("Wheat", "Turkey, Bacon", "American", "Lettuce, Tomato, Onion, Olives");
            sandwichMenu["ThreeMeatCombo"] = new Sandwich("Rye", "Turkey, Ham, Salami", "Provolone", "Lettuce, Onion");
            sandwichMenu["Vegetarian"] = new Sandwich("Wheat", "", "", "Lettuce, Onion, Tomato, Olives, Spinach");

            Sandwich firstCloneSandwich = sandwichMenu["BLT"].Clone() as Sandwich;
            Sandwich secondCloneSandwich = sandwichMenu["ThreeMeatCombo"].Clone() as Sandwich;
            Sandwich thirdCloneSandwich = sandwichMenu["PB&J"].Clone() as Sandwich;
        }
    }
}
