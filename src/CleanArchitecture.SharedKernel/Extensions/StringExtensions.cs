namespace CleanArchitecture.SharedKernel.Extensions;

public static class StringExtensions
{
    public static Uri ToUri(this string value, UriKind uriKind = UriKind.RelativeOrAbsolute)
    {
        return new Uri(value, uriKind);
    }
}
