using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WikiAnimal.Domain;
using WikiAnimal.Domain.Model;
using WikiAnimal.Domain.Repository.Interface;

namespace WikiAnimal.Domain.Repository
{
    public class TypeOfAnimalRepository : IRepository<TypeOfAnimal>
    {
        private AnimalDatabaseContext Context { get; }
        public TypeOfAnimalRepository(AnimalDatabaseContext context) { Context = context; }

        public async Task Add(TypeOfAnimal obj)
        {
            await Context.TypeOfAnimals.AddAsync(obj);
            await Context.SaveChangesAsync();
        }
        public async Task Change(TypeOfAnimal obj)
        {
            var typeAnimal = (await Context.TypeOfAnimals.FirstOrDefaultAsync(x => x.Id == obj.Id));
            if (typeAnimal != null)
            {
                typeAnimal.Name = obj.Name;
                typeAnimal.PhotoPath = obj.PhotoPath;
                await Context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<TypeOfAnimal>> GetAllAsync()
        {
            return await Context.TypeOfAnimals.Include(x => x.Animals).ToListAsync();
        }
        public async Task<IReadOnlyCollection<TypeOfAnimal>> FindByConditionAsync(Expression<Func<TypeOfAnimal, bool>> predicat)
        {
            return await Context.TypeOfAnimals.Include(x => x.Animals).Where(predicat).ToListAsync();
        }

        public async Task Remove(TypeOfAnimal obj)
        {
            if(await Context.TypeOfAnimals.FirstOrDefaultAsync(x => x.Id == obj.Id) is TypeOfAnimal typeOfAnimal )
                typeOfAnimal.IsRemove = true;

            await Context.SaveChangesAsync();
        }
    }
}