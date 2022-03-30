namespace Cli.GeneticAlgorithm
{
    public interface IFitnessProcessor<TGene> : IProcessor<TGene>
    {
        double Calculate(IIndividual<TGene> genes);
    }
}