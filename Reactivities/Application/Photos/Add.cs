using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class Add
    {
        public class Command : IRequest<Result<Photo>>
        {
                public IFormFile File {get;set;}  //need to be call File!
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
        {
        private readonly DataContext _context;
        private readonly IPhotoAccessor _PhotoAccessor;
        private readonly IUserAccessor _userAccessor; //get user by his claim (token)

            public Handler (DataContext context,IPhotoAccessor PhotoAccessor, IUserAccessor userAccessor  ){ //get the service that we will used here...
            _userAccessor = userAccessor;
            _PhotoAccessor = PhotoAccessor;
            _context = context;

            }
            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken) //what we do ...
            {
                var user =  await _context.Users.Include(p=>p.Photos) //get user collection of photos after locate the user...
                .FirstOrDefaultAsync(x=>x.UserName == _userAccessor.GetUsername()); //get user from database from user token
                
                if (user == null) return  null;

                var photoUploadResult = await _PhotoAccessor.AddPhoto(request.File); //try to upload photo 

                var photo = new Photo 
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                }; //update the url of the phto

                //if first photo upload
                if(!user.Photos.Any(x=>x.IsMain)) photo.IsMain =true;  //make main photo

                user.Photos.Add(photo); //add the photo to user

                var result = await _context.SaveChangesAsync()>0; //save our change in db

                if(result) return Result<Photo>.Success(photo);  //scc

                return Result<Photo>.Failure("problem adding photo"); //fail
             }
        }
    }

}