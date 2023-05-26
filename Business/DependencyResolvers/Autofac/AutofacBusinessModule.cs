using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Authentication;
using Business.Repositories.CategoryRepository;
using Business.Repositories.EmailRepository;
using Business.Repositories.EventRepository;
using Business.Repositories.EventRequestRepository;
using Business.Repositories.OperationClaimRepository;
using Business.Repositories.UserOperationClaimRepository;
using Business.Repositories.UserRepository;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Repositories.CategoryRepository;
using DataAccess.Repositories.EmailRepository;
using DataAccess.Repositories.EventRepository;
using DataAccess.Repositories.EventRequestRepository;
using DataAccess.Repositories.OperationClaimRepository;
using DataAccess.Repositories.UserOperationClaimRepository;
using DataAccess.Repositories.UserRepository;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<EmailManager>().As<IEmailService>();
            builder.RegisterType<EfEmailDal>().As<IEmailDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<EventManager>().As<IEventService>();
            builder.RegisterType<EfEventDal>().As<IEventDal>();

            builder.RegisterType<EventRequestManager>().As<IEventRequestService>();
            builder.RegisterType<EfEventRequestDal>().As<IEventRequestDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
