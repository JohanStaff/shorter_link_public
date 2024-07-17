using CryptSharp.Core;

namespace ShorterLink;

public static class PasswordService {
    public static string HashPassword(string password) => PhpassCrypter.Phpass.Crypt(password);
    public static bool CheckPassword(string password, string crypted) => PhpassCrypter.CheckPassword(password, crypted);
}
