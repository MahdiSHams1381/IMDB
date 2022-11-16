﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Domain.CardViewModel
{
    public class HomeViewModel
    {
        public APIListResult<Movie> PopularMovies { get; set; }
        public APIListResult<Movie> TopRatedMovies { get; set; }
        public Movie LatestMovie { get; set; }
        public APIListResult<Person> PopularPeople { get; set; }
    }
}
