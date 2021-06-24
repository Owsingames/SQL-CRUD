using SQLMovieList.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SQLMovieList
{
    class Program
    {
        static void Main(string[] args)
        {
            //CRUD -- Create, Read, Update, Delete/Destroy

            //import the Database - using SQLMoiveList.Models;
            //create new Datebase object to work with models
            MovieListContext db = new MovieListContext();

            //list of movie objects from DB table Movies
            List<Movie> movieList = db.Movies.ToList();

            //print all movie in list -- wokring
            PrintMovies(movieList, db);
            Console.WriteLine();

            //search movie by Id -- working
            Console.WriteLine("Select a Movie by Id:");
            Movie m = SearchMovieById(7, db);
            Console.WriteLine(m.Title);
            Console.WriteLine();

            //search move by title -- working
            Console.WriteLine("Search Movie by title:");
            List<Movie> myMovie = SearchMovieByTitle("Gladiator", db);
            PrintMovies(myMovie, db);
            Console.WriteLine();

            //add new movie to list/table -- working
            //Movie newMovie = new Movie() { Title = "UP", Genre = "Animated", Runtime = 90 };
            //AddMovie(newMovie, db);

            //Update a movie in the list/table -- working
            //UpdateMovie(13, db);

            //Delete a movie from list/table -- working
            //DeleteMovie(13, db);
        }

        //Read -- print out all movies from movies table
        public static void PrintMovies(List<Movie> movieList, MovieListContext db)
        {
            if (movieList.Count == 0)
            {
                Console.WriteLine("There are no movies currently in the list");
            }

            foreach (Movie m in movieList) 
            {
                Console.WriteLine($"{m.Id}) {m.Title}, {m.Genre}, {m.Runtime}");
            }
        }

        //Read -- search for movie by Id
        //pass in index/primary key number, and Database object
        public static Movie SearchMovieById(int id, MovieListContext db)
        {
            try 
            {
                //find a movie in the DB by Id, store that item in a Movie object
                Movie m = db.Movies.Find(id);
                //return the movie object
                return m;
            }
            catch (NullReferenceException)
            {
                //return null/nothing if no movie was found
                return null;
            }
        }

        //Read -- search for movie by title/name
        //pass in title and DB object
        public static List<Movie> SearchMovieByTitle(string title, MovieListContext db)
        {
            List<Movie> movieList = db.Movies.Where(x => x.Title == title).ToList();
            return movieList;
        }

        //Create -- add a new movie to the list
        public static void AddMovie(Movie newMovie, MovieListContext db)
        {
            //Insert a movie into the SQL table and Add movie to List
            db.Movies.Add(newMovie);

            //this will save the new movie to the list and SQL table
            db.SaveChanges();
        }

        //Update -- this will allow changes and updates made the movie objects in the list
        //int id = id is the movie you want to update
        //must pass Database to make changes
        public static void UpdateMovie(int id, MovieListContext db)
        {
            //find the movie by id
            Movie m = db.Movies.Find(id);

            //Update/change movie
            m.Title = "Test - title";
            m.Genre = "Test - genre";
            m.Runtime = 111;

            //select movies table and update selected movie
            db.Movies.Update(m);
            //will save changes in the DB
            db.SaveChanges();
        }

        //Delete -- Warning deleting in permanent
        //int id = movie selected to delete
        //must pass DB to reference Database
        public static void DeleteMovie(int id, MovieListContext db)
        {
            //get the movie by id/index
            Movie m = db.Movies.Find(id);

            //remove movie and save changes
            db.Movies.Remove(m);
            db.SaveChanges();
        }
    }
}
