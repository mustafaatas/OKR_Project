using API.DTO;
using API.DTO.Department;
using API.DTO.KeyResult;
using API.DTO.Objective;
using API.DTO.Team;
using AutoMapper;
using Core.Auth;
using Core.Models;

namespace API.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Music, MusicDTO>();
            CreateMap<Artist, ArtistDTO>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<KeyResult, KeyResultDTO>();
            CreateMap<Objective, ObjectiveDTO>();
            CreateMap<Team, TeamDTO>();

            // Resource to Domain
            CreateMap<MusicDTO, Music>();
            CreateMap<ArtistDTO, Artist>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<KeyResultDTO, KeyResult>();
            CreateMap<ObjectiveDTO, Objective>();
            CreateMap<TeamDTO, Team>();

            CreateMap<SaveMusicDTO, Music>();
            CreateMap<SaveArtistDTO, Artist>();
            CreateMap<SaveDepartmentDTO, Department>();
            CreateMap<SaveKeyResultDTO, KeyResult>();
            CreateMap<SaveObjectiveDTO, Objective>();
            CreateMap<SaveTeamDTO, Team>();

            CreateMap<UserSignUpDTO, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }
}
