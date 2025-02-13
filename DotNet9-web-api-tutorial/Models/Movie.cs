﻿namespace DotNet9_web_api_tutorial.Models
{
    public class Movie : EntityBase
    {
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public DateTimeOffset ReleaseDate { get; private set; }
        public double Rating { get; private set; }

        private Movie()
        {
            Title = string.Empty;
            Genre = string.Empty;
        }

        private Movie(string title, string genre, DateTimeOffset releaseDate, double rating)
        {
            Title= title;
            Genre = genre;
            ReleaseDate = releaseDate;
            Rating = rating;
        }

        public static Movie Create(string title, string genre, DateTimeOffset releaseDate, double rating)
        {
            ValidateInputs(title, genre, releaseDate, rating);
            return new Movie(title, genre, releaseDate, rating);
        }

        public void Update(string title, string genre, DateTimeOffset releaseDate, double rating)
        {
            ValidateInputs(title, genre, releaseDate, rating);

            Title = title;
            Genre = genre;
            ReleaseDate = releaseDate;
            Rating = rating;

            //UpdateLastModified();
        }

        private static void ValidateInputs(string title, string genre, DateTimeOffset releaseDate, double rating)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Title cannot be null or empty", nameof(title));
            }

            if (string.IsNullOrEmpty(genre))
            {
                throw new ArgumentNullException("Genre cannot be null or empty", nameof(genre));
            }

            if (releaseDate > DateTimeOffset.UtcNow)
            {
                throw new ArgumentException("Release Date cannot be in the future", nameof(releaseDate));
            }

            if (rating < 0 || rating > 10)
            {
                throw new ArgumentException("Rating value should be between 0 and 10", nameof(rating));
            }
        }
    }
}
