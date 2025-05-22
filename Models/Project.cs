
public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public double Budget { get; set; }
    public Status Status { get; set; }

    public List<string> Contributors { get; set; } = new();
}