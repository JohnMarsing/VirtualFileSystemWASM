
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System.IO.Compression;
using System.Text;

namespace Client.Features.VirtualFileSystem;

public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

#nullable disable

	string status = "";
	bool FilesExist = false;
	bool ZipFileExists = false;
	int directoryCount = 0;
	int fileCount = 0;
	string fileContent = "";

	protected override async Task OnInitializedAsync()
	{
		ZipFileExists = await localStorage!.ContainKeyAsync(Files.ZipFile);
		Logger!.LogInformation("{Class}!{Method}; ZipFile: {ZipFile}, ZipFileExists: {ZipFileExists}."
		, nameof(Index), nameof(OnInitializedAsync), Files.ZipFile, ZipFileExists);
	}

	private IJSObjectReference jsModuleRef;
	private DotNetObjectReference<Index> objRef;   // FN 2
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			Logger!.LogInformation("{Class}!{Method}; JSModule: {JSModule}"
				, nameof(Index), nameof(OnAfterRenderAsync), JavaScriptModules.SetupBeforeUnload);

			try
			{
				jsModuleRef = await JS.InvokeAsync<IJSObjectReference>("import", JavaScriptModules.Path);
				objRef = DotNetObjectReference.Create(this);
				await jsModuleRef.InvokeVoidAsync(JavaScriptModules.SetupBeforeUnload, objRef);
			}

			catch (Exception ex)
			{
				Logger!.LogError(ex, "{Class}!{Method}.", nameof(Index), nameof(OnAfterRenderAsync));
				Toast!.ShowError("...Exception thrown, see the Console in your Browsers Developer Tools");
			}

		}
	}

	private IDisposable navRegistrationDisposable; 

	protected override void OnAfterRender(bool firstRender)
	{
		if (firstRender)
		{
			Logger!.LogInformation("{Class}!{Method}"
			, nameof(Index), nameof(OnAfterRender));
			navRegistrationDisposable = Navigation.RegisterLocationChangingHandler(OnLocationChanging);
		}
	}

	private ValueTask OnLocationChanging(LocationChangingContext context)
	{
		string baseUrl = Navigation.BaseUri;

		Logger!.LogInformation("{Class}!{Method}", nameof(Index), nameof(OnLocationChanging));

		if (context.TargetLocation != baseUrl) // that user is going somewhere else
		{
			status = "User is navigating away from the page - Zip any files";
			Logger!.LogInformation("{Class}!{Method}; Status: {status}; TargetLocation: {TargetLocation}"
				, nameof(Index), nameof(OnLocationChanging), status, context.TargetLocation);

			Task.Run(async () => await ZipTheFiles()); // Zip to local storage
		}
		else
		{
			Toast!.ShowInfo($"TargetLocation == {baseUrl}; {nameof(Index)}!{nameof(OnLocationChanging)}");
		}

		return ValueTask.CompletedTask;
	}

	[JSInvokable]
	public void HandleBeforeUnload()
	{
		status = "User is navigating away from the page - Zip any files";
		Logger!.LogInformation("{Class}!{Method}; Status: {status}"
		, nameof(Index), nameof(HandleBeforeUnload), status);
		Task.Run(async () => await ZipTheFiles());
	}

	public void Dispose()
	{
		Logger!.LogInformation("{Class}!{Method}", nameof(Index), nameof(Dispose));
		objRef?.Dispose();
	}

	private async Task CreateFiles()
	{
		status = "Creating directories and files...";
		Logger!.LogInformation("{Class}!{Method}; Status: {status}"
		, nameof(Index), nameof(CreateFiles), status);
		await CreateDirectoriesAndFiles(Files.BasePath, 2, 10);
		status = "Directories and files created.";
		FilesExist = true;
		Logger!.LogInformation("{Class}!{Method}; Status: {status}; FilesExist: {FilesExist}"
			, nameof(Index), nameof(CreateFiles), status, FilesExist);
	}

	private async Task CountFiles()
	{
		directoryCount = 0;
		fileCount = 0;

		try
		{
			status = "Counting directories and files...";
			Logger!.LogInformation("{Class}!{Method}; Status: {status}; Files.BasePath: {BasePath}"
				, nameof(Index), nameof(CountFiles), status, Files.BasePath);
			await CountItems(Files.BasePath);  // Count dir's and files
			status = $"Directories = {directoryCount} and files = {fileCount}.";
			Logger!.LogInformation("{Class}!{Method}; Status: {status}", nameof(Index), nameof(CountFiles), status);
		}
		catch (Exception ex)
		{
			status = ex.Message;
			return;
		}
	}

	private void ClearFiles()
	{
		if (!Directory.Exists(Files.BasePath))
		{
			status = $"BasePath: {Files.BasePath} is not a directory.";
			Logger!.LogInformation("{Class}!{Method}; Status: {status}"
			, nameof(Index), nameof(ClearFiles), status);
			return;
		}

		Directory.Delete(Files.BasePath, true); // recursive:true
		FilesExist = false;
		status = "Directories and files cleared.";
		Logger!.LogInformation("{Class}!{Method}; Status: {status}; FilesExist: {FilesExist}"
		, nameof(Index), nameof(ClearFiles), status, FilesExist);

	}

	private void LoadFile()
	{
		// Load the file into the textarea
		string filePath = Files.WorkingFileAndPath();

		if (File.Exists(filePath))
		{
			fileContent = File.ReadAllText(filePath); // Read the file into a string
			Logger!.LogInformation("{Class}!{Method}; filePath: {filePath}. Command: {Command} "
			, nameof(Index), nameof(LoadFile), filePath, Command.CallingStateHasChanged);
			StateHasChanged();
		}
		else
		{
			fileContent = "File not found.";
			Logger!.LogInformation("{Class}!{Method}; filePath: {filePath}. fileContent: {fileContent} "
			, nameof(Index), nameof(LoadFile), filePath, fileContent);
		}
	}

	private void UpdateFiles()
	{
		string filePath = Files.WorkingFileAndPath();

		if (File.Exists(filePath))
		{
			File.WriteAllText(filePath, fileContent);
			status = "File updated.";
		}
		else
		{
			fileContent = "File not found.";
		}

		Logger!.LogInformation("{Class}!{Method}; Status: {status}; fileContent: {fileContent}"
		, nameof(Index), nameof(UpdateFiles), status, fileContent);

		fileContent = "";
	}

	private async Task ZipTheFiles()
	{
		if (File.Exists(Files.ZipFileAndFolder()))
		{
			Logger!.LogInformation("{Class}!{Method}; Command: {Command}"
			, nameof(Index), nameof(ZipTheFiles), Command.DeleteZippedFile);
			File.Delete(Files.ZipFileAndFolder());
		}

		if (!Directory.Exists(Files.ZipFolder))
		{
			Logger!.LogInformation("{Class}!{Method}; Command: {Command}"
			, nameof(Index), nameof(ZipTheFiles), Command.CreateZipFolder);
			Directory.CreateDirectory(Files.ZipFolder);
		}

		if (!Directory.Exists(Files.BasePath))
		{
			status = "Nothing to Zip up.";
			Logger!.LogInformation("{Class}!{Method}; status: {status}; Files.BasePath: {Files.BasePath}."
			, nameof(Index), nameof(ZipTheFiles), status, Files.BasePath);
			return;
		}

		// Create a zip file from the directory
		ZipFile.CreateFromDirectory(Files.BasePath, Files.ZipFileAndFolder());
		Logger!.LogInformation("{Class}!{Method}; Command: {Command}"
		, nameof(Index), nameof(ZipTheFiles), Command.CreateZipFile);

		status = "Files zipped.";
		Logger!.LogInformation("{Class}!{Method}; status: {status}. Command: {Command} "
		, nameof(Index), nameof(ZipTheFiles), status, Command.CallingStateHasChanged);
		StateHasChanged();

		status = "Read the Zip file into a byte array";
		byte[] exportFileBytes = File.ReadAllBytes(Files.ZipFileAndFolder());
		Logger!.LogInformation("{Class}!{Method}; status: {status}. Command: {Command} "
		, nameof(Index), nameof(ZipTheFiles), status, Command.CallingStateHasChanged);
		StateHasChanged();

		status = "Convert byte array to Base64 string";
		string base64String = Convert.ToBase64String(exportFileBytes);
		Logger!.LogInformation("{Class}!{Method}; status: {status}. Command: {Command} "
		, nameof(Index), nameof(ZipTheFiles), status, Command.CallingStateHasChanged);
		StateHasChanged();

		await localStorage.SetItemAsync(Files.ZipFile, base64String); // Store base64String to local storage
		ZipFileExists = true;
		status = "Zip file stored in the browser's local storage";
		Logger!.LogInformation("{Class}!{Method}; status: {status} Command: {Command}, ZipFileExists: {ZipFileExists}."
		, nameof(Index), nameof(ZipTheFiles), status, Command.ZippedFileToLocalStorage, ZipFileExists);

	}

	private async Task UnzipFile()
	{

		// If the extract directory does not exist, create it
		if (!Directory.Exists(Files.ZipFolder))
		{
			Logger!.LogInformation("{Class}!{Method}; Command: {Command}"
			, nameof(Index), nameof(UnzipFile), Command.CreateZipFolder);
			Directory.CreateDirectory(Files.ZipFolder);
		}

		Logger!.LogInformation("{Class}!{Method}; Command: {Command}"
		, nameof(Index), nameof(UnzipFile), Command.GetZippedFilesFromLocalStorage);

		string exportFileString = await localStorage.GetItemAsync<string>(Files.ZipFile);
		byte[] exportFileBytes = Convert.FromBase64String(exportFileString);
		await File.WriteAllBytesAsync($"{Files.ZipFileAndFolder}", exportFileBytes);
		ZipFile.ExtractToDirectory($"{Files.ZipFileAndFolder}", Files.BasePath);

		status = "Files unzipped.";
		FilesExist = true;
		Logger!.LogInformation("{Class}!{Method}; status: {status}, Command: {Command}, FilesExist: {FilesExist}."
		, nameof(Index), nameof(UnzipFile), status, Command.FilesUnzipped, FilesExist);

	}

	private async Task DownloadZipFile()
	{
		Logger!.LogInformation("{Class}!{Method}; Command: {Command}, ZipFile: {Files.ZipFile}."
	, nameof(Index), nameof(DownloadZipFile), Command.GetStringFromLocalStorage, Files.ZipFile);

		string base64String = await localStorage.GetItemAsync<string>(Files.ZipFile);

		Logger!.LogInformation("{Class}!{Method}; JSModule: {JSModule}; Command: {Command}."
			, nameof(Index), nameof(DownloadZipFile), JavaScriptModules.SaveAsFile, Command.DownloadTheZipFile);
		//await JS        .InvokeVoidAsync(JavaScriptModules.SaveAsFile, Files.ZipFile, base64String);
		await jsModuleRef.InvokeVoidAsync(JavaScriptModules.SaveAsFile, Files.ZipFile, base64String);
	}

	private void DeleteZipFile()
	{
		ZipFileExists = false;
		Logger!.LogInformation("{Class}!{Method}; Command: {Command}, ZipFileExists: {ZipFileExists}."
		, nameof(Index), nameof(DeleteZipFile), Command.RemoveZippedFilesFromLocalStorage, ZipFileExists);
		localStorage.RemoveItemAsync(Files.ZipFile);
	}

	private async Task CreateDirectoriesAndFiles(string basePath, int dirCount, int filesPerDir)
	{
		if (!Directory.Exists(basePath))
		{
			Logger!.LogInformation("{Class}!{Method}; Command: {Command}, basePath: {basePath}."
			, nameof(Index), nameof(CreateDirectoriesAndFiles), Command.CreateBaseFolder, basePath);
			Directory.CreateDirectory(basePath);
		}

		// Create a string of 10,000 characters
		StringBuilder sb = new StringBuilder();
		for (int i = 1; i <= 10000; i++)
		{
			sb.Append("data" + i + " ");
		}
		string data = sb.ToString();

		Logger!.LogInformation("{Class}!{Method}; Command: {Command}."
		, nameof(Index), nameof(CreateDirectoriesAndFiles), Command.CreateSubFoldersAndFiles);

		for (int dirNum = 1; dirNum <= dirCount; dirNum++)
		{
			string dirPath = Path.Combine(basePath, "Directory" + dirNum.ToString());
			Directory.CreateDirectory(dirPath);

			for (int fileNum = 1; fileNum <= filesPerDir; fileNum++)
			{
				string filePath = Path.Combine(dirPath, "File" + fileNum.ToString() + ".txt");
				await Task.Run(() => File.WriteAllText(filePath, data));
			}
		}
	}

	private async Task CountItems(string path)
	{
		if (!Directory.Exists(path))
		{
			status = $"path: {path} is not a directory.";
			Logger!.LogInformation("{Class}!{Method}; Status: {status}"
			, nameof(Index), nameof(CountItems), status);
			return;
		}

		directoryCount++;  // Count the current directory
		fileCount += Directory.GetFiles(path).Length; // Get all files in the current directory and increment the file count
		string[] subDirectories = Directory.GetDirectories(path); // Get all subdirectories in the current directory

		// Recursively count the items in all subdirectories
		foreach (string dir in subDirectories)
		{
			await Task.Run(() => CountItems(dir));
		}

		status = $"Directories = {directoryCount} and files = {fileCount}.";
		Logger!.LogInformation("{Class}!{Method}; status: {status}. Command: {Command} "
		, nameof(Index), nameof(CountItems), status, Command.CallingStateHasChanged);

		StateHasChanged();
	}
}

/*
  ## Footnotes
	
	### FN 1: ToDo, this doesn't seem to work
	```
	private readonly ISyncLocalStorageService? localStorage; 
	ZipFileExists = await localStorage!.ContainKey("ZipFiles.zip")
	private ILocalStorageService localStorage;  @inject in Index.razor file 
	```


	### FN 2 ToDo, explain what this does
	```csharp
	private DotNetObjectReference<Index> objRef;
	```
 */



// Ignore Spelling: textarea
// Ignore Spelling: csharp