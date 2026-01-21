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
    /// Логика взаимодействия для DeteilPacient.xaml
    /// </summary>
    public partial class DeteilPacient : Page
    {
        private ObservableCollection<Appointment> Appointments;
        private Pacient _pacient;
        private Appointment _appointment;
        private int currDoctor;


        public DeteilPacient(Pacient pacient, int cur)
        {
            _pacient = pacient;
            currDoctor = cur;
            if(_pacient.AppointmentStories == null)
            {
                _pacient.AppointmentStories = new ObservableCollection<Appointment>();
            }
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void addForm_Initialized(object sender, EventArgs e)
        {
            _appointment = new Appointment();
            addForm.DataContext = _appointment;
        }
        private void ListView_Initialized(object sender, EventArgs e)
        {
            DataContext = _pacient;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _appointment.Date = DateTime.Now.ToString();
            _appointment.Doctor_id = currDoctor.ToString();
            _pacient.AppointmentStories.Add(_appointment);

            using (StreamWriter sw = new StreamWriter($"P_{_pacient.Id}.json"))
            {
                sw.WriteLine(JsonSerializer.Serialize(_pacient));
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Edit(_pacient));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(_pacient.SelectedApp == null)
            {
                MessageBox.Show("Прием не выбран");
                return;
            }
            _pacient.AppointmentStories.Remove(_pacient.SelectedApp);

            using (StreamWriter sw = new StreamWriter($"P_{_pacient.Id}.json"))
            {
                sw.WriteLine(JsonSerializer.Serialize(_pacient));
            }

        }

    }
}
