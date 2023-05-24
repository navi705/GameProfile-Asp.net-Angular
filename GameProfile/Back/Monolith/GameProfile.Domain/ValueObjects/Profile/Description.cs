namespace GameProfile.Domain.ValueObjects.Profile
{
    public sealed class Description : ValueObject
    {
        public const int MaxLength = 512;
        public Description(string value)
        {
            Value = value;
        }
        public string Value { get; private init; }

        public static Description Create(string description)
        {
            if(description.Length > MaxLength)
            {
                throw new Exception("The description exceeds the maximum length");
            }
            return new Description(description);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
