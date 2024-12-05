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
    /// Логика взаимодействия для UchPage.xaml
    /// </summary>
    public partial class UchPage : Page
    {
        public UchPage()
        {
            InitializeComponent();
            UchLV.ItemsSource = Connect.contex.UchTable.ToList();
            var f = Connect.contex.UchTable.ToList();
            f.Insert(0, new UchTable() { Name_Product = "По умолчанию" });
            var g = Connect.contex.UchTable.ToList();
            g.Insert(0, new UchTable() { Price = 1 });
            SortCmb.ItemsSource = new[] { "По умолчанию", "По возрастанию", "По убыванию" };
            FiltrCmb1.ItemsSource = f;
            FiltrCmb1.SelectedIndex = SortCmb.SelectedIndex = 0;
            FiltrCmb2.ItemsSource = f;
            FiltrCmb2.SelectedIndex = SortCmb.SelectedIndex = 0;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddUchPage(null));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UchLV.ItemsSource = Connect.contex.UchTable.ToList();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddUchPage((sender as Button).DataContext as UchTable));
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var delProd = UchLV.SelectedItems.Cast<UchTable>().ToList();
           
            if (MessageBox.Show($"Удалить {delProd.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Connect.contex.UchTable.RemoveRange(delProd);
            try
            {
                Connect.contex.SaveChanges();
                UchLV.ItemsSource = Connect.contex.UchTable.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void Update()
        {
            var p = Connect.contex.UchTable.ToList();
            //Поиск
            if (Poisktol.Text.Length > 0)
                p = p.Where(cx => cx.Name_Product.ToLower().Contains(Poisktol.Text.ToLower()) || cx.Price.ToString().Contains(Poisktol.Text.ToLower())).ToList();
            UchLV.ItemsSource = p;
            //Фильтрация
            if (FiltrCmb1.SelectedIndex > 0)
            {
                var selectedFiltr = FiltrCmb1.SelectedItem as UchTable;
                p = p.Where(cx => cx.Name_Product == selectedFiltr.Name_Product).ToList();
            }
            if (FiltrCmb2.SelectedIndex > 0)
            {
                var selectedFiltr = FiltrCmb2.SelectedItem as UchTable;
                p = p.Where(cx => cx.Kol_vo == selectedFiltr.Kol_vo).ToList();
            }
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
            UchLV.ItemsSource = p;
        }
        private void Poisktol_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void SortCmb_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void FiltrCmb_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void FiltrCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
