using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WikiAnimal.Domain;
using WikiAnimal.Domain.Repository;

namespace WikiAnimal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TypeOfAnimalRepository typeOfAnimalRepository;
        public MainWindow() { InitializeComponent(); }
        public MainWindow(TypeOfAnimalRepository _typeOfAnimalRepository)
        {
            InitializeComponent();

            typeOfAnimalRepository = _typeOfAnimalRepository;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = await typeOfAnimalRepository.GetAllAsync();

            var l = await typeOfAnimalRepository.FindByConditionAsync(x=>x.Id == 1);
        }
    }
}
