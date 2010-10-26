using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SimonRadford.Site.Models;

namespace SimonRadford.Site.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public void Add(Review review)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(review);
                transaction.Commit();
            }
        }

        public void Update(Review review)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(review);
                transaction.Commit();
            }
        }

        public void Remove(Review review)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(review);
                transaction.Commit();
            }
        }

        public IList<Review> List(int productId)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                return session.CreateCriteria(typeof(Review)).Add(Restrictions.Eq("ProductId", productId))
                    .List<Review>();
            }
        }

        public Review GetById(int reviewId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Review>(reviewId);
        }

        public int GetAverageProductRating(int productId)
        {
            var result = 0;
            var total = 0;
            var count = 0;
            using (var session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                foreach (var r in
                    session.CreateCriteria(typeof(Review)).Add(Restrictions.Eq("ProductId", productId))
                        .List<Review>())
                {
                    count++;
                    total = total + r.Rating; 
                }
            }
            if (count>0) result = total/count;
            return result;
        }

        public IList<Review> ListFlagged()
        {
            using (var session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                return session.CreateCriteria(typeof(Review)).Add(Restrictions.Eq("Flagged", true))
                    .List<Review>();
            }
        }

        public IList<Review> SearchFlagged(string word)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var reviewSearchList = session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Like("DetailedReview", "%" + word + "%") && Restrictions.Eq("Flagged", true) )
                    .List<Review>();
                return reviewSearchList;
            }
        }
    }
}