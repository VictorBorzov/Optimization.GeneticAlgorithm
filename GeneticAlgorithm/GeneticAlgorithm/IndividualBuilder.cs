using System.Collections.Generic;

namespace Cli.GeneticAlgorithm
{
    public class IndividualBuilder<TGene>
    {
        public IndividualBuilder(int chromosomeLength)
        {
            ChromosomeLength = chromosomeLength;
        }

        private IList<TGene> GenesCache { get; } = new List<TGene>();

        private int ChromosomeLength { get; }

        public int RequiredGenesNumber => ChromosomeLength - GenesCache.Count;

        public IIndividual<TGene> GetIndividual()
        {
            var individual = new Individual<TGene>(GenesCache);
            GenesCache.Clear();
            return individual;
        }

        public void AddGene(TGene gene) => GenesCache.Add(gene);
    }
}