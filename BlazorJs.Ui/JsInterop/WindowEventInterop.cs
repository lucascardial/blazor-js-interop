using BlazorJs.Ui.Models;
using Microsoft.JSInterop;

namespace BlazorJs.Ui.JsInterop;

public class WindowEventInterop: IAsyncDisposable
{
	public Window window { get; private set; }
	private readonly Lazy<Task<IJSObjectReference>> jSObjectReference;
	
	public delegate void WindowEventHandler(Window sender, EventArgs e);
	public event WindowEventHandler? OnWindowStateChanged;
	
	public WindowEventInterop(IJSRuntime jsRuntime)
	{
		window = new Window(0,0);
		
		jSObjectReference = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
			"import", "./_content/BlazorJs.Ui/js/window-event.interoperability.js").AsTask());
	}
	
	public async Task InitWindowWidthListener()
	{
		var module = await jSObjectReference.Value;
		await module.InvokeVoidAsync("AddWindowListener", DotNetObjectReference.Create(this));
	}
	
	[JSInvokable]
	public void UpdateWindowState(int width, int height)
	{
		window = new Window(width, height);
		OnWindowStateChanged?.Invoke(window, EventArgs.Empty);
	}
	
	public async ValueTask DisposeAsync()
	{
		var module = await jSObjectReference.Value;
		await module.InvokeVoidAsync("RemoveWindowListener");
	}
}
