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
        public TypeOfAnimalRepository(AnimalDatabaseContext context)
        {
            Context = context;
        }

        private AnimalDatabaseContext Context { get; }

        public async Task<IReadOnlyCollection<TypeOfAnimal>> GetAllAsync()
        {
            return await Context.TypeOfAnimals.Include(x => x.Animals).ToListAsync();
        }

        public async Task<IReadOnlyCollection<TypeOfAnimal>> FindByConditionAsync(
            Expression<Func<TypeOfAnimal, bool>> predicat)
        {
            return await Context.TypeOfAnimals.Include(x => x.Animals).Where(predicat).ToListAsync();
        }
    }
}
