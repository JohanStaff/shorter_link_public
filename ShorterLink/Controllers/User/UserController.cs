using Microsoft.AspNetCore.Mvc;
using ShorterLink.Models.User;
using ShorterLink.Code.Devices;

namespace ShorterLink;

public class UserController : Controller {
    private AppDatabase _database;
    private UserSession _userSession;
    private UserService _userService;
    private DeviceService _devices;

    public UserController(UserSession session, AppDatabase database, UserService userService, DeviceService deviceService) { 
        _database = database;
        _userSession = session;
        _devices = deviceService;
        _userService = userService;
    }

    public IActionResult Account() {
        if(_userSession.IsAnonymous) {
            return Redirect("/User/SignIn");
        }

        return View();
    }
    public IActionResult SignIn() {
        return View();
    }
    public IActionResult SignUp() {
        return View();
    }

    public IActionResult LinksHistory() {
        return View();
    }

    [HttpPost]
    public ActionResult<string> Register(string email, string username, string password) {
        if(!_userSession.IsAnonymous) {
            return Redirect("/");
        }

        if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
            return new RegistrationResponse {
                message = "You given incorrect data",
                response_code = 400,
            }.GetJson();
        }

        try { 
            UserObject user = _userService[email, username];

            return new RegistrationResponse {
                message = "User with given credentials already exists",
                response_code = 403,
            }.GetJson();
        } catch(UserNotFoundException) {
        } catch {
            throw;
        }

        _userService.CreateUser(new UserObject {
            email = email,
            username = username,
            password = PasswordService.HashPassword(password),
            device_id = _userSession.DeviceId
        });

        return Authentificate(email, password);

        return new RegistrationResponse {
            message = "OK",
            redirect = "/",
            response_code = 200
        }.GetJson();
    }
    
    public new ActionResult<string> SignOut() {
        if(_userSession.IsAnonymous) {
            return Redirect("/");
        }

        _userSession.Terminate();        

        return Redirect("/");
    }

    [HttpPost]
    public ActionResult<string> Authentificate(string email, string password) {
        if(!_userSession.IsAnonymous) {
            return Redirect("/");
        }

        try { 
            var user = _userService[email, true];

            if(PasswordService.CheckPassword(password, user.password)) {
                if(!user.active) {
                    return new RegistrationResponse {
                        message = "User has been blocked",
                        response_code = 403
                    }.GetJson();
                }

                _userSession.Start(user);
                _devices.Create(_userSession.DeviceId, user.id);

                return new AuthentificationResponse {
                    message = "OK",
                    redirect = "/",
                    response_code = 200
                }.GetJson();
            }
        } catch(UserNotFoundException) {
            return new AuthentificationResponse {
                response_code = 403,
                redirect = "",
                message = "No user found with given credentials"
            }.GetJson();
        } catch {
            throw;
        }

        return new AuthentificationResponse {
            response_code = 403,
            redirect = "",
            message = "Forbidden"
        }.GetJson();
    }
}
