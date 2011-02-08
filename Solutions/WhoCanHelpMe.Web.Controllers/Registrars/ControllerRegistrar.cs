namespace WhoCanHelpMe.Web.Controllers.Registrars
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using Castle.Core;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using WhoCanHelpMe.Framework.Extensions;

	#endregion

    public class ControllerRegistrar : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Assembly.GetAssembly(typeof(ControllerRegistrar)).GetExportedTypes()
                    .Where(IsController)
                    .Each(type => container.Register(Component.For(type).ImplementedBy(type).Named(type.Name.ToLower()).LifeStyle.Is(LifestyleType.Transient)));
        }

        private static bool IsController(Type type)
        {
            return typeof(IController).IsAssignableFrom(type);
        }
    }
}