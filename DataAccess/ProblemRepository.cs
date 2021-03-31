using System.Linq;

using Domain;
using IDataAccess;

namespace DataAccess
{
    public class ProblemRepository: IRepository<Problem>
    {
        readonly Context _Context;

        public ProblemRepository(Context context)
        {
            _Context = context;
        }

        public IQueryable<Problem> GetAll()
        {
            return _Context.Problems;
        }

        public Problem Get(int id)
        {
            return _Context.Problems.Find(id);
        }

        public void Add(Problem problem)
        {
            _Context.Problems.Add(problem);
            _Context.SaveChanges();
        }

        public void Update(int id, Problem problem)
        {
            Get(id).Name = problem.Name;
            _Context.SaveChanges();
        }

        public void Delete(int id)
        {
            _Context.Problems.Remove(Get(id));
            _Context.SaveChanges();
        }
    }
}
