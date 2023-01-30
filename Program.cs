using System.Collections;

namespace Assignment;

class Program {
    static void Main() {
        string file = "ticketData.csv";
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
                    if(!File.Exists(file)) {
                        Console.WriteLine($"Error: No ticket data file exists. ({file})");
                        break;
                    }

                    StreamReader sr = new StreamReader(file);

                    while(!sr.EndOfStream) {
                        string[] line = (sr.ReadLine() ?? "").Split(',');

                        for(int i = 0; i < line.Length; i++) {
                            if(i == line.Length - 1) {
                                String[] watchList = line[i].Split('|');

                                Console.WriteLine(watchList[0].PadLeft(12) + ',');
                                for(int j = 1; j < watchList.Length; j++) {
                                    Console.WriteLine(watchList[j].PadLeft(90) + ',');
                                }
                                
                                Console.WriteLine("-".PadLeft(91, '-'));
                                
                            } else {
                                Console.Write(line[i].PadLeft(12) + ',');
                            }
                        }
                    }
                    
                    Console.WriteLine();

                    sr.Close();
                    break;
                case '2':
                case 'W':
                case 'w':
                    bool addCsvTemplate = !File.Exists(file);
                    FileStream fs = new FileStream(file, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);

                    if(addCsvTemplate) sw.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching");

                    bool addingTickets = true;
                    List<Ticket> ticketList = new List<Ticket>();

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

                        ticketList.Add(new Ticket(ticketId, summary, status, priority, submitter, assigned,
                            watchList.ToArray()));

                        Console.WriteLine("Add another ticket? (y/n)");
                        if((Console.ReadLine() ?? "").ToUpper()[0] == 'N') addingTickets = false;
                    }

                    foreach(var ticket in ticketList) {
                        sw.WriteLine(ticket.ToString());
                    }

                    sw.Close();
                    fs.Close();
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