namespace Client.Layout.SecondRowSection;

public static class LogUiConstants
{
	public static class Login
	{
		public const string Title = "Modal"; // Login Text: Login
		public const string Icon = "fas fa-window-restore"; // Login Icon: "fas fa-sign-in-alt";
		public const string ButtonCSS = "btn btn-xs btn-outline-info text-black-50"; //  float-end

	}
	public static class Logout
	{
		public const string Href = "/.auth/logout?post_logout_redirect_uri=/loggedout";
		public const string Title = "Log out";
		public const string Icon = "fas fa-sign-out-alt";
		public const string ButtonCSS = "btn btn-xs btn-outline-info text-black-50";
	}

}

// Ignore Spelling: loggedout uri xs