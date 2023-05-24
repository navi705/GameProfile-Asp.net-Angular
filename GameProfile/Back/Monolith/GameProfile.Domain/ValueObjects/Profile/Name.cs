namespace GameProfile.Domain.ValueObjects.Profile
{
    public sealed class Name : ValueObject
    {
        public const int MaxLength = 64;
        public Name(string value)
        {
            Value = value;
        }
        public string Value { get; private init; }

        public static Name Create(string name)
        {
            if (name.Length > MaxLength)
            {
                throw new Exception("The name exceeds the maximum length");
            }
            return new Name(name);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
