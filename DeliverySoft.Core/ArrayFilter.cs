namespace DeliverySoft.Core
{
    /// <summary>
    /// Фильтр выборки
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ArrayFilter<TEntity>
    {
        public bool Inverted { get; }
        public TEntity[] Values { get; }
        public ArrayFilter(bool inverted, TEntity[] values)
        {
            this.Inverted = inverted;
            this.Values = values;
        }
    }
}