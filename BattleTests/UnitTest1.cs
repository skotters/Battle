
using Battle;

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


        }
    }



}