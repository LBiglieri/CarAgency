using System;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Personas;
using CarAgency.Security.Integrity;
using CarAgency.Security.Security;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    // Los datos personales se guardan encriptados: el mapper encripta al escribir y desencripta
    // al leer, asi BLL y UI trabajan siempre con texto plano. El DVH se calcula sobre lo encriptado,
    // que es lo que queda guardado en la tabla.
    public class PersonaMapper
    {
        private const string Table = "Persona";
        private readonly PersonaDataAccess data = new PersonaDataAccess();

        public Persona GetByDni(int dni)
        {
            return Decrypt(MappingHandler.MapTableToEntities<Persona>(data.GetByDni(dni)).FirstOrDefault());
        }

        public Persona GetById(Guid id)
        {
            return Decrypt(MappingHandler.MapTableToEntities<Persona>(data.GetById(id)).FirstOrDefault());
        }

        // Descripcion de la persona para otras entidades: "Nombre Apellido DNI: 123".
        public string GetDescription(Guid id)
        {
            Persona person = GetById(id);
            return person == null ? null : Format(person.Nombre, person.Apellido, person.DNI);
        }

        // Para SP que traen nombre y apellido encriptados por separado: concatenados en SQL ya no se
        // podrian desencriptar.
        public static string Describe(string encryptedName, string encryptedSurname, int dni)
        {
            return Format(CryptographyHandler.Decrypt(encryptedName), CryptographyHandler.Decrypt(encryptedSurname), dni);
        }

        private static string Format(string name, string surname, int dni)
        {
            return name + " " + surname + " DNI: " + dni;
        }

        public SQLUpdateResult AddPerson(Persona person)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(person.Persona_Id, person.DNI,
                CryptographyHandler.Encrypt(person.Nombre),
                CryptographyHandler.Encrypt(person.Apellido),
                CryptographyHandler.Encrypt(person.Direccion),
                CryptographyHandler.Encrypt(person.TelefonoCasa),
                CryptographyHandler.Encrypt(person.TelefonoPersonal),
                CryptographyHandler.Encrypt(person.Email),
                person.FechaNacimiento,
                dvh));
        }

        private static Persona Decrypt(Persona person)
        {
            if (person == null) return null;
            person.Nombre = CryptographyHandler.Decrypt(person.Nombre);
            person.Apellido = CryptographyHandler.Decrypt(person.Apellido);
            person.Direccion = CryptographyHandler.Decrypt(person.Direccion);
            person.TelefonoPersonal = CryptographyHandler.Decrypt(person.TelefonoPersonal);
            person.TelefonoCasa = CryptographyHandler.Decrypt(person.TelefonoCasa);
            person.Email = CryptographyHandler.Decrypt(person.Email);
            return person;
        }
    }
}
