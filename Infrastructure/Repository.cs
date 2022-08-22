using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public class Repository :  IRepository
{
    private DbContextOptions<RepositoryDbContext> _opts;

    public Repository()
    {
        _opts = new DbContextOptionsBuilder<RepositoryDbContext>()
            .UseSqlite("Data source=..//GUI/db.db").Options;
        //Reset();
    }

    public void Reset()
    {
        using( var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }

    public List<Review> GetReviews()
    {
        using (var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            return context.ReviewTable.Include(r => r.Movie).ToList();
        }
    }

    public List<Movie> GetMovies()
    {
        var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped);
        return context.MovieTable.ToList();
    }

    public Movie DeleteMovie(int movieId)
    {
        using( var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            Movie movie = new Movie() { Id = movieId };
            context.MovieTable.Remove(movie);
            context.SaveChanges();
            return movie;
        }
    }

    public Review DeleteReview(int reviewId)
    {
        using( var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            Review review = new Review() { Id = reviewId };
            context.ReviewTable.Remove(review);
            context.SaveChanges();
            return review;
        }
    }

    public Movie AddMovie(Movie movie)
    {
        using( var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            context.MovieTable.Add(movie);
            context.SaveChanges();
            return movie;
        }
    }

    public Review AddReview(Review review)
    {
        using( var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            
            var m = context.MovieTable.Find(review.MovieId);
            review.Movie = m;

            context.ReviewTable.Add(review);
            context.SaveChanges();
            return review;
        }
    }
}