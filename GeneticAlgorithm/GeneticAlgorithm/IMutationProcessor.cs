namespace Cli.GeneticAlgorithm
{
    public interface IMutationProcessor<TGene> : IProcessor<TGene>
    {
        IIndividual<TGene> Mutate(IIndividual<TGene> individual, int power);
    }
}