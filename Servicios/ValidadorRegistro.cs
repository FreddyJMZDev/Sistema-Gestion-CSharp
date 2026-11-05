using System;
using System.Net.Mail;

namespace SistemaGestion.Servicios
{
    public static class ValidadorRegistro
    {
        public static void Validar(string nombre, string correo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (nombre.Trim().Length > 100)
                throw new ArgumentException("El nombre supera 100 caracteres.");
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo es obligatorio.");
            correo = correo.Trim();
            if (correo.Length > 254)
                throw new ArgumentException("El correo es demasiado largo.");
            try
            {
                MailAddress direccion = new MailAddress(correo);
                if (!string.Equals(direccion.Address, correo, StringComparison.OrdinalIgnoreCase))
                    throw new FormatException();
            }
            catch (FormatException)
            {
                throw new ArgumentException("El formato del correo no es válido.");
            }
        }
    }
}
