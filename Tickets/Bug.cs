using Assignment.Tickets.Classifier;

namespace Assignment.Tickets;

using Classifier;

public class Bug : Ticket {
    public override TicketType TicketType => TicketType.Bug;
    public Severity Severity {get; set;}

    public Bug(string ticketId, string summary, string status, string priority, string submitter, string assigned, string[] watching, Severity severity) : 
        base(ticketId, summary, status, priority, submitter, assigned, watching) {
        Severity = severity;
    }

    public Bug(string stringData) : 
        base(stringData) {
        string[] split = stringData.Split(',');

        if(!Enum.TryParse(split[7], out Severity s))
            throw new InvalidDataException($"\"{split[7]}\" is not a valid identifier for Severity");
        
        Severity = s;
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
            Severity.ToString()
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
               $"Severity:  {Severity}";
    }
}