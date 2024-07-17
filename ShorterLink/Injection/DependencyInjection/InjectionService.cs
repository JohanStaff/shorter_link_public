using System.Reflection;

namespace ShorterLink;

public static class InjectionService {
    private static Dictionary<Type, object> _services = new();

    public static void Register<T>(object value)  {
        _services[typeof(T)] = value;
    }
    public static void Register<T, U>(U value) where U : T {
        _services[typeof(T)] = value;
    }
    public static T Get<T>() {
        return (T)_services[typeof(T)];
    }
    public static void Resolve(object toResolve) {
        FieldInfo[] fields = toResolve.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach(var field in fields) {
            if(!Attribute.IsDefined(field, typeof(InjectAttribute))) {
                continue;
            }

            field.SetValue(toResolve, _services[field.FieldType]);
        }
    }
}