using System.Text;
using Assignment.Tickets.Classifier;

namespace Assignment;

using Tickets;

internal static class Program {
    static void Main() {
        TicketManager tMan = new("bugs.csv", "enhancements.csv", "tasks.csv");
        bool run = true;

        do {
            Console.WriteLine("*- Welcome Ticket Admin, Please Select A Task -*\n" +
                              "1. (R)ead all tickets from the ticket list.\n" +
                              "2. (S)earch for tickets in the ticket list.\n" +
                              "3. (W)rite to the ticket list.\n" +
                              "4. (E)xit the program");

            char userInput =
                (Console.ReadLine() ?? " ")[0]; //Substitute null values for space and get the first character.

            switch(userInput) {
                case '1':
                case 'R':
                case 'r':
                    foreach(var ticket in tMan.ReadAll()) {
                        Console.WriteLine(ticket);
                    }
                    break;
                case '2':
                case 'S':
                case 's':
                    throw new NotImplementedException();
                    break;
                case '3':
                case 'W':
                case 'w':
                    List<Ticket> toAdd = new();
                    bool addingTickets = true;

                    while(addingTickets) {
                        TicketType ticketType;
                        do {
                            Console.WriteLine("Enter a ticket type:");
                        } while(!Enum.TryParse(Console.ReadLine(), true, out ticketType));

                        List<string> ticketData = new();
                        Console.WriteLine("Enter a ticket ID:");
                        ticketData.Add(Console.ReadLine() ?? "");

                        Console.WriteLine("Enter a ticket summary:");
                        ticketData.Add(Console.ReadLine() ?? "");

                        Console.WriteLine("Enter a ticket status:");
                        ticketData.Add(Console.ReadLine() ?? "");

                        Console.WriteLine("Enter a ticket priority:");
                        ticketData.Add(Console.ReadLine() ?? "");

                        Console.WriteLine("Enter the person who submitted this ticket:");
                        ticketData.Add(Console.ReadLine() ?? "");

                        Console.WriteLine("Enter the person assigned to this ticket:");
                        ticketData.Add(Console.ReadLine() ?? "");

                        bool addingWatchers = true;
                        var watchList = new List<string>();

                        while(addingWatchers) {
                            Console.WriteLine("Enter a person watching this ticket:");
                            watchList.Add(Console.ReadLine() ?? "");

                            Console.WriteLine("Add another watcher? (y/n)");
                            if((Console.ReadLine() ?? "").ToUpper()[0] == 'N') addingWatchers = false;
                        }

                        ticketData.Add(string.Join('|', watchList));

                        //Begin ticket subtype specifics.
                        switch(ticketType) {
                            case TicketType.Bug:
                                Severity severity;
                                do {
                                    Console.WriteLine("Enter a bug severity: ");
                                } while(!Enum.TryParse(Console.ReadLine(), true, out severity));

                                ticketData.Add(severity.ToString());

                                toAdd.Add(new Bug(string.Join(',', ticketData)));

                                break;
                            case TicketType.Enhancement:
                                Console.WriteLine("Enter the software this feature request is for:");
                                ticketData.Add(Console.ReadLine() ?? "");
                                Console.WriteLine("Enter an estimated cost for the feature (Numeric Only):");
                                ticketData.Add(Console.ReadLine() ?? "");
                                Console.WriteLine("Enter the reason:");
                                ticketData.Add(Console.ReadLine() ?? "");
                                Console.WriteLine("Enter a time estimate (mm/dd/yyyy):");
                                ticketData.Add(Console.ReadLine() ?? "");
                                break;
                            case TicketType.Task:
                                Console.WriteLine("Enter the name of the task: ");
                                ticketData.Add(Console.ReadLine() ?? "");
                                Console.WriteLine("Enter the due-date of the task: ");
                                ticketData.Add(Console.ReadLine() ?? "");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("");
                        }

                        Console.WriteLine("Add another ticket? (y/n)");
                        if((Console.ReadLine() ?? "").ToUpper()[0] == 'N') addingTickets = false;
                    }

                    tMan.Write(toAdd);
                    break;
                case '4':
                case 'E':
                case 'e':
                    run = false;
                    break;
            }
        } while(run);
    }
}