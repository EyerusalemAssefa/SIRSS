using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using SIRSS.Domain.Repository;
using SIRSS.Domain.UnitOfWork;
using SIRSS.Web;
using SIRSS.Web.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace SIRSS.Web
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));

            kernel.Bind<ApplicationUserManager>().ToMethod(GetOwinInjection<ApplicationUserManager>);
            kernel.Bind<ApplicationSignInManager>().ToMethod(GetOwinInjection<ApplicationSignInManager>);

            kernel.Bind<IAuthenticationManager>().ToMethod(context =>
            {
                var contextBase = new HttpContextWrapper(HttpContext.Current);
                return contextBase.GetOwinContext().Authentication;
            });
        }

        private static T GetOwinInjection<T>(IContext context) where T : class
        {
            var contextBase = new HttpContextWrapper(HttpContext.Current);
            return contextBase.GetOwinContext().Get<T>();
        }
    }
}
