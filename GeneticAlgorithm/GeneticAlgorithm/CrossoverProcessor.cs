using System;
using System.Collections.Generic;
using System.Linq;

namespace Cli.GeneticAlgorithm
{
    public sealed class CrossoverProcessor<TGene> : ProcessorBase<TGene>, ICrossoverProcessor<TGene>
    {
        private readonly Random _randomizer = new ();

        private readonly double _parentSelectionProbabilitySlope;

        public CrossoverProcessor(double parentSelectionProbabilitySlope)
        {
            _parentSelectionProbabilitySlope = parentSelectionProbabilitySlope;
        }

        public IEnumerable<IIndividual<TGene>> Run(IPopulation<TGene> population, int childrenNumber)
        {
            if (Context is null)
                throw new InvalidOperationException();

            var result = new List<IIndividual<TGene>>();

            if (population.Individuals.Count == 0)
                return result;

            population.Range();

            if (childrenNumber % 2 == 1)
                childrenNumber++;

            for (var i = 0; i < childrenNumber / 2; i++)
            {
                var parent1 = SelectParent(population.Individuals, _parentSelectionProbabilitySlope);
                var parent2 = SelectParent(population.Individuals.Where(individual => individual != parent1).ToList(), 0.5);

                var (child1, child2) = Hug(parent1, parent2);
                result.Add(child1);
                result.Add(child2);
            }

            return result;
        }

        private (IIndividual<TGene> child1, IIndividual<TGene> child2) Hug(IIndividual<TGene> parent1, IIndividual<TGene> parent2)
        {
            var individualBuilder = new IndividualBuilder<TGene>(parent1.Genes.Count);
            var splitPoint = _randomizer.Next(parent1.Genes.Count);

            foreach (var gene in parent1.Genes.Take(splitPoint))
                individualBuilder.AddGene(gene);

            foreach (var gene in parent2.Genes.TakeLast(parent2.Genes.Count - splitPoint))
                individualBuilder.AddGene(gene);

            var child1 = individualBuilder.GetIndividual();
            OnLog($"Crossover: {parent1} & {parent2} -> {child1}");

            foreach (var gene in parent2.Genes.Take(splitPoint))
                individualBuilder.AddGene(gene);

            foreach (var gene in parent1.Genes.TakeLast(parent1.Genes.Count - splitPoint))
                individualBuilder.AddGene(gene);

            var child2 = individualBuilder.GetIndividual();
            OnLog($"Crossover: {parent1} & {parent2} -> {child2}");

            return (child1, child2);
        }

        /// <summary>
        /// Выбирает родителя для кроссинговера из отранжированной популяции.
        /// </summary>
        /// <param name="population">Популяция, отранжированная по убыванию по приспособленности.</param>
        /// <param name="probabilitySlope">К-т изменения вероятности. Пример: probabilitySlope = 0.5, p0 = 0.5, p1 = 0.25, p2 = 0.125, ...</param>
        /// <returns>Возвращает выбранного индивида или null, если популяция пустая.</returns>
        private IIndividual<TGene> SelectParent(IReadOnlyList<IIndividual<TGene>> population, double probabilitySlope)
        {
            probabilitySlope = probabilitySlope switch
            {
                <= 0 => 0.01,
                >= 1 => 0.99,
                _ => probabilitySlope,
            };

            var random = _randomizer.NextDouble();
            var previousProbability = 0d;
            for (var i = 1; i < population.Count; i++)
            {
                var probability = previousProbability + Math.Pow(probabilitySlope, i);
                if (random <= probability)
                    return population[i];
                previousProbability = probability;
            }

            return population[^1];
        }
    }
}