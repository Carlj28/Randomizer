using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Randomizer.Algorithms;
using Randomizer.UI.ViewModels;
using System;
using System.Windows;

namespace Randomizer.UI
{
    /// <summary>
    /// https://msdn.microsoft.com/magazine/mt767700
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();

            InitializeComponent();
        }
    }
}
