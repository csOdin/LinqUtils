namespace csOdin.LinqUtils.OrderBy.Clauses
{
    internal abstract class OrderByClausePart
    {
        public abstract string AscDesc { get; }
        public string PropertyName { get; internal set; }

        public override string ToString() => $"{PropertyName} {AscDesc}";
    }
}