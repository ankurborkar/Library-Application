using Library_Application;


namespace Lib_Application_Testing
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void Test_AddBook_DuplicateISBN()
        {
            // Arrange
            MainViewModel viewModel = new MainViewModel();
            Book existingBook = new Book { Title = "Existing Book", Author = "Jane Doe", Genre = "Non-Fiction", ISBN = "DuplicateISBN456" };
            viewModel.Books.Add(existingBook);

            // Attempt to add a book with a duplicate ISBN
            Book newBook = new Book { Title = "Duplicate Book", Author = "John Smith", Genre = "Fiction", ISBN = "DuplicateISBN456" };

            // Act
            viewModel.AddBookCommand.Execute(newBook);

            // Assert
            Assert.IsFalse(viewModel.Books.Contains(newBook), "Book with duplicate ISBN should not be added.");
        }

        [TestMethod]
        public void Test_DeleteBook()
        {

            MainViewModel viewModel = new MainViewModel();
            Book bookToDelete = new Book { Title = "To Be Deleted", Author = "Jane Doe", Genre = "Mystery", ISBN = "0987654321" };
            viewModel.Books.Add(bookToDelete);
            viewModel.SelectedBook = bookToDelete;


            viewModel.DeleteBookCommand.Execute(null);


            Assert.IsFalse(viewModel.Books.Contains(bookToDelete), "Book should be removed from the collection.");
        }

        [TestMethod]
        public void Test_AddBook_WithEmptyTitle()
        {
            MainViewModel viewModel = new MainViewModel();
            Book newBook = new Book { Title = "", Author = "John Doe", Genre = "Fiction", ISBN = "1234567890" };

            viewModel.AddBookCommand.Execute(newBook);

            Assert.IsFalse(viewModel.Books.Contains(newBook), "Book with empty title should not be added to the collection.");
        }
        [TestMethod]
        public void Test_AddBook_WithEmptyAuthor()
        {
            MainViewModel viewModel = new MainViewModel();
            Book newBook = new Book { Title = "Sample Book", Author = "", Genre = "Fiction", ISBN = "1234567890" };

            viewModel.AddBookCommand.Execute(newBook);

            Assert.IsFalse(viewModel.Books.Contains(newBook), "Book with empty author should not be added to the collection.");
        }
        [TestMethod]
        public void Test_AddBook_WithEmptyGenre()
        {
            MainViewModel viewModel = new MainViewModel();
            Book newBook = new Book { Title = "Sample Book", Author = "John Doe", Genre = "", ISBN = "1234567890" };

            viewModel.AddBookCommand.Execute(newBook);

            Assert.IsFalse(viewModel.Books.Contains(newBook), "Book with empty genre should not be added to the collection.");
        }
        [TestMethod]
        public void Test_AddBook_WithEmptyISBN()
        {
            MainViewModel viewModel = new MainViewModel();
            Book newBook = new Book { Title = "Sample Book", Author = "John Doe", Genre = "Fiction", ISBN = "" };

            viewModel.AddBookCommand.Execute(newBook);

            Assert.IsFalse(viewModel.Books.Contains(newBook), "Book with empty ISBN should not be added to the collection.");
        }

        [TestMethod]
        public void Test_FilterBooks()
        {
            // Arrange
            MainViewModel viewModel = new MainViewModel();
            Book book1 = new Book { Title = "Book 1", Author = "Author 1", Genre = "Genre 1", ISBN = "1111111111" };
            Book book2 = new Book { Title = "Book 2", Author = "Author 2", Genre = "Genre 2", ISBN = "2222222222" };
            viewModel.Books.Add(book1);
            viewModel.Books.Add(book2);

            // Act
            viewModel.FilterTitle = "Book 1";
            viewModel.FilterAuthor = "Author 1";
            viewModel.FilterGenre = "Genre 1";

            // Assert
            Assert.AreEqual(1, viewModel.FilteredBooksView.Count, "FilteredBooksView should contain only one book.");
            Assert.IsTrue(viewModel.FilteredBooksView.Contains(book1), "FilteredBooksView should contain Book 1.");

            // Reset filters
            viewModel.FilterTitle = "";
            viewModel.FilterAuthor = "";
            viewModel.FilterGenre = "";
        }

        [TestMethod]
        public void Test_AddBook()
        {

            MainViewModel viewModel = new MainViewModel();
            Book newBook = new Book { Title = "Sample Book", Author = "John Doe", Genre = "Fiction", ISBN = "1234567890" };
            viewModel.Books.Add(newBook);

            viewModel.AddBookCommand.Execute(newBook);


            Assert.IsTrue(viewModel.Books.Contains(newBook), "Book should be added to the collection.");
        }

        public void Test_ReadBooksFromCSV_FileNotFound()
        {
            // Arrange
            MainViewModel viewModel = new MainViewModel();
            string csvFilePath = "NonExistentFile.csv";

            // Act & Assert
            Assert.ThrowsException<IOException>(() => viewModel.ReadBooksFromCSV(csvFilePath), "IOException should be thrown for a non-existent file.");
        }

        //[TestMethod]
        //public void Test_ImportBooksFromCSV()
        //{
        //    // Arrange
        //    MainViewModel viewModel = new MainViewModel();

        //    // Create a CSV file with sample data (ensure it doesn't contain duplicate ISBNs)
        //    //string csvFilePath = "Path\\To\\CSV\\Without\\DuplicateISBN.csv";
        //    string csvFilePath = "Path\\To\\TestData\\csv_unique.csv";
        //    // Write logic to create a CSV file with sample data

        //    // Act
        //    viewModel.ImportBooksCommand.Execute(csvFilePath);

        //    // Assert
        //    Assert.IsTrue(viewModel.Books.Any(), "Books collection should not be empty after importing from CSV.");
        //}






        //[TestMethod]
        //public void Test_DeleteBook_NoSelection()
        //{
        //    // Arrange
        //    MainViewModel viewModel = new MainViewModel();
        //    Book initialBook = new Book { Title = "Initial Book", Author = "Mark Johnson", Genre = "Adventure", ISBN = "InitialISBN123" };
        //    viewModel.Books.Add(initialBook);

        //    // Act
        //    viewModel.DeleteBookCommand.Execute(null);

        //    // Assert
        //    Assert.IsTrue(viewModel.Books.Contains(initialBook), "Collection should remain unchanged.");
        //}

        //[TestMethod]
        //public void Test_ImportBooksFromCSV_Success()
        //{
        //    // Arrange
        //    MainViewModel viewModel = new MainViewModel();
        //    // Create a CSV file with valid book data (ensure the file exists)
        //    string csvFilePath = "Path\\To\\TestData\\csv_unique.csv";

        //    // Act
        //    viewModel.ImportBooksCommand.Execute(csvFilePath);

        //    // Assert
        //    Assert.IsTrue(viewModel.Books.Count > 0, "Books should be imported from the CSV file.");
        //}

        //[TestMethod]
        //public void Test_ImportBooksFromCSV_DuplicateISBN()
        //{
        //    // Arrange
        //    MainViewModel viewModel = new MainViewModel();
        //    // Create a CSV file with a book having a duplicate ISBN
        //    string csvFilePath = "Path\\To\\CSV\\With\\DuplicateISBN.csv";
        //    Book initialBook = new Book { Title = "Initial Book", Author = "John Smith", Genre = "Fantasy", ISBN = "DuplicateISBN123" };
        //    viewModel.Books.Add(initialBook);

        //    // Act
        //    viewModel.ImportBooksCommand.Execute(csvFilePath);

        //    // Assert
        //    Assert.IsFalse(viewModel.Books.Any(book => book.ISBN == "DuplicateISBN123"), "Book with duplicate ISBN should not be imported.");
        //}



    }
}