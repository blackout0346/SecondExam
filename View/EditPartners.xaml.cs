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
    /// Логика взаимодействия для EditPartners.xaml
    /// </summary>
    public partial class EditPartners : Window
    {
        AppDbContext db;
        private Partner Partner;
        public EditPartners(Partner partner)
        {

            InitializeComponent();
            Partner = partner;
            typeOrg.Text = partner.typePartners.NamePartner;
            URadress.Text = partner.URPartner;
            NameOrg.Text = partner.NameParntersPlace;
            FIOdirectory.Text = partner.DirectionName;
            email.Text = partner.email;
            Number.Text = partner.number;
            INN.Text = partner.INN;
            Rate.Text = partner.Rate;

        }

        void edit()
        {
            db = new AppDbContext();
            var exesting = db.Partner.FirstOrDefault(p => p.Id == Partner.Id);
            if (exesting == null)
            {
                return;
            }



            exesting.URPartner = URadress.Text;
            exesting.NameParntersPlace = NameOrg.Text;
            exesting.DirectionName = FIOdirectory.Text;
            exesting.email = email.Text;
            exesting.INN = INN.Text;
            exesting.Rate = Rate.Text;
            exesting.number = Number.Text;

            var type = db.TypePartner.FirstOrDefault(p => p.NamePartner == typeOrg.Text);
            if(type != null)
            {
                exesting.typePartners = type;
            }
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
                MessageBox.Show("Успешно редактированно");
                edit();
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
