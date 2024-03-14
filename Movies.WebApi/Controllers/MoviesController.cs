﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpPost("movies")]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Genres = request.Genres.ToList(),
                YearOfRelease = request.YearOfRelease
            };

            var result = await _movieRepository.CreateAsync(movie);
            return Created($"/api/movies/{movie.Id}", movie);
        }
    }
}
