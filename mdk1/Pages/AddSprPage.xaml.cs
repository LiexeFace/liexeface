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
using mdk1.AppData;

namespace mdk1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddSprPage.xaml
    /// </summary>
    public partial class AddSprPage : Page
    {
        SprTable pred;
        bool checkNew;
        public AddSprPage(SprTable c)
        {
            InitializeComponent();
            if(c==null)
            {
                c = new SprTable();
                checkNew = true;    
            }
            else
                checkNew = false;
            DataContext = pred = c;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
            {
                Connect.contex.SprTable.Add(pred);
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

        public static bool CheckSpr(SprTable c)
        {
            if (string.IsNullOrEmpty(c.Name_Pred) || !c.Name_Pred.All(char.IsLetter))
                return false;
            return true;
        }
    }
}
