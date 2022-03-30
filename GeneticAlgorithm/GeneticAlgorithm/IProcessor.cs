using System;

namespace Cli.GeneticAlgorithm
{
    public interface IProcessor<TGene>
    {
        void Initialize(GenericAlgorithmContext<TGene> context);

        event EventHandler<string>? Log;
    }
}