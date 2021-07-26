using AutoMapper;
using ServiceRequest.Application.Features.ServiceRequest;
using ServiceRequest.Application.Features.ServiceRequest.Commands.CreateServiceRequest;
using ServiceRequest.Application.Features.ServiceRequest.Commands.DeleteServiceRequest;
using ServiceRequest.Application.Features.ServiceRequest.Commands.UpdateServiceRequest;
using ServiceRequest.Domain.Common;
using ServiceRequest.Domain.Entities;
using System;

namespace ServiceRequest.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Request, AddServiceRequestCommand>().ForMember(r => r.Status, op => op.MapFrom(x => x.CurrentStatus.ToString()));
            CreateMap<AddServiceRequestCommand, Request>().ForMember(r => r.CurrentStatus, op => op.MapFrom(o => setCaseOriginCode(o.Status)));
            CreateMap<Request, UpdateServiceRequestCommand>().ReverseMap();
            CreateMap<Request, DeleteServiceRequestCommand>().ReverseMap();
            CreateMap<Request, ServiceRequestVm>().ForMember(r => r.Status, op => op.MapFrom(x => x.CurrentStatus.ToString()));
        }

        public static int setCaseOriginCode(string CaseOriginCode)
        {
            int caseOriginCode = (int)(Enums.CurrentStatus)Enum.Parse(typeof(Enums.CurrentStatus), CaseOriginCode);
            return caseOriginCode;
        }
    }
}
