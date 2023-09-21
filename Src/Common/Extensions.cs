namespace Common
{
    public static class Extensions
    {
        ///<summary>在 C# 中將列表拆分為大小為 n 的子列表</summary>
        ///<param>chunkSize:要拆成n組</param>
        public static IEnumerable<IEnumerable<T>> partition<T>(
            this IEnumerable<T> values, int chunkSize)
        {
            while (values.Any())
            {
                yield return values.Take(chunkSize).ToList();
                values = values.Skip(chunkSize).ToList();
            }
        }
    }
}