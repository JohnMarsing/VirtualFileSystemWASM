using Ardalis.SmartEnum;
namespace Client.Layout.Enums;

public abstract class NavSocialMedia : SmartEnum<NavSocialMedia>
{

	#region Id's
	private static class Id
	{
		internal const int YouTube = 1;
		internal const int GitHub = 2;
		internal const int Twitter = 3;
		//<a href="https://www.facebook.com/LivingMessiahMinistries"
	}
	#endregion

	#region  Declared Public Instances
	public static readonly NavSocialMedia YouTube = new YouTubeSE();
	public static readonly NavSocialMedia GitHub = new GitHubSE();
	public static readonly NavSocialMedia Twitter = new TwitterSE();
	#endregion

	private NavSocialMedia(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Href { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	//public abstract string ButtonColler { get; } "btn btn-danger"
	#endregion

	#region Private Instantiation

	private const string _channelId = "AAAAAAAAAAAAAAA";
	private sealed class YouTubeSE : NavSocialMedia
	{
		public YouTubeSE() : base($"{nameof(Id.YouTube)}", Id.YouTube) { }
		public override string Href => $"https://www.youtube.com/channel/{_channelId}";
		public override string Title => "Watch our videos on YouTube";
		public override string Icon => "fab fa-youtube";
	}

	private sealed class GitHubSE : NavSocialMedia
	{
		public GitHubSE() : base($"{nameof(Id.GitHub)}", Id.GitHub) { }
		public override string Href => "https://github.com/AAAAAAAAAAAAAA";
		public override string Title => "AAAAAAAAAA website is NOW OPEN SOURCE!!!";
		public override string Icon => "fab fa-github";
	}

	private sealed class TwitterSE : NavSocialMedia
	{
		public TwitterSE() : base($"{nameof(Id.Twitter)}", Id.Twitter) { }
		public override string Href => "https://twitter.com/AAAAAAAAAAAAA";
		public override string Title => "Follow us on Twitter";
		public override string Icon => "fab fa-twitter";
	}
	#endregion

}
// Ignore Spelling: Nav 
// Ignore Spelling: Href