namespace ShorterLink.Code.DataObjects;

public abstract class DataObjectService<T> where T : DataObject {
	protected AppDatabase _database;

	public DataObjectService(AppDatabase database) {
		_database = database;
	}

	public abstract T DBToObject(DatabaseReader reader);
}

public abstract class DataObjectService<T, U>  {
	protected AppDatabase _database;

	public DataObjectService(AppDatabase database) {
		_database = database;
	}

	public abstract (T, U) DBToObject(DatabaseReader reader);
}