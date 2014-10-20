using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Enyim.Caching.Configuration;
using Polly;
using ServiceStack.Caching;
using ServiceStack.Caching.Memcached;

namespace FurryBear.Installers
{
    public class MemcachedInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            try
            {
                Policy.Handle<SocketException>().Retry(1, (exception, i, context) =>
                {
                    Trace.WriteLine(string.Format("Failed to get cache service. This was retry {0}. Exception catched: {1}", i, exception.Message));

                }).Execute(() =>
                {
                    container.Register(Component.For<ICacheClient>().Instance(new MemcachedClientCache()).LifeStyle.PerWebRequest);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Failed to get cache service");
            }
        }
    }
}