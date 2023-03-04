using System.Collections;

namespace Assignment;

internal static class Program {
    static void Main() {
        TicketManager tMan = new TicketManager("ticketData.csv");
        bool run = true;

        do {
            Console.WriteLine("*- Welcome Ticket Admin, Please Select A Task -*\n" +
                              "1. (R)ead from the ticket list.\n" +
                              "2. (W)rite to the ticket list.\n" +
                              "3. (E)xit the program");

            char userInput =
                (Console.ReadLine() ?? " ")[0]; //Substitute null values for space and get the first character.

            switch(userInput) {
                case '1':
                case 'R':
                case 'r':
                    tMan.Read();
                    break;
                case '2':
                case 'W':
                case 'w':
                    List<Ticket> toAdd = new();
                    bool addingTickets = true;

                    while(addingTickets) {
                        Console.WriteLine("Enter a ticket ID (Numeric Only)");
                        string ticketId = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter a ticket summary.");
                        string summary = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter a ticket status.");
                        string status = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter a ticket priority.");
                        string priority = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter the person who submitted this ticket.");
                        string submitter = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter the person assigned to this ticket.");
                        string assigned = Console.ReadLine() ?? "";

                        bool addingWatchers = true;
                        List<string> watchList = new List<string>();

                        while(addingWatchers) {
                            Console.WriteLine("Enter a person watching this ticket.");
                            watchList.Add(Console.ReadLine() ?? "");

                            Console.WriteLine("Add another watcher? (y/n)");
                            if((Console.ReadLine() ?? "").ToUpper()[0] == 'N') addingWatchers = false;
                        }

                        toAdd.Add(new Ticket(ticketId, summary, status, priority, submitter, assigned,
                            watchList.ToArray()));

                        Console.WriteLine("Add another ticket? (y/n)");
                        if((Console.ReadLine() ?? "").ToUpper()[0] == 'N') addingTickets = false;
                    }

                    tMan.Write(toAdd);
                    break;
                case '3':
                case 'E':
                case 'e':
                    run = false;
                    break;
            }
        } while(run);
    }
}