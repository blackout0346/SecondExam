using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace secondExam.View
{
    /// <summary>
    /// Логика взаимодействия для HistorySale.xaml
    /// </summary>
    public partial class HistorySale : Window
    {
        AppDbContext db;
        public HistorySale()
        {
            InitializeComponent();
            Uri iconUri = new Uri("Master_pol.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            search.TextChanged += search_TextChanged;
            selectload();



        }
        async void selectload()
        {
            await select();
            await LoadDb();
        }
        async Task LoadDb()
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


            var query = db.PartnerProducts
            .Include(p => p.Product)
            .Select(p => new
            {
                p.Product.name,
                p.CountProducts,
                p.Product.MinPriceForPartner,
                p.TotalPrice,
                

            });


            if (!string.IsNullOrWhiteSpace(search.Text.ToLower()))
            {
                query = query.Where(p => EF.Functions.Like(p.name, $"%{search.Text}%"));
            }
            var result = await query.ToListAsync();
            HistoryTable.ItemsSource = result;

            counts.Text = $"Общая сумма: {result.Sum(p=>p.TotalPrice)}";
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.Invoke(() => select());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
