using secondExam.Module;
using System;
using System.Collections.Generic;
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
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace secondExam.View
{
    /// <summary>
    /// Логика взаимодействия для AddingProduct.xaml
    /// </summary>
    public partial class AddingProduct : Window
    {
        AppDbContext db;
        public AddingProduct()
        {
            InitializeComponent();
            Uri iconUri = new Uri("Master_pol.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
        void AddProduct()
        {
            db = new AppDbContext();
            var typeProduct = new TypeProduct() { Name = TypeProduct.Text };
            var dataProduct = new Products()
            {
                name = NameProduct.Text,
                MinPriceForPartner = decimal.Parse(MinPrice.Text),
                typeProduct = typeProduct,
                Article = int.Parse(Articl.Text),

            };
            db.AddRange(typeProduct, dataProduct);
            db.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Articl.Text) ||
           !string.IsNullOrEmpty(MinPrice.Text) ||
           !string.IsNullOrEmpty(NameProduct.Text) ||
           !string.IsNullOrEmpty(TypeProduct.Text)
     )
            {
                MessageBox.Show("Успешно добавлен");
                AddProduct();
                TypeProduct.Text = "";
                MinPrice.Text = "";
                Articl.Text = "";
                NameProduct.Text = "";
              
                return;
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
