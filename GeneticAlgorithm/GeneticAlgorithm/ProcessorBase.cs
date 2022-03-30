using System;

namespace Cli.GeneticAlgorithm
{
    public class ProcessorBase<TGene> : IProcessor<TGene>
    {
        protected GenericAlgorithmContext<TGene>? Context { get; private set; }

        public event EventHandler<string>? Log;

        public void Initialize(GenericAlgorithmContext<TGene> context)
        {
            Context = context;
        }

        protected void OnLog(string e)
        {
            Log?.Invoke(this, e);
        }
    }
}