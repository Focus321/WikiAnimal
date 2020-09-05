using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAnimal.Domain;
using WikiAnimal.Domain.Model;

namespace WikiAnimal.Repository
{
    public class TypeOfAnimalRepository
    {
        public TypeOfAnimalRepository(AnimalDatabaseContext context)
        {
            Context = context;
        }

        protected AnimalDatabaseContext Context { get; set; }

        public List<TypeOfAnimal> GetAll()
        {
            return  Context.TypeOfAnimals.Include(x=>x.Animals).ToList();
        }
    }
}
