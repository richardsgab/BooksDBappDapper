using BooksClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ClassLibraryBook.Models
{
    public class CountryRepo
    {
        public List<Country> GetAllCountries()
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                var sql = "Select * from Country";
                return connection.Query<Country>(sql).ToList();
            }
        }
    }
}
