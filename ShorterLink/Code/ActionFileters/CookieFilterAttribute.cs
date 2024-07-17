using System.Net.Mime;
using Microsoft.AspNetCore.Mvc.Filters;
using ShorterLink.Code.Devices;
using ShorterLink.Code.Subscriptions.Logic;
using ShorterLink.Code.Users.Crypto;

namespace ShorterLink.Code.ActionFileters;

public class CookieFilterAttribute : ActionFilterAttribute {
	private DeviceService _deviceService;
	private UserSession _session;

	public CookieFilterAttribute(DeviceService deviceService, UserSession session) {
		_deviceService = deviceService;
		_session = session;
	}

    public override void OnActionExecuting(ActionExecutingContext context) {
		var currentContext = context.HttpContext;
		var device_id = _session.DeviceId;
    }
}
