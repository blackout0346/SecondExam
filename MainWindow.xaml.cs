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
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeComponent();

            ParseFromCSV parseFromCSV = new ParseFromCSV(db);
            parseFromCSV.csvAddingtypeProduct();
            parseFromCSV.csvAdding();
            parseFromCSV.csvAddingProducts();
            parseFromCSV.csvAddingPartner();

            parseFromCSV.csvAddingPartnerProductsa();
            select();

        }
        void select()
        {
            db = new AppDbContext();
            //var data1 = db.TypeMaterial
            //    .Select(p => p)
            //    .ToList();
            //table1.ItemsSource = data1;

            var data2 = db.PartnerProducts
            .Select(p => new
            {
                p.Partner.NameParntersPlace,
                p.Product.Name,
                p.CountProducts,
                p.DateSale,
                p.Partner.DirectionName
            })
                .ToList();
            table2.ItemsSource = data2;

            //var data3 = db.TypePartner
            //    .Select(p => p.Partners)
            //    .ToList();
            //table3.ItemsSource = data3;

            //var data4 = db.TypeProduct
            //    .Select(p => p)
            //    .ToList();
            //table4.ItemsSource = data4;

            //var data5 = db.Products
            //    .Select(p => new
            //    {
            //        p.Name,
            //        p.MinPriceForPartner,
            //        p.typeProduct,
            //        p.Article


            //    })
            //    .ToList();
            //table5.ItemsSource = data5;

            //var data6 = db.Partner
            //       .Select(p => new
            //       {
            //           p.DirectionName,
            //           p.NameParntersPlace,
            //           p.Rate


            //       })
            //    .ToList();
            //table6.ItemsSource = data6;

        }

    }
}