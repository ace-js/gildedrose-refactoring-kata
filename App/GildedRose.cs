using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class GildedRose
    {
        private const int ZERO = 0;
        private const int MAX_QUALITY = 50;
        private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
        private const string AGED_BRIE = "Aged Brie";
        private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
        private const string CONJURED = "Conjured Mana Cake";

        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            var specialItems = new[] {AGED_BRIE, BACKSTAGE_PASSES, CONJURED};
            foreach (var item in Items)
            {
                item.SellIn--;
                if (specialItems.Any(specialItem => specialItem == item.Name))
                {
                    UpdateSpecialItemQuality(item);
                }
                else if (item.Name != SULFURAS)
                {
                    DecreaseQuality(item);
                }
            }
        }

        private void UpdateSpecialItemQuality(Item item)
        {
            switch (item.Name)
            {
                case AGED_BRIE:
                    if (item.SellIn < ZERO) DecreaseQuality(item);
                    else IncreaseQuality(item);
                    break;

                case BACKSTAGE_PASSES:
                    if (item.SellIn < ZERO) DecreaseQuality(item, item.Quality);
                    else
                    {
                        var increaseValue = 1;
                        if (item.SellIn <= 10)
                        {
                            increaseValue = item.SellIn > 5 ? 2 : 3;
                        }

                        IncreaseQuality(item, increaseValue);
                    }

                    break;

                case CONJURED:
                    DecreaseQuality(item, 2);
                    break;
            }
        }

        private void DecreaseQuality(Item item, int value = 1)
        {
            if (item.Quality > ZERO)
            {
                var newQuality = item.Quality - value;
                item.Quality = newQuality > ZERO ? newQuality : ZERO;
            }
        }

        private void IncreaseQuality(Item item, int value = 1)
        {
            if (item.Quality < MAX_QUALITY)
            {
                var newQuality = item.Quality + value;
                item.Quality = newQuality > MAX_QUALITY ? MAX_QUALITY : newQuality;
            }
        }
    }
}