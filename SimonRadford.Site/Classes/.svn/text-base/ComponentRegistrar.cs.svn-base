using Castle.Windsor;
using SimonRadford.Site.Models;
using SimonRadford.Site.Repositories;

namespace SimonRadford.Site.Classes
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            container.AddComponent<IManafacturerRepository, ManafacturerRepository>();
            container.AddComponent<IProductRepository, ProductRepository>();
            container.AddComponent<IReviewRepository, ReviewRepository>();
            container.AddComponent<ISubmitterRepository, SubmitterRepository>();
        }
    }
}