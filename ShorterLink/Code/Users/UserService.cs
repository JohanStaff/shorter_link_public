using Org.BouncyCastle.Crypto.Signers;
using ShorterLink.Code.DataObjects;
using ShorterLink.Code.Users.Crypto;

namespace ShorterLink;

public class UserService : DataObjectService<UserObject> {
    public UserService(AppDatabase database) : base(database) { }

    public UserObject this[ulong id, bool includePassword = false]
    {
        get { 
            if(id == 0) {
                throw new ArgumentException("Id must not be equal 0");
            }
            var command = _database.CreatePlainCommand("SELECT * FROM users WHERE id=@id;");
            command.AddWithValue("@id", id);

            using(var reader = command.ExecuteReader()) { 
                if(reader.Read()) {
                    return FromDBToUser(reader, includePassword);
                }
            }

            throw new UserNotFoundException("User not found");
        }
    }
    public UserObject this[string email,  bool includePassword = false]
    {
        get { 
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            var command = _database.CreatePlainCommand("SELECT * FROM users WHERE email=@email;");
            command.AddWithValue("@email", email);

            using(var reader = command.ExecuteReader()) { 
                if(reader.Read()) {
                    return DBToObject(reader);
                }
            }

            throw new UserNotFoundException("User not found");
        }
    }
    public UserObject this[string email, string username] {
        get { 
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            var command = _database.CreatePlainCommand("SELECT * FROM users WHERE email=@email OR username=@username;");
            command.AddValues([
                new("@email", email),
                new("@username", username)
            ]);

            using(var reader = command.ExecuteReader()) { 
                if(reader.Read()) {
                    return DBToObject(reader);
                }
            }

            throw new UserNotFoundException("User not found");
        }
    }

    public void CreateUser(UserObject userObject) {
        var command = _database.CreatePlainCommand("INSERT INTO users (username, email, password, device_id) VALUES (@username, @email, @password, @device_id);");
        command.AddValues([
            new("@username", userObject.username),
            new("@email", userObject.email),
            new("@password", userObject.password),
            new("@device_id", userObject.device_id)
        ]);
        command.ExecuteNonQuery();
    }

    public void SetPassword(ulong userId, string password) {
        var command = _database.CreatePlainCommand("UPDATE users SET password=@newPassword WHERE id=@userId;");
        command.AddValues([
            new("@userId", userId),
            new("@newPassword", password),
        ]);
        command.ExecuteNonQuery();
    }

    private static UserObject FromDBToUser(DatabaseReader reader, bool password = false) { 
        return new UserObject {
            email = (string)reader["email"],
            id = (ulong)reader["id"],
            password = password ? (string)reader["password"] : "",
            username = (string)reader["username"],
            active = (bool)reader["active"],
			added_ts = (DateTime)reader["added_ts"],
            device_id = (string)reader["device_id"],
        };
    }

    public override UserObject DBToObject(DatabaseReader reader) {
        return FromDBToUser(reader, true);
    }
}
