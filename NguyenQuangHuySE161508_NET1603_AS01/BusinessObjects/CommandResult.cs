namespace BusinessObjects
{
    public class CommandResult<T>
    {
        public string Message { get; set; } = string.Empty;
        public CommandType Type { get; set; }
        public T ResponseObject { get; set; }
    }
}
