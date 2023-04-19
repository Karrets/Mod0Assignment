using Assignment.Tickets.Classifier;

namespace Assignment.Tickets;

public class Enhancement : Ticket {
    public override TicketType TicketType => TicketType.Enhancement;

    public string Software {get; set;}
    public int Cost {get; set;}
    public string Reason {get; set;}
    public string Estimate {get; set;}
    
    public Enhancement(string ticketId, string summary, string status, string priority, string submitter, string assigned, string[] watching, string software, int cost, string reason, string estimate) :
        base(ticketId, summary, status, priority, submitter, assigned, watching) {
        Software = software;
        Cost = cost;
        Reason = reason;
        Estimate = estimate;
    }

    public Enhancement(string stringData) : 
        base(stringData) {
        string[] split = stringData.Split(',');

        Software = split[7];

        if(!int.TryParse(split[8], out int c))
            throw new InvalidDataException($"\"{split[8]}\" is not a valid entry for Cost.");
        Cost = c;
                
        Reason = split[9];
        Estimate = split[10];
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
            Software,
            Cost.ToString(),
            Reason,
            Estimate
        };

        //Join Method takes a separator, and an array, and returns a string seperated list by your delimiter.

        return string.Join(',', result);
    }
}