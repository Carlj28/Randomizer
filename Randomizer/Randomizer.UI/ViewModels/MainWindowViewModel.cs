using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Randomizer.Algorithms;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private List<string> algorithms;
        private string selectedAlgorithm, tryCounts, counter;
        private double average, variation, median;
        private List<int> randoms;
        private SeriesCollection algorithmSeriesCollection;

        public MainWindowViewModel()
        {
            this.randoms = new List<int>();
            this.Algorithms = new List<string>();

            this.Algorithms.Add("System.Random");
            this.Algorithms.Add("Lagged Fibonacci");
            this.Algorithms.Add("Lehmer");
            this.Algorithms.Add("Linear Congruential");
            this.Algorithms.Add("Wichmann");

            this.SelectedAlgorithm = this.Algorithms.First();

            this.WhenAnyValue(x => x.SelectedAlgorithm).Subscribe(_ => this.Recalculate());
            this.WhenAnyValue(x => x.TryCounts).Subscribe(_ => this.Recalculate());
            this.WhenAnyValue(x => x.Counter).Subscribe(_ => this.Recalculate());
        }

        public SeriesCollection AlgorithmSeriesCollection
        {
            get { return this.algorithmSeriesCollection; }
            set { this.RaiseAndSetIfChanged(ref this.algorithmSeriesCollection, value); }
        }

        public List<string> Algorithms
        {
            get { return this.algorithms; }
            set { this.RaiseAndSetIfChanged(ref this.algorithms, value); }
        }
        public string SelectedAlgorithm
        {
            get { return this.selectedAlgorithm; }
            set { this.RaiseAndSetIfChanged(ref this.selectedAlgorithm, value); }
        }

        public string TryCounts
        {
            get { return this.tryCounts; }
            set { this.RaiseAndSetIfChanged(ref this.tryCounts, value); }
        }

        public string Counter
        {
            get { return this.counter; }
            set { this.RaiseAndSetIfChanged(ref this.counter, value); }
        }

        public double Average
        {
            get { return this.average; }
            set { this.RaiseAndSetIfChanged(ref this.average, value); }
        }

        public double Variation
        {
            get { return this.variation; }
            set { this.RaiseAndSetIfChanged(ref this.variation, value); }
        }

        public double Median
        {
            get { return this.median; }
            set { this.RaiseAndSetIfChanged(ref this.median, value); }
        }

        private void GetAverage()
        {
            int sum = 0;
            foreach (var random in this.randoms)
                sum += random;

            this.Average = sum / this.randoms.Count;
        }

        private void GetVariation()
        {
            double x = 0;
            foreach (var random in this.randoms)
                x += Math.Pow(random + this.Average, 2);

            this.Variation = x / this.randoms.Count;
        }

        private void GetMedian()
        {
            this.randoms.Sort();
            var index = (int)Math.Round((double)(this.randoms.Count / 2), MidpointRounding.AwayFromZero);

            this.Median = this.randoms[index]; 
        }

        private void Recalculate()
        {
            int random, x, y;
            this.randoms.Clear();

            if (!int.TryParse(this.TryCounts, out x))
                x = 100;

            if (!int.TryParse(this.counter, out y))
                y = 100;

            AlgorithmSeriesCollection = new SeriesCollection
            {
                new ScatterSeries
                {
                    Values = new ChartValues<ScatterPoint>{},
                    MinPointShapeDiameter = 15,
                    MaxPointShapeDiameter = 20,
                }
            };

            switch (this.selectedAlgorithm)
            {
                case "System.Random":
                    Random r = new Random();

                    for (int i = 0; i < x; i++)
                    {
                        random = r.Next(y);
                        this.randoms.Add(random);
                        this.AlgorithmSeriesCollection[0].Values.Add(new ScatterPoint(i, random, 0.1));
                    }
                    break;
                case "Lehmer":
                    LehmerAlgorithm lehmerAlgorithm = new LehmerAlgorithm(100);

                    for (int i = 0; i < x; i++)
                    {
                        random = lehmerAlgorithm.Next(y);
                        this.randoms.Add(random);
                        this.AlgorithmSeriesCollection[0].Values.Add(new ScatterPoint(i, random, 0.1));
                    }
                    break;
                case "Wichmann":
                    WichmannAlgorithm wichmannAlgorithm = new WichmannAlgorithm(100);

                    for (int i = 0; i < x; i++)
                    {
                        random = wichmannAlgorithm.Next(y);
                        this.randoms.Add(random);
                        this.AlgorithmSeriesCollection[0].Values.Add(new ScatterPoint(i, random, 0.1));
                    }
                    break;
                case "Lagged Fibonacci":
                    var laggedFibonacciAlgorithm = new LaggedFibonacciAlgorithm(100);

                    for (int i = 0; i < x; i++)
                    {
                        random = laggedFibonacciAlgorithm.Next(y);
                        this.randoms.Add(random);
                        this.AlgorithmSeriesCollection[0].Values.Add(new ScatterPoint(i, random, 0.1));
                    }
                    break;
                case "Linear Congruential":
                    var linearCongruentialAlgorithm = new LinearCongruentialAlgorithm(100);

                    for (int i = 0; i < x; i++)
                    {
                        random = linearCongruentialAlgorithm.Next(y);
                        this.randoms.Add(random);
                        this.AlgorithmSeriesCollection[0].Values.Add(new ScatterPoint(i, random, 0.1));
                    }
                    break;
            }

            this.CalculateStatistics();
        }

        private void CalculateStatistics()
        {
            this.GetAverage();
            this.GetVariation();
            this.GetMedian();
        }
    }

}
