using AutoMapper;
using Service.Identity.Commands.Certificates;
using Service.Identity.Commands.Users;
using Service.Identity.DTOs;
using SharedDomain.Entities.Users;

namespace Service.Identity.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Member, UserResponse>().ReverseMap();
            CreateMap<Member, UserDetailResponse>()
                .ForMember(des=>des.DOB, o=>o.MapFrom(s=>((DateTime)s.DOB!)!.ToString("yyyy/MM/dd")))
                .ForMember(des=>des.CreatedDate, o=>o.MapFrom(s=>s.CreatedDate.ToString("yyyy/MM/dd")))
                .ForMember(des=>des.CreatedDate, o=>o.MapFrom(s=>s.LastModify.ToString("yyyy/MM/dd")))
                .ReverseMap();
            CreateMap<Member, CreateMemberCommand>().ReverseMap();


            CreateMap<SaveMemberDetailRequest, UpdateMemberCommand>().ReverseMap();

            CreateMap<UpdateMemberCommand, Member>().ReverseMap();

            CreateMap<CertificateRequest, UpdateCertificateCommand>().ReverseMap();
            CreateMap<CertificateRequest, AddCertificateCommand>().ReverseMap();

            CreateMap<UpdateCertificateCommand, Certificate>().ReverseMap();
            CreateMap<AddCertificateCommand, Certificate>().ReverseMap();

            CreateMap<Certificate,CertificateResponse>().ReverseMap();
        }
    }
}
