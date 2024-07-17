using Microsoft.AspNetCore.Mvc;
using ShorterLink.Models.User;
using ShorterLink.Code.Devices;
using ShorterLink.Controllers.User;
using Mysqlx.Crud;
using ShorterLink.Code.Users.Crypto;

namespace ShorterLink;

public class UserController : Controller {
    private AppDatabase _database;
    private UserSession _userSession;
    private UserService _userService;
    private DeviceService _devices;
    private IPasswordService _passwordService;

    public UserController(UserSession session,
                          AppDatabase database,
                          UserService userService,
                          DeviceService deviceService,
                          IPasswordService passwordService) { 
        _database = database;
        _userSession = session;
        _devices = deviceService;
        _userService = userService;
        _passwordService = passwordService;
    }

    public IActionResult Account() {
        if(_userSession.IsAnonymous) {
            return Redirect("/User/SignIn");
        }

        return View();
    }
    public IActionResult SignIn() {
        if(!_userSession.IsAnonymous) {
            return Redirect("/");
        }

        return View();
    }
    public IActionResult SignUp() {
        if(!_userSession.IsAnonymous) {
            return Redirect("/");
        }
        return View();
    }
    public IActionResult ResetPassword() {
        if(_userSession.IsAnonymous) {
            return Redirect("/");
        }

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

        if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username)) {
            return new RegistrationResponse {
                message = "Please, fill both email and username fields!",
                response_code = 400,
            }.GetJson();
        }

        var passwordCheck = _passwordService.MeetsRequirements(password);
        
        if(!passwordCheck.HasPassedCheck) {
            return new RegistrationResponse {
                message = passwordCheck.Message,
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
            password = _passwordService.HashPassword(password),
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

            if(_passwordService.CheckPassword(password, user.password)) {
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
            message = "No user found with given credentials"
        }.GetJson();
    }

    [HttpPost]
    public ActionResult<string> UpdatePassword(string current_password, string new_password, string new_password_repeat) {
        if(_userSession.IsAnonymous) {
            return Redirect("/");
        }

        if(new_password != new_password_repeat) {
            return new UpdatePasswordResponse { 
                message = "New passwords don't match each other",
                response_code = 400
            }.GetJson();
        }

        var user = _userService[_userSession.User.id, true];
        var passwordCheck = _passwordService.MeetsRequirements(new_password);

        if(!passwordCheck.HasPassedCheck) {
            return new UpdatePasswordResponse { 
                message = passwordCheck.Message,
                response_code = 400
            }.GetJson();
        }

        if(_passwordService.CheckPassword(current_password, user.password)) {
            _userService.SetPassword(user.id, _passwordService.HashPassword(new_password));
            return new UpdatePasswordResponse {
                message = "OK",
                redirect = "/User/Account/",
                response_code = 200,
            }.GetJson();
        }

        return new UpdatePasswordResponse {
            message = "The current password you entered is incorrect",
            response_code = 403,
        }.GetJson();
    }
}
