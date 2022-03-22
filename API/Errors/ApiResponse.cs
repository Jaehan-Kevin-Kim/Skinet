namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }


        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            // 아래 문법은 c# version 8 부터 사용 가능한 양식이고, switch를 편하게 사용할 수 있는 방식 임.
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
                _ => null // _ 가 Default 임
            };

        }

    }
}