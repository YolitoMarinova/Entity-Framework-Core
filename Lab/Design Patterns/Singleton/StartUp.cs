namespace Singleton
{
    using System;
    using Singleton.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            var db = SingletonDataContainer.Instance;
            Console.WriteLine(db.GetPopulation("London"));

            var db2 = SingletonDataContainer.Instance;
            Console.WriteLine(db2.GetPopulation("Plovdiv"));

            var db3 = SingletonDataContainer.Instance;
            Console.WriteLine(db3.GetPopulation("Paris"));
        }
    }
}
