using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HistoryMaps.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        [HttpGet]
        public string Get(string par)
        {
            return par.ToLower();
        } 
    }
}
