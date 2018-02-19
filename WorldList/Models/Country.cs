using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WorldList;
using System;


namespace WorldList.Models
{
  public class Country
  {
    private static Dictionary<string, Country> _dictionaryCountries = new Dictionary<string, Country>() {};
    private string _code;
    private string _name;
    private string _continent;
    private int _population;
    private string _headOfState;

    public Country(string code, string name, string continent,int population, string headOfState)
    {
        _code = code;
        _name = name;
        _continent = continent;
        _population = population;
        _headOfState = headOfState;
    }

    public string GetCode()
    {
        return _code;
    }

    public string GetName()
    {
        return _name;
    }

    public static Country Find(string code)
    {
        return _dictionaryCountries[code];
    }

    public static List<Country> GetAll()
    {
        List<Country> allCountries = new List<Country> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM country;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            string code = rdr.GetString(0);
            string name = rdr.GetString(1);
            string continent = rdr.GetString(2);
            int population = rdr.GetInt32(6);
            string headOfState = "";
            if (!rdr.IsDBNull(12))
            {
                headOfState = rdr.GetString(12);
            }
            Country newCountry = new Country(code, name, continent, population, headOfState);
            allCountries.Add(newCountry);
            _dictionaryCountries.Add(code, newCountry);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allCountries;
    }
  }
}
