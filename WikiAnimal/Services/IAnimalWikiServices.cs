using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WikiAnimal.Services
{
    public interface IAnimalWikiServices
    {
        Task GetPage(WrapPanel wrapPanel, int number);
    }
}