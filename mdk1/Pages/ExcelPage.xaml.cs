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
using Excel = Microsoft.Office.Interop.Excel;
using Page = System.Windows.Controls.Page;
using mdk1.AppData;

namespace mdk1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExcelPage.xaml
    /// </summary>
    public partial class ExcelPage : Page
    {
        public ExcelPage()
        {
            InitializeComponent();
        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            // Объявляем приложение
            Excel.Application app = new Excel.Application
            {
                Visible = true,
                // Количество листов в рабочей книге
                SheetsInNewWorkbook = 1
            };
            Excel.Workbook workBook = app.Workbooks.Add(Type.Missing);
            app.DisplayAlerts = false;
            Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
            sheet.Name = "Отчет";

            // Установили шрифт
            sheet.Cells.Font.Name = "Times New Roman";

            // Объединяем ячейки и задаем стиль
            Excel.Range reportTitleRange = sheet.Cells[1, 1].Resize[1, 6];
            reportTitleRange.Merge();
            reportTitleRange.Value = "Ведомость отгрузки продукции предприятием";
            reportTitleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter; //По центру  текст
            reportTitleRange.Font.Bold = true; //Жирность

            // Заголовки столбцов
            sheet.Cells[2, 1].Value = "Наименование продукции";
            sheet.Cells[2, 2].Value = "Наименование предприятия";
            sheet.Cells[2, 3].Value = "ФИО директора";
            sheet.Cells[2, 4].Value = "Количество";
            sheet.Cells[2, 5].Value = "Цена";
            sheet.Cells[2, 6].Value = "Стоимость";

            // Заполнение данных
            int currow = 3;
            var sad = Connect.contex.UchTable.ToList();
            foreach (var item in sad)
            {
                sheet.Cells[currow, 1].Value = item.Name_Product;
                sheet.Cells[currow, 2].Value = item.SprTable.Name_Pred;
                sheet.Cells[currow, 3].Value = item.SprTable.FIO;
                sheet.Cells[currow, 4].Value = item.Kol_vo;
                sheet.Cells[currow, 5].Value = item.Price;
                sheet.Cells[currow, 6].Value = item.Price * item.Kol_vo;
                currow++;
            }
            sheet.Columns.AutoFit();

            // Обводка для диапазона ячеек A2 по G7
            Excel.Range borderRange = sheet.Range["A2:F13"];
            borderRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borderRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            // Итого
            int totalRow = currow;
            Excel.Range totalRange = sheet.Cells[totalRow, 1].Resize[1, 5];
            totalRange.Merge();
            totalRange.Value = "Итого начислено за месяц: ";
            totalRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter; // По центру текст
            totalRange.Font.Bold = true; // Жирность

            Excel.Range l = sheet.get_Range("F13");
            l.FormulaLocal = "=СУММ(F2:F12)";
        }
    }
}
