using Assignment.Tickets.Classifier;

namespace Assignment.Tickets;

public class Task : Ticket {
    public override TicketType TicketType => TicketType.Task;

    public string ProjectName {get; set;}
    public string DueDate {get; set;} //TODO: Should ideally be a represented as a date?

    public Task(string ticketId, string summary, string status, string priority, string submitter, string assigned, string[] watching, string projectName, string dueDate) :
        base(ticketId, summary, status, priority, submitter, assigned, watching) {
        ProjectName = projectName;
        DueDate = dueDate;
    }

    public Task(string stringData) :
        base(stringData) {
        string[] split = stringData.Split(',');

        ProjectName = split[7];
        DueDate = split[8];
    }

    public override string Serialize() {
        var result = new List<string> {
            TicketId,
            Summary,
            Status,
            Priority,
            Submitter,
            Assigned,
            string.Join('|', Watching),
            ProjectName,
            DueDate
        };

        //Join Method takes a separator, and an array, and returns a string seperated list by your delimiter.

        return string.Join(',', result);
    }

    protected override string HumanReadable() {
        
        return $"ID:        {TicketId}\n" +
               $"Summary:   {Summary}\n" +
               $"Status:    {Status}\n" +
               $"Priority:  {Priority}\n" +
               $"Submitter: {Submitter}\n" +
               $"Assigned:  {Assigned}\n" +
               $"Watching:  {string.Join(", ", Watching)}\n" +
               $"Severity:  {ProjectName}\n" +
               $"Due Date:  {DueDate}";
    }
}