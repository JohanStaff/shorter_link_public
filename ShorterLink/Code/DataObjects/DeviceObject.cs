using ShorterLink.Code.Users.Crypto;

namespace ShorterLink.Code.DataObjects;

public class DeviceObject : DataObject {
	public DeviceId device_id { get; set; }
	public ulong user_id { get; set; }
}
