namespace Assets.Scripts.Map
{
    /// <summary>
    /// This object shares a seeded random with other ISharedSeed objects.
    /// SetSeed will either generate a new seed once (and ONLY once) or will use a pre-generated seed
    /// loaded from elsewhere. This is typically used in conjunction with MapRandom and other map instantiation objects.
    /// </summary>
    public interface ISharedSeed
    {
        /// <summary>
        /// Set a static seed to be shared between all other ISharedSeed objects.
        /// Guaranteed to never generate a seed more than once between all shared seed objects.
        /// </summary>
        /// <param name="willGenerateSeed">Will generate a new seed if a seed has not already been set</param>
        void SetSharedSeed(bool willGenerateSeed);
    }
}
