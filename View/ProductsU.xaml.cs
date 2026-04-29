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
using secondExam.View;
using secondExam.Module;
using System.Threading.Tasks;

namespace secondExam
{

    public partial class ProductsU : Window
    {

        AppDbContext db;

        public ProductsU()
        {

            db = new AppDbContext();
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
            bool hasdata = db.Partner.Any() || db.Products.Any() || db.TypePartner.Any();
            if (!hasdata)
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
                await LoadDb();
            }

            await select();
        }
        async Task LoadDb()
        {
            ParseFromCSV parseFromCSV = new ParseFromCSV(db);
            parseFromCSV.csvAddingtypeProduct();
   
       
            parseFromCSV.csvAddingProducts();
     

        }
        async Task select()
        {
            db = new AppDbContext();
  

            var query = db.Products
            .Include(p => p.typeProduct)
            .AsQueryable();


            if (!string.IsNullOrWhiteSpace(search.Text.ToLower()))
            {
                query = query.Where(p => EF.Functions.Like(p.name, $"%{search.Text}%"));
            }
            var result = await query.ToListAsync();
            ProductTable.ItemsSource = result;

            counts.Text = $"Всего записей: {result.Count}";
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.Invoke(() => select());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddingProduct addingProduct = new AddingProduct();
            addingProduct.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
     
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedItems = ProductTable.SelectedItems;

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одну запись для удаления", "Предупреждение",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

 
            var result = MessageBox.Show($"Вы уверены, что хотите удалить {selectedItems.Count} запись(ей)?",
                                         "Подтверждение удаления",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var item in selectedItems)
                    {
                      
                        var product = item as Products; 
                        if (product != null)
                        {
                            db.Products.Remove(product);
                        }
                    }

                    db.SaveChanges();
                    await select();
                    MessageBox.Show("Записи успешно удалены", "Успех",
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedItem = ProductTable.SelectedItem as Products;
            if (selectedItem == null)
            {
                MessageBox.Show("Выберите хотя бы одну запись для редактированмя", "Предупреждение");
                return;
            }
            try
            {
                var products = await db.Products
                    .Include(p => p.typeProduct)
                    .FirstOrDefaultAsync(p => p.Id == selectedItem.Id);
                if (products == null)
                {
                    MessageBox.Show("Партнёр не найден");
                    return;
                }
                EditProducts editPartners = new EditProducts(products);
                editPartners.Show();
            }
            catch (Exception ex)
            {

            }
        }
    }
}