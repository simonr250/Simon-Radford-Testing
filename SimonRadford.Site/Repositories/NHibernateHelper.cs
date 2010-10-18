using NHibernate;
using NHibernate.Cfg;
using SimonRadford.Site.Models;

namespace SimonRadford.Site.Repositories
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {

            get
            {
                if (_sessionFactory == null)
                {
                    Configure("~/hibernate.cfg.xml");
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void Configure(string configurationPath)
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(Product).Assembly);

            _sessionFactory = configuration.BuildSessionFactory();
        }
    }
}