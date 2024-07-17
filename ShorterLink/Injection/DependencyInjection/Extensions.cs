using ShorterLink;

namespace ShorterLink;

public static class Extensions {
    public static void Resolve(this object obj) {
        InjectionService.Resolve(obj);
    }
}
