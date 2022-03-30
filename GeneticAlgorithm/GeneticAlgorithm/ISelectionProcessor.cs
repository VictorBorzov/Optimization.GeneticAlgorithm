using System.Collections.Generic;

namespace Cli.GeneticAlgorithm
{
    public interface ISelectionProcessor<TGene> : IProcessor<TGene>
    {
        IList<IIndividual<TGene>> Select(IEnumerable<IIndividual<TGene>> population, int count);
    }
}