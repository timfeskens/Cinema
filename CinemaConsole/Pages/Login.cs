﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data;
using CinemaConsole.Data.BackEnd;

namespace CinemaConsole.Pages
{
    public class Login
    {
        List<Profiles> profiles = new List<Profiles>()
        {
            {new Profiles("retailer","retailer","retailer") },
            {new Profiles("admin","admin","admin") },
            {new Profiles("retailer","retailer","retailer") },
            {new Profiles("ticketsalesman","ticketsalesman","ticketsalesman") },
            {new Profiles("ticketsalesman","ticketsalesman","ticketsaleman") },
            {new Profiles("admin","admin","admin") }
        };

        private string Username { get; set; }

        private string Password { get; set; }

        private int ErrorCode = 0;

        private int ProfilePlace = 0;

        public string ErrorMessage;

        public string Function = "";

        public bool loggedIn = false;

        public Login()
        {

        }

        private void register(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void Menu()
        {
            bool checkLogin = true;
            while (checkLogin == true)
            {
                Console.WriteLine("Give your credentials:(username - password)");
                string login = Console.ReadLine();
                string[] credentials = login.Split(' ');
                if (credentials.Length != 2)
                {
                    Console.WriteLine("Your credentials are not in the right format. (username - password)");
                }
                else
                {
                    register(credentials[0], credentials[1]);
                    if (checkIfLoginIsRight())
                    {
                        Function = checkFunction();
                        ErrorMessage = getErrorMessage(ErrorCode);
                        checkLogin = false;
                        Console.WriteLine("You are Logged in and redirected to your page");
                    }
                    else
                    {
                        Console.WriteLine("Wrong Username/Password");
                    }
                }
            }

        }

        private bool checkIfLoginIsRight()
        {
            bool usernameExists = false;
            //Checks if the username exist
            for (int i = 0; i < profiles.Count; i++)
            {
                if (profiles[i].Username == Username)
                {
                    ProfilePlace = i;
                    i = profiles.Count;
                    usernameExists = true;
                }
                else
                {
                    usernameExists = false;
                    ErrorCode = 1;
                }
            }

            if (usernameExists && Password == profiles[ProfilePlace].Password)
            {
                Console.WriteLine("Logged in!");
                return true;
            }
            else
            {
                ErrorCode = 1;
                return false;
            }

        }

        private string checkFunction()
        {
            if (checkIfLoginIsRight())
            {
                return profiles[ProfilePlace].Function;
            }
            else
            {
                Console.WriteLine(ErrorCode);
                return "";
            }
        }

        private string getErrorMessage(int id)
        {
            switch (id)
            {
                case 0:
                    return "There is nothing wrong";
                case 1:
                    return "Wrong Username or Password. \n Check for misspels";
                default:
                    return "There is nothing wrong";
            }
        }
    }
}