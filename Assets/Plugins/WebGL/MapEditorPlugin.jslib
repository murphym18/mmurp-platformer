var MapEditorPlugin = {
    ReadMapDataFromDom: function()
    {
        var mapStr = window.document.getElementById('map-editor').value;
        var buffer = _malloc(mapStr.length + 1);
        writeStringToMemory(mapStr, buffer);
        return buffer;
    }
};

mergeInto(LibraryManager.library, MapEditorPlugin);
