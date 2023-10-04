using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatestWarrior
{
    public class Warrior
    {
        private readonly byte[] _levelRange = { 1, 100 };
        public int Level { get; set; }
        public int Experience { get; set; }
        public readonly string[] _rank = { "Pushover", "Novice", "Fighter", "Warrior", "Veteran", "Sage", "Elite", "Conqueror", "Champion", "Master", "Greatest" };
        public int ActualRank { get; set; }
        public bool IsAlive;

        public Warrior()
        {
            Level = 1;
            Experience = 100;
            ActualRank = 0;
            IsAlive = true;
        }

        public string BattleAgainst(Warrior ennemy)
        {
            int levelDifference = ennemy.Level - Level;
            //ennemy has same rank or lower cases
            if(ennemy.ActualRank <= ActualRank)
            {
                this.AddExperience(ennemy.Level);
                switch (levelDifference)
                {
                    case < -2:
                        return "Easy fight";
                    case <= 0:
                        return "A good fight";
                    case > 1:
                        return "An intense fight";
                }
            }

            //ennmy has higher rank case
            if(levelDifference < 3) 
            {
                this.AddExperience(ennemy.Level);
                return "An intense fight";
            }

            IsAlive = false;
            ennemy.AddExperience(this.Level);
            return "You've been defeated";
        }

        public void AddExperience(int ennemyLevel)
        {
            int levelDifference = ennemyLevel - Level;
            if(Level < 100 ) Experience += CalculExperience(levelDifference);
            AdjustLevel();
        }

        public int CalculExperience(int levelDifference)
        {
            if (Level >= 100) throw new MaxLevelReachedException();
            if (levelDifference > 0)
            {
                return 20 * levelDifference * levelDifference;
            }
            else if (levelDifference == 0)
            {
                return 10;
            }
            else if (levelDifference == -1)
            {
                return 5;
            }
            return 0;
        }

        public void AdjustLevel() 
        {
             Level = this.Experience / 100;
            AdjustRank();
        }

        public void AdjustRank() 
        {
            ActualRank = this.Level / 10 +1;
        }

        public string GetRankName() 
        {
            return this._rank[ActualRank - 1];
        }
    }
    public class MaxLevelReachedException : Exception
    {
        public MaxLevelReachedException(string? message = "Le niveau maximum est déjà atteind.") : base(message)
        {
        }
    }
}
