using System.Text.Json;

namespace ShorterLink.Models.User;

public class AuthentificationResponse : JsonResponse {
    public string redirect { get; set; } 
}
