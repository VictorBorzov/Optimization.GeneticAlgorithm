using System;
using System.Collections.Generic;
using System.Linq;

namespace Cli.GeneticAlgorithm
{
    public class Population<TGene> : IPopulation<TGene>
    {
        private readonly Random _randomizer = new ();

        private IList<IIndividual<TGene>> _individuals = new List<IIndividual<TGene>>();

        private bool _ranged;

        public Population(GenericAlgorithmContext<TGene> context)
        {
            Context = context;
        }

        private GenericAlgorithmContext<TGene> Context { get; }

        public IPopulation<TGene> Initialize(int size, int chromosomeLength)
        {
            _individuals.Clear();
            var individualBuilder = new IndividualBuilder<TGene>(chromosomeLength);

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < chromosomeLength; j++)
                    individualBuilder.AddGene(Context.Data.GetRandomGene());
                _individuals.Add(individualBuilder.GetIndividual());
            }

            _ranged = false;

            return this;
        }

        public IPopulation<TGene> Range()
        {
            if (_ranged)
                return this;

            _individuals = Context.TargetFitness switch
            {
                TargetFitness.Minimize => _individuals.OrderBy(individual => Context.FitnessProcessor.Calculate(individual)).ToList(),
                TargetFitness.Maximize => _individuals.OrderByDescending(individual => Context.FitnessProcessor.Calculate(individual)).ToList(),
                _ => throw new ArgumentOutOfRangeException(),
            };
            _ranged = true;

            return this;
        }

        public IPopulation<TGene> Select(double percentage)
        {
            percentage = percentage switch
            {
                >= 0 and <= 1 => percentage,
                < 0 => 0,
                > 1 => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(percentage), percentage, null),
            };

            _individuals = Context.SelectionProcessor.Select(_individuals, (int)Math.Round(percentage * _individuals.Count, MidpointRounding.ToZero));

            return this;
        }

        public IPopulation<TGene> Crossover(int childrenNumber)
        {
            foreach (var child in Context.CrossoverProcessor.Run(this, childrenNumber))
                _individuals.Add(child);
            _ranged = false;

            return this;
        }

        public IPopulation<TGene> Mutation(int mutantsNumber, int minPower = 1, int maxPower = 2)
        {
            for (var i = 0; i < mutantsNumber; i++)
                _individuals.Add(Context.MutationProcessor.Mutate(GetRandomIndividual(), _randomizer.Next(minPower, maxPower)));
            _ranged = false;

            return this;
        }

        public IReadOnlyList<IIndividual<TGene>> Individuals => _individuals.ToList();

        public double BestFitness()
        {
            Range();
            return Context.FitnessProcessor.Calculate(_individuals.First());
        }

        public double MedianFitness()
        {
            Range();
            return _individuals.Count % 2 == 0
                ? (Context.FitnessProcessor.Calculate(_individuals[_individuals.Count / 2]) + Context.FitnessProcessor.Calculate(_individuals[_individuals.Count / 2 + 1])) / 2
                : Context.FitnessProcessor.Calculate(_individuals[_individuals.Count / 2]);
        }

        public IIndividual<TGene> BestIndividual
        {
            get
            {
                Range();
                return _individuals.First();
            }
        }

        private IIndividual<TGene> GetRandomIndividual() => _individuals[_randomizer.Next(0, _individuals.Count)];
    }
}