using Entities;

namespace Utility;

public class ValidatorResponse : IResponse
{
    public bool Success { get; set; }
    public String Message { get; set; }

    public ValidatorResponse()
    {
        Success = true;
        Message = string.Empty;
    }
}

public class MovieValidator : IValidator<Movie>
{
    public IResponse Validate(Movie movie)
    {
        IResponse response = new ValidatorResponse();

        if (String.IsNullOrEmpty(movie.Title))
        {
            response.Success = false;
            response.Message = "Movie title is required";
        }

        if (String.IsNullOrEmpty(movie.Summary))
        {
            response.Success = false;
            response.Message = "Movie summary is required";
        }

        return response;
    }
}


/*
public class ReviewValidator : IValidator<Review>
{
    public IResponse Validate(Review review)
    {
        if (String.IsNullOrEmpty(review.ReviewerName)) return false;
        if (String.IsNullOrEmpty(review.Headline)) return false;
        if (review.Rating < 0 || review.Rating > 5) return false;
        return true;
    }
}
*/