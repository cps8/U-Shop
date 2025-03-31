namespace UShop.Shared.Dto
{
    public class ResultModel<T>
    {
        public int Code { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }

        public ResultModel(int code, string message, T? data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public static ResultModel<T> Success(string message="请求成功")
        {
            return new ResultModel<T>(200, message, default);
        }

        public static ResultModel<T> Success(T? data, string message = "请求成功")
        {
            return new ResultModel<T>(200, message, data);
        }

        public static ResultModel<T> Failed(string message = "请求失败")
        {
            return new ResultModel<T>(0, message, default);
        }

        public static ResultModel<T> Failed(int code, string message = "请求成功")
        {
            return new ResultModel<T>(code, message, default);
        }
    }
}
