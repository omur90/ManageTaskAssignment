
namespace ManageTaskAssignment.SharedObjects
{
    [Serializable]
    public class CustomBusinessException : Exception
    {
        public CustomBusinessException(string message) : base($"Business Rule Error : {message}") { }
    }
}
