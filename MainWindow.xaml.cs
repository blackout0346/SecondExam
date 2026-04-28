using CsvHelper;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration.Attributes;
using System.Data;

namespace secondExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        AppDbContext db = new AppDbContext();

        public MainWindow()
        {
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            InitializeComponent();
            Uri iconUri = new Uri("Master_pol.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            search.TextChanged += search_TextChanged;
            selectload();
            


        }
        async void selectload()
        {
            await select();
        }
        void LoadDb()
        {
            ParseFromCSV parseFromCSV = new ParseFromCSV(db);
            parseFromCSV.csvAddingtypeProduct();
            parseFromCSV.csvAdding();
            parseFromCSV.csvAddingProducts();
            parseFromCSV.csvAddingPartner();

            parseFromCSV.csvAddingPartnerProductsa();
        }
        async Task select()
        {
            db = new AppDbContext();
         

            var query = await db.Products
            .Include(p => p.typeProduct)
            .Select(p => new
            {
                p.name,
                p.MinPriceForPartner,
                p.typeProduct.Name,
                p.Article,

            })
           
            .ToListAsync();
            if (!string.IsNullOrWhiteSpace(search.Text))
            {
                var filter = query.Where(p => EF.Functions.Like(p.name, $"%{search.Text}%"));
            }
            ProductTable.ItemsSource = query;
            counts.Text = $"Всего записей: {query.Count}";
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.Invoke(() => select());
        }
    }
}