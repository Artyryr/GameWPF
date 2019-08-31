using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWPF.Model
{
    public class Enemy : Base
    {
        public int Id { get; set; }
        //public Base Base { get; set; }
        //public int[] Position { get; set; }
        public BehaviorType Behavior { get; set; }
        public List<Enemy> Enemies { get; set; }


        public Enemy(int id)
        {
            //Base = new Base();
            Id = id;
        }
        public Enemy(int id, int[] position)
        {
            //Base = new Base();
            Id = id;
            Position = position; 
        }
        public Enemy(int id, int[] position, BehaviorType behavior)
        {
            //Base = new Base();
            Id = id;
            Position = position;
            Behavior = behavior;
        }
        public void LifeCycle()
        {
            //SetEnemies();
            if (Behavior == BehaviorType.Aggressor)
            {
                if(Army.totalArmy() <  ArmyLimit)
                {
                    ArmyCreation(0, getMaxNumberOfArmyCreation(), 0);
                }
                if(GetUpdatePrice(Hut)[0] <= Credits && GetUpdatePrice(Hut)[1] <= Goods && Hut.Lvl < 10)
                {
                    BuildingLvlUp(Hut);
                }
            }
            else if( Behavior == BehaviorType.Builder)
            {

            }
        }

        public void SetEnemies(List<Enemy> enemies, Base playerBase)
        {
            enemies[Id] = playerBase as Enemy;
            Enemies = enemies;
        }

        public void ArmyCreation(int speed, int attack, int defence)
        {
            int totalPrice = 10 * (speed + attack + defence);
            int totalGoods = 10 * (speed + attack + defence);
            int totalPeople = speed + attack + defence;

            if (Credits - totalPrice >= 0 && Goods - totalGoods >= 0 && Population - totalPeople >= 0)
            {
                if (Army.totalArmy() + totalPeople <= ArmyLimit)
                {
                    Credits -= totalPrice;
                    Goods -= totalGoods;
                    Population -= totalPeople;
                    Army.AttackUnits += attack;
                    Army.DefenceUnits += defence;
                    Army.SpeedUnits += speed;
                }
            }
        }
        public int getMaxNumberOfArmyCreation()
        {
            int price = Convert.ToInt32(Credits / 10);
            int goods = Convert.ToInt32(Goods / 10);
            int population = Convert.ToInt32(Population / 10);
            int min = new int[] { price, goods, population }.Min();
            return min;
        }
    }
}
