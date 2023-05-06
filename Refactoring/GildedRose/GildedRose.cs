namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                UpdateQuality(i);
            }
        }

        private void UpdateQuality(int i)
        {
            Item item = Items[i];
            item.UpdateQulity();
        }
    }
}