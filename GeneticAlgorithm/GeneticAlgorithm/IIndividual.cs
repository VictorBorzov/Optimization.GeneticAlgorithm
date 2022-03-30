using System.Collections.Generic;

namespace Cli.GeneticAlgorithm
{
    public interface IIndividual<TGene>
    {
        IList<TGene> Genes { get; }

        IIndividual<TGene> Clone();
    }
}