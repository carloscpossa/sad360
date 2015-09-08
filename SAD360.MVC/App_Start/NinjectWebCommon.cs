[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SAD360.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SAD360.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace SAD360.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using SAD360.Application.Interfaces;
    using SAD360.Application;
    using SAD360.Domain.Interfaces.Services;
    using SAD360.Domain.Services;
    using SAD360.Domain.Interfaces.Repositories;
    using SAD360.Infra.Data.Repositories;    

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
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
            //Application
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind<IAdministradorAppService>().To<AdministradorAppService>();
            kernel.Bind<IAlternativaAppService>().To<AlternativaAppService>();
            kernel.Bind<IAvaliacaoAppService>().To<AvaliacaoAppService>();
            kernel.Bind<IFuncionarioAppService>().To<FuncionarioAppService>();
            kernel.Bind<IGerenteAppService>().To<GerenteAppService>();
            kernel.Bind<IQuestaoAppService>().To<QuestaoAppService>();
            kernel.Bind<IQuestionarioAppService>().To<QuestionarioAppService>();

            //Services da camada de Domínio
            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IAdministradorService>().To<AdministradorService>();
            kernel.Bind<IAlternativaService>().To<AlternativaService>();
            kernel.Bind<IAvaliacaoService>().To<AvaliacaoService>();
            kernel.Bind<IFuncionarioService>().To<FuncionarioService>();
            kernel.Bind<IGerenteService>().To<GerenteService>();
            kernel.Bind<IQuestaoService>().To<QuestaoService>();
            kernel.Bind<IQuestionarioService>().To<QuestionarioService>();

            //Repositórios
            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IAdministradorRepository>().To<AdministradorRepository>();
            kernel.Bind<IAlternativaRepository>().To<AlternativaRepository>();
            kernel.Bind<IAvaliacaoRepository>().To<AvaliacaoRepository>();
            kernel.Bind<IFuncionarioRepository>().To<FuncionarioRepository>();
            kernel.Bind<IGerenteRepository>().To<GerenteRepository>();
            kernel.Bind<IQuestaoRepository>().To<QuestaoRepository>();
            kernel.Bind<IQuestionarioRepository>().To<QuestionarioRepository>();

        }        
    }
}
