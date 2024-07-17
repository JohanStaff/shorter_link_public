using System.Text.Json;

namespace ShorterLink;

public class JsonResponse {
	public int response_code { get; set; } 
	public string message { get; set; }
}
