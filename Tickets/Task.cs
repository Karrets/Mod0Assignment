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
        throw new NotImplementedException();
    }
}