using System;
using System.Collections.Generic;
using System.Linq;

namespace Cli.GeneticAlgorithm
{
    public class Data<TGene> : IData<TGene>
    {
        private readonly IList<TGene> _data;

        private readonly Random _randomizer = new ();

        public Data(IEnumerable<TGene> data)
        {
            _data = data.ToList();
        }

        public TGene GetRandomGene() => _data[_randomizer.Next(0, _data.Count)];
    }
}