using System;

namespace Cli.GeneticAlgorithm
{
    public sealed class RandomMutationProcessor<TGene> : ProcessorBase<TGene>, IMutationProcessor<TGene>
    {
        private readonly Random _randomizer = new ();

        public IIndividual<TGene> Mutate(IIndividual<TGene> individual, int power)
        {
            var mutant = individual;
            for (var i = 0; i < power; i++)
                mutant = RandomMutation(mutant);
            OnLog($"Mutation: {individual} -> {mutant}");
            return mutant;
        }

        private IIndividual<TGene> RandomMutation(IIndividual<TGene> individual)
        {
            if (Context is null)
                throw new InvalidOperationException();

            var result = individual.Clone();
            var geneIndex = _randomizer.Next(individual.Genes.Count);
            result.Genes[geneIndex] = Context.Data.GetRandomGene();
            return result;
        }
    }
}