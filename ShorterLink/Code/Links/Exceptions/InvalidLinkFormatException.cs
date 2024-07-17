namespace ShorterLink.Code.Links.Exceptions;

public class InvalidLinkFormatException : Exception {
	public InvalidLinkFormatException(string message = "") : base(message) {}
}
