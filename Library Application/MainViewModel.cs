using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;

namespace Library_Application
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Book> books;
        public ObservableCollection<Book> Books
        {
            get { return books; }
            set
            {
                if (books != value)
                {
                    books = value;
                    OnPropertyChanged(nameof(Books));
                }
            }
        }

        private Book selectedBook;
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                if (selectedBook != value)
                {
                    selectedBook = value;
                    OnPropertyChanged(nameof(SelectedBook));
                }
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                if (author != value)
                {
                    author = value;
                    OnPropertyChanged(nameof(Author));
                }
            }
        }

        private string isbn;
        public string ISBN
        {
            get { return isbn; }
            set
            {
                if (isbn != value)
                {
                    isbn = value;
                    OnPropertyChanged(nameof(ISBN));
                }
            }
        }

        private string genre;
        public string Genre
        {
            get { return genre; }
            set
            {
                if (genre != value)
                {
                    genre = value;
                    OnPropertyChanged(nameof(Genre));
                }
            }
        }

        public ICommand AddBookCommand => new RelayCommand(AddBook);
        public ICommand DeleteBookCommand => new RelayCommand(DeleteBook);

        public ICommand ImportBooksCommand => new RelayCommand(ReadBooksFromCSV);


        public MainViewModel()
        {
            Books = new ObservableCollection<Book>();
        }

        public void ReadBooksFromCSV(object parameter)
        {
            // Implement the logic to open a file dialog and read a CSV file
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".csv",
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Implement logic to read the CSV file and populate your book collection
                // You can use a library like CsvHelper or write custom code to parse the CSV.
                // Here's an example using CsvHelper:
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var importedBooks = csv.GetRecords<Book>().ToList();
                    foreach (var book in importedBooks)
                    {
                        Books.Add(book);
                    }

                    MessageBox.Show($"Imported {importedBooks.Count} books from CSV.", "Import Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        public void AddBook(object parameter)
        {
            // Implement the logic for adding a book
          Book newBook = new Book { Title = Title, Author = Author, Genre = Genre, ISBN = ISBN };
          // Books.Add(newBook);
          //  MessageBox.Show("Book added successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void DeleteBook(object parameter)
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Do you want to delete the book: {SelectedBook.Title}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Books.Remove(SelectedBook);
                SelectedBook = null; // Clear the selection
                MessageBox.Show("Book deleted successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
