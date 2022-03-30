using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cli.GeneticAlgorithm;
using NUnit.Framework;

namespace GeneticAlgorithm.Tests;

public class IntegrationTests
{
    [Test]
    [TestCase(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, ExpectedResult = 90)]
    public double T01_FindMaxSum(int[] testValues)
    {
        double FitnessFunction(IList<int> values) => values.Sum();
        Console.OutputEncoding = Encoding.UTF8;
        var best = GeneticAlgorithmManager.Start(
            testValues,
            FitnessFunction,
            GenericAlgorithmOptions.Default with { TargetFitness = TargetFitness.Maximize, MutantsNumber = 30, ChildrenNumber = 40, PopulationSize = 50 },
            Logger.Info with { Log = _ => {} }
        );
        return FitnessFunction(best);
    }
    
    [Test]
    [TestCase(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, ExpectedResult = 0)]
    public double T02_FindMinSum(int[] testValues)
    {
        double FitnessFunction(IList<int> values) => values.Sum();
        Console.OutputEncoding = Encoding.UTF8;
        var best = GeneticAlgorithmManager.Start(
            testValues,
            FitnessFunction,
            GenericAlgorithmOptions.Default with { TargetFitness = TargetFitness.Minimize, MutantsNumber = 30, ChildrenNumber = 40, PopulationSize = 50 },
            Logger.Info with { Log = _ => {} }
        );
        return FitnessFunction(best);
    }
}