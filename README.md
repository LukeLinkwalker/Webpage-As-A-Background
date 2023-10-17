# Webpage-As-A-Background

This project enables the user to display a webpage behind the desktop icons but in front of the desktop background. This effectively enables the user to have a webpage as a background.

Broadly speaking the tool functions as follows:
1. A HTTP server starts serving the webpage content
2. A Winform window containing a Blazor WebView is spawned. This WebView loads a Razor component that contains an IFrame which retrieves the page content from the HTTP server.
3. To move the Winform window behind the desktop icons the parent of the window is changed to move the window further back in the z-order.

To facilitate the live refresh of the content of the IFrame a FileSystemWatcher is used which triggers an event whenever a file is changed in the source directory that is served by the HTTP server. The event emitted by the FileSystemWatcher leads to an event being emitted to the UI which subsequently results in the IFrame being refreshed.

Demonstration:
![til](https://i.imgur.com/p8wSWYN.gif)
