namespace MilbridgeHoldings.Models.Local
{
    public class ActionResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ActionResult() { }

        public ActionResult(string message)
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
