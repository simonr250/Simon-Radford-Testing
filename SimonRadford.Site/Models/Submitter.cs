using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SimonRadford.Site.Models
{
    public class Submitter
    {
        public virtual int UserId { get; set; }

        [Required(ErrorMessage="You need to enter a name.")]
        public virtual string Name { get; set; }

        public virtual IList Reviews { get; set; }
    }
}
