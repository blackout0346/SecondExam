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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace secondExam.View
{
    /// <summary>
    /// Логика взаимодействия для ItemPartners.xaml
    /// </summary>
    public partial class ItemPartners : UserControl
    {
        public int PartnerId {  get; set; }
        public ItemPartners(int partnerId, string typeName, string directory, string number, string rate, int discount, string URAdress)
        {
            InitializeComponent();
     
            PartnerId = partnerId; 
            TypeName.Text = $"{typeName} | {URAdress}";
            Discount.Text = discount.ToString();
            DirectorName.Text = directory;
            Number.Text = $"+ {number}";
            Rate.Text = $"Рейтинг: {rate}";
        }
    }
}
