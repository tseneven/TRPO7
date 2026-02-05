using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        private Doc _doc;
        private Doc currentDoc;
        public Auth()
        {
            _doc = new Doc();
            currentDoc = new Doc();
            InitializeComponent();
            DataContext = _doc;
            IdReader.read();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_doc.Password))
            {
                try
                {
                    using (StreamReader sr = new StreamReader($"D_{Auth_Id.Text}.json"))
                    {
                        currentDoc = JsonSerializer.Deserialize<Doc>(sr.ReadLine());
                    }

                    if (currentDoc.Password == _doc.Password)
                    {
                        NavigationService.Navigate(new Main(currentDoc, int.Parse(Auth_Id.Text)));
                    }
                    else
                    {
                        MessageBox.Show("Пароль неверный");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Не все поля заполнены");

        }
    }
}
