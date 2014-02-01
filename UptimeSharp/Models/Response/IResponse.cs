namespace UptimeSharp.Models
{
  internal interface IResponse
  {
    string ErrorCode { get; set; }
    string ErrorMessage { get; set; }
    string RawStatus { get; set; }
    bool Success { get; }
  }
}
