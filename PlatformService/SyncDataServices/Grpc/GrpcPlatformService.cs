﻿using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using PlatformService.Repositories;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IMapper _mapper;
        private readonly IPlatformRepository _repository;

        public GrpcPlatformService(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _repository.GetAllPlatforms();

            foreach (var plat in platforms) response.Platform.Add(_mapper.Map<GrpcPlatformModel>(plat));

            return Task.FromResult(response);
        }
    }
}