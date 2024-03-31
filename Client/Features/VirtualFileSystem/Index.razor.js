
export const saveAsFile = async (filename, base64String) => {
	// Convert Base64 string to a Blob
	const byteCharacters = atob(base64String);
	const byteNumbers = new Array(byteCharacters.length);
	for (let i = 0; i < byteCharacters.length; i++) {
		byteNumbers[i] = byteCharacters.charCodeAt(i);
	}
	const byteArray = new Uint8Array(byteNumbers);
	const blob = new Blob([byteArray], { type: "application/zip" });

	// Create a link element and download the file
	const link = document.createElement('a');
	link.href = window.URL.createObjectURL(blob);
	link.download = filename;
	link.click();
}


export function setupBeforeUnload(callback) {
	window.addEventListener("beforeunload", function (event) {
		callback.invokeMethodAsync("HandleBeforeUnload");
	});
};


