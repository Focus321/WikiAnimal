using System;
using System.Collections.Generic;
using System.Text;

namespace WikiAnimal.Domain.Model
{
    public class TypeOfAnimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public IEnumerable<Animal> Animals { get; set; }
        public bool IsRemove { get; set; } = false;

        public TypeOfAnimal()
        {
            Animals = new List<Animal>();
        }
    }
}