namespace GameProfile.Domain.ValueObjects.Game
{
    public sealed class StringForGame : ValueObject
    {
        public StringForGame(string gameString)
        {
            GameString = gameString;
        }

       public string GameString { get; private init; }
        
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return GameString;
        }
    }
}
