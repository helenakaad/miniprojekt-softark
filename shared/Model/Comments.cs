using System;
using System.Text.Json.Serialization;

namespace Shared.Model;

public class Comments
{
    
    public Comments(string text, string user)
    {
        this.Text = text;
        this.User = user;
    }

    [JsonConstructor]
    public Comments(string text, DateTime dateTime, string user, int votes)
    {
        this.Text = text;
        this.DateTime = dateTime;
        this.User = user;
        this.Votes = votes;

    }

    public long CommentsId { get; set; }

    public string Text { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string User { get; set; }
    public int Votes { get; set; } = 0;
}


