using System.Text.RegularExpressions;
using ShorterLink.Code.Links.Exceptions;
using ShorterLink.Code.Workers;

namespace ShorterLink;

public class Links {
    public static GroupCollection GetGroups(string url, bool includeLocalhost = false) {
        string pattern = @"\b(?<http>http(s)?\:\/\/)" +
                         @"?(?<domain>(\w+(\:[\w-]+)?\@)?" + 
                         @"(([\w-]+\.))+(\w+)" + (includeLocalhost  ? "|localhost" : "") + ")" + 
                         @"(?<port>\:[0-9]+)?" +
                         @"(?<query>(\/(.+))*(\/)?)?";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(url);

        if(match.Success) {
            string[] names = regex.GetGroupNames();

            return match.Groups;
        }

        throw new InvalidLinkFormatException();
    }
    public static string GetFullLinkAndCheck(string url) {
        var groups = GetGroups(url);

        Group httpGroup = groups["http"];
        Group domainGroup = groups["domain"];

        if(string.IsNullOrEmpty(httpGroup.Value)) {
            return $"https://{url}";
        }
        if(BannedDomainsWorker.IsBanned(domainGroup.Value)) {
            throw new DomainBannedException($"The domain {domainGroup.Value} is banned");
        }
        return url;
    }
    public static string GetFullLink(string url) {
        var groups = GetGroups(url);

        Group httpGroup = groups["http"];
        Group domainGroup = groups["domain"];

        if(string.IsNullOrEmpty(httpGroup.Value)) {
            return $"https://{url}";
        }
        return url;
    }
    public static string GetDomain(string url) {
        var groups = GetGroups(url);

        Group domainGroup = groups["domain"];

        return domainGroup.Value;
    }

    public static bool VerifyAlias(string alias) {
        if(alias is null) {
            return false;
        }
        foreach(var ch in alias) {
            if((ch == '_' || ch == '-') && !char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch)) {
                return false;
            }
        }
        return true;
    }
}
