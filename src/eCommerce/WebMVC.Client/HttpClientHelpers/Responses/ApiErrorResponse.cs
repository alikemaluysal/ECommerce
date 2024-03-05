namespace WebMVC.Client.HttpClientHelpers.Responses;

public class ApiErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Detail { get; set; }
    public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
