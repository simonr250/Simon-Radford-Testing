using System.Collections.Generic;

namespace SimonRadford.Site.Models
{
    public interface ISubmitterRepository
    {
        void Add(Submitter user);
        void Update(Submitter user);
        void Remove(Submitter user);
        IList<Submitter> List();
        Submitter GetByUserId(int userId);
        Submitter GetByName(string name);

        int CheckExistingNamesAdd(string submitterName);
    }
}
