using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using ZstdSharp.Unsafe;

namespace ShorterLink;

public class DatabaseReader : IDisposable {
    private MySqlDataReader _reader;
    private AppDatabase _database;

    internal DatabaseReader(AppDatabase database, MySqlDataReader reader) {
        _reader = reader;
        _database = database;
    }

    public object this[string index]
    {
        get { 
            return _reader[index];
        }
    }

    public void Close() {
        _reader.Close();
        _database.CloseConnection();
    }
    public void Dispose() {
        Close();
    }

    public bool Read() {
        return _reader.Read();
    }
}
public class DatabaseQuery {
    private MySqlCommand _query;
    private AppDatabase _database;

    public DatabaseQuery(AppDatabase connection, string query) {
        _query = new MySqlCommand() {
            CommandText = query,
            CommandType = System.Data.CommandType.Text,
            Connection = connection.Connection
        };
        _database = connection;
    }

    public void AddValues(KeyValuePair<string, object>[] pairs) {
        foreach(var pair in pairs) {
            AddWithValue(pair.Key, pair.Value);
        }
    }
    public void AddWithValue(string key, object value) {
        _query.Parameters.AddWithValue(key, value);
    }
    public DatabaseReader ExecuteReader() {
        return new DatabaseReader(_database, _query.ExecuteReader()); 
    }
    public int ExecuteNonQuery() {
        return _query.ExecuteNonQuery();
    }
    public object ExecuteScalar() {
        return _query.ExecuteScalar();
    }
    public T ExecuteScalar<T>() {
        return (T)_query.ExecuteScalar();
    }
}
public class AppDatabase {
    private List<MySqlConnection> _connection = new();
    public MySqlConnection Connection => _connection[^1];
    private string _connectionString;

    private AppDatabase(string connection) {
        _connectionString = connection;
        PushConnection(_connectionString);
    }

    private void PushConnection(string connection) {
        _connection.Add(new MySqlConnection() {
            ConnectionString = connection
        });
        Connection.Open();
    }
    internal void CloseConnection() {
        Connection.Close();
        _connection.RemoveAt(_connection.Count - 1);
    }
    
    public static AppDatabase Create(string connection) {
        AppDatabase output = new (connection);

        return output;
    }
    public DatabaseQuery CreatePlainCommand(string command) {
        try { 
            PushConnection(_connectionString);
            return new DatabaseQuery(this, command);
        } catch {
            CloseConnection();
            return null;
        }
    }
}
