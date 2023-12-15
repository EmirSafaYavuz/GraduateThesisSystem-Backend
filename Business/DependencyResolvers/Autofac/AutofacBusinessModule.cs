using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Context;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorManager>().As<IAuthorService>();
            builder.RegisterType<EfAuthorDal>().As<IAuthorDal>();
            
            builder.RegisterType<InstituteManager>().As<IInstituteService>();
            builder.RegisterType<EfInstituteDal>().As<IInstituteDal>();
            
            builder.RegisterType<KeywordManager>().As<IKeywordService>();
            builder.RegisterType<EfKeywordDal>().As<IKeywordDal>();
            
            builder.RegisterType<KeywordsThesisManager>().As<IKeywordsThesisService>();
            builder.RegisterType<EfKeywordsThesisDal>().As<IKeywordsThesisDal>();
            
            builder.RegisterType<LanguageManager>().As<ILanguageService>();
            builder.RegisterType<EfLanguageDal>().As<ILanguageDal>();
            
            builder.RegisterType<LocationManager>().As<ILocationService>();
            builder.RegisterType<EfLocationDal>().As<ILocationDal>();
            
            builder.RegisterType<SubjectTopicManager>().As<ISubjectTopicService>();
            builder.RegisterType<EfSubjectTopicDal>().As<ISubjectTopicDal>();
            
            builder.RegisterType<SubjectTopicsThesisManager>().As<ISubjectTopicsThesisService>();
            builder.RegisterType<EfSubjectTopicsThesisDal>().As<ISubjectTopicsThesisDal>();
            
            builder.RegisterType<SupervisorManager>().As<ISupervisorService>();
            builder.RegisterType<EfSupervisorDal>().As<ISupervisorDal>();
            
            builder.RegisterType<SupervisorsThesisManager>().As<ISupervisorsThesisService>();
            builder.RegisterType<EfSupervisorsThesisDal>().As<ISupervisorsThesisDal>();
            
            builder.RegisterType<ThesisManager>().As<IThesisService>();
            builder.RegisterType<EfThesisDal>().As<IThesisDal>();
            
            builder.RegisterType<UniversityManager>().As<IUniversityService>();
            builder.RegisterType<EfUniversityDal>().As<IUniversityDal>();
            
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<MyDbContext>().InstancePerLifetimeScope();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
