using System.Collections.Generic;
using System.Linq;

namespace Cli.GeneticAlgorithm
{
    internal sealed class SimpleTakeFirstSelectionProcessor<TGene> : ProcessorBase<TGene>, ISelectionProcessor<TGene>
    {
        public IList<IIndividual<TGene>> Select(IEnumerable<IIndividual<TGene>> population, int count) => population.Take(count).ToList();
    }
}