using System;
using System.ComponentModel.DataAnnotations;

namespace Cli.GeneticAlgorithm
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IsOddAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value) => value is int inputValue && inputValue % 2 == 0;
    }
}