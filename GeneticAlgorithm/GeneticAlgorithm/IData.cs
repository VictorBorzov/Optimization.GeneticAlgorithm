namespace Cli.GeneticAlgorithm
{
    public interface IData<out TGene>
    {
        TGene GetRandomGene();
    }
}