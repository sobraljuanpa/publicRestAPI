﻿using System.Linq;

using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ConsultationRepository: IRepository<Consultation>
    {
        readonly Context _context;

        public ConsultationRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Consultation> GetAll()
        {
            return _context.Consultations.Include(c => c.Psychologist);
        }

        public Consultation Get(int id)
        {
            return _context.Consultations.Include(c => c.Psychologist).FirstOrDefault(x => x.Id == id);
        }

        public void Add(Consultation consultation)
        {
            
            _context.Consultations.Add(consultation);
            _context.SaveChanges();
        }

        public void Update(int id, Consultation consultation)
        {
            Get(id).PatientName = consultation.PatientName;
            Get(id).PatientBirthDate = consultation.PatientBirthDate;
            Get(id).PatientEmail = consultation.PatientEmail;
            Get(id).PatientPhone = consultation.PatientPhone;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Consultations.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}
