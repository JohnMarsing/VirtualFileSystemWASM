namespace Client.Helpers;

public class ButtonCss
{
	public static string Size(Enums.ButtonSize buttonSize)
	{
		Dictionary<Enums.ButtonSize, string> buttonSizeClassMap = new Dictionary<Enums.ButtonSize, string>
				{
						{ Enums.ButtonSize.Xs, " btn-xs" },
						{ Enums.ButtonSize.Sm, " btn-sm" },
						{ Enums.ButtonSize.Lg, " btn-lg" }
				};

		if (buttonSizeClassMap.TryGetValue(buttonSize, out string? s))
		{
			return s;  //$"<button class=\"btn {sizeClass}\">Button</button>";
		}
		else
		{
			return string.Empty;
		}
	}

	public static string IconSize(Enums.ButtonSize buttonSize)
	{
		Dictionary<Enums.ButtonSize, string> iconSizeClassMap = new Dictionary<Enums.ButtonSize, string>
				{
						{ Enums.ButtonSize.Xs, "" },
						{ Enums.ButtonSize.Sm, " fa-fw fa-2x" },
						{ Enums.ButtonSize.Lg, " fa-fw fa-3x" }
				};

		if (iconSizeClassMap.TryGetValue(buttonSize, out string? s))
		{
			return s; 
		}
		else
		{
			return string.Empty;
		}
	}

}


// Ignore Spelling: Entra Css