using System.ComponentModel.DataAnnotations;

namespace Questao_2.Models
{
    public class ParamSearch
    {
        [Display(Name = "Ano")]
        public int? year { get; set; }
        [Display(Name = "Time 1")]
        public string? team1 { get; set; }
        [Display(Name = "Time 2")]
        public string? team2 { get; set; }
        [Display(Name = "Página")]
        public int? page { get; set; }
        public ResultSearch? resultData { get; set; }

    }
}
