using System.Collections.Generic;
using System.Linq;
using NHibernate;
using SimonRadford.Site.Models;
using NHibernate.Criterion;

namespace SimonRadford.Site.Repositories
{
    public class ManafacturerRepository : IManafacturerRepository
    {
        public void Add(Manafacturer manafacturer)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(manafacturer);
                transaction.Commit();
            }
        }

        public void Update(Manafacturer manafacturer)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(manafacturer);
                transaction.Commit();
            }
        }

        public void Remove(Manafacturer manafacturer)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(manafacturer);
                transaction.Commit();
            }
        }
        public IList<Manafacturer> List()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                return session.CreateCriteria(typeof(Manafacturer))
                    .List<Manafacturer>();
            }
        }

        public IList<string> DistinctNamesList()
        {
            return List().Select(m => m.Name).ToList();
        }

        public Manafacturer GetByManafacturerId(int manafacturerId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Manafacturer>(manafacturerId);
        }

        public Manafacturer GetByName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var manafacturer = session
                    .CreateCriteria(typeof(Manafacturer))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Manafacturer>();
                return manafacturer;
            }
        }

        public int CheckExistingNamesAdd(string name)
        {
            var result = 0;
            var userFound = false;
            foreach (var m in List().Where(m => name == m.Name))
            {
                userFound = true;
                result = m.ManafacturerId;
                break;
            }
            if (!userFound)
            {
                Add(new Manafacturer { Name = name });
                result = GetByName(name).ManafacturerId;
            }

            return result;
        }

        public IList<Manafacturer> Search(string word)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var manafacturerSearchList = session
                    .CreateCriteria(typeof(Manafacturer))

                    .Add(Restrictions.Like("Name", "%" + word + "%") )
                    .List<Manafacturer>();
                return manafacturerSearchList;
            }
        }
    }
}