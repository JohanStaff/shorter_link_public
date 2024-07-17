using System.Text.Json;

namespace ShorterLink;

public static class JsonExtensions {
    public static string GetJson(this object obj) {
        return JsonSerializer.Serialize(obj);
    }
}
