namespace ShorterLink.Code.Links.Exceptions;

public class DomainBannedException : Exception {
	public DomainBannedException(string message = "") : base(message) {}
}
