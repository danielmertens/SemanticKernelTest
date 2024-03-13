using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelTest.Web.Components;
using SemanticKernelTest.Web.Options;
using SemanticKernelTest.Web.Plugins;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOptions<AzureOpenAI>()
        .Bind(builder.Configuration.GetSection(nameof(AzureOpenAI)))
        .ValidateDataAnnotations()
        .ValidateOnStart();

builder.Services.AddSingleton<IChatCompletionService>(sp =>
{
    AzureOpenAI options = sp.GetRequiredService<IOptions<AzureOpenAI>>().Value;
    return new AzureOpenAIChatCompletionService(options.ChatDeploymentName, options.Endpoint, options.ApiKey);
});

// Plugins
builder.Services.AddSingleton<TimePlugin>();
builder.Services.AddSingleton<AbsencePlugin>();

builder.Services.AddTransient((sp) =>
{
    // Create a collection of plugins that the kernel will use
    KernelPluginCollection pluginCollection = new();
    pluginCollection.AddFromObject(sp.GetRequiredService<TimePlugin>());
    pluginCollection.AddFromObject(sp.GetRequiredService<AbsencePlugin>());
    
    return new Kernel(sp, pluginCollection);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
