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
    /// Логика взаимодействия для SprPage.xaml
    /// </summary>
    public partial class SprPage : Page
    {
        public SprPage()
        {
            InitializeComponent();
            SprLV.ItemsSource = Connect.contex.SprTable.ToList();
            var f = Connect.contex.SprTable.ToList();
            f.Insert(0, new SprTable() { Name_Pred = "По умолчанию" });
            SortCmb.ItemsSource = new[] { "По умолчанию", "По возрастанию", "По убыванию" };
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddSprPage(null));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SprLV.ItemsSource = Connect.contex.SprTable.ToList();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddSprPage((sender as Button).DataContext as SprTable));
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var delPred = SprLV.SelectedItems.Cast<SprTable>().ToList();
            foreach (var delClient in delPred) //Цикл проверки наличия в учетной таблице данных из справочной
                if (Connect.contex.UchTable.Any(x => x.Cod_Pred == delClient.Cod_Pred))
                {
                    MessageBox.Show("Данные используются!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBox.Show($"Удалить {delPred.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Connect.contex.SprTable.RemoveRange(delPred);
            try
            {
                Connect.contex.SaveChanges();
                SprLV.ItemsSource = Connect.contex.SprTable.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        void Update()
        {
            var p = Connect.contex.SprTable.ToList();
            //поиск
            if (Poisktol.Text.Length > 0)
                p = p.Where(cx => cx.Name_Pred.ToLower().Contains(Poisktol.Text.ToLower())).ToList();

            //Сортировка
            switch (SortCmb.SelectedIndex)
            {
                case 1:
                    p = p.OrderBy(x => x.Cod_Pred).ToList();
                    break;
                case 2:
                    p = p.OrderByDescending(x => x.Cod_Pred).ToList();
                    break;
            }
            SprLV.ItemsSource = p;
        }
        private void Poisktol_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void SortCmb_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
