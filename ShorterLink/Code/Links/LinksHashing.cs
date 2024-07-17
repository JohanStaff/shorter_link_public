using System.Security.Cryptography;
using System.Text;

namespace ShorterLink;

public class LinksHashing {
    public static string HashLink() { 
        StringBuilder sb = new(16);
        string hash = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        for(int i = 0; i < 10; i++) {
            switch(hash[i]) {
                case '/':
                    sb.Append('_');
                    continue;
                case '+':
                    sb.Append('-');
                    continue;
                default: 
                    sb.Append(hash[i]);
                    continue;
            }
        }

        return sb.ToString();
    }
}
