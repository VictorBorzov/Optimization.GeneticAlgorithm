using System.Collections.Generic;

namespace Cli.GeneticAlgorithm
{
    public class Individual<TGene> : IIndividual<TGene>
    {
        public Individual(IEnumerable<TGene> genes)
        {
            Genes = new List<TGene>(genes);
        }

        public IList<TGene> Genes { get; }

        public IIndividual<TGene> Clone() => new Individual<TGene>(new List<TGene>(Genes));

        public override string ToString() => $"[{string.Join(',', Genes)}]";
    }
}