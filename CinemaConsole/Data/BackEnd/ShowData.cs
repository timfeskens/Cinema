﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using System.Data;

namespace CinemaConsole.Data.BackEnd
{
    public class ShowData : Connecter
    {
        /// <summary>
        /// show all movies from the db
        /// </summary>
        public void ShowMovies()
        {
            try
            {
                Connection.Open();
                string oString = @"SELECT * from movie";
                MySqlCommand oCmd = new MySqlCommand(oString, Connection);

                // creating the strings 
                string movieID;
                string movieName;
                string movieYear;

                using (MySqlDataReader getMovieInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getMovieInfo);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        movieID = row["MovieID"].ToString();
                        movieName = row["MovieName"].ToString();
                        movieYear = row["MovieYear"].ToString();
                        Console.WriteLine("[" + movieID + "] " + movieName + " (" + movieYear + ")");
                    }
                    Console.WriteLine("\nEnter the number of the movie for details:");
                }

            }
            catch (MySqlException ex)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }
        /// <summary>
        /// Show the extra movie info with the right ID
        /// </summary>
        /// <param name="movieID">given movie id</param>
        public Tuple<string,string> ShowMovieByID(string movieID)
        {
            try
            {
                Connection.Open();
                string oString = @"SELECT * from movie WHERE MovieID = @id";
                MySqlCommand oCmd = new MySqlCommand(oString, Connection);
                oCmd.Parameters.AddWithValue("@id", movieID);

                using (MySqlDataReader getMovieInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getMovieInfo);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine("\nMovie selected: " + row["MovieName"].ToString());
                        Console.WriteLine("Year: " + row["MovieYear"].ToString());
                        Console.WriteLine("Age restriction: " + row["MovieMinimumAge"].ToString() + "+");
                        Console.WriteLine("Actors: " + row["MovieActors"].ToString());
                        Console.WriteLine("Summary: " + row["MovieSummary"].ToString());

                        // show the times with the id of the movie
                        return Tuple.Create(row["MovieID"].ToString(), row["MovieName"].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
            return Tuple.Create("","");
        }
        /// <summary>
        /// show the right times for the right movie
        /// </summary>
        /// <param name="movieID">given movie id</param>
        /*public void ShowTimesByMovieID(string movieID, string CustomerTimeOption)
        {
            while (true)
            {
                if (CustomerTimeOption == "1")
                {
                    try
                    {
                        Connection.Open();

                        string queryDateTime = @"SELECT * from date WHERE movieID = @movieid";
                        MySqlCommand command = new MySqlCommand(queryDateTime, Connection);
                        command.Parameters.AddWithValue("@movieid", movieID);
                        using (MySqlDataReader getDateTimeInfo = command.ExecuteReader())
                        {
                            int totalRows = getDateTimeInfo.FieldCount;
                            DateTime DT;
                            int dateNumber = 0;
                            Console.WriteLine("");

                            while (getDateTimeInfo.Read())
                            {
                                dateNumber += 1;
                                DT = getDateTimeInfo["DateTime"];
                                Console.WriteLine("[" + dateNumber + "] " + DT.ToString("HH:mm dd/MM/yyyy") + "      Theaterhall " + getDateTimeInfo["Hall"]);
                            }

                            Console.WriteLine("[exit] Back to menu");
                            break;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw;
                    }
                    finally
                    {
                        Connection.Close();
                    }
                }
                else if (CustomerTimeOption == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                }
            }
        }*/
    

        public void DisplayTickets()
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                Connection.Open();
                string TicketInfo = @"SELECT * FROM ticket";

                MySqlCommand oCmd = new MySqlCommand(TicketInfo, Connection);

                // creating the strings 
                string TicketID;
                string TicketCode;
                string Owner;
                string MovieID;
                string DateID;

                using (MySqlDataReader getMovieInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getMovieInfo);

                    Console.WriteLine("\n[1] Search on name\n[2] Search on ticket number\n[3] Search on movie, time and date");
                    string SearchOption = Console.ReadLine();

                    if (SearchOption == "1")
                    {
                        Console.WriteLine("\nPlease enter the customer full name");
                        string name = Console.ReadLine();

                        bool isFound = false;

                        while (true)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                Owner = row["Owner"].ToString();
                                TicketCode = row["TicketCode"].ToString();
                                TicketID = row["TicketID"].ToString();
                                MovieID = row["MovieID"].ToString();
                                DateID = row["DateID"].ToString();

                                if (Owner == name)
                                {
                                    isFound = true;
                                    Overview(TicketID, MovieID, DateID);
                                    Console.WriteLine("\nPress enter to go back to the menu");
                                    Console.ReadLine();
                                    break;
                                }
                            }

                            if (isFound)
                            {
                                break;
                            }

                            else
                            {
                                Console.WriteLine("\nThe name you entered was not found. Please enter again or type [exit] to exit");
                                name = Console.ReadLine();

                                if (name == "exit")
                                {
                                    break;
                                }
                            }
                        }
                    }

                    else if (SearchOption == "2")
                    {
                        Console.WriteLine("\nPlease enter the ticketnumber");
                        string ticketnumber = Console.ReadLine();

                        bool isFound = false;

                        while (true)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                Owner = row["Owner"].ToString();
                                TicketCode = row["TicketCode"].ToString();
                                TicketID = row["TicketID"].ToString();
                                MovieID = row["MovieID"].ToString();
                                DateID = row["DateID"].ToString();

                                if (TicketCode == ticketnumber)
                                {
                                    isFound = true;
                                    Overview(TicketID, MovieID, DateID);
                                    Console.WriteLine("\nPress enter to go back to the menu");
                                    Console.ReadLine();
                                    break;
                                }
                            }

                            if (isFound)
                            {
                                break;
                            }

                            else
                            {
                                Console.WriteLine("\n\nThere were no results found with ticketnumber: " + ticketnumber + "Please enter again or type [exit] to exit");
                                string exit = Console.ReadLine();

                                if (exit == "exit")
                                {
                                    break;
                                }
                            }
                        }

                    }

                    //!!!! Search option 3 not done yet!!! Need to search on date/time waiting on DateId to covert to date and time

                }
            }
            catch (MySqlException ex)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }


        public void Overview(string TicketID, string MovieID, string DateID)
        {
            string TicketInfo = @"SELECT * FROM ticket";
            string MovieInfo = @"SELECT * FROM movie";
            string DateInfo = @"SELECT * FROM date";

            // creating the strings 
            MySqlCommand oCmd = new MySqlCommand(TicketInfo, Connection);
            MySqlCommand oCmd2 = new MySqlCommand(MovieInfo, Connection);
            MySqlCommand oCmd3 = new MySqlCommand(DateInfo, Connection);

            string movieTitle;
            string movieYear;
            string Owner;
            string Email;
            string TicketCode;
            int SeatX;
            int SeatY;
            int amount;
            string Datetime;
            string Hall;
            double TotalPrice;

            using (MySqlDataReader getMovieInfo = oCmd2.ExecuteReader())
            {
                DataTable dataTable2 = new DataTable();

                dataTable2.Load(getMovieInfo);

                foreach (DataRow row in dataTable2.Rows)
                {
                    if (MovieID == row["MovieID"].ToString())
                    {
                        movieTitle = row["MovieName"].ToString();
                        movieYear = row["MovieYear"].ToString();

                        Console.WriteLine("\n" + movieTitle + "   " + movieYear);
                    }
                }
            }

            using (MySqlDataReader getDateTimeHallInfo = oCmd3.ExecuteReader())
            {
                DataTable dataTable3 = new DataTable();

                dataTable3.Load(getDateTimeHallInfo);

                foreach (DataRow row in dataTable3.Rows)
                {
                    if (DateID == row["DateID"].ToString())
                    {
                        Datetime = row["DateTime"].ToString();
                        Hall = row["Hall"].ToString();

                        Console.WriteLine(Datetime + "   Hall: " + Hall);
                    }
                }
            }

            using (MySqlDataReader getTicketInfo = oCmd.ExecuteReader())
            {
                DataTable dataTable = new DataTable();

                dataTable.Load(getTicketInfo);

                foreach (DataRow row in dataTable.Rows)
                {
                    if (TicketID == row["TicketID"].ToString())
                    {
                        Owner = row["Owner"].ToString();
                        Email = row["Email"].ToString();
                        TicketCode = row["TicketCode"].ToString();
                        TotalPrice = Convert.ToDouble(row["TotalPrice"]);

                        SeatX = Convert.ToInt32(row["seatX"]);
                        SeatY = Convert.ToInt32(row["seatY"]);
                        amount = Convert.ToInt32(row["amount"]);

                        string seats = "";

                        for (int i = SeatX; i < amount + SeatX; i++)
                        {
                            seats += "(" + i + "/" + SeatY + ") ";
                        }

                        Console.WriteLine("Seats: " + seats);

                        Console.WriteLine(Owner + "    " + Email + "\nTotal price: €" + TotalPrice + "\nTicket number: " + TicketCode);
                    }
                }
            }
        }
    }
}
