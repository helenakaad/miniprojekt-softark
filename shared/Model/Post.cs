namespace Shared.Model;

public class Post
{
    public Post(string text, string name)
    {
        this.Text = text;
        this.Name = name;
    }

    public Post(string text, DateTime dateTime, string name, int votes)
    {
        this.Text = text;
        this.DateTime = dateTime;
        this.Name = name;
        this.Votes = votes;
    }

    public long PostId { get; set; }

    public string Text { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string Name { get; set; }
    public int Votes { get; set; } = 0;
    public List<Comments> Comments { get; set; } = new List<Comments>();
}


