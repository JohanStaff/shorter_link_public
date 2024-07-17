using CryptSharp.Core;
using ShorterLink.Code.Users.Crypto;

namespace ShorterLink;

public class PasswordService : IPasswordService {
    private const int MIN_LENGTH = 6;
    private HashSet<char> _specialSymbols = new();

    public PasswordService() {
        foreach(char symb in "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~") { 
            _specialSymbols.Add(symb); 
        }
    }
    
    public string HashPassword(string password) => PhpassCrypter.Phpass.Crypt(password);
    public bool CheckPassword(string password, string crypted) => PhpassCrypter.CheckPassword(password, crypted);

    PasswordCheckResult IPasswordService.MeetsRequirements(string password) {
        if(string.IsNullOrWhiteSpace(password)) {
            return new PasswordCheckResult(false, "Password must be 6 characters long at least, must not be spaces-only, and must contain either digits or special symbols, or both");
        }

        if(password.Length < MIN_LENGTH) {
            return new PasswordCheckResult(false, "Password must be 6 characters long at least");
        }

        bool containsDigits, containsLetters, containsSpecialSymbols = containsDigits = containsLetters = false;

        foreach(char symbol in password) {
            if(char.IsDigit(symbol)) {
                containsDigits = true;
                continue;
            }
            if(_specialSymbols.Contains(symbol)) {
                containsSpecialSymbols = true;
                continue;
            }
            if(char.IsAsciiLetter(symbol)) {
                containsLetters = true;
                continue;
            }
            return new PasswordCheckResult(false, "Only special symbols, digits, and latin characters are allowed in passwords");
        }

        if((containsDigits && containsLetters) || (containsSpecialSymbols && containsLetters) || (containsDigits && containsSpecialSymbols)) {
            return new PasswordCheckResult(true, "OK");
        }

        return new PasswordCheckResult(false, "Your password must contain either digits or special symbols, or both");
    }
}
