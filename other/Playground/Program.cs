using System;
using System.Linq;
using Raven.Client;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var results = session
                    .Query<Product, Products_SearchByName>()
                    .Search(r => r.Name, "guarana")
                    .ToList();

                foreach (var result in results)
                {
                    Console.WriteLine(result.Name);
                }
                Console.ReadLine();
            }
        }
    }
}
