using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application;

using Application.Activities;

namespace API.Controllers
{
    //use mediator  => selct how to handle the requst
    //medaitor is centalzie the connection between classes? =>need look.... 
    public class ActivitiesController : BaseApiController //BaseApiController is provide the "frame" of the  Controller =>we make this class 
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await Mediator.Send(new List.Query()); //mediator injection from baseAPI class => List template
        }

        [HttpGet("{id}")]//activities/id
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id }); //=>details template
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)//IActionResult give access to object witout value but with status (200 404 ...)
        {
            return Ok(await Mediator.Send(new Create.Command { Activity = activity }));
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> EditActivity(Guid id,Activity activity)
        {
            activity.Id= id;
            return Ok(await Mediator.Send(new Edit.Command{Activity= activity}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity (Guid id){
            return Ok(await Mediator.Send(new Delete.Command{Id=id}));
        }

    }
}