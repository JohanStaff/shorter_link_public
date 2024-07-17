using ShorterLink.Code.Users.Crypto;

namespace ShorterLink;

public class UserObject : DataObject {
    public const ulong ANONYMOUS_ID = 0;

    public ulong id { get; set; }
    public string username { get; set;}
    public string email { get; set; }
    public string password { get; set; }
    public bool active { get; set; }
    public required DeviceId device_id { get; set; }
}
