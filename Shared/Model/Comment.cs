namespace Shared.Model;

public class Comments
{
    public Comments(string text, DateTime dateTime, string name, int upvote, int downvote)
    {
        this.Text = text;
        this.DateTime = dateTime;
        this.Name = name;
        this.Upvote = upvote;
        this.Downvote = downvote;


    }

    public Comments()
    {

    }

  
    public long CommentId { get; set; }

    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public string Name { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
}


