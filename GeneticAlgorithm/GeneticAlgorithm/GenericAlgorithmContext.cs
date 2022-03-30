using System;

namespace Cli.GeneticAlgorithm
{
    public class GenericAlgorithmContext<TGene>
    {
        public GenericAlgorithmContext(
            ICrossoverProcessor<TGene> crossoverProcessor,
            IMutationProcessor<TGene> mutationProcessor,
            ISelectionProcessor<TGene> selectionProcessor,
            IData<TGene> data,
            IFitnessProcessor<TGene> fitnessProcessor,
            TargetFitness targetFitness)
        {
            CrossoverProcessor = crossoverProcessor;
            CrossoverProcessor.Initialize(this);
            FitnessProcessor = fitnessProcessor;
            FitnessProcessor.Initialize(this);
            MutationProcessor = mutationProcessor;
            MutationProcessor.Initialize(this);
            SelectionProcessor = selectionProcessor;
            SelectionProcessor.Initialize(this);
            Data = data;
            TargetFitness = targetFitness;
        }

        public ICrossoverProcessor<TGene> CrossoverProcessor { get; }

        public IMutationProcessor<TGene> MutationProcessor { get; }

        public ISelectionProcessor<TGene> SelectionProcessor { get; }

        public IData<TGene> Data { get; }

        public IFitnessProcessor<TGene> FitnessProcessor { get; }

        public TargetFitness TargetFitness { get; }

        public void AddLogger(EventHandler<string> logger)
        {
            SelectionProcessor.Log += logger;
            CrossoverProcessor.Log += logger;
            FitnessProcessor.Log += logger;
            MutationProcessor.Log += logger;
        }
    }
}