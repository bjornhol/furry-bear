using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Polly;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;

namespace FurryBear.Installers
{
    public class RavenInstaller : IWindsorInstaller
    {
        const string CONNECTION_STRING_NAME = "RavenHQ";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDocumentStore>().Instance(CreateDocumentStore()).LifeStyle.Singleton,
                Component.For<IDocumentSession>().UsingFactoryMethod(GetDocumentSesssion).LifeStyle.PerWebRequest
                );
        }

        static IDocumentStore CreateDocumentStore()
        {
            DocumentStore store = new DocumentStore { ConnectionStringName = CONNECTION_STRING_NAME };

            return Policy
                .Handle<WebException>()
                .Retry(1, (exception, i, arg3) =>
            {
                Trace.WriteLine(string.Format("Failed to get database service. This was retry {0}. Exception catched: {1}", i, exception.Message));
            }).Execute(() =>
            {
                store.Initialize();

                var o = store.DatabaseCommands.Get("111");

                return store;
            });
        }

        static IDocumentSession GetDocumentSesssion(IKernel kernel)
        {
            return kernel.Resolve<IDocumentStore>().OpenSession();
        }
    }
}