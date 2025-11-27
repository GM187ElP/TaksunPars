namespace ERP.UI.Blazor.Models;

//public class Result<T> where T : class
//{
//    public bool IsPartialySuccess { get; set; }
//    public List<string> Errors { get; set; } = [];
//    public bool IsSuccess => Errors.Count == 0;
//    public T? Data { get; set; }


//}

public class Result<T> where T : class
{
    public ResultData<T> Data { get; set; } = new();
    public ResultStatus Status { get; set; } = new();
}

public class ResultData<T> where T : class
{
    public T? Data { get; set; }
}

public class ResultStatus 
{
    public bool IsPartialySuccess { get; set; }
    public List<string> Errors { get; set; } = [];
    public bool IsSuccess => Errors.Count == 0;
}