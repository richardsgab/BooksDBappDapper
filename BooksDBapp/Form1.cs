using BooksClassLibrary.Models;
using ClassLibraryBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksDBapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Book book = BookCreate();

            BookRepo repo = new BookRepo();
            repo.AddBookReturnId(book);//from BookRepo
            FillListBox();


            /*MessageBox.Show(id.ToString());*///shows the last id entered
        }

        private Book BookCreate()
        {
            Book book = new Book();
            book.Title = txtTitle.Text;
            book.Author = txtAuthor.Text;
            book.Price = decimal.Parse(txtPrice.Text);
            book.Describe = txtDescribe.Text;
            book.CountryId = (int)cmbCountry.SelectedValue;//convierte en int el country seleccionado
            return book;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Country> countries = new List<Country>();
            CountryRepo countryRepo = new CountryRepo();
            countries = countryRepo.GetAllCountries();

            this.cmbCountry.Items.Clear();
            cmbCountry.DisplayMember = "Name";
            cmbCountry.ValueMember = "Id";
            cmbCountry.DataSource = countries;

            FillListBox();
        }

        private void FillListBox() //Display books in box
        {
            List<Book> list = new List<Book>();
            BookRepo repo = new BookRepo();
            list = repo.GetAllBooks();
            this.lstBooks.Items.Clear();
            foreach (Book book in list)
            {
                lstBooks.Items.Add(book);
            }
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cmbCountry.SelectedValue.ToString());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book = (Book)lstBooks.SelectedItem;//Convertir la seleccion del book en la lista, en un objeto Book
            BookRepo bkrepo = new BookRepo();
            bkrepo.DeleteBookById(book.Id);//el objeto Book puede usar el methos delete
            FillListBox();//to show the list without the delete book
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BookRepo repo = new BookRepo();
                Book book = new Book();
                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.Price = decimal.Parse(txtPrice.Text);
                book.Describe = txtDescribe.Text;
                book.CountryId = (int)cmbCountry.SelectedValue;

                repo.UpdateBook(book);
                FillListBox();
            }
            catch (Exception ex) 
            {
                lblError.Visible = true;
                this.lblError.Text ="Error";
            }
        }

        private void lstBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
            Book book = new Book();
            book = (Book)lstBooks.SelectedItem;

            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtPrice.Text = book.Price.ToString();
            txtDescribe.Text = book.Describe;
            cmbCountry.SelectedValue = book.CountryId;
                

        }
    }
}
