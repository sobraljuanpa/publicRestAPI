using IDataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ScheduleRepository : IRepository<Schedule>
    {
        readonly Context _Context;

        public ScheduleRepository(Context context)
        {
            _Context = context;
        }

        public IQueryable<Schedule> GetAll()
        {
            return _Context.Schedules;
        }

        public Schedule Get(int id)
        {
            return _Context.Schedules.Find(id);
        }

        public void Add(Schedule schedule)
        {
            this._Context.Entry(schedule).State = EntityState.Added;
            _Context.Schedules.Add(schedule);
            _Context.SaveChanges();
        }

        public void Update(int id, Schedule schedule)
        {
            Get(id).MondayConsultations = schedule.MondayConsultations;
            Get(id).TuesdayConsultations = schedule.TuesdayConsultations;
            Get(id).WednesdayConsultations = schedule.WednesdayConsultations;
            Get(id).ThursdayConsultations = schedule.ThursdayConsultations;
            Get(id).FridayConsultations = schedule.FridayConsultations;
            _Context.SaveChanges();
        }

        public void Delete(int id)
        {
            _Context.Schedules.Remove(Get(id));
            _Context.SaveChanges();
        }
    }
}
