using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao_2.Models;
using System.Net.Http.Headers;
using System.Web;

namespace Questao_2.Controllers
{
    public class PesquisaController : Controller
    {
        private ParamSearch model = new ParamSearch();

        // GET: PesquisaController
        public ActionResult Index()
        {
            model.resultData = new ResultSearch();
            model.resultData.data = new List<Competition> { new Competition() };
            return View(model);
        }

        
        // POST: PesquisaController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ParamSearch model)
        {
            var builder = new UriBuilder("https://jsonmock.hackerrank.com/api/football_matches");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["year"] = model.year.ToString();
            query["team1"] = model.team1; 
            query["team1"] = model.team1;
            query["page"] = model.page.ToString();
            builder.Query = query.ToString();
            string url = builder.ToString();

            using var client = new HttpClient();
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var resultado = response.Content.ReadFromJsonAsync<ResultSearch>().Result;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(resultado);
                ResultSearch resultadoCopy = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultSearch>(json);

                model.resultData = resultado;
                model.resultData.data = new List<Competition> { resultado.data[0] };

                model.resultData.data[0].totalgoalsyear = resultadoCopy.data.Select(x => x.team1goals).Sum(x => long.Parse(x)).ToString();
            }
            else
            {
                model.resultData = new ResultSearch();
                model.resultData.data = new List<Competition> { new Competition() };
            }
            return View(model);
        }

    }
}
