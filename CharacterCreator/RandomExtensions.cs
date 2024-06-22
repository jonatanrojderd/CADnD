namespace CharacterCreator;

public static class RandomExtensions
{
    /// <summary>
    /// Rolls a die.
    /// </summary>
    /// <param name="random">The <see cref="Random"/> class.</param>
    /// <param name="sides">The number of sides the die has. Default is 6.</param>
    /// <returns>The result of the die roll.</returns>
    public static int RollDie(this Random random, int sides = 6)
    {
        var values = new int[sides];
        for (int i = 0; i < sides; i++)
        {
            values[i] = i + 1;
        }

        for (int i = 0; i < sides; i++)
        {
            random.Shuffle(values);
        }

        return values[random.Next(values.Length)];
    }

    /// <summary>
    /// Rolls an amount of dice.
    /// </summary>
    /// <param name="random">The <see cref="Random"/> class.</param>
    /// <param name="diceCount">The amount of dice to roll.</param>
    /// <param name="sides">The number of sides each die has. Default is 6.</param>
    /// <returns>An <see cref="IList{T}"/> of the dice result.</returns>
    public static IList<int> RollDice(this Random random, int diceCount = 2, int sides = 6)
    {
        var values = new int[sides];
        for (int i = 0; i < sides; i++)
        {
            values[i] = i + 1;
        }

        var dice = new List<int>();
        for (int i = 0; i < diceCount; i++)
        {
            random.Shuffle(values);
            dice.Add(values[random.Next(values.Length)]);
        }

        return dice;
    }
}