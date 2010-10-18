using System.Collections;

namespace SimonRadford.Site.Models
{
    public class Manafacturer
    {
        public virtual int ManafacturerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Website { get; set; }
        public virtual IList Products { get; set; }
    }
}