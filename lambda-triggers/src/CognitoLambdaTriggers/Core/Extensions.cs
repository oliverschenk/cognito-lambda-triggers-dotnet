namespace CognitoLambdaTriggers.Core;

static class Extensions
{
    public static IEnumerable<Type> BaseTypes(this Type type)
    {
        Type t = type;
        while (true)
        {
            t = t.BaseType;
            if (t == null) break;
            yield return t;
        }
    }

    public static bool AnyBaseType(this Type type, Func<Type, bool> predicate) =>
        type.BaseTypes().Any(predicate);

    public static bool IsParticularGeneric(this Type type, Type generic) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == generic;
}