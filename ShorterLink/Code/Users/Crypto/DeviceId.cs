namespace ShorterLink.Code.Users.Crypto;

public class DeviceId {
	private string _id;

	public string Id => _id;

	private DeviceId(string id) {
		if(id.Length > 36) {
			throw new ArgumentException("Id must not exceed 32 characters");
		}

		_id = id;
	}

	public static DeviceId Generate() {
		return new DeviceId(Guid.NewGuid().ToString());
	}

	public static implicit operator DeviceId(string id) {
		return new DeviceId(id);
	}
    public static implicit operator string(DeviceId deviceId) => deviceId._id;

    public override string ToString()
    {
        return _id;
    }
}
