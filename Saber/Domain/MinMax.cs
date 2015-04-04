namespace Saber.Domain
{
    /// <summary>
    ///     Min and Max holder
    /// </summary>
    /// <typeparam name="T">The source type.</typeparam>
    public class MinMax<T>
    {
        /// <summary>
        ///     The maximum value
        /// </summary>
        public readonly T Max;

        /// <summary>
        ///     The minimum value
        /// </summary>
        public readonly T Min;

        internal MinMax(T min, T max)
        {
            Min = min;
            Max = max;
        }
    }
}