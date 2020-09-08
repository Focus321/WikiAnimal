using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WikiAnimal.Domain;
using WikiAnimal.Domain.Model;
using WikiAnimal.Domain.Repository.Interface;

namespace WikiAnimal.Domain.Repository
{
    public class AnimalRepository : IRepository<Animal>
    {
        private AnimalDatabaseContext Context { get; }
        public AnimalRepository(AnimalDatabaseContext context) { Context = context; }

        public async Task Add(Animal obj)
        {
            await Context.Animals.AddAsync(obj);
            await Context.SaveChangesAsync();
        }
        public async Task Change(Animal obj)
        {
            var animal = (await Context.Animals.FirstOrDefaultAsync(x => x.Id == obj.Id));
            if (animal != null)
            {
                animal.Name = animal.Name;
                animal.Habitat = animal.Habitat;
                animal.ImagePath = animal.ImagePath;
                animal.ShortDescription = animal.ShortDescription;
                animal.Description = animal.Description;
                animal.Appearance = animal.Appearance;

                await Context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<Animal>> FindByConditionAsync(Expression<Func<Animal, bool>> predicat)
        {
            return await Context.Animals.Include(x => x.TypeOfAnimal).Where(predicat).ToListAsync();
        }
        public async Task<IReadOnlyCollection<Animal>> GetAllAsync()
        {
            return await Context.Animals.Include(x => x.TypeOfAnimal).ToListAsync();
        }
    }
}