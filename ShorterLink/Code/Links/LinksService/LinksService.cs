using ShorterLink.Code.DataObjects;

namespace ShorterLink.Code.Links;

public class LinksService : DataObjectService<LinkObject> {
    public LinksService(AppDatabase database) : base(database) { }

    public LinkObject this[string hash]
	{
		get { 
			var command = _database.CreatePlainCommand("SELECT * FROM links WHERE hash_url = @url;");
			command.AddWithValue("@url", hash);
			
			using(var reader = command.ExecuteReader()) {
				if(reader.Read()) {
					return DBToObject(reader);
				}
				return null;
			}
		}
	}
	
	public IEnumerable<LinkObject> GetLinksByUser(ulong userId) {
		var command = _database.CreatePlainCommand("SELECT * FROM links WHERE user_id=@userId;");
		command.AddWithValue("@userId", userId);

		using(var reader = command.ExecuteReader()) { 
			while(reader.Read()) {
				yield return DBToObject(reader);
			}
		}
	}

	public void Create(string source, string hashLink, ulong userId) {
        var command = _database.CreatePlainCommand("INSERT INTO links (name, original_url, hash_url, mask_url, user_id) VALUES (@name, @original_url, @hash_url, @mask_url, @user_id);");
        command.AddValues([
            new("@name", source),
            new("@original_url", source),
            new("@hash_url", hashLink),
            new("@mask_url", ""),
            new("@user_id", userId),
        ]);

        command.ExecuteNonQuery();
	}
	public void UpdateState(string hashLink, string alias, bool active) {
        var command = _database.CreatePlainCommand("UPDATE links SET mask_url=@alias, active=@active WHERE hash_url=@hashLink;");
        command.AddValues([
            new("@hashLink", hashLink),
            new("@mask_url", alias),
			new("@active", active),
        ]);

        command.ExecuteNonQuery();
	}
	public void SetActive(string hashLink, bool active) {
        var command = _database.CreatePlainCommand("UPDATE links SET active=@active WHERE hash_url=@hashLink;");
        command.AddValues([
            new("@hashLink", hashLink),
            new("@active", active),
        ]);

        command.ExecuteNonQuery();
	}
	public void AssignAlias(string hashLink, string alias) {
        var command = _database.CreatePlainCommand("UPDATE links SET mask_url=@alias WHERE hash_url=@hashLink;");
        command.AddValues([
            new("@hashLink", hashLink),
            new("@mask_url", alias),
        ]);

        command.ExecuteNonQuery();
	}

    public override LinkObject DBToObject(DatabaseReader reader) {
		return new LinkObject {
			id = (ulong)reader["id"],
			name = (string)reader["name"],
			original_url = (string)reader["original_url"],
			user_id = (ulong)reader["user_id"],
			mask_url = (string)reader["mask_url"],
			hash_url = (string)reader["hash_url"],
			active = (bool)reader["active"],
			added_ts = (DateTime)reader["added_ts"],
		};
    }
}
