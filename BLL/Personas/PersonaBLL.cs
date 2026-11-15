using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using CarAgency.BE;
using CarAgency.Mappers;
using System;

namespace BLL
{
    public class PersonaBLL
    {
        private PersonaMapper _peoplemapper;
        public PersonaBLL()
        {
            _peoplemapper = new PersonaMapper();
        }
        public Persona GetByDni(int dni)
        {
            return _peoplemapper.GetByDni(dni);
        }
        public Persona GetById(Guid id)
        {
            return _peoplemapper.GetById(id);
        }

        public SQLUpdateResult AddPerson(Persona person)
        {
            Persona validationPerson;
            validationPerson = _peoplemapper.GetByDni(person.DNI);
            if (validationPerson != null)
                throw new Exception("A Person with this Dni already exists.");

            person.Persona_Id = Guid.NewGuid();
            SQLUpdateResult result = _peoplemapper.AddPerson(person);
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(TipoEvento.PersonCreated, person.Persona_Id);
            return result;
        }
    }
}
