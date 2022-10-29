using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest //command => change value of database but return noting
        {
            public Activity? Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) //return value to how used that he finish
            {
                _context.Activities.Add(request.Activity); //add in memeory? we need in this point to save changes...
                await _context.SaveChangesAsync();
                return Unit.Value; //is the same to return nothing but we need to used it to let the api contoroler know that we finsh ...
            }
        }
    }
}