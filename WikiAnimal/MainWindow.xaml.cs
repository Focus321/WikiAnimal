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
using WikiAnimal.Services;

namespace WikiAnimal
{
    public partial class MainWindow : Window
    {
        private AnimalWikiServices _animalServices;

        public MainWindow() { InitializeComponent(); }
        public MainWindow(AnimalWikiServices animalServices)
        {
            InitializeComponent();
            _animalServices = animalServices;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) { await _animalServices.GetPage(mainWrapPanel, 0); }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {          
            if (_animalServices.PageNumber == 1) return;
            _animalServices.PageNumber--;
            await _animalServices.GetPage(mainWrapPanel, 0);
        }

    }
}