using System;
namespace Shared.Model;

public class Comments
{
    public Comments(string text, string name)
    {
        this.Text = text;
        this.Name = name;
    }

    public Comments(string text, DateTime dateTime, string name, int votes)
    {
        this.Text = text;
        this.DateTime = dateTime;
        this.Name = name;
        this.Votes = votes;

    }

    public long CommentsId { get; set; }

    public string Text { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string Name { get; set; }
    public int Votes { get; set; } = 0;
}


