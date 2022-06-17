using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.PLACEHOLDER
{
    // TODO REMOVE IF NEEDED
    // PLACEHOLDER weight randomization for weighted categories
    public static class RandomWeightGenerator
    {
        private const double _minWeight = 0.5;
        private const double _maxWeight = 2.5;
        public static decimal Random()
        {
            Random random = new Random();
            return decimal.Round(Convert.ToDecimal(random.NextDouble() * (_maxWeight - _minWeight) + _minWeight),2,MidpointRounding.AwayFromZero);
        }
    }
}
