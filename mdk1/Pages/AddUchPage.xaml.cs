using mdk1.AppData;
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

namespace mdk1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUchPage.xaml
    /// </summary>
    public partial class AddUchPage : Page
    {
        UchTable prod;
        bool checkNew;
        public AddUchPage(UchTable c)
        {
            InitializeComponent();
            PredCmb.ItemsSource = Connect.contex.SprTable.ToList();
            if (c == null)
            {
                c = new UchTable();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = prod = c;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
            {
                Connect.contex.UchTable.Add(prod);
            }
            try
            {
                Connect.contex.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Nav.MainFrame.GoBack();
        }
        public static bool CheckSpr(UchTable c)
        {
            if (string.IsNullOrEmpty(c.Name_Product) || !c.Name_Product.All(char.IsLetter))
                return false;
            return true;
        }
    }
}
