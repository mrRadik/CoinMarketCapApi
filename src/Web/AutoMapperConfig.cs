﻿using AutoMapper;
using BussinesFacade;
using Web.Models;

namespace Web
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<Data, CurrencyViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Quote.Usd.Price))
                .ForMember(dest => dest.LastUpdated, opts => opts.MapFrom(src => src.Quote.Usd.LastUpdated))
                .ForMember(dest => dest.MarketCap, opts => opts.MapFrom(src => src.Quote.Usd.MarketCap))
                .ForMember(dest => dest.PercentChange1H, opts => opts.MapFrom(src => src.Quote.Usd.PercentChange1H))
                .ForMember(dest => dest.PercentChange24H, opts => opts.MapFrom(src => src.Quote.Usd.PercentChange24H))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Logo))
                .ForMember(dest => dest.Symbol, opts => opts.MapFrom(src => src.Symbol))
                ;
        }
    }
}