namespace Shared.Model;

public class Thread
{
    public Thread (string text, DateTime dateTime, string name, int votes)
    {
        this.Text = text;
        this.DateTime = dateTime;
        this.Name = name;
        this.Votes = votes;

    }

    public long ThreadId { get; set; }

    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public string Name { get; set; }
    public int Votes { get; set; }
}


