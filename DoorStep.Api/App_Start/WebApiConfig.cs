using DoorStep.Api.App_Start;
using DoorStep.Api.Controllers;
using DoorStep.Api.Models;
using DoorStep.Core.Implementation;
using DoorStep.Core.Repository;
using DoorStep.Infrastructure.Implementation;
using DoorStep.Infrastructure.Repository;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace DoorStep.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IBikeDetailsCore, BikeDetailsCore>(new HierarchicalLifetimeManager());
            container.RegisterType<IBikeDetails, BikeDetails>(new HierarchicalLifetimeManager());
            container.RegisterType<IBikeHomeDetailsCore, BikeHomeDetailsCore>(new HierarchicalLifetimeManager());
            container.RegisterType<IBikeHomeDetails, BikeHomeDetails>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentsCore, PaymentsCore>(new HierarchicalLifetimeManager());
            container.RegisterType<IPayments, Payments>(new HierarchicalLifetimeManager());
            container.RegisterType<IMobileApiCore, MobileApiCore>(new HierarchicalLifetimeManager());
            container.RegisterType<IMobileApi, MobileApi>(new HierarchicalLifetimeManager());
            container.RegisterType<IMobileHelper, MobileHelper>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new TokenValidationHandler());

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
