using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGCombat
{

    public enum FighterType
    {
        MeleeFighter,
        RangedFighter
    }

    public class Character : Entity, ILiving, IDamagable, IHealable
    {

        private ISet<Faction> _factions = new HashSet<Faction>();
        public IEnumerable<Faction> Factions => _factions.AsEnumerable();

        public int MaxHealth => 1000;
        public int Health { get; set; }
        public bool IsAlive => Health > 0;
        public bool IsDead => !IsAlive;

        public const int MIN_LEVEL_DIFFERENCE_FOR_DAMAGE_MODIFICATION = 5;

        public int Level { get; set; }
        public const int STARTING_LEVEL = 1;

        public const float MALUS_DAMAGE_RATIO = 0.5f;

        public const float BONUS_DAMAGE_RATIO = 1.5f;

        public FighterType Type { get; set; }

        public int AttackRange { get => fighterTypeToRange[Type]; }

        private Dictionary<FighterType, int> fighterTypeToRange =
            new Dictionary<FighterType, int>
            {
                {FighterType.MeleeFighter, 2 },
                {FighterType.RangedFighter, 20 }
            };

        public Character(FighterType type = FighterType.MeleeFighter) : base()
        {
            Type = type;
            Level = STARTING_LEVEL;
            Health = MaxHealth;
        }

        public bool HealSelf(int healAmount)
        {
            return Heal(this, healAmount);
        }

        public bool Heal(Character target, int healAmount)
        {           
            if (IsNotAlliedWith(target))
                return false;

            if (target.IsDead)
                return false;

            target.ReceiveHeal(healAmount);

            return true;
        }

        public void ReceiveHeal(int healAmount)
        {          
            Health = Math.Min(MaxHealth, Health + healAmount);
        }

        public bool Attack(Character target, int damageAmount)
        {
            if (this == target)
                return false;

            if (IsNotInRangeToAttack(target))
                return false;

            if (IsAlliedWith(target))
                return false;

            var damageRatio = DamageRatioAgainst(target);
            var adjustedDamageAmount = (int) (damageAmount * damageRatio);

            target.SufferDamage(adjustedDamageAmount);

            return true;
        }

        public bool Attack(Props target, int damageAmount)
        {
            if (IsNotInRangeToAttack(target))
                return false;

            target.SufferDamage(damageAmount);

            return true;
        }

        public void SufferDamage(int damageAmount)
        {
            Health = Math.Max(0, Health - damageAmount);
        }

        private bool IsInRangeToAttack(Entity other)
        {
            return this.Position.DistanceTo(other.Position) <= AttackRange;
        }

        private bool IsNotInRangeToAttack(Entity other)
        {
            return !IsInRangeToAttack(other);
        }

        private float DamageRatioAgainst(Character target)
        {
            var dmgRatio = 1f;

            if (IsEligibleForBonusDmgAgainst(target))
                dmgRatio *= BONUS_DAMAGE_RATIO;

            else if (IsEligibleForMalusDmgAgainst(target))
                dmgRatio *= MALUS_DAMAGE_RATIO;

            return dmgRatio;
        }

        private bool IsEligibleForBonusDmgAgainst(Character other)
        {
            return this.Level - other.Level >= MIN_LEVEL_DIFFERENCE_FOR_DAMAGE_MODIFICATION;
        }

        private bool IsEligibleForMalusDmgAgainst(Character other)
        {
            return other.Level - this.Level >= MIN_LEVEL_DIFFERENCE_FOR_DAMAGE_MODIFICATION;
        }

        public void JoinFaction(Faction faction)
        {
            _factions.Add(faction);
        }

        public void LeaveFaction(Faction faction)
        {
            _factions.Remove(faction);
        }

        public bool IsAlliedWith(Character other)
        {
            return this == other || _factions.Intersect(other._factions).Any();
        }

        public bool IsNotAlliedWith(Character other)
        {
            return !IsAlliedWith(other);
        }

        
    }
}
