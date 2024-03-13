using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace SemanticKernelTest.Web.Plugins
{
    public class TimePlugin
    {
        [KernelFunction, Description("Get the current time")]
        public DateTimeOffset Time() => DateTimeOffset.Now;
    }
}
