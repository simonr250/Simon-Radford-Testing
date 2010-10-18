using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SimonRadford.Site.Models;

namespace SimonRadford.Site.Repositories
{
    public class SubmitterRepository : ISubmitterRepository
    {
        public void Add(Submitter user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(user);
                transaction.Commit();
            }
        }

        public void Update(Submitter user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(user);
                transaction.Commit();
            }
        }

        public void Remove(Submitter user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(user);
                transaction.Commit();
            }
        }
        public IList<Submitter> List()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                return session.CreateCriteria(typeof(Submitter))
                    .List<Submitter>();
            }
        }

        public Submitter GetByUserId(int userId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Submitter>(userId);
        }

        public Submitter GetByName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var submitter = session
                    .CreateCriteria(typeof(Submitter))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Submitter>();
                return submitter;
            }
        }
        
        public int CheckExistingNamesAdd(string name)
        {
            int result = 0;
            bool userFound = false;
            foreach (Submitter s in List())
            {
                if (name == s.Name)
                {
                    userFound = true;
                    result = s.UserId;
                    break;
                }
            }
            if (!userFound)
            {
                Add(new Submitter {Name = name});
                result = GetByName(name).UserId;
            }

            return result;
        }

    }
}