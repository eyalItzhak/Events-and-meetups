using System;
using System.Threading.Tasks;
using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(Create.Command command)
        {
            var comment = await _mediator.Send(command); //after comment save in our db
            //sent evryone connected to the hub
            await Clients.Group(command.ActivityId.ToString())  //set to the right group (by activity)
                .SendAsync("ReceiveComment", comment.Value); //the name ReceiveComment => used from the client side?
        }

        public override async Task OnConnectedAsync() //to join group
        {
            var httpContext = Context.GetHttpContext(); 
            var activityId = httpContext.Request.Query["activityId"];//get the activity Id from httpcontext
            await Groups.AddToGroupAsync(Context.ConnectionId, activityId); //make the conetion...
            var result = await _mediator.Send(new List.Query{ActivityId = Guid.Parse(activityId)}); //sent the list of coment to user
            await Clients.Caller.SendAsync("LoadComments", result.Value);
        }
    }
}