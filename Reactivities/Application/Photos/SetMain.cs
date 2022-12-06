using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class SetMain
    {
         public class Command : IRequest<Result<Unit>>
        {
                public String Id {get;set;}  //need to be call File!
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor; //get user by his claim (token)

            public Handler (DataContext context, IUserAccessor userAccessor  ){ //get the service that we will used here...
            _userAccessor = userAccessor;
            _context = context;

            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken) 
            {
                var user = await _context.Users.Include(p=>p.Photos) //get user with his photos objects
                .FirstOrDefaultAsync(x=>x.UserName == _userAccessor.GetUsername()); //like in Add class

                if(user == null) return null;

                var photo = user.Photos.FirstOrDefault(x=> x.Id ==request.Id); //get the photo by id

                if (photo==null) return null;

                var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

                if(currentMain!=null) currentMain.IsMain = false; //if found the currentMain main photo

                photo.IsMain = true; //make new main photo

                var success = await _context.SaveChangesAsync()>0;
                
                if(success) return Result<Unit>.Success(Unit.Value); //return when success

                return Result<Unit>.Failure("Problem setting main photo");
            }

       
        }


    }
}