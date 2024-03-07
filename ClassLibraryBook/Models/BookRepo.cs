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
        public void AddBookReturnId(Book book)
        {
            #region Without Generic Repo
            /*string sql = "INSERT INTO Book(Title, Author, Price, describe, CountryID)" + 
            "VALUES(@Title, @AUthor, @Price, @Describe, @CountryId)" +
                            "Select Cast(SCOPE_IDENTITY()as int) ";

            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                var returnId = connection.Query<int>(sql, book).SingleOrDefault();
                //book.Id = returnId
                return returnId;
            }*/
            #endregion

            string sql = "INSERT INTO Book(Title, Author, Price, describe, CountryID)" +
            "VALUES(@Title, @AUthor, @Price, @Describe, @CountryId)" +
                            "Select Cast(SCOPE_IDENTITY()as int) ";
            
            repo.SaveData(sql, new { book.Title,book.Author, book.Price, book.Describe, book.CountryId} );
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
        

        //With GenericRepo:
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                var sql = "Delete from Book where Id = @Id";
                repo.SaveData(sql,new { Id = id });

            }
        }




        public void UpdateBook(Book book)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                #region Without Generic Repo
                /*connection.Execute("Update book SET Title = @Title," +
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
                     });*/
                #endregion
            }
            //With Generic repo
            var sql = "Update book SET Title = @Title," +
                "Author = @Author, Price = @Price, Describe = @Describe," +
                     "CountryId = @CountryId  WHERE Id = @id";
            repo.SaveData(sql, new {
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
