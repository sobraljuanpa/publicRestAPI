using System.Linq;

using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PsychologistRepository: IRepository<Psychologist>
    {

        readonly Context _Context;

        public PsychologistRepository(Context context)
        {
            _Context = context;
        }

        public IQueryable<Psychologist> GetAll()
        {
            return _Context.Psychologists
                .Include(x => x.Schedule)
                .Include(x => x.Expertise);
        }

        public Psychologist Get(int id)
        {
            return _Context.Psychologists
                .Include(x => x.Schedule)
                .Include(x => x.Expertise)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Psychologist psychologist)
        {
            this._Context.Entry(psychologist).State = EntityState.Added;
            _Context.Psychologists.Add(psychologist);
            _Context.SaveChanges();
        }

        public void Update(int id, Psychologist psychologist)
        {
            var aux = _Context.Psychologists.FirstOrDefault(p => p.Id == id);

            aux.ActiveYears = psychologist.ActiveYears;
            aux.Address = psychologist.Address;
            aux.Fee = psychologist.Fee;
            aux.Expertise = psychologist.Expertise;
            aux.IsRemote = psychologist.IsRemote;
            aux.PsychologistName = psychologist.PsychologistName;
            aux.PsychologistSurname = psychologist.PsychologistSurname;
            aux.ScheduleId = psychologist.ScheduleId;

            _Context.SaveChanges();
        }

        public void Delete(int id)
        {
            _Context.Psychologists.Remove(Get(id));
            _Context.SaveChanges();
        }
    }
}
