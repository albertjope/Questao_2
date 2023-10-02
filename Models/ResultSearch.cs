namespace Questao_2.Models;

public class ResultSearch
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int total_pages { get; set; }
    public List<Competition>? data { get; set; }
}
