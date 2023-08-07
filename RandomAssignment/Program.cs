/*H3
 * UGE 1
 * KRYPTERING 
 * FØRSTE FORSØG
 */
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class RandomnessExercise
{
    public static void Main()
    {
        // Opgave 2
        List<int> randomListUsingRandom = GenerateRandomList(100, 0, 999);

        Console.WriteLine("Brug af Random");
        foreach (int num in randomListUsingRandom)
        {
            Console.Write(num + " ");
        }

        Console.WriteLine();

        // Opgave 3
        List<int> randomListUsingRNGCrypto = GenerateRandomListRNGCrypto(100, 0, 999);

        Console.WriteLine("Brug af RandomNumberGenerator.Create():");
        foreach (int num in randomListUsingRNGCrypto)
        {
            Console.Write(num + " ");
        }

        // Opgave 4
        int numberOfTests = 10;
        int arraySize = 4;

        RandomnessInvestigationRNGCrypto(numberOfTests, arraySize);
        RandomnessInvestigationRandom(numberOfTests, arraySize);
    }

    // Method to generate a list of random numbers using Random
    private static List<int> GenerateRandomList(int count, int minValue, int maxValue)
    {
        // Instantiate the Random class for random number generation
        Random random = new Random();
        List<int> randomList = new List<int>();

        // Generate 'count' number of random numbers within the specified range and add them to the randomList
        for (int i = 0; i < count; i++)
        {
            int randomNumber = random.Next(minValue, maxValue + 1);
            randomList.Add(randomNumber);
        }

        return randomList;
    }

    // Method to generate a list of random numbers using RandomNumberGenerator.Create()
    private static List<int> GenerateRandomListRNGCrypto(int count, int minValue, int maxValue)
    {
        List<int> randomList = new List<int>();

        // Create an instance of the RandomNumberGenerator class for random number generation
        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            byte[] randomBytes = new byte[4];

            // Generate 'count' number of random bytes and convert them to integers within the specified range, adding them to randomList
            for (int i = 0; i < count; i++)
            {
                randomNumberGenerator.GetBytes(randomBytes);
                int randomNumber = Math.Abs(BitConverter.ToInt32(randomBytes, 0)) % (maxValue - minValue + 1) + minValue;
                randomList.Add(randomNumber);
            }
        }

        return randomList;
    }

    // Method to perform randomness investigation using RNGCryptoServiceProvider
    private static void RandomnessInvestigationRNGCrypto(int numberOfTests, int arraySize)
    {
        Console.WriteLine("Brug af  RNGCryptoServiceProvider:");

        for (int i = 0; i < numberOfTests; i++)
        {
            byte[] randomBytes = new byte[arraySize];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomBytes);
            }

            int randomInt = BitConverter.ToInt32(randomBytes, 0);
            Console.WriteLine($"Using RandomNumberGenerator.Create() {i + 1}: {BitConverter.ToString(randomBytes).Replace("-", "")} => {randomInt}");
        }
    }

    // Method to perform randomness investigation using Random
    private static void RandomnessInvestigationRandom(int numberOfTests, int arraySize)
    {
        Console.WriteLine("Brug af Random");

        Random random = new Random();

        for (int i = 0; i < numberOfTests; i++)
        {
            byte[] randomBytes = new byte[arraySize];
            random.NextBytes(randomBytes);
            int randomInt = BitConverter.ToInt32(randomBytes, 0);

            Console.WriteLine($"Resultat {i + 1}: {BitConverter.ToString(randomBytes).Replace("-", "")} => {randomInt}");
        }
    }
}
