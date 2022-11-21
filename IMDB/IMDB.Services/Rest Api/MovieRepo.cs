﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IMDB.Domain.DTOs;
using Infrastructure;
using Newtonsoft.Json;
using TMDbLib.Objects;
using TMDbLib.Objects.Authentication;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Genres;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Trending;
using static System.Net.Mime.MediaTypeNames;
using static IMDB.Domain.DTOs.ApiDTO;
using static IMDB.Domain.DTOs.MovieDTO;

namespace IMDB.Services.Api
{
    public class MovieRepo : IMovie
    {
        private const string key = "646f26e9bfd4f042f28a7160726dd239";
     
        private readonly string api_key = $"api_key={key}";
        private readonly IUser _user;

        public MovieRepo(IUser user)
        {
            _user = user;   
        }
        public MovieRepo()
        {
        }
        public async Task<GenreContainer> GetAllGenre()
        {
            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/genre/movie/list?{api_key}&language=en-US";
                         
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var Respons = Newtonsoft.Json.JsonConvert.DeserializeObject<TMDbLib.Objects.Genres.GenreContainer>(strContent);
                return Respons;
            }
            return null;
        }

        public async Task<Movie> GetMovieById(int id)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{id}?{api_key}";
                          
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                Movie movie = Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(strContent);
                return movie;
            }
            return null;
        }


        public async Task<TMDbLib.Objects.Movies.Credits> GetMovieCreditsById(int id)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{id}/credits?{api_key}&language=en-US";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var Credits = Newtonsoft.Json.JsonConvert.DeserializeObject<TMDbLib.Objects.Movies.Credits>(strContent);
                return Credits;
            }
            
            return null;
        }

        public async Task<APIListResult<Review>> GetReviewsOfMovieById(int id,int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{id}/reviews?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Review> movieReview = Newtonsoft.Json.JsonConvert.DeserializeObject<APIListResult<Review>>(strContent);
                return movieReview;
            }
            return null;
        }

        public  async Task<TrailersResult> GetVideoById(int id)
        {

            HttpClient httpClient = new HttpClient();
            string path = $"https://api.themoviedb.org/3/movie/{id}/videos?{api_key}&language=en-US";
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await  httpClient.GetAsync(path);
            TrailersResult trailers = new TrailersResult();
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();
                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();
                trailers = Newtonsoft.Json.JsonConvert.DeserializeObject<TrailersResult>(strContent);
                return trailers;
            }
            return null;
        }

        public async Task<APIListResult<Movie>> GetPopularMovies(int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/popular?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Movie> popMovies = Newtonsoft.Json.JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                return popMovies;
            }
            return null;
        }

        public async Task<APIListResult<Movie>> GetTrendingMovies(MediaType mediaType , TimeWindow period,int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/trending/{mediaType}/{period}?{api_key}&page={page}";
            path = path.ToLower();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Movie> popMovies = Newtonsoft.Json.JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                return popMovies;
            }
            return null;
        }


        public async Task<APIListResult<Person>> GetPopularPeople(int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/person/popular?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Person> popPeople = Newtonsoft.Json.JsonConvert.DeserializeObject<APIListResult<Person>>(strContent);
                
                return popPeople;
            }
            return null;
        }

        public async Task<Movie> GetLatestMovies()
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/latest?{api_key}&language=en-US";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                Movie popMovies = Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(strContent);
                return popMovies;
            }
            return null;
        }

        public async Task<APIListResult<Movie>> GetTopRatedMovies(int page = 1)
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/top_rated?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await  httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                APIListResult<Movie> popMovies = Newtonsoft.Json.JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                return popMovies;
            }
            return null;
        }

        public async Task<bool> RateMovie(int movieId,int userId, double rate,string SessionId)
        {
            try
            {
                var user = _user.GetUserById(userId);
                HttpClient httpClient = new HttpClient();


                string path = $"https://api.themoviedb.org/3/movie/{movieId}/rating?{api_key}&guest_session_id={SessionId}";

                #region Request Body
                var requestData = new Dictionary<string, string>
                {
                    {
                  "value", rate.ToString("0.0")
                }
                };

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(path),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(requestData)
                };
                #endregion


                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStreamAsync();

                    using Stream receiveStream = res;
                    using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string strContent = readStream.ReadToEnd();

                    var respons = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(strContent);
                    throw new Exception(respons.status_message);
                }
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<APIListResult<Movie>> SearchMovies(string txt, int page = 1)
        {
            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/search/movie?{api_key}&query={txt}&language=en-US&page={page}&include_adult=false";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var movie = Newtonsoft.Json.JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                return movie;
            }
            return null;
        }

        public async Task<Person> GetPersonDetailes(int person_id)
        {
            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/person/{person_id}?{api_key}&language=en-US";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var person = Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(strContent);
                return person;
            }
            return null;
        }

        public async Task<APIListResult<Movie>> SimilarMovies(int movieId, int page = 1)
        {
            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/movie/{movieId}/similar?{api_key}&language=en-US&page={page}";

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var movie = Newtonsoft.Json.JsonConvert.DeserializeObject<APIListResult<Movie>>(strContent);
                return movie;
            }
            return null;
        }

        public async Task<GuestSession> CreateSession()
        {

            HttpClient httpClient = new HttpClient();


            string path = $"https://api.themoviedb.org/3/authentication/guest_session/new?{api_key}";

            
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStreamAsync();

                using Stream receiveStream = res;
                using StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string strContent = readStream.ReadToEnd();

                var respons = Newtonsoft.Json.JsonConvert.DeserializeObject<TMDbLib.Objects.Authentication.GuestSession>(strContent);
                return respons;
            }

            return null;

        }


        //        public void test()
        //        {
        //            var client = new HttpClient();

        //            var requestData = new Dictionary<string, string>
        //{
        //    {
        //  "value", "8.5"
        //}
        //};

        //            var request = new HttpRequestMessage()
        //            {
        //                RequestUri = new Uri($"https://api.themoviedb.org/3/authentication/token/new?api_key={api_key}"),
        //                Method = HttpMethod.Get,
        //                Content = new FormUrlEncodedContent(requestData)
        //            };
        //            //request.Headers.Add("Token", ""); // Add or modify headers

        //            var response = client.SendAsync(request);

        //            // To read the response as string
        //            var responseString = await response.Content.ReadAsStringAsync();

        //            // To read the response as json
        //            //var responseJson = await response.Content.ReadAsStringAsync<ResponseObject>();
        //        }
    }
}
