using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Controllers
{
    [ApiController]
    [Authorize]
    //[Route("{controller}/{action}")]
    public class AdminApiController : ControllerBase
    {
        private PredictionsManager _predictionsManager;

        public AdminApiController(PredictionsManager predictionsManager) => _predictionsManager = predictionsManager;

        private ActionResult TryToDo(Action action)
        {
            try { action(); } catch (Exception ex) { Console.WriteLine(ex.Message); return StatusCode(StatusCodes.Status500InternalServerError); }

            return Ok();
        }

        [HttpGet]
        [Route("{action}")]
        public ActionResult GethtmlPredictions()
        {
            string output = "<table><tr><td><center><h7 style=\"color: white\">Number</h7></center></td><td><center><h7 style=\"color: white\">Prediction</h7></center></td><td><center><h7 style=\"color: white\">Edit</h7></center></td><td><center><h7 style=\"color: white\">Delete</h7></center></td></tr>";

            int num = 0;
            foreach (var item in _predictionsManager.GetAllPredictions())
            {
                output += $"<tr><td><center><h7 style=\"color: white\">{num + 1}</h7></center></td><td><input type=\"text\" id=\"{item.Id}\" value=\"{item.PredictionString}\" style=\"color: black\">" + $"</td><td><center><button style=\"color: black\" onclick=\"Edit({item.Id})\">✎</button></center></td><td><center><button style=\"color: red\" onclick=\"Delete({item.Id})\">✘</button></center></td></tr>";
                num++;
            }

            output += "</table>";

            return Ok(output);
        }

        [HttpPost]
        [Route("{action}")]
        public ActionResult AddPrediction([FromBody] Prediction prediction)
        {
            if (prediction == null || prediction.PredictionString == null) return StatusCode(StatusCodes.Status400BadRequest);

            return TryToDo(() => _predictionsManager.AddPrediction(prediction.PredictionString));
        }

        [HttpDelete]
        [Route("{action}")]
        public ActionResult DeletePrediction([FromBody] Prediction prediction)
        {
            if (prediction == null || prediction.Id == -1) return StatusCode(StatusCodes.Status400BadRequest);

            return TryToDo(() => _predictionsManager.RemovePrediction(prediction.Id));
        }

        [HttpPut]
        [Route("{action}")]
        public ActionResult EditPrediction([FromBody] Prediction prediction)
        {
            if (prediction == null || prediction.Id == -1 || prediction.PredictionString == null) return StatusCode(StatusCodes.Status400BadRequest);

            return TryToDo(() => _predictionsManager.Edit(prediction.Id, prediction.PredictionString));
        }
    }
}
