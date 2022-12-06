using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class Details
    {
        public class Query : IRequest<Result<Profile>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Profile>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper) //map user object to profile object
            {
                _mapper = mapper;
                _context = context;

            }

            public async Task<Result<Profile>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                .ProjectTo<Profile>(_mapper.ConfigurationProvider) //query the Name column of the Item table =>from user to profile use ConfigurationProvider? use mappingProfiles file**
                .SingleOrDefaultAsync(x =>x.Username == request.Username); //get the user

                if(user==null) return null ; 

                return Result <Profile>.Success(user);
            }
        }
    }
}