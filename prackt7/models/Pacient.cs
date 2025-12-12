using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prackt7.models
{
    public class Pacient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Birthday { get; set; }
        public string? LastAppointment { get; set; }
        public string? LastDoctor { get; set; }
        public string? Diagnosis { get; set; }
        public string? Recomendations { get; set; }

    }
}
