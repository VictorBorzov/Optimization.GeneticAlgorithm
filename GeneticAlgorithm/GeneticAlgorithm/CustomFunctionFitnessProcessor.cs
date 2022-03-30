using System;

namespace Cli.GeneticAlgorithm
{
    public sealed class CustomFunctionFitnessProcessor<TGene> : ProcessorBase<TGene>, IFitnessProcessor<TGene>
    {
        private readonly Func<IIndividual<TGene>, double> _fitnessFunction;

        public CustomFunctionFitnessProcessor(Func<IIndividual<TGene>, double> fitnessFunction)
        {
            _fitnessFunction = fitnessFunction;
        }

        public double Calculate(IIndividual<TGene> individual)
        {
            var fitness = _fitnessFunction(individual);
            OnLog($"Fitness: {individual} -> {fitness}");
            return fitness;
        }
    }
}