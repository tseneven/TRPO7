using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace prackt7
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        private Doc _doc;
        public Register()
        {
            InitializeComponent();
            _doc = new Doc();
            DataContext = _doc;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_doc.Name) && !string.IsNullOrEmpty(_doc.LastName) && !string.IsNullOrEmpty(_doc.MiddleName) && !string.IsNullOrEmpty(_doc.Password) && !string.IsNullOrEmpty(rep_pass.Text) && !string.IsNullOrEmpty(_doc.Spectialisation))
            {
                if (rep_pass.Text == _doc.Password)
                {

                    var docJson = JsonSerializer.Serialize(_doc);

                    int id = 0;

                    while (true)
                    {
                        Random rnd = new Random();

                        id = rnd.Next(0, 99999);
                        if (!IdReader.docsId.Any(i => i == id))
                        {

                            using (StreamWriter sw = new StreamWriter($"D_{id}.json"))
                            {
                                sw.WriteLine(docJson);
                            }

                            break;
                        }
                    }
                    NavigationService.Navigate(new Main(_doc, id));
                }
                else MessageBox.Show("Пароли не совпадают");

            }
            else MessageBox.Show("Не все поля заполнены");

        }
    }
}
