using System;
using System.Collections.Generic;

namespace Cli.GeneticAlgorithm
{
    public static class GeneticAlgorithmManager
    {
        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        public static IList<TGene> Start<TGene>(IEnumerable<TGene> data, Func<IList<TGene>, double> fitnessFunction, GenericAlgorithmOptions options, Logger logger)
        {
            var inputData = new Data<TGene>(data);

            var genericAlgorithmContext = new GenericAlgorithmContext<TGene>(
                new CrossoverProcessor<TGene>(options.ParentsSelectionProbabilitySlope),
                new RandomMutationProcessor<TGene>(),
                new SimpleTakeFirstSelectionProcessor<TGene>(),
                inputData,
                new CustomFunctionFitnessProcessor<TGene>(individual => fitnessFunction(individual.Genes)),
                options.TargetFitness);

            var population = new Population<TGene>(genericAlgorithmContext).Initialize(options.PopulationSize, options.ChromosomeLength);

            var bestFitness = new List<double>();
            var medianFitness = new List<double>();
            for (var i = 0; i < options.Epochs; i++)
            {
                logger.LogProgress(population);
                population = population
                    .Crossover(options.ChildrenNumber)
                    .Mutation(options.MutantsNumber)
                    .Range()
                    .Select(options.SelectionPercentage);
                bestFitness.Add(population.BestFitness());
                medianFitness.Add(population.MedianFitness());
            }

            logger.LogBestFitness(population);
            logger.LogMedianFitness(population);
            logger.LogBestIndividual(population);
            logger.LogBestFitnessAsciiPlot(bestFitness);
            logger.LogMedianFitnessAsciiPlot(medianFitness);

            return population.BestIndividual.Genes;
        }
    }
}