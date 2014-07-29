using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Document;

namespace FurryBear
{
    public static class Store
    {
        public static DocumentStore Init()
        {
            var store = new DocumentStore
            {
                ConnectionStringName = "FurryBearDb"
            };
            
            store.Initialize();

            return store;
        }
    }
}