﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGCombat
{

    public enum FighterType
    {
        MeleeFighter,
        RangedFighter
    }

    public class Character
    {

        private ISet<Faction> _factions = new HashSet<Faction>();

        public IEnumerable<Faction> Factions => _factions.AsEnumerable();

        public const int MAX_HEALTH = 1000;

        public const int MIN_LEVEL_DIFFERENCE_FOR_DAMAGE_MODIFICATION = 5;

        public const int STARTING_LEVEL = 1;

        public const float DMG_RATIO_WHEN_LEVEL_DIFF= 0.5f;
        public int Health { get; set; }
        public int Level { get; set; }
        public bool IsAlive { get => Health > 0; }
        public bool IsDead { get => !IsAlive; }
        public FighterType Type { get; set; }
        public int AttackRange { get => fighterTypeToRange[Type]; }

        private Dictionary<FighterType, int> fighterTypeToRange =
            new Dictionary<FighterType, int>
            {
                {FighterType.MeleeFighter, 2 },
                {FighterType.RangedFighter, 20 }
            };

        public Point2D Position;

        public Character(FighterType type = FighterType.MeleeFighter)
        {
            Type = type;
            Health = MAX_HEALTH;
            Level = STARTING_LEVEL;
            Position = new Point2D(0, 0);
        }

        public bool Attack(Character target, int damageAmount)
        {
            if (this == target)
                return false;

            if (IsInRangeToAttack(target) == false)
                return false;

            if (IsAlliedWith(target))
                return false;

            if (target.IsFiveOrMoreLevelHigherThan(this))
            {
                var multiplier = DMG_RATIO_WHEN_LEVEL_DIFF;
                damageAmount = (int) (damageAmount * multiplier);
            }
            else if (this.IsFiveOrMoreLevelHigherThan(target))
            {
                var multiplier = 1 + DMG_RATIO_WHEN_LEVEL_DIFF;
                damageAmount = (int)(damageAmount *multiplier);
            }

            target.applyDamage(damageAmount);

            return true;
        }


        private bool IsInRangeToAttack(Character other)
        {
            return this.Position.DistanceTo(other.Position) <= AttackRange;
        }

        private void applyDamage(int damageAmount)
        {
            Health = Math.Max(0, Health - damageAmount);
        }

        public bool Heal(Character target, int healAmount)
        {
            if (target.IsDead)
                return false;

            if (IsNotAlliedWith(target))
                return false;

            target.Health = Math.Min(MAX_HEALTH, target.Health + healAmount);

            return true;

        }

        public bool HealSelf(int healAmount)
        {
            return Heal(this, healAmount);
        }

        private bool IsFiveOrMoreLevelHigherThan(Character other)
        {
            return this.Level - other.Level >= MIN_LEVEL_DIFFERENCE_FOR_DAMAGE_MODIFICATION;
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
