namespace SemanticKernelTest.Web.Models
{
    public class Absence
    {
        public DateTime Date { get; set; }
        public int AbsenceDefinitionId { get; set; }
    }

    public class AbsenceDefinition
    {
        public int? AbseceDefinitionId { get; set; }
        public string Name { get; set; }
    }
}
