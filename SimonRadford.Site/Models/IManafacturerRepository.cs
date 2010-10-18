using System.Collections.Generic;

namespace SimonRadford.Site.Models
{
    public interface IManafacturerRepository
    {
        void Add(Manafacturer manafacturer);
        void Update(Manafacturer manafacturer);
        void Remove(Manafacturer manafacturer);
        IList<Manafacturer> List();
        Manafacturer GetByManafacturerId(int manafacturerId);
        Manafacturer GetByName(string name);
        IList<string> DistinctNamesList();
        int CheckExistingNamesAdd(string manafacturerName);
        IList<Manafacturer> Search(string word);
    }
}
