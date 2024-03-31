namespace Client.Features.VirtualFileSystem;

public static class Command
{
	public const string CallingStateHasChanged = "Calling StateHasChanged";

	public const string DeleteZippedFile = "Delete Zipped File";
	public const string CreateZipFolder = "Create Zip Folder";
	public const string CreateZipFile = "Create Zip File";
	public const string ZippedFileToLocalStorage = "Store the zipped file to LocalStorage";
	public const string FilesUnzipped = "Files Unzipped";
	public const string GetZippedFilesFromLocalStorage = "Get zipped files from LocalStorage";
	public const string RemoveZippedFilesFromLocalStorage = "Remove zipped files from LocalStorage";

	public const string GetStringFromLocalStorage = "Get string from LocalStorage";
	public const string DownloadTheZipFile = "Download the zip file";
	
	public const string CreateBaseFolder = "Create base folder";
	public const string CreateSubFoldersAndFiles = "Create sub folders and files";
}
// Verbatim 

public static class JavaScriptModules
{
	public const string Path = "./Features/VirtualFileSystem/Index.razor.js";
	public const string SetupBeforeUnload = "setupBeforeUnload";
	public const string SaveAsFile = "saveAsFile";
}


public static class Files
{
	// use `@` to get a Verbatim string
	public const string BasePath = @"/Temp";
	public const string SubFolder = @"/Directory1/File5.txt";
	public static string WorkingFileAndPath()
	{
		return BasePath + "/" + SubFolder;
	}

	public const string ZipFolder = @"/Zip";
	public const string ZipFile = "ZipFiles.zip";
	//
	public static string ZipFileAndFolder()
	{
		return ZipFolder + "/" + ZipFile;
	}
}
