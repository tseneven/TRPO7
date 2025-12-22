using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private Doc currentDoc;
        public Pacient? SelectedPacient { get; set; }
        int docId;

        public ObservableCollection<Pacient> Pacients { get; set; } = new();
        public Main(Doc doc, int docId)
        {
            this.docId = docId;
            currentDoc = doc;
            InitializeComponent();
            loadedPacients();

            DataContext = this;
            Counts.Text = $"Врач-{currentDoc.Spectialisation}: {currentDoc.LastName} {currentDoc.Name} {currentDoc.MiddleName}";
        }

        void loadedPacients()
        {
            for (int i = 0; i < IdReader.pacientsId.Count; i++)
            {
                using (StreamReader sr = new StreamReader($"P_{IdReader.pacientsId[i]}.json"))
                {
                    Pacients.Add(JsonSerializer.Deserialize<Pacient>(sr.ReadLine()));
                }

            }
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add(currentDoc));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient == null)
            {
                MessageBox.Show("Пользователь не выбран");
                return;
            }
            NavigationService.Navigate(new DeteilPacient(SelectedPacient, docId));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient == null)
            {
                MessageBox.Show("Пользователь не выбран");
                return;
            }
            NavigationService.Navigate(new Edit(SelectedPacient));

        }
    }
}
