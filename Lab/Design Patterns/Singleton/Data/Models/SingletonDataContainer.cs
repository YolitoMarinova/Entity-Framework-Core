namespace Singleton.Data.Models
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    using Singleton.Data.Interfaces;

    public class SingletonDataContainer : ISingletonContainer
    {
        private Dictionary<string, int> capitals = new Dictionary<string, int>();

        private static SingletonDataContainer instance = new SingletonDataContainer();

        private SingletonDataContainer()
        {
            Console.WriteLine("Initializing singleton object");

            var elements = File.ReadAllLines("../../../Datasets/capitals.txt");

            for (int i = 0; i < elements.Length; i += 2)
            {
                capitals.Add(elements[i], int.Parse(elements[i+1]));
            }
        }

        public static SingletonDataContainer Instance => instance;

        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }
}
