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
using System;
using System.Windows.Data;
using System.Collections.Generic;

namespace Library_Application
{
    public class MainViewModel : INotifyPropertyChanged
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

        // Properties for filtering
        private string filterTitle;
        public string FilterTitle
        {
            get { return filterTitle; }
            set
            {
                if (filterTitle != value)
                {
                    filterTitle = value;
                    OnPropertyChanged(nameof(FilterTitle));
                    ApplyFilter();
                }
            }
        }

        private string filterAuthor;
        public string FilterAuthor
        {
            get { return filterAuthor; }
            set
            {
                if (filterAuthor != value)
                {
                    filterAuthor = value;
                    OnPropertyChanged(nameof(FilterAuthor));
                    ApplyFilter();
                }
            }
        }

        private string filterGenre;
        public string FilterGenre
        {
            get { return filterGenre; }
            set
            {
                if (filterGenre != value)
                {
                    filterGenre = value;
                    OnPropertyChanged(nameof(FilterGenre));
                    ApplyFilter();
                }
            }
        }


        // Filtered view
        private ListCollectionView filteredBooksView;
        public ListCollectionView FilteredBooksView
        {
            get { return filteredBooksView; }
            set
            {
                if (filteredBooksView != value)
                {
                    filteredBooksView = value;
                    OnPropertyChanged(nameof(FilteredBooksView));
                    OnPropertyChanged(nameof(FilteredBooksView.Count)); // Ensure this line is present

                }
            }
        }

        public ICommand AddBookCommand => new RelayCommand(AddBook);
        public ICommand DeleteBookCommand => new RelayCommand(DeleteBook);

        public ICommand ImportBooksCommand => new RelayCommand(ReadBooksFromCSV);


        public MainViewModel()
        {
            Books = new ObservableCollection<Book>();
            FilteredBooksView = new ListCollectionView(Books); // Initialize the filtered view

        }

        public void ReadBooksFromCSV(object parameter)
        {
            try
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
                        var importedBooks = csv.GetRecords<Book>();
                        foreach (var book in importedBooks)
                        {
                            // Check if the ISBN is already in use
                            if (Books.Any(existingBook => existingBook.ISBN == book.ISBN))
                            {
                                MessageBox.Show($"Skipping book with ISBN {book.ISBN} as it already exists.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                continue;
                            }

                            // Proceed to add the book if ISBN is unique
                            Books.Add(book);
                        }

                        MessageBox.Show($"Imported  books from CSV.", "Import Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    



        public void AddBook(object parameter)
        {
            // Check if the ISBN is already in use
            if (Books.Any(book => book.ISBN == ISBN))
            {
                MessageBox.Show("ISBN must be unique. A book with this ISBN already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Implement the logic for adding a book
            //Book newBook = new Book { Title = Title, Author = Author, Genre = Genre, ISBN = ISBN };
            //Books.Add(newBook);
            //MessageBox.Show("Book added successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

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

        // Filter logic
        private void ApplyFilter()
        {
            FilteredBooksView.Filter = book =>
            {
                var b = (Book)book;

                // Apply filters based on the criteria (you can add more criteria)
                var titleMatch = string.IsNullOrEmpty(FilterTitle) || b.Title.IndexOf(FilterTitle, StringComparison.OrdinalIgnoreCase) >= 0;
                var authorMatch = string.IsNullOrEmpty(FilterAuthor) || b.Author.IndexOf(FilterAuthor, StringComparison.OrdinalIgnoreCase) >= 0;
                var genreMatch = string.IsNullOrEmpty(FilterGenre) || b.Genre.IndexOf(FilterGenre, StringComparison.OrdinalIgnoreCase) >= 0;

                return titleMatch && authorMatch && genreMatch;
            };
            // Notify that FilteredBooksView has changed
            OnPropertyChanged(nameof(FilteredBooksView));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object? obj)
        {
            return obj is MainViewModel model &&
                   EqualityComparer<ObservableCollection<Book>>.Default.Equals(Books, model.Books);
        }
    }
}
