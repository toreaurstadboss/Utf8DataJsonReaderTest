using System.Text;

namespace SystemTextJsonTestRun
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder RemoveLast(this StringBuilder sb, string value)
        {
            if (sb.Length < 1) return sb;
            int removeIndex = sb.ToString().LastIndexOf(value);
            if (removeIndex < 0)
            {
                return sb;
            }
            sb.Remove(sb.ToString().LastIndexOf(value), value.Length);
            return sb;
        }
    }
}
