using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Enyim.Caching.Configuration;
using ServiceStack.Caching;
using ServiceStack.Caching.Memcached;

namespace FurryBear.Installers
{
    public class MemcachedInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICacheClient>().Instance(new MemcachedClientCache()).LifeStyle.PerWebRequest);
        }
    }
}