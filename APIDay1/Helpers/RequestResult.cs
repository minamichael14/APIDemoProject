namespace APIDay1.Helpers
{
    public record RequestResult<T> (T Data, bool IsSuccess, string Message)
    {
        public static RequestResult<T> Success(T data)
        {
            return new RequestResult<T>(data, true,"");
        }

        public static RequestResult<T> Failure(string message)
        {
            return new RequestResult<T>(default, false, message);
        }
    }
}
