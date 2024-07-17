using ShorterLink.Code.Devices;
using ShorterLink.Code.Users.Crypto;

namespace ShorterLink;

/// <summary>
/// To me honest, it's just a singletone
/// </summary>
public class UserSession {
    private ISession _session;
    private HttpContext _httpContext;
    private DeviceService _deviceService;

    public UserObject User => GetCurrent();
    private ulong UserSessionId { 
        get { 
            var id = _session.GetString("id");
            return ulong.Parse(id is null || id == "" ? "0" : id);
        }
    }
    private string UserSessionEmail { 
        get { 
            var email = _session.GetString("email");
            return email is null || email == "" ? "" : string.Empty;
        }
    }
    private string UserSessionUsername { 
        get { 
            var username = _session.GetString("username");
            return username is null || username == "" ? "" : string.Empty;
        }
    }
    public bool IsAnonymous { 
        get { 
            var id = _session.GetString("id");
            return UserSessionId  == 0;
        }
    }
    public DeviceId DeviceId { 
        get { 
            if(!_httpContext.Request.Cookies.ContainsKey("did")) {
                DeviceId newDeviceId = DeviceId.Generate();
                _deviceService.Create(newDeviceId, UserSessionId);
                _httpContext.Response.Cookies.Append("did", newDeviceId);
                return newDeviceId;
            }
            return _httpContext.Request.Cookies["did"]; 
        }
    }

    public UserSession(IHttpContextAccessor httpContext, DeviceService deviceService) {
        _session = httpContext.HttpContext.Session;
        _httpContext = httpContext.HttpContext;
        _deviceService = deviceService;
    }

    public void Start(UserObject user) {
        if(!IsAnonymous) {
            throw new Exception("You cannot start another session while you having one active!");
        }

        _session.SetString("id", user.id.ToString());
        _session.SetString("email", user.email);
        _session.SetString("username", user.username);
    }
    public void Terminate() {
        _session.SetString("id", "");
        _session.SetString("email", "");
        _session.SetString("username", "");
    }

    public UserObject GetCurrent() {
        return new UserObject {
            id = UserSessionId,
            email = UserSessionEmail,
            username = UserSessionUsername,
            password = string.Empty,
            device_id = DeviceId
        };
    }
}
