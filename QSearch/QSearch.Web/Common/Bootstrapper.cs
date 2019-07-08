using QSearch.Core;
using QSearch.Core.Impl;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Lifetime;

namespace QSearch.Web.Common
{    

    public static class Bootstrapper
    {
        private static IUnityContainer _container;

        public static IUnityContainer Initialise()
        {
            _container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));
            return _container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IStackExchangeApiConsumer, StackExchangeApiClientBasedOnHttpClient>(new ContainerControlledLifetimeManager())
                    .RegisterType<IQuestSearchService, QuestSearchService>(new ContainerControlledLifetimeManager())
                    .RegisterType<ICacheServiceAsync, MongoDbBasedCacheServiceAsync>(new ContainerControlledLifetimeManager());
            return container;
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}