using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WikiAnimal.Domain.Repository;

namespace WikiAnimal.Services
{
    public class AnimalWikiServices : IAnimalWikiServices
    {
        public int PageNumber { get; set; } = 1;
        private int _typeNumber;
        private TypeOfAnimalRepository _repository;
        private AnimalRepository _repositoryAnimal;
        public AnimalWikiServices(TypeOfAnimalRepository repository, AnimalRepository repositoryAnimal)
        {
            _repository = repository;
            _repositoryAnimal = repositoryAnimal;
        }

        public async Task GetPage(WrapPanel wrapPanel, int number)
        {
            if (PageNumber == 1)
                await GetFirstPage(wrapPanel);
            else if (PageNumber == 2)
                await GetSecondPage(wrapPanel);
            else if (PageNumber == 3)
                await GetThirdPage(wrapPanel, number);
        }

        private async Task GetFirstPage(WrapPanel wrapPanel)
        {
            wrapPanel.Children.Clear();
            var res = await _repository.GetAllAsync();
            foreach (var item in res)
            {
                wrapPanel.Children.Add(new Border()
                {
                    Tag = item.Id,
                    CornerRadius = new System.Windows.CornerRadius(5),
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(10),
                    Height = 250,
                    Width = 650,
                    BorderBrush = Brushes.Black,
                    Background = (Brush)new BrushConverter().ConvertFrom("#FF4FB7BA"),

                });
            }

            foreach (var item in wrapPanel.Children)
            {
                if (item is Border border)
                {
                    border.MouseDown += MouseDownBorder;
                    border.Child = new Grid();
                    if (border.Child is Grid grid)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.75, GridUnitType.Star) });
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        Image image = new Image();
                        try
                        {
                            image.Margin = new Thickness(5);
                            image.Stretch = Stretch.UniformToFill;
                            image.Source = new BitmapImage(new Uri($"{res.Where(x => x.Id == Convert.ToInt32(border.Tag)).First().PhotoPath}"));
                        }
                        catch (Exception) { }
                        finally { grid.Children.Add(image); }

                        TextBlock textBlock = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,

