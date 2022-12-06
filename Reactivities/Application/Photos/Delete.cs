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
    public class Delete
    {
            public class Command : IRequest<Result<Unit>> //retrn "nothing"
            {
                public string Id{get;set;}
            }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
        private readonly DataContext _context;
        private readonly IPhotoAccessor _PhotoAccessor;
        private readonly IUserAccessor _userAccessor; //get user by his claim (token)

            public Handler (DataContext context,IPhotoAccessor PhotoAccessor, IUserAccessor userAccessor  ){ //get the service that we will used here...
            _userAccessor = userAccessor;
            _PhotoAccessor = PhotoAccessor;
            _context = context;

            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken) 
            {
                var user = await _context.Users.Include(p=>p.Photos) //get user with his photos objects
                .FirstOrDefaultAsync(x=>x.UserName == _userAccessor.GetUsername()); //like in Add class

                if(user == null) return null;

                var photo = user.Photos.FirstOrDefault(x=> x.Id ==request.Id); //get the photo by id

                if (photo==null) return null;

                if(photo.IsMain) return Result<Unit>.Failure("You cannot delete your main photo"); //if main photo cannot delete

                var result = await _PhotoAccessor.DeletePhoto(photo.Id); //try to delete from Cloudinary

                if(result==null) return Result<Unit>.Failure("problem deleting photo from Cloudinary"); 

                user.Photos.Remove(photo); //try to delete from db
                
                var success = await _context.SaveChangesAsync() > 0;

                if(success) return Result<Unit>.Success(Unit.Value); //return when success

                return Result<Unit>.Failure("problem deleting photo from API");
                
                
            }
        }
    }
}