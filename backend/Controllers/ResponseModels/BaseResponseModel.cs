namespace backend.Controllers.ResponseModels
{
    public class BaseResponseModel
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
    }
}
