using Assignment.Tickets.Classifier;

namespace Assignment;

using Tickets;

public class TicketManager {
    private readonly string _bugFile;
    private readonly string _enhancementFile;
    private readonly string _taskFile;

    public TicketManager(string bugFile, string enhancementFile, string taskFile) {
        _bugFile = bugFile;
        _enhancementFile = enhancementFile;
        _taskFile = taskFile;
    }

    public void ReadAll() {
        foreach(var type in Enum.GetValues<TicketType>()) {
            Read(type);
        }
    }
    
    public void Read(TicketType type) {
        string file = type switch {
            TicketType.Bug => _bugFile,
            TicketType.Enhancement => _enhancementFile,
            TicketType.Task => _taskFile,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid ticket type.")
        };

        if(!File.Exists(file)) {
            Console.WriteLine($"Warn: No ticket data file exists. ({file})");
            return;
        }

        StreamReader sr = new(file);

        while(!sr.EndOfStream) {
            string[] line = (sr.ReadLine() ?? "").Split(',');

            for(int i = 0; i < line.Length; i++) {
                if(i == line.Length - 1) {
                    string[] watchList = line[i].Split('|');

                    Console.WriteLine(watchList[0].PadLeft(12) + ',');
                    for(int j = 1; j < watchList.Length; j++) {
                        Console.WriteLine(watchList[j].PadLeft(90) + ',');
                    }

                    Console.WriteLine("-".PadLeft(104, '-'));
                } else {
                    Console.Write(line[i].PadLeft(12) + ',');
                }
            }
        }

        Console.WriteLine();

        sr.Close();
    }

    public void Write(List<Ticket> tickets) {
        bool addCsvTemplateBug = !File.Exists(_bugFile);
        bool addCsvTemplateEnhancement = !File.Exists(_enhancementFile);
        bool addCsvTemplateTask = !File.Exists(_taskFile);
        
        using FileStream fsBug = new(_bugFile, FileMode.Append);
        using StreamWriter swBug = new(fsBug);
        using FileStream fsEnhancement = new(_enhancementFile, FileMode.Append);
        using StreamWriter swEnhancement = new(fsEnhancement);
        using FileStream fsTask = new(_taskFile, FileMode.Append);
        using StreamWriter swTask = new(fsTask);

        if(addCsvTemplateBug)
            swBug.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching,Severity");
        if(addCsvTemplateEnhancement)
            swEnhancement.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching,Software,Cost,Reason,Estimate");
        if(addCsvTemplateTask)
            swTask.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching,ProjectName,DueDate");

        foreach(var ticket in tickets) {
            switch(ticket.TicketType) {
                case TicketType.Bug:
                    swBug.WriteLine(ticket.Serialize());
                    break;
                case TicketType.Enhancement:
                    swEnhancement.WriteLine(ticket.Serialize());
                    break;
                case TicketType.Task:
                    swTask.WriteLine(ticket.Serialize());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ticket.TicketType), ticket.TicketType, "Invalid ticket type.");
            }
        }
    }
}