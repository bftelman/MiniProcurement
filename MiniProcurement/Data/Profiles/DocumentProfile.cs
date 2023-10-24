using AutoMapper;
using MiniProcurement.Data.Contracts.InvoiceRequest;
using MiniProcurement.Data.Contracts.PurchaseRequest;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, GetPurchaseRequestDto>();
            CreateMap<CreatePurchaseRequestDto, Document>();
            CreateMap<CreateInvoiceDto, Document>();
            CreateMap<CreateInvoiceDto, InvoiceRequest>();
            CreateMap<CreatePurchaseRequestDto, PurchaseRequest>();
            CreateMap<CreatePurchaseRequestItemDto, PurchaseRequestItem>();
            CreateMap<PurchaseRequestItem, GetPurchaseRequestItemDto>();
            CreateMap<PurchaseRequest, GetPurchaseRequestDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DocumentId))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.Document.DocumentNumber))
                .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.Document.CreatedById))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.Document.CreatedOn));
            CreateMap<InvoiceRequest, GetInvoiceRequestDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DocumentId));

            CreateMap<CreateInvoiceItemDto, InvoiceRequestItem>()
                .ForMember(dest => dest.PurchaseRequestItemId, opt => opt.MapFrom(src => src.ItemId));

            CreateMap<UpdateInvoiceDto, InvoiceRequest>().ForPath(dest => dest.Document.CreatedOn, opt => opt.MapFrom(src => src.UpdatedOn));
            CreateMap<UpdatePurchaseRequestDto, PurchaseRequest>().ForPath(dest => dest.Document.CreatedOn, opt => opt.MapFrom(src => src.UpdatedOn));
            CreateMap<UpdatePurchaseRequestItemDto, PurchaseRequestItem>();
            CreateMap<UpdateInvoiceItemDto, InvoiceRequestItem>();

        }
    }
}
