using System.Collections.Generic;

namespace Cli.GeneticAlgorithm
{
    public interface IPopulation<TGene>
    {
        IPopulation<TGene> Initialize(int size, int chromosomeLength);
        IPopulation<TGene> Range();
        IPopulation<TGene> Select(double percentage);
        IPopulation<TGene> Crossover(int childrenNumber);
        IPopulation<TGene> Mutation(int mutantsNumber, int minPower = 1, int maxPower = 2);

        IReadOnlyList<IIndividual<TGene>> Individuals { get; }
        double BestFitness();

        double MedianFitness();

        IIndividual<TGene> BestIndividual { get; }
    }
}