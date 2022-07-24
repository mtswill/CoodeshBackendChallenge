using BackendChallenge.Core.ApiModels.Responses;

namespace BackendChallenge.Core.Result
{
    public class Result<T>
    {
        public PaginatedApiResponse<T>? PaginatedApiResponse { get; set; }
        public ApiErrorResponse? ApiError { get; set; }
        public T? SimpleResponse { get; set; }
        public bool IsSuccess { get; set; }
        public bool FromCache { get; set; }

        public Result<T> Success(PaginatedApiResponse<T>? content)
        {
            return new Result<T>
            {
                PaginatedApiResponse = content,
                IsSuccess = true
            };
        }

        public Result<T> Success(T content)
        {
            return new Result<T>
            {
                SimpleResponse = content,
                IsSuccess = true
            };
        }

        public Result<T> Success()
        {
            return new Result<T>
            {
                IsSuccess = true
            };
        }

        public Result<T> Error(string errorMessage)
        {
            return new Result<T>
            {
                IsSuccess = false,
                ApiError = new ApiErrorResponse(errorMessage),
            };
        }
    }
}
