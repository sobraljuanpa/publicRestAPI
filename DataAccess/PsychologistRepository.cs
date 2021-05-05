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
            Get(id).Expertise = psychologist.Expertise;
            Get(id).Address = psychologist.Address;
            Get(id).IsRemote = psychologist.IsRemote;
            Get(id).PsychologistName = psychologist.PsychologistName;
            Get(id).PsychologistSurname = psychologist.PsychologistSurname;
            Get(id).Schedule = psychologist.Schedule;
            _Context.SaveChanges();
        }

        public void Delete(int id)
        {
            _Context.Psychologists.Remove(Get(id));
            _Context.SaveChanges();
        }
    }
}
