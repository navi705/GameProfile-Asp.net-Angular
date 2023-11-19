namespace GameProfile.Domain.Shared
{
    public sealed class Result<T>
    {
        public Result(T content, string errorMessage) 
        { 
            Content = content;
            ErrorMessage = errorMessage;
        }

        public T? Content { get; private set; }

        public string? ErrorMessage { get; private set; }

        public Result<T> UpdateContent(T content)
        {
            Content = (T)content;
            return this;
        }

        public void Failture(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

    }
}
