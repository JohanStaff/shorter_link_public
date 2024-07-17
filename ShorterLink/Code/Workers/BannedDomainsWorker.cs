

namespace ShorterLink.Code.Workers;

public class BannedDomainsWorker : BackgroundService
{
	private const int TEN_MINUTES_IN_MS = 600_000;
	private AppDatabase _database;

	private static List<string> bannedDomains;
	public static string[] BannedDomains => bannedDomains.ToArray();

	public static bool IsBanned(string domain) {
		return bannedDomains.Find(x => x == domain) is not null;
	}

	public BannedDomainsWorker(IConfiguration configuration) {
		bannedDomains = new();
		
		string connstr = configuration.GetConnectionString("DefaultConnection");
		ArgumentException.ThrowIfNullOrEmpty(connstr);

		try {
			var database = AppDatabase.Create(connstr);
			_database = database;
		} catch {
			throw;
		}
	}
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		while(!stoppingToken.IsCancellationRequested) {
			bannedDomains.Clear();

			var command = _database.CreatePlainCommand("SELECT * FROM banned_domains;");
			var reader = command.ExecuteReader();

			while(reader.Read()) {
				bannedDomains.Add((string)reader["domain"]);
			}

			await Task.Delay(TEN_MINUTES_IN_MS);
		}
    }
}
