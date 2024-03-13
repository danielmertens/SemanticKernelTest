using Microsoft.SemanticKernel;
using SemanticKernelTest.Web.Models;
using System.ComponentModel;

namespace SemanticKernelTest.Web.Plugins
{
    public class AbsencePlugin
    {
        public static AbsenceDefinition[] _definitions = [
                new ()
                {
                    AbseceDefinitionId = 1,
                    Name = "Vacation"
                },
                new ()
                {
                    AbseceDefinitionId = 2,
                    Name = "Illness"
                }
            ];

        [KernelFunction, Description("Retrieve all absence definitions.")]
        public AbsenceDefinition[] GetAbsenceDefinitions()
        {
            return _definitions;
        }

        [KernelFunction, Description("Request an absence for the user.")]
        public AbsenceRequestResponse RequestAbsence(
            [Description("The absence definition to use for the request.")] int absenceDefinitionId,
            [Description("The date of the absence")] DateTime date)
        {
            if (!_definitions.Any(d => d.AbseceDefinitionId == absenceDefinitionId))
            {
                return ErrorResponse("The requested absence definition doesn't exist.");
            }

            var definition = _definitions.First(d => d.AbseceDefinitionId == absenceDefinitionId);
            Console.WriteLine($"Requesting absence {definition.Name} on {date:U}");

            return new AbsenceRequestResponse { Success = true };
        }

        private AbsenceRequestResponse ErrorResponse(string error)
        {
            return new AbsenceRequestResponse
            {
                Success = false,
                Errors = [error]
            };
        }
    }

    public class AbsenceRequestResponse
    {
        public bool Success { get; set; }
        public string[]? Errors { get; set; }
    }
}
