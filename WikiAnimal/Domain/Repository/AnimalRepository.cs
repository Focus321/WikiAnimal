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
        public AnimalRepository(AnimalDatabaseContext context)
        {
            Context = context;
        }

        private AnimalDatabaseContext Context { get; }


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
