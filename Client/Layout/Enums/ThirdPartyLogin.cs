using Ardalis.SmartEnum;
namespace Client.Layout.Enums;

public abstract class ThirdPartyLogin : SmartEnum<ThirdPartyLogin>
{

    #region Id's
    private static class Id
    {
        internal const int Twitter = 1;
        internal const int Facebook = 2;
        internal const int GitHub = 3;
        internal const int Google = 4;
        internal const int Entra = 5;
    }
    #endregion

    #region  Declared Public Instances
    public static readonly ThirdPartyLogin Twitter = new TwitterSE();
    public static readonly ThirdPartyLogin Facebook = new FacebookSE();
    public static readonly ThirdPartyLogin GitHub = new GitHubSE();
    public static readonly ThirdPartyLogin Google = new GoogleSE();
    public static readonly ThirdPartyLogin Entra = new EntraSE();
    #endregion

    private ThirdPartyLogin(string name, int value) : base(name, value)  // Constructor
    {
    }

    #region Extra Fields
    public abstract string Href { get; }  // href="@($"/.auth/login/twitter?post_login_redirect_uri={currentUrl}")" 
    public abstract string Title { get; }
    public abstract string Icon { get; }
    public abstract string ButtonCss { get; }
    #endregion

    #region Private Instantiation

    private sealed class TwitterSE : ThirdPartyLogin
    {
        public TwitterSE() : base($"{nameof(Id.Twitter)}", Id.Twitter) { }
        public override string Href => "/.auth/login/twitter";
        public override string Title => "Twitter";
        public override string Icon => "fab fa-twitter";
        public override string ButtonCss => "btn btn-info";
    }

    private sealed class FacebookSE : ThirdPartyLogin
    {
        public FacebookSE() : base($"{nameof(Id.Facebook)}", Id.Facebook) { }
        public override string Href => "/.auth/login/facebook";
        public override string Title => "Facebook";
        public override string Icon => "fab fa-facebook";
        public override string ButtonCss => "btn btn-primary";
    }

    private sealed class GitHubSE : ThirdPartyLogin
    {
        public GitHubSE() : base($"{nameof(Id.GitHub)}", Id.GitHub) { }
        public override string Href => "/.auth/login/github";
        public override string Title => "GitHub";
        public override string Icon => "fab fa-github";
        public override string ButtonCss => "btn btn-dark";
    }

    private sealed class GoogleSE : ThirdPartyLogin
    {
        public GoogleSE() : base($"{nameof(Id.Google)}", Id.Google) { }
        public override string Href => "/.auth/login/google";
        public override string Title => "Google";
        public override string Icon => "fab fa-google";
        public override string ButtonCss => "btn btn-danger";
    }

    private sealed class EntraSE : ThirdPartyLogin
    {
        public EntraSE() : base($"{nameof(Id.Entra)}", Id.Entra) { }
        public override string Href => "/.auth/login/aad";
        public override string Title => "Entra";
        public override string Icon => "fab fa-windows";  // "fab fa-microsoft"
        public override string ButtonCss => "btn btn-secondary";
    }
    #endregion

}

// Ignore Spelling: Facebook
// Ignore Spelling: Entra Css