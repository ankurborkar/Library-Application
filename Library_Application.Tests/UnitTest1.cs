using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library_Application;



namespace Library_Application.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_AddBook()
        {
            
            MainViewModel viewModel = new MainViewModel();
            Book newBook = new Book { Title = "Sample Book", Author = "John Doe", Genre = "Fiction", ISBN = "1234567890" };

           
            viewModel.AddBookCommand.Execute(newBook);

            
            Assert.IsTrue(viewModel.Books.Contains(newBook), "Book should be added to the collection.");
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

       
    }
}