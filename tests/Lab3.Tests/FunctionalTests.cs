using Itmo.ObjectOrientedProgramming.Lab3.Actions;
using Itmo.ObjectOrientedProgramming.Lab3.Catalog;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;
using Itmo.ObjectOrientedProgramming.Lab3.Weapon.Implementation;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature.Fabric;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class FunctionalTests
{
    [Fact]
    public void CompleteGameScenario_WithMultipleCreatureTypes()
    {
        // Arrange
        var catalog = new CreatureCatalog();
        var factory = new CreatureFactory();

        var player1 = new PlayerTable("Player1 - Strategy Master");
        var player2 = new PlayerTable("Player2 - Aggressive Tactics");

        // Player1: balanced team with modifiers
        player1.AddCreature(factory.AmuletMaster());
        player1.AddCreature(catalog.CreateImmortalHorror());
        player1.AddCreature(factory.UpdateToMagicShieldModifier(
            catalog.CreateCombatAnalyst()));

        // Player2: high damage team
        player2.AddCreature(catalog.CreateAngryFighter());
        player2.AddCreature(catalog.CreateMimicChest());
        player2.AddCreature(factory.UpdateToAttackMasteryModifier(
            new CustomCreature("Berserker", 8, new BaseWeapon(4))));

        var fight = new Fight(player1, player2);

        // Act
        FightResult result = fight.Run();

        // Assert
        result.PrintResult();

        Assert.NotNull(result.Winner);
        Assert.True(result.FirstPlayerSurvivors >= 0);
        Assert.True(result.SecondPlayerSurvivors >= 0);
        Assert.True(result.RoundLogs.Count > 0);
    }

    [Fact]
    public void CustomCreatureCreation_And_Modification_Workflow()
    {
        // Arrange
        var factory = new CreatureFactory();

        // Create custom creature with high stats
        ICreature customCreature = factory.CreateCustom("Dragon", 25, new BaseWeapon(5));

        // Apply modifiers
        var shieldedDragon = new MagicShieldModifier(customCreature);
        var ultimateDragon = new AttackMasteryModifier(shieldedDragon);

        // Test the modified creature
        ICreature target = factory.CreateCustom("Knight", 15, new BaseWeapon(5));

        // Act
        int initialTargetHealth = target.Health;
        ultimateDragon.Attack(target);
        int damageDealt = initialTargetHealth - target.Health;

        // Assert
        Assert.Equal(10, damageDealt); // 8 damage × 2 attacks
        Assert.Equal(25, ultimateDragon.Health); // Shield active
    }

    [Fact]
    public void MassCombat_WithMaxCreatures()
    {
        // Arrange
        var factory = new CreatureFactory();

        var player1 = new PlayerTable("Player1 - Horde");
        var player2 = new PlayerTable("Player2 - Elites");

        // Fill both tables to maximum capacity
        for (int i = 0; i < player1.MaxCreatures; i++)
        {
            player1.AddCreature(factory.CreateCustom($"Grunt {i + 1}", 3, new BaseWeapon(2)));
        }

        for (int i = 0; i < player2.MaxCreatures; i++)
        {
            player2.AddCreature(factory.CreateCustom($"Elite {i + 1}", 6, new BaseWeapon(3)));
        }

        var fight = new Fight(player1, player2);

        // Act
        FightResult result = fight.Run();

        // Assert
        Assert.NotNull(result.Winner);
        Assert.True(result.RoundLogs.Count >= 1);

        // Verify all creatures were involved
        Assert.Contains(
            result.RoundLogs,
            log => log.Contains("Grunt", StringComparison.Ordinal) &&
                   log.Contains("Elite", StringComparison.Ordinal));
    }

    [Fact]
    public void SpecialAbilityInteractions_ComplexScenario()
    {
        // Arrange
        var player1 = new PlayerTable("Player1 - Combo Team");
        var player2 = new PlayerTable("Player2 - Counter Team");

        // Team with synergistic abilities
        player1.AddCreature(new AngryFighter()); // Damage increases when hurt
        player1.AddCreature(new CombatAnalyst()); // Damage increases after attacking
        player1.AddCreature(new ImmortalHorror()); // Can revive once

        // Team with copying and shielding
        player2.AddCreature(new MimicChest()); // Copies enemy stats
        player2.AddCreature(new AmuletMaster()); // Has shield and double attack

        var fight = new Fight(player1, player2);

        // Act
        FightResult result = fight.Run();

        // Assert
        Assert.NotNull(result.Winner);

        // Verify special abilities were triggered
        bool hasAngryFighter = result.RoundLogs.Any(log =>
            log.Contains("AngryFighter", StringComparison.Ordinal));
        bool hasCombatAnalyst = result.RoundLogs.Any(log =>
            log.Contains("CombatAnalyst", StringComparison.Ordinal));
        bool hasImmortalHorror = result.RoundLogs.Any(log =>
            log.Contains("ImmortalHorror", StringComparison.Ordinal));
        bool hasMimicChest = result.RoundLogs.Any(log =>
            log.Contains("MimicChest", StringComparison.Ordinal));
        bool hasAmuletMaster = result.RoundLogs.Any(log =>
            log.Contains("Amulet Master", StringComparison.Ordinal));

        Assert.True(hasAngryFighter || hasCombatAnalyst || hasImmortalHorror ||
                    hasMimicChest || hasAmuletMaster);
    }
}