using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Create an instance of MainViewModel and set it as the DataContext
            DataContext = new MainViewModel();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel viewModel = (MainViewModel)DataContext;
            var addBookView = new AddBookView(viewModel);
            addBookView.BookAdded += (s, args) =>
            {
                // Close the AddBookView window when a book is added
                (s as AddBookView).Close();
            };

            // Create a new window to host the AddBookView
            Window addBookWindow = new Window
            {
                Content = addBookView,
                Title = "Add Book",
                SizeToContent = SizeToContent.WidthAndHeight
            };

            addBookWindow.ShowDialog(); // Show the window as a dialog
        }

    }
}
