using System;
using System.Collections.Generic;
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
        private string _birthday;
        public string Birthday
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
        private string _lastAppointment;
        public string? LastAppointment
        {
            get => _lastAppointment;
            set
            {
                if (_lastAppointment != value)
                {
                    _lastAppointment = value;
                    OnPropertyChanged();
                }

            }
        }
        private string _lastDoctor;
        public string? LastDoctor
        {
            get => _lastDoctor;
            set
            {
                if (_lastDoctor != value)
                {
                    _lastDoctor = value;
                    OnPropertyChanged();
                }

            }
        }
        private string _diagnosis;
        public string? Diagnosis
        {
            get => _diagnosis;
            set
            {
                if (_diagnosis != value)
                {
                    _diagnosis = value;
                    OnPropertyChanged();
                }

            }
        }
        private string _recomendation;
        public string? Recomendations
        {
            get => _recomendation;
            set
            {
                if (_recomendation != value)
                {
                    _recomendation = value;
                    OnPropertyChanged();
                }

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


    }
}
