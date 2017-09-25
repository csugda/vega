using System;

namespace Assets.Scripts.Map
{
    /// <summary>
    /// Specific Random class to be used with a shared seed. (ISharedSeed interface)
    /// This is usually only used in conjunction with map generation.
    /// ||Note: DO NOT USE if you care about getting random values!||
    ///       It relies on a backing seed that is set and shared at runtime
    ///       between map objects!
    /// </summary>
    public static class MapRandom
    {
        //TODO: USE LIBRARY FROM LEOPOTAM (Because he is awesome!)
        //https://github.com/Leopotam/LeopotamGroupLibraryUnity/blob/develop/Math/RngFast.cs
        //https://github.com/Leopotam/LeopotamGroupLibraryUnity/blob/develop/Math/RngFast.cs
        //https://github.com/Leopotam/LeopotamGroupLibraryUnity/blob/develop/Math/RngFast.cs
        private static Random rand;
        private static int randSeed = -1;
        public static bool SeedIsGenerated { get; private set; }

        public static int GenerateAndGetNewSeed()
        {
            randSeed = Math.Abs((int)DateTime.Now.Ticks);
            SeedIsGenerated = true;
            InstantiateRand();
            return randSeed;
        }

        public static void SetManualSeed(int seed)
        {
            SeedIsGenerated = false;
            randSeed = seed;
            InstantiateRand();
        }

        public static int GetCurrentSeed()
        {
            return randSeed;
        }

        private static void InstantiateRand()
        {
            rand = new Random(randSeed);
        }

        public static int NextInt(int minValue = Int32.MinValue, int maxValue = Int32.MaxValue)
        {
            rand = new Random(randSeed);
            if (!SeedIsGenerated && randSeed < 0)
            {
                throw new SharedSeedNotSetException();
            }
            else return rand.Next(minValue, maxValue);
        }
    }
}
