using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BooksClassLibrary.Models
{
    public class BookRepo
    {
        public int AddBookReturnId(Book book)
        {
            string sql = "INSERT INTO Book(Title, Author, Price, describe, CountryID)" + 
                            "VALUES(@Title, @AUthor, @Price, @Describe, @CountryId)" +
                            "Select Cast(SCOPE_IDENTITY()as int) ";

            using (IDbConnection connection = new SQLConnection)
            {

            }
        }
    }
}
