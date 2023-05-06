using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata
{
    internal class AgedBrie : Item
    {
        public AgedBrie(string name, int sellIn, int quality) : base(name, sellIn, quality)
        {
        }

        public override void UpdateQulity()
        {
            if (Quality < 50)
            {
                Quality = Quality + 1;
            }

            SellIn = SellIn - 1;

            if (SellIn < 0)
            {
                if (Quality < 50)
                {
                    Quality = Quality + 1;
                }
            }
        }
    }
}
