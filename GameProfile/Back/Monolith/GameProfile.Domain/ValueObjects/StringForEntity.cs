namespace GameProfile.Domain.ValueObjects
{
    public sealed class StringForEntity : ValueObject
    {
        public StringForEntity(string stringFor)
        {
            StringFor = stringFor;
        }

       public string StringFor { get; private init; }
        
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return StringFor;
        }
    }
}
