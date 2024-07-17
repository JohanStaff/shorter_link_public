using ShorterLink.Code.Users.Crypto;

namespace ShorterLink.Code.DataObjects;

public class EventObject : DataObject {
	public ulong id { get; set; }
	public ulong user_id { get; set; }
	public int action_id { get; set; }
	public string message { get; set; }
	public DeviceId device_id { get; set; }
}
