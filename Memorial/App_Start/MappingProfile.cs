using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            CreateMap<Applicant, ApplicantDto>();
            CreateMap<Deceased, DeceasedDto>();
            CreateMap<Deceased, DeceasedBriefDto>();
            CreateMap<FuneralCompany, FuneralCompanyDto>();
            CreateMap<CremationTransaction, CremationTransactionDto>();
            CreateMap<MiscellaneousTransaction, MiscellaneousTransactionDto>();
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<Receipt, ReceiptDto>();
            CreateMap<PaymentMethod, PaymentMethodDto>();


            // Dto to Domain
            CreateMap<ApplicantDto, Applicant>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<DeceasedDto, Deceased>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<FuneralCompanyDto, FuneralCompany>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CremationTransactionDto, CremationTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());
            CreateMap<MiscellaneousTransactionDto, MiscellaneousTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());
            CreateMap<InvoiceDto, Invoice>()
                .ForMember(i => i.IV, opt => opt.Ignore());
            CreateMap<ReceiptDto, Receipt>()
                .ForMember(i => i.RE, opt => opt.Ignore());
            CreateMap<PaymentMethodDto, PaymentMethod>()
                .ForMember(pm => pm.Id, opt => opt.Ignore());
        }
    }
}