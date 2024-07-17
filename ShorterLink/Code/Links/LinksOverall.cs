using ShorterLink.Code.DataObjects;
using ShorterLink.Code.DataObjects.Links;
using ShorterLink.Code.Links.LinkStats;

namespace ShorterLink.Code.Links;

public class LinksOverall : DataObjectService<LinkOverallObject> {
	[Inject]
	private LinksService _linksService;
	[Inject]
	private LinkStatsService _linksStatService;

    public LinksOverall(LinksService linksService, LinkStatsService linkStatsService, AppDatabase database) : base(database) {
		_linksService = linksService;
		_linksStatService = linkStatsService;
	}

    public LinkOverallObject? GetSingleLinkById(ulong linkId) {
		var command = _database.CreatePlainCommand("SELECT * FROM links RIGHT JOIN link_stat ON links.id = link_stat.link_id WHERE links.id=@linkId;");
		command.AddWithValue("@linkId", linkId);

		using(var reader = command.ExecuteReader()) { 
			if(reader.Read()) {
				return DBToObject(reader);
			}
		}

		return null;
	}
	public IEnumerable<LinkOverallObject> GetLinksByUser(ulong userId) {
		var command = _database.CreatePlainCommand("SELECT * FROM links LEFT JOIN link_stat ON links.id = link_stat.link_id WHERE links.user_id=@userId;");
		command.AddWithValue("@userId", userId);

		using(var reader = command.ExecuteReader()) { 
			while(reader.Read()) {
				yield return DBToObject(reader);
			}
		}
	}

    public override LinkOverallObject DBToObject(DatabaseReader reader) {
		var linkData = _linksService.DBToObject(reader);
		var linkStat = _linksStatService.DBToObject(reader);

		if(linkStat.link_id is null) {
			_linksStatService.CreateStat(linkData.id);
			linkStat = new LinkStatsObject { 
				visits = 0
			};
		}

		return new LinkOverallObject{ 
			link = linkData, 
			stats =  linkStat
		};
    }
}
