using System;
using Xunit;
using FluentAssertions;

namespace RPGCombat.Test
{
    //  ITERATION 1

    //All Characters, when created, have:
    //Health, starting at 1000
    //Level, starting at 1
    //May be Alive or Dead, starting Alive(Alive may be a true/false)

    //Characters can Deal Damage to Characters.
    //Damage is subtracted from Health
    //When damage received exceeds current Health, Health becomes 0 and the character dies


    //A Character can Heal a Character.
    //Dead characters cannot be healed
    //Healing cannot raise health above 1000



    // ITERATION 2

    //A Character cannot Deal Damage to itself.
    //A Character can only Heal itself.

    //When dealing damage:
    //If the target is 5 or more Levels above the attacker, Damage is reduced by 50%
    //If the target is 5 or more levels below the attacker, Damage is increased by 50%

    
        
    // ITERATION 3

    //Characters have an attack Max Range.
    //Melee fighters have a range of 2 meters.
    //Ranged fighters have a range of 20 meters.
    //Characters must be in range to deal damage to a target.



    public class CharacterTest
    {
        [Fact]
        public void a_character_starts_with_1000hp()
        {
            var character = new Character();
            character.Health.Should().Be(1000);
        }

        [Fact]
        public void a_character_starts_at_level_1()
        {
            var character = new Character();
            character.Level.Should().Be(1);
        }

        [Fact]
        public void a_character_starts_alive()
        {
            var character = new Character();
            character.IsAlive.Should().Be(true);
        }

        [Fact]
        public void damaging_a_character_substracts_its_health()
        {
            var attacker = new Character();
            var receiver = new Character();
            attacker.Attack(receiver, 300);
            receiver.Health.Should().Be(700);
        }

        [Fact]
        public void damage_is_decreased_by_50_percent_if_receiver_is_5_or_more_level_above()
        {
            var attackerLevel1 = new Character();
            var receiverLevel6 = new Character();
            receiverLevel6.Level = 6;
            attackerLevel1.Attack(receiverLevel6, 300);
            receiverLevel6.Health.Should().Be(850);
        }

        [Fact]
        public void damage_is_increased_by_50_percent_if_attacker_is_5_or_more_level_above()
        {
            var attackerLevel6 = new Character();
            var receiverLevel1 = new Character();
            attackerLevel6.Level = 6;
            attackerLevel6.Attack(receiverLevel1, 100);
            receiverLevel1.Health.Should().Be(850);
        }

        [Fact]
        public void damaging_a_character_substracts_its_health_no_lower_than_zero()
        {
            var attacker = new Character();
            var receiver = new Character();
            attacker.Attack(receiver, 1100);
            receiver.Health.Should().Be(0);
        }

        [Fact]
        public void a_character_cannot_deal_damage_to_itself()
        {
            var clumsyGuy = new Character();
            var successful = clumsyGuy.Attack(clumsyGuy, 200);
            successful.Should().BeFalse();
            
        }

        [Fact]
        public void reaching_0_hp_implies_death()
        {
            var attacker = new Character();
            var receiver = new Character();
            attacker.Attack(receiver, 1100);
            receiver.IsAlive.Should().Be(false);
        }

        [Fact]
        public void healing_restores_health()
        {
            var character = new Character();
            character.Health = 500;
            character.HealSelf(100);
            character.Health.Should().Be(600);
        }


        [Fact]
        public void healing_a_character_restores_no_more_than_1000hp()
        {
            var dealer = new Character();
            var receiver = new Character();
            dealer.Attack(receiver, 200);
            receiver.HealSelf(300);
            receiver.Health.Should().Be(1000);
        }

        [Fact]
        public void dead_characters_cannot_be_healed()
        {
            var dealer = new Character();
            var receiver = new Character();
            dealer.Attack(receiver, 1200);
            receiver.HealSelf(300);
            receiver.Health.Should().Be(0);
        }


        [Fact]
        public void melee_characters_have_attack_range_of_2_meters()
        {
            var meleeFighter = new Character(FighterType.MeleeFighter);
            meleeFighter.AttackRange.Should().Be(2);
        }

        [Fact]
        public void ranged_characters_have_attack_range_of_20_meters()
        {
            var rangedFighter = new Character(FighterType.RangedFighter);
            rangedFighter.AttackRange.Should().Be(20);
        }

        [Fact]
        public void can_attack_if_in_range()
        {
            var rangedFighter = new Character(FighterType.RangedFighter);
            var target = new Character();
            target.Position = new Point2D(10, 10);
            var successful = rangedFighter.Attack(target, 100);
            successful.Should().BeTrue();
        }

        [Fact]
        public void cannot_attack_if_not_in_range()
        {
            var rangedFighter = new Character(FighterType.RangedFighter);
            var target = new Character();
            target.Position = new Point2D(19, 19);
            var successful = rangedFighter.Attack(target, 100);
            successful.Should().BeFalse();
        }

    }
}
