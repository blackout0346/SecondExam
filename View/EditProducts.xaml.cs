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
    /// Логика взаимодействия для EditProducts.xaml
    /// </summary>
    public partial class EditProducts : Window
    {
        AppDbContext db;
        Products Products;
        public EditProducts(Products products)
        {
            InitializeComponent();
            Uri iconUri = new Uri("Master_pol.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            Products = products;
            NameProduct.Text = products.name;
            Article.Text = products.Article.ToString();
            MinPrice.Text = products.MinPriceForPartner.ToString();
            TypeProduct.Text = products.typeProduct.Name;
        }
        void edit()
        {
            db = new AppDbContext();
            var exesting = db.Products.FirstOrDefault(p => p.Id == Products.Id);
            if (exesting == null)
            {
                return;
            }



            exesting.MinPriceForPartner = decimal.Parse(MinPrice.Text);
            exesting.Article = int.Parse(Article.Text);
            exesting.name = NameProduct.Text;



            var type = db.TypeProduct.FirstOrDefault(p => p.Name == TypeProduct.Text);
            if (type != null)
            {
                exesting.typeProduct = type;
            }
            db.SaveChanges();

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameProduct.Text) ||
                !string.IsNullOrEmpty(Article.Text) ||
                !string.IsNullOrEmpty(TypeProduct.Text) ||
                !string.IsNullOrEmpty(MinPrice.Text))
            {
                MessageBox.Show("Успешно редактированно");
                edit();
                return;
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }

        }
    }
}
