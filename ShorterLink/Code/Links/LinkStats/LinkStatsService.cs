using ShorterLink.Code.DataObjects;

namespace ShorterLink.Code.Links.LinkStats;

public class LinkStatsService : DataObjectService<LinkStatsObject> {
    public LinkStatsService(AppDatabase database) : base(database) { }

    public LinkStatsObject this[ulong linkId]
	{
		get { 
			var command = _database.CreatePlainCommand("SELECT * FROM link_stat WHERE link_id=@id;");
			command.AddWithValue("@id", linkId);

			using(var reader = command.ExecuteReader()) { 
				if(reader.Read()) {
					return DBToObject(reader);
				}
			}

			return null;
		}
	}

	public void CreateStat(ulong link_id, bool bypassCheck = false) {
		if(!bypassCheck) {
			if(this[link_id] is not null) {
				throw new Exception("Link statistics already exist!");
			}
		}

		var command = _database.CreatePlainCommand("INSERT INTO link_stat (link_id, visits) VALUES (@id, 0);");
		command.AddWithValue("@id", link_id);
		command.ExecuteNonQuery();
	}
	public void Increment(ulong linkId) {
		if(this[linkId] is null) {
			CreateStat(linkId, true);
		}

		var command = _database.CreatePlainCommand("UPDATE link_stat SET visits=visits+1 WHERE link_id=@id;");
		command.AddWithValue("@id", linkId);

		command.ExecuteNonQuery();
	}

    public override LinkStatsObject DBToObject(DatabaseReader reader) {
		var linkId = reader["link_id"];
		var visits = reader["visits"];

		return new LinkStatsObject {
			link_id = (ulong?)(linkId is null || linkId == DBNull.Value ? null : linkId),
			visits = (uint?)(visits is null || visits == DBNull.Value ? null : visits),
		};
    }
}
