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

namespace secondExam.View
{
    /// <summary>
    /// Логика взаимодействия для AddingPartners.xaml
    /// </summary>
    public partial class AddingPartners : Window
    {
        AppDbContext db;
        public AddingPartners()
        {

            InitializeComponent();
            Uri iconUri = new Uri("Master_pol.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

        }
        void AddPartner()
        {
            db = new AppDbContext();
            var typePartner = new TypePartner() { NamePartner = typeOrg.Text };
            var dataPartner = new Partner() { typePartners = typePartner, URPartner = URadress.Text, NameParntersPlace = NameOrg.Text, DirectionName = FIOdirectory.Text, email = email.Text, INN = INN.Text, Rate = Rate.Text, number = Number.Text };
            db.AddRange(typePartner, dataPartner);
            db.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(typeOrg.Text) ||
                !string.IsNullOrEmpty(URadress.Text) ||
                !string.IsNullOrEmpty(NameOrg.Text) ||
                !string.IsNullOrEmpty(FIOdirectory.Text) ||
                !string.IsNullOrEmpty(email.Text) ||
                !string.IsNullOrEmpty(Number.Text) ||
                !string.IsNullOrEmpty(INN.Text) ||
                !string.IsNullOrEmpty(Rate.Text))
            {
                MessageBox.Show("Успешно добавлен");
                AddPartner();
                typeOrg.Text = "";
                URadress.Text = "";
                NameOrg.Text = "";
                FIOdirectory.Text = "";
                email.Text = "";
                Number.Text = "";
                INN.Text = "";
                Rate.Text = "";
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
