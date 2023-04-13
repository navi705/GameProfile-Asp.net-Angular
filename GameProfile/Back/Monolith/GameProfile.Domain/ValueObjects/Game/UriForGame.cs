namespace GameProfile.Domain.ValueObjects.Game
{
    public sealed class UriForGame : ValueObject
    {
        public UriForGame( Uri uri)
        {
            Uri = uri;
        }


        public Uri Uri { get; private init; }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Uri;
        }
    }
}
