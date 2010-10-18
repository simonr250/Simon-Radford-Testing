using System.Collections.Generic;

namespace SimonRadford.Site.Models
{
    public interface IReviewRepository
    {
        void Add(Review review);
        void Update(Review review);
        void Remove(Review review);
        IList<Review> List(int productId);
        Review GetById(int reviewId);
        int GetAverageProductRating(int productId);
    }
}
