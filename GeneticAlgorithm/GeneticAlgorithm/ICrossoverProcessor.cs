using System.Collections.Generic;

namespace Cli.GeneticAlgorithm
{
    public interface ICrossoverProcessor<TGene> : IProcessor<TGene>
    {
        IEnumerable<IIndividual<TGene>> Run(IPopulation<TGene> population, int childrenNumber);
    }
}