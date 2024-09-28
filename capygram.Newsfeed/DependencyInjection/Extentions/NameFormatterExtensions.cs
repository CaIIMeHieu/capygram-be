using MassTransit;
using System.Reflection;

namespace capygram.NewsFeed.DependencyInjection.Extentions
{
    internal static class NameFormatterExtensions
    {
        public static string ToKebabCaseString(this MemberInfo member)
            => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);
    }

    internal class KebabCaseEntityNameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
            => typeof(T).ToKebabCaseString();
    }
}
