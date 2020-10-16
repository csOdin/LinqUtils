namespace csOdin.LinqUtils.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtensions
    {
        public static bool IsNullOrempty<T>(this List<T> list) => list == null || !list.Any();
    }
}