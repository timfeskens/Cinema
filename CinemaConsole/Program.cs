﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages.Admin;
using CinemaConsole.Pages.Customer;
using CinemaConsole.Pages;

namespace CinemaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Running = true;
            Login login = new Login();
            string pageToBe = "";
            string toDo = "";
            while (Running)
            {
                if (pageToBe != "")
                {
                    toDo = pageToBe;
                    pageToBe = "";
                }
                else
                {
                    Console.WriteLine("Do you want to see the movielist or login? (movielist/login)");
                    toDo = Console.ReadLine();
                }
                toDo.ToLower();
                switch (toDo)
                {
                    case "login":
                        login.Menu();
                        break;

                    case "admin":
                        Admin.Menu();
                        break;

                    case "movielist":
                        Customer.Menu();
                        break;

                    case "help":
                        Console.WriteLine("Help: show help.\nLogin: Log into your own page.\nMovielist: Show movielist.");
                        break;

                    case "exit":
                        Running = false;
                        break;

                    default:
                        Console.WriteLine("You are writting a command wrong or the command doesn't exist yet");
                        break;
                }
                if(login.Function != "")
                {
                    pageToBe = login.Function;
                }
            }
        }
    }
}