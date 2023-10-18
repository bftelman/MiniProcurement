using AutoMapper;
using MiniProcurement.Data.Contracts.Document;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<CreateDocumentDto, DocumentBase>();
            CreateMap<DocumentBase, GetDocumentDto>();
            CreateMap<CreatePurchaseRequestDto, DocumentBase>();
            CreateMap<CreatePurchaseRequestDto, PurchaseRequestDocument>();
            CreateMap<PurchaseRequestDocumentItem, CreatePurchaseRequestDocumentItemDto>();
        }
    }
}
