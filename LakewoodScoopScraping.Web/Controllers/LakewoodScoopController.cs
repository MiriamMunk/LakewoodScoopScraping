using LakewoodScoopScraper.Scraping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakewoodScoopScraper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LakewoodScoopController : ControllerBase
    {
        [Route("getNews")]
        [HttpGet]
        public List<Post> GetNews()
        {
           return Scraper.Scrape();
        }
    }
}