                            FontSize = 40,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontWeight = FontWeights.Bold,
                            FontStyle = FontStyles.Italic,
                            FontFamily = new FontFamily("Segoe Print"),
                            TextAlignment = TextAlignment.Center,
                            Text = res.Where(x => x.Id == Convert.ToInt32(border.Tag)).First().Name
                        };
                        Grid.SetColumn(textBlock, 1);
                        grid.Children.Add(textBlock);
                    }
                }
            }
        }
        private async Task GetSecondPage(WrapPanel wrapPanel)
        {
            wrapPanel.Children.Clear();
            var res = await _repositoryAnimal.FindByConditionAsync(x => x.TypeOfAnimalId == _typeNumber);

            foreach (var item in res)
            {
                wrapPanel.Children.Add(new Border()
                {
                    Tag = item.Id,
                    CornerRadius = new System.Windows.CornerRadius(5),
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(10),
                    Height = 250,
                    Width = 650,
                    BorderBrush = Brushes.Black,
                    Background = (Brush)new BrushConverter().ConvertFrom("#FF4FB7BA"),
                });
            }

            foreach (var item in wrapPanel.Children)
            {
                if (item is Border border)
                {
                    border.MouseDown += MouseDownBorder;
                    border.Child = new Grid();
                    if (border.Child is Grid grid)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.75, GridUnitType.Star) });
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        Image image = new Image();
                        try
                        {
                            image.Margin = new Thickness(5);
                            image.Stretch = Stretch.UniformToFill;
                            image.Source = new BitmapImage(new Uri($"{res.Where(x => x.Id == Convert.ToInt32(border.Tag)).First().ImagePath}"));
                        }
                        catch (Exception) { }
                        finally { grid.Children.Add(image); }

                        TextBlock textBlock = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,

                            FontSize = 40,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontWeight = FontWeights.Bold,
                            FontStyle = FontStyles.Italic,
                            FontFamily = new FontFamily("Segoe Print"),
                            TextAlignment = TextAlignment.Center,
                            Text = res.Where(x => x.Id == Convert.ToInt32(border.Tag)).First().Name
                        };
                        Grid.SetColumn(textBlock, 1);
                        grid.Children.Add(textBlock);
                    }
                }
            }
        }

        private async Task GetThirdPage(WrapPanel wrapPanel, int number)
        {
            wrapPanel.Children.Clear();
            var res = await _repositoryAnimal.FindByConditionAsync(x => x.Id == number);
            foreach (var item in res)
            {
                wrapPanel.Children.Add(new Border()
                {
                    CornerRadius = new System.Windows.CornerRadius(5),
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(15),
                    BorderBrush = Brushes.Black,
                    Background = (Brush)new BrushConverter().ConvertFrom("#FF4FB7BA"),
                });
            }

            foreach (var item in wrapPanel.Children)
            {
                if (item is Border border)
                {
                    border.MouseDown += MouseDownBorder;
                    border.Child = new Grid();
                    if (border.Child is Grid grid)
                    {
                        grid.MinWidth = 700;
                        grid.Margin = new Thickness(10);
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
                        grid.ColumnDefinitions.Add(new ColumnDefinition());

                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                        Image image = new Image();
                        try
                        {
                            image.Margin = new Thickness(5);
                            image.Stretch = Stretch.Uniform;
                            image.Source = new BitmapImage(new Uri($"{res.Where(x => x.Id == number).First().ImagePath}"));
                        }
                        catch (Exception) { }
                        finally
                        {

                            Grid.SetRowSpan(image, 2);
                            grid.Children.Add(image);
                        }

                        TextBlock textBlock1 = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            FontSize = 40,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontWeight = FontWeights.Bold,
                            FontFamily = new FontFamily("Segoe Print"),
                            TextAlignment = TextAlignment.Center,
                            Text = res.Where(x => x.Id == number).First().Name
                        };
                        Grid.SetColumn(textBlock1, 1);
                        grid.Children.Add(textBlock1);

                        TextBlock textBlock2 = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                            FontFamily = new FontFamily("Segoe Print"),
                            Text = res.Where(x => x.Id == number).First().ShortDescription
                        };
                        Grid.SetColumn(textBlock2, 1);
                        Grid.SetRow(textBlock2, 1);
                        grid.Children.Add(textBlock2);

                        TextBlock textBlock3 = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            FontSize = 28,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontWeight = FontWeights.Bold,
                            FontFamily = new FontFamily("Segoe Print"),
                            TextAlignment = TextAlignment.Center,
                            Text = "Описание"
                        };
                        Grid.SetColumn(textBlock3, 0);
                        Grid.SetRow(textBlock3, 2);
                        Grid.SetColumnSpan(textBlock3, 2);
                        grid.Children.Add(textBlock3);

                        TextBlock textBlock4 = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                            FontFamily = new FontFamily("Segoe Print"),
                            Text = res.Where(x => x.Id == number).First().Description
                        };
                        Grid.SetColumn(textBlock4, 0);
                        Grid.SetRow(textBlock4, 3);
                        Grid.SetColumnSpan(textBlock4, 2);
                        grid.Children.Add(textBlock4);

                        TextBlock textBlock5 = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            FontSize = 28,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontWeight = FontWeights.Bold,
                            FontFamily = new FontFamily("Segoe Print"),
                            TextAlignment = TextAlignment.Center,
                            Text = "Внешний вид"
                        };
                        Grid.SetColumn(textBlock5, 0);
                        Grid.SetRow(textBlock5, 4);
                        Grid.SetColumnSpan(textBlock5, 2);
                        grid.Children.Add(textBlock5);

                        TextBlock textBlock6 = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                            FontFamily = new FontFamily("Segoe Print"),
                            Text = res.Where(x => x.Id == number).First().Appearance
                        };
                        Grid.SetColumn(textBlock6, 0);
                        Grid.SetRow(textBlock6, 5);
                        Grid.SetColumnSpan(textBlock6, 2);
                        grid.Children.Add(textBlock6);

                        TextBlock textBlock7 = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            FontSize = 28,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontWeight = FontWeights.Bold,
                            FontFamily = new FontFamily("Segoe Print"),
                            TextAlignment = TextAlignment.Center,
                            Text = "Среда обитания"
                        };
                        Grid.SetColumn(textBlock7, 0);
                        Grid.SetRow(textBlock7, 6);
                        Grid.SetColumnSpan(textBlock7, 2);
                        grid.Children.Add(textBlock7);

                        TextBlock textBlock8 = new TextBlock()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                            FontFamily = new FontFamily("Segoe Print"),
                            Text = res.Where(x => x.Id == number).First().Habitat
                        };
                        Grid.SetColumn(textBlock8, 0);
                        Grid.SetRow(textBlock8, 7);
                        Grid.SetColumnSpan(textBlock8, 2);
                        grid.Children.Add(textBlock8);

                    }
                }
            }
        }

        private async void MouseDownBorder(object sender, MouseButtonEventArgs e)
        {

            var res = sender as Border;
            var ress = res.Parent as WrapPanel;

            if (PageNumber == 1)
                _typeNumber = Convert.ToInt32(res.Tag);

            if (PageNumber == 3) return;

            PageNumber++;

            await GetPage(ress, Convert.ToInt32(res.Tag));
        }
    }
}