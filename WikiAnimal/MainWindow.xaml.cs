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
using WikiAnimal.Models;
using WikiAnimal.Services;

namespace WikiAnimal
{
    public partial class MainWindow : Window
    {
        private AnimalWikiServices _animalServices;
        private ChatService _chatService;
        private string _nickname;

        public MainWindow() { InitializeComponent(); }
        public MainWindow(AnimalWikiServices animalServices, ChatService chatService)
        {
            InitializeComponent();
            _animalServices = animalServices;
            _chatService = chatService;
            _chatService.CommandReciveEvent += ReciveMessage;
            _chatService.Start();
        }

        private async Task ReciveMessage(Command command)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Width = chatListView.ActualWidth };
                    stackPanel.Children.Add(new TextBlock() { Text = command.Nickname + ": ", Foreground = Brushes.Coral });
                    stackPanel.Children.Add(new TextBlock() { Text = command.Message });
                    stackPanel.Children.Add(new TextBlock() { Text = DateTime.Now.ToShortTimeString(), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(chatListView.ActualWidth - 20, 0, 0, 0) });

                    chatListView.Items.Add(stackPanel);
                });
            });
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _animalServices.GetPage(mainWrapPanel, 0);
            await _chatService.ReadCommand();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_animalServices.PageNumber == 1) return;
            else if (_animalServices.PageNumber == 4) _animalServices.PageNumber = 2;
            else _animalServices.PageNumber--;
            await _animalServices.GetPage(mainWrapPanel, 0);
        }
        private void ChatButtonClick(object sender, RoutedEventArgs e)
        {
            chatGrid.Visibility = Visibility.Collapsed;
            Grid.SetColumnSpan(mainGrid, 2);
            chatbutton.Visibility = Visibility.Visible;
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (nickNameTextBox.Text == "") MessageBox.Show("Введите свой NickName");
            else
            {
                _nickname = nickNameTextBox.Text;
                nickNameTextBox.IsEnabled = false;
                await Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        _chatService.SendCommand(new Command() { Message = massageTextBox.Text, Nickname = _nickname });

                        StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Width = chatListView.ActualWidth };
                        stackPanel.Children.Add(new TextBlock() { Text = _nickname + ": ", Foreground = Brushes.Coral });
                        stackPanel.Children.Add(new TextBlock() { Text = massageTextBox.Text });
                        stackPanel.Children.Add(new TextBlock() { Text = DateTime.Now.ToShortTimeString(), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(chatListView.ActualWidth - 20, 0, 0, 0) });

                        chatListView.Items.Add(stackPanel);
                        massageTextBox.Text = "";
                    });
                });
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            chatGrid.Visibility = Visibility.Visible;
            Grid.SetColumnSpan(mainGrid, 1);
            chatbutton.Visibility = Visibility.Collapsed;
        }
    }
}