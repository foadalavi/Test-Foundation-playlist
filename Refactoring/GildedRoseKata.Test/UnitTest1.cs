using Xunit.Abstractions;

namespace GildedRoseKata.Test
{
    [UsesVerify]
    public class UnitTest1
    {
        [Fact]
        public Task Test1()
        {
            // Arrange
            var items = new List<Item>();


            var names = new[] { "foo", "Aged Brie", "Backstage passes to a TAFKAL80ETC concert", "Sulfuras, Hand of Ragnaros" };
            var sellIns = new[] { -1, 0, 1, 5, 6, 7, 10, 11, 12 };
            var qualites = new[] { -1, 0, 1, 49, 50, 51 };

            foreach (var name in names)
            {
                foreach (var sellIn in sellIns)
                {
                    foreach (var quality in qualites)
                    {
                        items.Add(Item.CreateItem(name, sellIn, quality));
                    }
                }
            }

            var instance = new GildedRose(items);

            // Act
            instance.UpdateQuality();

            // Assert
            return Verifier.Verify(items);
        }
    }
}