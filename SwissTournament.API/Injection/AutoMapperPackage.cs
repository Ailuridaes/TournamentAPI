using AutoMapper;
using SimpleInjector;
using SimpleInjector.Packaging;
using SwissTournament.Core.Domain;
using SwissTournament.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Injection
{
    public class AutoMapperPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            var config = new MapperConfiguration(cfg => {
                // Extract Tournament mappings to Profile if need more properties
                // cfg.AddProfiles(typeof(TournamentProfile));
                cfg.CreateMap<Tournament, TournamentDto>();
                cfg.CreateMap<Match, MatchDto>();
                cfg.CreateMap<Matchup, MatchupDto>();
                cfg.CreateMap<Player, PlayerDto>();
                cfg.CreateMap<Player, PlayerDto.WithScores>();
                cfg.CreateMap<Player, PlayerDto.WithMatchups>();
            });

            container.RegisterSingleton(config);
            container.Register(() => config.CreateMapper(container.GetInstance));
        }
    }
}