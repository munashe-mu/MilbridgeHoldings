namespace MilbridgeHoldings.Models.Local
{
    public class ActionResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ActionResult() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ActionResult(string message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Message = message;
        }

        public ActionResult(bool success, string message) : this(message)
        {
            Success = success;
        }

        public ActionResult(bool success, string message, T data)
            : this(success, message)
        {
            Data = data;
        }

        public static ActionResult<T> FromFailureObject(T obj)
        {
            return new ActionResult<T>(false, "", obj);
        }

        public static ActionResult<T> FromSuccessObject(T obj)
        {
            return new ActionResult<T>(true, "", obj);
        }
    }
}
