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
	public class ChangeData : Connecter
	{
		public void CreateProfile(int totalprofiles, string username, string password, string function)
		{
			try
			{
				Connection.Open();

				string stringToInsert = @"INSERT INTO login (ID, Username, Password, Functions) VALUES (@ID, @Username, @Password, @Functions)";

				MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
				MySqlParameter IDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
				MySqlParameter UsernameParam = new MySqlParameter("@Username", MySqlDbType.VarChar);
				MySqlParameter PasswordParam = new MySqlParameter("@Password", MySqlDbType.VarChar);
				MySqlParameter FunctionParam = new MySqlParameter("@Functions", MySqlDbType.VarChar);

				IDParam.Value = totalprofiles;
				UsernameParam.Value = username;
				PasswordParam.Value = password;
				FunctionParam.Value = function;

				command.Parameters.Add(IDParam);
				command.Parameters.Add(UsernameParam);
				command.Parameters.Add(PasswordParam);
				command.Parameters.Add(FunctionParam);

                command.Prepare();
                command.ExecuteNonQuery();
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

		public void UpdateMovie(int id, string name = "", int year = -1, int minimumage = -1, string summary = "", string actors = "")
		{
			try
			{
				Connection.Open();
				bool updating = true;
				while (updating)
				{

					MySqlParameter ParamID = new MySqlParameter("@MovieID", MySqlDbType.Int32);
					ParamID.Value = id;


					//Check which variables you need to get
					if (name != "")
					{
						string UpdateName = @"UPDATE movie SET MovieName = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandName = new MySqlCommand(UpdateName, Connection);
						MySqlParameter NameParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);
						
						NameParam.Value = name;

						commandName.Parameters.Add(ParamID);
						commandName.Parameters.Add(NameParam);

						commandName.Prepare();
						commandName.ExecuteNonQuery();

						name = "";
					}
					else if (year != -1)
					{
						string UpdateYear = @"UPDATE movie SET MovieYear = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandYear = new MySqlCommand(UpdateYear, Connection);
						MySqlParameter YearParam = new MySqlParameter("@NewType", MySqlDbType.Int32);

						YearParam.Value = year;

						commandYear.Parameters.Add(ParamID);
						commandYear.Parameters.Add(YearParam);

						commandYear.Prepare();
						commandYear.ExecuteNonQuery();

						year = -1;
					}
					else if (minimumage != -1)
					{
						string UpdateAge = @"UPDATE movie SET MovieMinimumAge = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandAge = new MySqlCommand(UpdateAge, Connection);
						MySqlParameter AgeParam = new MySqlParameter("@NewType", MySqlDbType.Int32);

						AgeParam.Value = minimumage;

						commandAge.Parameters.Add(ParamID);
						commandAge.Parameters.Add(AgeParam);

						commandAge.Prepare();
						commandAge.ExecuteNonQuery();

						minimumage = -1;
					}
					else if (summary != "")
					{
						string UpdateSum = @"UPDATE movie SET MovieSummary = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandSum = new MySqlCommand(UpdateSum, Connection);
						MySqlParameter SumParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);

						SumParam.Value = summary;

						commandSum.Parameters.Add(ParamID);
						commandSum.Parameters.Add(SumParam);

						commandSum.Prepare();
						commandSum.ExecuteNonQuery();

						summary = "";
					}
					else if (actors != "")
					{
						string UpdateActors = @"UPDATE movie SET MovieActors = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandActor = new MySqlCommand(UpdateActors, Connection);
						MySqlParameter ActorsParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);

						ActorsParam.Value = actors;

						commandActor.Parameters.Add(ParamID);
						commandActor.Parameters.Add(ActorsParam);

						commandActor.Prepare();
						commandActor.ExecuteNonQuery();

						actors = "";
					}
					else
					{
						updating = false;
						break;
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
		}

		public bool CheckTicket(string ticketid)
		{
			bool TicketExists = false;
			try
			{
				Connection.Open();
				string stringToCheck = @"SELECT TicketCode FROM ticket WHERE TicketCode = @TicketID";

				MySqlCommand command = new MySqlCommand(stringToCheck, Connection);
				MySqlParameter ParamticketID = new MySqlParameter("@TicketID", MySqlDbType.VarChar);

				ParamticketID.Value = ticketid;
				command.Parameters.Add(ParamticketID);

				MySqlDataReader dataReader = command.ExecuteReader();

				while (dataReader.Read())
				{
					int TicketCheck = dataReader.GetInt32("COUNT(*)");
					if (TicketCheck == 1)
					{
						TicketExists = true;
						dataReader.Close();
					}
					else
					{
						TicketExists = false;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				Connection.Close();
			}
			return TicketExists;
		}

		public void DisplayProducts()
		{
			Console.Clear();
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Restaurant menu:");
			try
			{
				Connection.Open();
				string stringToDisplay = @"SELECT * FROM restaurantitems";

				MySqlCommand command = new MySqlCommand(stringToDisplay, Connection);

				MySqlDataReader dataReader = command.ExecuteReader();
				while (dataReader.Read())
				{
					double test = dataReader.GetDouble("Price");
					Console.WriteLine("(" + dataReader["ItemID"] + ") " + dataReader["ItemName"] + "    €" + test.ToString("0.00"));
				}

				dataReader.Close();
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

		public int TotalProducts()
		{
			int productsamount = 0;
			try
			{
				Connection.Open();
				string stringToRead = @"SELECT ItemID FROM restaurantitems";

				MySqlCommand command = new MySqlCommand(stringToRead, Connection);
				MySqlDataReader dataReader = command.ExecuteReader();

				while (dataReader.Read())
				{
					productsamount += 1;
				}

				dataReader.Close();
			}
			catch (MySqlException ex)
			{

				throw;
			}
			finally
			{
				Connection.Close();
			}
			return productsamount;
		}

		public void CreateProduct(string itemname, double price)
		{
			try
			{
				Connection.Open();

				string stringToInsert = @"INSERT INTO restaurantitems (ItemName, Price) VALUES (@ItemName, @Price)";

				MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
				MySqlParameter ItemNameParam = new MySqlParameter("@ItemName", MySqlDbType.VarChar);
				MySqlParameter PriceParam = new MySqlParameter("@Price", MySqlDbType.Double);

				ItemNameParam.Value = itemname;
				PriceParam.Value = price;

				command.Parameters.Add(ItemNameParam);
				command.Parameters.Add(PriceParam);

				command.Prepare();
				command.ExecuteNonQuery();
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

		public void UpdateProduct(int id = -1, string name = "", double price = -1)
		{
			try
			{
				Connection.Open();
				if (name != "" && price != -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET ItemName = @NewName, Price = @NewPrice WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewName = new MySqlParameter("@NewName", MySqlDbType.VarChar);
					MySqlParameter ParamNewPrice = new MySqlParameter("@NewPrice", MySqlDbType.Double);

					ParamID.Value = id;
					ParamNewName.Value = name;
					ParamNewPrice.Value = price;

					command.Parameters.Add(ParamNewName);
					command.Parameters.Add(ParamNewPrice);
					command.Parameters.Add(ParamID);

					command.Prepare();
					command.ExecuteNonQuery();
				}

				else if(name != "" && price == -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET ItemName = @NewName WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewName = new MySqlParameter("@NewName", MySqlDbType.VarChar);

					ParamID.Value = id;
					ParamNewName.Value = name;

					command.Parameters.Add(ParamNewName);
					command.Parameters.Add(ParamID);

					command.Prepare();
					command.ExecuteNonQuery();
				}

				else if (name == "" && price != -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET Price = @NewPrice WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewPrice = new MySqlParameter("@NewPrice", MySqlDbType.Double);

					ParamID.Value = id;
					ParamNewPrice.Value = price;

					command.Parameters.Add(ParamNewPrice);
					command.Parameters.Add(ParamID);

					command.Prepare();
					command.ExecuteNonQuery();
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

        public void DeleteProduct(int deleteItem)
		{
			try
			{
				Connection.Open();

				string stringToDelete = "DELETE FROM restaurantitems WHERE ItemID = @ItemID";

				MySqlCommand command = new MySqlCommand(stringToDelete, Connection);
				MySqlParameter ItemIDParam = new MySqlParameter("@ItemID", MySqlDbType.Int32);

				ItemIDParam.Value = deleteItem;

				command.Parameters.Add(ItemIDParam);

				command.Prepare();
				command.ExecuteNonQuery();
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

		public string checkLoginAndFunction(string username, string password)
		{
			//if string is empty it means you are not logged in, otherwise you are
			string function = "";
			try
			{
				Connection.Open();
				string stringToCheck = @"SELECT * FROM login WHERE username = @uname AND password = @pword";

				MySqlCommand command = new MySqlCommand(stringToCheck, Connection);
				MySqlParameter ParamUsername = new MySqlParameter("@uname", MySqlDbType.VarChar);
				MySqlParameter ParamPassword = new MySqlParameter("@pword", MySqlDbType.VarChar);

				ParamUsername.Value = username;
				ParamPassword.Value = password;

				command.Parameters.Add(ParamUsername);
				command.Parameters.Add(ParamPassword);

				MySqlDataReader dataReader = command.ExecuteReader();

				while (dataReader.Read())
				{
					function = dataReader.GetString("Functions");
				}
				dataReader.Close();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				Connection.Close();
			}
			return function;
		}

		public void InsertMovie(string name, int year, int mage, string msummary, string Actors)
		{
			try
			{
				Connection.Open();
				string stringToInsert = "INSERT INTO Movie (MovieName, MovieYear, MovieMinimumAge, MovieSummary, MovieActors) VALUES (@Name, @Year, @MAge, @MSummary, @MovieActors)";

				MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
				MySqlParameter NameParam = new MySqlParameter("@Name", MySqlDbType.VarChar);
				MySqlParameter YearParam = new MySqlParameter("@Year", MySqlDbType.Int32);
				MySqlParameter MAgeParam = new MySqlParameter("@MAge", MySqlDbType.Int32);
				MySqlParameter MSummaryParam = new MySqlParameter("@MSummary", MySqlDbType.LongText);
				MySqlParameter ActorsParam = new MySqlParameter("@MovieActors", MySqlDbType.LongText);

				NameParam.Value = name;
				YearParam.Value = year;
				MAgeParam.Value = mage;
				MSummaryParam.Value = msummary;
				ActorsParam.Value = Actors;

				command.Parameters.Add(NameParam);
				command.Parameters.Add(YearParam);
				command.Parameters.Add(MAgeParam);
				command.Parameters.Add(MSummaryParam);
				command.Parameters.Add(ActorsParam);

				command.Prepare();
				command.ExecuteNonQuery();
			}
			catch (Exception)
			{

                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public void ReserveTicket(string Owner, string Email, string TicketCode, int MovieID, int Amount, int seatX, int seatY, int DateID, int Hall, double TotalPrice)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO ticket (Owner, Email, TicketCode, MovieID, Amount, seatX, seatY, DateID, HallID, TotalPrice) VALUES (@Owner, @Email, @TicketCode, @MovieID, @Amount, @seatX, @seatY, @DateID, @HallID, @TotalPrice)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                //MySqlParameter TicketIDParam = new MySqlParameter("@TicketID", MySqlDbType.Int32);
                MySqlParameter OwnerParam = new MySqlParameter("@Owner", MySqlDbType.VarChar);
                MySqlParameter EmailParam = new MySqlParameter("@Email", MySqlDbType.VarChar);
                MySqlParameter TicketCodeParam = new MySqlParameter("@TicketCode", MySqlDbType.VarChar);
                MySqlParameter MovieIDParam = new MySqlParameter("@MovieID", MySqlDbType.Int32);
                MySqlParameter AmountParam = new MySqlParameter("@Amount", MySqlDbType.Int32);
                MySqlParameter seatXParam = new MySqlParameter("@seatX", MySqlDbType.Int32);
                MySqlParameter seatYParam = new MySqlParameter("@seatY", MySqlDbType.Int32);
                MySqlParameter DateIDParam = new MySqlParameter("@DateID", MySqlDbType.Int32);
                MySqlParameter HallIDParam = new MySqlParameter("@HallID", MySqlDbType.Int32);
                MySqlParameter TotalPriceParam = new MySqlParameter("@TotalPrice", MySqlDbType.Double);

                //TicketIDParam.Value = TicketID;
                OwnerParam.Value = Owner;
                EmailParam.Value = Email;
                TicketCodeParam.Value = TicketCode;
                MovieIDParam.Value = MovieID;
                AmountParam.Value = Amount;
                seatXParam.Value = seatX;
                seatYParam.Value = seatY;
                DateIDParam.Value = DateID;
                HallIDParam.Value = Hall;
                TotalPriceParam.Value = TotalPrice;

                //command.Parameters.Add(TicketIDParam);
                command.Parameters.Add(OwnerParam);
                command.Parameters.Add(EmailParam);
                command.Parameters.Add(TicketCodeParam);
                command.Parameters.Add(MovieIDParam);
                command.Parameters.Add(AmountParam);
                command.Parameters.Add(seatXParam);
                command.Parameters.Add(seatYParam);
                command.Parameters.Add(DateIDParam);
                command.Parameters.Add(HallIDParam);
                command.Parameters.Add(TotalPriceParam);

                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}