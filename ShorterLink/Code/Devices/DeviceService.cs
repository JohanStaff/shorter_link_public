using ShorterLink.Code.DataObjects;
using ShorterLink.Code.Users.Crypto;

namespace ShorterLink.Code.Devices;

public class DeviceService : DataObjectService<DeviceObject>
{
    public DeviceService(AppDatabase database) : base(database) { }

    public IEnumerator<DeviceObject> this[string id] {
		get {
			var command = _database.CreatePlainCommand("SELECT * FROM devices WHERE device_id=@id;");
			command.AddWithValue("@id", id);

			using(var reader = command.ExecuteReader()) { 
				while(reader.Read()) {
					yield return DBToObject(reader);
				}
			}
		} 
	}
	public IEnumerator<DeviceObject> this[ulong userId] {
		get {
			var command = _database.CreatePlainCommand("SELECT * FROM devices WHERE user_id=@userId;");
			command.AddWithValue("@userId", userId);
			using(var reader = command.ExecuteReader()) { 
				while(reader.Read()) {
					yield return DBToObject(reader);
				}
			}
		} 
	}

	public void Create(DeviceId id, ulong userId = 0) {
		{
			var countCommand = _database.CreatePlainCommand("SELECT COUNT(*) FROM devices WHERE device_id=@deviceId AND user_id=@userId;");
			countCommand.AddValues([
				new("@deviceId", id),
				new("@userId", userId)
			]);
			long count = (long?)countCommand.ExecuteScalar() ?? 0;
			if(count > 0) {
				return;
			}
		}

		var command = _database.CreatePlainCommand("INSERT INTO devices (device_id, user_id) VALUES(@id, @userId);");
		command.AddValues([
			new("@id", id),
			new("@userId", userId)
		]);	
		command.ExecuteNonQuery();
	}
    public override DeviceObject DBToObject(DatabaseReader reader) {
		return new DeviceObject {
			device_id = (string)reader["device_id"],
			user_id = (ulong)reader["user_id"],
			added_ts = (DateTime)reader["added_ts"],
		};
    }
}
