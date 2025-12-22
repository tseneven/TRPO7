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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        private Pacient _pacient;
        private Doc currentDoc;

        public Add(Doc doc)
        {
            currentDoc = doc;
            _pacient = new Pacient();
            InitializeComponent();
            Counts.Text = $"Врач-{currentDoc.Spectialisation}: {currentDoc.LastName} {currentDoc.Name} {currentDoc.MiddleName}";
            DataContext = _pacient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_pacient.LastName) && !string.IsNullOrEmpty(_pacient.Name) && !string.IsNullOrEmpty(_pacient.MiddleName) && !string.IsNullOrEmpty(_pacient.Birthday))
                {
                    int id = 0;
                    while (true)
                    {
                        Random rnd = new Random();

                        id = rnd.Next(0, 99999);
                        if (!IdReader.docsId.Any(i => i == id))
                        {

                            _pacient.Id = id;
                            using (StreamWriter sw = new StreamWriter($"P_{id}.json"))
                            {
                                sw.WriteLine(JsonSerializer.Serialize(_pacient));
                            }
                            break;

                        }

                    }
                    NavigationService.GoBack();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
