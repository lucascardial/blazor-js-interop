using BlazorJs.Ui.JsInterop;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorJs.Ui;

public static class DependencyInjection
{
	public static IServiceCollection AddUi(this IServiceCollection services)
	{
		services.AddScoped<WindowEventInterop>();
		return services;
	}
}

