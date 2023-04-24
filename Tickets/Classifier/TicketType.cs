namespace Assignment.Tickets.Classifier;

public static class Extensions {
    public static Ticket Of(this TicketType type, string ticketSerial) {
        return type switch {
            TicketType.Bug => new Bug(ticketSerial),
            TicketType.Enhancement => new Enhancement(ticketSerial),
            TicketType.Task => new Task(ticketSerial),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid ticket type provided...")
        };
    } 
}

public enum TicketType {
    Bug,
    Enhancement,
    Task,
}