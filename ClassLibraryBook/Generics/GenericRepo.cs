using BooksClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ClassLibraryBook.Generics
{
    public class GenericRepo
    {
        //ReadOnly:
        public List<T> LoadData<T , U>(string sqlstatement, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                List<T> result = connection.Query<T> (sqlstatement, parameters).ToList();
                return result;
            }
        }

        //read and Write
        public void SaveData<T, U>(string sqlstatement, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Execute(sqlstatement, parameters);
                
            }
        }
    }
}
