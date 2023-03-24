
using Battle;
using Battle.Items;

namespace BattleTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MonsterTakeDamageTest()
        {
            Monster monster = new Monster
            {
                CurrentHP = 100
            };

            monster.TakeDmg(20);

            int expected = 80;

            int actual = monster.CurrentHP;

            Assert.AreEqual(expected, actual);  
            
        }

        [TestMethod]
        public void GoldSpendingTest()
        {
            Player p1 = new Player
            {
                gold = 500
            };

            Store.BuyItem(p1, new HealthPotion(), HealthPotion.Cost);
            Store.BuyItem(p1, new HealthPotion(), HealthPotion.Cost);
            Store.BuyItem(p1, new MagicPotion(), MagicPotion.Cost);
            Store.BuyItem(p1, new Antidote(), Antidote.Cost);
            Store.BuyItem(p1, new Antidote(), Antidote.Cost);
            Store.BuyItem(p1, new Antidote(), Antidote.Cost);
            Store.BuyItem(p1, new Sword(), Sword.Cost);
            Store.BuyItem(p1, new Sword(), Sword.Cost);
            Store.BuyItem(p1, new Sword(), Sword.Cost);
            Store.BuyItem(p1, new Armor(), Armor.Cost);
            Store.BuyItem(p1, new Armor(), Armor.Cost);

            int expected = 240;

            int actual = p1.gold;

            Assert.AreEqual(expected, actual);  
        }

        [TestMethod]
        [DataRow(100, 50, "[||||||||||          ]")]
        [DataRow(100, 51, "[||||||||||          ]")]
        [DataRow(100, 55, "[|||||||||||         ]")]
        [DataRow(100, 1,  "[|                   ]")] //one bar equals 5 hp by default but having health where 0<x<5 still needs to display one bar
        [DataRow(200, 25, "[||                  ]")]
        [DataRow(100, 99, "[||||||||||||||||||| ]")]
        public void VisualBarLineCountTest(int startingHP, int currentHP, string expected)
        {
            string actual = VisualMeter.GetFullMeterString(startingHP, currentHP);
            Assert.AreEqual(expected, actual);
        }


    }
}