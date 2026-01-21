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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        private Pacient curPacient;
        public Edit(Pacient pacient)
        {
            InitializeComponent();
            curPacient = pacient;
            DataContext = curPacient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter($"P_{curPacient.Id}.json"))
            {
                sw.WriteLine(JsonSerializer.Serialize(curPacient));
            }
            NavigationService.GoBack();
        }
    }
}
