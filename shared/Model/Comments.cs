using System;
namespace Shared.Model;

public class Comments
{
    public Comments(string text, DateTime dateTime, string name, int votes)
    {
        this.Text = text;
        this.DateTime = dateTime;
        this.Name = name;
        this.Votes = votes;

    }

    public long CommentId { get; set; }

    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public string Name { get; set; }
    public int Votes { get; set; }
}


