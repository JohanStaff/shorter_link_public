using System.Text.RegularExpressions;

namespace ShorterLink;

// https://
// 

public class Links {
    public static string GetFullLink(string url) {
        string pattern = @"\b(?<http>http(s)?\:\/\/)?((\w+(\:[\w-]+)?\@)?([\w-]+\.))+(\w+)(\:[0-9]+)?(\/(.+))*(\/)?";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(url);

        if(match.Success) {
            string[] names = regex.GetGroupNames();
            Group group = match.Groups["http"];

            if(string.IsNullOrEmpty(group.Value)) {
                return $"https://{url}";
            }
            return url;
        }

        throw new Exception();
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
