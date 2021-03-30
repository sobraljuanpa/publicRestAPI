using System.Linq;

using Domain;
using IDataAccess;

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
            return _Context.Psychologists;
        }

        public Psychologist Get(int id)
        {
            return _Context.Psychologists.Find(id);
        }

        public void Add(Psychologist psychologist)
        {
            _Context.Psychologists.Add(psychologist);
            _Context.SaveChanges();
        }

        public void Update(int id, Psychologist psychologist)
        {
            Get(id).Expertise = psychologist.Expertise;
            _Context.SaveChanges();
        }

        public void Delete(int id)
        {
            _Context.Psychologists.Remove(Get(id));
            _Context.SaveChanges();
        }
    }
}
