using Ardalis.SmartEnum;

namespace Client.Enums;

[Flags]
public enum PageListType
{
	None = 0,
	SitemapPage = 1,
	Footer = 2,
	Layout = 4,
	Reply = 8
}

public abstract class Nav : SmartEnum<Nav>
{

	#region Id's
	private static class Id
	{
		internal const int Home = 1;
		internal const int About = 2;
		internal const int Sitemap = 3;
		internal const int Contact = 4;
		internal const int VirtualFileSystem = 5;
		//internal const int SampleCode = 6;
		internal const int Profile = 47;
	}

	#endregion

	#region  Declared Public Instances
	public static readonly Nav Home = new HomeSE();
	public static readonly Nav About = new AboutSE();
	public static readonly Nav Sitemap = new SitemapSE();
	public static readonly Nav Contact = new ContactSE();
	//public static readonly Nav SampleCode = new SampleCodeSE();
	public static readonly Nav VirtualFileSystem = new VirtualFileSystemSE();
	public static readonly Nav Profile = new ProfileSE();

	#endregion

	private Nav(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract int Sort { get; }
	public abstract bool IsPartOfList(PageListType pageListType);
	public abstract PageListType PageListType { get; }
	#endregion

	#region Private Instantiation

	private sealed class HomeSE : Nav
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/";
		public override string Title => "Home";
		public override string Icon => "fas fa-home";
		public override int Sort => Id.Home;
		public override PageListType PageListType => PageListType.Footer;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
	}

	private sealed class AboutSE : Nav
	{
		public AboutSE() : base($"{nameof(Id.About)}", Id.About) { }
		public override string Index => "/About";
		public override string Title => "About";
		public override string Icon => "fas fa-info";
		public override int Sort => Id.About;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
	}

	private sealed class SitemapSE : Nav
	{
		public SitemapSE() : base($"{nameof(Id.Sitemap)}", Id.Sitemap) { }
		public override string Index => "/Sitemap";
		public override string Title => "Sitemap";
		public override string Icon => "fas fa-sitemap";
		public override int Sort => Id.Sitemap;
		public override PageListType PageListType => PageListType.Footer | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
	}

	private sealed class ContactSE : Nav
	{
		public ContactSE() : base($"{nameof(Id.Contact)}", Id.Contact) { }
		public override string Index => "/Contact";
		public override string Title => "Contact";
		public override string Icon => "far fa-comment-dots";
		public override int Sort => Id.Contact;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout | PageListType.Footer;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
	}

	/*
	private sealed class SampleCodeSE : Nav
	{
		public SampleCodeSE() : base($"{nameof(Id.SampleCode)}", Id.SampleCode) { }
		public override string Index => "/SampleCode";
		public override string Title => "Sample Code";
		public override string Icon => "fas fa-vial";
		public override int Sort => Id.SampleCode;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
	}
	*/

	private sealed class VirtualFileSystemSE : Nav
	{
		public VirtualFileSystemSE() : base($"{nameof(Id.VirtualFileSystem)}", Id.VirtualFileSystem) { }
		public override string Index => "/VirtualFileSystem";
		public override string Title => "Virtual File System";
    public override string Icon => "fas fa-file-archive";  //  far fa-folder-open
    public override int Sort => Id.VirtualFileSystem;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
	}

	private sealed class ProfileSE : Nav
	{
		public ProfileSE() : base($"{nameof(Id.Profile)}", Id.Profile) { }
		public override string Index => "/profile";
		public override string Title => "Profile";
		public override string Icon => "fas fa-user";  //fab fa-superpowers
		public override int Sort => Id.Profile;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Footer;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
	}
	#endregion
}

// Ignore Spelling: Nav Permaculture Indepth Mishpocha Parasha Chala Challah Syncfusion QRC Descr tshirt Torah pesach YouTube loggedout Blazored
