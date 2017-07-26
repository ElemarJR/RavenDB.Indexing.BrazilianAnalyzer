using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Playground
{
    public class Products_SearchByName : AbstractIndexCreationTask<Product>
    {
        public Products_SearchByName()
        {
            Map = products =>
                from p in products
                select new
                {
                    p.Name
                };

            Index(entry => entry.Name, FieldIndexing.Analyzed);
        }
    }
}