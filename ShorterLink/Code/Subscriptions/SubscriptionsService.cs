using ShorterLink.Code.DataObjects;

namespace ShorterLink.Code.Subscriptions;

public class SubscriptionsService : DataObjectService<SubscriptionPlanObject>
{
    public SubscriptionsService(AppDatabase database) : base(database) { }

    public SubscriptionPlanObject? this[ulong id]
	{
		get { 
			var command = _database.CreatePlainCommand("SELECT * FROM subscription_plans WHERE id=@id;");
			command.AddWithValue("@id", id);

			using(var reader = command.ExecuteReader()) { 
				if(reader.Read()) {
					return DBToObject(reader);
				}
			}

			return null;
		}
	}
	public IEnumerable<SubscriptionPlanObject> GetGroups(int group) {
		var command = _database.CreatePlainCommand("SELECT * FROM subscription_plans WHERE `group`=@group;");
		command.AddWithValue("@group", group);

		using(var reader = command.ExecuteReader()) { 
			while(reader.Read()) {
				yield return DBToObject(reader);
			}
		}
	}

    public override SubscriptionPlanObject DBToObject(DatabaseReader reader) {
		return new SubscriptionPlanObject {
			id = (ulong)reader["id"],
			title = (string)reader["title"],
			description = (string)reader["description"],
			group = (int)reader["group"],
			price = (float)reader["price"],
			period = (uint)reader["period"],
			links_per_month = (uint)reader["links_per_month"],
			alias_edition = (bool)reader["alias_edition"],
		};
    }
}
