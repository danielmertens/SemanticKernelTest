using System.ComponentModel.DataAnnotations;

namespace SemanticKernelTest.Web.Options;

public class AzureOpenAI
{
    [Required]
    public string ChatDeploymentName { get; set; } = string.Empty;

    [Required]
    public string Endpoint { get; set; } = string.Empty;

    [Required]
    public string ApiKey { get; set; } = string.Empty;
}
