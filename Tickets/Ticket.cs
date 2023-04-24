using Assignment.Tickets.Classifier;

namespace Assignment.Tickets;

public abstract class Ticket {
    public abstract TicketType TicketType {get;}

    public string TicketId {get; set;}
    public string Summary {get; set;}
    public string Status {get; set;}
    public string Priority {get; set;}
    public string Submitter {get; set;}
    public string Assigned {get; set;}
    public string[] Watching {get; set;}

    protected Ticket(string ticketId, string summary, string status, string priority, string submitter, string assigned, string[] watching) {
        TicketId = ticketId;
        Summary = summary;
        Status = status;
        Priority = priority;
        Submitter = submitter;
        Assigned = assigned;
        Watching = watching;
    }

    protected Ticket(string stringData) {
        string[] split = stringData.Split(',');

        TicketId = split[0];
        Summary = split[1];
        Status = split[2];
        Priority = split[3];
        Submitter = split[4];
        Assigned = split[5];

        Watching = split[6].Split('|');
    }

    public override string ToString() {
        return HumanReadable();
    }

    public abstract string Serialize();
    protected abstract string HumanReadable();
}