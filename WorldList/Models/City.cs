using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WorldList;
using System;

namespace WorldList.Models
{
  public class City
  {
    private int _id;
    private string _name;
    private string _country;
    private int _population;

    public City(int id, string name, string country, int population)
    {
      _id = id;
      _name = name;
      _country = country;
      _population = population;
    }

    public static List<City> GetAll()
    {
        List<City> allCities = new List<City> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM city;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int id = rdr.GetInt32(0);
          string name = rdr.GetString(1);
          string country = rdr.GetString(2);
          int population = rdr.GetInt32(4);
          City newCity = new City(id, name, country, population);
          allCities.Add(newCity);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allCities;
    }
  }
}
