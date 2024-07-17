using ShorterLink.Code.DataObjects;
using ShorterLink.Code.Users.Crypto;

namespace ShorterLink.Code.Events;

public class EventsService : DataObjectService<EventObject> {
    public EventsService(AppDatabase database) : base(database) { }

    public IEnumerable<EventObject> this[DeviceId deviceId]
	{
		get {
			var command = _database.CreatePlainCommand("SELECT * FROM events WHERE device_id=@deviceId;");
			command.AddWithValue("@deviceId", deviceId);
			
			using(var reader = command.ExecuteReader()) {
				while(reader.Read()) {
					yield return DBToObject(reader);
				}
			}
		}
	}
	public IEnumerable<EventObject> this[ulong userId]
	{
		get {
			var command = _database.CreatePlainCommand("SELECT * FROM events WHERE user_id=@userId;");
			command.AddWithValue("@userId", userId);
			
			using(var reader = command.ExecuteReader()) { 
				while(reader.Read()) {
					yield return DBToObject(reader);
				}
			}
		}
	}
	/// <summary>
	/// Yields events WITH provided parameters. So, it works exactly userId AND deviceId
	/// </summary>
	/// <param name="userId">Id of the user which events to be found</param>
	/// <param name="deviceId">Id of device of the user which events to be found</param>
	/// <returns>An EventObject array</returns>
	public IEnumerable<EventObject> this[ulong userId, DeviceId deviceId] {
		get {
			var command = _database.CreatePlainCommand("SELECT * FROM events WHERE user_id=@userId AND device_id=@deviceId;");
			command.AddValues([
				new("@userId", userId),
				new("@deviceId", deviceId),
			]);
			
			using(var reader = command.ExecuteReader()) {
				while(reader.Read()) {
					yield return DBToObject(reader);
				}
			}
		}
	}
	public IEnumerable<EventObject> this[DeviceId deviceId, int days] {
		get {
			var command = _database.CreatePlainCommand("SELECT * FROM events WHERE device_id=@deviceId AND added_ts > NOW() - INTERVAL @days DAY;");
			command.AddValues([
				new("@deviceId", deviceId),
				new("@days", days)
			]);
			
			using(var reader = command.ExecuteReader()) { 
				while(reader.Read()) {
					yield return DBToObject(reader);
				}
			}
		}
	}

	public IEnumerable<EventObject> GetEventsByUserIdOrDeviceId(ulong userId, DeviceId deviceId, int periodInDays) {
		var command = _database.CreatePlainCommand("SELECT * FROM events WHERE user_id=@userId OR device_id=@deviceId AND added_ts > NOW() - INTERVAL @days DAY;");
		command.AddValues([
			new("@userId", userId),
			new("@deviceId", deviceId),
			new("@days", deviceId),
		]);
		
		using(var reader = command.ExecuteReader()) {
			while(reader.Read()) {
				yield return DBToObject(reader);
			}
		}
	}

	public void Create(EventObject eventObject) {
		var command = _database.CreatePlainCommand("INSERT INTO events (user_id, action_id, message, device_id) VALUES (@user_id, @action_id, @message, @device_id)");
		command.AddValues([
			new("@user_id", eventObject.user_id),
			new("@action_id", eventObject.action_id),
			new("@message", eventObject.message),
			new("@device_id", eventObject.device_id),
		]);

		command.ExecuteNonQuery();
	}
	public void CreateAddLink(ulong userId, string message, DeviceId deviceId) {
		Create(new EventObject {
			action_id = (int)UserActionCode.LinkAddition,
			device_id = deviceId,
			message = message,
			user_id = userId,
		});
	}
	
	public override EventObject DBToObject(DatabaseReader reader) {
		return new EventObject { 
			id = (ulong)reader["id"],
			message = (string)reader["message"],
			user_id = (ulong)reader["user_id"],
			device_id = (string)reader["device_id"],
			action_id = (int)reader["action_id"],
			added_ts = (DateTime)reader["added_ts"],
		};
    }
}
