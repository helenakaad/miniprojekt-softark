using System.Text.Json.Serialization;

namespace Shared.Model;

public class Post
{
    
    public Post(string title, string user, string content)
    {
        this.Title = title;
        this.User = user;
        this.Content = content;
    }

    [JsonConstructor]
    public Post(string title, DateTime dateTime, string user, int votes, string content)
    {
        this.Title = title;
        this.DateTime = dateTime;
        this.User = user;
        this.Votes = votes;
        this.Content = content;
    }

    public long PostId { get; set; }

    public string Title { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string User { get; set; }
    public int Votes { get; set; } = 0;
    public string Content { get; set; }
    public List<Comments> Comments { get; set; } = new List<Comments>();
}


