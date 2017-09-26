using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OventService;

namespace OventApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public OventBusinessService OventService { get; }

        public ValuesController(OventBusinessService oventService)
        {
            this.OventService = oventService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            OventService.HandleLightsAsync("on", "salon");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
