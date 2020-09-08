using WikiAnimal.Domain.Model;

namespace WikiAnimal.Domain
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; } // Краткое описание
        public string Description { get; set; } // Описание
        public string Appearance { get; set; } // Внешний вид
        public string Habitat { get; set; } // Ареал(среда обитание)
        public string ImagePath { get; set; }
        public TypeOfAnimal TypeOfAnimal { get; set; }
        public bool IsRemove { get; set; } = false;
        public int TypeOfAnimalId { get; set; }

    }
}