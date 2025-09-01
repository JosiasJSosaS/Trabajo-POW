namespace Proyecto2.Models;

public class PageResultModel
{
    public int page { get; set; }
    public List<MovieModel> results { get; set; }
    public int total_pages { get; set; }
    public int total_results { get; set; }
}

