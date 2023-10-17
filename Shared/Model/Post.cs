
namespace Shared.Model;

public class Post
{
    public Post (string text, DateTime dateTime, string name, int upvote, int downvote)
    {
        this.Text = text;
        this.DateTime = dateTime;
        this.Name = name;
        this.Upvote = upvote;
        this.Downvote = downvote;
    }

    public Post()
    {
    }

    public long PostId { get; set; }

    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public string Name { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    public List<Comments> Comments { get; set; } = new List<Comments>();
}


 