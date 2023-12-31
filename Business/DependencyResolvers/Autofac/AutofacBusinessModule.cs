using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.AdoNet;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearchManager>().As<ISearchService>();
            builder.RegisterType<AnSearchDal>().As<ISearchDal>();
            
            builder.RegisterType<AuthorManager>().As<IAuthorService>();
            builder.RegisterType<AnAuthorDal>().As<IAuthorDal>();
            
            builder.RegisterType<InstituteManager>().As<IInstituteService>();
            builder.RegisterType<AnInstituteDal>().As<IInstituteDal>();
            
            builder.RegisterType<KeywordManager>().As<IKeywordService>();
            builder.RegisterType<AnKeywordDal>().As<IKeywordDal>();
            
            builder.RegisterType<KeywordsThesisManager>().As<IKeywordsThesisService>();
            builder.RegisterType<AnKeywordsThesisDal>().As<IKeywordsThesisDal>();
            
            builder.RegisterType<LanguageManager>().As<ILanguageService>();
            builder.RegisterType<AnLanguageDal>().As<ILanguageDal>();
            
            builder.RegisterType<LocationManager>().As<ILocationService>();
            builder.RegisterType<AnLocationDal>().As<ILocationDal>();
            
            builder.RegisterType<SubjectTopicManager>().As<ISubjectTopicService>();
            builder.RegisterType<AnSubjectTopicDal>().As<ISubjectTopicDal>();
            
            builder.RegisterType<SubjectTopicsThesisManager>().As<ISubjectTopicsThesisService>();
            builder.RegisterType<AnSubjectTopicsThesisDal>().As<ISubjectTopicsThesisDal>();
            
            builder.RegisterType<SupervisorManager>().As<ISupervisorService>();
            builder.RegisterType<AnSupervisorDal>().As<ISupervisorDal>();
            
            builder.RegisterType<SupervisorsThesisManager>().As<ISupervisorsThesisService>();
            builder.RegisterType<AnSupervisorsThesisDal>().As<ISupervisorsThesisDal>();
            
            builder.RegisterType<ThesisManager>().As<IThesisService>();
            builder.RegisterType<AnThesisDal>().As<IThesisDal>();
            
            builder.RegisterType<UniversityManager>().As<IUniversityService>();
            builder.RegisterType<AnUniversityDal>().As<IUniversityDal>();
            
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
            
        }
    }
}
