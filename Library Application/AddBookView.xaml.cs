using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_Application
{
    /// <summary>
    /// Interaction logic for AddBookView.xaml
    /// </summary>
    public partial class AddBookView : UserControl
    {
        

        private MainViewModel _viewModel; // Reference to the MainViewModel
        // Step 1: Define the event for closing the window
        public event EventHandler BookAdded;

        internal AddBookView(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel; // Set the reference to the MainViewModel
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the values entered by the user
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;
            string genre = GenreTextBox.Text;
            string isbn = ISBNTextBox.Text;

            // Check if any of the fields are empty
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(isbn))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new Book object with the entered data
            Book newBook = new Book { Title = title, Author = author, Genre = genre, ISBN = isbn };

            // Add the new book to the Books ObservableCollection in MainViewModel
            _viewModel.Books.Add(newBook);

            // Display a confirmation message
            MessageBox.Show("Book added successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear the TextBoxes
            TitleTextBox.Clear();
            AuthorTextBox.Clear();
            GenreTextBox.Clear();
            ISBNTextBox.Clear();

            BookAdded?.Invoke(this, EventArgs.Empty);
            Close();
        }
        public void Close()
        {
            // Close the window here
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
    }
}
