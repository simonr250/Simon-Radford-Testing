using SimonRadford.Site.Models;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SimonRadford.Site.Repositories;


namespace SimonRadford.Site
{
    [TestFixture]
    public class GenerateSchemaFixture
    {
        [Test]
        public void CanGenerateSchema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Product).Assembly);
//            cfg.AddAssembly(typeof(Manafacturer).Assembly);
 //           cfg.AddAssembly(typeof(Review).Assembly);
            new SchemaExport(cfg).Execute(false, true, false);
        }
       
    
    }
}