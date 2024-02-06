using AutoMapper;
using Service.Identity.DTOs;
using SharedDomain.Entities.Users;

namespace Service.Identity.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Member, UserResponse>()
                .ForMember(des=>des.isLockout, o=>o.MapFrom(s=>(s.LockoutEnd != null)))    
                .ReverseMap();
            
            CreateMap<Member, UserDetailResponse>()
                .ForMember(des=>des.DOB, o=>o.MapFrom(s=>s.DOB.Value.ToString("yyyy/MM/dd")))
                .ForMember(des=>des.CreatedDate, o=>o.MapFrom(s=>s.CreatedDate.ToString("yyyy/MM/dd")))
                .ForMember(des=>des.CreatedDate, o=>o.MapFrom(s=>s.LastModify.ToString("yyyy/MM/dd")))
                .ForMember(des => des.isLockout, o => o.MapFrom(s => (s.LockoutEnd != null)))
                .ReverseMap();

            CreateMap<AddStaffRequest, Member>().ReverseMap();
            CreateMap<SaveMemberDetailRequest, Member>().ReverseMap();


            CreateMap<CertificateRequest, Certificate>().ReverseMap();

            CreateMap<Certificate,CertificateResponse>().ReverseMap();
            CreateMap<Certificate,CertificateDetailResponse>().ReverseMap();
        }
    }
}
