using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelTest.Console.Plugins;

var builder = Kernel.CreateBuilder();

builder.AddAzureOpenAIChatCompletion(
         "",                      // Azure OpenAI Deployment Name
         "", // Azure OpenAI Endpoint
         "");      // Azure OpenAI Key

builder.Plugins.AddFromType<MyTimePlugin>();

var kernel = builder.Build();

var completionService = kernel.GetRequiredService<IChatCompletionService>();

OpenAIPromptExecutionSettings settings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

Console.Write("> ");

string? input = null;
while ((input = Console.ReadLine()) != null)
{
    Console.WriteLine();

    ChatMessageContent chatResult = await completionService.GetChatMessageContentAsync(input,
            settings, kernel);

    Console.Write($"\n>>> Result: {chatResult}\n\n> ");
}