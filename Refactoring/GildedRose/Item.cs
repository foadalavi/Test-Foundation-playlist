namespace GildedRoseKata
{
    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        protected Item(string name, int sellIn, int quality)
        {
            Name = name;
            SellIn = sellIn;
            Quality = quality;
        }

        public static Item CreateItem(string name, int sellIn, int quality)
        {
            switch (name)
            {
                case "Aged Brie":
                    return new AgedBrie(name, sellIn, quality);
                case "Backstage passes to a TAFKAL80ETC concert":
                    return new BackstagePasses(name, sellIn, quality);
                case "Sulfuras, Hand of Ragnaros":
                    return new Sulfuras(name, sellIn, quality);
                default:
                    return new Item(name, sellIn, quality);
            }
        }

        public virtual void UpdateQulity()
        {
            if (Quality > 0)
            {
                Quality = Quality - 1;
            }
            SellIn = SellIn - 1;


            if (SellIn < 0)
            {
                if (Quality > 0)
                {
                    Quality = Quality - 1;
                }
            }
        }
    }
}