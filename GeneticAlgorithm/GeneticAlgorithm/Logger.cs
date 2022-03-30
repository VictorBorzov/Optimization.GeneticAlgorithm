using System;
using System.Collections.Generic;
using System.Globalization;
using Cli.AsciiChart;

namespace Cli.GeneticAlgorithm
{
    public readonly struct Logger
    {
        public bool Progress { get; init; }

        public bool BestIndividual { get; init; }

        public bool BestFitness { get; init; }

        public bool MedianFitness { get; init; }

        public bool AsciiPlotBestFitness { get; init; }

        public bool AsciiPlotMedianFitness { get; init; }

        public Action<string>? Log { get; init; }

        public Options PlotOptions { get; init; }

        public void LogBestFitness<TGene>(IPopulation<TGene> population)
        {
            if (BestFitness)
                Log?.Invoke($"Best fitness: {population.BestFitness().ToString(CultureInfo.InvariantCulture)}");
        }

        public void LogMedianFitness<TGene>(IPopulation<TGene> population)
        {
            if (MedianFitness)
                Log?.Invoke($"Median fitness: {population.MedianFitness().ToString(CultureInfo.InvariantCulture)}");
        }

        public void LogBestIndividual<TGene>(IPopulation<TGene> population)
        {
            if (BestIndividual)
                Log?.Invoke($"Best individual: {population.BestIndividual.ToString() ?? string.Empty}");
        }

        public void LogProgress<TGene>(IPopulation<TGene> population)
        {
            if (!Progress)
                return;

            LogBestFitness(population);
            LogMedianFitness(population);
            LogBestIndividual(population);
        }

        public void LogBestFitnessAsciiPlot(IEnumerable<double> series)
        {
            if (!AsciiPlotBestFitness)
                return;

            Log?.Invoke("Best fitness:");
            Log?.Invoke(Graph.Plot(series, PlotOptions));
        }

        public void LogMedianFitnessAsciiPlot(IEnumerable<double> series)
        {
            if (!AsciiPlotMedianFitness)
                return;

            Log?.Invoke("Median fitness:");
            Log?.Invoke(Graph.Plot(series, PlotOptions));
        }

        public static Options FormatPlot { get; } = new ()
        {
            AxisLabelLeftMargin = 3,
            AxisLabelRightMargin = 0,
            Height = 4,
            Fill = '·',
            AxisLabelFormat = "0,000.000",
        };

        public static Logger All { get; } = new ()
        {
            Progress = true,
            BestFitness = true,
            MedianFitness = true,
            AsciiPlotBestFitness = true,
            AsciiPlotMedianFitness = true,
            BestIndividual = true,
            Log = null,
            PlotOptions = FormatPlot,
        };

        public static Logger Simple { get; } = new ()
        {
            Progress = false,
            BestFitness = true,
            MedianFitness = true,
            AsciiPlotBestFitness = false,
            AsciiPlotMedianFitness = false,
            BestIndividual = true,
            Log = null,
            PlotOptions = FormatPlot,
        };

        public static Logger Info { get; } = new ()
        {
            Progress = false,
            BestFitness = true,
            MedianFitness = true,
            AsciiPlotBestFitness = true,
            AsciiPlotMedianFitness = true,
            BestIndividual = true,
            Log = null,
            // PlotOptions = FormatPlot,
        };
    }
}