namespace CodeRank.Web.Entities;

public class TestCase
{
    public string Name { get; set; }
    public string Input { get; set; }
    public string ExpectedOutput { get; set; }
    public string ActualOutput { get; set; }
    public bool Passed { get; set; }
}

