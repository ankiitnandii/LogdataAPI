using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogdataAPI.Models;
using LogdataAPI.Services;

namespace LogdataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityService activityService;

        public ActivityController()
        {
            activityService = new ActivityService();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(activityService.GetActivities());
        }


        [HttpGet("{id}")]
        public IActionResult GetProduct(string id)
        {
            return Ok(activityService.GetActivity(id));
        }

        [HttpPost]
        public IActionResult AddProduct(Activity activity)
        {
            int rowsAffected= activityService.AddActivity(activity);
            if (rowsAffected>0)
                return Ok("Added");
            else return Ok("Error occured");
        }

    }
}
