﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data.BackEnd;

namespace CinemaConsole.Data.Employee
{
    public class Movies
    {
        private int Mid { get; set; }

        private string Mname { get; set; }

        private int Myear { get; set; }

        private int Mage { get; set; }

        private string Msumm { get; set; }

        private string Mactors { get; set; }

        private ChangeData Database { get; set; }
        
        public List<DateTimeHall> DateTimeHallsList { get; set; } = new List<DateTimeHall>();
        
        /// <summary>
        /// The movie class with all data
        /// </summary>
        /// <param name="name">Name of the movie</param>
        /// <param name="year">Year of the movie</param>
        /// <param name="age">Year  of the movie</param>
        /// <param name="summary">Summary of the movie</param>
        /// <param name="actors">Actors of the movie (split by ",")</param>
        public Movies(string name, int year, int age, string summary, string actors)
        {
            Mname = name;
            Myear = year;
            Mage = age;
            Msumm = summary;
            Mactors = actors;
            Mid = MovieID();
            Database = new ChangeData();
        }
        /// <summary>
        /// Creating a public turple to get the movie info so you dont have to touch the private ints and strings
        /// </summary>
        public Tuple<int, string, int, int, string, string> getMovieInfo()
        {
            int idd = Mid;
            string name = Mname;
            int year = Myear;
            int age = Mage;
            string summary = Msumm;
            string actors = Mactors;

            return Tuple.Create(idd, name, year, age, summary, actors);
        }

        /// <summary>
        /// Creating a public tuple to set the movie info so you dont have to touch the private ints and strings, this is only for editting movies, not for adding them.
        /// </summary>
        public void setMovieInfo(string name = "" , int year = -1, int age = -1, string sum = "", string actors = "")
        {
            Database.UpdateMovie(Mid, name, year, age, sum);
            // only change these if a value is given
            if (name != "")
            {
                Mname = name;
            }
            if (year != 0)
            {
                Myear = year;
            }
            if (age != 0)
            {
                Mage = age;
            }

            // these can be skipped seperatly, only change these if a value is given
            if (sum != "")
            {
                Msumm = sum;
            }

            if (actors != "")
            {
                Mactors = actors;
            }
        }

        /// <summary>
        /// Creating a new unique ID and checking for missing ID's
        /// </summary>
        private int MovieID()
        {
            int idd;
            for (int i = 0; i < MovieList.movieList.Count; i++){
                idd = i + 1;
                if (MovieList.movieList[i].getMovieInfo().Item1 != idd)
                {
                    return idd;
                }
            }
            return MovieList.movieList.Count + 1;
        }

        public void orderList()
        {
            List<DateTimeHall> orderedList = DateTimeHallsList.OrderBy(dateId => dateId.getDateInfo().Item1).ToList();
            DateTimeHallsList = orderedList;
        }
    }
}
