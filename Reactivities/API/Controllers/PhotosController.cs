using Application.Photos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotosController : BaseApiController
    {
        public async Task<IActionResult> Add([FromForm] Add.Command command)  //get Add class command
        {
            return HandleResult(await Mediator.Send(command));//exe command here
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)  //get param from rquest
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(string id) //get param from rquest
        {
            return HandleResult(await Mediator.Send(new SetMain.Command { Id = id }));
        }
    }
}