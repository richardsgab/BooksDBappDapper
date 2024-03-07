using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryBook.Generics;
using Dapper;

namespace BooksClassLibrary.Models
{
    public class BookRepo
    {
        private GenericRepo repo = new GenericRepo();
        public int AddBookReturnId(Book book)
        {
            string sql = "INSERT INTO Book(Title, Author, Price, describe, CountryID)" + 
                            "VALUES(@Title, @AUthor, @Price, @Describe, @CountryId)" +
                            "Select Cast(SCOPE_IDENTITY()as int) ";

            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                var returnId = connection.Query<int>(sql, book).SingleOrDefault();
                //book.Id = returnId
                return returnId;
            }
        }

        public List<Book> GetAllBooks() 
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                //Without generics:
                /* var sql = "Select * from Book";
                return connection.Query<Book>(sql).ToList();*/

                //With generics
                var sql = "Select * from Book";
                return repo.LoadData<Book, dynamic>(sql, new { });
            }
        }

        public void DeleteBookById(int id)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Execute("Delete from Book where Id = @id", new {Id = id });                
            }
        }

        public void UpdateBook(Book book)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Execute("Update book SET Title = @Title," +
                     "Author = @Author, Price = @Price, Describe = @Describe," +
                     "CountryId = @CountryId  WHERE Id = @id",
                     new
                     {
                        Title = book.Title,
                        Author = book.Author,
                        Price = book.Price,
                        Describe = book.Describe,
                        CountryId = book.CountryId,
                        Id = book.Id
                     });
                               
            }
        }
    }
}
