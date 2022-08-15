
namespace ManageTaskAssignment.SharedObjects
{
    public class GenericResponse<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Description { get; set; }
        public T? Data { get; set; }

        public static GenericResponse<T> Sucess(T data, int statusCode)
        {
            return new GenericResponse<T>()
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccess = true
            };
        }

        public static GenericResponse<T> Sucess(int statusCode)
        {
            return new GenericResponse<T>()
            {
                StatusCode = statusCode,
                IsSuccess = true
            };
        }

        public static GenericResponse<T> Failed(string description, int statusCode)
        {
            return new GenericResponse<T>
            {
                StatusCode = statusCode,
                Description = description
            };
        }
    }
}

