namespace ShorterLink.Code.Users.Crypto;

public class PasswordCheckResult { 
	public bool HasPassedCheck { get; set; }
	public string Message { get; set; }

	public PasswordCheckResult(bool result, string message) {
		HasPassedCheck = result;
		Message = message;
	}
}
public delegate PasswordCheckResult PasswordCriterea(string password);
public interface IPasswordService {
	string HashPassword(string password);
	bool CheckPassword(string encrypted, string password);
	PasswordCheckResult MeetsRequirements(string password);
}
