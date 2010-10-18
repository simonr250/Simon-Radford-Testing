using System.Collections;

namespace SimonRadford.Site.Models
{
    public class Product
    {
        public virtual int ProductId { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string Name {get; set; }       
        public virtual int ManafacturerId { get; set; }
        public virtual string ManafacturerName { get; set; }
        public virtual double Price { get; set; }
        public virtual string Description { get; set; }

        public virtual IList Reviews { get; set; }
    }
}