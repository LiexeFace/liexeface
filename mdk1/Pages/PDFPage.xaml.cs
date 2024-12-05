using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
using static iTextSharp.text.pdf.AcroFields;

namespace mdk1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PDFPage.xaml
    /// </summary>
    public partial class PDFPage : Page
    {
        public PDFPage()
        {
            InitializeComponent();
        }

        private void PDFBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем новый документ PDF
                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream("Продукция.pdf", FileMode.Create));
                doc.Open();

                // Устанавливаем шрифт
                BaseFont baseFont = BaseFont.CreateFont(@"C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);

                // Создаем таблицу с 6 столбцами
                PdfPTable table = new PdfPTable(6);

                // Добавляем заголовки таблицы
                table.AddCell(new PdfPCell(new Phrase("Наименование продукции", font)));
                table.AddCell(new PdfPCell(new Phrase("Наименование предприятия", font)));
                table.AddCell(new PdfPCell(new Phrase("ФИО директора", font)));
                table.AddCell(new PdfPCell(new Phrase("Кол-во", font)));
                table.AddCell(new PdfPCell(new Phrase("Цена", font)));
                table.AddCell(new PdfPCell(new Phrase("Стоимость", font)));

                // Переменная для хранения общей стоимости
                decimal totalCost = 0;

                // Заполняем таблицу данными
                foreach (var item in Connect.contex.UchTable.ToList())
                {
                    decimal itemCost = item.Price * item.Kol_vo;
                    totalCost += itemCost; // Суммируем стоимость

                    table.AddCell(new Phrase(item.Name_Product.ToString(), font));
                    table.AddCell(new Phrase(item.SprTable.Name_Pred.ToString(), font));
                    table.AddCell(new Phrase(item.SprTable.FIO.ToString(), font));
                    table.AddCell(new Phrase(item.Kol_vo.ToString(), font));
                    table.AddCell(new Phrase(item.Price.ToString(), font));
                    table.AddCell(new Phrase(itemCost.ToString(), font));
                }

                // Добавляем строку "Итого"
                table.AddCell(new PdfPCell(new Phrase("Итого", font)) { Colspan = 5, HorizontalAlignment = Element.ALIGN_RIGHT });
                table.AddCell(new Phrase(totalCost.ToString(), font));

                // Добавляем таблицу в документ
                doc.Add(table);
                doc.Close();

                MessageBox.Show("Pdf-документ сохранен");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pdf-документ не сохранен: {ex.Message}", "Ошибка!");
            }
        }
    }
}
