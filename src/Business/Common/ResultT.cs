namespace Business.Common
{
    public class Result<T> : Result
    {
        private readonly T? value;

        public T? Value =>
            IsSuccess ? value : throw new InvalidOperationException();

        protected Result(
            T? value, 
            bool isSuccess,
            Error error)
            : base(isSuccess, error)
        {
            this.value = value;
        }

        public static Result<T> Success(T value) => new (value, true, Error.None);
        public static new Result<T> Failure(Error error) => new (default, false, error);
    }
}
