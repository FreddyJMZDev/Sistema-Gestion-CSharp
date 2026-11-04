
using System.Runtime.Serialization;

namespace SistemaGestion.Modelos
{
    [DataContract]
    public class Registro
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Correo { get; set; }

        public Registro() { }

        public Registro(int id, string nombre, string correo)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
        }
    }
}
