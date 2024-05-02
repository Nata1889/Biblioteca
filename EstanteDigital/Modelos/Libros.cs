using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstanteDigital.Modelos
{
    public class Libros
    {
        public string _id {  get; set; }
        public string titulo { get; set; }
        public string genero { get; set; }
        public string autor { get; set; }
        public string editorial { get; set; }
        public string portada { get; set; }
        public ObservableCollection<Libros> SelectedItem { get; internal set; }

        public override string ToString()
        {
            return titulo + " de " + autor;
        }
    }
}
