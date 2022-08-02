using API.DTO;
using API.DTO.Department;
using API.DTO.KeyResult;
using API.DTO.Objective;
using API.DTO.Team;
using API.DTO.UserDTO;
using AutoMapper;
using Core.Auth;
using Core.Models;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Music, MusicDTO>();
            CreateMap<Artist, ArtistDTO>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<KeyResult, KeyResultDTO>().ForMember(u => u.Interval, opt => opt.MapFrom(ur => ur.TargetValue - ur.StartValue)); ;
            CreateMap<Objective, ObjectiveDTO>();
            CreateMap<Team, TeamDTO>();
            CreateMap<User, UpdateUserDTO>();
            CreateMap<User, UserDTO>().ConvertUsing<UserDTOConverter>();

            // Resource to Domain
            CreateMap<MusicDTO, Music>();
            CreateMap<ArtistDTO, Artist>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<KeyResultDTO, KeyResult>();
            CreateMap<ObjectiveDTO, Objective>();
            CreateMap<TeamDTO, Team>();
            CreateMap<UserDTO, User>();

            CreateMap<SaveMusicDTO, Music>();
            CreateMap<SaveArtistDTO, Artist>();
            CreateMap<SaveDepartmentDTO, Department>();
            CreateMap<SaveKeyResultDTO, KeyResult>();
            CreateMap<UpdateKeyResultDTO, KeyResult>();
            CreateMap<SaveObjectiveDTO, Objective>();
            CreateMap<SaveTeamDTO, Team>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<SaveSubObjectiveDTO, Objective>();

            CreateMap<UserSignUpDTO, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }

    public class UserDTOConverter : ITypeConverter<User, UserDTO>
    {
        public UserDTO Convert(User source, UserDTO destination, ResolutionContext context)
        {
            return new UserDTO
            {
                Email = source.Email,
                FirstName = source.FirstName,
                LastName = source.LastName,
                RoleId = source.RoleId,
                TeamId = source.TeamId,
                DepartmentId = source?.Team?.DepartmentId
            };
        }
    }
}
