mergeInto(LibraryManager.library, {
	DownloadToFile : function (content, filename, contentType) {
		const a = document.createElement('a');
		const file = new Blob([Pointer_stringify(content)], {type: Pointer_stringify(contentType)});
  
		a.href= URL.createObjectURL(file);
		a.download = Pointer_stringify(filename);
		a.click();

		URL.revokeObjectURL(a.href);
	},	
});