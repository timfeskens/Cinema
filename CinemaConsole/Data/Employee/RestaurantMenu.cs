﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data
{
    class RestaurantMenu
    {
		var ProductList = new List<>();

        void addItem(string name, double price)
        {
            ProductList.Add(Tuple.Create(name, price));
        }
    }
}
