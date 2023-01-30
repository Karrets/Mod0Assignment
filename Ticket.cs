namespace Assignment; 

public class Ticket {
    public string TicketId { get; set; }
    public string Summary { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string Submitter { get; set; }
    public string Assigned { get; set; }
    public string[] Watching { get; set; }

    public Ticket(string ticketId, string summary, string status, string priority, string submitter, string assigned, string[] watching) {
        TicketId = ticketId;
        Summary = summary;
        Status = status;
        Priority = priority;
        Submitter = submitter;
        Assigned = assigned;
        Watching = watching;
    }

    public Ticket(string stringData) {
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
        List<string> result = new List<string>();
        result.Add(TicketId);
        result.Add(Summary);
        result.Add(Status);
        result.Add(Priority);
        result.Add(Submitter);
        result.Add(Assigned);

        result.Add(String.Join('|', Watching));
        
        //Join Method takes a separator, and an array, and returns a string seperated list by your delimiter.

        return String.Join(',', result);
    }
}