namespace Client.Layout.Navigation;

public static class Anchors
{

	//use Client.Helpers.ButtonCss
	public static class BtnSize
	{
		public const string Default = "";
		public const string Xs = "btn-xs";
		public const string Sm = "btn-sm";
		public const string Lg = "btn-lg";
	}

	public static class BtnColors
	{
		public const string Primary = "btn-primary";
		public const string Secondary = "btn-secondary";
		public const string Success = "btn-success";
		public const string Danger = "btn-danger";
		public const string Warning = "btn-warning";
		public const string Info = "btn-info";
		public const string Light = "btn-light";
		public const string Dark = "btn-dark";
		public const string Link = "btn-link";
		//public const string Tertiary = "btn-tertiary"; 
	}

	public static class BtnOutlineColors
	{
		public const string Primary = "btn-outline-primary";
		public const string Secondary = "btn-outline-secondary";
		public const string Success = "btn-outline-success";
		public const string Danger = "btn-outline-danger";
		public const string Warning = "btn-outline-warning";
		public const string Info = "btn-outline-info";
		public const string Light = "btn-outline-light";
		public const string Dark = "btn-outline-dark";
		public const string Link = "btn-outline-link";
	}

}
/*

## Anchors as buttons
<a class="btn btn-primary" href="#" role="button">

## disabled
<button type="button" class="btn btn-primary" disabled>Primary button</button>
<a class="btn btn-primary disabled" role="button" aria-disabled="true">Primary link</a>  <!-- anchors require disabled in the class -->.

- [tertiary](https://getbootstrap.com/docs/5.3/customize/color/#colors)

*/