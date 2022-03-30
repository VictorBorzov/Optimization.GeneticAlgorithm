using System.ComponentModel.DataAnnotations;

namespace Cli.GeneticAlgorithm
{
    public readonly struct GenericAlgorithmOptions
    {
        public static GenericAlgorithmOptions Default { get; } = new ()
        {
            Epochs = 30,
            PopulationSize = 20,
            ChromosomeLength = 10,
            ChildrenNumber = 10,
            MutantsNumber = 10,
            SelectionPercentage = 0.5,
            ParentsSelectionProbabilitySlope = 0.7,
            TargetFitness = TargetFitness.Minimize,
        };

        [Required(ErrorMessage = "Number of epochs is required")]
        [Range(0, int.MaxValue)]
        public int Epochs { get; init; }

        [Required(ErrorMessage = "Number of children is required")]
        [Range(0, int.MaxValue)]
        [IsOdd]
        public int ChildrenNumber { get; init; }

        [Required(ErrorMessage = "Number of mutants is required")]
        [Range(0, int.MaxValue)]
        public int MutantsNumber { get; init; }

        [Required(ErrorMessage = "Population size is required")]
        [Range(0, int.MaxValue)]
        public int PopulationSize { get; init; }

        [Required(ErrorMessage = "Chromosome length required")]
        [Range(0, int.MaxValue)]
        public int ChromosomeLength { get; init; }

        [Required(ErrorMessage = "Selection percentage is required")]
        [Range(0, 1)]
        public double SelectionPercentage { get; init; }

        [Required(ErrorMessage = "Parameter is required")]
        [Range(0, 1)]
        public double ParentsSelectionProbabilitySlope { get; init; }


        [Required(ErrorMessage = "Target fitness parameter is required")]
        public TargetFitness TargetFitness { get; init; }
    }
}