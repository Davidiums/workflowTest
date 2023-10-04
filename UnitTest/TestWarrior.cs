using TheGreatestWarrior;

namespace UnitTest
{
    [TestClass]
    public class TestWarrior
    {
        private Warrior _warrior;
        private Warrior _ennemyWarrior;

        [TestInitialize]
        public void Init()
        {
            _warrior = new Warrior();
            _ennemyWarrior = new Warrior();
        }

        [TestMethod]
        [DataRow(100, 1)]
        [DataRow(500, 5)]
        [DataRow(10000, 100)]
        public void Warrior_WithAnAmountOfExperience_ShouldCalculateExpecedLevel(int xp, int expectedLevel)
        {
            _warrior.Experience = xp;
            _warrior.AdjustLevel();
            Assert.AreEqual(expectedLevel, _warrior.Level);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(10, 2)]
        [DataRow(100, 11)]
        public void Warrior_WithLevel_RankShouldBe(int level, int expecedRank)
        {
            _warrior.Level = level;
            _warrior.AdjustRank();
            Assert.AreEqual(expecedRank, _warrior.ActualRank);
        }

        [TestMethod]
        [DataRow(2, "Novice")]
        [DataRow(10, "Master")]
        [DataRow(5, "Veteran")]
        public void Warrior_WithRank_RankNameShouldBe(int rank, string expectedRankName)
        {
            _warrior.ActualRank = rank;
            string actualRankName = _warrior.GetRankName();
            Assert.AreEqual(expectedRankName, actualRankName);
        }

        [TestMethod]
        [DataRow(0, 10)]
        [DataRow(2, 80)]
        [DataRow(-1, 5)]
        [DataRow(-2, 0)]
        [DataRow(6, 720)]
        public void AFight_WithADifferenceOfLevel_ShouldReturnThisAmountOfExperience(int differenceLevel, int amountOfExperienceExpected)
        {
            int returnedExperience = _warrior.CalculExperience(differenceLevel);
            Assert.AreEqual(amountOfExperienceExpected, returnedExperience);
        }

        [TestMethod]
        [ExpectedException(typeof(MaxLevelReachedException))]
        public void MaxedLevelWarrior_calculatingGainedExperience_ShouldThrowException()
        {
            _warrior.Level = 100;
            _warrior.CalculExperience(0);        
        }

        [TestMethod]
        [DataRow(1, 1, "A good fight")]
        [DataRow(1, 3, "An intense fight")]
        [DataRow(5, 4, "A good fight")]
        [DataRow(3, 9, "An intense fight")]
        [DataRow(8, 13, "You've been defeated")]
        public void EndOfFightMessage_Battle_ShouldBe(int level, int ennemyLevel, string expectedMessage)
        {
            _ennemyWarrior.Level = ennemyLevel;
            _warrior.Level = level;
            _ennemyWarrior.AdjustRank();
            _warrior.AdjustRank();
            string actualMessage = _warrior.BattleAgainst(_ennemyWarrior);

            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}