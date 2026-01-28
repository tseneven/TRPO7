using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prackt7
{
    public class Pacient : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }

            }
        }
        private string _name;

        public string Name { 
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }

            }
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }

            }
        }
        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged();
                }

            }
        }
        private DateTime? _birthday;
        public DateTime? Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                }

            }
        }
        private long? _phoneNumber;

        public long? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }

            }
        }

        private Appointment? _selectedApp;

        public Appointment? SelectedApp
        {
            get => _selectedApp;
            set
            {
                if (_selectedApp != value)
                {
                    _selectedApp = value;
                    OnPropertyChanged();
                }

            }
        }


        private ObservableCollection<Appointment>? _appointmentStories;
        public ObservableCollection<Appointment>? AppointmentStories
        {
            get => _appointmentStories ?? new ObservableCollection<Appointment>();
            set
            {
                if (_appointmentStories != value)
                {
                    _appointmentStories = value;
                    OnPropertyChanged();
                }

            }
        }

        private Appointment? _lastAppointment;


        public Appointment? LastAppointment
        {
            set
            {
                if(AppointmentStories.Count != 0 && AppointmentStories != null)
                {
                    var lastOr = AppointmentStories.OrderByDescending(a => a.Date).FirstOrDefault();
                    _lastAppointment = lastOr;
                    OnPropertyChanged();
                }
            }
            get
            {
                return _lastAppointment ?? new Appointment();
            }

        }





        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


    }
}
