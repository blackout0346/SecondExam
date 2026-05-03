using Microsoft.EntityFrameworkCore;
using secondExam.Module;
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
    public partial class PartnersShow : Window
    {
        AppDbContext db;
        public PartnersShow()
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

                await LoadDb();
            }

            await select();
        }
        async Task LoadDb()
        {

            ParseFromCSV parseFromCSV = new ParseFromCSV(db);
            parseFromCSV.csvAddingtypeProduct();

            parseFromCSV.csvAddingPartner();

        }

        async Task select()
        {
            db = new AppDbContext();


            var query = db.PartnerProducts
            .Include(p => p.Partner)
            .ThenInclude(p => p.typePartners)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Text))
            {
                query = query.Where(p => EF.Functions.Like(p.Partner.DirectionName, $"%{search.Text}%"));

            }
        

            var results = await query.ToListAsync();
            displayPartner.Items.Clear();


            foreach (var result in results)
            {
                int count = 0;
                if (result.TotalPrice < 10000)
                {
                    count = 0;

                }
                else if (result.TotalPrice >= 10000 && result.TotalPrice < 50000)
                {
                    count = 5;
                }
                else if (result.TotalPrice >= 50000 && result.TotalPrice < 300000)
                {
                    count = 10;
                }
                else if (result.TotalPrice > 300000 )
                {
                    count = 15;
                }
                ItemPartners itemPartners = new ItemPartners(result.Partner.Id, result.Partner.typePartners.NamePartner, result.Partner.DirectionName, result.Partner.number, result.Partner.Rate.ToString(), count, result.Partner.URPartner);

                displayPartner.Items.Add(itemPartners);

            }
            //MessageBox.Show($"{results.Count}");
            //PartnersTable.ItemsSource = results;

        }
        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.Invoke(() => select());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductsU products = new ProductsU();
            products.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HistorySale historySale = new HistorySale();
            historySale.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddingPartners addingPartners = new AddingPartners();
            addingPartners.Show();

        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedItems = displayPartner.SelectedItems;

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

                        var Itempartner = item as ItemPartners;

                        if (Itempartner != null)
                        {
                            var partner = await db.Partner.FindAsync(Itempartner.PartnerId);
                            if (partner != null)
                            {
                                db.Partner.Remove(partner);
                            }


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

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var selectedItem = displayPartner.SelectedItem as ItemPartners;
            if (selectedItem == null)
            {
                MessageBox.Show("Выберите хотя бы одну запись для редактированмя", "Предупреждение");
                return;
            }
            try
            {
                var partner = await db.Partner
                    .Include(p => p.typePartners)
                    .FirstOrDefaultAsync(p => p.Id == selectedItem.PartnerId);
                if (partner == null)
                {
                    MessageBox.Show("Партнёр не найден");
                    return;
                }
                EditPartners editPartners = new EditPartners(partner);
                editPartners.Show();
            }
            catch (Exception ex)
            {

            }


        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            select();
        }
    }
}
