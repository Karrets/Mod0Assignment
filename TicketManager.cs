namespace Assignment;

public class TicketManager {
    private string _file;
    private List<Ticket> _ticketList;

    public TicketManager(string file) {
        _file = file;
        _ticketList = new List<Ticket>();
    }

    public List<Ticket> Read() {
        if(!File.Exists(_file)) {
            Console.WriteLine($"Error: No ticket data file exists. ({_file})");
            return new();
        }

        StreamReader sr = new StreamReader(_file);

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

        return _ticketList;
    }

    public void Write(List<Ticket> tickets) {
        bool addCsvTemplate = !File.Exists(_file);
        FileStream fs = new FileStream(_file, FileMode.Append);
        StreamWriter sw = new StreamWriter(fs);

        if(addCsvTemplate) sw.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching");

        foreach(var ticket in tickets) {
            sw.WriteLine(ticket.ToString());
        }

        sw.Close();
        fs.Close();
    }
}